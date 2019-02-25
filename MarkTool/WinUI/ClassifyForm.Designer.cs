namespace MarkTool.WinUI
{
    partial class ClassifyForm
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
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.pnlTree = new System.Windows.Forms.Panel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTypeId = new System.Windows.Forms.TextBox();
            this.labelTypeID = new MaterialSkin.Controls.MaterialLabel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCellType = new System.Windows.Forms.TextBox();
            this.txtCellCode = new System.Windows.Forms.TextBox();
            this.labelDescription = new MaterialSkin.Controls.MaterialLabel();
            this.labelCellType = new MaterialSkin.Controls.MaterialLabel();
            this.labelCellCode = new MaterialSkin.Controls.MaterialLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbCateID = new System.Windows.Forms.ComboBox();
            this.txtCateName = new System.Windows.Forms.TextBox();
            this.labelCateName = new MaterialSkin.Controls.MaterialLabel();
            this.labelCateID = new MaterialSkin.Controls.MaterialLabel();
            this.btnRefresh = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnAdd = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnUpdate = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnDelete = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnSave = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnCancel = new MaterialSkin.Controls.MaterialRaisedButton();
            this.grpsubMenu3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.pnlTree.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpsubMenu3.SuspendLayout();
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
            this.materialTabSelector1.Size = new System.Drawing.Size(965, 23);
            this.materialTabSelector1.TabIndex = 0;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // pnlTree
            // 
            this.pnlTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTree.Controls.Add(this.treeView);
            this.pnlTree.Location = new System.Drawing.Point(12, 29);
            this.pnlTree.Name = "pnlTree";
            this.pnlTree.Size = new System.Drawing.Size(286, 498);
            this.pnlTree.TabIndex = 2;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(284, 496);
            this.treeView.TabIndex = 0;
            this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeview_BeforeSelect);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeview_AfterSelect);
            this.treeView.Leave += new System.EventHandler(this.treeview_Leave);
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.groupBox2);
            this.pnlContent.Controls.Add(this.groupBox1);
            this.pnlContent.Location = new System.Drawing.Point(305, 30);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(474, 497);
            this.pnlContent.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnColor);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtTypeId);
            this.groupBox2.Controls.Add(this.labelTypeID);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.txtCellType);
            this.groupBox2.Controls.Add(this.txtCellCode);
            this.groupBox2.Controls.Add(this.labelDescription);
            this.groupBox2.Controls.Add(this.labelCellType);
            this.groupBox2.Controls.Add(this.labelCellCode);
            this.groupBox2.Location = new System.Drawing.Point(36, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(393, 358);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtTypeId
            // 
            this.txtTypeId.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTypeId.Location = new System.Drawing.Point(162, 31);
            this.txtTypeId.Name = "txtTypeId";
            this.txtTypeId.ReadOnly = true;
            this.txtTypeId.Size = new System.Drawing.Size(142, 26);
            this.txtTypeId.TabIndex = 7;
            // 
            // labelTypeID
            // 
            this.labelTypeID.AutoSize = true;
            this.labelTypeID.Depth = 0;
            this.labelTypeID.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelTypeID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelTypeID.Location = new System.Drawing.Point(43, 31);
            this.labelTypeID.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelTypeID.Name = "labelTypeID";
            this.labelTypeID.Size = new System.Drawing.Size(69, 19);
            this.labelTypeID.TabIndex = 6;
            this.labelTypeID.Text = "类型编号";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(162, 197);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(212, 103);
            this.txtDescription.TabIndex = 5;
            // 
            // txtCellType
            // 
            this.txtCellType.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCellType.Location = new System.Drawing.Point(162, 118);
            this.txtCellType.Name = "txtCellType";
            this.txtCellType.Size = new System.Drawing.Size(142, 26);
            this.txtCellType.TabIndex = 4;
            // 
            // txtCellCode
            // 
            this.txtCellCode.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCellCode.Location = new System.Drawing.Point(162, 75);
            this.txtCellCode.Name = "txtCellCode";
            this.txtCellCode.Size = new System.Drawing.Size(142, 26);
            this.txtCellCode.TabIndex = 3;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Depth = 0;
            this.labelDescription.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelDescription.Location = new System.Drawing.Point(71, 200);
            this.labelDescription.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(41, 19);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "备注";
            // 
            // labelCellType
            // 
            this.labelCellType.AutoSize = true;
            this.labelCellType.Depth = 0;
            this.labelCellType.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelCellType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCellType.Location = new System.Drawing.Point(43, 121);
            this.labelCellType.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelCellType.Name = "labelCellType";
            this.labelCellType.Size = new System.Drawing.Size(73, 19);
            this.labelCellType.TabIndex = 1;
            this.labelCellType.Text = "细胞类型";
            // 
            // labelCellCode
            // 
            this.labelCellCode.AutoSize = true;
            this.labelCellCode.Depth = 0;
            this.labelCellCode.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelCellCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCellCode.Location = new System.Drawing.Point(43, 79);
            this.labelCellCode.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelCellCode.Name = "labelCellCode";
            this.labelCellCode.Size = new System.Drawing.Size(73, 19);
            this.labelCellCode.TabIndex = 0;
            this.labelCellCode.Text = "细胞编码";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbCateID);
            this.groupBox1.Controls.Add(this.txtCateName);
            this.groupBox1.Controls.Add(this.labelCateName);
            this.groupBox1.Controls.Add(this.labelCateID);
            this.groupBox1.Location = new System.Drawing.Point(36, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbCateID
            // 
            this.cmbCateID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCateID.Font = new System.Drawing.Font("SimSun", 12F);
            this.cmbCateID.FormattingEnabled = true;
            this.cmbCateID.Location = new System.Drawing.Point(162, 37);
            this.cmbCateID.Name = "cmbCateID";
            this.cmbCateID.Size = new System.Drawing.Size(142, 24);
            this.cmbCateID.TabIndex = 4;
            // 
            // txtCateName
            // 
            this.txtCateName.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCateName.Location = new System.Drawing.Point(162, 77);
            this.txtCateName.Name = "txtCateName";
            this.txtCateName.Size = new System.Drawing.Size(142, 26);
            this.txtCateName.TabIndex = 3;
            // 
            // labelCateName
            // 
            this.labelCateName.AutoSize = true;
            this.labelCateName.Depth = 0;
            this.labelCateName.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelCateName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCateName.Location = new System.Drawing.Point(39, 80);
            this.labelCateName.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelCateName.Name = "labelCateName";
            this.labelCateName.Size = new System.Drawing.Size(73, 19);
            this.labelCateName.TabIndex = 1;
            this.labelCateName.Text = "种类名称";
            // 
            // labelCateID
            // 
            this.labelCateID.AutoSize = true;
            this.labelCateID.Depth = 0;
            this.labelCateID.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelCateID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelCateID.Location = new System.Drawing.Point(39, 39);
            this.labelCateID.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelCateID.Name = "labelCateID";
            this.labelCateID.Size = new System.Drawing.Size(73, 19);
            this.labelCateID.TabIndex = 0;
            this.labelCateID.Text = "种类编号";
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.Depth = 0;
            this.btnRefresh.Icon = null;
            this.btnRefresh.Location = new System.Drawing.Point(44, 20);
            this.btnRefresh.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Primary = true;
            this.btnRefresh.Size = new System.Drawing.Size(65, 36);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "刷    新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdd.Depth = 0;
            this.btnAdd.Icon = null;
            this.btnAdd.Location = new System.Drawing.Point(44, 78);
            this.btnAdd.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Primary = true;
            this.btnAdd.Size = new System.Drawing.Size(65, 36);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "新    增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUpdate.Depth = 0;
            this.btnUpdate.Icon = null;
            this.btnUpdate.Location = new System.Drawing.Point(44, 136);
            this.btnUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Primary = true;
            this.btnUpdate.Size = new System.Drawing.Size(65, 36);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "修    改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDelete.Depth = 0;
            this.btnDelete.Icon = null;
            this.btnDelete.Location = new System.Drawing.Point(44, 198);
            this.btnDelete.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Primary = true;
            this.btnDelete.Size = new System.Drawing.Size(65, 36);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删    除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Depth = 0;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(44, 338);
            this.btnSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.Primary = true;
            this.btnSave.Size = new System.Drawing.Size(65, 36);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保    存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Depth = 0;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(44, 402);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primary = true;
            this.btnCancel.Size = new System.Drawing.Size(65, 36);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取    消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpsubMenu3
            // 
            this.grpsubMenu3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpsubMenu3.Controls.Add(this.btnCancel);
            this.grpsubMenu3.Controls.Add(this.btnSave);
            this.grpsubMenu3.Controls.Add(this.btnDelete);
            this.grpsubMenu3.Controls.Add(this.btnUpdate);
            this.grpsubMenu3.Controls.Add(this.btnAdd);
            this.grpsubMenu3.Controls.Add(this.btnRefresh);
            this.grpsubMenu3.Location = new System.Drawing.Point(801, 29);
            this.grpsubMenu3.Name = "grpsubMenu3";
            this.grpsubMenu3.Size = new System.Drawing.Size(152, 498);
            this.grpsubMenu3.TabIndex = 1;
            this.grpsubMenu3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(47, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "标记颜色";
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(162, 160);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(61, 23);
            this.btnColor.TabIndex = 9;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // ClassifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 539);
            this.Controls.Add(this.grpsubMenu3);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTree);
            this.Controls.Add(this.materialTabSelector1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClassifyForm";
            this.Text = "分类设置";
            this.Load += new System.EventHandler(this.ClassifyForm_Load);
            this.pnlTree.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpsubMenu3.ResumeLayout(false);
            this.grpsubMenu3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.Panel pnlTree;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private MaterialSkin.Controls.MaterialLabel labelCateName;
        private MaterialSkin.Controls.MaterialLabel labelCateID;
        private System.Windows.Forms.TextBox txtCateName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtCellType;
        private System.Windows.Forms.TextBox txtCellCode;
        private MaterialSkin.Controls.MaterialLabel labelDescription;
        private MaterialSkin.Controls.MaterialLabel labelCellType;
        private MaterialSkin.Controls.MaterialLabel labelCellCode;
        private System.Windows.Forms.TextBox txtTypeId;
        private MaterialSkin.Controls.MaterialLabel labelTypeID;
        private MaterialSkin.Controls.MaterialRaisedButton btnRefresh;
        private MaterialSkin.Controls.MaterialRaisedButton btnAdd;
        private MaterialSkin.Controls.MaterialRaisedButton btnUpdate;
        private MaterialSkin.Controls.MaterialRaisedButton btnDelete;
        private MaterialSkin.Controls.MaterialRaisedButton btnSave;
        private MaterialSkin.Controls.MaterialRaisedButton btnCancel;
        private System.Windows.Forms.GroupBox grpsubMenu3;
        private System.Windows.Forms.ComboBox cmbCateID;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label label1;
    }
}