using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Utility;
using DBCtrl.Model;
using DBCtrl.DAO;
using CommonLibrary;

namespace MarkTool.WinUI
{
    enum MarkType
    {
        MarkLabel,
        MarkPanel
    }
    public enum MarkOperation
    {
        NoAction,
        Add,
        Delete,
        Modify
    }
    public partial class OperatorForm : Form
    {
        #region instance
        //Singleton instance
        private static OperatorForm _instance;
        public static OperatorForm Instance => _instance ?? (_instance = new OperatorForm());
        #endregion

        private const int MAX_MARK_NUMBER = 3000;

        private string[] imageSubFolders;
        private string[] imageNames;
        private int curSwath = 0;
        private int curSeq = 0;

        private IList<MarkCell> lstMarkCell;
        private IList<MarkArea> lstMarkArea;
        MarkCellDao markCellDao;
        MarkAreaDao markAreaDao;
        public int markIdLastRecord = 0;
        
        public static MarkOperation markOperation = MarkOperation.NoAction;

        public delegate void updateImagePathDelegate(object sender, EventArgs e);
        public static updateImagePathDelegate updateImagePath = null;

        public delegate void updateRatioDelegate(int ratio);
        public updateRatioDelegate updateRatio = null;

        public delegate void updateMarkCellAreaIdDelegate(int areaid);
        public static updateMarkCellAreaIdDelegate updateMarkCellAreaId = null;

        //
        private int curMarkIndex = 0;
        public List<MarkLabel> markLabelList = new List<MarkLabel>();
        public Dictionary<int, MarkLabel> markIdLabelMap = new Dictionary<int, MarkLabel>();
        public Dictionary<int, MarkPanel> areaIdPanelMap = new Dictionary<int, MarkPanel>();

        //
        private Stack<MarkOperator> stackMarkOperator = new Stack<MarkOperator>();

        public static DataTable markDataTable = null;

        public OperatorForm()
        {
            InitializeComponent();

            markCellDao = new MarkCellDao();
            markAreaDao = new MarkAreaDao();
            
            this.pbImageView.MouseWheel += new MouseEventHandler(pbImg_MouseWheel);
            updateImagePath += new updateImagePathDelegate(OperatorForm_Load);
            updateMarkCellAreaId += new updateMarkCellAreaIdDelegate(UpdateMarkCellAreaId);

            curMarkIndex = 0;
        }

