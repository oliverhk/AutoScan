using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using MaterialSkin.Controls;
using HWCtrl;
using HOcrBarcodeInterface;
using Utility;
using CommonLibrary;

namespace MainApp.WinUI
{
    public partial class OCRLiveForm : MaterialForm
    {
        public delegate void displayImageDelegate(byte[] img);
        public displayImageDelegate displayImage = null;

        public delegate void closeDialogDelegate();
        public closeDialogDelegate closeDialog = null;

        private string omcFilePath = "OCRA_0-9.omc";
        private string ocrDecodeString;
        private string barcodeString;
        private string errMsg;

        private bool m_grabImages = false;
        private BackgroundWorker m_grabThread;

        private string[] strExclude = { "*", "/", "$", "%" };

        public string OcrDecodeString
        {
            get { return ocrDecodeString; }
            set { ocrDecodeString = value; }
        }

        public OCRLiveForm()
        {
            InitializeComponent();

            displayImage = new displayImageDelegate(DisplayImages);
            closeDialog = new closeDialogDelegate(StopGrabAndCloseDialog);
        }

        private void OCRLiveForm_Load(object sender, EventArgs e)
        {
            try
            {
                Manager.ICamCtrl_HaiKang.UpdateImage += ICamCtrl_HaiKang_UpdateImage;
                //start grab
                //Manager.ICamCtrl_HaiKang.SetExposureTime(10000);
                Manager.ICamCtrl_HaiKang.SetGain(15);
                Manager.ICamCtrl_HaiKang.SetTriggerMode(true, HWCtrl.CameraCtrl.TriggerType.Software);
                Manager.ICamCtrl_HaiKang.BeginAcquisition();

                m_grabImages = true;
                m_grabThread = new BackgroundWorker();
                m_grabThread.DoWork += new DoWorkEventHandler(GrabLoop);
                m_grabThread.WorkerReportsProgress = false;
                m_grabThread.RunWorkerAsync();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (m_grabImages)
                {
                    Manager.ICamCtrl_HaiKang.SetOneSoftwareTrigger();
                    Thread.Sleep(400);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);   
            }
        }

