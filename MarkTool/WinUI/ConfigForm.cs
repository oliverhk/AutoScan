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
using Utility.DEncrypt;
using DBCtrl.Model;
using DBCtrl.DAO;

namespace MarkTool.WinUI
{
    public partial class ConfigForm : Form
    {
        private SystemConfig sysCfg = SystemConfig.Instance;
        private IList<User> lstUers;     //list user for
        private UserDao dao;
        #region instance
        //Singleton instance
        private static ConfigForm _instance;
        public static ConfigForm Instance => _instance ?? (_instance = new ConfigForm());
        #endregion
        public ConfigForm()
        {
            InitializeComponent();

            SysInfoDao SysInfodao = new SysInfoDao();
            SysInfo info = SysInfodao.GetSysInfo(ConstDef.SysInfoImagePath);
            ConstDef.ImagePath = info.FieldName;

            dgvUser.AutoGenerateColumns = false;
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            dao = new UserDao();

            btnQueryUser_Click(null, null);
            btnCfgLoad_Click(null, null);
        }

        private void btnQueryUser_Click(object sender, EventArgs e)
        {
            lstUers = dao.GetList();
            dgvUser.DataSource = lstUers;
            dgvUser.Refresh();
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dialogs.Confirm("确认删除用户?"))
                {
                    string userid = dgvUser.SelectedRows[0].Cells[0].Value.ToString();
                    dao.Delete(userid);
                    btnQueryUser_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            UserAddForm frm = new UserAddForm();
            frm.IsAdd = true;
            frm.ShowDialog();
            btnQueryUser_Click(sender, e);
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUers.Count < 0)
                    return;
                if (dgvUser.SelectedRows.Count <= 0)
                {
                    Dialogs.Show("请选择需要修改的人员!");
                    return;
                }
                string usrId = dgvUser.SelectedRows[0].Cells[0].Value.ToString();
                UserAddForm frm = new UserAddForm();
                frm.IsAdd = false;
                frm.UpUserId = usrId;
                frm.ShowDialog();
                btnQueryUser_Click(sender, e);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnCfgLoad_Click(object sender, EventArgs e)
        {
            try
            {
                SysInfoDao dao = new SysInfoDao();
                SysInfo info = new SysInfo();

                info = dao.GetSysInfo(ConstDef.SysInfoHospital);
                txtHospital.Text = info.FieldName;

                info = dao.GetSysInfo(ConstDef.SysInfoImagePath);
                txtImagePath.Text = info.FieldName;

                info = dao.GetSysInfo(ConstDef.SysInfoMicroType);
                txtMicroscope.Text = info.FieldName;

                info = dao.GetSysInfo(ConstDef.SysInfoLensPara);
                txtLensPara.Text = info.FieldName;

                info = dao.GetSysInfo(ConstDef.SysInfoCameraPara);
                txtCameraPara.Text = info.FieldName;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnCfgSave_Click(object sender, EventArgs e)
        {
            if (SaveConfig())
            {
                Dialogs.Show("保存配置成功.");
            }
            else
            {
                Dialogs.Show("保存配置失败.");
            }
            btnCfgLoad_Click(sender, e);
        }
        
        private bool SaveSysConfig(SysInfo info)
        {
            bool ret = false;
            SysInfoDao dao = new SysInfoDao();

            if (dao.GetSysInfo(info.FieldId) != null)
            {
                if (!dao.Update(info))
                {
                    return ret;
                }
            }
            else
            {
                if (!dao.Insert(info))
                {
                    return ret;
                }
            }

            return true;
        }

        private bool SaveConfig()
        {
            bool ret = false;
            try
            {
                SysInfo info = new SysInfo();

                info.FieldId = ConstDef.SysInfoHospital;
                info.FieldName = txtHospital.Text;
                if(SaveSysConfig(info) == false)
                {
                    return ret;
                }

                info.FieldId = ConstDef.SysInfoImagePath;
                info.FieldName = txtImagePath.Text;
                ConstDef.ImagePath = txtImagePath.Text;
                BeginInvoke(OperatorForm.updateImagePath, null, null);
                if (SaveSysConfig(info) == false)
                {
                    return ret;
                }

                info.FieldId = ConstDef.SysInfoMicroType;
                info.FieldName = txtMicroscope.Text;
                if (SaveSysConfig(info) == false)
                {
                    return ret;
                }

                info.FieldId = ConstDef.SysInfoLensPara;
                info.FieldName = txtLensPara.Text;
                if (SaveSysConfig(info) == false)
                {
                    return ret;
                }

                info.FieldId = ConstDef.SysInfoCameraPara;
                info.FieldName = txtCameraPara.Text;
                ret = SaveSysConfig(info);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }

            return ret;
        }

        private void btnImagePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择图像所在文件夹";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                txtImagePath.Text = dialog.SelectedPath;
            }
        }
    }
}
