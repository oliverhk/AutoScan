using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin.Controls;
namespace CommonLibrary
{
    public partial class WaitForm : Form
    {
        #region property
        private string _title;
        private bool _isShowButton = true;
        private bool _isClose;

        public bool IsClose
        {
            get { return _isClose; }
            set { _isClose = value; }
        }

        public bool IsShowButton
        {
            get { return _isShowButton; }
            set { _isShowButton = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        #endregion
        public WaitForm()
        {
            InitializeComponent();
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_title))
            {
                lblTitle.Text = "Please waiting ...";
            }
            else
            {
                lblTitle.Text = _title;
            }
            btnStop.Visible = IsShowButton;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_isClose)
            {
                this.Close();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
