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
using DBCtrl.DAO;

namespace MarkTool.WinUI
{
    public partial class MarkAddForm : MaterialForm
    {
        #region property
        public bool IsAdd { get; set; }
        public int MarkId { get; set; }
        public int TypeId { get; set; }
        public string Remark { get; set; }
        //public string UpUserId { get; set; }
        #endregion

        private CategoryDao cateDao;
        private TypesDao typesDao;
        IList<Category> lstCate;
        IList<Types> lstTypes;

        public MarkAddForm()
        {
            InitializeComponent();
        }

        private void MarkAddForm_Load(object sender, EventArgs e)
        {
            cateDao = new CategoryDao();
            typesDao = new TypesDao();

            LoadTypes();
            if (IsAdd)
            {
                cmbMarkCategory.Focus();
                this.Text = "新增标记";
            }
            else
            {
                UpdateMarkInfo();
                txtMarkId.Text = MarkId.ToString();
                this.Text = string.Format("修改标记");
            }
        }

        private void LoadTypes()
        {
            try
            {
                cmbMarkCategory.Items.Clear();
                cmbMarkType.Items.Clear();

                lstCate = cateDao.GetList();
                foreach (var v in lstCate)
                {
                    cmbMarkCategory.Items.Add(string.Format("{0}_{1}", v.CateId, v.CategoryName));
                }
                cmbMarkCategory.SelectedIndex = 0;
                cmbCate_SelectChange(null, null);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void UpdateMarkInfo()
        {
            MarkCellDao markDao = new MarkCellDao();

            MarkCell mark = markDao.GetMarkCellById(MarkId);
            Types type = typesDao.GetTypeById(mark.TypeId);

            foreach (var v in lstCate)
            {
                if (v.CateId == type.CateId)
                {                   
                    cmbMarkCategory.Text = string.Format("{0}_{1}", v.CateId, v.CategoryName);
                    break;
                }
            }
            cmbMarkType.Text = string.Format("{0}_{1}", type.TypeId, type.CellType);

            txtRemark.Text = Remark;
        }

        private void cmbCate_SelectChange(object sender, EventArgs e)
        {
            try
            {
                int CateId = DataUtility.ParseInt(
                    cmbMarkCategory.Text.Substring(0, cmbMarkCategory.Text.IndexOf('_')));

                cmbMarkType.Items.Clear();
                if (CateId > 0)
                {
                    lstTypes = typesDao.GetListByCategory(CateId);
                    foreach (var v in lstTypes)
                    {
                        cmbMarkType.Items.Add(string.Format("{0}_{1}", v.TypeId, v.CellType));
                    }
                    cmbMarkType.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnMarkCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnMarkSave_Click(object sender, EventArgs e)
        {
            TypeId = DataUtility.ParseInt(cmbMarkType.Text.Substring(0, cmbMarkType.Text.IndexOf('_')));
            Remark = txtRemark.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
