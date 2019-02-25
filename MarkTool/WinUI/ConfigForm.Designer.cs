namespace MarkTool.WinUI
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.tcSet = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnImagePath = new System.Windows.Forms.Button();
            this.grpSubMenu = new System.Windows.Forms.GroupBox();
            this.btnCfgLoad = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnCfgSave = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtCameraPara = new System.Windows.Forms.TextBox();
            this.txtLensPara = new System.Windows.Forms.TextBox();
            this.txtMicroscope = new System.Windows.Forms.TextBox();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.txtHospital = new System.Windows.Forms.TextBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grpsubMenu2 = new System.Windows.Forms.GroupBox();
            this.btnDelUser = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnUpdateUser = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnNewUser = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnQueryUser = new MaterialSkin.Controls.MaterialRaisedButton();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.colid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colroleid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colrolename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcreatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcSet.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpSubMenu.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grpsubMenu2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // tcSet
            // 
            this.tcSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSet.Controls.Add(this.tabPage1);
            this.tcSet.Controls.Add(this.tabPage2);
            this.tcSet.Depth = 0;
            this.tcSet.Font = new System.Drawing.Font("SimSun", 10F);
            this.tcSet.Location = new System.Drawing.Point(0, 20);
            this.tcSet.MouseState = MaterialSkin.MouseState.HOVER;
            this.tcSet.Name = "tcSet";
            this.tcSet.SelectedIndex = 0;
            this.tcSet.Size = new System.Drawing.Size(893, 508);
            this.tcSet.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.btnImagePath);
            this.tabPage1.Controls.Add(this.grpSubMenu);
            this.tabPage1.Controls.Add(this.txtCameraPara);
            this.tabPage1.Controls.Add(this.txtLensPara);
            this.tabPage1.Controls.Add(this.txtMicroscope);
            this.tabPage1.Controls.Add(this.txtImagePath);
            this.tabPage1.Controls.Add(this.txtHospital);
            this.tabPage1.Controls.Add(this.materialLabel5);
            this.tabPage1.Controls.Add(this.materialLabel4);
            this.tabPage1.Controls.Add(this.materialLabel3);
            this.tabPage1.Controls.Add(this.materialLabel2);
            this.tabPage1.Controls.Add(this.materialLabel1);
            this.tabPage1.Font = new System.Drawing.Font("SimSun", 10F);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(885, 481);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "系统参数";
            // 
            // btnImagePath
            // 
            this.btnImagePath.Image = ((System.Drawing.Image)(resources.GetObject("btnImagePath.Image")));
            this.btnImagePath.Location = new System.Drawing.Point(555, 102);
            this.btnImagePath.Name = "btnImagePath";
            this.btnImagePath.Size = new System.Drawing.Size(26, 23);
            this.btnImagePath.TabIndex = 11;
            this.btnImagePath.UseVisualStyleBackColor = true;
            this.btnImagePath.Click += new System.EventHandler(this.btnImagePath_Click);
            // 
            // grpSubMenu
            // 
            this.grpSubMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSubMenu.Controls.Add(this.btnCfgLoad);
            this.grpSubMenu.Controls.Add(this.btnCfgSave);
            this.grpSubMenu.Location = new System.Drawing.Point(716, 6);
            this.grpSubMenu.Name = "grpSubMenu";
            this.grpSubMenu.Size = new System.Drawing.Size(152, 453);
            this.grpSubMenu.TabIndex = 10;
            this.grpSubMenu.TabStop = false;
            // 
            // btnCfgLoad
            // 
            this.btnCfgLoad.AutoSize = true;
            this.btnCfgLoad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCfgLoad.Depth = 0;
            this.btnCfgLoad.Icon = global::MarkTool.Properties.Resources.FileOpen;
            this.btnCfgLoad.Location = new System.Drawing.Point(24, 29);
            this.btnCfgLoad.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCfgLoad.Name = "btnCfgLoad";
            this.btnCfgLoad.Primary = true;
            this.btnCfgLoad.Size = new System.Drawing.Size(109, 36);
            this.btnCfgLoad.TabIndex = 1;
            this.btnCfgLoad.Text = "加载参数";
            this.btnCfgLoad.UseVisualStyleBackColor = true;
            this.btnCfgLoad.Click += new System.EventHandler(this.btnCfgLoad_Click);
            // 
            // btnCfgSave
            // 
            this.btnCfgSave.AutoSize = true;
            this.btnCfgSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCfgSave.Depth = 0;
            this.btnCfgSave.Icon = global::MarkTool.Properties.Resources.Save32;
            this.btnCfgSave.Location = new System.Drawing.Point(24, 100);
            this.btnCfgSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCfgSave.Name = "btnCfgSave";
            this.btnCfgSave.Primary = true;
            this.btnCfgSave.Size = new System.Drawing.Size(109, 36);
            this.btnCfgSave.TabIndex = 0;
            this.btnCfgSave.Text = "保存参数";
            this.btnCfgSave.UseVisualStyleBackColor = true;
            this.btnCfgSave.Click += new System.EventHandler(this.btnCfgSave_Click);
            // 
            // txtCameraPara
            // 
            this.txtCameraPara.Location = new System.Drawing.Point(181, 289);
            this.txtCameraPara.Name = "txtCameraPara";
            this.txtCameraPara.Size = new System.Drawing.Size(374, 23);
            this.txtCameraPara.TabIndex = 9;
            // 
            // txtLensPara
            // 
            this.txtLensPara.Location = new System.Drawing.Point(181, 228);
            this.txtLensPara.Name = "txtLensPara";
            this.txtLensPara.Size = new System.Drawing.Size(374, 23);
            this.txtLensPara.TabIndex = 8;
            // 
            // txtMicroscope
            // 
            this.txtMicroscope.Location = new System.Drawing.Point(181, 164);
            this.txtMicroscope.Name = "txtMicroscope";
            this.txtMicroscope.Size = new System.Drawing.Size(374, 23);
            this.txtMicroscope.TabIndex = 7;
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(181, 103);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.ReadOnly = true;
            this.txtImagePath.Size = new System.Drawing.Size(374, 23);
            this.txtImagePath.TabIndex = 6;
            // 
            // txtHospital
            // 
            this.txtHospital.Location = new System.Drawing.Point(181, 47);
            this.txtHospital.Name = "txtHospital";
            this.txtHospital.Size = new System.Drawing.Size(374, 23);
            this.txtHospital.TabIndex = 5;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(76, 292);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(73, 19);
            this.materialLabel5.TabIndex = 4;
            this.materialLabel5.Text = "相机参数";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(76, 231);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(73, 19);
            this.materialLabel4.TabIndex = 3;
            this.materialLabel4.Text = "镜头参数";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(60, 167);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(89, 19);
            this.materialLabel3.TabIndex = 2;
            this.materialLabel3.Text = "显微镜型号";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(76, 106);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(73, 19);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "图像路径";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(100, 50);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(49, 19);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "医  院";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grpsubMenu2);
            this.tabPage2.Controls.Add(this.dgvUser);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(885, 481);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "人员管理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grpsubMenu2
            // 
            this.grpsubMenu2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpsubMenu2.Controls.Add(this.btnDelUser);
            this.grpsubMenu2.Controls.Add(this.btnUpdateUser);
            this.grpsubMenu2.Controls.Add(this.btnNewUser);
            this.grpsubMenu2.Controls.Add(this.btnQueryUser);
            this.grpsubMenu2.Location = new System.Drawing.Point(716, 6);
            this.grpsubMenu2.Name = "grpsubMenu2";
            this.grpsubMenu2.Size = new System.Drawing.Size(152, 453);
            this.grpsubMenu2.TabIndex = 1;
            this.grpsubMenu2.TabStop = false;
            // 
            // btnDelUser
            // 
            this.btnDelUser.AutoSize = true;
            this.btnDelUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDelUser.Depth = 0;
            this.btnDelUser.Icon = global::MarkTool.Properties.Resources.Delete;
            this.btnDelUser.Location = new System.Drawing.Point(23, 199);
            this.btnDelUser.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDelUser.Name = "btnDelUser";
            this.btnDelUser.Primary = true;
            this.btnDelUser.Size = new System.Drawing.Size(109, 36);
            this.btnDelUser.TabIndex = 3;
            this.btnDelUser.Text = "删除用户";
            this.btnDelUser.UseVisualStyleBackColor = true;
            this.btnDelUser.Click += new System.EventHandler(this.btnDelUser_Click);
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.AutoSize = true;
            this.btnUpdateUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUpdateUser.Depth = 0;
            this.btnUpdateUser.Icon = global::MarkTool.Properties.Resources.edit;
            this.btnUpdateUser.Location = new System.Drawing.Point(23, 141);
            this.btnUpdateUser.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Primary = true;
            this.btnUpdateUser.Size = new System.Drawing.Size(109, 36);
            this.btnUpdateUser.TabIndex = 2;
            this.btnUpdateUser.Text = "修改用户";
            this.btnUpdateUser.UseVisualStyleBackColor = true;
            this.btnUpdateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.AutoSize = true;
            this.btnNewUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNewUser.Depth = 0;
            this.btnNewUser.Icon = global::MarkTool.Properties.Resources.add;
            this.btnNewUser.Location = new System.Drawing.Point(23, 83);
            this.btnNewUser.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Primary = true;
            this.btnNewUser.Size = new System.Drawing.Size(109, 36);
            this.btnNewUser.TabIndex = 1;
            this.btnNewUser.Text = "新增用户";
            this.btnNewUser.UseVisualStyleBackColor = true;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // btnQueryUser
            // 
            this.btnQueryUser.AutoSize = true;
            this.btnQueryUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQueryUser.Depth = 0;
            this.btnQueryUser.Icon = global::MarkTool.Properties.Resources.view;
            this.btnQueryUser.Location = new System.Drawing.Point(23, 22);
            this.btnQueryUser.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnQueryUser.Name = "btnQueryUser";
            this.btnQueryUser.Primary = true;
            this.btnQueryUser.Size = new System.Drawing.Size(109, 36);
            this.btnQueryUser.TabIndex = 0;
            this.btnQueryUser.Text = "加载用户";
            this.btnQueryUser.UseVisualStyleBackColor = true;
            this.btnQueryUser.Click += new System.EventHandler(this.btnQueryUser_Click);
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToOrderColumns = true;
            this.dgvUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUser.BackgroundColor = System.Drawing.Color.White;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUser.ColumnHeadersHeight = 28;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colid,
            this.colfullname,
            this.RoleId,
            this.RoleName,
            this.colroleid,
            this.colrolename,
            this.colcreatetime});
            this.dgvUser.Location = new System.Drawing.Point(0, 0);
            this.dgvUser.MultiSelect = false;
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersWidth = 10;
            this.dgvUser.RowTemplate.Height = 23;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(699, 475);
            this.dgvUser.TabIndex = 0;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.tcSet;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 0);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(884, 23);
            this.materialTabSelector1.TabIndex = 1;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // colid
            // 
            this.colid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colid.DataPropertyName = "UserId";
            this.colid.HeaderText = "用户名";
            this.colid.Name = "colid";
            this.colid.ReadOnly = true;
            this.colid.Width = 74;
            // 
            // colfullname
            // 
            this.colfullname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colfullname.DataPropertyName = "UserName";
            this.colfullname.HeaderText = "姓名";
            this.colfullname.Name = "colfullname";
            this.colfullname.ReadOnly = true;
            this.colfullname.Width = 60;
            // 
            // RoleId
            // 
            this.RoleId.DataPropertyName = "RoleId";
            this.RoleId.HeaderText = "角色编号";
            this.RoleId.Name = "RoleId";
            this.RoleId.ReadOnly = true;
            // 
            // RoleName
            // 
            this.RoleName.DataPropertyName = "RoleName";
            this.RoleName.HeaderText = "角色名称";
            this.RoleName.Name = "RoleName";
            this.RoleName.ReadOnly = true;
            // 
            // colroleid
            // 
            this.colroleid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colroleid.DataPropertyName = "LevelId";
            this.colroleid.HeaderText = "级别编号";
            this.colroleid.Name = "colroleid";
            this.colroleid.ReadOnly = true;
            this.colroleid.Width = 88;
            // 
            // colrolename
            // 
            this.colrolename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colrolename.DataPropertyName = "LevelName";
            this.colrolename.HeaderText = "级别名称";
            this.colrolename.Name = "colrolename";
            this.colrolename.ReadOnly = true;
            this.colrolename.Width = 88;
            // 
            // colcreatetime
            // 
            this.colcreatetime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colcreatetime.DataPropertyName = "CreateTime";
            this.colcreatetime.HeaderText = "创建时间";
            this.colcreatetime.Name = "colcreatetime";
            this.colcreatetime.ReadOnly = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 514);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.tcSet);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.tcSet.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.grpSubMenu.ResumeLayout(false);
            this.grpSubMenu.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.grpsubMenu2.ResumeLayout(false);
            this.grpsubMenu2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl tcSet;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private System.Windows.Forms.TextBox txtCameraPara;
        private System.Windows.Forms.TextBox txtLensPara;
        private System.Windows.Forms.TextBox txtMicroscope;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.TextBox txtHospital;
        private System.Windows.Forms.GroupBox grpSubMenu;
        private MaterialSkin.Controls.MaterialRaisedButton btnCfgSave;
        private MaterialSkin.Controls.MaterialRaisedButton btnCfgLoad;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.GroupBox grpsubMenu2;
        private MaterialSkin.Controls.MaterialRaisedButton btnQueryUser;
        private MaterialSkin.Controls.MaterialRaisedButton btnNewUser;
        private MaterialSkin.Controls.MaterialRaisedButton btnUpdateUser;
        private MaterialSkin.Controls.MaterialRaisedButton btnDelUser;
        private System.Windows.Forms.Button btnImagePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colroleid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colrolename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcreatetime;
    }
}