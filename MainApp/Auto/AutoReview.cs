using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
//using Gif.Components;
using HWCtrl.CameraCtrl;
using HalconHelper;
using DBCtrl.Model;
using Utility;

namespace MainApp
{
    public class AutoReviewer
    {
        #region instance
        //Singleton instance
        private static AutoReviewer _instance;
        public static AutoReviewer Instance => _instance ?? (_instance = new AutoReviewer());
        #endregion

        public AutoFocusManager FocusManager
        {
            get
            {
                return focusManager;
            }
        }

        private IList<CellAIResult> lstSuspiciousCell = null;
        public IList<CellAIResult> SuspiciousCellList
        {
            get
            {
                return lstSuspiciousCell;
            }
            set
            {
                if (bgWorker != null && bgWorker.IsBusy)
                {
                    throw new InvalidOperationException("Can not update cell list while doing review!");
                }
                else
                {
                    lstSuspiciousCell = value;
                    reviewedCellCount = 0;
                }
            }
        }

        private int reviewedCellCount = 0;
        /// <summary>
        /// 已进行复查的细胞数量
        /// </summary>
        public int ReviewedCellCount
        {
            get
            {
                return reviewedCellCount;
            }
        }

        private PointF sliceLocation = new PointF(0f, 0f);
        /// <summary>
        /// 切片左上角相对于运动平台原点的坐标
        /// </summary>
        public PointF SliceLocation
        {
            get
            {
                return sliceLocation;
            }
            set
            {
                sliceLocation = value;
            }
        }

        private PointF calibrateLocation = new PointF(0f, 0f);
        /// <summary>
        /// 切片上的标定位置相对于切片左上角的坐标
        /// </summary>
        public PointF CalibrateLocation
        {
            get
            {
                return calibrateLocation;
            }
            set
            {
                calibrateLocation = value;
            }
        }

        private PointF scanStartLocation = new PointF(0f, 0f);
        /// <summary>
        /// 扫描起始位置相对于标定位置的坐标
        /// </summary>
        public PointF ScanStartLocation
        {
            get
            {
                return scanStartLocation;
            }
            set
            {
                scanStartLocation = value;
            }
        }

        public bool FocusStackSaveEnable { get; set; } = false;

        public delegate void UpdateImageHandler(object img);
        public event UpdateImageHandler UpdateImage;
        private void ThrowImage(object img)
        {
            if (UpdateImage != null)
            {
                foreach (UpdateImageHandler t in UpdateImage.GetInvocationList())
                {
                    t.BeginInvoke(img, null, null);
                    //t.Invoke(img);
                }
            }
        }

        private AutoFocusManager focusManager = null;
        private BackgroundWorker bgWorker = null;
        private AutoResetEvent focusCompleteEvent = null;
        public AutoReviewer()
        {
            focusManager = AutoFocusManager.Instance;
            focusManager.FocusComplete += FocusCompleteHandler;

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += worker_DoReview;
            bgWorker.RunWorkerCompleted += worker_ReviewComplete;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;

            focusCompleteEvent = new AutoResetEvent(false);
        }

        private void FocusCompleteHandler(bool focusSuccess)
        {
            if (focusSuccess)
            {
                //
            }
            else
            {
                Trace.WriteLine("全局聚焦失败！");
            }
            focusCompleteEvent.Set();
        }

        private PointF GetCellLocationStage(int cellIndex)
        {
            PointF res = new PointF();
            if (cellIndex < lstSuspiciousCell.Count)
            {
                //res.X = lstSuspiciousCell[cellIndex].X + scanStartLocation.X + calibrateLocation.X + sliceLocation.X;
                //res.Y = lstSuspiciousCell[cellIndex].Y + scanStartLocation.Y + calibrateLocation.Y + sliceLocation.Y;

                //
                string imageName = lstSuspiciousCell[cellIndex].ImageName;
                int col = Convert.ToInt32(imageName.Substring(imageName.Length - 3, 3));
                int row = Convert.ToInt32(imageName.Substring(imageName.Length - 7, 3));
                res.X = (float)((21992 - 651 * (col + 53)) + (1024 - lstSuspiciousCell[cellIndex].PixelX) * 0.345);
                res.Y = (float)((-10000 + 460 * row) + (lstSuspiciousCell[cellIndex].PixelY - 768) * 0.345);

                //运动偏移补偿
                res.X = (float)(res.X - 195 * 0.345);

                //10x => 20x 补偿
                res.X = (float)(res.X + 790 * 0.345);
                res.Y = (float)(res.Y - 790 * 0.345);
            }
            else
            {
                MessageBox.Show("CellIndex Error!");
            }
            return res;
        }

