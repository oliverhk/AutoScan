using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin;

namespace CommonLibrary
{
    public partial class ConfirmForm : MaterialSkin.Controls.MaterialForm
    {
        #region property 
        private string _message_Info;

        public string Message_Info
        {
            get { return _message_Info; }
            set { _message_Info = value; }
        }
        
        #endregion
        public ConfirmForm()
        {
            InitializeComponent();
        }

        private void ConfirmForm_Load(object sender, EventArgs e)
        {
            //initalize
            txtInfo.Text = _message_Info;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmForm_Resize(object sender, EventArgs e)
        {
            if (this.Width != 434 || this.Height != 225)
            {
                this.Width = 434;
                this.Height = 225;
            }
        }
    }
}
