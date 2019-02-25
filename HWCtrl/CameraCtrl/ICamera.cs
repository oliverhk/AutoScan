using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWCtrl.CameraCtrl
{
    public enum TriggerType
    {
        Software,
        Hardware,
    }

    public enum PixelType
    {
        BGR8,
        Mono8
    }

    public delegate void UpdateImageHandler(object img);
    public interface ICamera
    {
        bool IsLiveMode
        {
            get;
            set;
        }

        int ImageMaxWidth
        {
            get;
        }

        int ImageMaxHeight
        {
            get;
        }

        int ImageWidthIncrement
        {
            get;
        }

        int ImageHeightIncrement
        {
            get;
        }

        /// <summary>
        /// stated swath count
        /// </summary>
        int SwathCount
        {
            get;
            set;
        }

        /// <summary>
        /// stated image count in one swath
        /// </summary>
        int SwathBufferCount
        {
            get;
            set;
        }

        /// <summary>
        /// current swath count in buffer
        /// </summary>
        int CurrentSwathCount
        {
            get;
        }

        bool IsCurSwathGrabEnd
        {
            get;
        }

        bool IsGrabEnd
        {
            get;
            set;
        }

        bool IsPreMap
        {
            get;
            set;
        }

        int BeginSaveIndexPos
        {
            get;
            set;
        }

        int BeginSaveIndexNeg
        {
            get;
            set;
        }

        //相机参数配置
        bool SetAcquisitionMode(string strMode);
        bool SetTriggerMode(bool extTrigger, TriggerType chosenTrigger);
        bool SetStreamBufferCount(int bufCount);
        bool SetStreamBufferHandlingMode(string strMode);
        bool SetRoi(int offsetx, int offsety, int width, int height);
        bool SetExposureTime(float exposureTime);
        bool SetBalanceRatio(float fRoG, float fBoG);
        bool SetGain(float gainValue);
        float GetCameraTemperature();
        bool AcquisitionStatus();

        //相机拍照控制
        bool Init(int index = 0);
        void Deinit(int index = 0);
        void BeginAcquisition();
        void EndAcquisition();
        void SetOneSoftwareTrigger();
        byte[] GrabImageBytes(PixelType pixelType);
        byte[] GrabImageBytes(PixelType pixelType, UInt64 timeout);
        Bitmap GrabImageBitmap(PixelType pixelType);
        void ThrowImage(object img);

        //扫描控制
        void Start();
        void Stop();
        void SetSwathEnd();
        void ResetSwatchId();

        //events
        event UpdateImageHandler UpdateImage;
    }
}
