using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace MarkTool.WinUI
{
    public partial class WaitForm : Form
    {
        #region instance
        //Singleton instance
        private static WaitForm _instance;
        public static WaitForm Instance => _instance ?? (_instance = new WaitForm());
        #endregion
        private WaitForm()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int pos)
        {
            labelProgress.Text = pos.ToString() + "%";
            progressBar.Value = Convert.ToInt32(pos);
        }
    }
}
