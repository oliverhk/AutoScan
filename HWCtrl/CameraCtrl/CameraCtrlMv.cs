/******************************************
 * 相机类型：海康工业相机
 * Author:JJ.DING
 * 2018/09/14
 * ***************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using MvCamCtrl.NET;
using Utility;
using HalconDotNet;

namespace HWCtrl.CameraCtrl
{
    public class ImageArriveEventArgs : EventArgs
    {
        public byte[] imageData;
        public int witdh;
        public int heigth;
        public ImageArriveEventArgs()
        {
            imageData = null;
            witdh = 0;
            heigth = 0;
        }
    }

    public class CameraCtrlMv : CameraBase
    {
        [DllImport("Kernel32.dll")]
        internal static extern void CopyMemory(int dest, int source, int size);

        private static ArrayList instanceList = new ArrayList();
        private static ArrayList cameraIDList = new ArrayList();
        private static object cameraLock = new object();
        List<string> cameraName = new List<string>();
        CameraOperator m_pOperator;
        MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        MyCamera.cbOutputdelegate ImageCallback;
        private IntPtr pBufForDriver = IntPtr.Zero;
        private IntPtr pBufForSave = IntPtr.Zero;
        uint nSaveSize = 1024 * 1024 * 60;

        //Singleton instance
        private static CameraCtrlMv _instance;
        public static CameraCtrlMv Instance
        {
            get
            {
                if (cameraIDList.Count > 0)
                {
                    lock (cameraLock)
                    {
                        return instanceList[0] as CameraCtrlMv;
                    }
                }
                else
                {
                    lock (cameraLock)
                    {
                        _instance = new CameraCtrlMv();
                        instanceList.Add(_instance);
                        return _instance;
                    }
                }
            }
        }

        private CameraCtrlMv()
        {
            m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            m_pOperator = new CameraOperator();

            uint nBufSize = 1024 * 1024 * 50;
            pBufForDriver = Marshal.AllocHGlobal((int)nBufSize);
            pBufForSave = Marshal.AllocHGlobal((int)nSaveSize);
        }

        ~CameraCtrlMv()
        {
            Marshal.FreeHGlobal(pBufForDriver);
            Marshal.FreeHGlobal(pBufForSave);
            if (IsGrabEnd == false)
            {
                m_pOperator.StopGrabbing();
                IsGrabEnd = true;
            }
            m_pOperator.Close();
        }

        #region method
        private string GetCameraSerialNumber(int index)
        {
            try
            {
                string serialNumber = "";
                MyCamera.MV_CC_DEVICE_INFO device;
                if (index >= m_pDeviceList.nDeviceNum || index < 0)
                {
                    return null;
                }

                device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(
                    m_pDeviceList.pDeviceInfo[index],
                    typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(
                        buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    serialNumber = gigeInfo.chSerialNumber;
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(
                        buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    serialNumber = usbInfo.chSerialNumber;
                }

                return serialNumber;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
                return null;
            }
        }

        public override bool Init(int index = 0)
        {
            bool ret = false;
            string serialNumber = null;
            MyCamera.MV_CC_DEVICE_INFO device;
            try
            {
                CameraOperator.EnumDevices(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                if (m_pDeviceList.nDeviceNum == 0)
                {
                    throw new Exception("相机未连接");
                }

                serialNumber = GetCameraSerialNumber(index);
                if (!cameraIDList.Contains(serialNumber))
                {
                    cameraIDList.Add(serialNumber);
                }

                device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[0], typeof(MyCamera.MV_CC_DEVICE_INFO));
                //打开设备
                int nRet = -1;
                nRet = m_pOperator.Open(ref device);
                if (MyCamera.MV_OK != nRet)
                {
                    throw new Exception("初始化失败");
                    //return;
                }

                //设置采集连续模式
                m_pOperator.SetEnumValue("AcquisitionMode", 2);
                m_pOperator.SetEnumValue("TriggerMode", 1);
                //m_pOperator.SetPixelFormatValue(0);

                ImageCallback = new MyCamera.cbOutputdelegate(ImageOut);
                m_pOperator.RegisterImageCallBack(ImageCallback, IntPtr.Zero);

                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception(serialNumber + "初始化失败：" + ex.Message);
            }
            return ret;
        }

        public override void BeginAcquisition()
        {
            m_pOperator.StartGrabbing();
            IsGrabEnd = false;
        }
        public override void EndAcquisition()
        {
            if (IsGrabEnd == false)
            {
                m_pOperator.StopGrabbing();
                IsGrabEnd = true;
            }
        }
        public override void SetOneSoftwareTrigger()
        {
            try
            {
                m_pOperator.CommandExecute("TriggerSoftware");
            }
            catch
            {
                throw new Exception("取图失败");
            }
        }
 
        public override bool SetTriggerMode(bool extTrigger, TriggerType chosenTrigger)
        {
            m_pOperator.SetEnumValue("TriggerMode", 1);
            if (chosenTrigger == TriggerType.Software)
            {
                m_pOperator.SetEnumValue("TriggerSource", 7);
            }
            else if (chosenTrigger == TriggerType.Hardware)
            {
                m_pOperator.SetEnumValue("TriggerSource", 0);
            }
            return false;
        }

        public override bool SetRoi(int offsetx, int offsety, int width, int height)
        {
            bool ret = false;
            uint MaxWidth = 0;
            m_pOperator.GetIntValue("WidthMax", ref MaxWidth);
            if (MaxWidth > width)
            {
                ret = SetImageWidth(width, height);
                ret &= SetImageOffset(offsetx, offsety);
            }
            else
            {
                ret = SetImageOffset(offsetx, offsety);
                ret &= SetImageWidth(width, height);
            }
            return ret;
        }

        private bool SetImageWidth(int width, int height)
        {
            bool ret = false;
            int res = 0;

            res = m_pOperator.SetIntValue("Width", (uint)width);
            res += m_pOperator.SetIntValue("Height", (uint)height);
            if (res == 0)
            {
                ret = true;
            }

            return ret;
        }

        private bool SetImageOffset(int offsetx, int offsety)
        {
            bool ret = false;
            int res = 0;

            res = m_pOperator.SetIntValue("OffsetX", (uint)offsetx);
            res += m_pOperator.SetIntValue("OffsetY", (uint)offsety);
            if (res == 0)
            {
                ret = true;
            }

            return ret;
        }

        public override bool SetExposureTime(float exposureTime)
        {
            if (0 == m_pOperator.SetFloatValue("ExposureTime", exposureTime))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool SetGain(float gainValue)
        {
            if (0 == m_pOperator.SetFloatValue("Gain", gainValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override float GetCameraTemperature()
        {
            float temperature = -1f;
            try
            {
                m_pOperator.SetEnumValue("DeviceTemperatureSelector", 0);  //0: Sensor  1: Mainboard
                m_pOperator.GetFloatValue("DeviceTemperature", ref temperature);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return temperature;
        }

        #endregion

        private void ImageOut(IntPtr pixelPointer, ref MyCamera.MV_FRAME_OUT_INFO pFrameInfo, IntPtr pUser)
        {
            try
            {
                MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
                stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp;
                stSaveParam.enPixelType = pFrameInfo.enPixelType;
                stSaveParam.pData = pixelPointer;
                stSaveParam.nDataLen = pFrameInfo.nFrameLen;
                stSaveParam.nHeight = pFrameInfo.nHeight;
                stSaveParam.nWidth = pFrameInfo.nWidth;
                stSaveParam.pImageBuffer = pBufForSave;
                stSaveParam.nBufferSize = nSaveSize;
                stSaveParam.nJpgQuality = 80;
                if (MyCamera.MV_OK != m_pOperator.SaveImage(ref stSaveParam))
                {
                    return;
                }

                //byte[] image = new byte[stSaveParam.nImageLen];
                //Marshal.Copy(pBufForSave, image, 0, (int)stSaveParam.nImageLen);
                //FileStream file = new FileStream("image.bmp", FileMode.Create, FileAccess.Write);
                //file.Write(image, 0, (int)stSaveParam.nImageLen);
                //file.Close();

                //Stopwatch sw = new Stopwatch();
                //sw.Start();

                //图像压缩
                HImage hImage = new HImage("byte", pFrameInfo.nWidth, pFrameInfo.nHeight, pixelPointer);
                HImage hNewImage = hImage.ZoomImageFactor(0.25, 0.25, "constant");  //4倍
                Bitmap bImage = GenertateGrayBitmap(hNewImage);
                //bImage.Save("image.bmp");
                byte[] image = GlobalFunction.ImageToBytes(bImage);

                //sw.Stop();
                //TimeSpan ts2 = sw.Elapsed;
                //string info = "压缩总共花费" + ts2.TotalMilliseconds.ToString() + "ms.";
                //LogHelper.AppLoger.Info(info);

                ThrowImage(image);
            }
            catch
            {
                if (IsGrabEnd == false)
                {
                    m_pOperator.StopGrabbing();
                    IsGrabEnd = true;
                }
                m_pOperator.Close();
            }
        }

        private Bitmap GenertateGrayBitmap(HImage image)
        {
            HTuple hpoint, type, width, height;

            const int Alpha = 255;
            int[] ptr = new int[2];
            HOperatorSet.GetImagePointer1(image, out hpoint, out type, out width, out height);

            Bitmap res = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            ColorPalette pal = res.Palette;
            for (int i = 0; i <= 255; i++)
            {
                pal.Entries[i] = Color.FromArgb(Alpha, i, i, i);
            }
            res.Palette = pal;
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
            ptr[0] = bitmapData.Scan0.ToInt32();
            ptr[1] = hpoint.I;
            IntPtr ptr0 = new IntPtr(ptr[0]);
            IntPtr ptr1 = new IntPtr(ptr[1]);
            if (width % 4 == 0)
            {
                CopyMemory(ptr[0], ptr[1], width * height * PixelSize);
            }
            else
            {
                for (int i = 0; i < height - 1; i++)
                {
                    ptr[1] += width;
                    CopyMemory(ptr[0], ptr[1], width * PixelSize);
                    ptr[0] += bitmapData.Stride;
                }
            }
            res.UnlockBits(bitmapData);

            return res;
        }
    }
}