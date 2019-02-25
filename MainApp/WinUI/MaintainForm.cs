using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;
using Utility;
using System.Threading;
using FlyCapture2Managed;
using MainApp.Src;
using System.Diagnostics;
using MathWorks.MATLAB.NET.Arrays;
using System.IO;
using System.Drawing.Imaging;
using HalconHelper;

namespace MainApp.WinUI
{
    public partial class MaintainForm : Form
    {
        #region property
        private SystemConfig sysCfg = SystemConfig.Instance;
        private AutoFocus autoFocus;
        float MAX_Z_LEN = 7.0f;
        #endregion
        #region instance
        //Singleton instance
        private static MaintainForm _instance;
        public static MaintainForm Instance => _instance ?? (_instance = new MaintainForm());
        #endregion
        public MaintainForm()
        {
            InitializeComponent();
            HWCtrl.GalilTool.UpdateMessage += new HWCtrl.GalilTool.UpdateMessageHander(onMessage);
            Src.SocketManager.UpdateMessage += new Src.SocketManager.UpdateMessageHander(socketMessage);

            try
            {
                autoFocus = new AutoFocus(this);
                //this.myDll2 = new GfPremap.GfPremap();
                //this.myDll1 = new GlobalFocusOfSinglePointROIForDll.Class1();                
            }
            catch (Exception ex)
            { LogHelper.AppLoger.Error(ex); }
        }

        private void GalilTool_UpdateMessage(object msg)
        {
            throw new NotImplementedException();
        }

        private void btnHomeAll_Click(object sender, EventArgs e)
        {
            Manager.IMotorCtrl.ExecHomeALL();
        }
        private void btnInit_Click(object sender, EventArgs e)
        {
            //if (Manager.IMotorCtrl.Connect(sysCfg.Control_IP))
            //{
            //    Dialogs.Show("连接成功!!");
            //    timer1.Start();

            //}
            //else
            //{
            //    Dialogs.Show("连接失败!!");
            //}
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Manager.IMotorCtrl.StopAxis();
        }

        private void btnXHome_Click(object sender, EventArgs e)
        {
            Manager.IMotorCtrl.ExecHome(1);
        }

        private void btnYHome_Click(object sender, EventArgs e)
        {
            Manager.IMotorCtrl.ExecHome(2);
        }

