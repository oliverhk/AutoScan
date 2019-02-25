using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using CommonLibrary;
using System.Reflection;
using Utility;
namespace MainApp.WinUI
{
    public partial class MainForm :MaterialForm
    {


        #region skinmgt
        private readonly MaterialSkinManager materialSkinManager= MaterialSkinManager.Instance;
        #endregion
        #region property
        private const int MENU_PNL_HEIGHT = 100;            //MENU PANEL HEITHGT 
        private const int MENU_PNL_TOP = 25;                //MENU PANEL LOCALTION                 
        private const int MIN_FORM_WIDTH = 1280;               //MIN MAIN FORM WIDTH
        private const int MIN_FORM_HEIGHT = 900;              //MIN MAIN FORM HEIGHT
        #endregion
        #region instance
        //Singleton instance
        private static MainForm _instance;
        public static MainForm Instance => _instance ?? (_instance = new MainForm());
        #endregion
        #region frm inst
        private ConfigForm frmConfig = ConfigForm.Instance;
        private QueryForm frmQuery = QueryForm.Instance;
        private OperatorForm frmOperator = OperatorForm.Instance;
        private MaintainForm frmMaintain = MaintainForm.Instance;
        private RecipeForm frmRecipe = RecipeForm.Instance;
        
        #endregion
        public MainForm()
        {
            InitializeComponent();            
        }
        #region method
        /// <summary>
        /// 装载窗体
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="isShow"></param>
        private void AddFormInPanel(Form frm, bool isShow)
        {
            try
            {
                if (frm == null) return;
                if (!pnlContent.Controls.Contains(frm))
                {
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    frm.Dock = DockStyle.Fill;
                    pnlContent.Controls.Add(frm);
                }

                if (isShow)
                {
                    frm.Refresh();
                    frm.Show();
                    frm.BringToFront();
                }

            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        /// <summary>
        /// 窗口初始化
        /// </summary>
        private void Init()
        {
            try
            {
                //time start 
                timer_sys.Start();
                //SOFT VERSION
                GetSysVersion();
                //add sub form in mainform
                AddAllForm();
                //GET DB STATU
                GetDbStatus();
                //get device no
                GetDeviceNo();
                //get user
                GetUserName();
                //system mgmt 
                var ins = Sysmgmt.Instance;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        /// <summary>
        /// 添加所有的子窗体
        /// </summary>
        private void AddAllForm()
        {
            AddFormInPanel(frmConfig, false);
            AddFormInPanel(frmRecipe, false);
            AddFormInPanel(frmMaintain, false);
            AddFormInPanel(frmQuery, false);
            AddFormInPanel(frmOperator, true);
        }
        private void InitSkin()
        {
            try
            {
                // Initialize MaterialSkinManager
                //materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                //materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        /// <summary>
        /// 获取数据库状态
        /// </summary>
        private void GetDbStatus()
        {
            //get db status 
            bool ret = true;
            if(ret)
            {
                // status ok
                bardbstatus.Text = "DATABSE CONNECTED";
                bardbstatus.BackColor = Color.Green;
            }
            else
            {
                bardbstatus.Text = "DATABSE DISCONNECTED";
                bardbstatus.BackColor = Color.Red;
            }
        }
        /// <summary>
        /// 获取设备编号
        /// </summary>
        private void GetDeviceNo()
        {
            bardeviceno.Text = ConstDef.DeviceNo.ToUpper();
        }
        /// <summary>
        /// 获取系统版本
        /// </summary>
        private void GetSysVersion()
        {
            barversion.Text = string.Format("VERSION:{0}",ConstDef.SoftVersion);
        }
        private void GetUserName()
        {
            barUser.Text = string.Format("[{0}] 点击切换用户",ConstDef.UserID);
        }
        private void CheckUserPerssion()
        {
            try
            {
                if(ConstDef.OperatorRole)
                {
                    //operator mode
                    btnMain.Enabled = true;
                    btnQuery.Enabled = true;
                    btnMaintain.Enabled = false;
                    btnRecipe.Enabled = false;
                    btnSet.Enabled = false;
                }
                else
                {
                    List<string> lstAcc = Usr.UserAccess.GetUserPermissions(ConstDef.UserID);
                    btnMain.Enabled = true;
                    btnQuery.Enabled = lstAcc.Contains("button_query") ? true : false; 
                    btnMaintain.Enabled = lstAcc.Contains("button_maintain") ? true : false;
                    btnRecipe.Enabled = lstAcc.Contains("button_recipe") ? true : false; ;
                    btnSet.Enabled = lstAcc.Contains("button_set") ? true : false; ;
                }                
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void GetRecipe()
        {
            try
            {
                barRecipe.Text = string.Format("配置参数:[{0}] 名称:[{1}]", Manager.IRcp.RecipeId,Manager.IRcp.RecipeName);
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void GetCamTempture()
        {
            try
            {
                barCamTemp.Text = string.Format("相机温度:{0} ℃", Manager.ICamCtrl.GetCameraTemperature().ToString("f2"));
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();
            InitSkin();
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                this.Width = this.Width < MIN_FORM_WIDTH ? MIN_FORM_WIDTH : this.Width;
                this.Height = this.Height < MIN_FORM_HEIGHT ? MIN_FORM_HEIGHT : this.Height;

                //resize menu size
                this.pnlMenu.Width = this.Width;
                this.pnlMenu.Height = MENU_PNL_HEIGHT;
                this.pnlMenu.Location = new Point(0, MENU_PNL_TOP);

                //resize content pnl size
                this.pnlContent.Width = this.Width;
                this.pnlContent.Height = this.Height - MENU_PNL_HEIGHT - statusStrip1.Height - MENU_PNL_TOP + 1;
                this.pnlContent.Location = new Point(0, MENU_PNL_TOP + MENU_PNL_HEIGHT - 1);
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        #region menution buttion click
        private void btnMain_Click(object sender, EventArgs e)
        {
            if(!Usr.UserAccess.IsUserRight(ConstDef.UserID, "BUT_OPERATOR"))
            {
                Dialogs.Show("用户没有权限，请重新登录系统!");
                return;
            }
            if (ConstDef.IsScan)
                return;
            frmOperator.BringToFront();
            frmOperator.Show();
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            if (!Usr.UserAccess.IsUserRight(ConstDef.UserID, "BUT_RECIPE"))
            {
                Dialogs.Show("用户没有权限，请重新登录系统!");
                return;
            }
            if (ConstDef.IsScan)
                return;
            frmRecipe.BringToFront();
            frmRecipe.Show();
        }

        private void btnMaintain_Click(object sender, EventArgs e)
        {
            if (!Usr.UserAccess.IsUserRight(ConstDef.UserID, "BUT_MAINTAIN"))
            {
                Dialogs.Show("用户没有权限，请重新登录系统!");
                return;
            }
            if (ConstDef.IsScan)
                return;
            frmMaintain.BringToFront();
            frmMaintain.Show();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (!Usr.UserAccess.IsUserRight(ConstDef.UserID, "BUT_QUERY"))
            {
                Dialogs.Show("用户没有权限，请重新登录系统!");
                return;
            }
            if (ConstDef.IsScan)
                return;
            frmQuery.BringToFront();
            frmQuery.Show();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (!Usr.UserAccess.IsUserRight(ConstDef.UserID, "BUT_SETTING"))
            {
                Dialogs.Show("用户没有权限，请重新登录系统!");
                return;
            }
            if (ConstDef.IsScan)
                return;
            frmConfig.BringToFront();
            frmConfig.Show();
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Dialogs.Confirm("确认关闭系统?"))
            {
                e.Cancel = false;
            }else
            {
                e.Cancel = true;
            }
        }

        #region sub menu click
        private void menu_scheme_dark_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
        }

        private void menu_scheme_light_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void menu_color_blue_Click(object sender, EventArgs e)
        {
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);            
        }

        private void menu_color_indigo_Click(object sender, EventArgs e)
        {
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void menu_color_green_Click(object sender, EventArgs e)
        {
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
        }

        private void menu_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void timer_sys_Tick(object sender, EventArgs e)
        {
            bartime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            GetRecipe();
            GetCamTempture();
        }

        private void barUser_Click(object sender, EventArgs e)
        {
            if (ConstDef.IsScan)
                return;
            WinUI.UserLogIn frm = new UserLogIn();
            frm.ShowDialog();
            GetUserName();
            //show main form
            btnMain_Click(sender, e);
        }

        private void menu_user_login_Click(object sender, EventArgs e)
        {
            barUser_Click(sender, e);
        }

        private void timer_stat_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now.Millisecond % 2 == 0)
                {
                    if (SystemStatus.Instance.SysStat == SysStatus.Normal)
                    {                        
                        picNormalIco.Image = imgStat.Images[1];
                    }
                    else if (SystemStatus.Instance.SysStat == SysStatus.Alarm
                       || SystemStatus.Instance.SysStat == SysStatus.Error)
                    {                        
                        picErrIco.Image = imgStat.Images[0];
                    }
                }
                else
                {
                    picNormalIco.Image = imgStat.Images[2];
                    picErrIco.Image = imgStat.Images[2];
                }
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void menu_manual_motor_Click(object sender, EventArgs e)
        {
            var frm = new  MotorManualForm();
            frm.Show();
        }

        private void menu_manual_camera_Click(object sender, EventArgs e)
        {
            var frm = new FocusManualForm();
            frm.Show();
        }
    }
}
