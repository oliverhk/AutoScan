using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using CommonLibrary;
using Utility;
using DBCtrl.DAO;
using DBCtrl.Model;

namespace MarkTool.WinUI
{
    public partial class MainForm : MaterialForm
    {
        //Singleton instance
        #region instance
        private static MainForm _instance;
        public static MainForm Instance => _instance ?? (_instance = new MainForm());
        #endregion

        private readonly MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;

        private const int MENU_PNL_HEIGHT = 100;            //MENU PANEL HEITHGT
        private const int MENU_PNL_TOP = 25;                //MENU PANEL LOCALTION
        private const int MIN_FORM_WIDTH = 1000;            //MIN MAIN FORM WIDTH
        private const int MIN_FORM_HEIGHT = 650;            //MIN MAIN FORM HEIGHT

        private OperatorForm frmOperator = OperatorForm.Instance;
        private ConfigForm frmConfig = ConfigForm.Instance;
        private ClassifyForm frmClassify = ClassifyForm.Instance;

        public delegate void updateCoordDelegate();
        public static updateCoordDelegate updateCoord = null; 

        public static Dictionary<string, User> dicUser = new Dictionary<string, User>();
        public static Dictionary<int, UserLevel> dicUserLevel = new Dictionary<int, UserLevel>();
        public static Dictionary<int, Category> dicCategory = new Dictionary<int, Category>();
        public static Dictionary<int, Types> dicCellType = new Dictionary<int, Types>();

        public MainForm()
        {
            InitializeComponent();

            updateCoord += new updateCoordDelegate(UpdateCoordInBar);
        }

        private void InitSkin()
        {
            try
            {
                //Initialize MaterialSkinManager
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;             
                materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.Red100, TextShade.WHITE);
                //materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitSkin();

            AddFormInPanel(frmConfig, false);
            AddFormInPanel(frmClassify, false);
            AddFormInPanel(frmOperator, false);
            frmOperator.updateRatio += UpdateBarRatio;

            barUser_Click(null, null);
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
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void GetUserName()
        {
            barUser.Text = string.Format("[{0}] 点击切换用户", ConstDef.UserID);
        }

        private void UpdateBarRatio(int ratio)
        {
            barRatio.Text = string.Format("缩放比例[{0}%]", ratio);
        }

        private void UpdateCoordInBar()
        {
            barXY.Text = string.Format("图像坐标[{0}，{1}]", ConstDef.ImageX, ConstDef.ImageY);
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (!Usr.UserAccess.IsUserRight(ConstDef.UserID, "BUT_SETTING"))
            {
                Dialogs.Show("用户没有权限，请重新登录系统!");
                return;
            }
            frmConfig.BringToFront();
            frmConfig.Show();
            btnMarkSave.Visible = false;
            btnMarkUndo.Visible = false;
        }

        private void btnClassify_Click(object sender, EventArgs e)
        {
            if (!Usr.UserAccess.IsUserRight(ConstDef.UserID, "BUT_SETTING"))
            {
                Dialogs.Show("用户没有权限，请重新登录系统!");
                return;
            }
            frmClassify.BringToFront();
            frmClassify.Show();
            btnMarkSave.Visible = false;
            btnMarkUndo.Visible = false;
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            if (!Usr.UserAccess.IsUserRight(ConstDef.UserID, "BUT_OPERATOR"))
            {
                Dialogs.Show("用户没有权限，请重新登录系统!");
                return;
            }
            frmOperator.BringToFront();
            frmOperator.Show();
            btnMarkSave.Visible = true;
            btnMarkUndo.Visible = true;
        }

        private void barUser_Click(object sender, EventArgs e)
        {
            UserLogIn frm = new UserLogIn();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            GetUserName();

            //show main form
            btnMark_Click(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Dialogs.Confirm("确认关闭系统?"))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnMarkSave_Click(object sender, EventArgs e)
        {
            if(frmOperator.Visible)
            {
                frmOperator.btnMarkSave_Click();
            }
        }

        private void btnMarkUndo_Click(object sender, EventArgs e)
        {
            if(frmOperator.Visible)
            {
                frmOperator.btnMarkUndo_Click();
            }
        }

        public static void UpdateUserDictionary()
        {
            UserDao dao = new UserDao();
            IList<User> list;
            list = dao.GetList();

            dicUser.Clear();
            foreach(var item in list)
            {
                dicUser.Add(item.UserId, item);
            }
        }

        public static void UpdateUserLevelDictionary()
        {
            UserLevelDao dao = new UserLevelDao();
            IList<UserLevel> list;
            list = dao.GetList();

            dicUserLevel.Clear();
            foreach (var item in list)
            {
                dicUserLevel.Add(item.LevelId, item);
            }
        }

        public static void UpdateCategoryDictionary()
        {
            CategoryDao dao = new CategoryDao();
            IList<Category> list;
            list = dao.GetList();

            dicCategory.Clear();
            foreach (var item in list)
            {
                dicCategory.Add(item.CateId, item);
            }
        }

        public static void UpdateCellTypeDictionary()
        {
            TypesDao dao = new TypesDao();
            IList<Types> list;
            list = dao.GetList();

            dicCellType.Clear();
            foreach (var item in list)
            {
                dicCellType.Add(item.TypeId, item);
            }
        }
    }
}