        private void btnZHome_Click(object sender, EventArgs e)
        {
            Manager.IMotorCtrl.ExecHome(3);
        }
        #region evt onmessage
        private void onMessage(object message)
        {
            try
            {
                string[] str = message.ToString().Split('\n');
                foreach (var v in str)
                {
                    //this.lsbMsg.Items.Insert(0, message);//will cause error  marked by zhian 20180716
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion

        private void btnClearMsg_Click(object sender, EventArgs e)
        {
            lsbMsg.Items.Clear();
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            Manager.IMotorCtrl.UnLoadGlass(sysCfg.Load_Pos_X);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Manager.IMotorCtrl.LoadGlass();
        }
        private string XCount2UM(string count)
        {
            string ret = string.Empty;
            try
            {
                var p = float.Parse(count) * ConstDef.XCountUnit;
                ret = ((p / 1000)).ToString("f2");
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        private string YCount2MM(string count)
        {
            string ret = string.Empty;
            try
            {
                var p = float.Parse(count) * ConstDef.YCountUnit;
                ret = ((p / 1000)).ToString("f2");
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        private string ZCount2MM(string count)
        {
            string ret = string.Empty;
            try
            {
                var p = float.Parse(count) * ConstDef.ZCountUnit;
                ret = ((p / 1000)).ToString("f2");
                SystemStatus.ZPos = (float)((float)(p / 1000) / 1000);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpTime();
            //txtXpos.Text = Manager.IMotorCtrl.GetAxisXPosUm().ToString();20181213,顾叶俊
            //txtYpos.Text = Manager.IMotorCtrl.GetAxisYPosUm().ToString();
            //txtZpos.Text = Manager.IMotorCtrl.GetAxisZPosUm().ToString();
            //txtbox_Xpos.Text = Manager.IMotorCtrl.GetAxisXPosUm().ToString();
            //txtbox_Ypos.Text = Manager.IMotorCtrl.GetAxisYPosUm().ToString();
            //txtbox_ZPos.Text = Manager.IMotorCtrl.GetAxisZPosUm().ToString();
            txtXpos.Text = Manager.IMotorCtrl.GetAxisXPosUmEx().ToString();
            txtYpos.Text = Manager.IMotorCtrl.GetAxisYPosUmEx().ToString();
            txtZpos.Text = Manager.IMotorCtrl.GetAxisZPosUmEx().ToString();
            txtbox_Xpos.Text = Manager.IMotorCtrl.GetAxisXPosUmEx().ToString();
            txtbox_Ypos.Text = Manager.IMotorCtrl.GetAxisYPosUmEx().ToString();
            txtbox_ZPos.Text = Manager.IMotorCtrl.GetAxisZPosUmEx().ToString();
            //txtXpos.BackColor = Manager.IMotorCtrl..IsAxisRun(1) ? Color.Red : Color.White;
            //txtYpos.BackColor = Manager.IMotorCtrl..IsAxisRun(2) ? Color.Red : Color.White;
        }
        /// <summary>
        /// Enable all Motor param control in each TabPages 0711 by zhian
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSpeed_TextChanged(object sender, EventArgs e)
        {

            //X Speed
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                if (!string.IsNullOrEmpty(txtXspeed.Text))
                {
                    float p = float.Parse(txtXspeed.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(1, p);
                }
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                if (!string.IsNullOrEmpty(txtbox_SpeedX.Text))
                {
                    float p = float.Parse(txtbox_SpeedX.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(1, p);
                }
            }
            //Y Speed
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                if (!string.IsNullOrEmpty(txtYspeed.Text))
                {
                    float p = float.Parse(txtYspeed.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(2, p);
                }
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                if (!string.IsNullOrEmpty(txtbox_SpeedX.Text))
                {
                    float p = float.Parse(txtbox_SpeedX.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(1, p);
                }
            }
            //Z Speed
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                if (!string.IsNullOrEmpty(txtZspeed.Text))
                {
                    float p = float.Parse(txtZspeed.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(3, p);
                }
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                if (!string.IsNullOrEmpty(txtbox_SpeedZ.Text))
                {
                    float p = float.Parse(txtbox_SpeedZ.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(3, p);
                }
            }
        }

        private void btnXLeft_Click(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                var dist = float.Parse(txtXpos.Text) - float.Parse(txtXstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(1, dist);
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                var dist = float.Parse(txtbox_Xpos.Text) - float.Parse(txtbox_StepX.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(1, dist);
            }
        }

        private void btnXRight_Click(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                var dist = float.Parse(txtXpos.Text) + float.Parse(txtXstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(1, dist);
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                var dist = float.Parse(txtbox_Xpos.Text) + float.Parse(txtbox_StepX.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(1, dist);
            }
        }

        private void btnYleft_Click(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                var dist = float.Parse(txtYpos.Text) - float.Parse(txtYstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(2, dist);
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                var dist = float.Parse(txtbox_Ypos.Text) - float.Parse(textBox_StepY.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(2, dist);
            }
        }

        private void btnYRight_Click(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                var dist = float.Parse(txtYpos.Text) + float.Parse(txtYstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(2, dist);
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                var dist = float.Parse(txtbox_Ypos.Text) + float.Parse(textBox_StepY.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(2, dist);
            }
        }

        private void btnZLeft_Click(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                var dist = float.Parse(txtZpos.Text) - float.Parse(txtZstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(3, dist);
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                var dist = float.Parse(txtbox_ZPos.Text) - float.Parse(textBox_StepZ.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(3, dist);
            }
        }

        private void btnZRight_Click(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[0])
            {
                var dist = float.Parse(txtZpos.Text) + float.Parse(txtZstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(3, dist);
            }
            else if (materialTabControl1.SelectedTab == materialTabControl1.TabPages[2])
            {
                var dist = float.Parse(txtbox_ZPos.Text) + float.Parse(textBox_StepZ.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.Move(3, dist);
            }
        }

        #region 循环扫描
        private bool isNext = false;
        private bool isStop = false;
        private bool isRun = false;
        private void socketMessage(object message)
        {
            if (message.ToString().Contains("OK"))
            {
                isNext = true;
            }
            else
            {
                //lsbMsg.Items.Add(message);
            }
        }
        private void LoopRun()
        {
            if (isRun)
                return;
            isRun = true;
            try
            {
                int row = int.Parse(txtLoopXCnt.Text);
                int column = int.Parse(txtLoopYCnt.Text);
                float posx = float.Parse(txtStartPosx.Text);
                float posy = float.Parse(txtStartPosy.Text);
                float step = float.Parse(txtLoopxStep.Text);
                float yst = float.Parse(txtLoopyStep.Text);

                //int cur_row = 0;
                //1. move to start pos
                Manager.IMotorCtrl.Move(1, posx);
                Thread.Sleep(100);
                while (true)
                {
                    if (!Manager.IMotorCtrl.IsAxisRun(1))
                        break;
                    Thread.Sleep(100);
                }
                Manager.IMotorCtrl.Move(2, posy);
                Thread.Sleep(100);
                while (true)
                {
                    if (!Manager.IMotorCtrl.IsAxisRun(2))
                        break;
                    Thread.Sleep(100);
                }
                Thread.Sleep(1000);
                //isNext = true;
                //2. loop scan
                for (int i = 0; i < column; i++)
                {
                    if (i % 2 == 0)
                    {
                        for (int j = 0; j < row; j++)
                        {
                            Manager.IMotorCtrl.Move(1, posx + (j * step + step));
                            Thread.Sleep(100);
                            while (true)
                            {
                                if (!Manager.IMotorCtrl.IsAxisRun(1))
                                    break;
                                Thread.Sleep(10);
                            }
                            Thread.Sleep(100);
                            Src.SocketManager.Instance.SendMessage("G");
                            //Thread.Sleep(500);
                            //isNext = true;
                            while (true)
                            {
                                if (isNext)
                                {
                                    isNext = false;
                                    break;
                                }
                                if (isStop)
                                    break;
                                Thread.Sleep(10);
                            }
                        }
                    }
                    else
                    {
                        for (int j = row; j > 0; j--)
                        {
                            Manager.IMotorCtrl.Move(1, posx + (j * step - step));
                            Thread.Sleep(100);
                            while (true)
                            {
                                if (!Manager.IMotorCtrl.IsAxisRun(1))
                                    break;
                                Thread.Sleep(10);
                            }
                            Thread.Sleep(100);
                            Src.SocketManager.Instance.SendMessage("G");
                            //Thread.Sleep(500);
                            //isNext = true;
                            while (true)
                            {
                                if (isNext)
                                {
                                    isNext = false;
                                    break;
                                }
                                if (isStop)
                                    break;
                                Thread.Sleep(10);
                            }
                        }
                    }
                    //2. move y axis
                    Thread.Sleep(500);
                    Manager.IMotorCtrl.Move(2, posy + (i * yst + yst));
                    Thread.Sleep(100);
                    while (true)
                    {
                        if (!Manager.IMotorCtrl.IsAxisRun(2))
                            break;
                        Thread.Sleep(10);
                    }

                    Src.SocketManager.Instance.SendMessage("G");
                    while (true)
                    {
                        if (isNext)
                        {
                            isNext = false;
                            break;
                        }
                        if (isStop)
                            break;
                        Thread.Sleep(10);
                    }
                }

                //3. move zero
                Manager.IMotorCtrl.XYToZero();
            }
            catch (Exception ex)
            {
                Dialogs.Show(ex);
            }
            finally
            {
                timer_scan.Stop();
                isRun = false;
            }
        }
        #endregion

        #region loop click evet
        private void btnSockectConn_Click(object sender, EventArgs e)
        {
            //builder socket
            Src.SocketManager.Instance.Start();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            //start scan
            timer_scan.Enabled = true;
            Thread.Sleep(2000);
            isStop = false;
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            //stop scan
            Manager.IMotorCtrl.StopAxis();
            timer_scan.Stop();
            isStop = true;
            isDryRun = false;
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            //move zero
            Manager.IMotorCtrl.XYToZero();
        }
        private void timer_scan_Tick(object sender, EventArgs e)
        {
            LoopRun();
        }

        #endregion

        private void btnDryRun_Click(object sender, EventArgs e)
        {
            if (thdDryrun == null)
            {
                thdDryrun = new Thread(new ThreadStart(DryThread));
                thdDryrun.Name = "Dry run thread";
                thdDryrun.IsBackground = true;
                thdDryrun.Start();
            }
            isDryRun = true;
        }

        #region dry run
        private Thread thdDryrun;
        private AutoResetEvent evDryRun = new AutoResetEvent(false);
        private bool isDryRun = false;
        private DateTime strTime;
        private int dryRunCount = 0;
        private void DryThread()
        {
            while (true)
            {
                if (isDryRun)
                {
                    DoDryRun();
                    evDryRun.WaitOne();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        private void DoDryRun()
        {
            try
            {

                int row = int.Parse(txtLoopXCnt.Text);
                int column = int.Parse(txtLoopYCnt.Text);
                float posx = float.Parse(txtStartPosx.Text);
                float posy = float.Parse(txtStartPosy.Text);
                float step = float.Parse(txtLoopxStep.Text);
                float yst = float.Parse(txtLoopyStep.Text);

                //int cur_row = 0;
                //1. move to start pos
                Manager.IMotorCtrl.Move(1, posx);
                Manager.IMotorCtrl.Move(2, posy);

                strTime = DateTime.Now;
                dryRunCount++;
                //isNext = true;
                //2. loop scan
                for (int i = 0; i < column; i++)
                {
                    if (i % 2 == 0)
                    {
                        for (int j = 0; j < row; j++)
                        {
                            Manager.IMotorCtrl.Move(1, posx + (j * step + step));
                            Thread.Sleep(10);
                            while (true)
                            {
                                if (!Manager.IMotorCtrl.IsAxisRun(1))
                                    break;
                                Thread.Sleep(3);
                            }
                        }
                        if (!isDryRun)
                            break;

                    }
                    else
                    {
                        for (int j = row; j > 0; j--)
                        {
                            Manager.IMotorCtrl.Move(1, posx + (j * step - step));
                            Thread.Sleep(10);
                            while (true)
                            {
                                if (!Manager.IMotorCtrl.IsAxisRun(1))
                                    break;
                                Thread.Sleep(3);
                            }
                            if (!isDryRun)
                                break;
                        }
                    }
                    //2. move y axis
                    Thread.Sleep(10);
                    Manager.IMotorCtrl.Move(2, posy + (i * yst + yst));
                    Thread.Sleep(10);
                    while (true)
                    {
                        if (!Manager.IMotorCtrl.IsAxisRun(2))
                            break;
                        Thread.Sleep(3);
                    }
                }
                if (!chkNoStop.Checked)
                    isDryRun = false;
                //3. move zero
                Manager.IMotorCtrl.XYToZero();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                LogHelper.AppLoger.InfoFormat("dry run count-start-end.{0}:{1}:{2}", dryRunCount, strTime, DateTime.Now);
                if (!chkNoStop.Checked)
                    isDryRun = false;
                evDryRun.Set();
            }
        }
        private void UpTime()
        {
            try
            {
                if (isDryRun)
                {
                    if (strTime != null)
                    {
                        TimeSpan usT = DateTime.Now - strTime;
                        txtUseTime.Text = usT.TotalSeconds.ToString("f1");
                        txtRunCount.Text = dryRunCount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion

        #region for Z focus        
        private bool m_grabImages;

        string path = @"d:\images\af\";
        private void GrabImageFun()
        {
            try
            {
                //1. set camera roi                
                int roiX = 512;
                int roiY = 256;
                int width = 960;
                int height = 960;

                bool ret = Manager.ICamCtrl.SetRoi(roiX, roiY, width, height);
                if (!ret)
                {
                    LogHelper.AppLoger.Error("set camera roi failed. abort move");
                    //msgBody.msg = "设置相机ROI区域失败,Abort";                    
                }
                float exposueT = 10f;
                ret = Manager.ICamCtrl.SetExposureTime(exposueT);
                if (!ret)
                {
                    LogHelper.AppLoger.Error("set camera exposure time failed. abort move");
                    //msgBody.msg = "设置相机曝光时间失败,Abort";
                }
                ret = Manager.ICamCtrl.SetTriggerMode(true, HWCtrl.CameraCtrl.TriggerType.Software);
                if (!ret)
                {
                    LogHelper.AppLoger.Error("set camera trigger mode failed. abort move");
                    //msgBody.msg = "设置相机触发机制失败,Abort";
                }
                //2. z move to start pos
                float z_start_pos = float.Parse(txtStartZPos.Text);
                //Manager.IMotorCtrl..Move(3, z_start_pos);                
                int max_cnt = int.Parse(txtZCnt.Text);
                float gap = float.Parse(txtZgap.Text);
                float z_pos = 0;
                m_grabImages = true;
                double rect = 0f;
                for (int i = 0; i < max_cnt; i++)
                {
                    z_pos = z_start_pos + i * gap;
                    SetPosOnForm(z_pos);
                    float z_pos_m = (float)z_pos / 1000;

                    Manager.IMotorCtrl.Move(3, z_pos_m);
                    Thread.Sleep(1000);
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    Bitmap img = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);

                    //send to matlab                    
                    rect = Matlab.FocusCtrl.Instance.SetImage(z_pos_m, img);

                    //output message
                    SetMsgOnForm(this.txtOutMsg, string.Format("Z位置:{0},清晰度值:{1}\n", z_pos_m, rect));

                    //display
                    SetImageOnForm(img);
                }

                //get peak
                //rect = Matlab.FocusCtrl.Instance.GetPeak2(img);
                //set data
                //SetResultOnForm(rect);

                //output message
                //Manager.IMotorCtrl.Move(3, (float)rect);
                SetMsgOnForm(this.txtOutMsg, string.Format("最清晰位置:{0}\n", rect));
                Thread.Sleep(200);

                //m_camera.RetrieveBuffer(m_rawImage);
                //m_rawImage.Convert(PixelFormat.PixelFormatRaw8, m_processedImage);
                //SetImageOnForm(m_processedImage.bitmap.Clone() as Bitmap);

                //3.close camera
                //m_camera.Disconnect();
                //m_camera.Disconnect();
                //4.show message
                //m_grabImages = false;
                //Dialogs.Show("采集图像完成!");
                //Thread.Sleep(1000);
                //SetMsgOnForm("图像采集完成！");

            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStatGrab_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartZPos.Text))
            {
                Dialogs.Show("请输入起始位置!");
                txtStartZPos.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtZgap.Text))
            {
                Dialogs.Show("请输入间隔!");
                txtZgap.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtZCnt.Text))
            {
                Dialogs.Show("请输入次数!");
                txtZCnt.Focus();
                return;
            }
            Thread grabThread = new Thread(autoFocus.DoFocus);
            grabThread.Name = "Z grabe image";
            grabThread.Start();
            LogHelper.AppLoger.Debug("af start grab image .");
        }


        private void btnCalcFocus_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region Light Controller parameter set
        private void btnSetMode_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetTriggerModel())
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnSetCycle_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetCycle())
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnSetSource_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetSource())
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnSetDirc_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetDirect())
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnSetEnabled_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetEnable())
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnCh1Delay_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetDelayTime((int)numCh1Delay.Value, 1)
                && Manager.Ilight.SetDelayTime38((int)numCh1Delay.Value))
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnCh1Plus_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetPlusTime((int)numCh1Plus.Value, 1)
                && Manager.Ilight.SetPlusTime38((int)numCh1Plus.Value))
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnCh2Delay_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetDelayTime((int)numCh2Delay.Value, 2))
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnCh2Plus_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetPlusTime((int)numCh2Plus.Value, 2))
            {
                Dialogs.Show("写入成功!");
            }
        }

        private void btnSetFilter_Click(object sender, EventArgs e)
        {
            if (Manager.Ilight.SetFilter((int)numFilter.Value))
            {
                Dialogs.Show("写入成功!");
            }
        }
        #endregion
        #region 自动聚焦
        private void txtStartZPos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStartZPos.Text))
                    return;
                if (!DataUtility.IsValidDecimal(txtStartZPos.Text))
                    return;
                decimal stValue = DataUtility.ParseDecimal(txtStartZPos.Text);
                tbZValue.Minimum = (int)stValue;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void txtStopZpos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStopZpos.Text))
                    return;
                if (!DataUtility.IsValidDecimal(txtStopZpos.Text))
                    return;
                decimal stValue = DataUtility.ParseDecimal(txtStopZpos.Text);
                tbZValue.Maximum = (int)stValue;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void txtZCnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStopZpos.Text))
                    return;
                if (!DataUtility.IsValidDecimal(txtStopZpos.Text))
                    return;
                decimal minValue = DataUtility.ParseDecimal(txtStartZPos.Text);
                decimal maxValue = DataUtility.ParseDecimal(txtStopZpos.Text);
                decimal cnt = DataUtility.ParseDecimal(txtZCnt.Text);

                txtZgap.Text = (Math.Abs(maxValue - minValue) / cnt).ToString();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void txtCurZPos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCurZPos.Text))
                    return;
                if (!DataUtility.IsValidDecimal(txtCurZPos.Text))
                    return;
                decimal stValue = DataUtility.ParseDecimal(txtCurZPos.Text);
                tbZValue.Value = (int)stValue;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public void SetMsgOnForm(TextBox txtbox, string msg)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    txtbox.Text = msg;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public void SetResultOnForm(double msg)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    txtZresult.Text = msg.ToString();
                }));
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public void SetPosOnForm(float pos)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    txtCurZPos.Text = pos.ToString();
                }));
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public void SetImageOnForm(Bitmap img)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    if (picView.Image != null)
                    {
                        picView.Image.Dispose();
                    }
                    picView.Image = img.Clone() as Bitmap;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public void SetFocusImageOnForm(Bitmap img)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    if (picAView.Image != null)
                    {
                        picAView.Image.Dispose();
                    }
                    picAView.Image = img.Clone() as Bitmap;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
        private void MaintainForm_Leave(object sender, EventArgs e)
        {
            //
            if (timer1.Enabled)
                timer1.Stop();
            if (timer2.Enabled)
                timer2.Stop();
            if (io_timer.Enabled)
                io_timer.Stop();
        }
        #region Z map 
        private Thread thrAutoScan;
        private bool IsScan = false;
        private const long TIMEOUT = 10 * 1000;     //10S
        private AutoResetEvent evt_Auto = new AutoResetEvent(false);
        private void Init()
        {
            if (thrAutoScan == null)
            {
                thrAutoScan = new Thread(new ThreadStart(AutoProc));
                thrAutoScan.Name = "Thread Z map Scan";
                thrAutoScan.IsBackground = true;
                thrAutoScan.Start();
            }
        }
        private void AutoProc()
        {
            while (true)
            {
                if (IsScan)
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
        private void AutoProcFun()
        {
            DateTime startTmr = DateTime.Now;
            try
            {
                Manager.ICamCtrl.BeginAcquisition();
                bool isSetRect = false;
                //1. run start pos.
                float x = float.Parse(txtStartX.Text);
                float y = float.Parse(txtStartY.Text);
                isSetRect = MoveAxisPos(x, y);
                if (!isSetRect)
                {
                    LogHelper.AppLoger.Error("move start pos failed. abort move");
                }
                dicMap.Clear();
                //1.2 
                float tmz = float.Parse(txtStartZ.Text);
                Manager.IMotorCtrl.Move(3, tmz);
                if (!IsOnPosZ(tmz))
                {
                    LogHelper.AppLoger.ErrorFormat("failed to move z pos.{0}", tmz);
                }
                //1.3 open camear
                //OpenCamera();
                //SetRoi(896,536, 128, 128);
                //2. auto focus
                float MAX_X_LEN = 16;
                float MAX_Y_LEN = 16;
                float x_gap = float.Parse(txtXGap.Text);
                float y_gap = float.Parse(txtYGap.Text);
                int max_x_cnt = (int)(MAX_X_LEN / x_gap);
                int max_y_cnt = (int)(MAX_Y_LEN / y_gap);
                float cur_x = 0;
                float cur_y = 0;
                //set exposure time
                float exposreT = float.Parse(txtAExposure.Text);
                //Manager.ICamCtrl.SetExposureTime(exposreT);
                //set sofeweare trigger
                //Manager.ICamCtrl.SetTriggerMode(true, TriggerType.Software);
                //set ror
                //Manager.ICamCtrl.SetRoi(896, 640, 256, 256);
                Manager.IFocus.Init();

                for (int i = 0; i < max_y_cnt; i++)
                {
                    for (int j = 0; j < max_x_cnt; j++)
                    {
                        Grab(j, i);
                        cur_x = x - (j + 1) * x_gap;
                        Manager.IMotorCtrl.Move(1, cur_x);
                        if (!IsOnPosX(cur_x))
                        {
                            LogHelper.AppLoger.ErrorFormat("failed move to x pos.{0}", x);
                        }

                    }
                    cur_y = y + (i + 1) * y_gap;
                    isSetRect = MoveAxisPos(x, cur_y);

                }
                //fitting                           
                //MatlabFitting();
                Manager.IFocus.MatlabFitting(dicMap);

                //3.close camera
                //m_camera.Disconnect();
                //m_camera.Disconnect();
                //4.show message
                //m_grabImages = false;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                TimeSpan ustT = DateTime.Now - startTmr;
                if (ustT.TotalMilliseconds > 10)
                    LogHelper.AppLoger.DebugFormat("Auto proc fun use time {0} mil sec", ustT.TotalMilliseconds);
                IsScan = false;
            }
        }

        private Dictionary<Point, float> dicMap = new Dictionary<Point, float>();
        private void Grab(int x, int y)
        {
            try
            {
                List<float> zposList = new List<float>();
                List<byte[,]> bytelist = new List<byte[,]>();
                //z stack                
                int z_cnt = int.Parse(txtZGrabCnt.Text);
                float z = float.Parse(txtStartZ.Text);
                MAX_Z_LEN = float.Parse(txtEndZ.Text);
                float z_gap = (float)(MAX_Z_LEN - z) / z_cnt;
                float cur_z = 0;
                
                for (int i = 0; i < z_cnt; i++)
                {
                    cur_z = z + i * z_gap;
                    //move z 
                    Manager.IMotorCtrl.Move(3, cur_z);
                    if (!IsOnPosZ(cur_z))
                    {
                        LogHelper.AppLoger.ErrorFormat("failed to move z pos.{0}", cur_z);
                    }
                    Thread.Sleep(10);
                    //GrabSavePic(x, y, cur_z);
                    //get all data to list                                   
                    //trigger camera                    
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    //recevi image
                    Bitmap img = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);
                    //save img 
                    string path = string.Format("c:\\map\\{0}_{1}\\", x, y);
                    string file = string.Format("{0}.{1}", cur_z, "bmp");
                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);
                    img.Save(path + file);

                    //output message
                    SetMsgOnForm(this.txtAMsg, string.Format("Z位置:{0}\n", z));
                    //display
                    SetFocusImageOnForm(img);
                    //savt to list
                    Thread.Sleep(10);
                    byte[,] grayData = Manager.IFocus.GetImageArray2D(img);
                    zposList.Add(cur_z);
                    bytelist.Add(grayData);
                }
                //calc focus                
                float new_z_pos_m = Manager.IFocus.MatlabPeek(zposList, bytelist, z_cnt);
                Point p = new Point(x, y);
                if (dicMap.ContainsKey(p))
                {
                    dicMap[p] = new_z_pos_m;
                }
                else
                {
                    dicMap.Add(p, new_z_pos_m);
                }
                //move to start z pos
                float tmz = float.Parse(txtStartZ.Text);
                Manager.IMotorCtrl.Move(3, tmz);
                if (!IsOnPosZ(tmz))
                {
                    LogHelper.AppLoger.ErrorFormat("failed to move z pos.{0}", tmz);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        //private void MatlabFitting()
        //{
        //    int max = dicMap.Count;
        //    double[,] dbx = new double[max,3];
        //    MWCellArray mwary = new MWCellArray(max,3);            
        //    List<Point> lst = new List<Point>(dicMap.Keys);
        //    StringBuilder strDat = new StringBuilder();
        //    for(int i=0;i<lst.Count;i++)
        //    {
        //        Point p = lst[i];
        //        float v = dicMap[p];
        //        //float[] fv = new float[3] { (float)p.X, (float)p.Y, v };
        //        dbx[i,0] = (double)p.X;
        //        dbx[i,1] = (double)p.Y;
        //        dbx[i,2] = (double)v;
        //        strDat.AppendFormat("{0},{1},{2}",p.X,p.Y,v);
        //        strDat.AppendFormat(System.Environment.NewLine);
        //        //MWNumericArray mwarr = new float[3] {(float)p.X, (float)p.Y,v };
        //        //mwary[i + 1, 1] = (float)p.X;
        //        //mwary[i + 1, 2] = (float)p.Y;
        //        //mwary[i + 1, 3] = v;
        //        //mwary[i + 1, 1] = mwarr;
        //        //mwary[i + 1, 2] = mwarr;
        //        //mwary[i + 1, 3] = mwarr;
        //    }

        //    //save result to file
        //    string path = @"c:\map\";
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path);
        //    }
        //    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
        //    FileStream fs = new FileStream(path + "final_data.csv", FileMode.Create);
        //    StreamWriter sw = new StreamWriter(fs, utf8WithoutBom);
        //    sw.Write(strDat.ToString());
        //    //清空缓冲区
        //    sw.Flush();
        //    //关闭流
        //    sw.Close();
        //    fs.Close();

        //    MWNumericArray x = new MWNumericArray(dbx);            
        //    MWArray mwret = this.myDll2.PreMapFunction(x);


        //    //float[,] ret = (float[,])((MWNumericArray)mwret).ToArray(MWArrayComponent.Real);

        //}
        //private GlobalFocusOfSinglePointROIForDll.Class1 myDll1;
        //private GfPremap.GfPremap myDll2;
        //public float MatlabPeek(List<float> zposList, List<byte[,]> bytelist, int max_cnt)
        //{
        //    float rect = 0;
        //    try
        //    {
        //        MWCellArray mwcell = new MWCellArray(max_cnt, 1);
        //        for (int i = 0; i < max_cnt; i++)
        //        {
        //            MWNumericArray mwarr = bytelist[i];
        //            mwcell[i + 1, 1] = mwarr;
        //        }
        //        MWNumericArray mwz = zposList.ToArray();
        //        MWArray mwret = this.myDll2.GlobalFocusOfSinglePointROIForDll(mwz, mwcell);
        //        float[,] ret = (float[,])((MWNumericArray)mwret).ToArray(MWArrayComponent.Real);
        //        float new_z_pos_m = ret[0, 0];
        //        rect = new_z_pos_m;
        //    }catch(Exception ex)
        //    { LogHelper.AppLoger.Error(ex); }
        //    return rect;
        //}
        //private byte[,] GetImageArray2D(Bitmap img)
        //{
        //    byte[,] grayData = new byte[960, 960];
        //    try
        //    {
        //        for (int i = 0; i < 960; i++)
        //        {
        //            for (int j = 0; j < 960; j++)
        //            {
        //                grayData[i, j] = img.GetPixel(j, i).B;
        //            }
        //        }
        //    }catch(Exception ex)
        //    {
        //        LogHelper.AppLoger.Error(ex);
        //    }
        //    return grayData;
        //}
        private void GrabSavePic(int x, int y, float z)
        {
            Manager.ICamCtrl.SetOneSoftwareTrigger();  //trigger camera

            //recevi image
            Bitmap img = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);

            //save img 
            string path = string.Format("c:\\map\\{0}_{1}\\", x, y);
            string file = string.Format("{0}.{1}", z, "bmp");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            img.Save(path + file);
            //output message
            SetMsgOnForm(this.txtOutMsg, string.Format("Z位置:{0}\n", z));

            //display
            SetImageOnForm(img);
        }
        private bool MoveAxisPos(float x, float y)
        {
            bool rect = false;
            try
            {
                float curPos = 0;
                //move to start pos                
                //x axis
                Manager.IMotorCtrl.Move(1, x);
                if (!IsOnPosX(x))
                {
                    LogHelper.AppLoger.ErrorFormat("failed move to x pos.{0}", x);
                }
                //y axis
                Manager.IMotorCtrl.Move(2, y);
                if (!IsOnPosY(y))
                {
                    LogHelper.AppLoger.ErrorFormat("failed move to y pos.{0}", y);
                }
                rect = true;
            }
            catch (Exception ex)
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
                //float curPos = Manager.IMotorCtrl.GetAxisXPosUm() / 1000;20181213,顾叶俊
                float curPos = Manager.IMotorCtrl.GetAxisXPosUmEx() / 1000;
                while (true)
                {
                    if (Manager.IMotorCtrl.IsAxisRun(1))
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    //curPos = Manager.IMotorCtrl.GetAxisXPosUm() / 1000;20181213,顾叶俊
                    curPos = Manager.IMotorCtrl.GetAxisXPosUmEx() / 1000;

                    if (Math.Abs(trigpos - curPos) < 1)
                        break;
                    if (watch.ElapsedMilliseconds > TIMEOUT)
                    {
                        watch.Stop();
                        LogHelper.AppLoger.ErrorFormat("on x pos check time out.pls check.");
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
        private bool IsOnPosY(float trigpos)
        {
            bool rect = false;
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                //float curPos = Manager.IMotorCtrl.GetAxisYPosUm() / 1000; 20181213,顾叶俊
                float curPos = Manager.IMotorCtrl.GetAxisYPosUmEx() / 1000; 
                while (true)
                {
                    if (Manager.IMotorCtrl.IsAxisRun(2))
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    //curPos = Manager.IMotorCtrl.GetAxisYPosUm() / 1000; 20181213,顾叶俊
                    curPos = Manager.IMotorCtrl.GetAxisYPosUmEx() / 1000; 
                    if (Math.Abs(trigpos - curPos) < 1)
                        break;
                    if (watch.ElapsedMilliseconds > TIMEOUT)
                    {
                        watch.Stop();
                        LogHelper.AppLoger.ErrorFormat("on y pos check time out.pls check.");
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
                //float curPos = Manager.IMotorCtrl.GetAxisZPosUm() / 1000; 20181213，顾叶俊
                float curPos = Manager.IMotorCtrl.GetAxisZPosUmEx() / 1000; 
                while (true)
                {
                    if (Manager.IMotorCtrl.IsAxisRun(3))
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    //curPos = Manager.IMotorCtrl.GetAxisZPosUm() / 1000;20181213，顾叶俊
                    curPos = Manager.IMotorCtrl.GetAxisZPosUmEx() / 1000;

                    if (Math.Abs(trigpos - curPos) < 1)
                        break;
                    if (watch.ElapsedMilliseconds > TIMEOUT)
                    {
                        watch.Stop();
                        LogHelper.AppLoger.ErrorFormat("on z pos check time out.pls check.");
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
        private void btnRun_Click(object sender, EventArgs e)
        {
            Init();
            IsScan = true;
            evt_Auto.Set();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            IsScan = false;
        }
        #endregion
        private void txtZgap2_TextChanged(object sender, EventArgs e)
        {
            float startZ = float.Parse(txtStartZ.Text);
            float zgap = float.Parse(txtZgap2.Text);
            MAX_Z_LEN = float.Parse(txtEndZ.Text);            
            float tot = MAX_Z_LEN - startZ;
            int cnt = (int)(tot / zgap);
            txtZGrabCnt.Text = cnt.ToString();
        }
        private void ICamCtrl_UpdateImage(object img)
        {
            try
            {

                byte[] t = (byte[])img;
                Bitmap bit = (Bitmap)Utility.GlobalFunction.BytesToImage(t);
                //int width = int.Parse("744");
                //int heith = int.Parse("510");
                //Bitmap newBit = CropImage(bit, width, heith);
                //bit.Dispose();
                //DisplayImage(newBit);
                DisplayImage(bit);

            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #region evt display
        public delegate void DisplayHandler(Bitmap obj);
        public DisplayHandler updateImg;
        public void UpdateImgMethod(Bitmap obj)
        {
            this.picView.Image = obj;
        }
        public void DisplayImage(Bitmap obj)
        {
            try
            {
                ThreadMethodImg(obj);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        //private void Zoom()
        //{
        //    float s1 = (float)(control.Width) / (float)(control.Height);
        //    float s2 = (float)(image.Width) / (float)(image.Height);
        //    if (s1 > s2)
        //    {
        //        widthShow = (int)((float)(control.Height) / (float)(image.Height) * image.Width);
        //        heightShow = control.Height;
        //        xBegin = (control.Width - widthShow) >> 1;
        //        yBegin = 0;
        //        scale = s1;
        //    }
        //    else
        //    {
        //        widthShow = control.Width;
        //        heightShow = (int)((float)(control.Width) / (float)(image.Width) * image.Height);
        //        xBegin = 0;
        //        yBegin = (control.Height - heightShow) >> 1;
        //        scale = s2;
        //    }
        //}
        //private void GraphicalImage()
        //{
        //    Graphics g = Graphics.FromImage(bmp);
        //    g.Clear(Color.LightGray);
        //    g.InterpolationMode = InterpolationMode.NearestNeighbor;
        //    g.DrawImage(image, xBegin, yBegin, widthShow, heightShow);
        //    g.Dispose();
        //    Graphics gg = control.CreateGraphics();
        //    gg.DrawImage(bmp, 0, 0);
        //    gg.Dispose();
        //}
        public void ThreadMethodImg(Bitmap obj)
        {
            //bmp = new Bitmap(control.Width, control.Height, PixelFormat.Format24bppRgb);
            //Zoom();
            //GraphicalImage();
            picView.Invoke(updateImg, obj);
        }
        #endregion
        private Bitmap CropImage(Bitmap orgImg, int width, int height)
        {
            Bitmap rect = new Bitmap(width, height);
            try
            {
                if (orgImg == null)
                    return null;
                Rectangle cropArea = new Rectangle();

                int startx = (picView.Width - width) / 2;
                int starty = (picView.Height - height) / 2;

                cropArea.X = startx < 0 ? 0 : startx;
                cropArea.Y = starty < 0 ? 0 : starty;
                cropArea.Width = width > orgImg.Width ? orgImg.Width : width;
                cropArea.Height = height > orgImg.Height ? orgImg.Height : height;
                rect = new Bitmap(cropArea.Width, cropArea.Height);
                rect = orgImg.Clone(cropArea, orgImg.PixelFormat);
            }
            catch (Exception ex)
            { LogHelper.AppLoger.Error(ex); }
            return rect;
        }
        //private Bitmap CropImage(Bitmap orgImg, int width, int height)
        //{
        //    Bitmap rect = new Bitmap(width, height);
        //    try
        //    {
        //        if (orgImg == null)
        //            return null;
        //        Rectangle cropArea = new Rectangle();
        //        int startx = (orgImg.Width - width) / 2;
        //        int starty = (orgImg.Height - height) / 2;
        //        cropArea.X = startx < 0 ? 0 : startx;
        //        cropArea.Y = starty < 0 ? 0 : starty;
        //        cropArea.Width = width > orgImg.Width ? orgImg.Width : width;
        //        cropArea.Height = height > orgImg.Height ? orgImg.Height : height;
        //        rect = new Bitmap(cropArea.Width, cropArea.Height);
        //        rect = orgImg.Clone(cropArea, orgImg.PixelFormat);
        //    }
        //    catch (Exception ex)
        //    { LogHelper.AppLoger.Error(ex); }
        //    return rect;
        //}
        private void btn_FreeRun_Click(object sender, EventArgs e)
        {
            Manager.ICamCtrl.SetTriggerMode(false, HWCtrl.CameraCtrl.TriggerType.Software);
            Manager.ICamCtrl.IsLiveMode = true;
            Manager.ICamCtrl.Start();
            timer2.Enabled = true;
            timer2.Start();
        }
        private void Initbmp()
        {
            try
            {
                Manager.ICamCtrl.UpdateImage += ICamCtrl_UpdateImage;
                updateImg = this.UpdateImgMethod;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void MaintainForm_Load(object sender, EventArgs e)
        {
            Initbmp();
            InitMotion();
            LoadPizeType();
            io_timer.Start();

            FakeCells();
            updateFocusImg = this.UpdateFocusImgProc;
            Manager.reviewCtrl.UpdateImage += UpdateFocusImage;
            //Manager.reviewCtrl.ReviewComplete += StartReviewAgain;
        }
        private void InitMotion()
        {
            if (Manager.IMotorCtrl.Connect(sysCfg.Control_IP))
            {
                //Dialogs.Show("连接成功!!");
                timer1.Start();
            }
            else
            {
                //Dialogs.Show("连接失败!!");
                timer1.Stop();
            }
        }
        #region 自动快速三点聚焦

        private float fZCurCenPos = 0f;
        private float fZStepGap = 0f;//const variable twice of dof
        private float fZCurGap = 0f;
        private float fRetZCenPos = 0f;
        private int iDir = 0;
        private readonly float f20x_COF = 0.00086f;//20倍物镜的景深0.00086mm
        private bool isQStart = false;
        private Thread thrQAF;
        private bool isQAF = false;
        private bool isQAFExcp = false;
        private double fCalcTime = 0f;
        private double fOtherTime = 0f;
        private double fMotionTime = 0f;
        private double fCaptureTime = 0f;
        private double fHalconTime = 0f;
        //Temp focus variable
        float fTakePicZPos = 0f;
        int iRoiWidth = 128;
        int iRoiHeight = 128;
        Bitmap imgMono8;
        Bitmap imgRoi;
        Rectangle rect;
        IntPtr pBit0Address;
        BitmapData srcBmpData;
        private List<IntPtr> ImgdataBitList = new List<IntPtr>();
        public void Grab3Image()
        {
            Manager.ICamCtrl.BeginAcquisition();
            DateTime startT = DateTime.Now;
            try
            {
                ImgdataBitList.Clear();
                if (!isQStart)
                {
                    isQStart = true;

                    fCalcTime = 0f;
                    fOtherTime = 0f;
                    fMotionTime = 0f;
                    fCaptureTime = 0f;
                    fHalconTime = 0f;
                    for (int i = 0; i < 3; i++)
                    {
                        fTakePicZPos = fZCurCenPos + (i - 1) * fZStepGap;
                        Manager.IMotorCtrl.Move(3, fTakePicZPos);
                        Thread.Sleep(200);
                        if (!IsOnPosZ(fTakePicZPos))
                        {
                            isQAFExcp = true;
                            //Dialogs.Show("failed to move z pos:" + fTakePicZPos.ToString(), false);
                            SetMsgOnForm(this.txt_3PointsFocusMsg, "failed to move z pos:" + fTakePicZPos.ToString());
                            break;
                        }
                        Manager.ICamCtrl.SetOneSoftwareTrigger();
                        imgMono8 = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);
                        if (imgMono8 == null)
                        {
                            isQAFExcp = true;
                            SetMsgOnForm(this.txt_3PointsFocusMsg, "Grab Image Failed by softTrigger\n");
                            break;
                        }
                        SetImageOnForm(imgMono8);
                        imgRoi = CropImage(imgMono8, iRoiWidth, iRoiHeight);
                        //imgRoi.Save(string.Format("{0}.bmp",i),ImageFormat.Bmp);
                        rect = new Rectangle(0, 0, iRoiWidth, iRoiHeight);
                        srcBmpData = imgRoi.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                        pBit0Address = srcBmpData.Scan0;
                        ImgdataBitList.Add(pBit0Address);
                        imgRoi.UnlockBits(srcBmpData);
                    }
                }
                else
                {
                    //first pos
                    DateTime startMotion = DateTime.Now;
                    fTakePicZPos = fZCurCenPos - fZStepGap;
                    Manager.IMotorCtrl.Move(3, fTakePicZPos);
                    if (!IsOnPosZ(fTakePicZPos))
                    {
                        isQAFExcp = true;
                        //Dialogs.Show("failed to move z pos:" + fTakePicZPos, false);
                        SetMsgOnForm(this.txt_3PointsFocusMsg, "failed to move z pos:" + fTakePicZPos.ToString());
                        return;
                    }
                    Thread.Sleep(200);
                    TimeSpan MotionSpan = DateTime.Now - startMotion;
                    fMotionTime += MotionSpan.TotalSeconds;
                    DateTime startCapture = DateTime.Now;
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    imgMono8 = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);
                    if (imgMono8 == null)
                    {
                        isQAFExcp = true;
                        //Dialogs.Show("Grab Image failed!");
                        SetMsgOnForm(this.txt_3PointsFocusMsg, "First Point Grab Image Failed by softTrigger\n");
                        return;
                    }
                    TimeSpan CaptureSpan = DateTime.Now - startCapture;
                    fCaptureTime += CaptureSpan.TotalSeconds;
                    SetImageOnForm(imgMono8);
                    imgRoi = CropImage(imgMono8, iRoiWidth, iRoiHeight);
                    rect = new Rectangle(0, 0, iRoiWidth, iRoiHeight);
                    srcBmpData = imgRoi.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    pBit0Address = srcBmpData.Scan0;
                    ImgdataBitList.Add(pBit0Address);
                    imgRoi.UnlockBits(srcBmpData);
                    SetMsgOnForm(this.txtOutMsg, string.Format("Z位置:{0}", fTakePicZPos));
                    Thread.Sleep(10);
                    //two pos
                    fTakePicZPos = fZCurCenPos;
                    Manager.IMotorCtrl.Move(3, fTakePicZPos);
                    if (!IsOnPosZ(fTakePicZPos))
                    {
                        isQAFExcp = true;
                        //Dialogs.Show("failed to move z pos:" + fTakePicZPos.ToString(), false);
                        SetMsgOnForm(this.txt_3PointsFocusMsg, "failed to move z pos:" + fTakePicZPos.ToString());
                        return;
                    }
                    Thread.Sleep(200);

                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    imgMono8 = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);
                    SetImageOnForm(imgMono8);
                    if (imgMono8 == null)
                    {
                        isQAFExcp = true;
                        //Dialogs.Show("Grab Image failed!");
                        SetMsgOnForm(this.txt_3PointsFocusMsg, "Second Point Grab Image Failed by softTrigger\n");
                        return;
                    }
                    imgRoi = CropImage(imgMono8, iRoiWidth, iRoiHeight);
                    rect = new Rectangle(0, 0, iRoiWidth, iRoiHeight);
                    srcBmpData = imgRoi.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    pBit0Address = srcBmpData.Scan0;
                    ImgdataBitList.Add(pBit0Address);
                    imgRoi.UnlockBits(srcBmpData);
                    //output message
                    SetMsgOnForm(this.txtOutMsg, string.Format("Z位置:{0}", fTakePicZPos));
                    //savt to list
                    Thread.Sleep(10);
                    //three pos
                    fTakePicZPos = fZCurCenPos + fZStepGap;
                    Manager.IMotorCtrl.Move(3, fTakePicZPos);
                    if (!IsOnPosZ(fTakePicZPos))
                    {
                        isQAFExcp = true;
                        //Dialogs.Show("failed to move z pos:" + fTakePicZPos.ToString(), false);\
                        SetMsgOnForm(this.txt_3PointsFocusMsg, "failed to move z pos:" + fTakePicZPos.ToString());
                        return;
                    }
                    Thread.Sleep(200);
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    imgMono8 = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);
                    SetImageOnForm(imgMono8);
                    if (imgMono8 == null)
                    {
                        isQAFExcp = true;
                        //Dialogs.Show("Grab Image failed!");
                        SetMsgOnForm(this.txt_3PointsFocusMsg, "Third Point Grab Image Failed by softTrigger\n");
                        return;
                    }
                    imgRoi = CropImage(imgMono8, iRoiWidth, iRoiHeight);
                    rect = new Rectangle(0, 0, iRoiWidth, iRoiHeight);
                    srcBmpData = imgRoi.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    pBit0Address = srcBmpData.Scan0;
                    ImgdataBitList.Add(pBit0Address);
                    imgRoi.UnlockBits(srcBmpData);
                    //output message
                    SetMsgOnForm(this.txtOutMsg, string.Format("Z位置:{0}", fTakePicZPos));
                    //savt to list
                    Thread.Sleep(10);
                }
                if (ImgdataBitList.Count == 3)
                {
                    DateTime startHalcon = DateTime.Now;
                    FocusHelper.QFocusInit(128, 128, 20.0, 1.0);
                    FocusHelper.Direction = iDir;
                    FocusHelper.CurZPos = fZCurCenPos;
                    FocusHelper.CurGap = fZCurGap;
                    bool ret = FocusHelper.QFocus(ImgdataBitList[0], ImgdataBitList[1], ImgdataBitList[2]);
                    TimeSpan HalconSpan = DateTime.Now - startHalcon;
                    fHalconTime += HalconSpan.TotalSeconds;
                    if (ret == true)//Convergency
                    {
                        isQStart = false;
                        //move to new pos                     
                        Manager.IMotorCtrl.Move(3, fRetZCenPos);
                        if (!IsOnPosZ(fRetZCenPos))
                        {
                            //Dialogs.Show("failed to move z pos:" + fRetZCenPos.ToString(), false);
                            SetMsgOnForm(this.txt_3PointsFocusMsg, "failed to move z pos:" + fRetZCenPos.ToString());
                            return;
                        }
                        Thread.Sleep(200);
                        Manager.ICamCtrl.SetOneSoftwareTrigger();
                        Bitmap imgFocus = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);
                        SetImageOnForm(imgFocus);
                    }             
                }
                else
                {
                    isQAFExcp = true;
                    SetMsgOnForm(this.txt_3PointsFocusMsg, "取图过程发生错误!\n");
                    return;
                }

            }
            catch (Exception ex)
            {
                isQAFExcp = true;
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                TimeSpan ustT = DateTime.Now - startT;
                fOtherTime += ustT.TotalSeconds;
                Manager.ICamCtrl.EndAcquisition();
            }
        }
        private void Inital3PointsFocus()
        {
            fZCurCenPos = 0f;
            fRetZCenPos = 0f;
            fTakePicZPos = 0f;
            iDir = 0;
            iRoiWidth = int.Parse(textBox_ROIWidth.Text);
            iRoiHeight = int.Parse(textBox_ROIHeight.Text);
            iRoiWidth = iRoiWidth % 4 != 0 ? (iRoiWidth / 4 + 1) * 4 : iRoiWidth;
            iRoiHeight = iRoiHeight % 4 != 0 ? (iRoiHeight / 4 + 1) * 4 : iRoiHeight;
            fZCurCenPos = float.Parse(txtbox_ZPos.Text);
            fZCurCenPos = fZCurCenPos / 1000;//um->mm
            fZCurCenPos = fZCurCenPos > 0.4f ? 0.4f : fZCurCenPos;//??need read config file to determine the Z limit value
            fZStepGap = 2 * f20x_COF;
            fZCurGap = 3 * f20x_COF;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SetMsgOnForm(txt_3PointsFocusMsg, "");
            Manager.ICamCtrl.SetTriggerMode(true, HWCtrl.CameraCtrl.TriggerType.Software);
            Manager.ICamCtrl.Stop();
            Inital3PointsFocus();
            StartQAF();
            isQStart = false;
            isQAF = true;
            isQAFExcp = false;
        }
        private void StartQAF()
        {
            if (thrQAF == null)
            {
                thrQAF = new Thread(new ThreadStart(AutoQAF));
                thrQAF.Name = "Auto Q AF";
                thrQAF.IsBackground = true;
                thrQAF.Start();
            }
        }
        private void AutoQAF()
        {
            while (!isQAFExcp)
            {
                if (isQAF)
                {
                    AutoQAFFun();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        private void AutoQAFFun()
        {
            DateTime startT = DateTime.Now;
            try
            {

                for (;;)
                {
                    Grab3Image();
                    Thread.Sleep(100);
                    if (!isQStart)
                    {
                        LogHelper.AppLoger.InfoFormat("Motion time:{0},Capture time:{1},matlab calc time:{2} second,total time:{3}", fMotionTime*3, fCaptureTime*3, fHalconTime, fOtherTime - fCalcTime);
                        SetMsgOnForm(txt_3PointsFocusMsg, "三点聚焦完成!\n");
                        SetMsgOnForm(txt_3PointsFocusMsg, "聚集次数为:" + FocusHelper.LoopCount.ToString());
                        Manager.ICamCtrl.SetTriggerMode(false, HWCtrl.CameraCtrl.TriggerType.Software);
                        Manager.ICamCtrl.Start();
                        isQAF = false;
                        break;
                    }
                    if (isQAFExcp)
                    {
                        SetMsgOnForm(txt_3PointsFocusMsg, "三点聚焦未完成!\n");
                        isQAF = false;
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                TimeSpan ustT = DateTime.Now - startT;
                LogHelper.AppLoger.DebugFormat("AutoQAF user time:{0} second", ustT.TotalSeconds);
                //MessageBox.Show("快速聚焦使用时间:" + ustT.TotalSeconds + "秒" + "循环次数:" + lopCnt);              
            }
        }
        #endregion
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            Manager.ICamCtrl.SetTriggerMode(true, HWCtrl.CameraCtrl.TriggerType.Hardware);
            Manager.ICamCtrl.IsLiveMode = true;
            Manager.ICamCtrl.Stop();
            timer2.Enabled = false;
            timer2.Stop();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            Graphics g = picView.CreateGraphics();
            Pen p = new Pen(Brushes.Red);
            int iRoiWidth = int.Parse(textBox_ROIWidth.Text);
            int iRoiHeight = int.Parse(textBox_ROIHeight.Text);
            iRoiWidth = iRoiWidth > 600 ? 256 : iRoiWidth;
            iRoiHeight = iRoiHeight > 500 ? 256 : iRoiHeight;

            int startx = (picView.Width - iRoiWidth) / 2;
            int starty = (picView.Height - iRoiHeight) / 2;
            g.DrawRectangle(p, startx, starty, iRoiWidth, iRoiHeight);

        }
        private void picView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(e.X.ToString() + "  " + e.Y.ToString());
            int CenWndX = picView.Width / 2;
            int CenWndY = picView.Height / 2;
            int OffsetX = e.X - CenWndX;
            int OffsetY = e.Y - CenWndY;
            float pixelRatio = 0.1725f;
            float XRealOffset = (OffsetX * pixelRatio);
            float YRealOffset = (OffsetY * pixelRatio);
            var distX = float.Parse(txtbox_Xpos.Text) + XRealOffset;
            distX = distX / 1000;
            Manager.IMotorCtrl.Move(1, distX);
            var distY = float.Parse(txtbox_Ypos.Text) - YRealOffset;
            distY = distY / 1000;
            Manager.IMotorCtrl.Move(2, distY);
        }

        #region IO card manual 
        private void GetIoStat()
        {
            try
            {
                int iput = Manager.IioCard.ReadInAll();
                string strbin = Convert.ToString(iput, 2).PadLeft(8,'0');
                string v;
                for(int i=0;i<strbin.Length;i++)
                {
                    v = strbin.Substring(i, 1);
                    switch(strbin.Length -1 - i)
                    {
                        case 7:
                            chkI7.Checked = v == "1" ? true : false;
                            break;
                        case 6:
                            chkI6.Checked = v == "1" ? true : false;
                            break;
                        case 5:
                            chkI5.Checked = v == "1" ? true : false;
                            break;
                        case 4:
                            chkI4.Checked = v == "1" ? true : false;
                            break;
                        case 3:
                            chkI3.Checked = v == "1" ? true : false;
                            break;
                        case 2:
                            chkI2.Checked = v == "1" ? true : false;
                            break;
                        case 1:
                            chkI1.Checked = v == "1" ? true : false;
                            break;
                        case 0:
                            chkI0.Checked = v == "1" ? true : false;
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void io_timer_Tick(object sender, EventArgs e)
        {
            GetIoStat();
        }
        

        private void chkO0_CheckedChanged(object sender, EventArgs e)
        {
            if (chkO0.Checked)
                Manager.IioCard.SetOutOn(0);
            else
                Manager.IioCard.SetOutOff(0);
        }

        private void chkO1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkO1.Checked)
                Manager.IioCard.SetOutOn(1);
            else
                Manager.IioCard.SetOutOff(1);
        }

        private void chkO2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkO2.Checked)
                Manager.IioCard.SetOutOn(2);
            else
                Manager.IioCard.SetOutOff(2);
        }

        private void chkO3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkO3.Checked)
                Manager.IioCard.SetOutOn(3);
            else
                Manager.IioCard.SetOutOff(3);
        }

        private void chkO4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkO4.Checked)
                Manager.IioCard.SetOutOn(4);
            else
                Manager.IioCard.SetOutOff(4);
        }

        private void chkO5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkO5.Checked)
                Manager.IioCard.SetOutOn(5);
            else
                Manager.IioCard.SetOutOff(5);
        }

        private void chkO6_CheckedChanged(object sender, EventArgs e)
        {
            if (chkO6.Checked)
                Manager.IioCard.SetOutOn(6);
            else
                Manager.IioCard.SetOutOff(6);
        }

        private void chkO7_CheckedChanged(object sender, EventArgs e)
        {
            if (chkO7.Checked)
                Manager.IioCard.SetOutOn(7);
            else
                Manager.IioCard.SetOutOff(7);
        }
        #endregion

        #region 压电陶瓷控制

        #region method
        private void LoadPizeType()
        {
            lblPizType.Text = sysCfg.PizType.ToString();
        }

        #endregion

        #region click ~evt
        private void btnReadStand_Click(object sender, EventArgs e)
        {
            try
            {
                Manager.IPiz.Open();
                chkPizClose.Checked = Manager.IPiz.GetCloseMode();
                HWCtrl.PIZParaEntity par = Manager.IPiz.GetPara();
                txtPizMaxDist.Text = par.MaxShift.ToString();
                Dialogs.Show("读取成功！");             
            }
            catch(Exception ex)
            {
                Dialogs.Show(ex);
            }                       
        }

        private void btnPizZero_Click(object sender, EventArgs e)
        {
            Manager.IPiz.SetDistance(0f);
            txtPizValue.Text = "0";
            Dialogs.Show("清零成功!");
        }

        private void btnPizSend_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtPizValue.Text))
                {
                    Dialogs.Show("请输入位移");
                    txtPizValue.Focus();
                    return;
                }
                float vt = float.Parse(txtPizValue.Text);
                if(vt >float.Parse(txtPizMaxDist.Text))
                {
                    Dialogs.Show("超过最大位移！");
                    txtPizValue.Focus();
                    return;
                }
                Manager.IPiz.SetDistance(vt);
                Dialogs.Show("发送成功!");
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void chkPizClose_CheckedChanged(object sender, EventArgs e)
        {
            if(chkPizClose.Checked)
            {
                chkPizClose.Text = "闭环";
                lblPizUnit.Text = "um";
            }else
            {
                chkPizClose.Text = "开环";
                lblPizUnit.Text = "v";
            }
        }


        #endregion

        #endregion
    }
}
