using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace HWCtrl.CameraCtrl
{
    public class CameraBase : ICamera
    {
        public bool IsPreMap
        {
            get;
            set;
        }

        public int CurrentSwathCount
        {
            get
            {
                return dicImgBuffer.Count;
            }
        }

        private int imageWidthIncrement = 4;
        public int ImageWidthIncrement
        {
            get
            {
                return imageWidthIncrement;
            }
        }

        private int imageHeightIncrement = 4;
        public int ImageHeightIncrement
        {
            get
            {
                return imageHeightIncrement;
            }
        }

        private int imageMaxWidth = 2048;
        public int ImageMaxWidth
        {
            get
            {
                return imageMaxWidth;
            }
        }

        private int imageMaxHeight = 1536;
        public int ImageMaxHeight
        {
            get
            {
                return imageMaxHeight;
            }
        }

        public bool IsCurSwathGrabEnd
        {
            get
            {
                return grabIndexInOneSwath >= SwathBufferCount ? true : false;
            }
        }

        private bool isGrabEnd = false;
        public bool IsGrabEnd
        {
            get
            {
                return isGrabEnd;
            }

            set
            {
                isGrabEnd = value;
            }
        }

        private bool isLiveMode = false;
        public bool IsLiveMode
        {
            get
            {
                return isLiveMode;
            }

            set
            {
                isLiveMode = value;
            }
        }

        private int swathBufferCount = 37;
        public int SwathBufferCount
        {
            get
            {
                return swathBufferCount;
            }

            set
            {
                swathBufferCount = value;
            }
        }

        private int swathCount = 48;
        public int SwathCount
        {
            get
            {
                return swathCount;
            }

            set
            {
                swathCount = value;
            }
        }

        public Dictionary<int, List<byte[]>> dicImgBuffer = new Dictionary<int, List<byte[]>>();

        //constructor
        public CameraBase()
        {

        }

        public virtual bool AcquisitionStatus()
        {
            throw new NotImplementedException();
        }

        public virtual void BeginAcquisition()
        {
            throw new NotImplementedException();
        }

        public virtual void EndAcquisition()
        {
            throw new NotImplementedException();
        }

        public virtual Bitmap GrabImageBitmap(PixelType pixelType)
        {
            throw new NotImplementedException();
        }

        public virtual byte[] GrabImageBytes(PixelType pixelType)
        {
            throw new NotImplementedException();
        }

        public virtual byte[] GrabImageBytes(PixelType pixelType, UInt64 timeout)
        {
            throw new NotImplementedException();
        }

        public virtual float GetCameraTemperature()
        {
            throw new NotImplementedException();
        }

        public virtual bool Init(int index = 0)
        {
            throw new NotImplementedException();
        }

        public virtual void Deinit(int index = 0)
        {
            throw new NotImplementedException();
        }

        public virtual bool SetAcquisitionMode(string strMode)
        {
            throw new NotImplementedException();
        }

        public virtual bool SetBalanceRatio(float fRoG, float fBoG)
        {
            throw new NotImplementedException();
        }

        public virtual bool SetExposureTime(float exposureTime)
        {
            throw new NotImplementedException();
        }

        public virtual bool SetGain(float gainValue)
        {
            throw new NotImplementedException();
        }

        public virtual void SetOneSoftwareTrigger()
        {
            throw new NotImplementedException();
        }

        public virtual bool SetRoi(int offsetx, int offsety, int width, int height)
        {
            throw new NotImplementedException();
        }

        public virtual bool SetStreamBufferCount(int bufCount)
        {
            throw new NotImplementedException();
        }

        public virtual bool SetStreamBufferHandlingMode(string strMode)
        {
            throw new NotImplementedException();
        }

        public int grabIndexInOneSwath = 0;
        public int swathIndex = 0;
        //public int BeginSaveIndexPos = 16;
        //public int BeginSaveIndexNeg = 37;
        ////public int BeginSaveIndexPos = 18;
        ////public int BeginSaveIndexNeg = 40;

        private int beginSaveIndexPos = 35;
        public int BeginSaveIndexPos
        {
            get
            {
                return beginSaveIndexPos;
            }
            set
            {
                beginSaveIndexPos = value;
            }
        }

        private int beginSaveIndexNeg = 70; 
        public int BeginSaveIndexNeg
        {
            get
            {
                return beginSaveIndexNeg;
            }
            set
            {
                beginSaveIndexNeg = value;
            }
        }

        public virtual void ResetSwatchId()
        {
            swathIndex = 0;
        }

        public virtual void SetSwathEnd()
        {
            swathIndex++;
            grabIndexInOneSwath = 0;
        }

        public virtual bool SetTriggerMode(bool extTrigger, TriggerType chosenTrigger)
        {
            throw new NotImplementedException();
        }

        public virtual void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new NotImplementedException();
        }

        public void ClearBeforeNewOne()
        {
            SwathBufferCount = SwathBufferCount == 0 ? 28 : SwathBufferCount;
            swathIndex = 0;
            if (dicImgBuffer == null)
                dicImgBuffer = new Dictionary<int, List<byte[]>>();
            dicImgBuffer.Clear();
            IsGrabEnd = false;
        }

        public void SaveToBuffer(Bitmap img)
        {
            //lock (dicImgBuffer)
            {
                DateTime startT = DateTime.Now;
                try
                {
                    if (dicImgBuffer == null)
                    {
                        dicImgBuffer = new Dictionary<int, List<byte[]>>();
                    }

                    if (dicImgBuffer.ContainsKey(swathIndex))
                    {
                        //have key add to cur
                        byte[] bimg = Utility.GlobalFunction.ImageToBytes(img);
                        dicImgBuffer[swathIndex].Add(bimg);
                    }
                    else
                    {
                        //not have ,create one
                        List<byte[]> lst = new List<byte[]>();
                        byte[] bimg = Utility.GlobalFunction.ImageToBytes(img);
                        lst.Add(bimg);
                        dicImgBuffer.Add(swathIndex, lst);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.AppLoger.Error(ex);
                }
                finally
                {
                    TimeSpan ustT = DateTime.Now - startT;
                    if (ustT.TotalSeconds > 1)
                        LogHelper.AppLoger.DebugFormat("save to buffer use time {0} second", ustT.TotalSeconds);
                }
            }
        }

        public void SaveToBuffer(byte[] img)
        {
            DateTime startT = DateTime.Now;
            try
            {
                if (dicImgBuffer == null)
                {
                    dicImgBuffer = new Dictionary<int, List<byte[]>>();
                }

                if (dicImgBuffer.ContainsKey(swathIndex))
                {
                    //have key add to cur
                    dicImgBuffer[swathIndex].Add(img);
                }
                else
                {
                    //not have ,create one
                    List<byte[]> lst = new List<byte[]>();
                    lst.Add(img);
                    dicImgBuffer.Add(swathIndex, lst);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                TimeSpan ustT = DateTime.Now - startT;
                if (ustT.TotalSeconds > 1)
                    LogHelper.AppLoger.DebugFormat("save to buffer use time {0} second", ustT.TotalSeconds);
            }
        }

        #region evt
        public event UpdateImageHandler UpdateImage;
        public void ThrowImage(object img)
        {
            if (UpdateImage != null)
            {
                //UpdateMessage(msg);
                foreach (UpdateImageHandler t in UpdateImage.GetInvocationList())
                {
                    t.BeginInvoke(img, null, null);
                }
            }
        }
        #endregion
    }
}
