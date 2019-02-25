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

namespace MainApp.WinUC
{
    public class GlobalFocus
    {
        private GlobalFocusUC m_uc;
        private ManagedCamera m_camera;
        private AutoResetEvent m_grabThreadExited;
        private GlobalFocusOfSinglePointROIForDll.Class1 myDll2;
        private ManagedImage m_OkImage;

        public GlobalFocus(GlobalFocusUC m_uc)
        {
            this.m_uc = m_uc;
            this.myDll2 = new GlobalFocusOfSinglePointROIForDll.Class1();
        }

        public void DoScan()
        {
            OpenCamera();
            try
            {
                List<float[]> results = new List<float[]>();
                float startX = float.Parse(this.m_uc.tbxStartX.Text);
                float endX = float.Parse(this.m_uc.tbxEndX.Text);
                float gapX = float.Parse(this.m_uc.tbxGapX.Text);
                float startY = float.Parse(this.m_uc.tbxStartY.Text);
                float endY = float.Parse(this.m_uc.tbxEndY.Text);
                float gapY = float.Parse(this.m_uc.tbxGapY.Text);
                float startZ = float.Parse(this.m_uc.tbxStartZ.Text);
                float endZ = float.Parse(this.m_uc.tbxEndZ.Text);
                float gapZ = float.Parse(this.m_uc.tbxGapZ.Text);
                int cntX = (int)((endX - startX) / gapX);
                int cntY = (int)((endY - startY) / gapY);
                int cntZ = (int)((endZ - startZ) / gapZ);
               
                float posY = 0;
                for (int j = 0; j < cntY; j++)
                {
                    posY = startY + j * gapY;
                    float posY_m = (float)posY / 1000;
                    Manager.IMotorCtrl.Move(2, posY_m);
                    Thread.Sleep(100);

                    float posX = 0;
                    for (int i = 0; i < cntX; i++)
                    {
                        posX = startX + i * gapX;
                        float posX_m = (float)posX / 1000;
                        Manager.IMotorCtrl.Move(1, posX_m);
                        Thread.Sleep(100);

                        float posZ_m= DoFocus(startZ, endZ, cntZ, gapZ);
                        results.Add(new float[] { posX_m, posY_m, posZ_m });
                    }
                }
                
            }
            catch(Exception ex)
            {
                this.m_uc.SetLogOnForm(ex.Message);
            }
            CloseCamera();
        }

        public float DoFocus(float startZ, float endZ, int cntZ,float gapZ)
        {
            float new_posZ_m = 0;
            List<float> zposList = new List<float>();
            List<byte[,]> bytelist = new List<byte[,]>();
            float posZ = 0;

            for (int i = 0; i < cntZ; i++)
            {
                posZ = startZ + i * gapZ;
                float posZ_m = (float)posZ / 1000;
                Manager.IMotorCtrl.Move(3, posZ_m);
                Thread.Sleep(300);

                ManagedImage m_processedImage = GrabImage();
                byte[,] grayData = GetImageArray2D(m_processedImage.bitmap.Clone() as Bitmap);

                this.m_uc.SetImageOnForm(m_processedImage.bitmap);
                zposList.Add(posZ_m);
                bytelist.Add(grayData);
            }

            new_posZ_m = MatlabPeek(zposList, bytelist, cntZ);
            Manager.IMotorCtrl.Move(3, new_posZ_m);
            Thread.Sleep(300);

            this.m_OkImage = GrabImage();
            this.m_uc.SetImageOnForm(this.m_OkImage.bitmap);
            return new_posZ_m;
        }

        private ManagedImage GrabImage()
        {
            ManagedImage m_rawImage = new ManagedImage();
            FireSoftwareTrigger(m_camera);  //trigger camera
            m_camera.RetrieveBuffer(m_rawImage);//recevi image
            ManagedImage m_processedImage = new ManagedImage();
            m_rawImage.Convert(PixelFormat.PixelFormatRaw8, m_processedImage);//convert 
            return m_processedImage;
        }

        private void OpenCamera()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("OpenCamera:" + DateTime.Now.ToString("HH:mm:ss.fff"));
                ManagedBusManager busMgr = new ManagedBusManager();
                uint numCameras = busMgr.GetNumOfCameras();
                if (numCameras == 0)
                {
                    System.Diagnostics.Debug.WriteLine("没有发现相机!");
                    return;
                }
                m_camera = new ManagedCamera();

                //m_processedImage = new ManagedImage();
                m_grabThreadExited = new AutoResetEvent(false);
                ManagedPGRGuid m_guid = busMgr.GetCameraFromIndex(0);

                // Connect to the first selected GUID
                m_camera.Connect(m_guid);

                // Set embedded timestamp to on
                EmbeddedImageInfo embeddedInfo = m_camera.GetEmbeddedImageInfo();
                embeddedInfo.timestamp.onOff = true;
                m_camera.SetEmbeddedImageInfo(embeddedInfo);

                m_camera.StartCapture();
                System.Diagnostics.Debug.WriteLine("OpenCamera:" + DateTime.Now.ToString("HH:mm:ss.fff"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void CloseCamera()
        {
            try
            {
                m_camera.Disconnect();
                m_camera.Dispose();
            }
            catch { }
        }

        private bool FireSoftwareTrigger(ManagedCamera cam)
        {
            const uint k_softwareTrigger = 0x62C;
            const uint k_fireVal = 0x80000000;
            cam.WriteRegister(k_softwareTrigger, k_fireVal);
            return true;
        }

        private byte[,] GetImageArray2D(Bitmap img)
        {
            byte[,] grayData = new byte[256, 256];
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    grayData[i, j] = img.GetPixel(j, i).B;
                }
            }
            return grayData;
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

        private float MatlabPeek(List<float> zposList, List<byte[,]> bytelist, int max_cnt)
        {
            MWCellArray mwcell = new MWCellArray(max_cnt, 1);
            for (int i = 0; i < max_cnt; i++)
            {
                MWNumericArray mwarr = bytelist[i];
                mwcell[i + 1, 1] = mwarr;
            }
            MWNumericArray mwz = zposList.ToArray();
            MWArray mwret = this.myDll2.GlobalFocusOfSinglePointROIForDll(mwz, mwcell);
            float[,] ret = (float[,])((MWNumericArray)mwret).ToArray(MWArrayComponent.Real);
            float new_z_pos_m = ret[0, 0];
            return new_z_pos_m;
        }
    }

}
