namespace MainApp.WinUI
{
    partial class MotorManualForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotorManualForm));
            this.picDraw = new System.Windows.Forms.PictureBox();
            this.txtZstep = new System.Windows.Forms.TextBox();
            this.txtYstep = new System.Windows.Forms.TextBox();
            this.txtXstep = new System.Windows.Forms.TextBox();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.btnZRight = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnYRight = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnXRight = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnZLeft = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnYleft = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnXLeft = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtZspeed = new System.Windows.Forms.TextBox();
            this.txtYspeed = new System.Windows.Forms.TextBox();
            this.txtXspeed = new System.Windows.Forms.TextBox();
            this.txtZpos = new System.Windows.Forms.TextBox();
            this.txtYpos = new System.Windows.Forms.TextBox();
            this.txtXpos = new System.Windows.Forms.TextBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.btnZHome = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnXHome = new MaterialSkin.Controls.MaterialRaisedButton();
            this.picHead = new System.Windows.Forms.PictureBox();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.picDiag1 = new System.Windows.Forms.PictureBox();
            this.picDiag2 = new System.Windows.Forms.PictureBox();
            this.btnHomeAll = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnStopAll = new MaterialSkin.Controls.MaterialRaisedButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelGapUC2 = new ExportTool.WinUI.LabelGapUC();
            this.labelGapUC1 = new ExportTool.WinUI.LabelGapUC();
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDiag1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDiag2)).BeginInit();
            this.SuspendLayout();
            // 
            // picDraw
            // 
            this.picDraw.Location = new System.Drawing.Point(83, 104);
            this.picDraw.Name = "picDraw";
            this.picDraw.Size = new System.Drawing.Size(315, 75);
            this.picDraw.TabIndex = 0;
            this.picDraw.TabStop = false;
            // 
            // txtZstep
            // 
            this.txtZstep.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtZstep.Location = new System.Drawing.Point(234, 509);
            this.txtZstep.Name = "txtZstep";
            this.txtZstep.Size = new System.Drawing.Size(51, 23);
            this.txtZstep.TabIndex = 59;
            this.txtZstep.Text = "100";
            // 
            // txtYstep
            // 
            this.txtYstep.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtYstep.Location = new System.Drawing.Point(234, 477);
            this.txtYstep.Name = "txtYstep";
            this.txtYstep.Size = new System.Drawing.Size(51, 23);
            this.txtYstep.TabIndex = 58;
            this.txtYstep.Text = "100";
            // 
            // txtXstep
            // 
            this.txtXstep.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtXstep.Location = new System.Drawing.Point(234, 447);
            this.txtXstep.Name = "txtXstep";
            this.txtXstep.Size = new System.Drawing.Size(51, 23);
            this.txtXstep.TabIndex = 57;
            this.txtXstep.Text = "100";
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(234, 418);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(78, 18);
            this.materialLabel6.TabIndex = 56;
            this.materialLabel6.Text = "步长(微米)";
            // 
            // btnZRight
            // 
            this.btnZRight.AutoSize = true;
            this.btnZRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZRight.Depth = 0;
            this.btnZRight.Icon = ((System.Drawing.Image)(resources.GetObject("btnZRight.Icon")));
            this.btnZRight.Location = new System.Drawing.Point(297, 351);
            this.btnZRight.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnZRight.Name = "btnZRight";
            this.btnZRight.Primary = true;
            this.btnZRight.Size = new System.Drawing.Size(44, 36);
            this.btnZRight.TabIndex = 55;
            this.btnZRight.UseVisualStyleBackColor = true;
            this.btnZRight.Click += new System.EventHandler(this.btnZRight_Click);
            // 
            // btnYRight
            // 
            this.btnYRight.AutoSize = true;
            this.btnYRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnYRight.Depth = 0;
            this.btnYRight.Icon = ((System.Drawing.Image)(resources.GetObject("btnYRight.Icon")));
            this.btnYRight.Location = new System.Drawing.Point(90, 351);
            this.btnYRight.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnYRight.Name = "btnYRight";
            this.btnYRight.Primary = true;
            this.btnYRight.Size = new System.Drawing.Size(44, 36);
            this.btnYRight.TabIndex = 54;
            this.btnYRight.UseVisualStyleBackColor = true;
            this.btnYRight.Click += new System.EventHandler(this.btnYRight_Click);
            // 
            // btnXRight
            // 
            this.btnXRight.AutoSize = true;
            this.btnXRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnXRight.Depth = 0;
            this.btnXRight.Icon = ((System.Drawing.Image)(resources.GetObject("btnXRight.Icon")));
            this.btnXRight.Location = new System.Drawing.Point(144, 307);
            this.btnXRight.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnXRight.Name = "btnXRight";
            this.btnXRight.Primary = true;
            this.btnXRight.Size = new System.Drawing.Size(44, 36);
            this.btnXRight.TabIndex = 53;
            this.btnXRight.UseVisualStyleBackColor = true;
            this.btnXRight.Click += new System.EventHandler(this.btnXRight_Click);
            // 
            // btnZLeft
            // 
            this.btnZLeft.AutoSize = true;
            this.btnZLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZLeft.Depth = 0;
            this.btnZLeft.Icon = ((System.Drawing.Image)(resources.GetObject("btnZLeft.Icon")));
            this.btnZLeft.Location = new System.Drawing.Point(297, 261);
            this.btnZLeft.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnZLeft.Name = "btnZLeft";
            this.btnZLeft.Primary = true;
            this.btnZLeft.Size = new System.Drawing.Size(44, 36);
            this.btnZLeft.TabIndex = 52;
            this.btnZLeft.UseVisualStyleBackColor = true;
            this.btnZLeft.Click += new System.EventHandler(this.btnZLeft_Click);
            // 
            // btnYleft
            // 
            this.btnYleft.AutoSize = true;
            this.btnYleft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnYleft.Depth = 0;
            this.btnYleft.Icon = ((System.Drawing.Image)(resources.GetObject("btnYleft.Icon")));
            this.btnYleft.Location = new System.Drawing.Point(90, 262);
            this.btnYleft.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnYleft.Name = "btnYleft";
            this.btnYleft.Primary = true;
            this.btnYleft.Size = new System.Drawing.Size(44, 36);
            this.btnYleft.TabIndex = 51;
            this.btnYleft.UseVisualStyleBackColor = true;
            this.btnYleft.Click += new System.EventHandler(this.btnYleft_Click);
            // 
            // btnXLeft
            // 
            this.btnXLeft.AutoSize = true;
            this.btnXLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnXLeft.Depth = 0;
            this.btnXLeft.Icon = ((System.Drawing.Image)(resources.GetObject("btnXLeft.Icon")));
            this.btnXLeft.Location = new System.Drawing.Point(37, 307);
            this.btnXLeft.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnXLeft.Name = "btnXLeft";
            this.btnXLeft.Primary = true;
            this.btnXLeft.Size = new System.Drawing.Size(44, 36);
            this.btnXLeft.TabIndex = 50;
            this.btnXLeft.UseVisualStyleBackColor = true;
            this.btnXLeft.Click += new System.EventHandler(this.btnXLeft_Click);
            // 
            // txtZspeed
            // 
            this.txtZspeed.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtZspeed.Location = new System.Drawing.Point(127, 509);
            this.txtZspeed.Name = "txtZspeed";
            this.txtZspeed.Size = new System.Drawing.Size(101, 23);
            this.txtZspeed.TabIndex = 49;
            this.txtZspeed.Text = "200";
            this.txtZspeed.TextChanged += new System.EventHandler(this.txtZspeed_TextChanged);
            // 
            // txtYspeed
            // 
            this.txtYspeed.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtYspeed.Location = new System.Drawing.Point(127, 477);
            this.txtYspeed.Name = "txtYspeed";
            this.txtYspeed.Size = new System.Drawing.Size(101, 23);
            this.txtYspeed.TabIndex = 48;
            this.txtYspeed.Text = "50000";
            this.txtYspeed.TextChanged += new System.EventHandler(this.txtYspeed_TextChanged);
            // 
            // txtXspeed
            // 
            this.txtXspeed.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtXspeed.Location = new System.Drawing.Point(127, 447);
            this.txtXspeed.Name = "txtXspeed";
            this.txtXspeed.Size = new System.Drawing.Size(101, 23);
            this.txtXspeed.TabIndex = 47;
            this.txtXspeed.Text = "50000";
            this.txtXspeed.TextChanged += new System.EventHandler(this.txtXspeed_TextChanged);
            // 
            // txtZpos
            // 
            this.txtZpos.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtZpos.Location = new System.Drawing.Point(34, 509);
            this.txtZpos.Name = "txtZpos";
            this.txtZpos.ReadOnly = true;
            this.txtZpos.Size = new System.Drawing.Size(91, 23);
            this.txtZpos.TabIndex = 46;
            this.txtZpos.Text = "0";
            // 
            // txtYpos
            // 
            this.txtYpos.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtYpos.Location = new System.Drawing.Point(34, 477);
            this.txtYpos.Name = "txtYpos";
            this.txtYpos.ReadOnly = true;
            this.txtYpos.Size = new System.Drawing.Size(91, 23);
            this.txtYpos.TabIndex = 45;
            this.txtYpos.Text = "0";
            // 
            // txtXpos
            // 
            this.txtXpos.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtXpos.Location = new System.Drawing.Point(34, 447);
            this.txtXpos.Name = "txtXpos";
            this.txtXpos.ReadOnly = true;
            this.txtXpos.Size = new System.Drawing.Size(91, 23);
            this.txtXpos.TabIndex = 44;
            this.txtXpos.Text = "0";
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(123, 418);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(97, 18);
            this.materialLabel5.TabIndex = 43;
            this.materialLabel5.Text = "速度(微米/秒)";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(34, 418);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(78, 18);
            this.materialLabel4.TabIndex = 42;
            this.materialLabel4.Text = "位置(微米)";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(10, 511);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(17, 18);
            this.materialLabel3.TabIndex = 41;
            this.materialLabel3.Text = "Z";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(10, 479);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(17, 18);
            this.materialLabel2.TabIndex = 40;
            this.materialLabel2.Text = "Y";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(10, 449);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(18, 18);
            this.materialLabel1.TabIndex = 39;
            this.materialLabel1.Text = "X";
            // 
            // btnZHome
            // 
            this.btnZHome.AutoSize = true;
            this.btnZHome.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZHome.Depth = 0;
            this.btnZHome.Icon = null;
            this.btnZHome.Location = new System.Drawing.Point(294, 307);
            this.btnZHome.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnZHome.Name = "btnZHome";
            this.btnZHome.Primary = true;
            this.btnZHome.Size = new System.Drawing.Size(51, 36);
            this.btnZHome.TabIndex = 38;
            this.btnZHome.Text = "原点";
            this.btnZHome.UseVisualStyleBackColor = true;
            this.btnZHome.Click += new System.EventHandler(this.btnZHome_Click);
            // 
            // btnXHome
            // 
            this.btnXHome.AutoSize = true;
            this.btnXHome.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnXHome.Depth = 0;
            this.btnXHome.Icon = null;
            this.btnXHome.Location = new System.Drawing.Point(87, 307);
            this.btnXHome.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnXHome.Name = "btnXHome";
            this.btnXHome.Primary = true;
            this.btnXHome.Size = new System.Drawing.Size(51, 36);
            this.btnXHome.TabIndex = 36;
            this.btnXHome.Text = "原点";
            this.btnXHome.UseVisualStyleBackColor = true;
            this.btnXHome.Click += new System.EventHandler(this.btnXHome_Click);
            // 
            // picHead
            // 
            this.picHead.Location = new System.Drawing.Point(12, 104);
            this.picHead.Name = "picHead";
            this.picHead.Size = new System.Drawing.Size(60, 75);
            this.picHead.TabIndex = 60;
            this.picHead.TabStop = false;
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.Location = new System.Drawing.Point(83, 198);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(102, 18);
            this.materialLabel7.TabIndex = 61;
            this.materialLabel7.Text = "坐标位置:{0,0}";
            // 
            // picDiag1
            // 
            this.picDiag1.Image = ((System.Drawing.Image)(resources.GetObject("picDiag1.Image")));
            this.picDiag1.Location = new System.Drawing.Point(12, 364);
            this.picDiag1.Name = "picDiag1";
            this.picDiag1.Size = new System.Drawing.Size(43, 43);
            this.picDiag1.TabIndex = 62;
            this.picDiag1.TabStop = false;
            // 
            // picDiag2
            // 
            this.picDiag2.Image = ((System.Drawing.Image)(resources.GetObject("picDiag2.Image")));
            this.picDiag2.Location = new System.Drawing.Point(236, 364);
            this.picDiag2.Name = "picDiag2";
            this.picDiag2.Size = new System.Drawing.Size(43, 43);
            this.picDiag2.TabIndex = 63;
            this.picDiag2.TabStop = false;
            // 
            // btnHomeAll
            // 
            this.btnHomeAll.AutoSize = true;
            this.btnHomeAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHomeAll.Depth = 0;
            this.btnHomeAll.Icon = null;
            this.btnHomeAll.Location = new System.Drawing.Point(327, 447);
            this.btnHomeAll.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnHomeAll.Name = "btnHomeAll";
            this.btnHomeAll.Primary = true;
            this.btnHomeAll.Size = new System.Drawing.Size(81, 36);
            this.btnHomeAll.TabIndex = 64;
            this.btnHomeAll.Text = "原点归位";
            this.btnHomeAll.UseVisualStyleBackColor = true;
            this.btnHomeAll.Click += new System.EventHandler(this.btnHomeAll_Click);
            // 
            // btnStopAll
            // 
            this.btnStopAll.AutoSize = true;
            this.btnStopAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStopAll.Depth = 0;
            this.btnStopAll.Icon = null;
            this.btnStopAll.Location = new System.Drawing.Point(327, 493);
            this.btnStopAll.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Primary = true;
            this.btnStopAll.Size = new System.Drawing.Size(81, 36);
            this.btnStopAll.TabIndex = 65;
            this.btnStopAll.Text = "停止运动";
            this.btnStopAll.UseVisualStyleBackColor = true;
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelGapUC2
            // 
            this.labelGapUC2.BackColor = System.Drawing.Color.Transparent;
            this.labelGapUC2.Font = new System.Drawing.Font("SimSun", 10F);
            this.labelGapUC2.Location = new System.Drawing.Point(12, 220);
            this.labelGapUC2.Name = "labelGapUC2";
            this.labelGapUC2.Size = new System.Drawing.Size(390, 24);
            this.labelGapUC2.TabIndex = 35;
            this.labelGapUC2.Title = "参数";
            // 
            // labelGapUC1
            // 
            this.labelGapUC1.BackColor = System.Drawing.Color.Transparent;
            this.labelGapUC1.Font = new System.Drawing.Font("SimSun", 10F);
            this.labelGapUC1.Location = new System.Drawing.Point(12, 74);
            this.labelGapUC1.Name = "labelGapUC1";
            this.labelGapUC1.Size = new System.Drawing.Size(390, 24);
            this.labelGapUC1.TabIndex = 1;
            this.labelGapUC1.Title = "操作";
            // 
            // MotorManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 550);
            this.Controls.Add(this.btnStopAll);
            this.Controls.Add(this.btnHomeAll);
            this.Controls.Add(this.picDiag2);
            this.Controls.Add(this.picDiag1);
            this.Controls.Add(this.materialLabel7);
            this.Controls.Add(this.picHead);
            this.Controls.Add(this.txtZstep);
            this.Controls.Add(this.txtYstep);
            this.Controls.Add(this.txtXstep);
            this.Controls.Add(this.materialLabel6);
            this.Controls.Add(this.btnZRight);
            this.Controls.Add(this.btnYRight);
            this.Controls.Add(this.btnXRight);
            this.Controls.Add(this.btnZLeft);
            this.Controls.Add(this.btnYleft);
            this.Controls.Add(this.btnXLeft);
            this.Controls.Add(this.txtZspeed);
            this.Controls.Add(this.txtYspeed);
            this.Controls.Add(this.txtXspeed);
            this.Controls.Add(this.txtZpos);
            this.Controls.Add(this.txtYpos);
            this.Controls.Add(this.txtXpos);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.btnZHome);
            this.Controls.Add(this.btnXHome);
            this.Controls.Add(this.labelGapUC2);
            this.Controls.Add(this.labelGapUC1);
            this.Controls.Add(this.picDraw);
            this.Font = new System.Drawing.Font("SimSun", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MotorManualForm";
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.Text = "控制器手动操作";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MotorManualForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDiag1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDiag2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDraw;
        private ExportTool.WinUI.LabelGapUC labelGapUC1;
        private System.Windows.Forms.TextBox txtZstep;
        private System.Windows.Forms.TextBox txtYstep;
        private System.Windows.Forms.TextBox txtXstep;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialRaisedButton btnZRight;
        private MaterialSkin.Controls.MaterialRaisedButton btnYRight;
        private MaterialSkin.Controls.MaterialRaisedButton btnXRight;
        private MaterialSkin.Controls.MaterialRaisedButton btnZLeft;
        private MaterialSkin.Controls.MaterialRaisedButton btnYleft;
        private MaterialSkin.Controls.MaterialRaisedButton btnXLeft;
        private System.Windows.Forms.TextBox txtZspeed;
        private System.Windows.Forms.TextBox txtYspeed;
        private System.Windows.Forms.TextBox txtXspeed;
        private System.Windows.Forms.TextBox txtZpos;
        private System.Windows.Forms.TextBox txtYpos;
        private System.Windows.Forms.TextBox txtXpos;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialRaisedButton btnZHome;
        private MaterialSkin.Controls.MaterialRaisedButton btnXHome;
        private ExportTool.WinUI.LabelGapUC labelGapUC2;
        private System.Windows.Forms.PictureBox picHead;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private System.Windows.Forms.PictureBox picDiag1;
        private System.Windows.Forms.PictureBox picDiag2;
        private MaterialSkin.Controls.MaterialRaisedButton btnHomeAll;
        private MaterialSkin.Controls.MaterialRaisedButton btnStopAll;
        private System.Windows.Forms.Timer timer1;
    }
}