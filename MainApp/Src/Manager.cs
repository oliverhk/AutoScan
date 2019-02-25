using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using CommonLibrary;
using System.Windows.Forms;
using HWCtrl;
using HWCtrl.CameraCtrl;
using MatlabHelper;

namespace MainApp
{
    public static class Manager
    {
        #region proerty
        public delegate void UpdateMessageHander(MessageBody message);
        public static event UpdateMessageHander UpdateMessage;

        public static ILight Ilight;
        public static IAuto IAutoCtrl;
        public static IMotor IMotorCtrl;
        public static ICamera ICamCtrl;
        public static ICamera ICamCtrl_HaiKang;
        public static IRecipe IRcp;
        public static IMFocus IFocus;
        public static IPIZ IPiz;
        public static IIO IioCard;
        public static AutoReviewer reviewCtrl;
        public static Entity.FocusParaEntity FParaEntity { get; set; }

        //public static IInsp IInspCtrl;
        private static bool IsInInit = false;
        #endregion
        public static void BuilderSys()
        {
            try
            {                
                //1.set logs configure
                LogHelper.SetConfig(ConstDef.LogFile);
                //2.load configure
                string path = System.IO.Directory.GetParent(Application.StartupPath).ToString();
                SystemConfig.Instance.LoadConfig(path + @"\configure\SystemConfig.xml");
                //3. set db connect 
                //string dbconnstr = SystemConfig.Instance.GetConnString();
                //DbHelperMySQL.SetConnectString(dbconnstr);
                //4.motor control

                //light control
                Ilight = LightCtrl.Instance;

                //auto ctrl
                IAutoCtrl = AutoScan.Instance;

                //motor ctrl
                IMotorCtrl = HWCtrl.GalilTool.Instance;

                //camera ctrl
                ICamCtrl = CameraCtrlBFS.Instance;
                //ICamCtrl_HaiKang = CameraCtrlMv.Instance;

                //inspect manager
                //IInspCtrl = InspManager.Instance;

                //irecipe
                IRcp = RecipeManager.Instance;

                //io card
                IioCard = IOCtrl.Instance;
                IioCard.SetOutIoAllOff();   //set all off
                IioCard.SetLightGreenOn();  //set green on

                //matlab help
                IFocus = MatlabHelper.FocusHelper.Instance;
                //focus entity
                FParaEntity = Entity.FocusParaEntity.ReadFromFile();
                if (FParaEntity == null)
                    FParaEntity = new Entity.FocusParaEntity();

                // piez type
                if (SystemConfig.Instance.PizType == PiezType.XinMingTian)
                {
                    IPiz = XinMingTianCtrl.Instance;
                }
                else if (SystemConfig.Instance.PizType == PiezType.PI)
                {
                    //pi
                    IPiz = PIControl.Instance;
                }
                //IPiz.Open();        //open 20181213,顾叶俊，放到线程中开起，并判断是否故障

                reviewCtrl = AutoReviewer.Instance;

                //init sys
                System.Threading.Thread mythread = new System.Threading.Thread(InitThread);
                mythread.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show("配置系统参数异常.","睿仟医疗",MessageBoxButtons.OK,MessageBoxIcon.Error);
                LogHelper.AppLoger.Error(ex);
            }
        }
        public static void Init()
        {
            if (!IsInInit)
            {
                System.Threading.Thread mythread = new System.Threading.Thread(InitThread);
                System.Threading.Thread.Sleep(1000);
                mythread.Start();
            }
        }
        #region InitThread
        private static void InitThread()
        {
            try
            {
                using (new ProcessBarMgt("正在在初始化系统..."))
                {
                    IsInInit = true;
                    //connect to                     
                    MessageBody message = new MessageBody();
                    //1. light 
                    LogHelper.AppLoger.Debug("init light control.");
                    message.type = MsgType.Information;
                    message.msg = "初始化触发控制卡";
                    ThrowMessage(message);

                    IsInInit = Ilight.InitSys();
                    if (IsInInit)
                    {
                        LogHelper.AppLoger.Debug("init light control successed.");
                        message.type = MsgType.Information;
                        message.msg = "初始化触发控制卡成功";
                        ThrowMessage(message);
                    }
                    else
                    {
                        LogHelper.AppLoger.Debug("init light control failed.");
                        message.type = MsgType.Information;
                        message.msg = "初始化触发控制卡失败";
                        ThrowMessage(message);
                    }


                    //2.connnect motor
                    LogHelper.AppLoger.Debug("init plc control.");
                    message.type = MsgType.Information;
                    message.msg = "初始化运动控制卡";
                    ThrowMessage(message);

                    IsInInit = IMotorCtrl.Connect(SystemConfig.Instance.Control_IP);

                    if (IsInInit)
                    {
                        LogHelper.AppLoger.Debug("init plc control successed.");
                        message.type = MsgType.Information;
                        message.msg = "初始化运动控制卡成功";
                        ThrowMessage(message);
                    }
                    else
                    {
                        LogHelper.AppLoger.Debug("init plc control failed.");
                        message.type = MsgType.Information;
                        message.msg = "初始化运动控制卡失败";
                        ThrowMessage(message);
                    }

                    //3. home
                    //IMotorCtrl.ExecHomeALL();
                    message.msg = "运动平台开始回原点";
                    ThrowMessage(message);

                    //while (true)
                    //{
                    //    if (IMotorCtrl.IsHome())
                    //    {
                    //        message.msg = "运动平台回原点成功";
                    //        ThrowMessage(message);
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        System.Threading.Thread.Sleep(100);
                    //    }
                    //}
                    if (IsInInit)
                    {
                        //4.move z to start pos
                        //IMotorCtrl.Move(3, -4.45015f);
                        //message.msg = "Z轴运动到初始位置";
                        ThrowMessage(message);
                    }

                    //4.init camera
                    LogHelper.AppLoger.Debug("init camera control.");
                    message.type = MsgType.Information;
                    message.msg = "初始化相机";
                    ThrowMessage(message);

                    IsInInit = ICamCtrl.Init();
                    if (IsInInit)
                    {
                        LogHelper.AppLoger.Debug("init camera control successed.");
                        message.type = MsgType.Information;
                        message.msg = "初始化相机成功";
                        ThrowMessage(message);
                    }
                    else
                    {
                        LogHelper.AppLoger.Debug("init camera control failed.");
                        message.type = MsgType.Information;
                        message.msg = "初始化相机失败";
                        ThrowMessage(message);
                    }

                    //IsInInit = ICamCtrl_HaiKang.Init();
                    //if (IsInInit)
                    //{
                    //    LogHelper.AppLoger.Debug("Init HaiKang camera control successed.");
                    //    message.type = MsgType.Information;
                    //    message.msg = "初始化海康相机成功";
                    //    ThrowMessage(message);
                    //}
                    //else
                    //{
                    //    LogHelper.AppLoger.Debug("Init HaiKang camera control failed.");
                    //    message.type = MsgType.Information;
                    //    message.msg = "初始化海康相机失败";
                    //    ThrowMessage(message);
                    //}

                    //5. init matlab control
                    //IsInInit = IFocus.Init();

                    if (IsInInit)
                    {
                        LogHelper.AppLoger.Debug("init matlab control successed.");
                    }
                    else
                    {
                        LogHelper.AppLoger.Debug("init matlab control failed.");
                    }

                    //6. set green light on                                
                    ConstDef.IsSysReady = IsInInit;

                    //7.初始化压电陶瓷控制器，20181213，顾叶俊修改
                    IsInInit = IPiz.Open();
                    if (IsInInit)
                    {
                        LogHelper.AppLoger.Debug("init Pize control successed.");
                        message.type = MsgType.Information;
                        message.msg = "初始化压电陶瓷控制器成功";
                        ThrowMessage(message);
                    }
                    else
                    {
                        LogHelper.AppLoger.Debug("init Pize control failed.");
                        message.type = MsgType.Information;
                        message.msg = "初始化压电陶瓷控制器失败";
                        ThrowMessage(message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化系统异常!", "睿仟医疗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                IsInInit = false;
            }
        }
        #endregion        
        #region evt
        private static void ThrowMessage(MessageBody message)
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
    }
}
