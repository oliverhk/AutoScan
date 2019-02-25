using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp.WinUC
{
    public partial class GlobalFocusUC : UserControl
    {
        public GlobalFocusUC()
        {
            InitializeComponent();

            this.Load += GlobalFocusUC_Load;
        }

        private void GlobalFocusUC_Load(object sender, EventArgs e)
        {
            this.btnStatGrab.Focus();
        }

        private void btnStatGrab_Click(object sender, EventArgs e)
        {
            CheckInput(tbxStartX.Text);
            CheckInput(tbxEndX.Text);
            CheckInput(tbxGapX.Text);

            CheckInput(tbxStartY.Text);
            CheckInput(tbxEndY.Text);
            CheckInput(tbxGapY.Text);

            CheckInput(tbxStartZ.Text);
            CheckInput(tbxEndZ.Text);
            CheckInput(tbxGapZ.Text);

            //Thread grabThread = new Thread(autoFocus.DoFocus);
            //grabThread.Name = "Z grabe image";
            //grabThread.Start();
        }

        private bool CheckInput(string input)
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    float.Parse(input);
                    ret = true;
                }
                catch { }
            }
            if(ret==false)
            {
                MessageBox.Show("输入格式非数字！");
            }
            return ret;
        }

        public void SetImageOnForm(Bitmap img)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    if (picView.Image != null)
                    {
                        picView.Image.Dispose();
                    }
                    picView.Image = img.Clone() as Bitmap;
                }));
            }
            catch (Exception ex)
            {
            }
        }

        public void SetLogOnForm(string log)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    if(tbxLog.Text.Length>2000)
                    {
                        tbxLog.Text = string.Empty;
                    }
                    tbxLog.Text += log + "\r\n";
                }));
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
