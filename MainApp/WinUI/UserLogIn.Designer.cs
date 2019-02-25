namespace MainApp.WinUI
{
    partial class UserLogIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.txtUserid = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.chkOperLogin = new MaterialSkin.Controls.MaterialCheckBox();
            this.btnCancel = new MaterialSkin.Controls.MaterialFlatButton();
            this.btnOk = new MaterialSkin.Controls.MaterialFlatButton();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(44, 97);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(57, 19);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "用户名";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(44, 143);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(57, 19);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "密    码";
            // 
            // txtUserid
            // 
            this.txtUserid.Depth = 0;
            this.txtUserid.Hint = "请输入用户名";
            this.txtUserid.Location = new System.Drawing.Point(124, 95);
            this.txtUserid.MaxLength = 32767;
            this.txtUserid.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtUserid.Name = "txtUserid";
            this.txtUserid.PasswordChar = '\0';
            this.txtUserid.SelectedText = "";
            this.txtUserid.SelectionLength = 0;
            this.txtUserid.SelectionStart = 0;
            this.txtUserid.Size = new System.Drawing.Size(202, 23);
            this.txtUserid.TabIndex = 2;
            this.txtUserid.TabStop = false;
            this.txtUserid.UseSystemPasswordChar = false;
            this.txtUserid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserid_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Depth = 0;
            this.txtPassword.Hint = "请输入密码";
            this.txtPassword.Location = new System.Drawing.Point(124, 141);
            this.txtPassword.MaxLength = 32767;
            this.txtPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.Size = new System.Drawing.Size(202, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TabStop = false;
            this.txtPassword.UseSystemPasswordChar = false;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // chkOperLogin
            // 
            this.chkOperLogin.AutoSize = true;
            this.chkOperLogin.BackColor = System.Drawing.Color.Transparent;
            this.chkOperLogin.Depth = 0;
            this.chkOperLogin.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkOperLogin.Location = new System.Drawing.Point(124, 177);
            this.chkOperLogin.Margin = new System.Windows.Forms.Padding(0);
            this.chkOperLogin.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkOperLogin.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkOperLogin.Name = "chkOperLogin";
            this.chkOperLogin.Ripple = true;
            this.chkOperLogin.Size = new System.Drawing.Size(136, 30);
            this.chkOperLogin.TabIndex = 6;
            this.chkOperLogin.Text = "操作员快速登录";
            this.chkOperLogin.UseVisualStyleBackColor = false;
            this.chkOperLogin.CheckedChanged += new System.EventHandler(this.chkOperLogin_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Depth = 0;
            this.btnCancel.Icon = global::MainApp.Properties.Resources.Delete;
            this.btnCancel.Location = new System.Drawing.Point(218, 226);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primary = false;
            this.btnCancel.Size = new System.Drawing.Size(79, 36);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOk.Depth = 0;
            this.btnOk.Icon = global::MainApp.Properties.Resources.check;
            this.btnOk.Location = new System.Drawing.Point(89, 226);
            this.btnOk.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnOk.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnOk.Name = "btnOk";
            this.btnOk.Primary = false;
            this.btnOk.Size = new System.Drawing.Size(79, 36);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // UserLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 291);
            this.Controls.Add(this.chkOperLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserid);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Font = new System.Drawing.Font("SimSun", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.UserLogIn_Load);
            this.Resize += new System.EventHandler(this.UserLogIn_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtUserid;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPassword;
        private MaterialSkin.Controls.MaterialFlatButton btnOk;
        private MaterialSkin.Controls.MaterialFlatButton btnCancel;
        private MaterialSkin.Controls.MaterialCheckBox chkOperLogin;
    }
}