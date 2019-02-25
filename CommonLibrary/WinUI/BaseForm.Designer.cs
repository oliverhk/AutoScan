namespace CommonLibrary
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.pnlMsg = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picError = new System.Windows.Forms.PictureBox();
            this.picInfo = new System.Windows.Forms.PictureBox();
            this.sys_timer = new System.Windows.Forms.Timer(this.components);
            this.pnlMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMsg
            // 
            this.pnlMsg.BackColor = System.Drawing.Color.White;
            this.pnlMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMsg.Controls.Add(this.lblMsg);
            this.pnlMsg.Controls.Add(this.picClose);
            this.pnlMsg.Controls.Add(this.picError);
            this.pnlMsg.Controls.Add(this.picInfo);
            this.pnlMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMsg.Location = new System.Drawing.Point(0, 381);
            this.pnlMsg.Name = "pnlMsg";
            this.pnlMsg.Size = new System.Drawing.Size(583, 29);
            this.pnlMsg.TabIndex = 0;
            this.pnlMsg.Visible = false;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(34, 5);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(41, 14);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "label1";
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.Image = global::CommonLibrary.Properties.Resources.ClearGrid;
            this.picClose.Location = new System.Drawing.Point(548, -2);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(31, 31);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picClose.TabIndex = 2;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseHover += new System.EventHandler(this.picClose_MouseHover);
            this.picClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseUp);
            // 
            // picError
            // 
            this.picError.Image = global::CommonLibrary.Properties.Resources.MacroSecurity;
            this.picError.Location = new System.Drawing.Point(-2, -1);
            this.picError.Name = "picError";
            this.picError.Size = new System.Drawing.Size(31, 31);
            this.picError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picError.TabIndex = 1;
            this.picError.TabStop = false;
            // 
            // picInfo
            // 
            this.picInfo.Image = global::CommonLibrary.Properties.Resources.SendAgain;
            this.picInfo.Location = new System.Drawing.Point(-1, -2);
            this.picInfo.Name = "picInfo";
            this.picInfo.Size = new System.Drawing.Size(31, 31);
            this.picInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picInfo.TabIndex = 0;
            this.picInfo.TabStop = false;
            // 
            // sys_timer
            // 
            this.sys_timer.Interval = 4000;
            this.sys_timer.Tick += new System.EventHandler(this.sys_timer_Tick);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(583, 410);
            this.Controls.Add(this.pnlMsg);
            this.Font = new System.Drawing.Font("DengXian", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.pnlMsg.ResumeLayout(false);
            this.pnlMsg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMsg;
        private System.Windows.Forms.PictureBox picInfo;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.PictureBox picError;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Timer sys_timer;
    }
}