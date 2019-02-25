namespace MainApp.WinUI
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
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.kpImgView = new ImageViewers.KpImageViewer();
            this.pnlStat = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            this.chkStartPos = new MaterialSkin.Controls.MaterialCheckBox();
            this.chkStopPos = new MaterialSkin.Controls.MaterialCheckBox();
            this.chkSwathStatPos = new MaterialSkin.Controls.MaterialCheckBox();
            this.chkSwathEndPos = new MaterialSkin.Controls.MaterialCheckBox();
            this.tpnStatCnt = new System.Windows.Forms.TableLayoutPanel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.lblSysHour = new MaterialSkin.Controls.MaterialLabel();
            this.lblSysCnt = new MaterialSkin.Controls.MaterialLabel();
            this.btnZero = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialLabel9 = new MaterialSkin.Controls.MaterialLabel();
            this.lblScanTime = new MaterialSkin.Controls.MaterialLabel();
            this.lblStat = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.lotid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coluseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblResult = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pnlbuton = new System.Windows.Forms.Panel();
            this.lstMessage = new MaterialSkin.Controls.MaterialListView();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.tpnlOper = new System.Windows.Forms.TableLayoutPanel();
            this.btnInit = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnSelect = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnScan = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnStop = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnAbortStop = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tpnlInput = new System.Windows.Forms.TableLayoutPanel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.txtPersonId = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtPersonName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tpnlMenu = new System.Windows.Forms.TableLayoutPanel();
            this.btnMenu = new MaterialSkin.Controls.MaterialRaisedButton();
            this.msg_timer = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.materialTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlImage.SuspendLayout();
            this.pnlStat.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tpnStatCnt.SuspendLayout();
            this.pnlResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.pnlbuton.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.tpnlOper.SuspendLayout();
            this.tpnlInput.SuspendLayout();
            this.tpnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 0);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(1184, 23);
            this.materialTabSelector1.TabIndex = 0;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 23);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1184, 839);
            this.materialTabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.pnlContent);
            this.tabPage2.Controls.Add(this.pnlMenu);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1176, 812);
            this.tabPage2.TabIndex = 1;
            // 
            // pnlContent
            // 
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.pnlImage);
            this.pnlContent.Controls.Add(this.pnlStat);
            this.pnlContent.Controls.Add(this.pnlbuton);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(165, 3);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1008, 806);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlImage
            // 
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImage.Controls.Add(this.kpImgView);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(604, 624);
            this.pnlImage.TabIndex = 2;
            // 
            // kpImgView
            // 
            this.kpImgView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.kpImgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpImgView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.kpImgView.GifAnimation = false;
            this.kpImgView.GifFPS = 15D;
            this.kpImgView.Image = null;
            this.kpImgView.Location = new System.Drawing.Point(0, 0);
            this.kpImgView.MenuColor = System.Drawing.Color.LightSteelBlue;
            this.kpImgView.MenuPanelColor = System.Drawing.Color.LightSteelBlue;
            this.kpImgView.MinimumSize = new System.Drawing.Size(350, 157);
            this.kpImgView.Name = "kpImgView";
            this.kpImgView.NavigationPanelColor = System.Drawing.Color.LightSteelBlue;
            this.kpImgView.NavigationTextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kpImgView.OpenButton = false;
            this.kpImgView.PreviewButton = false;
            this.kpImgView.PreviewPanelColor = System.Drawing.Color.LightSteelBlue;
            this.kpImgView.PreviewText = "Preview";
            this.kpImgView.PreviewTextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kpImgView.Rotation = 0;
            this.kpImgView.Scrollbars = false;
            this.kpImgView.ShowPreview = false;
            this.kpImgView.Size = new System.Drawing.Size(602, 622);
            this.kpImgView.TabIndex = 0;
            this.kpImgView.TextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kpImgView.Zoom = 100D;
            // 
            // pnlStat
            // 
            this.pnlStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStat.Controls.Add(this.tableLayoutPanel1);
            this.pnlStat.Controls.Add(this.tpnStatCnt);
            this.pnlStat.Controls.Add(this.lblStat);
            this.pnlStat.Controls.Add(this.pnlResult);
            this.pnlStat.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlStat.Location = new System.Drawing.Point(604, 0);
            this.pnlStat.Name = "pnlStat";
            this.pnlStat.Size = new System.Drawing.Size(402, 624);
            this.pnlStat.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.materialLabel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.materialLabel6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.materialLabel7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.materialLabel8, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkStartPos, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkStopPos, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkSwathStatPos, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkSwathEndPos, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 517);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 100);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(4, 1);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(92, 23);
            this.materialLabel5.TabIndex = 0;
            this.materialLabel5.Text = "开始信号";
            this.materialLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(202, 1);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(92, 23);
            this.materialLabel6.TabIndex = 1;
            this.materialLabel6.Text = "起始位置";
            this.materialLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel7.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.Location = new System.Drawing.Point(4, 25);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(92, 23);
            this.materialLabel7.TabIndex = 2;
            this.materialLabel7.Text = "结束信号";
            this.materialLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // materialLabel8
            // 
            this.materialLabel8.AutoSize = true;
            this.materialLabel8.Depth = 0;
            this.materialLabel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel8.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel8.Location = new System.Drawing.Point(202, 25);
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            this.materialLabel8.Size = new System.Drawing.Size(92, 23);
            this.materialLabel8.TabIndex = 3;
            this.materialLabel8.Text = "结束位置";
            this.materialLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkStartPos
            // 
            this.chkStartPos.AutoSize = true;
            this.chkStartPos.Depth = 0;
            this.chkStartPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkStartPos.Enabled = false;
            this.chkStartPos.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkStartPos.Location = new System.Drawing.Point(100, 1);
            this.chkStartPos.Margin = new System.Windows.Forms.Padding(0);
            this.chkStartPos.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkStartPos.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkStartPos.Name = "chkStartPos";
            this.chkStartPos.Ripple = true;
            this.chkStartPos.Size = new System.Drawing.Size(98, 23);
            this.chkStartPos.TabIndex = 4;
            this.chkStartPos.UseVisualStyleBackColor = true;
            // 
            // chkStopPos
            // 
            this.chkStopPos.AutoSize = true;
            this.chkStopPos.Depth = 0;
            this.chkStopPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkStopPos.Enabled = false;
            this.chkStopPos.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkStopPos.Location = new System.Drawing.Point(100, 25);
            this.chkStopPos.Margin = new System.Windows.Forms.Padding(0);
            this.chkStopPos.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkStopPos.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkStopPos.Name = "chkStopPos";
            this.chkStopPos.Ripple = true;
            this.chkStopPos.Size = new System.Drawing.Size(98, 23);
            this.chkStopPos.TabIndex = 5;
            this.chkStopPos.UseVisualStyleBackColor = true;
            // 
            // chkSwathStatPos
            // 
            this.chkSwathStatPos.AutoSize = true;
            this.chkSwathStatPos.Depth = 0;
            this.chkSwathStatPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSwathStatPos.Enabled = false;
            this.chkSwathStatPos.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkSwathStatPos.Location = new System.Drawing.Point(298, 1);
            this.chkSwathStatPos.Margin = new System.Windows.Forms.Padding(0);
            this.chkSwathStatPos.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkSwathStatPos.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkSwathStatPos.Name = "chkSwathStatPos";
            this.chkSwathStatPos.Ripple = true;
            this.chkSwathStatPos.Size = new System.Drawing.Size(101, 23);
            this.chkSwathStatPos.TabIndex = 6;
            this.chkSwathStatPos.UseVisualStyleBackColor = true;
            // 
            // chkSwathEndPos
            // 
            this.chkSwathEndPos.AutoSize = true;
            this.chkSwathEndPos.Depth = 0;
            this.chkSwathEndPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSwathEndPos.Enabled = false;
            this.chkSwathEndPos.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkSwathEndPos.Location = new System.Drawing.Point(298, 25);
            this.chkSwathEndPos.Margin = new System.Windows.Forms.Padding(0);
            this.chkSwathEndPos.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkSwathEndPos.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkSwathEndPos.Name = "chkSwathEndPos";
            this.chkSwathEndPos.Ripple = true;
            this.chkSwathEndPos.Size = new System.Drawing.Size(101, 23);
            this.chkSwathEndPos.TabIndex = 7;
            this.chkSwathEndPos.UseVisualStyleBackColor = true;
            // 
            // tpnStatCnt
            // 
            this.tpnStatCnt.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tpnStatCnt.ColumnCount = 2;
            this.tpnStatCnt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnStatCnt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnStatCnt.Controls.Add(this.materialLabel3, 0, 0);
            this.tpnStatCnt.Controls.Add(this.materialLabel4, 0, 1);
            this.tpnStatCnt.Controls.Add(this.lblSysHour, 1, 0);
            this.tpnStatCnt.Controls.Add(this.lblSysCnt, 1, 1);
            this.tpnStatCnt.Controls.Add(this.btnZero, 1, 3);
            this.tpnStatCnt.Controls.Add(this.materialLabel9, 0, 2);
            this.tpnStatCnt.Controls.Add(this.lblScanTime, 1, 2);
            this.tpnStatCnt.Dock = System.Windows.Forms.DockStyle.Top;
            this.tpnStatCnt.Location = new System.Drawing.Point(0, 390);
            this.tpnStatCnt.Name = "tpnStatCnt";
            this.tpnStatCnt.RowCount = 4;
            this.tpnStatCnt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpnStatCnt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpnStatCnt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpnStatCnt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpnStatCnt.Size = new System.Drawing.Size(400, 127);
            this.tpnStatCnt.TabIndex = 2;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(4, 1);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(192, 30);
            this.materialLabel3.TabIndex = 0;
            this.materialLabel3.Text = "运行时间(H)";
            this.materialLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(4, 32);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(192, 30);
            this.materialLabel4.TabIndex = 1;
            this.materialLabel4.Text = "扫描片数";
            this.materialLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSysHour
            // 
            this.lblSysHour.AutoSize = true;
            this.lblSysHour.Depth = 0;
            this.lblSysHour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSysHour.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblSysHour.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSysHour.Location = new System.Drawing.Point(203, 1);
            this.lblSysHour.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSysHour.Name = "lblSysHour";
            this.lblSysHour.Size = new System.Drawing.Size(193, 30);
            this.lblSysHour.TabIndex = 2;
            this.lblSysHour.Text = "0";
            this.lblSysHour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSysCnt
            // 
            this.lblSysCnt.AutoSize = true;
            this.lblSysCnt.Depth = 0;
            this.lblSysCnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSysCnt.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblSysCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSysCnt.Location = new System.Drawing.Point(203, 32);
            this.lblSysCnt.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSysCnt.Name = "lblSysCnt";
            this.lblSysCnt.Size = new System.Drawing.Size(193, 30);
            this.lblSysCnt.TabIndex = 3;
            this.lblSysCnt.Text = "0";
            this.lblSysCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnZero
            // 
            this.btnZero.AutoSize = true;
            this.btnZero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnZero.Depth = 0;
            this.btnZero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnZero.Icon = null;
            this.btnZero.Location = new System.Drawing.Point(204, 100);
            this.btnZero.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnZero.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnZero.Name = "btnZero";
            this.btnZero.Primary = false;
            this.btnZero.Size = new System.Drawing.Size(191, 20);
            this.btnZero.TabIndex = 4;
            this.btnZero.Text = "清零";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // materialLabel9
            // 
            this.materialLabel9.AutoSize = true;
            this.materialLabel9.Depth = 0;
            this.materialLabel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel9.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel9.Location = new System.Drawing.Point(4, 63);
            this.materialLabel9.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel9.Name = "materialLabel9";
            this.materialLabel9.Size = new System.Drawing.Size(192, 30);
            this.materialLabel9.TabIndex = 5;
            this.materialLabel9.Text = "扫描时间(秒)";
            this.materialLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScanTime
            // 
            this.lblScanTime.AutoSize = true;
            this.lblScanTime.Depth = 0;
            this.lblScanTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScanTime.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblScanTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblScanTime.Location = new System.Drawing.Point(203, 63);
            this.lblScanTime.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblScanTime.Name = "lblScanTime";
            this.lblScanTime.Size = new System.Drawing.Size(193, 30);
            this.lblScanTime.TabIndex = 6;
            this.lblScanTime.Text = "0";
            this.lblScanTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStat
            // 
            this.lblStat.AutoSize = true;
            this.lblStat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblStat.Depth = 0;
            this.lblStat.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStat.Icon = null;
            this.lblStat.Location = new System.Drawing.Point(0, 354);
            this.lblStat.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblStat.Name = "lblStat";
            this.lblStat.Primary = true;
            this.lblStat.Size = new System.Drawing.Size(400, 36);
            this.lblStat.TabIndex = 1;
            this.lblStat.Text = "状态";
            this.lblStat.UseVisualStyleBackColor = true;
            // 
            // pnlResult
            // 
            this.pnlResult.Controls.Add(this.dgvResult);
            this.pnlResult.Controls.Add(this.lblResult);
            this.pnlResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResult.Location = new System.Drawing.Point(0, 0);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(400, 354);
            this.pnlResult.TabIndex = 0;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.BackgroundColor = System.Drawing.Color.White;
            this.dgvResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResult.ColumnHeadersHeight = 32;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lotid,
            this.batchid,
            this.colWidth,
            this.colHeight,
            this.coluseTime});
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 36);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowHeadersWidth = 21;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(400, 318);
            this.dgvResult.TabIndex = 1;
            // 
            // lotid
            // 
            this.lotid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lotid.DataPropertyName = "LotId";
            this.lotid.HeaderText = "编号";
            this.lotid.Name = "lotid";
            this.lotid.Width = 60;
            // 
            // batchid
            // 
            this.batchid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.batchid.DataPropertyName = "BatchId";
            this.batchid.HeaderText = "批次";
            this.batchid.Name = "batchid";
            this.batchid.Width = 60;
            // 
            // colWidth
            // 
            this.colWidth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colWidth.DataPropertyName = "Starttime";
            this.colWidth.HeaderText = "开始时间";
            this.colWidth.Name = "colWidth";
            this.colWidth.Width = 88;
            // 
            // colHeight
            // 
            this.colHeight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colHeight.DataPropertyName = "Endtime";
            this.colHeight.HeaderText = "结束时间";
            this.colHeight.Name = "colHeight";
            this.colHeight.Width = 88;
            // 
            // coluseTime
            // 
            this.coluseTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.coluseTime.DataPropertyName = "Grade";
            this.coluseTime.HeaderText = "等级";
            this.coluseTime.Name = "coluseTime";
            this.coluseTime.Width = 60;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lblResult.Depth = 0;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResult.Icon = null;
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblResult.Name = "lblResult";
            this.lblResult.Primary = true;
            this.lblResult.Size = new System.Drawing.Size(400, 36);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "扫描结果";
            this.lblResult.UseVisualStyleBackColor = true;
            // 
            // pnlbuton
            // 
            this.pnlbuton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlbuton.Controls.Add(this.lstMessage);
            this.pnlbuton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlbuton.Location = new System.Drawing.Point(0, 624);
            this.pnlbuton.Name = "pnlbuton";
            this.pnlbuton.Size = new System.Drawing.Size(1006, 180);
            this.pnlbuton.TabIndex = 0;
            // 
            // lstMessage
            // 
            this.lstMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstMessage.Depth = 0;
            this.lstMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lstMessage.FullRowSelect = true;
            this.lstMessage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstMessage.Location = new System.Drawing.Point(0, 0);
            this.lstMessage.MouseLocation = new System.Drawing.Point(-1, -1);
            this.lstMessage.MouseState = MaterialSkin.MouseState.OUT;
            this.lstMessage.Name = "lstMessage";
            this.lstMessage.OwnerDraw = true;
            this.lstMessage.Size = new System.Drawing.Size(1004, 178);
            this.lstMessage.TabIndex = 0;
            this.lstMessage.UseCompatibleStateImageBehavior = false;
            this.lstMessage.View = System.Windows.Forms.View.Details;
            // 
            // pnlMenu
            // 
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.Controls.Add(this.tpnlOper);
            this.pnlMenu.Controls.Add(this.tpnlInput);
            this.pnlMenu.Controls.Add(this.tpnlMenu);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(3, 3);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(162, 806);
            this.pnlMenu.TabIndex = 0;
            // 
            // tpnlOper
            // 
            this.tpnlOper.ColumnCount = 1;
            this.tpnlOper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpnlOper.Controls.Add(this.btnInit, 0, 4);
            this.tpnlOper.Controls.Add(this.btnSelect, 0, 0);
            this.tpnlOper.Controls.Add(this.btnScan, 0, 1);
            this.tpnlOper.Controls.Add(this.btnStop, 0, 2);
            this.tpnlOper.Controls.Add(this.btnAbortStop, 0, 3);
            this.tpnlOper.Dock = System.Windows.Forms.DockStyle.Top;
            this.tpnlOper.Location = new System.Drawing.Point(0, 140);
            this.tpnlOper.Name = "tpnlOper";
            this.tpnlOper.RowCount = 5;
            this.tpnlOper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnlOper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnlOper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnlOper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnlOper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnlOper.Size = new System.Drawing.Size(160, 269);
            this.tpnlOper.TabIndex = 2;
            // 
            // btnInit
            // 
            this.btnInit.AutoSize = true;
            this.btnInit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInit.Depth = 0;
            this.btnInit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInit.Icon = null;
            this.btnInit.Location = new System.Drawing.Point(3, 215);
            this.btnInit.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnInit.Name = "btnInit";
            this.btnInit.Primary = true;
            this.btnInit.Size = new System.Drawing.Size(154, 51);
            this.btnInit.TabIndex = 4;
            this.btnInit.Text = "初始化系统";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.AutoSize = true;
            this.btnSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelect.Depth = 0;
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelect.Icon = null;
            this.btnSelect.Location = new System.Drawing.Point(3, 3);
            this.btnSelect.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Primary = true;
            this.btnSelect.Size = new System.Drawing.Size(154, 47);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "选择配置";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnScan
            // 
            this.btnScan.AutoSize = true;
            this.btnScan.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnScan.Depth = 0;
            this.btnScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScan.Icon = null;
            this.btnScan.Location = new System.Drawing.Point(3, 56);
            this.btnScan.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnScan.Name = "btnScan";
            this.btnScan.Primary = true;
            this.btnScan.Size = new System.Drawing.Size(154, 47);
            this.btnScan.TabIndex = 1;
            this.btnScan.Text = "开始扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnStop
            // 
            this.btnStop.AutoSize = true;
            this.btnStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStop.Depth = 0;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Icon = null;
            this.btnStop.Location = new System.Drawing.Point(3, 109);
            this.btnStop.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStop.Name = "btnStop";
            this.btnStop.Primary = true;
            this.btnStop.Size = new System.Drawing.Size(154, 47);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止扫描";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnAbortStop
            // 
            this.btnAbortStop.AutoSize = true;
            this.btnAbortStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAbortStop.Depth = 0;
            this.btnAbortStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAbortStop.Icon = null;
            this.btnAbortStop.Location = new System.Drawing.Point(3, 162);
            this.btnAbortStop.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAbortStop.Name = "btnAbortStop";
            this.btnAbortStop.Primary = true;
            this.btnAbortStop.Size = new System.Drawing.Size(154, 47);
            this.btnAbortStop.TabIndex = 3;
            this.btnAbortStop.Text = "紧急停止扫描";
            this.btnAbortStop.UseVisualStyleBackColor = true;
            this.btnAbortStop.Click += new System.EventHandler(this.btnAbortStop_Click);
            // 
            // tpnlInput
            // 
            this.tpnlInput.ColumnCount = 2;
            this.tpnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tpnlInput.Controls.Add(this.materialLabel1, 0, 0);
            this.tpnlInput.Controls.Add(this.materialLabel2, 0, 1);
            this.tpnlInput.Controls.Add(this.txtPersonId, 1, 0);
            this.tpnlInput.Controls.Add(this.txtPersonName, 1, 1);
            this.tpnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.tpnlInput.Location = new System.Drawing.Point(0, 40);
            this.tpnlInput.Name = "tpnlInput";
            this.tpnlInput.RowCount = 2;
            this.tpnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlInput.Size = new System.Drawing.Size(160, 100);
            this.tpnlInput.TabIndex = 1;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(3, 0);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(26, 50);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "编号";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(3, 50);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(26, 50);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "姓名";
            // 
            // txtPersonId
            // 
            this.txtPersonId.Depth = 0;
            this.txtPersonId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPersonId.Hint = "";
            this.txtPersonId.Location = new System.Drawing.Point(35, 3);
            this.txtPersonId.MaxLength = 32767;
            this.txtPersonId.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPersonId.Name = "txtPersonId";
            this.txtPersonId.PasswordChar = '\0';
            this.txtPersonId.SelectedText = "";
            this.txtPersonId.SelectionLength = 0;
            this.txtPersonId.SelectionStart = 0;
            this.txtPersonId.Size = new System.Drawing.Size(122, 23);
            this.txtPersonId.TabIndex = 2;
            this.txtPersonId.TabStop = false;
            this.txtPersonId.UseSystemPasswordChar = false;
            this.txtPersonId.DoubleClick += new System.EventHandler(this.txtPersonId_DoubleClick);
            // 
            // txtPersonName
            // 
            this.txtPersonName.Depth = 0;
            this.txtPersonName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPersonName.Hint = "";
            this.txtPersonName.Location = new System.Drawing.Point(35, 53);
            this.txtPersonName.MaxLength = 32767;
            this.txtPersonName.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPersonName.Name = "txtPersonName";
            this.txtPersonName.PasswordChar = '\0';
            this.txtPersonName.SelectedText = "";
            this.txtPersonName.SelectionLength = 0;
            this.txtPersonName.SelectionStart = 0;
            this.txtPersonName.Size = new System.Drawing.Size(122, 23);
            this.txtPersonName.TabIndex = 3;
            this.txtPersonName.TabStop = false;
            this.txtPersonName.UseSystemPasswordChar = false;
            // 
            // tpnlMenu
            // 
            this.tpnlMenu.ColumnCount = 1;
            this.tpnlMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpnlMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpnlMenu.Controls.Add(this.btnMenu, 0, 0);
            this.tpnlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.tpnlMenu.Location = new System.Drawing.Point(0, 0);
            this.tpnlMenu.Name = "tpnlMenu";
            this.tpnlMenu.RowCount = 1;
            this.tpnlMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpnlMenu.Size = new System.Drawing.Size(160, 40);
            this.tpnlMenu.TabIndex = 0;
            // 
            // btnMenu
            // 
            this.btnMenu.AutoSize = true;
            this.btnMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMenu.Depth = 0;
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMenu.Icon = null;
            this.btnMenu.Location = new System.Drawing.Point(3, 3);
            this.btnMenu.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Primary = true;
            this.btnMenu.Size = new System.Drawing.Size(154, 34);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "MENU";
            this.btnMenu.UseVisualStyleBackColor = true;
            // 
            // msg_timer
            // 
            this.msg_timer.Enabled = true;
            this.msg_timer.Interval = 500;
            this.msg_timer.Tick += new System.EventHandler(this.msg_timer_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // OperatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.materialTabSelector1);
            this.Font = new System.Drawing.Font("SimSun", 10F);
            this.Name = "OperatorForm";
            this.Text = "OperatorForm";
            this.Load += new System.EventHandler(this.OperatorForm_Load);
            this.Resize += new System.EventHandler(this.OperatorForm_Resize);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlImage.ResumeLayout(false);
            this.pnlStat.ResumeLayout(false);
            this.pnlStat.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tpnStatCnt.ResumeLayout(false);
            this.tpnStatCnt.PerformLayout();
            this.pnlResult.ResumeLayout(false);
            this.pnlResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.pnlbuton.ResumeLayout(false);
            this.pnlMenu.ResumeLayout(false);
            this.tpnlOper.ResumeLayout(false);
            this.tpnlOper.PerformLayout();
            this.tpnlInput.ResumeLayout(false);
            this.tpnlInput.PerformLayout();
            this.tpnlMenu.ResumeLayout(false);
            this.tpnlMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlbuton;
        private System.Windows.Forms.Panel pnlStat;
        private System.Windows.Forms.Panel pnlImage;
        private MaterialSkin.Controls.MaterialListView lstMessage;
        private System.Windows.Forms.TableLayoutPanel tpnlMenu;
        private MaterialSkin.Controls.MaterialRaisedButton btnMenu;
        private System.Windows.Forms.TableLayoutPanel tpnlInput;
        private System.Windows.Forms.TableLayoutPanel tpnlOper;
        private MaterialSkin.Controls.MaterialRaisedButton btnSelect;
        private MaterialSkin.Controls.MaterialRaisedButton btnScan;
        private MaterialSkin.Controls.MaterialRaisedButton btnStop;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPersonId;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPersonName;
        private ImageViewers.KpImageViewer kpImgView;
        private System.Windows.Forms.Timer msg_timer;
        private System.Windows.Forms.Panel pnlResult;
        private MaterialSkin.Controls.MaterialRaisedButton lblResult;
        private MaterialSkin.Controls.MaterialRaisedButton lblStat;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.TableLayoutPanel tpnStatCnt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel lblSysHour;
        private MaterialSkin.Controls.MaterialLabel lblSysCnt;
        private MaterialSkin.Controls.MaterialFlatButton btnZero;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
        private MaterialSkin.Controls.MaterialCheckBox chkStartPos;
        private MaterialSkin.Controls.MaterialCheckBox chkStopPos;
        private MaterialSkin.Controls.MaterialCheckBox chkSwathStatPos;
        private MaterialSkin.Controls.MaterialCheckBox chkSwathEndPos;
        private System.Windows.Forms.Timer timer1;
        private MaterialSkin.Controls.MaterialRaisedButton btnAbortStop;
        private MaterialSkin.Controls.MaterialRaisedButton btnInit;
        private MaterialSkin.Controls.MaterialLabel materialLabel9;
        private MaterialSkin.Controls.MaterialLabel lblScanTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn lotid;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn coluseTime;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}