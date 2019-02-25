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
    public partial class CommonForm : BaseForm
    {
        private string _title;
        public string Title
        {
            get { return lblTitle.Text; }
            set { _title = value;
                  lblTitle.Text = _title;
                }
        }

        public CommonForm()
        {
            InitializeComponent();            
        }
    }
}
