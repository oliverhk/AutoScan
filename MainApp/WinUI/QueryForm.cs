using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp.WinUI
{
    public partial class QueryForm : Form
    {
        #region instance
        //Singleton instance
        private static QueryForm _instance;
        public static QueryForm Instance => _instance ?? (_instance = new QueryForm());
        #endregion
        public QueryForm()
        {
            InitializeComponent();
        }
    }
}
