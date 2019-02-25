using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportTool.WinUI
{
    public partial class LabelGapUC : UserControl
    {
        #region property
        private string _Title="lblTitle";
        public string Title
        {
            get
            {
                return _Title;
            }

            set
            {
                _Title = value;
                lblTitle.Text = _Title;
            }
        }
        #endregion
        public LabelGapUC()
        {
            InitializeComponent();
        }
    }
}
