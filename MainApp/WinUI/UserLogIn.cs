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

namespace MainApp.WinUI
{
    public partial class UserLogIn : MaterialForm
    {        
        public UserLogIn()
        {
            InitializeComponent();
        }

        private void UserLogIn_Load(object sender, EventArgs e)
        {
            txtUserid.Text = "";
            txtPassword.Text = "";
            //chkOperLogin.Checked = true;

        }

        private void UserLogIn_Resize(object sender, EventArgs e)
        {            
            this.Width = 403;
            this.Height = 287;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (chkOperLogin.Checked)
            {
                ConstDef.UserID = "Operator";                
                ConstDef.OperatorRole = true;
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                if(string.IsNullOrEmpty(txtUserid.Text))
                {
                    Dialogs.Show("请输入用户名!");
                    txtUserid.Focus();
                    return;
                }
                if(string.IsNullOrEmpty(txtPassword.Text))
                {
                    Dialogs.Show("请输入密码!");
                    txtPassword.Focus();
                    return;
                }                
                if(Usr.UserAccess.UserLogin(txtUserid.Text,txtPassword.Text))
                {
                    this.DialogResult = DialogResult.Yes;
                    ConstDef.UserID = txtUserid.Text.Trim();
                    this.Close();
                }
                else
                {
                    //error info
                    Dialogs.Show("用户名密码错误，请检查",false);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkOperLogin_CheckedChanged(object sender, EventArgs e)
        {
            if(chkOperLogin.Checked)
            {
                txtUserid.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUserid.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void txtUserid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                btnOk_Click(sender, e);
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(sender, e);
        }
    }
}
