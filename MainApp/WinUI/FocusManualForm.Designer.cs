namespace MainApp.WinUI
{
    partial class FocusManualForm
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
            this.txtImgHeight = new System.Windows.Forms.TextBox();
            this.txtImgWidth = new System.Windows.Forms.TextBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.btnWholeFocus = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btmQucikFocus = new MaterialSkin.Controls.MaterialRaisedButton();
            this.picDraw = new System.Windows.Forms.PictureBox();
            this.btnCamLive = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            this.cmbList = new System.Windows.Forms.ComboBox();
            this.txtStartPos = new System.Windows.Forms.TextBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.txtGap = new System.Windows.Forms.TextBox();
            this.txtLastPos = new System.Windows.Forms.TextBox();
            this.txtCalcPos = new System.Windows.Forms.TextBox();
            this.btnSavePara = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnDeletePara = new MaterialSkin.Controls.MaterialRaisedButton();
            this.labelGapUC2 = new ExportTool.WinUI.LabelGapUC();
            this.labelGapUC1 = new ExportTool.WinUI.LabelGapUC();
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).BeginInit();
            this.SuspendLayout();
            // 
            // txtImgHeight
            // 
            this.txtImgHeight.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtImgHeight.Location = new System.Drawing.Point(114, 154);
            this.txtImgHeight.Name = "txtImgHeight";
            this.txtImgHeight.Size = new System.Drawing.Size(91, 23);
            this.txtImgHeight.TabIndex = 49;
            this.txtImgHeight.Text = "128";
            // 
            // txtImgWidth
            // 
            this.txtImgWidth.Font = new System.Drawing.Font("SimSun", 10F);
            this.txtImgWidth.Location = new System.Drawing.Point(114, 124);
            this.txtImgWidth.Name = "txtImgWidth";
            this.txtImgWidth.Size = new System.Drawing.Size(91, 23);
            this.txtImgWidth.TabIndex = 48;
            this.txtImgWidth.Text = "128";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(24, 156);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(68, 18);
            this.materialLabel2.TabIndex = 47;
            this.materialLabel2.Text = "图像高度";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(24, 126);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(68, 18);
            this.materialLabel1.TabIndex = 46;
            this.materialLabel1.Text = "图像宽度";
            // 
            // btnWholeFocus
            // 
            this.btnWholeFocus.AutoSize = true;
            this.btnWholeFocus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnWholeFocus.Depth = 0;
            this.btnWholeFocus.Icon = null;
            this.btnWholeFocus.Location = new System.Drawing.Point(21, 219);
            this.btnWholeFocus.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnWholeFocus.Name = "btnWholeFocus";
            this.btnWholeFocus.Primary = true;
            this.btnWholeFocus.Size = new System.Drawing.Size(81, 36);
            this.btnWholeFocus.TabIndex = 65;
            this.btnWholeFocus.Text = "全局聚焦";
            this.btnWholeFocus.UseVisualStyleBackColor = true;
            // 
            // btmQucikFocus
            // 
            this.btmQucikFocus.AutoSize = true;
            this.btmQucikFocus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btmQucikFocus.Depth = 0;
            this.btmQucikFocus.Icon = null;
            this.btmQucikFocus.Location = new System.Drawing.Point(129, 219);
            this.btmQucikFocus.MouseState = MaterialSkin.MouseState.HOVER;
            this.btmQucikFocus.Name = "btmQucikFocus";
            this.btmQucikFocus.Primary = true;
            this.btmQucikFocus.Size = new System.Drawing.Size(81, 36);
            this.btmQucikFocus.TabIndex = 66;
            this.btmQucikFocus.Text = "三点聚焦";
            this.btmQucikFocus.UseVisualStyleBackColor = true;
            // 
            // picDraw
            // 
            this.picDraw.Location = new System.Drawing.Point(226, 110);
            this.picDraw.Name = "picDraw";
            this.picDraw.Size = new System.Drawing.Size(128, 128);
            this.picDraw.TabIndex = 2;
            this.picDraw.TabStop = false;
            // 
            // btnCamLive
            // 
            this.btnCamLive.AutoSize = true;
            this.btnCamLive.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCamLive.Depth = 0;
            this.btnCamLive.Icon = null;
            this.btnCamLive.Location = new System.Drawing.Point(21, 180);
            this.btnCamLive.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCamLive.Name = "btnCamLive";
            this.btnCamLive.Primary = true;
            this.btnCamLive.Size = new System.Drawing.Size(83, 36);
            this.btnCamLive.TabIndex = 67;
            this.btnCamLive.Text = "Free Run";
            this.btnCamLive.UseVisualStyleBackColor = true;
            this.btnCamLive.Click += new System.EventHandler(this.btnCamLive_Click);
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(17, 307);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(68, 18);
            this.materialLabel3.TabIndex = 69;
            this.materialLabel3.Text = "参数列表";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(17, 335);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(68, 18);
            this.materialLabel4.TabIndex = 70;
            this.materialLabel4.Text = "起始位置";
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(17, 363);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(68, 18);
            this.materialLabel5.TabIndex = 71;
            this.materialLabel5.Text = "采集次数";
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(17, 391);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(68, 18);
            this.materialLabel6.TabIndex = 72;
            this.materialLabel6.Text = "采集间隔";
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.Location = new System.Drawing.Point(17, 419);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(68, 18);
            this.materialLabel7.TabIndex = 73;
            this.materialLabel7.Text = "终点位置";
            // 
            // materialLabel8
            // 
            this.materialLabel8.AutoSize = true;
            this.materialLabel8.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel8.Depth = 0;
            this.materialLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel8.Location = new System.Drawing.Point(17, 447);
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            this.materialLabel8.Size = new System.Drawing.Size(68, 18);
            this.materialLabel8.TabIndex = 74;
            this.materialLabel8.Text = "最佳位置";
            // 
            // cmbList
            // 
            this.cmbList.FormattingEnabled = true;
            this.cmbList.Location = new System.Drawing.Point(114, 306);
            this.cmbList.Name = "cmbList";
            this.cmbList.Size = new System.Drawing.Size(151, 21);
            this.cmbList.TabIndex = 75;
            this.cmbList.SelectedIndexChanged += new System.EventHandler(this.cmbList_SelectedIndexChanged);
            // 
            // txtStartPos
            // 
            this.txtStartPos.Location = new System.Drawing.Point(114, 333);
            this.txtStartPos.Name = "txtStartPos";
            this.txtStartPos.Size = new System.Drawing.Size(151, 23);
            this.txtStartPos.TabIndex = 76;
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(114, 361);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(151, 23);
            this.txtCount.TabIndex = 77;
            // 
            // txtGap
            // 
            this.txtGap.Location = new System.Drawing.Point(114, 389);
            this.txtGap.Name = "txtGap";
            this.txtGap.Size = new System.Drawing.Size(151, 23);
            this.txtGap.TabIndex = 78;
            // 
            // txtLastPos
            // 
            this.txtLastPos.Location = new System.Drawing.Point(114, 417);
            this.txtLastPos.Name = "txtLastPos";
            this.txtLastPos.ReadOnly = true;
            this.txtLastPos.Size = new System.Drawing.Size(151, 23);
            this.txtLastPos.TabIndex = 79;
            // 
            // txtCalcPos
            // 
            this.txtCalcPos.Location = new System.Drawing.Point(114, 445);
            this.txtCalcPos.Name = "txtCalcPos";
            this.txtCalcPos.ReadOnly = true;
            this.txtCalcPos.Size = new System.Drawing.Size(151, 23);
            this.txtCalcPos.TabIndex = 80;
            // 
            // btnSavePara
            // 
            this.btnSavePara.AutoSize = true;
            this.btnSavePara.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSavePara.Depth = 0;
            this.btnSavePara.Icon = null;
            this.btnSavePara.Location = new System.Drawing.Point(271, 298);
            this.btnSavePara.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSavePara.Name = "btnSavePara";
            this.btnSavePara.Primary = true;
            this.btnSavePara.Size = new System.Drawing.Size(51, 36);
            this.btnSavePara.TabIndex = 81;
            this.btnSavePara.Text = "保存";
            this.btnSavePara.UseVisualStyleBackColor = true;
            this.btnSavePara.Click += new System.EventHandler(this.btnSavePara_Click);
            // 
            // btnDeletePara
            // 
            this.btnDeletePara.AutoSize = true;
            this.btnDeletePara.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDeletePara.Depth = 0;
            this.btnDeletePara.Icon = null;
            this.btnDeletePara.Location = new System.Drawing.Point(328, 298);
            this.btnDeletePara.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDeletePara.Name = "btnDeletePara";
            this.btnDeletePara.Primary = true;
            this.btnDeletePara.Size = new System.Drawing.Size(51, 36);
            this.btnDeletePara.TabIndex = 82;
            this.btnDeletePara.Text = "删除";
            this.btnDeletePara.UseVisualStyleBackColor = true;
            this.btnDeletePara.Click += new System.EventHandler(this.btnDeletePara_Click);
            // 
            // labelGapUC2
            // 
            this.labelGapUC2.BackColor = System.Drawing.Color.Transparent;
            this.labelGapUC2.Font = new System.Drawing.Font("SimSun", 10F);
            this.labelGapUC2.Location = new System.Drawing.Point(12, 270);
            this.labelGapUC2.Name = "labelGapUC2";
            this.labelGapUC2.Size = new System.Drawing.Size(342, 24);
            this.labelGapUC2.TabIndex = 68;
            this.labelGapUC2.Title = "聚焦参数";
            // 
            // labelGapUC1
            // 
            this.labelGapUC1.BackColor = System.Drawing.Color.Transparent;
            this.labelGapUC1.Font = new System.Drawing.Font("SimSun", 10F);
            this.labelGapUC1.Location = new System.Drawing.Point(12, 80);
            this.labelGapUC1.Name = "labelGapUC1";
            this.labelGapUC1.Size = new System.Drawing.Size(342, 24);
            this.labelGapUC1.TabIndex = 3;
            this.labelGapUC1.Title = "相机";
            // 
            // FocusManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 483);
            this.Controls.Add(this.btnDeletePara);
            this.Controls.Add(this.btnSavePara);
            this.Controls.Add(this.txtCalcPos);
            this.Controls.Add(this.txtLastPos);
            this.Controls.Add(this.txtGap);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.txtStartPos);
            this.Controls.Add(this.cmbList);
            this.Controls.Add(this.materialLabel8);
            this.Controls.Add(this.materialLabel7);
            this.Controls.Add(this.materialLabel6);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.labelGapUC2);
            this.Controls.Add(this.btnCamLive);
            this.Controls.Add(this.btmQucikFocus);
            this.Controls.Add(this.btnWholeFocus);
            this.Controls.Add(this.txtImgHeight);
            this.Controls.Add(this.txtImgWidth);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.labelGapUC1);
            this.Controls.Add(this.picDraw);
            this.Font = new System.Drawing.Font("SimSun", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FocusManualForm";
            this.Sizable = false;
            this.Text = "聚焦手动操作";
            this.Load += new System.EventHandler(this.FocusManualForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExportTool.WinUI.LabelGapUC labelGapUC1;
        private System.Windows.Forms.PictureBox picDraw;
        private System.Windows.Forms.TextBox txtImgHeight;
        private System.Windows.Forms.TextBox txtImgWidth;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialRaisedButton btnWholeFocus;
        private MaterialSkin.Controls.MaterialRaisedButton btmQucikFocus;
        private MaterialSkin.Controls.MaterialRaisedButton btnCamLive;
        private ExportTool.WinUI.LabelGapUC labelGapUC2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
        private System.Windows.Forms.ComboBox cmbList;
        private System.Windows.Forms.TextBox txtStartPos;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.TextBox txtGap;
        private System.Windows.Forms.TextBox txtLastPos;
        private System.Windows.Forms.TextBox txtCalcPos;
        private MaterialSkin.Controls.MaterialRaisedButton btnSavePara;
        private MaterialSkin.Controls.MaterialRaisedButton btnDeletePara;
    }
}