        private void worker_DoReview(object sender, DoWorkEventArgs e)
        {
            if (!Directory.Exists(focusImagePath))
            {
                Directory.CreateDirectory(focusImagePath);
            }

            int cellNum = lstSuspiciousCell.Count;
            PointF cellPos;
            Stopwatch stopWatch = new Stopwatch();
            lock (lstSuspiciousCell)
            {
                for (int i = reviewedCellCount; i < cellNum; i++)
                {
                    if(((BackgroundWorker)sender).CancellationPending)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Review thread exit!");
                        return;
                    }

                    //1.get stage XY of a cell
                    cellPos = GetCellLocationStage(i);

                    for (int j = 0; j < 2; j++)
                    {
                        Manager.ICamCtrl.GrabImageBytes(PixelType.Mono8, 50); //clear image buffer
                    }

                    //2.move
                    Manager.IMotorCtrl.MoveWait(1, cellPos.X / 1000);
                    Manager.IMotorCtrl.MoveWait(2, cellPos.Y / 1000);
                    Trace.WriteLine(string.Format("cell{0} in position => Do Focus!", i));

                    stopWatch.Start();
                    //3.do focus
                    focusManager.DoFocus();
                    focusCompleteEvent.WaitOne();
                    Trace.WriteLine(string.Format("1:Focus => {0}ms", stopWatch.ElapsedMilliseconds));

                    reviewedCellCount++;
                    //4.grab focused image and throw out
                    //Bitmap focusedImage = null;
                    //Manager.ICamCtrl.SetOneSoftwareTrigger();
                    //byte[] bytImg = Manager.ICamCtrl.GrabImageBytes(PixelType.BGR8);
                    //focusedImage = (Bitmap)Utility.GlobalFunction.BytesToImage(bytImg);
                    //focusedImage.Save(string.Format("{0}_{1}_focus.bmp", i, focusManager.FocusZPos), ImageFormat.Bmp);
                    //ThrowImage(focusedImage);

                    stopWatch.Restart();
                    //get focus stack
                    if (SnapFuseImages(i) == true)
                    {
                        string focusImageSubPath = focusImagePath + "\\" + lstSuspiciousCell[i].ImageName; ;
                        string imgSaveName = string.Format("{0}_{1}.bmp",
                            lstSuspiciousCell[i].PixelX, lstSuspiciousCell[i].PixelY);
                        ThrowImage(new Bitmap(focusImageSubPath + "\\" + imgSaveName));
                    }
                    Trace.WriteLine(string.Format("2:Depth From Focus => {0}ms", stopWatch.ElapsedMilliseconds));

                    bgWorker.ReportProgress(reviewedCellCount / cellNum);
                }
            }
            stopWatch.Stop();
        }

        /// <summary>
        /// 把directory文件夹里的bmp文件生成为gif文件，放在giffile
        /// </summary>
        /// <param name="gifSavePath">gif保存路径</param>
        /// <param name="time">每帧的时间/ms</param>
        /// <param name="repeat">是否重复</param>
        //public void BmpsToGif(Bitmap[] images, string gifSavePath, int time, bool repeat)
        //{
        //    string gifSaveFile = string.Format("{0}\\result.gif", gifSavePath);
        //    AnimatedGifEncoder e = new AnimatedGifEncoder();
        //    e.Start(gifSaveFile);

        //    //每帧播放时间
        //    e.SetDelay(time);

        //    //-1：不重复，0：重复
        //    e.SetRepeat(repeat ? 0 : -1);
        //    for (int i = 0; i < images.Length; i++)
        //    {
        //        e.AddFrame(images[i]);
        //    }
        //    e.Finish();
        //}

