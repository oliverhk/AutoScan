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
using MaterialSkin.Controls;
namespace MainApp.WinUI
{
    public partial class FocusManualForm : MaterialForm
    {
        public FocusManualForm()
        {
            InitializeComponent();
        }

        private void FocusManualForm_Load(object sender, EventArgs e)
        {
            //init
            Init();
        }
        #region property
        private SystemConfig sysCfg = SystemConfig.Instance;
        #endregion
        #region method
        private void Init()
        {
            try
            {
                Manager.ICamCtrl.UpdateImage += ICamCtrl_UpdateImage;
                updateImg = this.UpdateImgMethod;
                InitList();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void InitList()
        {
            try
            {
                var lst = Manager.FParaEntity.GetParaBlockKeys();
                cmbList.Items.Clear();
                foreach (var v in lst)
                {
                    cmbList.Items.Add(v);
                }
                cmbList.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void ICamCtrl_UpdateImage(object img)
        {
            try
            {
                byte[] t = (byte[])img;
                Bitmap bit = (Bitmap)Utility.GlobalFunction.BytesToImage(t);
                int width = int.Parse(txtImgWidth.Text);
                int heith = int.Parse(txtImgHeight.Text);
                Bitmap newBit = CropImage(bit, width, heith);
                bit.Dispose();
                DisplayImage(newBit);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private Bitmap CropImage(Bitmap orgImg,int width,int height)
        {
            Bitmap rect = new Bitmap(width, height);
            try
            {
                if (orgImg == null)
                    return null;
                Rectangle cropArea = new Rectangle();
                int startx = (orgImg.Width - width) / 2;
                int starty = (orgImg.Height - height) / 2;
                cropArea.X = startx<0?0:startx;
                cropArea.Y = starty<0?0:starty;
                cropArea.Width = width>orgImg.Width?orgImg.Width:width;
                cropArea.Height = height > orgImg.Height ? orgImg.Height : height;
                rect = new Bitmap(cropArea.Width, cropArea.Height);
                rect = orgImg.Clone(cropArea, orgImg.PixelFormat);
            }
            catch (Exception ex)
            { LogHelper.AppLoger.Error(ex); }
            return rect;
        }
            
        #endregion
        #region evt display
        public delegate void DisplayHandler(Bitmap obj);
        public DisplayHandler updateImg;
        public void UpdateImgMethod(Bitmap obj)
        {
            this.picDraw.Image = obj;
        }
        public void DisplayImage(Bitmap obj)
        {
            try
            {
                ThreadMethodImg(obj);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public void ThreadMethodImg(Bitmap obj)
        {
            picDraw.Invoke(updateImg, obj);
        }
        #endregion

        private void btnCamLive_Click(object sender, EventArgs e)
        {
            Manager.ICamCtrl.SetTriggerMode(false, HWCtrl.CameraCtrl.TriggerType.Software);
            Manager.ICamCtrl.IsLiveMode = true;
            Manager.ICamCtrl.Start();
        }

        private void cmbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbList.Text))
                    return;
                string name = cmbList.Text.Trim();
                var para = Manager.FParaEntity.GetParaBlock(name);
                if(para!=null)
                {
                    txtStartPos.Text = para.StartPost.ToString();
                    txtCount.Text = para.Count.ToString();
                    txtGap.Text = para.Gap.ToString();
                }else
                {
                    Dialogs.Show("获取数据异常!");
                }
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnSavePara_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(cmbList.Text))
                {
                    Dialogs.Show("请输入参数名称");
                    return;
                }
                string name = cmbList.Text;
                Entity.ParaEntity para = new Entity.ParaEntity();
                para.Name = name;
                para.StartPost = Utility.DataUtility.ParseFloat(txtStartPos.Text);
                para.Count = Utility.DataUtility.ParseFloat(txtCount.Text);
                para.Gap = Utility.DataUtility.ParseFloat(txtGap.Text);
                Manager.FParaEntity.AddPara(para);
                InitList();
                cmbList.Text = name;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnDeletePara_Click(object sender, EventArgs e)
        {
            try
            {
                string name = cmbList.Text;
                if(string.IsNullOrEmpty(name))
                {
                    Dialogs.Show("名称不能为空!");
                    return;
                }
                if(Dialogs.Confirm(string.Format("确认删除参数 {0} ?",name)))
                {
                    Manager.FParaEntity.RemoveParaBlock(name);
                    InitList();
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
    }
}
