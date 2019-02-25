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
    public partial class FullImageForm : MaterialForm
    {
        public FullImageForm()
        {
            InitializeComponent();
        }

        private void FullImageForm_Load(object sender, EventArgs e)
        {
            //init
            updateImg = this.UpdateImgMethod;
            Manager.ICamCtrl.UpdateImage += ICamCtrl_UpdateImage;
        }
        long gIndex = 0;
        private void ICamCtrl_UpdateImage(object img)
        {
            if (gIndex % 2 == 0)
            {
                byte[] t = (byte[])img;
                Bitmap bit = (Bitmap)Utility.GlobalFunction.BytesToImage(t);
                DisplayImage(bit);
            }
            gIndex++;
            if (gIndex > long.MaxValue - 1)
                gIndex = 0;
        }

        #region 显示图像  
        public delegate void DisplayHandler(Bitmap obj);
        //定义一个委托变量
        public DisplayHandler updateImg;
        //修改Imgae值的方法。        
        public void UpdateImgMethod(Bitmap obj)
        {
            this.kpImgView.Image = obj;
        }
        //此为在非创建线程中的调用方法，其实是使用TextBox的Invoke方法。
        public void ThreadMethodImg(Bitmap obj)
        {
            this.kpImgView.Invoke(updateImg, obj);
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
        #endregion
    }
}
