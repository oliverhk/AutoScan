namespace MainApp.WinUI
{
    partial class RecipeSelectForm
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colversion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colisdefault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colcreateby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcratetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvData.ColumnHeadersHeight = 32;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.colversion,
            this.colisdefault,
            this.colcreateby,
            this.colcratetime});
            this.dgvData.Location = new System.Drawing.Point(1, 64);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 30;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(507, 464);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            // 
            // colId
            // 
            this.colId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colId.DataPropertyName = "RecipeId";
            this.colId.HeaderText = "配置编号";
            this.colId.Name = "colId";
            this.colId.Width = 88;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "名称";
            this.colName.Name = "colName";
            this.colName.Width = 60;
            // 
            // colversion
            // 
            this.colversion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colversion.DataPropertyName = "Version";
            this.colversion.HeaderText = "版本";
            this.colversion.Name = "colversion";
            this.colversion.Width = 60;
            // 
            // colisdefault
            // 
            this.colisdefault.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colisdefault.DataPropertyName = "IsDefault";
            this.colisdefault.HeaderText = "是否默认";
            this.colisdefault.Name = "colisdefault";
            this.colisdefault.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colisdefault.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colisdefault.Width = 88;
            // 
            // colcreateby
            // 
            this.colcreateby.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colcreateby.DataPropertyName = "CreateBy";
            this.colcreateby.HeaderText = "创建人";
            this.colcreateby.Name = "colcreateby";
            this.colcreateby.Width = 74;
            // 
            // colcratetime
            // 
            this.colcratetime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colcratetime.DataPropertyName = "CreateTime";
            this.colcratetime.HeaderText = "创建时间";
            this.colcratetime.Name = "colcratetime";
            this.colcratetime.Width = 88;
            // 
            // RecipeSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 529);
            this.Controls.Add(this.dgvData);
            this.Font = new System.Drawing.Font("SimSun", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecipeSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择配置参数";
            this.Load += new System.EventHandler(this.RecipeSelectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colversion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colisdefault;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcreateby;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcratetime;
    }
}