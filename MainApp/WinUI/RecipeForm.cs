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
using Utility;
using DBCtrl.Model;
namespace MainApp.WinUI
{
    public partial class RecipeForm : Form
    {
        #region property
        private IList<Recipes> lstRcp;       //load all recipe
        private bool isChange = false;
        private int MAX_IMAGE_WIDTH = 2048;
        private int MAX_IMAGE_HEIGHT = 1536;
        //private Recipes curRcp;
        //private CameraRecipe curCamRcp;
        //private ScanRecipe curScanRcp;
        #endregion
        #region instance
        //Singleton instance
        private static RecipeForm _instance;
        public static RecipeForm Instance => _instance ?? (_instance = new RecipeForm());
        #endregion
        public RecipeForm()
        {
            InitializeComponent();            
        }

        private void RecipeForm_Load(object sender, EventArgs e)
        {
            Init();
        }
        #region method
        private void Init()
        {
            try
            {
                //init camer width
                LoadAllRcp();
                //set evt 
                foreach (Control ctrl in spContent.Panel2.Controls)
                {
                    if (ctrl is TextBox||ctrl is NumericUpDown)
                    {
                        ctrl.TextChanged += Ctrl_TextChanged;
                    }
                }                
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void Ctrl_TextChanged(object sender, EventArgs e)
        {
            isChange = true;
        }
        private void LoadAllRcp()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.dgvRcp.AutoGenerateColumns = false;
                lstRcp = Manager.IRcp.GetAllRcpList();
                dgvRcp.DataSource = lstRcp;                
                int nRcpId = !string.IsNullOrEmpty(txtRcpId.Text)?int.Parse(txtRcpId.Text): Manager.IRcp.RecipeId;
                                                
                dgvRcp.ClearSelection();                
                //select cured                
                for (int i = 0; i < dgvRcp.Rows.Count; i++)
                {
                    if (lstRcp[i].RecipeId == nRcpId)
                    {                        
                        dgvRcp.Rows[i].Selected = true;
                        dgvRcp.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
                Dsiplay(nRcpId);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void NewRecipe()
        {
            try
            {
                DBCtrl.DAO.RecipesDao dao = new DBCtrl.DAO.RecipesDao();
                int maxid = dao.MaxRecipeId();
                txtRcpId.Text = maxid.ToString();
                ndpVersion.Value = 1;
                txtRcpName.Text = "";
                CameraRecipe camrcp = new CameraRecipe();
                DisplayCamera(camrcp);
                ScanRecipe scanrcp = new ScanRecipe();
                DisplayScan(scanrcp);
                ControlRecipe ctrlrcp = new ControlRecipe();
                DisplayControl(ctrlrcp);

            }
            catch(Exception ex)
            {
                Dialogs.Show(ex);
            }
        }
        private void DisplayCamera(CameraRecipe rcp)
        {
            if (rcp == null)
                return;
            npdStartX.Value = rcp.OffsetX;
            npdStartY.Value = rcp.OffsetY;
            npdImgWidth.Value = rcp.Width;
            npdImgHeight.Value = rcp.Height;
            npdExposureTime.Value =(decimal)rcp.ExposureTime;
            npdGain.Value = (decimal)rcp.Gain;
            if(rcp.IsSaveImage==1)
            {
                chkIssaveImg.Checked = true;
                chkIssaveImg.Text = "保存图像";
                btnSelPath.Enabled = true;
            }
            else
            {
                chkIssaveImg.Checked = false;
                chkIssaveImg.Text = "不保存图像";
                btnSelPath.Enabled = false;
            }
            txtImgPath.Text = rcp.ImagePath;
            txtCreateTime.Text = rcp.CreateTime.ToString();
            txtLastUpdate.Text = rcp.LastUpdate.ToString();
            txtCreateBy.Text = rcp.CreateBy;
                        
        }
        private void DisplayScan(ScanRecipe rcp)
        {
            if (rcp == null)
                return;
            txtZeroX.Text = rcp.ZeroX.ToString();
            txtZeroY.Text = rcp.ZeroY.ToString();
            txtSpeedX.Text = rcp.SpeedX.ToString();
            txtSpeedY.Text = rcp.SpeedY.ToString();
            txtSwathRows.Text = rcp.SwathRows.ToString();
            txtSwathColumn.Text = rcp.SwathColumns.ToString();
            txtSwathWidth.Text = rcp.SwathWidth.ToString();
            txtSwathHeight.Text = rcp.SwathHeight.ToString();
            txtZStatPos.Text = rcp.ScanZPostion.ToString();
        }
        private void DisplayControl(ControlRecipe rcp)
        {
            if (rcp == null)
                return;
            txtCamTrgDelayT.Text = rcp.CamDelayTime.ToString();
            txtCamTrgPlusT.Text = rcp.CamPlusTime.ToString();
            txtLightTrgDelayT.Text = rcp.LightDelayTime.ToString();
            txtLightTrgPlusT.Text = rcp.LightPlusTime.ToString();
        }
        private void Dsiplay(int rcpid)
        {
            try
            {
                Manager.IRcp.LoadRcp(rcpid);
                Recipes rcp = Manager.IRcp.GetRcp();
                CameraRecipe camrcp = Manager.IRcp.GetCameraRcp();
                ScanRecipe scanrcp = Manager.IRcp.GetScanRcp();
                ControlRecipe ctrlrcp = Manager.IRcp.GetControlRcp();
                if (rcp != null)
                {
                    txtRcpId.Text = rcp.RecipeId.ToString();
                    txtRcpName.Text = rcp.Name;
                    ndpVersion.Value = rcp.Version;
                    DisplayCamera(camrcp);
                    DisplayScan(scanrcp);
                    DisplayControl(ctrlrcp);
                    isChange = false;
                }
            }
            catch(Exception ex)
            {
                Dialogs.Show(ex);
            }
        }
        private Recipes Form2Para()
        {
            Recipes rcp = new Recipes();
            rcp.RecipeId = DataUtility.ParseInt(txtRcpId.Text);
            rcp.Version = (int)ndpVersion.Value;
            rcp.Name = txtRcpName.Text;
            rcp.CreateTime = DateTime.Now;
            rcp.LastUpdate = DateTime.Now;
            return rcp;
        }
        private CameraRecipe Form2CamPara()
        {
            CameraRecipe rcp = new CameraRecipe();
            rcp.RecipeId = DataUtility.ParseInt(txtRcpId.Text);
            rcp.OffsetX = (int)npdStartX.Value;
            rcp.OffsetY = (int)npdStartY.Value;
            rcp.Width= (int)npdImgWidth.Value;
            rcp.Height =(int)npdImgHeight.Value;
            rcp.ExposureTime = (float)npdExposureTime.Value;
            rcp.Gain = (float)npdGain.Value;
            rcp.IsSaveImage = chkIssaveImg.Checked ? 1 : 0;            
            rcp.ImagePath = txtImgPath.Text;
            rcp.CameraType = "";
            rcp.CreateTime = DateTime.Now;
            rcp.LastUpdate = DateTime.Now;
            rcp.CreateBy = "";
            return rcp;
        }
        private ScanRecipe Form2ScanPara()
        {
            ScanRecipe rcp = new ScanRecipe();
            rcp.RecipeId = DataUtility.ParseInt(txtRcpId.Text);
            rcp.ZeroX = DataUtility.ParseFloat(txtZeroX.Text);
            rcp.ZeroY =DataUtility.ParseFloat(txtZeroY.Text);
            rcp.SpeedX = DataUtility.ParseFloat(txtSpeedX.Text);
            rcp.SpeedY =  DataUtility.ParseFloat(txtSpeedY.Text);
            rcp.SwathRows = DataUtility.ParseInt(txtSwathRows.Text);
            rcp.SwathColumns = DataUtility.ParseInt(txtSwathColumn.Text);
            rcp.SwathWidth = DataUtility.ParseFloat(txtSwathWidth.Text);
            rcp.SwathHeight = DataUtility.ParseFloat(txtSwathHeight.Text);
            rcp.ScanZPostion = DataUtility.ParseFloat(txtZStatPos.Text);
            return rcp;
        }
        private ControlRecipe Form2ControlPara()
        {
            ControlRecipe rcp = new ControlRecipe();
            rcp.RecipeId = DataUtility.ParseInt(txtRcpId.Text);
            rcp.CamDelayTime = DataUtility.ParseFloat(txtCamTrgDelayT.Text);
            rcp.CamPlusTime = DataUtility.ParseFloat(txtCamTrgPlusT.Text);
            rcp.LightDelayTime = DataUtility.ParseFloat(txtLightTrgDelayT.Text);
            rcp.LightPlusTime = DataUtility.ParseFloat(txtLightTrgPlusT.Text);            
            return rcp;
        }
        private bool IsValid()
        {
            bool rect = false;
            try
            {
                if(string.IsNullOrEmpty(txtRcpName.Text))
                {
                    Dialogs.Show("请输入参数名称!");
                    txtRcpName.Focus();
                    return rect;
                }
                if(npdImgWidth.Value <= 0)
                {
                    Dialogs.Show("图像宽度不正确!");
                    npdImgWidth.Focus();
                    return rect;
                }
                if(npdImgHeight.Value<=0)
                {
                    Dialogs.Show("图像高度不正确!");
                    npdImgHeight.Focus();
                    return rect;
                }
                if(chkIssaveImg.Checked && string.IsNullOrEmpty(txtImgPath.Text))
                {
                    Dialogs.Show("图像保存路径不能为空!");                    
                    return rect;
                }
                if(string.IsNullOrEmpty(txtZeroX.Text))
                {
                    Dialogs.Show("请输入扫描X起始点!");
                    txtZeroX.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidNumber(txtZeroX.Text))
                {
                    Dialogs.Show("扫描X起始点不正确!");
                    txtZeroX.Focus();
                    return rect;
                }
                if (string.IsNullOrEmpty(txtZeroY.Text))
                {
                    Dialogs.Show("请输入扫描Y起始点!");
                    txtZeroY.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtZeroY.Text))
                {
                    Dialogs.Show("扫描Y起始点不正确!");
                    txtZeroY.Focus();
                    return rect;
                }
                if (string.IsNullOrEmpty(txtSpeedX.Text))
                {
                    Dialogs.Show("请输入X轴运行速度!");
                    txtSpeedX.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidNumber(txtSpeedX.Text))
                {
                    Dialogs.Show("X轴运行速度不正确!");
                    txtSpeedX.Focus();
                    return rect;
                }
                if (string.IsNullOrEmpty(txtSpeedY.Text))
                {
                    Dialogs.Show("请输入Y轴运行速度!");
                    txtSpeedY.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtSpeedY.Text))
                {
                    Dialogs.Show("Y轴运行速度不正确!");
                    txtSpeedY.Focus();
                    return rect;
                }
                if (string.IsNullOrEmpty(txtSwathRows.Text))
                {
                    Dialogs.Show("请输入扫描行数!");
                    txtSwathRows.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtSpeedY.Text))
                {
                    Dialogs.Show("扫描行数不正确!");
                    txtSwathRows.Focus();
                    return rect;
                }

                if (string.IsNullOrEmpty(txtSwathColumn.Text))
                {
                    Dialogs.Show("请输入扫描列数!");
                    txtSwathColumn.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtSwathColumn.Text))
                {
                    Dialogs.Show("扫描列数不正确!");
                    txtSwathColumn.Focus();
                    return rect;
                }

                if (string.IsNullOrEmpty(txtSwathWidth.Text))
                {
                    Dialogs.Show("请输入扫描行宽度!");
                    txtSwathWidth.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtSwathWidth.Text))
                {
                    Dialogs.Show("扫描行宽度不正确!");
                    txtSwathWidth.Focus();
                    return rect;
                }

                if (string.IsNullOrEmpty(txtSwathHeight.Text))
                {
                    Dialogs.Show("请输入扫描行高度!");
                    txtSwathHeight.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtSwathHeight.Text))
                {
                    Dialogs.Show("扫描行高度不正确!");
                    txtSwathHeight.Focus();
                    return rect;
                }
                //--- control prara
                if (string.IsNullOrEmpty(txtZStatPos.Text))
                {
                    Dialogs.Show("起始扫描Z轴位置不能为空!");
                    txtZStatPos.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtZStatPos.Text))
                {
                    Dialogs.Show("起始扫描Z轴位置数据不正确!");
                    txtZStatPos.Focus();
                    return rect;
                }
                //---
                if (string.IsNullOrEmpty(txtCamTrgDelayT.Text))
                {
                    Dialogs.Show("相机触发延时不能为空!");
                    txtCamTrgDelayT.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtCamTrgDelayT.Text))
                {
                    Dialogs.Show("相机触发延时格式不正确!");
                    txtCamTrgDelayT.Focus();
                    return rect;
                }

                if (string.IsNullOrEmpty(txtCamTrgPlusT.Text))
                {
                    Dialogs.Show("相机触发脉宽不能为空!");
                    txtCamTrgPlusT.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtCamTrgPlusT.Text))
                {
                    Dialogs.Show("相机触发脉宽格式不正确!");
                    txtCamTrgPlusT.Focus();
                    return rect;
                }

                if (string.IsNullOrEmpty(txtLightTrgDelayT.Text))
                {
                    Dialogs.Show("光源触发延时不能为空!");
                    txtLightTrgDelayT.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtLightTrgDelayT.Text))
                {
                    Dialogs.Show("光源触发延时格式不正确!");
                    txtLightTrgDelayT.Focus();
                    return rect;
                }

                if (string.IsNullOrEmpty(txtLightTrgPlusT.Text))
                {
                    Dialogs.Show("光源触发脉宽不能为空!");
                    txtLightTrgPlusT.Focus();
                    return rect;
                }
                if (!DataUtility.IsValidDecimal(txtLightTrgPlusT.Text))
                {
                    Dialogs.Show("光源触发脉宽格式不正确!");
                    txtLightTrgPlusT.Focus();
                    return rect;
                }
                //------
                rect = true;

            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool Save()
        {
            bool rect = false;
            try
            {                
                Recipes rcp = new Recipes();
                CameraRecipe camrcp;
                ScanRecipe scanrcp;
                ControlRecipe ctrlrcp;
                rcp.RecipeId = DataUtility.ParseInt(txtRcpId.Text);
                if(!Manager.IRcp.IsExist(rcp))
                {
                    rcp = Form2Para();
                    camrcp = Form2CamPara();
                    scanrcp = Form2ScanPara();
                    ctrlrcp = Form2ControlPara();
                    rect = Manager.IRcp.AddRecipe(rcp, camrcp, scanrcp, ctrlrcp);
                }else
                {
                    rcp = Form2Para();
                    rcp.Version += 1;                    
                    camrcp = Form2CamPara();                    
                    scanrcp = Form2ScanPara();
                    ctrlrcp = Form2ControlPara();
                    rect = Manager.IRcp.UpdateRecipe(rcp, camrcp, scanrcp,ctrlrcp);
                }

            }catch(Exception ex)
            { LogHelper.AppLoger.Error(ex); }
            return rect;
        }
        #endregion
        #region button click
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewRecipe();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            if(Save())
            {
                isChange = false;
                Dialogs.Show("保存参数成功");
                LoadAllRcp();
            }
            else
            {
                Dialogs.Show("保存参数失败");
            }
        }

        private void btnSaveas_Click(object sender, EventArgs e)
        {
            DBCtrl.DAO.RecipesDao dao = new DBCtrl.DAO.RecipesDao();
            int maxid = dao.MaxRecipeId();
            txtRcpId.Text = maxid.ToString();
            ndpVersion.Value = 1;
            txtRcpName.Text = "";            
        }

        private void btmDel_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtRcpId.Text))
            {
                Dialogs.Show("请选择需要删除的配置");
                return;
            }
            int rcpid = DataUtility.ParseInt(txtRcpId.Text);
            if (Dialogs.Confirm(string.Format("确认删除配置参数{0}", txtRcpId.Text)))
            {
                //delete
                if (Manager.IRcp.DeleteRecipe(rcpid))
                {
                    Dialogs.Show("删除配置参数成功!");
                    LoadAllRcp();
                }
                else
                {
                    Dialogs.Show("删除配置参数失败!");
                }
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            LoadAllRcp();
        }
        #endregion

        private void dgvRcp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (isChange)
                {
                    if (!Dialogs.Confirm("更改没有保存，放弃修改?"))
                        return;
                }
                if (lstRcp != null && lstRcp.Count > 0 && e.RowIndex>=0)
                {
                    int recpe_id = lstRcp[e.RowIndex].RecipeId;
                    Dsiplay(recpe_id);
                }
            }catch(Exception ex)
            {
                Dialogs.Show(ex);
            }
        }

        private void chkIssaveImg_CheckedChanged(object sender, EventArgs e)
        {
            btnSelPath.Enabled = chkIssaveImg.Checked;
            chkIssaveImg.Text = chkIssaveImg.Checked ? "保存图像" : "不保存图像";
        }

        private void btnSelPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.Description = "请选择图片保存文件夹";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtImgPath.Text = ofd.SelectedPath + @"\";
            }
        }

        private void btnImgCenter_Click(object sender, EventArgs e)
        {
            try
            {
                if (npdImgWidth.Value<=0)
                {
                    Dialogs.Show("请输入图像宽度");
                    return;
                }
                if (npdImgHeight.Value<=0)
                {
                    Dialogs.Show("请输入图像高度");
                    return;
                }
                int width = (int)npdImgWidth.Value;
                int height = (int)npdImgHeight.Value;
                int offx = (MAX_IMAGE_WIDTH - width) / 2;
                int offy = (MAX_IMAGE_HEIGHT - height) / 2;
                npdImgWidth.Value = offx;
                npdImgHeight.Value = offy;
                //ReCheckROI();
            }
            catch (Exception ex)
            { LogHelper.AppLoger.Error(ex); }
        }

        private void btnMaxImg_Click(object sender, EventArgs e)
        {
            npdImgWidth.Value = MAX_IMAGE_WIDTH;
            npdImgHeight.Value = MAX_IMAGE_HEIGHT;
            npdStartX.Value = 0;
            npdStartY.Value = 0;
            //ReCheckROI();
        }
    }
}