        private void DisplayImages(byte[] image)
        {
            try
            {
                MemoryStream ms = new MemoryStream(image);
                ms.Position = 0;
                Image img = Bitmap.FromStream(ms);
                ms.Close();

                picImage.Image = img;
                picImage.Show();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void ICamCtrl_HaiKang_UpdateImage(object img)
        {
            Invoke(displayImage, (byte[])img);

            try
            {
                if (SystemConfig.Instance.OcrBarcodeType == BarcodeType.OCR)
                {
                    OcrRead((byte[])img);
                }
                else if (SystemConfig.Instance.OcrBarcodeType == BarcodeType.Barcode)
                {
                    OcrBarcodeRead((byte[])img);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex + "\n IDoOcrandReadCode Error!");
                Manager.ICamCtrl_HaiKang.EndAcquisition();
            }
        }

        private void OCRLiveForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (Manager.ICamCtrl_HaiKang != null)
            {
                Manager.ICamCtrl_HaiKang.UpdateImage -= ICamCtrl_HaiKang_UpdateImage;
                Manager.ICamCtrl_HaiKang.EndAcquisition();
            }
        }

        private void StopGrabAndCloseDialog()
        {
            m_grabImages = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OcrBarcodeRead(byte[] img)
        {
            Bitmap image = (Bitmap)Utility.GlobalFunction.BytesToImage(img);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData srcBmpData = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            image.UnlockBits(srcBmpData);

            ocrDecodeString = "";
            barcodeString = "";
            omcFilePath = "Industrial_NoRej.omc";
            int ret = DoDecoding.Instance.IDoOcrandReadCode(
                srcBmpData.Scan0,
                image.Width,
                image.Height,
                0,   //OCR only
                SystemConfig.Instance.OCR_DbRow,
                SystemConfig.Instance.OCR_DbCol,
                SystemConfig.Instance.OCR_DbPhi,
                SystemConfig.Instance.OCR_DbLen1,
                SystemConfig.Instance.OCR_DbLen2,
                SystemConfig.Instance.OCR_FontWidth,
                SystemConfig.Instance.OCR_FontHeight,
                SystemConfig.Instance.OCR_StrokeWidth,
                1,
                omcFilePath,
                0,
                ref ocrDecodeString,
                ref barcodeString,
                ref errMsg
                );
            if (ret == 0 && ocrDecodeString != "" && ocrDecodeString.Length == 9)
            {
                foreach(string ch in strExclude)
                {
                    if(ocrDecodeString.Contains(ch))
                    {
                        return;
                    }
                }
                //识别成功
                //sw.Stop();
                //TimeSpan ts2 = sw.Elapsed;
                //string info = "识别总共花费" + ts2.TotalMilliseconds.ToString() + "ms.";
                //LogHelper.AppLoger.Info(info);

                ocrDecodeString = barcodeString;
                Manager.ICamCtrl_HaiKang.EndAcquisition();
                BeginInvoke(closeDialog);
            }
        }

        private void OcrRead(byte[] img)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            Bitmap image = (Bitmap)Utility.GlobalFunction.BytesToImage(img);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData srcBmpData = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            image.UnlockBits(srcBmpData);

            ocrDecodeString = "";
            int ret = DoDecoding.Instance.IDoOcrandReadCode(
                srcBmpData.Scan0,
                image.Width,
                image.Height,
                1,   //OCR only
                SystemConfig.Instance.OCR_DbRow,
                SystemConfig.Instance.OCR_DbCol,
                SystemConfig.Instance.OCR_DbPhi,
                SystemConfig.Instance.OCR_DbLen1,
                SystemConfig.Instance.OCR_DbLen2,
                SystemConfig.Instance.OCR_FontWidth,
                SystemConfig.Instance.OCR_FontHeight,
                SystemConfig.Instance.OCR_StrokeWidth,
                2,
                omcFilePath,
                0,
                ref ocrDecodeString,
                ref barcodeString,
                ref errMsg
                );
            if (ret == 0 && ocrDecodeString != "" && ocrDecodeString.Length == 14)
            {
                //识别成功
                //sw.Stop();
                //TimeSpan ts2 = sw.Elapsed;
                //string info = "识别总共花费" + ts2.TotalMilliseconds.ToString() + "ms.";
                //LogHelper.AppLoger.Info(info);
                Manager.ICamCtrl_HaiKang.EndAcquisition();
                BeginInvoke(closeDialog);
            }
            else
            {
                //识别失败，尝试反向识别
                ret = DoDecoding.Instance.IDoOcrandReadCode(
                    srcBmpData.Scan0,
                    image.Width,
                    image.Height,
                    1,   //OCR only
                    SystemConfig.Instance.OCR_DbRow,
                    SystemConfig.Instance.OCR_DbCol,
                    -(SystemConfig.Instance.OCR_DbPhi),
                    SystemConfig.Instance.OCR_DbLen1,
                    SystemConfig.Instance.OCR_DbLen2,
                    SystemConfig.Instance.OCR_FontWidth,
                    SystemConfig.Instance.OCR_FontHeight,
                    SystemConfig.Instance.OCR_StrokeWidth,
                    2,
                    omcFilePath,
                    0,
                    ref ocrDecodeString,
                    ref barcodeString,
                    ref errMsg
                    );
                if (ret == 0 && ocrDecodeString != "" && ocrDecodeString.Length == 14)
                {
                    Manager.ICamCtrl_HaiKang.EndAcquisition();
                    BeginInvoke(closeDialog);
                }
            }
        }

        private void PrepareRect(object sender, PaintEventArgs e)
        {
            //Draw rect
            Graphics g;
            g = picImage.CreateGraphics();
            Color color = Color.FromArgb(255, 0, 0);
            Pen pen = new Pen(color, 2);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 5, 5 };

            int width = picImage.Width;
            int height = picImage.Height;
            Rectangle rect = new Rectangle(50, 50, width - 100, height - 100);
            g.DrawRectangle(pen, rect);
            picImage.Refresh();
        }
    }
}
