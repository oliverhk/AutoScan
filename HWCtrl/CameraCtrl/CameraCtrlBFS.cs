/******************************************
 * 相机类型：BFS-U3-51S5M-C
 * Remark:use grey point spinview sdk.
 * ***************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using Utility;
using CommonLibrary;
using System.Drawing.Imaging;
using Spinnaker;
using SpinnakerNET;
using SpinnakerNET.GenApi;
using System.Collections;
using System.Diagnostics;

namespace HWCtrl.CameraCtrl
{
    public class CameraCtrlBFS : CameraBase
    {
        #region instance
        //Singleton instance
        private static CameraCtrlBFS _instance;
        public static CameraCtrlBFS Instance => _instance ?? (_instance = new CameraCtrlBFS());
        #endregion
      
        private IManagedCamera m_Camera = null;
        private INodeMap m_NodeMap;
        private AutoResetEvent m_grabThreadExited;
        public CameraCtrlBFS()
        {
            m_grabThreadExited = new AutoResetEvent(false);
            //InitSave();
        }

        #region CameraBase methods override
        public override void BeginAcquisition()
        {
            if (m_Camera != null)
            {
                m_Camera.BeginAcquisition();
            }
        }

        public override void EndAcquisition()
        {
            if (m_Camera != null)
            {
                m_Camera.EndAcquisition();
            }
        }

        public override bool AcquisitionStatus()
        {
            if (m_Camera != null)
                return m_Camera.IsStreaming();
            else
                return false;
        }

        public override Bitmap GrabImageBitmap(PixelType pixelType)
        {
            Bitmap image = null;
            try
            {
                ClearBeforeNewOne();
                IManagedImage m_rawImage = m_Camera.GetNextImage();
                if (m_rawImage.ImageStatus != ImageStatus.IMAGE_NO_ERROR)
                {
                    LogHelper.AppLoger.Error("grab img error,error:" + m_rawImage.ImageStatus);
                }
                lock (this)
                {
                    if (pixelType == PixelType.BGR8)
                    {
                        using (IManagedImage convertedImage = m_rawImage.Convert(PixelFormatEnums.BGR8))
                        {
                            image = convertedImage.bitmap.Clone() as Bitmap;
                        }
                    }
                    else if(pixelType == PixelType.Mono8)
                    {
                        using (IManagedImage convertedImage = m_rawImage.Convert(PixelFormatEnums.Mono8))
                        {
                            image = convertedImage.bitmap.Clone() as Bitmap;
                        }
                    }
                    m_rawImage.Release();
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return image;
        }

        public override byte[] GrabImageBytes(PixelType pixelType)
        {
            byte[] image = null;
            try
            {
                ClearBeforeNewOne();
                IManagedImage m_rawImage = m_Camera.GetNextImage();
                if (m_rawImage.ImageStatus != ImageStatus.IMAGE_NO_ERROR)
                {
                    LogHelper.AppLoger.Error("grab img error,error:" + m_rawImage.ImageStatus);
                }
                lock (this)
                {
                    if (pixelType == PixelType.BGR8)
                    {
                        using (IManagedImage convertedImage = m_rawImage.Convert(PixelFormatEnums.BGR8))
                        {
                            image = Utility.GlobalFunction.ImageToBytes(convertedImage.bitmap);                           
                        }
                    }
                    else if(pixelType == PixelType.Mono8)
                    {
                        using (IManagedImage convertedImage = m_rawImage.Convert(PixelFormatEnums.Mono8))
                        {
                            image = Utility.GlobalFunction.ImageToBytes(convertedImage.bitmap);
                        }
                    }
                    m_rawImage.Release();
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return image;
        }

        public override byte[] GrabImageBytes(PixelType pixelType, UInt64 timeout)
        {
            byte[] image = null;
            try
            {
                //ClearBeforeNewOne();
                IManagedImage m_rawImage = m_Camera.GetNextImage(timeout);
                if (m_rawImage.ImageStatus != ImageStatus.IMAGE_NO_ERROR)
                {
                    LogHelper.AppLoger.Error("grab img error,error:" + m_rawImage.ImageStatus);
                }
                lock (this)
                {
                    if (pixelType == PixelType.BGR8)
                    {
                        using (IManagedImage convertedImage = m_rawImage.Convert(PixelFormatEnums.BGR8))
                        {
                            image = Utility.GlobalFunction.ImageToBytes(convertedImage.bitmap);
                        }
                    }
                    else if (pixelType == PixelType.Mono8)
                    {
                        using (IManagedImage convertedImage = m_rawImage.Convert(PixelFormatEnums.Mono8))
                        {
                            image = Utility.GlobalFunction.ImageToBytes(convertedImage.bitmap);
                        }
                    }
                    m_rawImage.Release();
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return image;
        }

        public override bool Init(int index = 0)
        {
            bool ret = false;
            try
            {
                // Retrieve singleton reference to system object
                ManagedSystem system = new ManagedSystem();
                // Retrieve list of cameras from the system
                IList<IManagedCamera> camList = system.GetCameras();

                LogHelper.AppLoger.DebugFormat("Number of cameras detected: {0}", camList.Count);
                if (camList.Count == 0)
                {
                    LogHelper.AppLoger.Error("没有发现相机!");
                    return ret;
                }

                m_Camera = camList[index];
                // Retrieve TL device nodemap and print device information
                INodeMap nodeMapTLDevice = m_Camera.GetTLDeviceNodeMap();

                // Initialize camera
                m_Camera.Init();
                // Retrieve GenICam nodemap
                m_NodeMap = m_Camera.GetNodeMap();
                
                //if (!m_camera.DeviceConnectionStatus.IsRegister) 
                //{
                //    Dialogs.Show("连接相机失败!");
                //    return ret;
                //}
                //CameraInfo camInfo = m_camera.GetCameraInfo();
                IString iDeviceSerialNumber = nodeMapTLDevice.GetNode<IString>("DeviceSerialNumber");
                LogHelper.AppLoger.DebugFormat("camera serial number:{0}", iDeviceSerialNumber);
                //Set embedded timestamp to on
                //EmbeddedImageInfo embeddedInfo = m_camera.GetEmbeddedImageInfo();
                //embeddedInfo.timestamp.onOff = true;
                //m_camera.SetEmbeddedImageInfo(embeddedInfo);
                SetAcquisitionMode("Continuous");
                ret = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        public override void Deinit(int index = 0)
        {
            try
            {
                if (m_Camera != null)
                {
                    m_Camera.DeInit();
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        public override float GetCameraTemperature()
        {
            float rect = -1f;
            try
            {

            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }

        /// <summary>
        /// AcquisitionMode: Continuous/SingleFrame/MutipleFrame
        /// </summary>
        /// <param name="strMode"></param>
        public override bool SetAcquisitionMode(string strMode)
        {
            bool ret = false;
            try
            {
                IEnum iAcquisitionMode = m_NodeMap.GetNode<IEnum>("AcquisitionMode");
                if (iAcquisitionMode == null || !iAcquisitionMode.IsWritable)
                {                  
                    return ret;
                }

                IEnumEntry iAcquisitionModeContinuous;
                switch (strMode)
                {
                    case "Continuous":
                        // Retrieve entry node from enumeration node
                        iAcquisitionModeContinuous = iAcquisitionMode.GetEntryByName("Continuous");
                        if (iAcquisitionModeContinuous == null || !iAcquisitionMode.IsReadable)
                        {                         
                            return ret;
                        }
                        break;
                    case "SingleFrame":
                        iAcquisitionModeContinuous = iAcquisitionMode.GetEntryByName("SingleFrame");
                        if (iAcquisitionModeContinuous == null || !iAcquisitionMode.IsReadable)
                        {
                            return ret;
                        }
                        break;
                    case "MutipleFrame":
                        iAcquisitionModeContinuous = iAcquisitionMode.GetEntryByName("MutipleFrame");
                        if (iAcquisitionModeContinuous == null || !iAcquisitionMode.IsReadable)
                        {
                            return ret;
                        }
                        break;
                    default:
                        return ret;
                }
                // Set symbolic from entry node as new value for enumeration node
                iAcquisitionMode.Value = iAcquisitionModeContinuous.Symbolic;
                ret = true;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        public override bool SetBalanceRatio(float fRoG, float fBoG)
        {
            bool ret = false;
            try
            {
                IEnum iBalanceWhiteAuto = m_NodeMap.GetNode<IEnum>("BalanceWhiteAuto");
                if (iBalanceWhiteAuto == null || !iBalanceWhiteAuto.IsWritable)
                {
                    LogHelper.AppLoger.Error("Unable to disable automatic balance (enum retrieval). Aborting...");
                    return ret;
                }
                IEnumEntry iBalanceAutoOff = iBalanceWhiteAuto.GetEntryByName("Off");
                if (iBalanceAutoOff == null || !iBalanceAutoOff.IsReadable)
                {
                    LogHelper.AppLoger.Error("Unable to disable automatic balance (entry retrieval). Aborting...");
                    return ret;
                }
                iBalanceWhiteAuto.Value = iBalanceAutoOff.Value;
                LogHelper.AppLoger.Debug("Automatic balance disabled...");
                IEnum iBalanceSelector = m_NodeMap.GetNode<IEnum>("BalanceRatioSelector");
                IEnumEntry iBlueChannel = iBalanceSelector.GetEntryByName("Blue");
                iBalanceSelector.Value = iBlueChannel.Value;

                IFloat iBalanceRatio = m_NodeMap.GetNode<IFloat>("BalanceRatio");
                iBalanceRatio.Value = fBoG;
                LogHelper.AppLoger.DebugFormat("BalanceRatio B channel set to {0}...", iBalanceRatio.Value);

                IEnumEntry iRedChannel = iBalanceSelector.GetEntryByName("Red");
                iBalanceSelector.Value = iRedChannel.Value;
                iBalanceRatio.Value = fRoG;
                LogHelper.AppLoger.DebugFormat("BalanceRatio R channel set to {0}...", iBalanceRatio.Value);
                ret = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        public override bool SetExposureTime(float exposureTime)
        {
            bool rect = false;
            try
            {
                //
                // Turn off automatic exposure mode
                //
                // *** NOTES ***
                // Automatic exposure prevents the manual configuration of 
                // exposure time and needs to be turned off.
                //
                // *** LATER ***
                // Exposure time can be set automatically or manually as needed. 
                // This example turns automatic exposure off to set it manually 
                // and back on in order to return the camera to its default
                // state.
                //
                IEnum iExposureAuto = m_NodeMap.GetNode<IEnum>("ExposureAuto");
                if (iExposureAuto == null || !iExposureAuto.IsWritable)
                {
                    LogHelper.AppLoger.Error("Unable to disable automatic exposure (enum retrieval). Aborting...");
                    return rect;
                }

                IEnumEntry iExposureAutoOff = iExposureAuto.GetEntryByName("Off");
                if (iExposureAutoOff == null || !iExposureAutoOff.IsReadable)
                {
                    LogHelper.AppLoger.Error("Unable to disable automatic exposure (entry retrieval). Aborting...");
                    return rect;
                }

                iExposureAuto.Value = iExposureAutoOff.Value;
                LogHelper.AppLoger.Debug("Automatic exposure disabled...");

                //
                // Set exposure time manually; exposure time recorded in microseconds
                //
                // *** NOTES ***
                // The node is checked for availability and writability prior 
                // to the setting of the node. Further, it is ensured that the
                // desired exposure time does not exceed the maximum. Exposure 
                // time is counted in microseconds. This information can be 
                // found out either by retrieving the unit with the GetUnit() 
                // method or by checking SpinView.
                //                 

                IFloat iExposureTime = m_NodeMap.GetNode<IFloat>("ExposureTime");
                if (iExposureTime == null || !iExposureTime.IsWritable)
                {
                    LogHelper.AppLoger.Error("Unable to set exposure time. Aborting...");
                    return rect;
                }

                // Ensure desired exposure time does not exceed the maximum
                iExposureTime.Value = (exposureTime > iExposureTime.Max ? iExposureTime.Max : exposureTime);

                LogHelper.AppLoger.DebugFormat("Exposure time set to {0} us...", iExposureTime.Value);
                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }

        public override bool SetGain(float gainValue)
        {
            bool rect = false;
            try
            {
                IFloat iGainNode = m_NodeMap.GetNode<IFloat>("Gain");
                if (iGainNode == null || !iGainNode.IsWritable)
                {
                    LogHelper.AppLoger.Error("Unable to set gain. Aborting...");
                    return rect;
                }

                // Ensure desired exposure time does not exceed the maximum
                iGainNode.Value = (gainValue > iGainNode.Max ? iGainNode.Max : gainValue);

                LogHelper.AppLoger.DebugFormat("Gain time set to {0} us...", iGainNode.Value);
                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }

        public override void SetOneSoftwareTrigger()
        {
            ICommand iTriggerSoftware = m_NodeMap.GetNode<ICommand>("TriggerSoftware");
            if (iTriggerSoftware == null || !iTriggerSoftware.IsWritable)
            {
                LogHelper.AppLoger.Error("Unable to execute trigger. Aborting...");
                return;
            }
            iTriggerSoftware.Execute();
        }

        public override bool SetRoi(int offsetx, int offsety, int width, int height)
        {
            bool rect = false;
            IInteger iWidth = m_NodeMap.GetNode<IInteger>("Width");
            if (iWidth.Max > width)
            {
                rect = SetImageWidth(width, height);
                rect &= SetImageOffset(offsetx, offsety);
            }
            else
            {
                rect = SetImageOffset(offsetx, offsety);
                rect &= SetImageWidth(width, height);
            }
            return rect;
        }

        private bool SetImageWidth(int width, int height)
        {
            bool rect = false;
            try
            {
                //
                // Set maximum width
                //
                // *** NOTES ***
                // Other nodes, such as those corresponding to image width and
                // height, might have an increment other than 1. In these cases, 
                // it can be important to check that the desired value is a 
                // multiple of the increment. However, as these values are being 
                // set to the maximum, there is no reason to check against the 
                // increment.
                //
                IInteger iWidth = m_NodeMap.GetNode<IInteger>("Width");
                if (iWidth != null && iWidth.IsWritable)
                {
                    iWidth.Value = width;
                    LogHelper.AppLoger.DebugFormat("Width set to {0}...", width);
                }
                else
                {
                    LogHelper.AppLoger.Error("Width not available...");
                    return rect;
                }

                //
                // Set maximum height
                //
                // *** NOTES ***
                // A maximum is retrieved with the method GetMax(). A node's 
                // minimum and maximum should always be a multiple of its
                // increment.
                //
                IInteger iHeight = m_NodeMap.GetNode<IInteger>("Height");
                if (iHeight != null && iHeight.IsWritable)
                {
                    iHeight.Value = height;
                    LogHelper.AppLoger.DebugFormat("Height set to {0}......", height);
                }
                else
                {
                    LogHelper.AppLoger.Error("Height not available...");
                    return rect;
                }
                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool SetImageOffset(int offsetx, int offsety)
        {
            bool rect = false;
            try
            {
                // 
                // Apply minimum to offset X
                //
                // *** NOTES ***
                // Numeric nodes have both a minimum and maximum. A minimum is 
                // retrieved with the method GetMin(). Sometimes it can be 
                // important to check minimums to ensure that your desired value 
                // is within range.
                //
                IInteger iOffsetX = m_NodeMap.GetNode<IInteger>("OffsetX");
                if (iOffsetX != null && iOffsetX.IsWritable)
                {
                    iOffsetX.Value = offsetx;
                    LogHelper.AppLoger.DebugFormat("Offset X set to {0}...", offsetx);
                }
                else
                {
                    LogHelper.AppLoger.Error("Offset X not available...");
                    return rect;
                }

                //
                // Apply minimum to offset Y
                // 
                // *** NOTES ***
                // It is often desirable to check the increment as well. The 
                // increment is a number of which a desired value must be a 
                // multiple. Certain nodes, such as those corresponding to 
                // offsets X and Y, have an increment of 1, which basically 
                // means that any value within range is appropriate. The 
                // increment is retrieved with the method GetInc().
                //
                IInteger iOffsetY = m_NodeMap.GetNode<IInteger>("OffsetY");
                if (iOffsetY != null && iOffsetY.IsWritable)
                {
                    iOffsetY.Value = offsety;
                    LogHelper.AppLoger.DebugFormat("Offset Y set to {0}...", offsety);
                }
                else
                {
                    LogHelper.AppLoger.Error("Offset Y not available...");
                    return rect;
                }

                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        public override bool SetStreamBufferCount(int bufCount)
        {
            bool ret = false;
            try
            {
                INodeMap sNodeMap = m_Camera.GetTLStreamNodeMap();
                IInteger streamNode = sNodeMap.GetNode<IInteger>("StreamDefaultBufferCount");
                streamNode.Value = bufCount;
                ret = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        /// <summary>
        /// OldestFirst = 0,
        /// OldestFirstOverwrite = 1,
        /// NewestFirst = 2,
        /// NewestFirstOverwrite = 3,
        /// NUMSTREAMBUFFERHANDLINGMODE = 4
        /// </summary>
        /// <param name="strMode"></param>
        public override bool SetStreamBufferHandlingMode(string strMode)
        {
            bool ret = false;
            try
            {
                INodeMap sNodeMap = m_Camera.GetTLStreamNodeMap();
                IEnum iStreamBufferHandlingMode = sNodeMap.GetNode<IEnum>("StreamBufferHandlingMode");
                if (iStreamBufferHandlingMode == null || !iStreamBufferHandlingMode.IsWritable)
                {
                    return false;
                }
                IEnumEntry iMode = iStreamBufferHandlingMode.GetEntryByName(strMode);
                if (iMode == null || !iMode.IsReadable)
                {
                    return false;
                }
                iStreamBufferHandlingMode.Value = iMode.Symbolic;

                ret = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        public override bool SetTriggerMode(bool extTrigger, TriggerType chosenTrigger)
        {
            bool rect = false;
            try
            {
                //
                // Ensure trigger mode off
                //
                // *** NOTES ***
                // The trigger must be disabled in order to configure whether 
                // the source is software or hardware.
                //
                IEnum iTriggerMode = m_NodeMap.GetNode<IEnum>("TriggerMode");
                if (iTriggerMode == null || !iTriggerMode.IsWritable)
                {
                    LogHelper.AppLoger.Error("Unable to disable trigger mode (enum retrieval). Aborting...");
                    return rect;
                }
                IEnumEntry iTriggerModeOff = iTriggerMode.GetEntryByName("Off");
                if (iTriggerModeOff == null || !iTriggerModeOff.IsReadable)
                {
                    LogHelper.AppLoger.Error("Unable to disable trigger mode (entry retrieval). Aborting...");
                    return rect;
                }

                iTriggerMode.Value = iTriggerModeOff.Value;
                LogHelper.AppLoger.Debug("Trigger mode disabled...");
                //
                // Select trigger source
                //
                // *** NOTES ***
                // The trigger source must be set to hardware or software while 
                // trigger mode is off.
                //
                IEnum iTriggerSource = m_NodeMap.GetNode<IEnum>("TriggerSource");
                if (iTriggerSource == null || !iTriggerSource.IsWritable)
                {
                    LogHelper.AppLoger.Error("Unable to set trigger mode (enum retrieval). Aborting...");
                    return rect;
                }

                if (chosenTrigger == TriggerType.Software)
                {
                    // Set trigger mode to software
                    IEnumEntry iTriggerSourceSoftware = iTriggerSource.GetEntryByName("Software");
                    if (iTriggerSourceSoftware == null || !iTriggerSourceSoftware.IsReadable)
                    {
                        LogHelper.AppLoger.Error("Unable to set software trigger mode (entry retrieval). Aborting...");
                        return rect;
                    }

                    iTriggerSource.Value = iTriggerSourceSoftware.Value;

                    LogHelper.AppLoger.Debug("Trigger source set to software...");
                }
                else if (chosenTrigger == TriggerType.Hardware)
                {
                    // Set trigger mode to hardware ('Line0')
                    IEnumEntry iTriggerSourceHardware = iTriggerSource.GetEntryByName("Line0");
                    if (iTriggerSourceHardware == null || !iTriggerSourceHardware.IsReadable)
                    {
                        LogHelper.AppLoger.Error("Unable to set hardware trigger mode (entry retrieval). Aborting...");
                        return rect;
                    }

                    iTriggerSource.Value = iTriggerSourceHardware.Value;
                    LogHelper.AppLoger.Debug("Trigger source set to hardware(Line0)... ");
                }

                //
                // Turn trigger mode on
                //
                // *** LATER ***
                // Once the appropriate trigger source has been set, turn 
                // trigger mode on in order to retrieve images using the 
                // trigger.
                //
                IEnumEntry iTriggerModeOn = iTriggerMode.GetEntryByName("On");
                if (extTrigger)
                    iTriggerModeOn = iTriggerMode.GetEntryByName("On");
                else
                    iTriggerModeOn = iTriggerMode.GetEntryByName("Off");

                if (iTriggerModeOn == null || !iTriggerModeOn.IsReadable)
                {
                    LogHelper.AppLoger.Error("Unable to enable trigger mode (entry retrieval). Aborting...");
                    return rect;
                }

                iTriggerMode.Value = iTriggerModeOn.Value;

                // TODO: Blackfly and Flea3 GEV cameras need 1 second delay after trigger mode is turned on 
                LogHelper.AppLoger.Debug("Trigger mode enabled...");
                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }

        private bool m_grabImages = false;
        public override void Start()
        {
            try
            {
                //20181230
                //if (m_grabThread != null)
                //{
                //    if (m_grabThread.IsBusy)
                //    {
                //        m_grabThread.CancelAsync();
                //    }
                //}
                //Thread.Sleep(2000);
                return;
                ClearBeforeNewOne(); 
                if(!m_Camera.IsStreaming())
                m_Camera.BeginAcquisition();

                while (GrabImageBytes(PixelType.BGR8, 50) != null)
                {
                    Thread.Sleep(100);
                }

                m_grabImages = true;
                bNeedSave = true;

                StartGrabLoop();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        public override void Stop()
        {
            try
            {
                m_grabImages = false;
                bNeedSave = false;
                //m_Camera.EndAcquisition();
                //m_camera.Disconnect();
                //m_camera.DeInit();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        #endregion

        private BackgroundWorker m_grabThread;
        private void StartGrabLoop()
        {
            m_grabThread = new BackgroundWorker();
            //20181230
            m_grabThread.WorkerSupportsCancellation = true;
            m_grabThread.RunWorkerCompleted += M_grabThread_RunWorkerCompleted;
            m_grabThread.DoWork += new DoWorkEventHandler(GrabLoop);
            m_grabThread.WorkerReportsProgress = true;
            m_grabThread.RunWorkerAsync(); 
        } 

        private void M_grabThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Trace.WriteLine("Background Cancel");
                LogHelper.AppLoger.Error("Background Cancel");
            }
            else if (e.Error != null)
            {
                Trace.WriteLine("Background Error");
                LogHelper.AppLoger.Error("Background Error");
            }
            else
            {
                Trace.WriteLine("Background Compete");
                LogHelper.AppLoger.Error("Background Compete");
            }
        }

        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                while (m_grabImages)
                {
                    ///20181229，解决和全局聚焦采图冲突
                    if (IsPreMap)
                    {
                        return;
                    }
               
                    if (m_grabThread.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (!IsLiveMode)
                    {
                        if (grabIndexInOneSwath >= SwathBufferCount)
                        {
                            continue;
                        }

                        if (swathIndex >= SwathCount)
                        {
                            continue;
                        }

                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                    IManagedImage m_rawImage = m_Camera.GetNextImage();
                    if (m_grabImages == false)
                    {
                        return;
                    }
                    if (m_rawImage.ImageStatus != ImageStatus.IMAGE_NO_ERROR)
                    {
                        LogHelper.AppLoger.Error("grab img error,error:" + m_rawImage.ImageStatus);
                    }
                    else
                    {
                        grabIndexInOneSwath++;                
                        lock (this)
                        {
                            using (IManagedImage convertedImage = m_rawImage.Convert(PixelFormatEnums.BGR8))
                            {
                                if (swathIndex % 2 == 0)
                                {
                                    if (grabIndexInOneSwath <= BeginSaveIndexPos || grabIndexInOneSwath > BeginSaveIndexNeg + 1)
                                    {
                                        m_rawImage.Release();
                                        continue;
                                    }
                                }
                                if (swathIndex % 2 == 1)
                                {
                                    if (grabIndexInOneSwath <= (SwathBufferCount - BeginSaveIndexNeg - 1) ||
                                        grabIndexInOneSwath > (SwathBufferCount - BeginSaveIndexPos))
                                    {
                                        m_rawImage.Release();
                                        continue;
                                    }
                                }
                                if (SystemStatus.Instance.SaveImgFlag)
                                {
                                    SaveToBuffer(convertedImage.bitmap.Clone() as Bitmap);
                                }
                                byte[] bimg = Utility.GlobalFunction.ImageToBytes(convertedImage.bitmap);
                                ThrowImage(bimg);
                                Trace.WriteLine(string.Format("次数：{0},行号：{1}", grabIndexInOneSwath.ToString(), swathIndex.ToString()));
                            }
                            m_rawImage.Release();
                        }
                    }
                    //worker.ReportProgress(0);
                }
                //m_grabThreadExited.Set();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        #region image save method
        private Thread threadSaveImage = null;
        private AutoResetEvent evt_Save = new AutoResetEvent(false);

        private void InitSave()
        {
            if (threadSaveImage == null)
            {
                threadSaveImage = new Thread(new ThreadStart(SaveImageThreadProc));
                threadSaveImage.Name = "Thread save image";
                threadSaveImage.IsBackground = true;
                threadSaveImage.Start();
            }
        }

        private bool bNeedSave = false;
        private void SaveImageThreadProc()
        {
            while (true)
            {
                if (bNeedSave)
                {
                    SaveImages();
                    evt_Save.WaitOne();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void SaveImages()
        {
            //lock (dicImgBuffer)
            {
                DateTime starT = DateTime.Now;
                int keyid = swathIndex - 1;
                try
                {
                    if (dicImgBuffer.Count <= 0)
                        return;
                    if (IsGrabEnd)
                    {
                        //save all image
                        List<int> lstKey = dicImgBuffer.Keys.ToList();
                        for (int i = 0; i < lstKey.Count; i++)
                        {
                            SaveOneSwath(lstKey[i]);
                        }
                    }
                    else
                    {
                        if (dicImgBuffer.Keys.Count > 2)
                        {
                            List<int> keys = dicImgBuffer.Keys.ToList();
                            keyid = keys[0];
                            LogHelper.AppLoger.ErrorFormat("save index {0}", keyid);
                            SaveOneSwath(keyid);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.AppLoger.Error(ex);
                }
                finally
                {
                    TimeSpan ustT = DateTime.Now - starT;
                    if (ustT.TotalSeconds > 1)
                    {
                        LogHelper.AppLoger.DebugFormat("save image use time {0} second", ustT.TotalSeconds);
                    }
                    Thread.Sleep(1000);
                    evt_Save.Set();
                }
            }
        }

        private void SaveOneSwath(int swathid)
        {
            //lock (dicImgBuffer)
            {
                if (!dicImgBuffer.ContainsKey(swathid))
                {
                    return;
                }

                string fileExt = ".bmp";
                List<byte[]> keyvale = dicImgBuffer[swathid];
                //if (keyvale.Count < SwathBufferCount)
                //{
                //    //LogHelper.AppLoger.DebugFormat("save image error {0}", swathid);
                //    return;
                //}
                if (keyvale.Count < BeginSaveIndexNeg - BeginSaveIndexPos + 1)
                {
                    //LogHelper.AppLoger.DebugFormat("save image error {0}", swathid);
                    return;
                }
                string path = GetPath(swathid);
                string fileName;
                Bitmap tmp;
                if (swathid % 2 == 0)
                {
                    for (int i = 0; i < keyvale.Count; i++)
                    {
                        fileName = string.Format("{0}{1}", i.ToString("d3"), fileExt);
                        tmp = (Bitmap)Utility.GlobalFunction.BytesToImage(keyvale[i]);
                        tmp.Save(path + fileName);
                        Thread.Sleep(2);
                    }
                }
                else
                {
                    for (int i = 0; i < keyvale.Count; i++)
                    {
                        fileName = string.Format("{0}{1}", (keyvale.Count - 1 - i).ToString("d3"), fileExt);
                        tmp = (Bitmap)Utility.GlobalFunction.BytesToImage(keyvale[i]);
                        tmp.Save(path + fileName);
                        Thread.Sleep(2);
                    }
                }
                //remove key id
                dicImgBuffer.Remove(swathid);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect(1);
                LogHelper.AppLoger.DebugFormat("save successed.remove key:{0}", swathid);
            }
        }

        private string GetPath(int swathid)
        {
            string rect = string.Empty;
            try
            {
                //1.root path + 2.id +3.z cnt +4.swath id                
                rect = string.Format(@"{0}\{1}\{2}\{3}\",
                    SystemStatus.Instance.ImageSavePath,
                    SystemStatus.Instance.ScanID,
                    SystemStatus.Instance.ZStackID,
                    swathid.ToString("d3"));
                if (!System.IO.Directory.Exists(rect))
                    System.IO.Directory.CreateDirectory(rect);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        #endregion
    }
}
