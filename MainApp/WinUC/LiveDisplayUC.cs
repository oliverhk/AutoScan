using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using CommonLibrary;
using Utility;
namespace MainApp.WinUC
{
    public partial class LiveDisplayUC : UserControl
    {
        #region 属性
        private ArrayList lstImg;
        private int IndexDisplay;
        private bool IsShowIn = false;
        #endregion
        public LiveDisplayUC()
        {
            InitializeComponent();
            Init();
        }
        #region Method
        private void Init()
        {
            try
            {
                lstImg = new ArrayList();
                IndexDisplay = 0;
                timer1.Start();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
        #region interface
        public void SetImage(Bitmap image)
        {
            try
            {
                lstImg.Add(image);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public Point GetMousePostion()
        {
            Point rect = new Point(0, 0);
            try
            {

            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        #endregion

        #region form event
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsShowIn)
                    return;
                IsShowIn = true;

                if(lstImg.Count >0 )
                {
                    picImg.Image = (Bitmap)lstImg[IndexDisplay];
                }
                //zai xin

            }catch(Exception  ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion

        private void picImg_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                barLocation.Text = string.Format("坐标:{{0},{1}}",e.X,e.Y);                
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
    }
}
