namespace MarkTool.WinUI
{
    partial class OperatorForm
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
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.cmsMarkMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiResize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi25 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi150 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pbImageView = new System.Windows.Forms.PictureBox();
            this.pnlStat = new System.Windows.Forms.Panel();
            this.txtCurSwath = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtCurSequence = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtCurPath = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtSlideId = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.dgvMark = new System.Windows.Forms.DataGridView();
            this.areaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userlevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slideid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.swathid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imagex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imagey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stagex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stagey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slidefrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.celltype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkMark = new MaterialSkin.Controls.MaterialCheckBox();
            this.btnNextSwath = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnPrevSwath = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnNextSeq = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnPrevSeq = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.cmsMarkPanelMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMarkAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMarkGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMarkGridDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMarkMenu.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImageView)).BeginInit();
            this.pnlStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMark)).BeginInit();
            this.cmsMarkPanelMenu.SuspendLayout();
            this.cmsMarkGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = null;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 0);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(938, 23);
            this.materialTabSelector1.TabIndex = 0;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // cmsMarkMenu
            // 
            this.cmsMarkMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResize,
            this.tsmiRect,
            this.tsmiCircle,
            this.tsmiAdd,
            this.tsmiUpdate,
            this.tsmiDelete});
            this.cmsMarkMenu.Name = "cmsMarkMenu";
            this.cmsMarkMenu.Size = new System.Drawing.Size(125, 136);
            // 
            // tsmiResize
            // 
            this.tsmiResize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi25,
            this.tsmi50,
            this.tsmi100,
            this.tsmi150,
            this.tsmi200});
            this.tsmiResize.Name = "tsmiResize";
            this.tsmiResize.Size = new System.Drawing.Size(124, 22);
            this.tsmiResize.Text = "图像放大";
            // 
            // tsmi25
            // 
            this.tsmi25.Name = "tsmi25";
            this.tsmi25.Size = new System.Drawing.Size(108, 22);
            this.tsmi25.Text = "25%";
            this.tsmi25.Click += new System.EventHandler(this.tsmi25_Click);
            // 
            // tsmi50
            // 
            this.tsmi50.Name = "tsmi50";
            this.tsmi50.Size = new System.Drawing.Size(108, 22);
            this.tsmi50.Text = "50%";
            this.tsmi50.Click += new System.EventHandler(this.tsmi50_Click);
            // 
            // tsmi100
            // 
            this.tsmi100.Name = "tsmi100";
            this.tsmi100.Size = new System.Drawing.Size(108, 22);
            this.tsmi100.Text = "100%";
            this.tsmi100.Click += new System.EventHandler(this.tsmi100_Click);
            // 
            // tsmi150
            // 
            this.tsmi150.Name = "tsmi150";
            this.tsmi150.Size = new System.Drawing.Size(108, 22);
            this.tsmi150.Text = "150%";
            this.tsmi150.Click += new System.EventHandler(this.tsmi150_Click);
            // 
            // tsmi200
            // 
            this.tsmi200.Name = "tsmi200";
            this.tsmi200.Size = new System.Drawing.Size(108, 22);
            this.tsmi200.Text = "200%";
            this.tsmi200.Click += new System.EventHandler(this.tsmi200_Click);
            // 
            // tsmiRect
            // 
            this.tsmiRect.Name = "tsmiRect";
            this.tsmiRect.Size = new System.Drawing.Size(124, 22);
            this.tsmiRect.Text = "矩形";
            this.tsmiRect.Click += new System.EventHandler(this.tsmiRect_Click);
            // 
            // tsmiCircle
            // 
            this.tsmiCircle.Enabled = false;
            this.tsmiCircle.Name = "tsmiCircle";
            this.tsmiCircle.Size = new System.Drawing.Size(124, 22);
            this.tsmiCircle.Text = "圆形";
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(124, 22);
            this.tsmiAdd.Text = "新增标记";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(124, 22);
            this.tsmiUpdate.Text = "修改标记";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdate_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Enabled = false;
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(124, 22);
            this.tsmiDelete.Text = "删除标记";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // pnlImage
            // 
            this.pnlImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImage.AutoScroll = true;
            this.pnlImage.Controls.Add(this.pbImageView);
            this.pnlImage.Location = new System.Drawing.Point(12, 24);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(510, 525);
            this.pnlImage.TabIndex = 2;
            // 
            // pbImageView
            // 
            this.pbImageView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImageView.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbImageView.Location = new System.Drawing.Point(0, 0);
            this.pbImageView.Name = "pbImageView";
            this.pbImageView.Size = new System.Drawing.Size(510, 525);
            this.pbImageView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImageView.TabIndex = 0;
            this.pbImageView.TabStop = false;
            this.pbImageView.Visible = false;
            this.pbImageView.DoubleClick += new System.EventHandler(this.pbImageView_DoubleClick);
            this.pbImageView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbImg_MouseClick);
            this.pbImageView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbImageView_MouseDown);
            this.pbImageView.MouseEnter += new System.EventHandler(this.pbImageView_MouseEnter);
            this.pbImageView.MouseLeave += new System.EventHandler(this.pbImageView_MouseLeave);
            this.pbImageView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbImg_MouseMove);
            this.pbImageView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbImageView_MouseUp);
            // 
            // pnlStat
            // 
            this.pnlStat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStat.Controls.Add(this.txtCurSwath);
            this.pnlStat.Controls.Add(this.txtCurSequence);
            this.pnlStat.Controls.Add(this.txtCurPath);
            this.pnlStat.Controls.Add(this.txtSlideId);
            this.pnlStat.Controls.Add(this.dgvMark);
            this.pnlStat.Controls.Add(this.chkMark);
            this.pnlStat.Controls.Add(this.btnNextSwath);
            this.pnlStat.Controls.Add(this.btnPrevSwath);
            this.pnlStat.Controls.Add(this.btnNextSeq);
            this.pnlStat.Controls.Add(this.btnPrevSeq);
            this.pnlStat.Controls.Add(this.materialLabel4);
            this.pnlStat.Controls.Add(this.materialLabel3);
            this.pnlStat.Controls.Add(this.materialLabel2);
            this.pnlStat.Controls.Add(this.materialLabel1);
            this.pnlStat.Location = new System.Drawing.Point(522, 24);
            this.pnlStat.Name = "pnlStat";
            this.pnlStat.Size = new System.Drawing.Size(416, 525);
            this.pnlStat.TabIndex = 3;
            // 
            // txtCurSwath
            // 
            this.txtCurSwath.Depth = 0;
            this.txtCurSwath.Enabled = false;
            this.txtCurSwath.Hint = "";
            this.txtCurSwath.Location = new System.Drawing.Point(293, 96);
            this.txtCurSwath.MaxLength = 32767;
            this.txtCurSwath.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCurSwath.Name = "txtCurSwath";
            this.txtCurSwath.PasswordChar = '\0';
            this.txtCurSwath.SelectedText = "";
            this.txtCurSwath.SelectionLength = 0;
            this.txtCurSwath.SelectionStart = 0;
            this.txtCurSwath.Size = new System.Drawing.Size(107, 23);
            this.txtCurSwath.TabIndex = 13;
            this.txtCurSwath.TabStop = false;
            this.txtCurSwath.UseSystemPasswordChar = false;
            // 
            // txtCurSequence
            // 
            this.txtCurSequence.Depth = 0;
            this.txtCurSequence.Enabled = false;
            this.txtCurSequence.Hint = "";
            this.txtCurSequence.Location = new System.Drawing.Point(86, 96);
            this.txtCurSequence.MaxLength = 32767;
            this.txtCurSequence.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCurSequence.Name = "txtCurSequence";
            this.txtCurSequence.PasswordChar = '\0';
            this.txtCurSequence.SelectedText = "";
            this.txtCurSequence.SelectionLength = 0;
            this.txtCurSequence.SelectionStart = 0;
            this.txtCurSequence.Size = new System.Drawing.Size(140, 23);
            this.txtCurSequence.TabIndex = 12;
            this.txtCurSequence.TabStop = false;
            this.txtCurSequence.UseSystemPasswordChar = false;
            // 
            // txtCurPath
            // 
            this.txtCurPath.Depth = 0;
            this.txtCurPath.Enabled = false;
            this.txtCurPath.Hint = "";
            this.txtCurPath.Location = new System.Drawing.Point(101, 57);
            this.txtCurPath.MaxLength = 32767;
            this.txtCurPath.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCurPath.Name = "txtCurPath";
            this.txtCurPath.PasswordChar = '\0';
            this.txtCurPath.SelectedText = "";
            this.txtCurPath.SelectionLength = 0;
            this.txtCurPath.SelectionStart = 0;
            this.txtCurPath.Size = new System.Drawing.Size(303, 23);
            this.txtCurPath.TabIndex = 11;
            this.txtCurPath.TabStop = false;
            this.txtCurPath.UseSystemPasswordChar = false;
            // 
            // txtSlideId
            // 
            this.txtSlideId.Depth = 0;
            this.txtSlideId.Enabled = false;
            this.txtSlideId.Hint = "";
            this.txtSlideId.Location = new System.Drawing.Point(101, 19);
            this.txtSlideId.MaxLength = 32767;
            this.txtSlideId.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtSlideId.Name = "txtSlideId";
            this.txtSlideId.PasswordChar = '\0';
            this.txtSlideId.SelectedText = "";
            this.txtSlideId.SelectionLength = 0;
            this.txtSlideId.SelectionStart = 0;
            this.txtSlideId.Size = new System.Drawing.Size(303, 23);
            this.txtSlideId.TabIndex = 10;
            this.txtSlideId.TabStop = false;
            this.txtSlideId.UseSystemPasswordChar = false;
            // 
            // dgvMark
            // 
            this.dgvMark.AllowUserToAddRows = false;
            this.dgvMark.AllowUserToOrderColumns = true;
            this.dgvMark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMark.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMark.BackgroundColor = System.Drawing.Color.White;
            this.dgvMark.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMark.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.areaid,
            this.catename,
            this.typename,
            this.userid,
            this.userlevel,
            this.id,
            this.slideid,
            this.swathid,
            this.fieldid,
            this.imagex,
            this.imagey,
            this.stagex,
            this.stagey,
            this.remarks,
            this.slidefrom,
            this.createtime,
            this.celltype});
            this.dgvMark.Location = new System.Drawing.Point(8, 243);
            this.dgvMark.Name = "dgvMark";
            this.dgvMark.ReadOnly = true;
            this.dgvMark.RowHeadersWidth = 4;
            this.dgvMark.RowTemplate.Height = 23;
            this.dgvMark.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMark.Size = new System.Drawing.Size(396, 269);
            this.dgvMark.TabIndex = 9;
            this.dgvMark.VirtualMode = true;
            this.dgvMark.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMark_CellClick);
            this.dgvMark.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMark_CellMouseDown);
            this.dgvMark.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvMark_CellValueNeeded);
            this.dgvMark.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvMark_UserDeletingRow);
            this.dgvMark.Leave += new System.EventHandler(this.dgvMark_Leave);
            // 
            // areaid
            // 
            this.areaid.HeaderText = "区域编号";
            this.areaid.Name = "areaid";
            this.areaid.ReadOnly = true;
            this.areaid.Width = 78;
            // 
            // catename
            // 
            this.catename.DataPropertyName = "";
            this.catename.HeaderText = "细胞种类";
            this.catename.Name = "catename";
            this.catename.ReadOnly = true;
            this.catename.Width = 78;
            // 
            // typename
            // 
            this.typename.HeaderText = "细胞类型";
            this.typename.Name = "typename";
            this.typename.ReadOnly = true;
            this.typename.Width = 78;
            // 
            // userid
            // 
            this.userid.HeaderText = "标记人";
            this.userid.Name = "userid";
            this.userid.ReadOnly = true;
            this.userid.Width = 66;
            // 
            // userlevel
            // 
            this.userlevel.HeaderText = "标记人权限";
            this.userlevel.Name = "userlevel";
            this.userlevel.ReadOnly = true;
            this.userlevel.Width = 90;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.id.HeaderText = "标记编号";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 78;
            // 
            // slideid
            // 
            this.slideid.HeaderText = "切片序号";
            this.slideid.Name = "slideid";
            this.slideid.ReadOnly = true;
            this.slideid.Width = 78;
            // 
            // swathid
            // 
            this.swathid.HeaderText = "图片条号";
            this.swathid.Name = "swathid";
            this.swathid.ReadOnly = true;
            this.swathid.Width = 78;
            // 
            // fieldid
            // 
            this.fieldid.HeaderText = "图片序号";
            this.fieldid.Name = "fieldid";
            this.fieldid.ReadOnly = true;
            this.fieldid.Width = 78;
            // 
            // imagex
            // 
            this.imagex.HeaderText = "图像横坐标";
            this.imagex.Name = "imagex";
            this.imagex.ReadOnly = true;
            this.imagex.Width = 90;
            // 
            // imagey
            // 
            this.imagey.HeaderText = "图像纵坐标";
            this.imagey.Name = "imagey";
            this.imagey.ReadOnly = true;
            this.imagey.Width = 90;
            // 
            // stagex
            // 
            this.stagex.HeaderText = "平台横坐标";
            this.stagex.Name = "stagex";
            this.stagex.ReadOnly = true;
            this.stagex.Width = 90;
            // 
            // stagey
            // 
            this.stagey.HeaderText = "平台纵坐标";
            this.stagey.Name = "stagey";
            this.stagey.ReadOnly = true;
            this.stagey.Width = 90;
            // 
            // remarks
            // 
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Width = 54;
            // 
            // slidefrom
            // 
            this.slidefrom.HeaderText = "SlideFrom";
            this.slidefrom.Name = "slidefrom";
            this.slidefrom.ReadOnly = true;
            this.slidefrom.Width = 84;
            // 
            // createtime
            // 
            this.createtime.HeaderText = "创建时间";
            this.createtime.Name = "createtime";
            this.createtime.ReadOnly = true;
            this.createtime.Width = 78;
            // 
            // celltype
            // 
            this.celltype.HeaderText = "celltypeHidden";
            this.celltype.Name = "celltype";
            this.celltype.ReadOnly = true;
            this.celltype.Visible = false;
            this.celltype.Width = 114;
            // 
            // chkMark
            // 
            this.chkMark.AutoSize = true;
            this.chkMark.Depth = 0;
            this.chkMark.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkMark.Location = new System.Drawing.Point(8, 210);
            this.chkMark.Margin = new System.Windows.Forms.Padding(0);
            this.chkMark.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkMark.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkMark.Name = "chkMark";
            this.chkMark.Ripple = true;
            this.chkMark.Size = new System.Drawing.Size(90, 30);
            this.chkMark.TabIndex = 8;
            this.chkMark.Text = "显示标记";
            this.chkMark.UseVisualStyleBackColor = true;
            this.chkMark.CheckedChanged += new System.EventHandler(this.chkMark_CheckChange);
            // 
            // btnNextSwath
            // 
            this.btnNextSwath.AutoSize = true;
            this.btnNextSwath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNextSwath.Depth = 0;
            this.btnNextSwath.Icon = null;
            this.btnNextSwath.Location = new System.Drawing.Point(298, 154);
            this.btnNextSwath.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnNextSwath.Name = "btnNextSwath";
            this.btnNextSwath.Primary = true;
            this.btnNextSwath.Size = new System.Drawing.Size(66, 36);
            this.btnNextSwath.TabIndex = 7;
            this.btnNextSwath.Text = "下一条";
            this.btnNextSwath.UseVisualStyleBackColor = true;
            this.btnNextSwath.Click += new System.EventHandler(this.btnNextSwath_Click);
            // 
            // btnPrevSwath
            // 
            this.btnPrevSwath.AutoSize = true;
            this.btnPrevSwath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrevSwath.Depth = 0;
            this.btnPrevSwath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrevSwath.Icon = null;
            this.btnPrevSwath.Location = new System.Drawing.Point(226, 154);
            this.btnPrevSwath.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrevSwath.Name = "btnPrevSwath";
            this.btnPrevSwath.Primary = true;
            this.btnPrevSwath.Size = new System.Drawing.Size(66, 36);
            this.btnPrevSwath.TabIndex = 6;
            this.btnPrevSwath.Text = "上一条";
            this.btnPrevSwath.UseVisualStyleBackColor = true;
            this.btnPrevSwath.Click += new System.EventHandler(this.btnPrevSwath_Click);
            // 
            // btnNextSeq
            // 
            this.btnNextSeq.AutoSize = true;
            this.btnNextSeq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNextSeq.Depth = 0;
            this.btnNextSeq.Icon = null;
            this.btnNextSeq.Location = new System.Drawing.Point(98, 154);
            this.btnNextSeq.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnNextSeq.Name = "btnNextSeq";
            this.btnNextSeq.Primary = true;
            this.btnNextSeq.Size = new System.Drawing.Size(66, 36);
            this.btnNextSeq.TabIndex = 5;
            this.btnNextSeq.Text = "下一张";
            this.btnNextSeq.UseVisualStyleBackColor = true;
            this.btnNextSeq.Click += new System.EventHandler(this.btnNextSeq_Click);
            // 
            // btnPrevSeq
            // 
            this.btnPrevSeq.AutoSize = true;
            this.btnPrevSeq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrevSeq.Depth = 0;
            this.btnPrevSeq.Icon = null;
            this.btnPrevSeq.Location = new System.Drawing.Point(26, 154);
            this.btnPrevSeq.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrevSeq.Name = "btnPrevSeq";
            this.btnPrevSeq.Primary = true;
            this.btnPrevSeq.Size = new System.Drawing.Size(66, 36);
            this.btnPrevSeq.TabIndex = 4;
            this.btnPrevSeq.Text = "上一张";
            this.btnPrevSeq.UseVisualStyleBackColor = true;
            this.btnPrevSeq.Click += new System.EventHandler(this.btnPrevSeq_Click);
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(232, 101);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(57, 19);
            this.materialLabel4.TabIndex = 3;
            this.materialLabel4.Text = "当前条";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(25, 101);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(57, 19);
            this.materialLabel3.TabIndex = 2;
            this.materialLabel3.Text = "当前序";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(22, 61);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(73, 19);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "当前路径";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(22, 23);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(73, 19);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "切片序号";
            // 
            // cmsMarkPanelMenu
            // 
            this.cmsMarkPanelMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsPanel,
            this.tsmiMarkAdd});
            this.cmsMarkPanelMenu.Name = "cmsMarkPanelMenu";
            this.cmsMarkPanelMenu.Size = new System.Drawing.Size(125, 48);
            // 
            // cmsPanel
            // 
            this.cmsPanel.Name = "cmsPanel";
            this.cmsPanel.Size = new System.Drawing.Size(124, 22);
            this.cmsPanel.Text = "删除区域";
            this.cmsPanel.Click += new System.EventHandler(this.tsmiPanelDelete);
            // 
            // tsmiMarkAdd
            // 
            this.tsmiMarkAdd.Name = "tsmiMarkAdd";
            this.tsmiMarkAdd.Size = new System.Drawing.Size(124, 22);
            this.tsmiMarkAdd.Text = "新增标记";
            this.tsmiMarkAdd.Click += new System.EventHandler(this.tsmiMarkAdd_Click);
            // 
            // cmsMarkGrid
            // 
            this.cmsMarkGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMarkGridDelete});
            this.cmsMarkGrid.Name = "cmsMarkGrid";
            this.cmsMarkGrid.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiMarkGridDelete
            // 
            this.tsmiMarkGridDelete.Name = "tsmiMarkGridDelete";
            this.tsmiMarkGridDelete.Size = new System.Drawing.Size(124, 22);
            this.tsmiMarkGridDelete.Text = "删除标记";
            this.tsmiMarkGridDelete.Click += new System.EventHandler(this.tsmiMarkGridDelete_Click);
            // 
            // OperatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 548);
            this.Controls.Add(this.pnlStat);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.materialTabSelector1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OperatorForm";
            this.Text = "OperatorForm";
            this.Load += new System.EventHandler(this.OperatorForm_Load);
            this.Resize += new System.EventHandler(this.OperatorForm_Resize);
            this.cmsMarkMenu.ResumeLayout(false);
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImageView)).EndInit();
            this.pnlStat.ResumeLayout(false);
            this.pnlStat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMark)).EndInit();
            this.cmsMarkPanelMenu.ResumeLayout(false);
            this.cmsMarkGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.Panel pnlStat;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialRaisedButton btnNextSwath;
        private MaterialSkin.Controls.MaterialRaisedButton btnPrevSwath;
        private MaterialSkin.Controls.MaterialRaisedButton btnNextSeq;
        private MaterialSkin.Controls.MaterialRaisedButton btnPrevSeq;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.DataGridView dgvMark;
        private MaterialSkin.Controls.MaterialCheckBox chkMark;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtSlideId;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCurSwath;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCurSequence;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCurPath;
        private System.Windows.Forms.ContextMenuStrip cmsMarkMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiRect;
        private System.Windows.Forms.ToolStripMenuItem tsmiCircle;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.PictureBox pbImageView;
        private System.Windows.Forms.ToolStripMenuItem tsmiResize;
        private System.Windows.Forms.ToolStripMenuItem tsmi25;
        private System.Windows.Forms.ToolStripMenuItem tsmi50;
        private System.Windows.Forms.ToolStripMenuItem tsmi100;
        private System.Windows.Forms.ToolStripMenuItem tsmi150;
        private System.Windows.Forms.ToolStripMenuItem tsmi200;
        private System.Windows.Forms.ContextMenuStrip cmsMarkPanelMenu;
        private System.Windows.Forms.ToolStripMenuItem cmsPanel;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarkAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn catename;
        private System.Windows.Forms.DataGridViewTextBoxColumn typename;
        private System.Windows.Forms.DataGridViewTextBoxColumn userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn userlevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn slideid;
        private System.Windows.Forms.DataGridViewTextBoxColumn swathid;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldid;
        private System.Windows.Forms.DataGridViewTextBoxColumn imagex;
        private System.Windows.Forms.DataGridViewTextBoxColumn imagey;
        private System.Windows.Forms.DataGridViewTextBoxColumn stagex;
        private System.Windows.Forms.DataGridViewTextBoxColumn stagey;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn slidefrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn createtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn celltype;
        private System.Windows.Forms.ContextMenuStrip cmsMarkGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarkGridDelete;
    }
}