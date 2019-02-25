using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using FlyCapture2Managed;
using MathWorks.MATLAB.NET.Arrays;

namespace MainApp.WinUI
{
    public class AutoFocus
    {
        private MaintainForm m_form;
        //private ManagedCamera m_camera;
        //private AutoResetEvent m_grabThreadExited;
        //private GlobalFocusOfSinglePointROIForDll.Class1 myDll2;
        //private ManagedImage m_OkImage;
        public float z_globalFitPos;
        public AutoFocus(MaintainForm m_form)
        {
            this.m_form = m_form;
            //this.myDll2 = new GlobalFocusOfSinglePointROIForDll.Class1();
        }
     
        public void DoFocus()
        {
            //OpenCamera();
            try
            {
                List<float> zposList = new List<float>();
                List<byte[,]> bytelist = new List<byte[,]>();

                float z_start_pos = float.Parse(this.m_form.txtStartZPos.Text);
                int max_cnt = int.Parse(this.m_form.txtZCnt.Text);
                float gap = float.Parse(this.m_form.txtZgap.Text);
                float z_pos = 0;

                for (int i = 0; i < max_cnt; i++)
                {
                    z_pos = z_start_pos + i * gap;
                    float z_pos_m = (float)z_pos / 1000;
                    this.m_form.SetPosOnForm(z_pos);
                    Manager.IMotorCtrl.Move(3, z_pos_m);
                    Thread.Sleep(100);

                    Bitmap img = GrabImage();
                    byte[,] grayData = Manager.IFocus.GetImageArray2D(img);

                    this.m_form.SetImageOnForm(img);
                    zposList.Add(z_pos_m);
                    bytelist.Add(grayData);
                }

                //System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
                //float new_z_pos_m = MatlabPeek(zposList, bytelist, max_cnt);
                float new_z_pos_m = Manager.IFocus.MatlabPeek(zposList, bytelist, max_cnt);
                z_globalFitPos = new_z_pos_m;
                this.m_form.SetMsgOnForm(this.m_form.txtOutMsg, string.Format("最清晰位置:{0}\n", new_z_pos_m));
                Manager.IMotorCtrl.Move(3, new_z_pos_m);
                Thread.Sleep(200);

                Bitmap imgFocus = GrabImage();
                this.m_form.SetImageOnForm(imgFocus);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }            
        }

        public Bitmap GrabImage()
        {
            Manager.ICamCtrl.SetOneSoftwareTrigger();
            Bitmap img = Manager.ICamCtrl.GrabImageBitmap(HWCtrl.CameraCtrl.PixelType.Mono8);
            return img;
        }

        public static byte[,] GetImageArray2D(Bitmap srcBmp, Rectangle rect)
        {
            int width = rect.Width;
            int height = rect.Height;

            System.Drawing.Imaging.BitmapData srcBmpData = srcBmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            IntPtr srcPtr = srcBmpData.Scan0;
            int scanWidth = width * 3;
            int src_bytes = scanWidth * height;
            //int srcStride = srcBmpData.Stride;
            byte[] srcRGBValues = new byte[src_bytes];
            byte[,] grayValues = new byte[height, width];
            //RGB[] rgb = new RGB[srcBmp.Width * rows];
            //复制GRB信息到byte数组
            System.Runtime.InteropServices.Marshal.Copy(srcPtr, srcRGBValues, 0, src_bytes);
            //解锁位图
            srcBmp.UnlockBits(srcBmpData);
            //灰度化处理
            int m = 0, i = 0, j = 0;  //m表示行，j表示列
            int k = 0;
            byte gray;

            for (i = 0; i < height; i++)  //只获取图片的rows行像素值
            {
                for (j = 0; j < width; j++)
                {
                    //只处理每行中图像像素数据,舍弃未用空间
                    //注意位图结构中RGB按BGR的顺序存储
                    k = 3 * j;
                    //gray = (byte)(srcRGBValues[i * scanWidth + k + 2] * 0.299
                    //     + srcRGBValues[i * scanWidth + k + 1] * 0.587
                    //     + srcRGBValues[i * scanWidth + k + 0] * 0.114);

                    byte r = srcRGBValues[i * scanWidth + k + 2];
                    byte g = srcRGBValues[i * scanWidth + k + 1];
                    byte b = srcRGBValues[i * scanWidth + k + 0];
                    grayValues[m, j] = b;  //将灰度值存到double的数组中
                }
                m++;
            }

            return grayValues;
        }

        //private float MatlabPeek(List<float> zposList, List<byte[,]> bytelist, int max_cnt)
        //{
        //    MWCellArray mwcell = new MWCellArray(max_cnt, 1);
        //    for (int i = 0; i < max_cnt; i++)
        //    {
        //        MWNumericArray mwarr = bytelist[i];
        //        mwcell[i + 1, 1] = mwarr;
        //    }
        //    MWNumericArray mwz = zposList.ToArray();
        //    MWArray mwret = this.myDll2.GlobalFocusOfSinglePointROIForDll(mwz, mwcell);
        //    float[,] ret = (float[,])((MWNumericArray)mwret).ToArray(MWArrayComponent.Real);
        //    float new_z_pos_m = ret[0, 0];
        //    return new_z_pos_m;
        //}

        private float GetAcceleratDistance(float a,float v0, float v1)
        {
            float t = (float)((v1 - v0) / a);
            float s = v0 * t + (float)(0.5 * a * t * t);
            return s;
        }
    }
}
