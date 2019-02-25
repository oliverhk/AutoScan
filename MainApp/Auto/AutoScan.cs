using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility;
using System.Diagnostics;
using DBCtrl.Model;
using System.Drawing;
using System.Timers;
using System.Drawing.Imaging;
using HalconHelper;

namespace MainApp
{
    public class AutoScan:IAuto
    {
        #region 单例模式
        private static readonly Lazy<AutoScan> Lazy = new Lazy<AutoScan>(() => new AutoScan());
        public static AutoScan Instance { get { return Lazy.Value; } }
        private SystemConfig sysCfg = SystemConfig.Instance;
        private AutoScan()
        {
            Init();
        }
        #endregion
        #region Interface
        public bool Start()
        {
            IsAbort = false;
            IsScan = true;
            return true;
        }

        public bool Stop()
        {
            IsScan = false;
            return true;
        }
        
        public void AbortScan()
        {
            IsAbort = true;
            Stop();
        }
        public event UpdateMessageHander UpdateMessage;
        public event UpdateMoveStatHandler UpdateStat;
        public event UpdateJobHandler UpdateJob;
        #endregion

        #region 属性
        private Thread thrAutoScan;
        private bool IsScan = false;
        private bool IsAbort = false;
        private const long TIMEOUT = 10 * 1000;     //10S
        private AutoResetEvent evt_Auto = new AutoResetEvent(false);
        //rcp 
        private ScanRecipe scanRcp;
        private CameraRecipe camRcp;
        private ControlRecipe ctrlRcp;
        private bool IsChangeCamRcp = false;    //for check change 
        private bool IsChangeScanRcp = false;   //for check change 
        private bool IsChangeCtrlRcp = false;   //for check change
        //private float start_z_pos = -4.4501f;//6.747f;
        private float start_z_pos;      //start  postion from rcp set
        private int scan_cnt = 0;  
        public string LotId { get; set; }       //for proc lot id
        public string BatchId { get; set; }     //for proc batch
        private const int MAX_BUFFER_CNT = 16;  //for max buffer count
        private int gSwathIndex = 0;        //for temp swath index

        private System.Timers.Timer tmr_Swath_Evt = new System.Timers.Timer();

