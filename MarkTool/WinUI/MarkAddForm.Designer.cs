namespace MarkTool.WinUI
{
    partial class MarkAddForm
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
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.btnMarkSave = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnMarkCancel = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtMarkId = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.cmbMarkCategory = new System.Windows.Forms.ComboBox();
            this.cmbMarkType = new System.Windows.Forms.ComboBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(54, 101);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(41, 19);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "序号";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(54, 144);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(41, 19);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "类别";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(54, 192);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(41, 19);
            this.materialLabel3.TabIndex = 2;
            this.materialLabel3.Text = "类型";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(54, 243);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(41, 19);
            this.materialLabel4.TabIndex = 3;
            this.materialLabel4.Text = "备注";
            // 
            // btnMarkSave
            // 
            this.btnMarkSave.AutoSize = true;
            this.btnMarkSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMarkSave.Depth = 0;
            this.btnMarkSave.Icon = global::MarkTool.Properties.Resources.Save32;
            this.btnMarkSave.Location = new System.Drawing.Point(88, 329);
            this.btnMarkSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMarkSave.Name = "btnMarkSave";
            this.btnMarkSave.Primary = true;
            this.btnMarkSave.Size = new System.Drawing.Size(79, 36);
            this.btnMarkSave.TabIndex = 4;
            this.btnMarkSave.Text = "保存";
            this.btnMarkSave.UseVisualStyleBackColor = true;
            this.btnMarkSave.Click += new System.EventHandler(this.btnMarkSave_Click);
            // 
            // btnMarkCancel
            // 
            this.btnMarkCancel.AutoSize = true;
            this.btnMarkCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMarkCancel.Depth = 0;
            this.btnMarkCancel.Icon = global::MarkTool.Properties.Resources.Delete;
            this.btnMarkCancel.Location = new System.Drawing.Point(218, 329);
            this.btnMarkCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMarkCancel.Name = "btnMarkCancel";
            this.btnMarkCancel.Primary = true;
            this.btnMarkCancel.Size = new System.Drawing.Size(79, 36);
            this.btnMarkCancel.TabIndex = 5;
            this.btnMarkCancel.Text = "取消";
            this.btnMarkCancel.UseVisualStyleBackColor = true;
            this.btnMarkCancel.Click += new System.EventHandler(this.btnMarkCancel_Click);
            // 
            // txtMarkId
            // 
            this.txtMarkId.Depth = 0;
            this.txtMarkId.Enabled = false;
            this.txtMarkId.Hint = "";
            this.txtMarkId.Location = new System.Drawing.Point(116, 96);
            this.txtMarkId.MaxLength = 32767;
            this.txtMarkId.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtMarkId.Name = "txtMarkId";
            this.txtMarkId.PasswordChar = '\0';
            this.txtMarkId.SelectedText = "";
            this.txtMarkId.SelectionLength = 0;
            this.txtMarkId.SelectionStart = 0;
            this.txtMarkId.Size = new System.Drawing.Size(155, 23);
            this.txtMarkId.TabIndex = 6;
            this.txtMarkId.TabStop = false;
            this.txtMarkId.UseSystemPasswordChar = false;
            // 
            // cmbMarkCategory
            // 
            this.cmbMarkCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarkCategory.FormattingEnabled = true;
            this.cmbMarkCategory.Location = new System.Drawing.Point(116, 142);
            this.cmbMarkCategory.Name = "cmbMarkCategory";
            this.cmbMarkCategory.Size = new System.Drawing.Size(155, 20);
            this.cmbMarkCategory.TabIndex = 7;
            this.cmbMarkCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCate_SelectChange);
            // 
            // cmbMarkType
            // 
            this.cmbMarkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarkType.FormattingEnabled = true;
            this.cmbMarkType.Location = new System.Drawing.Point(116, 190);
            this.cmbMarkType.Name = "cmbMarkType";
            this.cmbMarkType.Size = new System.Drawing.Size(155, 20);
            this.cmbMarkType.TabIndex = 8;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(116, 240);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(155, 69);
            this.txtRemark.TabIndex = 9;
            // 
            // MarkAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 406);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.cmbMarkType);
            this.Controls.Add(this.cmbMarkCategory);
            this.Controls.Add(this.txtMarkId);
            this.Controls.Add(this.btnMarkCancel);
            this.Controls.Add(this.btnMarkSave);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MarkAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MarkAddForm";
            this.Load += new System.EventHandler(this.MarkAddForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialRaisedButton btnMarkSave;
        private MaterialSkin.Controls.MaterialRaisedButton btnMarkCancel;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtMarkId;
        private System.Windows.Forms.ComboBox cmbMarkCategory;
        private System.Windows.Forms.ComboBox cmbMarkType;
        private System.Windows.Forms.TextBox txtRemark;
    }
}