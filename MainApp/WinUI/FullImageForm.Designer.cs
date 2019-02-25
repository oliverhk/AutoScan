namespace MainApp.WinUI
{
    partial class FullImageForm
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
            this.kpImgView = new ImageViewers.KpImageViewer();
            this.SuspendLayout();
            // 
            // kpImgView
            // 
            this.kpImgView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kpImgView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.kpImgView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.kpImgView.GifAnimation = false;
            this.kpImgView.GifFPS = 15D;
            this.kpImgView.Image = null;
            this.kpImgView.Location = new System.Drawing.Point(0, 24);
            this.kpImgView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.kpImgView.MenuColor = System.Drawing.Color.LightSteelBlue;
            this.kpImgView.MenuPanelColor = System.Drawing.Color.LightSteelBlue;
            this.kpImgView.MinimumSize = new System.Drawing.Size(467, 238);
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
            this.kpImgView.Size = new System.Drawing.Size(1033, 620);
            this.kpImgView.TabIndex = 1;
            this.kpImgView.TextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.kpImgView.Zoom = 100D;
            // 
            // FullImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 646);
            this.Controls.Add(this.kpImgView);
            this.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FullImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "睿仟医疗";
            this.Load += new System.EventHandler(this.FullImageForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageViewers.KpImageViewer kpImgView;
    }
}