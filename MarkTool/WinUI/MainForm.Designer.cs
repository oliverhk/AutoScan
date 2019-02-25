namespace MarkTool.WinUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnMark = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnClassify = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnSet = new MaterialSkin.Controls.MaterialRaisedButton();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.barXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.barRatio = new System.Windows.Forms.ToolStripStatusLabel();
            this.barBlank = new System.Windows.Forms.ToolStripStatusLabel();
            this.barUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnMarkSave = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnMarkUndo = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.Controls.Add(this.btnMarkUndo);
            this.pnlMenu.Controls.Add(this.btnMarkSave);
            this.pnlMenu.Controls.Add(this.btnMark);
            this.pnlMenu.Controls.Add(this.btnClassify);
            this.pnlMenu.Controls.Add(this.btnSet);
            this.pnlMenu.Controls.Add(this.picLogo);
            this.pnlMenu.Location = new System.Drawing.Point(1, 25);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(1000, 100);
            this.pnlMenu.TabIndex = 0;
            // 
            // btnMark
            // 
            this.btnMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMark.AutoSize = true;
            this.btnMark.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMark.Depth = 0;
            this.btnMark.Icon = global::MarkTool.Properties.Resources.edit;
            this.btnMark.Location = new System.Drawing.Point(636, 38);
            this.btnMark.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMark.Name = "btnMark";
            this.btnMark.Primary = true;
            this.btnMark.Size = new System.Drawing.Size(79, 36);
            this.btnMark.TabIndex = 3;
            this.btnMark.Text = "标记";
            this.btnMark.UseVisualStyleBackColor = true;
            this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
            // 
            // btnClassify
            // 
            this.btnClassify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClassify.AutoSize = true;
            this.btnClassify.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClassify.Depth = 0;
            this.btnClassify.Icon = global::MarkTool.Properties.Resources.maintain;
            this.btnClassify.Location = new System.Drawing.Point(739, 38);
            this.btnClassify.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClassify.Name = "btnClassify";
            this.btnClassify.Primary = true;
            this.btnClassify.Size = new System.Drawing.Size(109, 36);
            this.btnClassify.TabIndex = 2;
            this.btnClassify.Text = "分类设置";
            this.btnClassify.UseVisualStyleBackColor = true;
            this.btnClassify.Click += new System.EventHandler(this.btnClassify_Click);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.AutoSize = true;
            this.btnSet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSet.Depth = 0;
            this.btnSet.Icon = global::MarkTool.Properties.Resources.set;
            this.btnSet.Location = new System.Drawing.Point(868, 38);
            this.btnSet.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSet.Name = "btnSet";
            this.btnSet.Primary = true;
            this.btnSet.Size = new System.Drawing.Size(109, 36);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "系统设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::MarkTool.Properties.Resources.logo;
            this.picLogo.Location = new System.Drawing.Point(11, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(228, 92);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // pnlContent
            // 
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Location = new System.Drawing.Point(0, 125);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1000, 495);
            this.pnlContent.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barXY,
            this.barRatio,
            this.barBlank,
            this.barUser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1000, 30);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // barXY
            // 
            this.barXY.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barXY.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barXY.Name = "barXY";
            this.barXY.Size = new System.Drawing.Size(102, 25);
            this.barXY.Text = "图像坐标 [0,0]";
            // 
            // barRatio
            // 
            this.barRatio.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barRatio.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barRatio.Name = "barRatio";
            this.barRatio.Size = new System.Drawing.Size(69, 25);
            this.barRatio.Text = "缩放比例";
            // 
            // barBlank
            // 
            this.barBlank.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barBlank.Name = "barBlank";
            this.barBlank.Size = new System.Drawing.Size(745, 25);
            this.barBlank.Spring = true;
            // 
            // barUser
            // 
            this.barUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.barUser.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barUser.Name = "barUser";
            this.barUser.Size = new System.Drawing.Size(69, 25);
            this.barUser.Text = "用户登录";
            this.barUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.barUser.Click += new System.EventHandler(this.barUser_Click);
            // 
            // btnMarkSave
            // 
            this.btnMarkSave.AutoSize = true;
            this.btnMarkSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMarkSave.Depth = 0;
            this.btnMarkSave.Icon = global::MarkTool.Properties.Resources.Save32;
            this.btnMarkSave.Location = new System.Drawing.Point(296, 38);
            this.btnMarkSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMarkSave.Name = "btnMarkSave";
            this.btnMarkSave.Primary = true;
            this.btnMarkSave.Size = new System.Drawing.Size(79, 36);
            this.btnMarkSave.TabIndex = 4;
            this.btnMarkSave.Text = "保存";
            this.btnMarkSave.UseVisualStyleBackColor = true;
            this.btnMarkSave.Click += new System.EventHandler(this.btnMarkSave_Click);
            // 
            // btnMarkUndo
            // 
            this.btnMarkUndo.AutoSize = true;
            this.btnMarkUndo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMarkUndo.Depth = 0;
            this.btnMarkUndo.Icon = ((System.Drawing.Image)(resources.GetObject("btnMarkUndo.Icon")));
            this.btnMarkUndo.Location = new System.Drawing.Point(381, 38);
            this.btnMarkUndo.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMarkUndo.Name = "btnMarkUndo";
            this.btnMarkUndo.Primary = true;
            this.btnMarkUndo.Size = new System.Drawing.Size(79, 36);
            this.btnMarkUndo.TabIndex = 5;
            this.btnMarkUndo.Text = "撤销";
            this.btnMarkUndo.UseVisualStyleBackColor = true;
            this.btnMarkUndo.Click += new System.EventHandler(this.btnMarkUndo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlMenu);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.PictureBox picLogo;
        private MaterialSkin.Controls.MaterialRaisedButton btnSet;
        private MaterialSkin.Controls.MaterialRaisedButton btnClassify;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel barXY;
        private System.Windows.Forms.ToolStripStatusLabel barBlank;
        private System.Windows.Forms.ToolStripStatusLabel barUser;
        private MaterialSkin.Controls.MaterialRaisedButton btnMark;
        private System.Windows.Forms.ToolStripStatusLabel barRatio;
        private MaterialSkin.Controls.MaterialRaisedButton btnMarkSave;
        private MaterialSkin.Controls.MaterialRaisedButton btnMarkUndo;
    }
}

