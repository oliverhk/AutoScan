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
using CommonLibrary;
using Utility;
using DBCtrl.Model;
namespace MainApp.WinUI
{
    public partial class RecipeSelectForm : MaterialForm
    {
        private IList<Recipes> lstRcp;
        public RecipeSelectForm()
        {
            InitializeComponent();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lstRcp == null || lstRcp.Count <= 0)
                return;
            int rcpid = lstRcp[e.RowIndex].RecipeId;
            Manager.IRcp.LoadRcp(rcpid);
            Manager.IRcp.SetDefault(rcpid);
            this.Close();
        }

        private void RecipeSelectForm_Load(object sender, EventArgs e)
        {
            try
            {
                dgvData.AutoGenerateColumns = false;
                lstRcp = Manager.IRcp.GetAllRcpList();
                dgvData.DataSource = lstRcp;
                dgvData.ClearSelection();
                //select cured                
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    if (lstRcp[i].IsDefault==1)
                    {
                        dgvData.Rows[i].Selected = true;
                        dgvData.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
    }
}