        private bool GetImages(int swathId)
        {
            bool ret = false;
            try
            {
                if (swathId >= imageSubFolders.Length)
                {
                    return ret;
                }

                string[] imageNames1 = Directory.GetFiles(imageSubFolders[swathId], "*.png");
                string[] imageNames2 = Directory.GetFiles(imageSubFolders[swathId], "*.bmp");
                imageNames = imageNames1.Concat(imageNames2).ToArray();
                if (imageNames.Length > 0)
                {
                    string name = Path.GetFileName(imageNames[0]);
                    txtCurSequence.Text = name.Substring(0, name.LastIndexOf('.'));
                    curSeq = 0;
                    btnPrevSeq.Enabled = false;
                    ret = true;
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        
        private void DestroyAllMarkLabel()
        {
            curMarkIndex = 0;
            markIdLabelMap.Clear();
            pbImageView.Controls.Clear();           
        }

        private void RelocateAllMarkLabel()
        {
            pbImageView.Visible = false;
            foreach (var item in markIdLabelMap)
            {
                item.Value.SetMarkLocation();
            }
            pbImageView.Visible = true;
        }

        private void UpdateImageMarkArea()
        {
            try
            {
                foreach (var item in areaIdPanelMap)
                {
                    item.Value.Hide();
                    item.Value.Dispose();
                }
                areaIdPanelMap.Clear();

                lstMarkArea = markAreaDao.GetSpecificList(txtSlideId.Text, txtCurSwath.Text, txtCurSequence.Text);
                foreach (var item in lstMarkArea)
                {
                    MarkPanel panel = new MarkPanel(item, pbImageView);
                    panel.MouseClick += new MouseEventHandler(panel_MouseClick);             
                    areaIdPanelMap.Add(item.Id, panel);
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void InitMarkLabels()
        {
            markLabelList.Clear();
            for (int i = 0; i < MAX_MARK_NUMBER; i++)
            {
                MarkLabel marklabel = new MarkLabel();
                marklabel.MouseClick += label_MouseClick;
                marklabel.MouseDoubleClick += label_MouseDoubleClick;
                marklabel.GotFocus += label_GotFocus;
                markLabelList.Add(marklabel);
            }
        }

        private void OperatorForm_Load(object sender, EventArgs e)
        {
            try
            {
                MainForm.UpdateUserDictionary();
                MainForm.UpdateUserLevelDictionary();
                MainForm.UpdateCategoryDictionary();
                MainForm.UpdateCellTypeDictionary();

                EnableAllButtons(false);
                dgvMark.AutoGenerateColumns = false;

                txtSlideId.Text = Path.GetFileName(ConstDef.ImagePath);
                imageSubFolders = Directory.GetDirectories(ConstDef.ImagePath); //文件夹列表
                if (imageSubFolders.Length > 0)
                {
                    txtCurPath.Text = imageSubFolders[0];
                    txtCurSwath.Text = Path.GetFileName(imageSubFolders[0]);
                    curSwath = 0;
                    btnPrevSwath.Enabled = false;

                    if (GetImages(0) == true)
                    {
                        pbImageView.Image = new Bitmap(imageNames[0]);
                    }
                }

                InitMarkLabels();

                EnableAllButtons(true);

                chkMark.Checked = true;
                pbImg_ImageChange();
                UpdateMarkGrid(); //to update AreaId
            }
            catch(Exception ex)
            {
                MessageBox.Show("图像路径错误！");
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void EnableAllButtons(bool enabled)
        {
            btnPrevSeq.Enabled = enabled;
            btnNextSeq.Enabled = enabled;
            btnPrevSwath.Enabled = enabled;
            btnNextSwath.Enabled = enabled;
        }

        private void btnPrevSwath_Click(object sender, EventArgs e)
        {
            try
            {
                if (curSwath == 0)
                {
                    return;
                }
                EnableAllButtons(false);

                curSwath--;
                txtCurSwath.Text = Path.GetFileName(imageSubFolders[curSwath]);
                txtCurPath.Text = imageSubFolders[curSwath];

                if (GetImages(curSwath))
                {
                    pbImageView.Visible = false;
                    pbImageView.ImageLocation = imageNames[0];
                    pbImg_ImageChange();
                }
                
                EnableAllButtons(true);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnNextSwath_Click(object sender, EventArgs e)
        {
            try
            {
                if (curSwath == imageSubFolders.Length - 1)
                {
                    return;
                }
                EnableAllButtons(false);

                curSwath++;
                txtCurSwath.Text = Path.GetFileName(imageSubFolders[curSwath]);
                txtCurPath.Text = imageSubFolders[curSwath];
                
                if (GetImages(curSwath))
                {
                    pbImageView.Visible = false;
                    pbImageView.ImageLocation = imageNames[0];
                    pbImg_ImageChange();
                }
                
                EnableAllButtons(true);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnPrevSeq_Click(object sender, EventArgs e)
        {
            try
            {
                if (curSeq == 0)
                {
                    return;
                }
                EnableAllButtons(false);
                curSeq--;
                string name = Path.GetFileName(imageNames[curSeq]);
                txtCurSequence.Text = name.Substring(0, name.LastIndexOf('.'));

                pbImageView.Visible = false;
                pbImageView.ImageLocation = imageNames[curSeq];
                pbImg_ImageChange();
                EnableAllButtons(true);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnNextSeq_Click(object sender, EventArgs e)
        {
            try
            {
                if(curSeq == imageNames.Length - 1)
                {
                    return;
                }
                EnableAllButtons(false);
                curSeq++;
                string name = Path.GetFileName(imageNames[curSeq]);
                txtCurSequence.Text = name.Substring(0, name.LastIndexOf('.'));

                pbImageView.Visible = false;
                pbImageView.ImageLocation = imageNames[curSeq];
                pbImg_ImageChange();
                EnableAllButtons(true);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        #region 表格处理
        public static DataTable DataTableConvert<T>(IList<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return null;
            }
            markDataTable = new DataTable("MarkCell");
            DataRow dr;
            PropertyInfo[] propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            markDataTable.Columns.Add("AreaId", Type.GetType("System.Int32"));
            markDataTable.Columns.Add("catename", Type.GetType("System.String"));
            markDataTable.Columns.Add("typename", Type.GetType("System.String"));
            markDataTable.Columns.Add("UserId", Type.GetType("System.String"));
            markDataTable.Columns.Add("UserLevel", Type.GetType("System.String"));
            markDataTable.Columns.Add("Id", Type.GetType("System.Int32"));
            markDataTable.Columns.Add("SlideId", Type.GetType("System.String"));
            markDataTable.Columns.Add("SwathId", Type.GetType("System.String"));
            markDataTable.Columns.Add("FieldId", Type.GetType("System.String"));
            markDataTable.Columns.Add("ImageX", Type.GetType("System.Int32"));
            markDataTable.Columns.Add("ImageY", Type.GetType("System.Int32"));
            markDataTable.Columns.Add("StageX", Type.GetType("System.Int32"));
            markDataTable.Columns.Add("StageY", Type.GetType("System.Int32"));
            markDataTable.Columns.Add("Remarks", Type.GetType("System.String"));
            markDataTable.Columns.Add("SlideFrom", Type.GetType("System.String"));
            markDataTable.Columns.Add("CreateTime", Type.GetType("System.DateTime"));
            markDataTable.Columns.Add("TypeId", Type.GetType("System.Int32"));

            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }
                dr = markDataTable.NewRow();
                for (int i = 0, j = propertyInfo.Length; i < j; i++)
                {
                    PropertyInfo pi = propertyInfo[i];
                    string name = pi.Name;
                    dr[name] = pi.GetValue(t, null);
                }

                //update typeid and userid
                int id = Convert.ToInt32(dr["TypeId"]);
                User user = MainForm.dicUser[dr["UserId"].ToString()];
                dr["catename"] = MainForm.dicCategory[MainForm.dicCellType[id].CateId].CategoryName;
                dr["typename"] = MainForm.dicCellType[id].CellType;
                dr["UserLevel"] = MainForm.dicUserLevel[user.LevelId].LevelName;

                markDataTable.Rows.Add(dr);
            }
            return markDataTable;
        }

        private void dgvMark_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex == markDataTable.Rows.Count)
                    return;
                e.Value = markDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void UpdateMarkGrid()
        {
            try
            {
                lstMarkCell = markCellDao.GetSpecificList(txtSlideId.Text, txtCurSwath.Text, txtCurSequence.Text);
                lstMarkCell = lstMarkCell.OrderBy(u => u.AreaId).ToList();  //按照区域ID升序排列
                //dgvMark.DataSource = lstMarkCell;   //take too much time
                markDataTable = DataTableConvert<MarkCell>(lstMarkCell);
                dgvMark.Rows.Clear();
                if (markDataTable != null)
                {
                    dgvMark.RowCount = markDataTable.Rows.Count;
                    dgvMark.Refresh();
                    dgvMark.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion

        private void pbImg_MouseWheel(object sender, MouseEventArgs e)
        {
            pbImageView.Width += e.Delta;     
            pbImageView.Height = Convert.ToInt32(pbImageView.Width / ((double)pbImageView.Image.Width / pbImageView.Image.Height));

            RelocateAllMarkLabel();
            pbImageView.Focus();

            MarkPanelsAutoChange();

            if (updateRatio != null)
            {
                int ratio = Convert.ToInt32((double)pbImageView.Width / pbImageView.Image.Width * 100);
                BeginInvoke(updateRatio, ratio);

                //if(ratio < 100)
                //{
                //    foreach (var item in markIdLabelMap)
                //    {
                //        item.Value.Hide();
                //    }
                //}
                //else
                //{
                //    foreach (var item in markIdLabelMap)
                //    {
                //        item.Value.Show();
                //    }
                //}
            }
        }

        private void pbImg_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {           
                ConstDef.ImageY = (int)(e.Y * ((double)pbImageView.Image.Height / pbImageView.Height));
                ConstDef.ImageX = (int)(e.X * ((double)pbImageView.Image.Width / pbImageView.Width));
                BeginInvoke(MainForm.updateCoord);

                if (isMouseLeftDown && IsMouseInPanel())
                {
                    int destPosX = pbImageView.Left + (Cursor.Position.X - mouseDownPoint.X);
                    int destPosY = pbImageView.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                    pbImageView.Left = destPosX > 0 ? destPosX : 0;
                    pbImageView.Top = destPosY > 0 ? destPosY : 0;
                    mouseDownPoint = Cursor.Position;
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void pbImg_MouseClick(object sender, MouseEventArgs e)
        {
            pbImageView.Focus();
            if(e.Button == MouseButtons.Right)
            {
                tsmiAdd.Enabled = true;
                tsmiUpdate.Enabled = false;
                tsmiDelete.Enabled = false;
                if(pbImageView.Image==null)
                {
                    tsmiResize.Enabled = false;
                }
                cmsMarkMenu.Show(pbImageView.PointToScreen(e.Location));
            }
        }

        private void chkMark_CheckChange(object sender, EventArgs e)
        {
            if(chkMark.Checked == true)
            {
                foreach(var item in markIdLabelMap)
                {
                    item.Value.Show();
                }
                foreach(var area in areaIdPanelMap)
                {
                    area.Value.Show();
                }
            }
            else
            {
                foreach (var item in markIdLabelMap)
                {
                    item.Value.Hide();
                }
                foreach (var area in areaIdPanelMap)
                {
                    area.Value.Hide();
                }
            }
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            MarkAddForm frm = new MarkAddForm();
            frm.IsAdd = true;
            while(true)
            {
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    break;
                }
                else if (result == DialogResult.OK)
                {
                    MarkCell mark = new MarkCell();
                    mark.SlideId = txtSlideId.Text;
                    mark.SwathId = txtCurSwath.Text;
                    mark.FieldId = txtCurSequence.Text;
                    mark.ImageX = ConstDef.ImageX;
                    mark.ImageY = ConstDef.ImageY;
                    mark.TypeId = frm.TypeId;
                    mark.UserId = ConstDef.UserID;
                    mark.SlideFrom = ConstDef.ImagePath;
                    mark.Remarks = frm.Remark;
                    mark.CreateTime = DateTime.Now;

                    if (markCellDao.Insert(mark))
                    {
                        MessageBox.Show("保存成功！");
                        UpdateMarkGrid();

                        MarkCell temp = markCellDao.GetMarkCellByTime(mark.CreateTime);
                        if (temp == null)
                        {
                            MessageBox.Show("读回新增标记失败！");
                        }
                        else
                        {
                            DropMarkLabel(temp, true);
                        }

                        MarkOperator op = new MarkOperator();
                        op.Type = MarkType.MarkLabel;
                        op.Mark = temp;
                        op.Operation = MarkOperation.Add;
                        stackMarkOperator.Push(op);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("保存失败！");
                        frm.TypeId = mark.TypeId;
                        frm.Remark = mark.Remarks;
                    }
                }
            }
        }

        private void DropMarkLabel(MarkCell mark, bool visible)
        {
            try
            {
                if (curMarkIndex >= MAX_MARK_NUMBER)
                {
                    LogHelper.AppLoger.Error("MarkLabel Overflow!");
                    return;
                }
                markLabelList[curMarkIndex].Parent = pbImageView;
                markLabelList[curMarkIndex].UpdateMark(mark);            
                if (visible)
                {
                    markLabelList[curMarkIndex].Visible = chkMark.Checked;
                }
                markIdLabelMap.Add(mark.Id, markLabelList[curMarkIndex]);
                curMarkIndex++;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void label_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tsmiAdd.Enabled = false;
                tsmiUpdate.Enabled = true;
                tsmiDelete.Enabled = true;
                cmsMarkMenu.Tag = (sender as MarkLabel).GetMarkId();
                cmsMarkMenu.Show(pbImageView.PointToScreen(((MarkLabel)sender).Location));
            }
        }

        private void label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cmsMarkMenu.Tag = (sender as MarkLabel).GetMarkId();
                tsmiUpdate.PerformClick();
            }
        }

        private void label_GotFocus(object sender, EventArgs e)
        {
            //
            dgvMark.ClearSelection();

            int markid = (sender as MarkLabel).GetMarkId();
            int x = 0;
            int y = 0;
            foreach (DataGridViewRow item in dgvMark.Rows)
            {
                if (Convert.ToInt32(item.Cells[5].Value) == markid)
                {
                    item.Selected = true;
                    dgvMark.FirstDisplayedScrollingRowIndex = item.Index;
                    x = Convert.ToInt32(item.Cells[9].Value);
                    y = Convert.ToInt32(item.Cells[10].Value);
                    break;
                }
            }

            foreach (DataGridViewRow item in dgvMark.Rows)
            {
                if (x == Convert.ToInt32(item.Cells[9].Value) &&
                    y == Convert.ToInt32(item.Cells[10].Value))
                {
                    item.Selected = true;
                }
            }
        }

        private void pbImg_ImageChange()
        {
            DestroyAllMarkLabel();
            Application.DoEvents();
            UpdateMarkGrid();

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            bool waitFormNeeded = (lstMarkCell.Count > 300);
            WaitForm waitForm = WaitForm.Instance;
            if (waitFormNeeded)
            {
                waitForm.Show();
                Application.DoEvents();
            }
            for (int i = 0; i < lstMarkCell.Count; i++)
            {
                DropMarkLabel(lstMarkCell[i], true);
                if (waitFormNeeded && i % 100 == 0)
                {
                    waitForm.UpdateProgress(100 * i / lstMarkCell.Count);
                    Application.DoEvents();
                }
            }
            pbImageView.Visible = true;
            if (waitFormNeeded)
            {
                waitForm.UpdateProgress(100);
                Application.DoEvents();
                waitForm.Hide();               
            }
            sw2.Stop();
            TimeSpan ts2 = sw2.Elapsed;
            string info = "标记显示总共花费" + ts2.TotalMilliseconds.ToString() + "ms.";
            LogHelper.AppLoger.Info(info);

            UpdateImageMarkArea();
        }

        private bool IsUserLimited(int markid, MarkType type)
        {
            bool ret = true;
            try
            {
                int markUserLevel = 0;
                int currentUserLevel = 0;

                if (type == MarkType.MarkLabel)
                {
                    MarkCell mark = markCellDao.GetMarkCellById(markid);
                    if (mark.UserId == ConstDef.UserID)
                    {
                        return false;
                    }
                    markUserLevel = MainForm.dicUser[mark.UserId].LevelId;
                }
                else if(type == MarkType.MarkPanel)
                {
                    MarkArea area = markAreaDao.GetMarkAreaById(markid);
                    if (area.UserId == ConstDef.UserID)
                    {
                        return false;
                    }
                    markUserLevel = MainForm.dicUser[area.UserId].LevelId;
                }
                currentUserLevel = MainForm.dicUser[ConstDef.UserID].LevelId;
                if (currentUserLevel > markUserLevel)
                {
                    ret = false;
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            int id = (int)cmsMarkMenu.Tag;

            //用户权限判断
            if (IsUserLimited(id, MarkType.MarkLabel))
            {
                Dialogs.Show("权限受限!", false);
                return;
            }

            MarkAddForm frm = new MarkAddForm();
            frm.IsAdd = false;
            MarkCell oldMark = markCellDao.GetMarkCellById(id);
            if(oldMark == null)
            {
                MessageBox.Show("获取标记信息失败！");
                return;
            }
            frm.MarkId = oldMark.Id;
            frm.Remark = oldMark.Remarks;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                MarkCell newMark = MarkCell.Copy(oldMark);
                newMark.TypeId = frm.TypeId;
                newMark.UserId = ConstDef.UserID;
                newMark.SlideFrom = ConstDef.ImagePath;
                newMark.Remarks = frm.Remark;

                if(ModifyMarkLabel(newMark))             
                {
                    MarkOperator op = new MarkOperator();
                    op.Type = MarkType.MarkLabel;
                    op.Mark = oldMark;
                    op.Operation = MarkOperation.Modify;
                    stackMarkOperator.Push(op);
                }      
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)cmsMarkMenu.Tag;

                //用户权限判断
                if (IsUserLimited(id, MarkType.MarkLabel))
                {
                    Dialogs.Show("权限受限!", false);
                    return;
                }

                MarkCell mark = markCellDao.GetMarkCellById(id);
                if (markCellDao.Delete(id))
                {
                    markIdLabelMap[id].Hide();
                    //markIdLabelMap[id].Dispose();
                    markIdLabelMap.Remove(id);
                    UpdateMarkGrid();

                    MarkOperator op = new MarkOperator();
                    op.Type = MarkType.MarkLabel;
                    op.Mark = mark;
                    op.Operation = MarkOperation.Delete;
                    stackMarkOperator.Push(op);
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void OperatorForm_Resize(object sender, EventArgs e)
        {
            try
            {
                pbImageView.Width = pnlImage.Width;
                pbImageView.Height = Convert.ToInt32(pbImageView.Width / ((double)pbImageView.Image.Width / pbImageView.Image.Height));
                RelocateAllMarkLabel();
                MarkPanelsAutoChange();

                if (updateRatio != null)
                {
                    int ratio = Convert.ToInt32((double)pbImageView.Width / pbImageView.Image.Width * 100);
                    BeginInvoke(updateRatio, ratio);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void ImageManualResize(double ratio)
        {
            pbImageView.Width = (int)(pbImageView.Image.Width * ratio);
            pbImageView.Height = Convert.ToInt32(pbImageView.Width / ((double)pbImageView.Image.Width / pbImageView.Image.Height));

            RelocateAllMarkLabel();
            MarkPanelsAutoChange();

            if (updateRatio != null)
            {
                BeginInvoke(updateRatio, (int)(ratio * 100));
            }
        }

        private void tsmi25_Click(object sender, EventArgs e)
        {
            ImageManualResize(0.25);
        }

        private void tsmi50_Click(object sender, EventArgs e)
        {
            ImageManualResize(0.5);
        }

        private void tsmi100_Click(object sender, EventArgs e)
        {
            ImageManualResize(1.0);
        }

        private void tsmi150_Click(object sender, EventArgs e)
        {
            ImageManualResize(1.5);
        }

        private void tsmi200_Click(object sender, EventArgs e)
        {
            ImageManualResize(2.0);
        }

        //标记区域
        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Point point = pbImageView.PointToScreen(((MarkPanel)sender).Location);
                    Point newPoint = new Point(point.X + e.Location.X, point.Y + e.Location.Y);
                    cmsMarkPanelMenu.Show(newPoint);
                }
                int id = (sender as MarkPanel).GetMarkId();
                cmsMarkPanelMenu.Tag = id;
                MarkOperator op = new MarkOperator();
                op.Type = MarkType.MarkPanel;
                op.Mark = MarkArea.Copy(markAreaDao.GetMarkAreaById(id));
                op.Operation = MarkOperation.Modify;
                stackMarkOperator.Push(op);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void DropMarkPanel(MarkArea mark)
        {
            try
            {
                MarkPanel panel = new MarkPanel(mark, pbImageView);
                panel.MouseClick += new MouseEventHandler(panel_MouseClick);  
                areaIdPanelMap.Add(mark.Id, panel);

                UpdateMarkCellAreaId(mark.Id);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void tsmiRect_Click(object sender, EventArgs e)
        {
            try
            {
                markOperation = MarkOperation.Add;
                
                //插入数据库
                MarkArea area = new MarkArea();
                area.SlideId = txtSlideId.Text;
                area.SwathId = txtCurSwath.Text;
                area.FieldId = txtCurSequence.Text;
                area.RectX = ConstDef.ImageX;  
                area.RectY = ConstDef.ImageY;
                area.Width = Convert.ToInt32((double)MarkPanel.PanelDefaultSize / pbImageView.Width * pbImageView.Image.Width);
                area.Height = Convert.ToInt32((double)MarkPanel.PanelDefaultSize / pbImageView.Height * pbImageView.Image.Height);
                area.UserId = ConstDef.UserID;
                area.CreateTime = DateTime.Now;

                if (markAreaDao.Insert(area))
                {
                    MarkArea areaBack = markAreaDao.GetMarkAreaByTime(area.CreateTime);                 
                    DropMarkPanel(areaBack);                

                    MarkOperator op = new MarkOperator();
                    op.Type = MarkType.MarkPanel;
                    op.Mark = areaBack;
                    op.Operation = MarkOperation.Add;
                    stackMarkOperator.Push(op);
                }
                else
                {
                    MessageBox.Show("插入区域失败！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void DeleteMarkLabel(int id)
        {
            try
            {
                if (markCellDao.Delete(id))
                {
                    markIdLabelMap[id].Hide();
                    //markIdLabelMap[id].Dispose();
                    markIdLabelMap.Remove(id);
                    UpdateMarkGrid();
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private bool ModifyMarkLabel(MarkCell mark)
        {
            bool ret = false;
            try
            {
                if (markCellDao.Update(mark))
                {
                    MessageBox.Show("保存成功！");
                    ret = true;
                    UpdateMarkGrid();
                    markIdLabelMap[mark.Id].UpdateMark(mark);
                }
                else
                {
                    MessageBox.Show("保存失败！");
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        private void DeleteMarkPanel(int id)
        {
            try
            {
                UpdateMarkCellAreaId(id);
                if (markAreaDao.Delete(id))
                {
                    areaIdPanelMap[id].Hide();
                    //areaIdPanelMap[id].Dispose();
                    areaIdPanelMap.Remove(id);
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void ModifyMarkPanel(MarkArea mark)
        {
            try
            {
                markAreaDao.Update(mark);

                areaIdPanelMap[mark.Id].Invalidate();
                areaIdPanelMap[mark.Id].UpdateMark(mark);
                areaIdPanelMap[mark.Id].Update();

                UpdateMarkCellAreaId(mark.Id);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void tsmiPanelDelete(object sender, EventArgs e)
        {
            try
            {
                int id = (int)cmsMarkPanelMenu.Tag;
                markOperation = MarkOperation.Delete;

                //用户权限判断
                if (IsUserLimited(id, MarkType.MarkPanel))
                {
                    Dialogs.Show("权限受限!", false);
                    return;
                }

                MarkOperator op = new MarkOperator();
                op.Type = MarkType.MarkPanel;
                op.Mark = markAreaDao.GetMarkAreaById(id);
                op.Operation = MarkOperation.Delete;
                stackMarkOperator.Push(op);

                DeleteMarkPanel(id);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void MarkPanelsAutoChange()
        {
            try
            {
                foreach (var item in areaIdPanelMap)
                {                   
                    item.Value.SetMarkAreaLocation();
                    item.Value.SetMarkAreaSize();
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void tsmiMarkAdd_Click(object sender, EventArgs e)
        {
            tsmiAdd_Click(sender, e);
            UpdateImageMarkArea();
        }

        private void UpdateMarkCellAreaId(int areaid)
        {
            try
            {
                if (areaid <= 0)
                {
                    return;
                }

                foreach(var mark in lstMarkCell)
                {
                    Point pos = new Point(mark.ImageX, mark.ImageY);
                    
                    switch(markOperation)
                    {
                        case MarkOperation.Add:
                            {
                                if (IsMarkInArea(pos, areaid))
                                {
                                    mark.AreaId = areaid;
                                    markCellDao.Update(mark);
                                }
                            }
                            break;
                        case MarkOperation.Modify:
                            {
                                if (IsMarkInArea(pos, areaid))
                                {
                                    mark.AreaId = areaid;
                                    markCellDao.Update(mark);
                                }
                                else if (mark.AreaId == areaid)
                                {
                                    mark.AreaId = 0;
                                    markCellDao.Update(mark);
                                }
                            }
                            break;
                        case MarkOperation.Delete:
                            {
                                if (IsMarkInArea(pos, areaid))
                                {
                                    mark.AreaId = 0;
                                    markCellDao.Update(mark);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                UpdateMarkGrid();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private bool IsMarkInArea(Point markpos, int areaid)
        {
            return areaIdPanelMap[areaid].IsPointInArea(markpos);
        }

        private void dgvMark_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    int markId = Convert.ToInt32(dgvMark.Rows[e.RowIndex].Cells[5].Value);
                    markIdLabelMap[markId].BackColor = Color.Red;
                    markIdLabelMap[markId].BringToFront();
                    if (markIdLabelMap.ContainsKey(markIdLastRecord))
                    {
                        markIdLabelMap[markIdLastRecord].BackColor = Color.White;
                    }
                    markIdLastRecord = markId;
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void dgvMark_Leave(object sender, EventArgs e)
        {
            try
            {
                dgvMark.ClearSelection();
                if (markIdLabelMap.ContainsKey(markIdLastRecord))
                {
                    markIdLabelMap[markIdLastRecord].BackColor = Color.White;
                }
                markIdLastRecord = 0;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        struct MarkOperator
        {
            public MarkType Type;
            public object Mark;
            public MarkOperation Operation;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                btnMarkUndo_Click();
                return true;
            }
            else if(keyData == (Keys.Control | Keys.S))
            {
                btnMarkSave_Click();
                return true;
            }
            else if(keyData == Keys.Delete)
            {
                foreach (var item in markIdLabelMap)
                {
                    if(item.Value.Focused)
                    {
                        cmsMarkMenu.Tag = item.Key;
                        tsmiDelete_Click(null, null);
                        return true;
                    }
                }
                foreach (var item in areaIdPanelMap)
                {
                    if (item.Value.Focused)
                    {
                        cmsMarkPanelMenu.Tag = item.Value.Tag;
                        tsmiPanelDelete(null, null);
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void btnMarkSave_Click()
        {
            stackMarkOperator.Clear();
        }

        public void btnMarkUndo_Click()
        {
            try
            {
                if (stackMarkOperator.Count > 0)
                {
                    MarkOperator op = stackMarkOperator.Pop();
                    switch(op.Type)
                    {
                        case MarkType.MarkLabel:
                            {
                                MarkCell markcell = (MarkCell)op.Mark;
                                if (op.Operation == MarkOperation.Add)
                                {
                                    DeleteMarkLabel(markcell.Id);
                                }
                                else if (op.Operation == MarkOperation.Delete)
                                {
                                    markCellDao.Insert(markcell);
                                    DropMarkLabel(markcell, true);
                                    UpdateMarkGrid();
                                }
                                else if (op.Operation == MarkOperation.Modify)
                                {
                                    ModifyMarkLabel(markcell);
                                }
                            }                         
                            break;
                        case MarkType.MarkPanel:
                            {
                                MarkArea markarea = (MarkArea)op.Mark;
                                if (op.Operation == MarkOperation.Add)
                                {
                                    markOperation = MarkOperation.Delete;
                                    DeleteMarkPanel(markarea.Id);
                                }
                                else if (op.Operation == MarkOperation.Delete)
                                {
                                    markOperation = MarkOperation.Add;
                                    markAreaDao.Insert(markarea);
                                    DropMarkPanel(markarea);
                                }
                                else if (op.Operation == MarkOperation.Modify)
                                {
                                    markOperation = MarkOperation.Modify;                                    
                                    ModifyMarkPanel(markarea);
                                }
                            }
                            break;
                        default:
                            break;
                    }                 
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void dgvMark_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            cmsMarkMenu.Tag = Convert.ToInt32(e.Row.Cells[5].Value);
            tsmiDelete_Click(null, null);
        }

        private void tsmiMarkGridDelete_Click(object sender, EventArgs e)
        {
            List<int> markId = new List<int>();
            for (int i = 0; i < dgvMark.SelectedRows.Count; i++)
            {
                markId.Add(Convert.ToInt32(dgvMark.SelectedRows[i].Cells[5].Value));
            }

            foreach(var id in markId)
            {
                cmsMarkMenu.Tag = id;
                tsmiDelete_Click(null, null);
            }
        }

        private void dgvMark_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (!dgvMark.Rows[e.RowIndex].Selected)
                    {
                        if (e.RowIndex >= 0)
                        {
                            dgvMark.ClearSelection();
                            dgvMark.Rows[e.RowIndex].Selected = true;

                            int markId = Convert.ToInt32(dgvMark.Rows[e.RowIndex].Cells[5].Value);
                            markIdLabelMap[markId].BackColor = Color.Red;
                            markIdLabelMap[markId].BringToFront();
                            if (markIdLabelMap.ContainsKey(markIdLastRecord))
                            {
                                markIdLabelMap[markIdLastRecord].BackColor = Color.White;
                            }
                            markIdLastRecord = markId;
                        }
                    }

                    cmsMarkGrid.Show(MousePosition);
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void pbImageView_MouseEnter(object sender, EventArgs e)
        {
            pbImageView.Cursor = Cursors.Hand;
        }

        private void pbImageView_MouseLeave(object sender, EventArgs e)
        {
            pbImageView.Cursor = Cursors.Default;
        }

        private void pbImageView_DoubleClick(object sender, EventArgs e)
        {
            tsmiAdd.PerformClick();
        }

        private Point mouseDownPoint;
        private bool isMouseLeftDown = false;
        private bool IsMouseInPanel()
        {
            if (pbImageView.Left < PointToClient(Cursor.Position).X
            && PointToClient(Cursor.Position).X < pbImageView.Left + pbImageView.Width
            && pbImageView.Top < PointToClient(Cursor.Position).Y
            && PointToClient(Cursor.Position).Y < pbImageView.Top + pbImageView.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void pbImageView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint = Cursor.Position;
                isMouseLeftDown = true;
            }
        }

        private void pbImageView_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseLeftDown = false;
        }
    }
}
