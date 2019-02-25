using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
    public partial class DialogForm : MaterialSkin.Controls.MaterialForm
    {
        #region property
        private int MAX_TIME = 12;
        private int timer_over = 0;
        private string _message_Info;
        private bool _auto_Close = false;

        public string Message_Info
        {
            get { return _message_Info; }
            set { _message_Info = value; }
        }
        public bool Auto_Close
        {
            get { return _auto_Close; }
            set { _auto_Close = value; }
        }
        #endregion
        public DialogForm()
        {
            InitializeComponent();
        }

        private void DialogForm_Load(object sender, EventArgs e)
        {
            //initalize ui
            txtInfo.Text = _message_Info;
            timer1.Enabled = _auto_Close;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer 
            timer_over++;
            if (timer_over >= MAX_TIME)
            {
                this.Close();
            }
            else
            {
                btnOk.Text = "OK ("+(MAX_TIME-timer_over)+")";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogForm_Resize(object sender, EventArgs e)
        {
            if (this.Width != 432 || this.Height != 213)
            {
                this.Width = 432;
                this.Height = 213;
            }
        }
    }
}
