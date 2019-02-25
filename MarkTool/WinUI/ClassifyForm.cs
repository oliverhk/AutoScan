using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;
using DBCtrl.Model;
using DBCtrl.DAO;

namespace MarkTool.WinUI
{
    public partial class ClassifyForm : Form
    {
        #region instance
        //Singleton instance
        private static ClassifyForm _instance;
        public static ClassifyForm Instance => _instance ?? (_instance = new ClassifyForm());
        #endregion

        private CategoryDao cateDao;
        private TypesDao typesDao;

        private string operate;

        enum TreeNodeType
        {
            Root,
            Category,
            Type
        }

        struct TreeNodeTag
        {
            public int Id;
            public TreeNodeType NodeType;           
        }

        public ClassifyForm()
        {
            InitializeComponent();
        }

        private void ClassifyForm_Load(object sender, EventArgs e)
        {
            cateDao = new CategoryDao();
            typesDao = new TypesDao();

            btnSave.Visible = false;
            btnCancel.Visible = false;

            ChangeToEditMode(false);

            btnRefresh_Click(null, null);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            treeView.Nodes.Clear();
            cmbCateID.Items.Clear();

            //Add root node
            TreeNode root = new TreeNode("细胞分类");
            TreeNodeTag tag;

            tag = new TreeNodeTag();
            tag.Id = 0;
            tag.NodeType = TreeNodeType.Root;
            root.Tag = tag;
            treeView.Nodes.Add(root);
            treeView.SelectedNode = root;

            IList<Category> lstCate;
            IList<Types> lstTypes;
            
            //Category
            lstCate = cateDao.GetList();
            foreach(Category cate in lstCate)
            {
                TreeNode cateNode = new TreeNode(cate.CategoryName);
                tag = new TreeNodeTag();
                tag.Id = cate.CateId;
                tag.NodeType = TreeNodeType.Category;
                cateNode.Tag = tag;
                treeView.SelectedNode.Nodes.Add(cateNode);
                treeView.SelectedNode = cateNode;

                cmbCateID.Items.Add(cate.CateId);
                
                //Type
                {
                    lstTypes = typesDao.GetListByCategory(cate.CateId);
                    foreach(Types type in lstTypes)
                    {
                        TreeNode typeNode = new TreeNode(type.CellType);
                        tag = new TreeNodeTag();
                        tag.Id = type.TypeId;
                        tag.NodeType = TreeNodeType.Type;
                        typeNode.Tag = tag;
                        treeView.SelectedNode.Nodes.Add(typeNode);
                    }
                }

                treeView.SelectedNode = root;
            }

            treeView.ExpandAll();
        }

        private void treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNodeType nodeType = ((TreeNodeTag)e.Node.Tag).NodeType;
            switch (nodeType)
            {
                case TreeNodeType.Root:
                    GroupHide("category", true);
                    GroupHide("type", true);
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    break; ;
                case TreeNodeType.Category:
                    GroupHide("category", false);
                    GroupHide("type", true);
                    btnAdd.Visible = true;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                    break;
                case TreeNodeType.Type:
                    GroupHide("category", false);
                    GroupHide("type", false);
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                    break;
                default:
                    break;
            }

            GetInfo((TreeNodeTag)e.Node.Tag);
        }

        private void DisableAllEditBox()
        {
            cmbCateID.Enabled = false;
            txtCateName.Enabled = false;
            txtTypeId.Enabled = false;
            txtCellCode.Enabled = false;
            txtCellType.Enabled = false;
            txtDescription.Enabled = false;

            btnColor.Enabled = false;
        }

        private void GroupHide(string type, bool hide)
        {
            switch (type)
            {
                case "category":
                    if (hide)
                    {
                        groupBox1.Hide();
                        labelCateID.Hide();
                        labelCateName.Hide();
                        cmbCateID.Hide();
                        txtCateName.Hide();
                    }
                    else
                    {
                        groupBox1.Show();
                        labelCateID.Show();
                        labelCateName.Show();
                        cmbCateID.Show();
                        txtCateName.Show();
                    }
                    break;
                case "type":
                    if (hide)
                    {
                        groupBox2.Hide();
                        labelTypeID.Hide();
                        labelCellType.Hide();
                        labelCellCode.Hide();
                        labelDescription.Hide();
                        txtTypeId.Hide();
                        txtCellType.Hide();
                        txtCellCode.Hide();
                        txtDescription.Hide();
                    }
                    else
                    {
                        groupBox2.Show();
                        labelTypeID.Show();
                        labelCellType.Show();
                        labelCellCode.Show();
                        labelDescription.Show();
                        txtTypeId.Show();
                        txtCellType.Show();
                        txtCellCode.Show();
                        txtDescription.Show();
                    }
                    break;
                default:
                    break;
            }
        }

        private void GetInfo(TreeNodeTag tag)
        {
            Category cate;
            Types cellType;
            switch (tag.NodeType)
            {
                case TreeNodeType.Category:
                    cate = cateDao.GetCategory(tag.Id);
                    cmbCateID.Text = cate.CateId.ToString();
                    txtCateName.Text = cate.CategoryName;
                    break;
                case TreeNodeType.Type:
                    cellType = typesDao.GetTypeById(tag.Id);
                    cmbCateID.Text = cellType.CateId.ToString();
                    txtTypeId.Text = cellType.TypeId.ToString();
                    txtCellType.Text = cellType.CellType;
                    txtCellCode.Text = cellType.CellCode;
                    txtDescription.Text = cellType.Description;
                    cate = cateDao.GetCategory(cellType.CateId);
                    txtCateName.Text = cate.CategoryName;
                    if (cellType.ColorStr != null && cellType.ColorStr!= "NULL")
                    {
                        btnColor.BackColor = ColorTranslator.FromHtml(cellType.ColorStr);
                    }
                    else
                    {
                        btnColor.BackColor = Color.Black;
                    }
                    break;
                default:
                    break;
            }
        }