        private string focusImagePath = Application.StartupPath + "\\FocusStack";
        private bool SnapFuseImages(int cellIndex)
        {
            //
            bool ret = false;
            string focusImageSubPath = focusImagePath + "\\" + lstSuspiciousCell[cellIndex].ImageName;
            string imgSaveName = string.Format("{0}_{1}.bmp", 
                lstSuspiciousCell[cellIndex].PixelX, lstSuspiciousCell[cellIndex].PixelY);
            if (!Directory.Exists(focusImageSubPath))
            {
                //DirectoryInfo dirInfo = new DirectoryInfo(focusImageSubPath);
                //dirInfo.Delete(true);
                Directory.CreateDirectory(focusImageSubPath);
            }
            //Directory.CreateDirectory(focusImageSubPath);
            //Trace.WriteLine(focusImageSubPath);

            if (!FocusStackSaveEnable)
            {
                int imgCount = 7;
                Rectangle rect = new Rectangle(0, 0, 512, 512);
                IntPtr[] imgPtr = new IntPtr[imgCount];
                BitmapData[] srcBmpData = new BitmapData[imgCount];
                Bitmap[] images = new Bitmap[imgCount + 1];
                for (int i = -3; i <= 3; i++)
                {
                    Manager.IMotorCtrl.MoveWait(3, (focusManager.FocusZPos + i) / 1000);
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    byte[] bytImg = Manager.ICamCtrl.GrabImageBytes(PixelType.BGR8);
                    images[i + 3] = (Bitmap)Utility.GlobalFunction.BytesToImage(bytImg);
                    ThrowImage(images[i + 3]);
                    srcBmpData[i + 3] = images[i + 3].LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                    imgPtr[i + 3] = srcBmpData[i + 3].Scan0;
                    images[i + 3].UnlockBits(srcBmpData[i + 3]);
                }
                ret = FocusHelper.DoDepthFromFocus(imgPtr, imgCount, images[0].Width, images[0].Height, focusImageSubPath, imgSaveName);

                //
                //Stopwatch watch = new Stopwatch();
                //watch.Start();
                //images[imgCount] = new Bitmap(focusImageSubPath + "\\SharpImage.bmp");
                //BmpsToGif(images, focusImageSubPath, 500, true);
                //watch.Stop();
                //Trace.WriteLine(string.Format("bmp=>gif: {0} ms", watch.Elapsed.TotalMilliseconds));

                for (int i = 0; i < imgCount; i++)
                {
                    //images[i].UnlockBits(srcBmpData[i]);
                    images[i].Dispose();
                }
            }
            else
            {
                for (int i = -3; i <= 3; i++)
                {
                    Manager.IMotorCtrl.MoveWait(3, (focusManager.FocusZPos + i) / 1000);
                    //Thread.Sleep(250);
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    byte[] bytImg = Manager.ICamCtrl.GrabImageBytes(PixelType.BGR8);
                    Bitmap focusedImage = (Bitmap)Utility.GlobalFunction.BytesToImage(bytImg);
                    focusedImage.Save(string.Format("{0}\\{1}_{2}.bmp", focusImageSubPath, i, focusManager.FocusZPos + i),
                        ImageFormat.Bmp);
                }

                //do depth from focus
                ret = FocusHelper.DoDepthFromFocus(focusImageSubPath, imgSaveName);
            }

            return ret;
        }

        private void worker_ReviewComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Trace.WriteLine("Review Cancelled!");
            }
            else
            {
                Trace.WriteLine("Review Complete!");
            }
            NotifyReviewComplete(!e.Cancelled);
        }

        public void StartReview()
        {
            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
        }

        public void StopReview()
        {
            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
            }
        }

        //stress test
        public delegate void ReviewCompleteHandler(bool reviewSuccess);
        public event ReviewCompleteHandler ReviewComplete;
        private void NotifyReviewComplete(bool reviewSuccess)
        {
            if (ReviewComplete != null)
            {
                foreach (ReviewCompleteHandler t in ReviewComplete.GetInvocationList())
                {
                    t.BeginInvoke(reviewSuccess, null, null);
                }
            }
        }
    }
}
