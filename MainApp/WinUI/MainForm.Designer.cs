namespace MainApp.WinUI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.bardeviceno = new System.Windows.Forms.ToolStripStatusLabel();
            this.bardbstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.barversion = new System.Windows.Forms.ToolStripStatusLabel();
            this.barRecipe = new System.Windows.Forms.ToolStripStatusLabel();
            this.barblank = new System.Windows.Forms.ToolStripStatusLabel();
            this.barUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.bartime = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.menu_title = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.menu_scheme = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_scheme_dark = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_scheme_light = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_color = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_color_blue = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_color_indigo = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_color_green = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_user_login = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_manual_motor = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_manual_camera = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlStatus = new System.Windows.Forms.TableLayoutPanel();
            this.picNormalIco = new System.Windows.Forms.PictureBox();
            this.picErrIco = new System.Windows.Forms.PictureBox();
            this.btnQuery = new MaterialSkin.Controls.MaterialRaisedButton();
            this.menuimglist = new System.Windows.Forms.ImageList(this.components);
            this.btnMaintain = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnRecipe = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnSet = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnMain = new MaterialSkin.Controls.MaterialRaisedButton();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.timer_sys = new System.Windows.Forms.Timer(this.components);
            this.timer_stat = new System.Windows.Forms.Timer(this.components);
            this.imgStat = new System.Windows.Forms.ImageList(this.components);
            this.barCamTemp = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.menu_title.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNormalIco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picErrIco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bardeviceno,
            this.bardbstatus,
            this.barversion,
            this.barRecipe,
            this.barCamTemp,
            this.barblank,
            this.barUser,
            this.bartime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 870);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1280, 30);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // bardeviceno
            // 
            this.bardeviceno.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.bardeviceno.Name = "bardeviceno";
            this.bardeviceno.Size = new System.Drawing.Size(102, 25);
            this.bardeviceno.Text = "DEVICE NO";
            // 
            // bardbstatus
            // 
            this.bardbstatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.bardbstatus.Name = "bardbstatus";
            this.bardbstatus.Size = new System.Drawing.Size(160, 25);
            this.bardbstatus.Text = "DATABASE STATUS";
            // 
            // barversion
            // 
            this.barversion.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barversion.Name = "barversion";
            this.barversion.Size = new System.Drawing.Size(88, 25);
            this.barversion.Text = "VERSION:";
            // 
            // barRecipe
            // 
            this.barRecipe.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barRecipe.Name = "barRecipe";
            this.barRecipe.Size = new System.Drawing.Size(83, 25);
            this.barRecipe.Text = "Recipe{0}";
            // 
            // barblank
            // 
            this.barblank.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barblank.Name = "barblank";
            this.barblank.Size = new System.Drawing.Size(439, 25);
            this.barblank.Spring = true;
            // 
            // barUser
            // 
            this.barUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barUser.Name = "barUser";
            this.barUser.Size = new System.Drawing.Size(54, 25);
            this.barUser.Text = "USER";
            this.barUser.Click += new System.EventHandler(this.barUser_Click);
            // 
            // bartime
            // 
            this.bartime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.bartime.Name = "bartime";
            this.bartime.Size = new System.Drawing.Size(193, 25);
            this.bartime.Text = "yyyy-MM-dd HH:mm:ss";
            this.bartime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlMenu
            // 
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.ContextMenuStrip = this.menu_title;
            this.pnlMenu.Controls.Add(this.pnlStatus);
            this.pnlMenu.Controls.Add(this.btnQuery);
            this.pnlMenu.Controls.Add(this.btnMaintain);
            this.pnlMenu.Controls.Add(this.btnRecipe);
            this.pnlMenu.Controls.Add(this.btnSet);
            this.pnlMenu.Controls.Add(this.btnMain);
            this.pnlMenu.Controls.Add(this.picLogo);
            this.pnlMenu.Location = new System.Drawing.Point(0, 28);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(1280, 900);
            this.pnlMenu.TabIndex = 1;
            // 
            // menu_title
            // 
            this.menu_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.menu_title.Depth = 0;
            this.menu_title.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_scheme,
            this.menu_color,
            this.menu_user_login,
            this.toolStripMenuItem1,
            this.menu_manual_motor,
            this.menu_manual_camera,
            this.toolStripMenuItem3,
            this.menu_exit,
            this.toolStripMenuItem2});
            this.menu_title.MouseState = MaterialSkin.MouseState.HOVER;
            this.menu_title.Name = "menu_title";
            this.menu_title.Size = new System.Drawing.Size(157, 166);
            // 
            // menu_scheme
            // 
            this.menu_scheme.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_scheme_dark,
            this.menu_scheme_light});
            this.menu_scheme.Name = "menu_scheme";
            this.menu_scheme.Size = new System.Drawing.Size(156, 24);
            this.menu_scheme.Text = "系统主题";
            // 
            // menu_scheme_dark
            // 
            this.menu_scheme_dark.Name = "menu_scheme_dark";
            this.menu_scheme_dark.Size = new System.Drawing.Size(104, 24);
            this.menu_scheme_dark.Text = "黑色";
            this.menu_scheme_dark.Click += new System.EventHandler(this.menu_scheme_dark_Click);
            // 
            // menu_scheme_light
            // 
            this.menu_scheme_light.Name = "menu_scheme_light";
            this.menu_scheme_light.Size = new System.Drawing.Size(104, 24);
            this.menu_scheme_light.Text = "白色";
            this.menu_scheme_light.Click += new System.EventHandler(this.menu_scheme_light_Click);
            // 
            // menu_color
            // 
            this.menu_color.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_color_blue,
            this.menu_color_indigo,
            this.menu_color_green});
            this.menu_color.Name = "menu_color";
            this.menu_color.Size = new System.Drawing.Size(156, 24);
            this.menu_color.Text = "系统颜色";
            // 
            // menu_color_blue
            // 
            this.menu_color_blue.Name = "menu_color_blue";
            this.menu_color_blue.Size = new System.Drawing.Size(104, 24);
            this.menu_color_blue.Text = "蓝色";
            this.menu_color_blue.Click += new System.EventHandler(this.menu_color_blue_Click);
            // 
            // menu_color_indigo
            // 
            this.menu_color_indigo.Name = "menu_color_indigo";
            this.menu_color_indigo.Size = new System.Drawing.Size(104, 24);
            this.menu_color_indigo.Text = "靛蓝";
            this.menu_color_indigo.Click += new System.EventHandler(this.menu_color_indigo_Click);
            // 
            // menu_color_green
            // 
            this.menu_color_green.Name = "menu_color_green";
            this.menu_color_green.Size = new System.Drawing.Size(104, 24);
            this.menu_color_green.Text = "绿色";
            this.menu_color_green.Click += new System.EventHandler(this.menu_color_green_Click);
            // 
            // menu_user_login
            // 
            this.menu_user_login.Name = "menu_user_login";
            this.menu_user_login.Size = new System.Drawing.Size(156, 24);
            this.menu_user_login.Text = "切换用户";
            this.menu_user_login.Click += new System.EventHandler(this.menu_user_login_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(153, 6);
            // 
            // menu_manual_motor
            // 
            this.menu_manual_motor.Name = "menu_manual_motor";
            this.menu_manual_motor.Size = new System.Drawing.Size(156, 24);
            this.menu_manual_motor.Text = "控制手动调节";
            this.menu_manual_motor.Click += new System.EventHandler(this.menu_manual_motor_Click);
            // 
            // menu_manual_camera
            // 
            this.menu_manual_camera.Name = "menu_manual_camera";
            this.menu_manual_camera.Size = new System.Drawing.Size(156, 24);
            this.menu_manual_camera.Text = "控制相机调节";
            this.menu_manual_camera.Click += new System.EventHandler(this.menu_manual_camera_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(153, 6);
            // 
            // menu_exit
            // 
            this.menu_exit.Name = "menu_exit";
            this.menu_exit.Size = new System.Drawing.Size(156, 24);
            this.menu_exit.Text = "关闭系统";
            this.menu_exit.Click += new System.EventHandler(this.menu_exit_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(153, 6);
            // 
            // pnlStatus
            // 
            this.pnlStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatus.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.pnlStatus.ColumnCount = 2;
            this.pnlStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlStatus.Controls.Add(this.picNormalIco, 0, 0);
            this.pnlStatus.Controls.Add(this.picErrIco, 1, 0);
            this.pnlStatus.Location = new System.Drawing.Point(1173, 3);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.RowCount = 1;
            this.pnlStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.pnlStatus.Size = new System.Drawing.Size(76, 38);
            this.pnlStatus.TabIndex = 6;
            // 
            // picNormalIco
            // 
            this.picNormalIco.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picNormalIco.Location = new System.Drawing.Point(4, 4);
            this.picNormalIco.Name = "picNormalIco";
            this.picNormalIco.Size = new System.Drawing.Size(30, 30);
            this.picNormalIco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picNormalIco.TabIndex = 0;
            this.picNormalIco.TabStop = false;
            // 
            // picErrIco
            // 
            this.picErrIco.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picErrIco.Location = new System.Drawing.Point(41, 4);
            this.picErrIco.Name = "picErrIco";
            this.picErrIco.Size = new System.Drawing.Size(31, 30);
            this.picErrIco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picErrIco.TabIndex = 1;
            this.picErrIco.TabStop = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.AutoSize = true;
            this.btnQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuery.Depth = 0;
            this.btnQuery.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnQuery.Icon = global::MainApp.Properties.Resources.query;
            this.btnQuery.ImageList = this.menuimglist;
            this.btnQuery.Location = new System.Drawing.Point(1001, 52);
            this.btnQuery.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Primary = true;
            this.btnQuery.Size = new System.Drawing.Size(109, 36);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询分析";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // menuimglist
            // 
            this.menuimglist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("menuimglist.ImageStream")));
            this.menuimglist.TransparentColor = System.Drawing.Color.Transparent;
            this.menuimglist.Images.SetKeyName(0, "BlogInsertCategories.png");
            this.menuimglist.Images.SetKeyName(1, "CreateFormInDesignView.png");
            this.menuimglist.Images.SetKeyName(2, "ViewsModeMenu.png");
            this.menuimglist.Images.SetKeyName(3, "DatabaseQueryNew.png");
            this.menuimglist.Images.SetKeyName(4, "CreateReportFromWizard.png");
            this.menuimglist.Images.SetKeyName(5, "FindDialog.png");
            this.menuimglist.Images.SetKeyName(6, "PageMenu.png");
            // 
            // btnMaintain
            // 
            this.btnMaintain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaintain.AutoSize = true;
            this.btnMaintain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMaintain.Depth = 0;
            this.btnMaintain.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnMaintain.Icon = global::MainApp.Properties.Resources.maintain;
            this.btnMaintain.ImageList = this.menuimglist;
            this.btnMaintain.Location = new System.Drawing.Point(852, 52);
            this.btnMaintain.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMaintain.Name = "btnMaintain";
            this.btnMaintain.Primary = true;
            this.btnMaintain.Size = new System.Drawing.Size(109, 36);
            this.btnMaintain.TabIndex = 4;
            this.btnMaintain.Text = "系统维护";
            this.btnMaintain.UseVisualStyleBackColor = true;
            this.btnMaintain.Click += new System.EventHandler(this.btnMaintain_Click);
            // 
            // btnRecipe
            // 
            this.btnRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecipe.AutoSize = true;
            this.btnRecipe.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRecipe.Depth = 0;
            this.btnRecipe.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnRecipe.Icon = global::MainApp.Properties.Resources.recipe;
            this.btnRecipe.ImageList = this.menuimglist;
            this.btnRecipe.Location = new System.Drawing.Point(703, 52);
            this.btnRecipe.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRecipe.Name = "btnRecipe";
            this.btnRecipe.Primary = true;
            this.btnRecipe.Size = new System.Drawing.Size(109, 36);
            this.btnRecipe.TabIndex = 3;
            this.btnRecipe.Text = "配置参数";
            this.btnRecipe.UseVisualStyleBackColor = true;
            this.btnRecipe.Click += new System.EventHandler(this.btnRecipe_Click);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.AutoSize = true;
            this.btnSet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSet.Depth = 0;
            this.btnSet.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnSet.Icon = global::MainApp.Properties.Resources.set;
            this.btnSet.ImageList = this.menuimglist;
            this.btnSet.Location = new System.Drawing.Point(1150, 52);
            this.btnSet.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSet.Name = "btnSet";
            this.btnSet.Primary = true;
            this.btnSet.Size = new System.Drawing.Size(109, 36);
            this.btnSet.TabIndex = 2;
            this.btnSet.Text = "系统设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnMain
            // 
            this.btnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMain.AutoSize = true;
            this.btnMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMain.Depth = 0;
            this.btnMain.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnMain.Icon = global::MainApp.Properties.Resources.main;
            this.btnMain.Location = new System.Drawing.Point(554, 52);
            this.btnMain.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMain.Name = "btnMain";
            this.btnMain.Primary = true;
            this.btnMain.Size = new System.Drawing.Size(109, 36);
            this.btnMain.TabIndex = 1;
            this.btnMain.Text = "用户操作";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::MainApp.Properties.Resources.logo;
            this.picLogo.Location = new System.Drawing.Point(2, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(292, 95);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // pnlContent
            // 
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Location = new System.Drawing.Point(0, 135);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1280, 900);
            this.pnlContent.TabIndex = 2;
            // 
            // timer_sys
            // 
            this.timer_sys.Interval = 1000;
            this.timer_sys.Tick += new System.EventHandler(this.timer_sys_Tick);
            // 
            // timer_stat
            // 
            this.timer_stat.Enabled = true;
            this.timer_stat.Interval = 500;
            this.timer_stat.Tick += new System.EventHandler(this.timer_stat_Tick);
            // 
            // imgStat
            // 
            this.imgStat.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgStat.ImageStream")));
            this.imgStat.TransparentColor = System.Drawing.Color.Transparent;
            this.imgStat.Images.SetKeyName(0, "bullet_ball_red.png");
            this.imgStat.Images.SetKeyName(1, "bullet_ball_green.png");
            this.imgStat.Images.SetKeyName(2, "bullet_ball_glass_grey.png");
            // 
            // barCamTemp
            // 
            this.barCamTemp.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barCamTemp.Name = "barCamTemp";
            this.barCamTemp.Size = new System.Drawing.Size(113, 25);
            this.barCamTemp.Text = "相机温度{0}℃";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 900);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("SimSun", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.menu_title.ResumeLayout(false);
            this.pnlStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picNormalIco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picErrIco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.PictureBox picLogo;
        private MaterialSkin.Controls.MaterialRaisedButton btnMain;
        private MaterialSkin.Controls.MaterialRaisedButton btnQuery;
        private MaterialSkin.Controls.MaterialRaisedButton btnMaintain;
        private MaterialSkin.Controls.MaterialRaisedButton btnRecipe;
        private MaterialSkin.Controls.MaterialRaisedButton btnSet;
        private System.Windows.Forms.ImageList menuimglist;
        private MaterialSkin.Controls.MaterialContextMenuStrip menu_title;
        private System.Windows.Forms.ToolStripMenuItem menu_scheme;
        private System.Windows.Forms.ToolStripMenuItem menu_scheme_dark;
        private System.Windows.Forms.ToolStripMenuItem menu_scheme_light;
        private System.Windows.Forms.ToolStripMenuItem menu_color;
        private System.Windows.Forms.ToolStripMenuItem menu_color_blue;
        private System.Windows.Forms.ToolStripMenuItem menu_color_indigo;
        private System.Windows.Forms.ToolStripMenuItem menu_color_green;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.ToolStripStatusLabel bardeviceno;
        private System.Windows.Forms.ToolStripStatusLabel bardbstatus;
        private System.Windows.Forms.ToolStripStatusLabel bartime;
        private System.Windows.Forms.ToolStripStatusLabel barblank;
        private System.Windows.Forms.Timer timer_sys;
        private System.Windows.Forms.ToolStripStatusLabel barversion;
        private System.Windows.Forms.ToolStripStatusLabel barUser;
        private System.Windows.Forms.ToolStripMenuItem menu_user_login;
        private System.Windows.Forms.TableLayoutPanel pnlStatus;
        private System.Windows.Forms.PictureBox picNormalIco;
        private System.Windows.Forms.PictureBox picErrIco;
        private System.Windows.Forms.Timer timer_stat;
        private System.Windows.Forms.ImageList imgStat;
        private System.Windows.Forms.ToolStripMenuItem menu_manual_motor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menu_manual_camera;
        private System.Windows.Forms.ToolStripStatusLabel barRecipe;
        private System.Windows.Forms.ToolStripStatusLabel barCamTemp;
    }
}

