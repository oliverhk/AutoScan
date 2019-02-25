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
using CommonLibrary;
using Utility;
using DBCtrl.Model;
using DBCtrl.DAO;

namespace MarkTool.WinUI
{
    public partial class UserAddForm : MaterialForm
    {
        #region property
        public bool IsAdd { get; set; }
        public string UpUserId { get; set; }
        #endregion
        public UserAddForm()
        {
            InitializeComponent();
        }

        private void UserAddForm_Load(object sender, EventArgs e)
        {
            LoadUserLevel();
            if (IsAdd)
            {
                txtUserId.Focus();
                this.Text = "新增用户";
            }
            else
            {
                UpUserInfo();
                this.Text = string.Format("修改用户:{0}", UpUserId);
            }
        }

        #region method
        private void UpUserInfo()
        {
            try
            {
                UserDao dao = new UserDao();
                User usr = dao.GetUser(UpUserId);
                txtUserId.Text = UpUserId;
                txtUserName.Text = usr.UserName;
                txtUserPwd.Text = usr.UserPwd;
                txtUserPwd2.Text = usr.UserPwd;
                cmbRoles.Text = string.Format("{0}_{1}", usr.RoleId, usr.RoleName);
                cmbRoleLevel.Text = string.Format("{0}_{1}", usr.LevelId, usr.LevelName);
                txtUserId.Enabled = false;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void LoadUserLevel()
        {
            try
            {
                UserLevelDao dao = new UserLevelDao();
                IList<UserLevel> lst = dao.GetList();
                foreach (var v in lst)
                {
                    cmbRoleLevel.Items.Add(string.Format("{0}_{1}", v.LevelId, v.LevelName));
                }
                cmbRoleLevel.SelectedIndex = 0;

                RoleDao roleDao = new RoleDao();
                IList<Role> roleLst = roleDao.GetList();
                foreach (var v in roleLst)
                {
                    cmbRoles.Items.Add(string.Format("{0}_{1}", v.RoleId, v.RoleName));
                }
                cmbRoles.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private bool IsValid()
        {
            bool rect = false;
            try
            {
                if (string.IsNullOrEmpty(txtUserId.Text))
                {
                    Dialogs.Show("请输入用户名");
                    txtUserId.Focus();
                    return rect;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    Dialogs.Show("请输入姓名");
                    txtUserName.Focus();
                    return rect;
                }
                if (string.IsNullOrEmpty(txtUserPwd.Text))
                {
                    Dialogs.Show("请输入密码");
                    txtUserPwd.Focus();
                    return rect;
                }
                if (string.IsNullOrEmpty(txtUserPwd2.Text))
                {
                    Dialogs.Show("请输入确认密码");
                    txtUserPwd2.Focus();
                    return rect;
                }
                if (txtUserPwd.Text != txtUserPwd2.Text)
                {
                    Dialogs.Show("二次输入的密码不一致，请重新输入");
                    txtUserPwd.Focus();
                    return rect;
                }
                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool Save()
        {
            bool rect = false;
            try
            {
                User usr;
                UserDao dao = new UserDao();
                if (IsAdd)
                {
                    usr = new User();
                    usr.UserId = txtUserId.Text.Trim();
                    usr.UserName = txtUserName.Text.Trim();
                    usr.UserPwd = txtUserPwd.Text;
                    usr.RoleId = DataUtility.ParseInt(cmbRoles.Text.Substring(0, cmbRoles.Text.IndexOf('_')));
                    usr.LevelId = DataUtility.ParseInt(cmbRoleLevel.Text.Substring(0, cmbRoleLevel.Text.IndexOf('_')));
                    usr.CreateTime = DateTime.Now;
                    rect = dao.Insert(usr);
                }
                else
                {
                    usr = dao.GetUser(UpUserId);
                    usr.UserName = txtUserName.Text.Trim();
                    usr.UserPwd = txtUserPwd.Text;
                    usr.RoleId = DataUtility.ParseInt(cmbRoles.Text.Substring(0, cmbRoles.Text.IndexOf('_')));
                    usr.LevelId = DataUtility.ParseInt(cmbRoleLevel.Text.Substring(0, cmbRoleLevel.Text.IndexOf('_')));
                    rect = dao.Update(usr);
                }

            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            if (Save())
            {
                Dialogs.Show("保存成功!");
                this.Close();
            }
            else
            {
                Dialogs.Show("保存失败!");
            }
            MainForm.UpdateUserDictionary();
        }
    }
}