        #endregion
        #region InitFunc
        private void Init()
        {
            if (thrAutoScan == null)
            {
                thrAutoScan = new Thread(new ThreadStart(AutoProc));
                thrAutoScan.Name = "Thread Auto Scan";
                thrAutoScan.IsBackground = true;
                thrAutoScan.Start();                
            }            
        }
        private void AutoProc()
        {
            while(true)
            {
                if(IsScan)
                {
                    AutoProcFun();
                    evt_Auto.WaitOne();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        #endregion
        #region Auto Sacn
        private void AutoProcFun()
        {
            DateTime startTmr = DateTime.Now;
            long jobId = 0;
            try
            {
                bool isSetRect = false;
                MessageBody msgBody = new MessageBody();
                //check rcp
                JobStart(ref jobId);
                scanRcp = Manager.IRcp.GetScanRcp();                
                camRcp = Manager.IRcp.GetCameraRcp();
                ctrlRcp = Manager.IRcp.GetControlRcp();
                //set change flage
                IsChangeCamRcp = Manager.IRcp.IsChangeRcp;
                IsChangeCtrlRcp = Manager.IRcp.IsChangeRcp;
                IsChangeScanRcp = Manager.IRcp.IsChangeRcp;
                Manager.IRcp.IsChangeRcp = false;       //after set is false
                //start sacn    
                start_z_pos = scanRcp.ScanZPostion;     

                msgBody.type = MsgType.Information;
                msgBody.msg = "开始扫描";
                ThrowMessage(msgBody);

                //Manager.ICamCtrl.EndAcquisition();
                //1. auto focus
                isSetRect = AutoFocus();
                if (!isSetRect)
                {
                    LogHelper.AppLoger.Error("auto focus failed.");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "自动聚焦失败";
                    //ThrowMessage(msgBody);
                }
                //Manager.ICamCtrl.EndAcquisition();

                //2. run start pos.
                float x = scanRcp.ZeroX == 0 ? -2.5f : scanRcp.ZeroX;
                float y = scanRcp.ZeroY == 0 ? -10f : scanRcp.ZeroY;
                isSetRect = MoveAxisPos(x, y);
                if (!isSetRect)
                {
                    LogHelper.AppLoger.Error("move start pos failed. abort move");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "运动到起始位置异常,Abort";
                    ThrowMessage(msgBody);
                }
                //auto focus faild move z to targer pos
                if (!isSetRect)
                {
                    Manager.IMotorCtrl.Move(3, start_z_pos);
                    Thread.Sleep(20);
                    if (!IsOnPosZ(start_z_pos))
                    {
                        LogHelper.AppLoger.ErrorFormat("failed to move z pos.{0}", start_z_pos);
                    }
                }
                //set follow z
                //SetFollowZ();暂时屏蔽，20181213，顾叶俊
                //3. set camer rcp
                isSetRect = SetCamRcp();
                if(!isSetRect)
                {
                    LogHelper.AppLoger.Error("Set camera rcp failed.");
                    msgBody.type = MsgType.Error;
                     msgBody.msg = "设置相机参数失败";
                    ThrowMessage(msgBody);
                }
                //4. set devic rcp
                isSetRect = SetMoveRcp();
                if (!isSetRect)
                {
                    LogHelper.AppLoger.Error("Set move rcp failed.");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "设置控制参数失败";
                    ThrowMessage(msgBody);
                }
                //5. set trigger para 
                isSetRect = SetTriggerRcp();
                if(!isSetRect)
                {
                    LogHelper.AppLoger.Error("Set trigger rcp failed.");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "设置触发板参数失败";
                    ThrowMessage(msgBody);
                }                           
                //6. scan glass
                ScanFun();
                //send to camera ctrl grab end
                Manager.ICamCtrl.IsGrabEnd = true;
                Manager.IMotorCtrl.SetOcaOff();

                //6.end sacn ,move to home
                ThrowStat(EnumStat.HomePos);

                //7.move z to trigger
                while (true)
                {
                    if (Manager.ICamCtrl.CurrentSwathCount == 0)
                        break;
                    else
                        Thread.Sleep(100);
                }
                ////send to camera ctrl grab end
                //Manager.ICamCtrl.ReSetSwatchId();

                //if (scan_cnt==0)
                //{
                //    start_z_pos = start_z_pos - 0.012f;
                //}
                //else if(scan_cnt==1)
                //{
                //    start_z_pos = start_z_pos + 0.024f;
                //}
                //else
                //{
                //    start_z_pos = scanRcp.ScanZPostion;
                //}
                SystemStatus.Instance.ZStackID = "Z" + start_z_pos;
                BatchId= "Z" + start_z_pos;
                //float tmz = 6.8f;             
                Manager.IMotorCtrl.Move(3, start_z_pos);
                if (!IsOnPosZ(start_z_pos))
                {
                    LogHelper.AppLoger.ErrorFormat("failed to move z pos.{0}", start_z_pos);
                }

                TimeSpan ustT = DateTime.Now - startTmr;                
                msgBody.type = MsgType.Information;
                //msgBody.msg = string.Format("结束扫描,扫描用时:{0} 毫秒", ustT.TotalMilliseconds);
                msgBody.msg = "扫描结束";
                ThrowMessage(msgBody);
                ThrowStat(EnumStat.ScanFinish);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                TimeSpan ustT = DateTime.Now - startTmr;
                if (ustT.TotalMilliseconds > 10)
                    LogHelper.AppLoger.DebugFormat("Auto proc fun use time {0} mil sec",ustT.TotalMilliseconds);
                //IsScan = false;
                scan_cnt++;
                if (scan_cnt >= scanRcp.SwathColumns)
                {
                    IsScan = false;
                    Move2LoadPort();
                    MessageBody msgBody = new MessageBody();
                    msgBody.type = MsgType.Information;
                    msgBody.msg = "整体扫描结束";
                    ThrowMessage(msgBody);
                    ThrowStat(EnumStat.ScanFinish);
                    scan_cnt = 0;
                }
                else
                {
                    //send to camera ctrl grab end
                    Manager.ICamCtrl.ResetSwatchId();
                }
                JobEnd(jobId);
                Thread.Sleep(2000);
                evt_Auto.Set();
            }
        }
        private bool SetCamRcp()
        {
            bool rect = false;
            try
            {
                if (IsChangeCamRcp)
                {
                    SystemStatus.Instance.SaveImgFlag = camRcp.IsSaveImage == 1 ? true : false;
                    SystemStatus.Instance.ImageSavePath = string.IsNullOrEmpty(camRcp.ImagePath) ? "c:\\data" : camRcp.ImagePath;
                    //set camer rcp
                    rect = Manager.ICamCtrl.SetRoi(camRcp.OffsetX, camRcp.OffsetY, camRcp.Width, camRcp.Height);
                    rect = Manager.ICamCtrl.SetExposureTime(camRcp.ExposureTime);
                    //rect &= Manager.ICamCtrl.SetGain(camRcp.Gain);
                    rect &= Manager.ICamCtrl.SetTriggerMode(true, HWCtrl.CameraCtrl.TriggerType.Hardware);
                    IsChangeCamRcp = false;
                    Manager.ICamCtrl.Start();
                }                                
                rect = true;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool SetMoveRcp()
        {
            bool rect = false;
            try
            {
                if (scanRcp == null)
                    return rect;
            
                //set light io off
                Manager.IioCard.SetLightOnAlways(false);
                //set move speed.
                Manager.IMotorCtrl.SetSpeed(1, scanRcp.SpeedX);
                Manager.IMotorCtrl.SetSpeed(2, scanRcp.SpeedY);
                                
                Manager.IMotorCtrl.SetOcaOff();
                Manager.IPiz.SetDistance(DEFAULT_PIZ);        //set to zero pos
                rect = true;                
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool SetTriggerRcp()
        {
            bool rect = false;
            try
            {
                if (ctrlRcp == null)
                    return rect;
                if (IsChangeCtrlRcp)
                {                    
                    //set trigger ext light
                    rect = Manager.Ilight.SetDelayTime((int)ctrlRcp.LightDelayTime, 2);
                    rect = Manager.Ilight.SetPlusTime((int)ctrlRcp.LightPlusTime, 2);
                    //set tirgger exit cam
                    rect = Manager.Ilight.SetDelayTime38((int)ctrlRcp.CamDelayTime);
                    rect = Manager.Ilight.SetPlusTime38((int)ctrlRcp.CamPlusTime);
                    IsChangeCtrlRcp = rect;
                }else
                {
                    rect = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private void ScanFun()
        {
            try
            {
                bool cv_x = true;
                //int x_off = 3200;  //0.5mm ct
                int trigger_plus_width = (int)(0.651 * 6400);//(int)(0.217 * 6400);// (int)(0.217*6400);  //0.65 mm ct    5.30 =1/3 for test
                float swath_width = scanRcp.SwathWidth;   //22
                //float y_dis = 0.7f;   //g3u3 cmera
                float swath_height = scanRcp.SwathHeight;// 0.47f;     //bfs 3.45 us
                float start_pos_x = scanRcp.ZeroX;// -2.5f;
                float start_pos_y = scanRcp.ZeroY;// -10;
                float tmx = 0.5f;
                float tmy = 0;
                int swath_cnt = scanRcp.SwathRows;// 2;                
                float z_dis = 0.0007f;
                float tmz = 0f;

                string cv_oca = string.Format("OCA=0,-{0}",trigger_plus_width);
                string oca_off = "OCA=0";
                for(int i=0;i<swath_cnt;i++)
                {
                    gSwathIndex = i;
                    //get cur x
                    //start_pos_x = Manager.IMotorCtrl.GetAxisXPosUm() / 1000;
                    //start_pos_x = (float)Math.Round(start_pos_x, 2);                    
                    //1.move x to one swath 
                    ThrowStat(EnumStat.SwathStart);                    
                    cv_oca = string.Format("OCA=-16050,-{0}", trigger_plus_width);
                    if (cv_x)
                    {
                        cv_x = false;
                        tmx = start_pos_x - swath_width;
                        Manager.IMotorCtrl.SetOca(cv_oca);
                        Thread.Sleep(20);
                        //start timer
                        TimerStart();
                        Manager.IMotorCtrl.Move(1,tmx);
                        LogHelper.AppLoger.InfoFormat("move x,dis:{0},start:{1},width:{2}",tmx, start_pos_x, swath_width);                      
                        //Manager.IMotorCtrl.SetOca(oca_off);
                        
                    }
                    else
                    {
                        cv_x = true;
                        //cv_oca = string.Format("OCA=-156790,{0}", trigger_plus_width);
                        //70mmps，X0.651
                        cv_oca = string.Format("OCA=-154494,{0}", trigger_plus_width);
                        tmx = start_pos_x;
                        Manager.IMotorCtrl.SetOca(cv_oca);
                        Thread.Sleep(20);
                        //start timer
                        TimerStart();
                        Manager.IMotorCtrl.Move(1, tmx);
                        LogHelper.AppLoger.InfoFormat("move x,dis:{0},start:{1},width:{2}", tmx, start_pos_x, swath_width);
                        //Manager.IMotorCtrl.SetOca(oca_off);                        
                    }
                    //check is move to 
                    Thread.Sleep(100);                    
                    if(!IsOnPosX(tmx))
                    {
                        LogHelper.AppLoger.ErrorFormat("failed to move x pos.{0}", tmx);
                    }
                    Manager.IMotorCtrl.SetOca(oca_off);
                    Thread.Sleep(20);
                    ThrowStat(EnumStat.SwathEnd);                                
                    //2. move y
                    tmy = start_pos_y + (i+1) * swath_height;
                    Manager.IMotorCtrl.Move(2, tmy);
                    Thread.Sleep(50);                  
                    if (!IsOnPosY(tmy))
                    {
                        LogHelper.AppLoger.ErrorFormat("failed to move y pos.{0}", tmy);
                    }
                    //3.move z
                    //tmz = start_z_pos + (i+1) * z_dis;
                    //Manager.IMotorCtrl.Move(3, tmz);
                    //if (!IsOnPosZ(tmz))
                    //{
                    //    LogHelper.AppLoger.ErrorFormat("failed to move z pos.{0}", tmz);
                    //}
                    //change to piz to move z
                    //MoveZByPiz(i);暂时屏蔽，20181213顾叶俊
                    //send to camera ctrl
                    //if (SystemStatus.Instance.SaveImgFlag)
                    //{
                    //    Thread.Sleep(2000);
                    //}
                    //query is grab is end
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    while (SystemStatus.Instance.SaveImgFlag)
                    {
                        if (Manager.ICamCtrl.IsCurSwathGrabEnd)
                            break;
                        else
                            Thread.Sleep(100);
                        if (watch.ElapsedMilliseconds / 1000 > 120)  //>120 s grab is not end ,abort
                        {
                            LogHelper.AppLoger.ErrorFormat(" timeout z-stack:{0}", start_z_pos);
                            break;
                        }
                    }
                    watch.Stop();
                    Thread.Sleep(100);
                    Manager.ICamCtrl.SetSwathEnd();
                    while(true)
                    {
                        if (Manager.ICamCtrl.CurrentSwathCount < MAX_BUFFER_CNT)
                            break;
                        else
                            Thread.Sleep(30);
                    }
                    if (IsAbort)
                    {
                        MessageBody msgBody = new MessageBody();
                        msgBody.type = MsgType.Warning;
                        msgBody.msg = string.Format("设备紧急停止!");
                        break;
                    }
                }

            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }            
        }
        private bool MoveAxisPos(float x, float y)
        {
            bool rect = false;
            try
            {
                float curPos = 0;
                //move to start pos
                ThrowStat(EnumStat.StartPos);
                //Thread.Sleep(5);
                //set high speed.
                Manager.IMotorCtrl.SetSpeed(1, sysCfg.Move_Speed_X);
                Manager.IMotorCtrl.SetSpeed(2, sysCfg.Move_Speed_Y);
                //x axis
                Manager.IMotorCtrl.Move(1, x);
                //y axis
                Manager.IMotorCtrl.Move(2, y);

                Thread.Sleep(20);
                if (!IsOnPosX(x))
                {
                    LogHelper.AppLoger.ErrorFormat("failed move to x pos.{0}",x);
                }                
                Thread.Sleep(20);
                if (!IsOnPosY(y))
                {
                    LogHelper.AppLoger.ErrorFormat("failed move to y pos.{0}", y);
                }
                rect = true;
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool AutoFocus()
        {
            bool rect = false;
            try
            {
                //if (scan_cnt != 0)
                //    return false;     //not first ,do not foucs
               // if (camRcp.IsSaveImage==0)
               //     return true;
                MessageBody msgBody = new MessageBody();
                msgBody.type = MsgType.Information;
                msgBody.msg = "开始当前片聚焦位置.";
                ThrowMessage(msgBody);                

                //1. move to pos
                float x = -13.0f;
                float y = -6.0f;
                rect = MoveAxisPos(x, y);
                if (!rect)
                {
                    LogHelper.AppLoger.Error("move focus pos failed. abort move");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "运动到聚焦位置异常,Abort";
                    ThrowMessage(msgBody);
                    return false;
                }

                //2. set camera rcp            
                int roiX =832;
                int roiY =472;
                int width = 256;
                int height = 256;
                

                rect = Manager.ICamCtrl.SetRoi(roiX,roiY,width,height);
                if (!rect)
                {
                    LogHelper.AppLoger.Error("set camera roi failed. abort move");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "设置相机ROI区域失败,Abort";
                    ThrowMessage(msgBody);
                    return rect;
                }
                float exposueT = 5;
                rect = Manager.ICamCtrl.SetExposureTime(exposueT);
                if (!rect)
                {
                    LogHelper.AppLoger.Error("set camera exposure time failed. abort move");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "设置相机曝光时间失败,Abort";
                    ThrowMessage(msgBody);
                    return rect;
                }
                rect = Manager.ICamCtrl.SetTriggerMode(true, HWCtrl.CameraCtrl.TriggerType.Software);
                if (!rect)
                {
                    LogHelper.AppLoger.Error("set camera trigger mode failed. abort move");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "设置相机触发机制失败,Abort";
                    ThrowMessage(msgBody);
                    return rect;
                }

                //wait change to light 
                rect = Manager.IioCard.SetLightOnAlways(true);
                if (!rect)
                {
                    LogHelper.AppLoger.Error("set light on errror ,abort");
                    msgBody.type = MsgType.Error;
                    msgBody.msg = "设置光源常亮失败,Abort";
                    ThrowMessage(msgBody);
                    return rect;
                }

                Manager.ICamCtrl.BeginAcquisition();

                //3. loop grab img ,3pics calc,wait focs
                List<IntPtr> lstImg = new List<IntPtr>();
                int lopCnt = 0;
                FocusHelper.QFocusInit(256, 256, 20.0, 1.0);
                FocusHelper.Direction = 0;
                FocusHelper.CurZPos = 8.8;
                FocusHelper.CurGap = 3 * 8.8;
                while (true)
                {
                    lstImg = GrabThreeImage((float)FocusHelper.CurZPos, FocusHelper.Direction, (float)FocusHelper.CurGap);
                    bool f_rect = FocusHelper.QFocus(lstImg[0], lstImg[1], lstImg[2]);
                    for (int i = 0; i < 3; i++)
                    {
                        imgs[i].UnlockBits(imgData[i]);
                    }
                    if (f_rect==true)
                    {
                        //收敛,return
                        LogHelper.AppLoger.DebugFormat("manual focus successed.return");
                        break;
                    }            

                    lopCnt++;
                }
                LogHelper.AppLoger.DebugFormat("focus completed,postion:{0},count:{1}", FocusHelper.CurZPos, lopCnt);

                start_z_pos = scanRcp.ScanZPostion + (float)FocusHelper.CurZPos / 1000;

                //4. move pize get dist.                
                Manager.IMotorCtrl.Move(3, start_z_pos);
                if (!IsOnPosZ(start_z_pos))
                {
                    LogHelper.AppLoger.ErrorFormat("failed to move z pos.{0}", start_z_pos);
                }
               
                //set light off
                Manager.IioCard.SetLightOnAlways(false);

                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        Bitmap []imgs =new Bitmap[3];
        BitmapData []imgData=new BitmapData[3];
        IntPtr []pImgs=new IntPtr[3];
        private List<IntPtr> GrabThreeImage(float z_dist,int direc,float fstep)
        {
            List<IntPtr> rect = new List<IntPtr>();
            try
            {
                float dist;
                float len_na = 2f * 8.8f; //um

                if (fstep < 0)
                    len_na = fstep;
                
                for (int i = 0; i < 3; i++)
                {
                    //1. move to stat pos
                    if(direc>=0)
                        dist = z_dist + i * len_na;
                    else
                        dist = z_dist - i * len_na;
                    Manager.IPiz.SetDistance(dist);
                    Thread.Sleep(100);
                    //2. trigger camera.                    
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    //3. grab img
                    imgs[i] = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);
                    //4. add to list
                    string filename = string.Format("C:\\Users\\ruiqian\\Desktop\\FocusLog\\Org\\{0}.bmp", i);
                    imgs[i].Save(filename);
                    imgData[i] = imgs[i].LockBits(new Rectangle(0, 0, 128, 128), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                    pImgs[i] = imgData[i].Scan0;
                    rect.Add(pImgs[i]);

                }
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool IsOnPosX(float trigpos)
        {
            bool rect = false;
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                float curPos;//= Manager.IMotorCtrl.GetAxisXPosUm()/1000;
                while (true)
                {
                    if (Manager.IMotorCtrl.IsAxisRun(1))
                    {
                        Thread.Sleep(10);
                        continue;
                    }else
                    {
                        break;
                    }
                    //curPos = Manager.IMotorCtrl.GetAxisXPosUm() / 1000;20181213,顾叶俊
                    curPos = Manager.IMotorCtrl.GetAxisXPosUmEx() / 1000;
                    if (IsAbort)
                        break;
                    if (Math.Abs(trigpos - curPos) < 1)
                        break;
                    if(watch.ElapsedMilliseconds>TIMEOUT)
                    {
                        watch.Stop();
                        LogHelper.AppLoger.ErrorFormat("on x pos check time out.pls check.");                        
                        MessageBody msg = new MessageBody();
                        msg.type = MsgType.Error;
                        msg.msg = "IsOnPostX Time out .Plase check";
                        ThrowMessage(msg);
                        break;
                    }
                }
                rect = true;
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool IsOnPosY(float trigpos)
        {
            bool rect = false;
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                float curPos;//= Manager.IMotorCtrl.GetAxisYPosUm() / 1000; ;
                while (true)
                {
                    if (Manager.IMotorCtrl.IsAxisRun(2))
                    {
                        Thread.Sleep(10);
                        continue;
                    }else
                    {
                        break;
                    }
                    //curPos =  Manager.IMotorCtrl.GetAxisYPosUm() / 1000; ;20181213，顾叶俊
                    curPos = Manager.IMotorCtrl.GetAxisYPosUmEx() / 1000; ;
                    if (IsAbort)
                        break;
                    if (Math.Abs(trigpos - curPos) < 1)
                        break;
                    if (watch.ElapsedMilliseconds > TIMEOUT)
                    {
                        watch.Stop();
                        LogHelper.AppLoger.ErrorFormat("on y pos check time out.pls check.");                        
                        MessageBody msg = new MessageBody();
                        msg.type = MsgType.Error;
                        msg.msg = "IsOnPostY Time out .Plase check";
                        ThrowMessage(msg);                        
                        break;
                    }
                }
                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool IsOnPosZ(float trigpos)
        {
            bool rect = false;
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                float curPos;// = Manager.IMotorCtrl.GetAxisZPosUm() / 1000; ;
                while (true)
                {
                    if (Manager.IMotorCtrl.IsAxisRun(3))
                    {
                        Thread.Sleep(5);
                        continue;
                    }else
                    {
                        break;
                    }
                    //curPos = Manager.IMotorCtrl.GetAxisZPosUm() / 1000; 20181213，顾叶俊
                    curPos = Manager.IMotorCtrl.GetAxisZPosUmEx() / 1000; 
                    if (IsAbort)
                        break;
                    if (Math.Abs(trigpos - curPos) < 1)
                        break;
                    if (watch.ElapsedMilliseconds > TIMEOUT)
                    {
                        watch.Stop();
                        LogHelper.AppLoger.ErrorFormat("on z pos check time out.pls check.");
                        MessageBody msg = new MessageBody();
                        msg.type = MsgType.Error;
                        msg.msg = "IsOnPostZ Time out .Plase check";
                        ThrowMessage(msg);
                        break;
                    }
                }
                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private void Move2LoadPort()
        {
            try
            {
                //set speed
                Manager.IMotorCtrl.SetSpeed(1, sysCfg.Move_Speed_X);
                Thread.Sleep(20);
                Manager.IMotorCtrl.SetSpeed(2, sysCfg.Move_Speed_Y);
                Thread.Sleep(20);
                //move load
                Manager.IMotorCtrl.Move(1, sysCfg.Load_Pos_X);
                Manager.IMotorCtrl.Move(2, sysCfg.Load_Pos_Y);
                LogHelper.AppLoger.DebugFormat("move glass to load port.{0},{1}",sysCfg.Load_Pos_X,sysCfg.Load_Pos_Y);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void JobStart(ref long job_id)
        {
            try
            {
                DBCtrl.DAO.JobsDao dao = new DBCtrl.DAO.JobsDao();
                long jobid = dao.MaxJobId();
                job_id = jobid;
                Jobs job = new Jobs();
                job.JobId = jobid;
                //job.Name = SystemStatus.Instance.ZStackID;
                job.RecipeId = RecipeManager.Instance.RecipeId;
                job.LotId = LotId;
                job.BatchId = BatchId;
                job.StartTime = DateTime.Now;
                job.Operator = ConstDef.UserID;
                job.EqptId = sysCfg.EquipmentId;
                job.EqptNo = sysCfg.EquipmentNum.ToString();
                job.Hospital = sysCfg.Hospital;

                bool rect = dao.Insert(job);
                if (!rect)
                    LogHelper.AppLoger.Debug("Inser job failed.");
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void JobEnd(long job_id)
        {
            try
            {
                DBCtrl.DAO.JobsDao dao = new DBCtrl.DAO.JobsDao();
                Jobs job = dao.GetJob(job_id);
                job.EndTime = DateTime.Now;
                
                bool rect = dao.Update(job);
                if (!rect)
                    LogHelper.AppLoger.Debug("Inser job failed.");
                ThrowJob(job);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
        #region evt
        private void ThrowMessage(MessageBody message)
        {
            if (UpdateMessage != null)
            {
                foreach (UpdateMessageHander t in UpdateMessage.GetInvocationList())
                {
                    t.BeginInvoke(message, null, null);
                }
            }
        }
        #endregion
        #region stat
        private void ThrowStat(EnumStat stat)
        {
            if (UpdateStat != null)
            {
                foreach (UpdateMoveStatHandler t in UpdateStat.GetInvocationList())
                {
                    t.BeginInvoke(stat, null, null);
                }
            }
        }
        #endregion
        #region job
        private void ThrowJob(Jobs job)
        {
            if (UpdateJob != null)
            {
                foreach (UpdateJobHandler t in UpdateJob.GetInvocationList())
                {
                    t.BeginInvoke(job, null, null);
                }
            }
        }
        #endregion
        #region timer to event z pos
        private float[] offsetZ = { };  //save z pre map
        private int axisZIndex = 0;     //z index
        private List<float> lstY = new List<float>();   //start swath z value.
        private List<float> lstY2 = new List<float>();  //start swath z value.
        private const float DEFAULT_PIZ = 25;   //65
        private const float DEFAULT_PIZ2 = 0;  //45

        private void SetFollowZ()
        {
            //speed 10mm/s y = 22mm ,total time 2.2s div 10 
            SetTimer(400);
            SetLstValue();    
        }
        private void SetLstValue()
        {
            try
            {
                //add swath z value
                #region add swath z value
                lstY.Clear();
                for (int i = 0;i<41;i++)
                {
                    lstY.Add(DEFAULT_PIZ-(i * 0.95f));
                }
                lstY2.Clear();
                for(int i= 0;i<41;i++)
                {
                    lstY2.Add(DEFAULT_PIZ2 - (i * 0.7f));
                }               
                #endregion

            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void MoveZByPiz(int swatchIndx)
        {
            try
            {
                TimerStop();
                float vz = 0f;
                if(swatchIndx %2 ==0)
                {
                    vz = swatchIndx > 40 ? 0.7f : lstY[swatchIndx];
                    Manager.IPiz.SetDistance(vz);
                    Thread.Sleep(150);
                }
                else
                {
                    vz = swatchIndx > 40 ? 0.07f : lstY2[swatchIndx];
                }
                //vz += (axisZIndex+1 * 0.7f);
                //Manager.IPiz.SetDistance(vz);
                LogHelper.AppLoger.DebugFormat("move z dis ,move swath index:{0}.value:{1}",swatchIndx,vz);
                //Thread.Sleep(50);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void SetTimer(int elapseT)
        {
            tmr_Swath_Evt = new System.Timers.Timer(elapseT);
            tmr_Swath_Evt.Elapsed += tmr_Elapsed;
        }
        private void TimerStart()
        {
            axisZIndex = 0;
            tmr_Swath_Evt.Start();
        }
        private void TimerStop()
        {            
            tmr_Swath_Evt.Stop();
        }
        private void tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (gSwathIndex > lstY.Count)
                    return;
                float axis=0f;// = lstY[gSwathIndex];
                //move axis z ,now is axis z
                //builder axis map
                float vt = 0f;
                if(gSwathIndex%2==0)
                {
                    // odd
                    axis = lstY[gSwathIndex];
                    vt = axis + (axisZIndex * (lstY2[gSwathIndex] - lstY[gSwathIndex]) / 5);
                }
                else
                {
                    //ev
                    axis = lstY2[gSwathIndex];
                    vt = axis - (axisZIndex * (lstY2[gSwathIndex] - lstY[gSwathIndex]) / 5);
                }                
                //Manager.IPiz.SetDistance(vt);
                axisZIndex++;
                //LogHelper.AppLoger.DebugFormat("timer to move z.axisZindex:{0},value:{1}",axisZIndex,vt);                              
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
    }
}