        private void ChangeToEditMode(bool editmode)
        {
            treeView.Enabled = !editmode;

            btnRefresh.Visible = !editmode;
            btnAdd.Visible = !editmode;
            btnUpdate.Visible = !editmode;
            btnDelete.Visible = !editmode;

            btnSave.Visible = editmode;
            btnCancel.Visible = editmode;

            if(!editmode)
            {
                DisableAllEditBox();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            operate = "add";
            if (treeView.SelectedNode == null)
            {
                return;
            }

            if (((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Root)
            {
                GroupHide("category",false);
                GroupHide("type", true);
                cmbCateID.Text = "";
                txtCateName.Text = "";

                cmbCateID.Enabled = false;
                txtCateName.Enabled = true;
                txtCellCode.Enabled = false;
                txtCellType.Enabled = false;
                txtDescription.Enabled = false;
            }
            else if(((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Category)
            {
                GroupHide("category", false);
                GroupHide("type", false);

                cmbCateID.Enabled = false;
                txtCateName.Enabled = false;
                txtCellCode.Enabled = true;
                txtCellType.Enabled = true;
                txtDescription.Enabled = true;
                btnColor.Enabled = true;
            }

            ChangeToEditMode(true);
            txtTypeId.Text = "";
            txtCellCode.Text = "";
            txtCellType.Text = "";
            txtDescription.Text = "";
            btnColor.BackColor = Color.Black;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            operate = "update";
            if (treeView.SelectedNode == null)
            {
                return;
            }      

            if (((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Type)
            {
                GroupHide("category", false);
                GroupHide("type", false);

                cmbCateID.Enabled = true;
                txtCateName.Enabled = false;
                txtCellCode.Enabled = true;
                txtCellType.Enabled = true;
                txtDescription.Enabled = true;
                btnColor.Enabled = true;
            }
            else if (((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Category)
            {
                GroupHide("category", false);
                GroupHide("type", true);

                cmbCateID.Enabled = false;
                txtCateName.Enabled = true;
                txtCellCode.Enabled = false;
                txtCellType.Enabled = false;
                txtDescription.Enabled = false;
            }

            ChangeToEditMode(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            operate = "delete";
            if (treeView.SelectedNode == null)
            {
                return;
            }

            if(((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Type)
            {
                typesDao.DeleteByCellType(treeView.SelectedNode.Text);
            }
            else if(((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Category)
            {
                if (Dialogs.Confirm("确认并删除所有子项?"))
                {
                    Category cate = cateDao.GetCategoryByName(treeView.SelectedNode.Text);
                    typesDao.DeleteByCateId(cate.CateId);
                    cateDao.Delete(cate.CateId);
                }
            }

            btnRefresh_Click(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Category cate = new Category();
            Types cellType = new Types();
            //合法性检查

            if (((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Root)
            {
                cate.CategoryName = txtCateName.Text.Trim();
                if (operate == "add")
                {
                    if(cateDao.Insert(cate) == false)
                    {
                        MessageBox.Show("保存失败");
                    }
                }
            }
            else if(((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Category)
            {
                if (operate == "add")
                {
                    cellType.CateId = Convert.ToInt32(cmbCateID.Text);
                    cellType.CellCode = txtCellCode.Text.Trim();
                    cellType.CellType = txtCellType.Text.Trim();
                    cellType.Description = txtDescription.Text.Trim();
                    if(typesDao.Insert(cellType) == false)
                    {
                        MessageBox.Show("保存失败");
                    }
                }
                else if (operate == "update")
                {
                    cate.CateId = Convert.ToInt32(cmbCateID.Text);
                    cate.CategoryName = txtCateName.Text;
                    if(cateDao.Update(cate) == false)
                    {
                        MessageBox.Show("更新失败");
                    }
                }
            }
            else if(((TreeNodeTag)treeView.SelectedNode.Tag).NodeType == TreeNodeType.Type)
            {
                if (operate == "update")
                {
                    cellType.CateId = Convert.ToInt32(cmbCateID.Text);
                    cellType.TypeId = Convert.ToInt32(txtTypeId.Text);
                    cellType.CellCode = txtCellCode.Text.Trim();
                    cellType.CellType = txtCellType.Text.Trim();
                    cellType.Description = txtDescription.Text.Trim();
                    cellType.ColorStr = ColorTranslator.ToHtml(btnColor.BackColor);
                    if (typesDao.Update(cellType) == false)
                    {
                        MessageBox.Show("更新失败");
                    }
                }
            }

            ChangeToEditMode(false);
            btnRefresh_Click(sender, e);
            MainForm.UpdateCategoryDictionary();
            MainForm.UpdateCellTypeDictionary();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ChangeToEditMode(false);
            btnRefresh_Click(sender, e);
        }

        private void treeview_Leave(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                treeView.SelectedNode.BackColor = Color.LightBlue;
            }
        }

        private void treeview_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if(treeView.SelectedNode != null)
            {
                treeView.SelectedNode.BackColor = Color.Empty;
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = colorDialog.Color;
            }
        }
    }
}
