using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using HWCtrl.CameraCtrl;
using HalconHelper;

namespace MainApp
{
    //默认使用全局聚焦
    public class AutoFocusManager
    {
        #region instance
        //Singleton instance
        private static AutoFocusManager _instance;
        public static AutoFocusManager Instance => _instance ?? (_instance = new AutoFocusManager());
        #endregion

        private float focusZPos = -1f;
        /// <summary>
        /// 焦平面Z轴位置
        /// </summary>
        public float FocusZPos
        {
            get
            {
                return focusZPos;
            }
        }

        private int imagesToGrab = 10;
        /// <summary>
        /// 全局聚焦采图数量
        /// </summary>
        public int ImagesToGrab
        {
            get
            {
                return imagesToGrab;
            }
            set
            {
                imagesToGrab = value;
            }
        }

        private float startPosition = 0f;
        /// <summary>
        /// 运动起始位置(单位:微米)
        /// </summary>
        public float StartPosition
        {
            get
            {
                return startPosition;
            }
            set
            {
                startPosition = value;
            }
        }

        private float moveDistance = 200f;
        /// <summary>
        /// 镜头移动距离(单位:微米)
        /// </summary>
        public float MoveDistace
        {
            get
            {
                return moveDistance;
            }
            set
            {
                moveDistance = value;
            }
        }

        private float moveSpeed = 400;
        /// <summary>
        /// Z轴运动速度(单位：微米/秒)
        /// </summary>
        public float MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            set
            {
                moveSpeed = value;
            }
        }

        private Rectangle roiRect;
        public Rectangle RoiRect
        {
            get
            {
                return roiRect;
            }
            set
            {
                roiRect = value;
            }
        }

        public bool ImageSaveEnable { get; set; } = false;

        public delegate void FocusCompleteHandler(bool focusSuccess);
        public event FocusCompleteHandler FocusComplete;
        private void NotifyFocusComplete(bool focusSuccess)
        {
            if (FocusComplete != null)
            {
                foreach (FocusCompleteHandler t in FocusComplete.GetInvocationList())
                {
                    t.BeginInvoke(focusSuccess, null, null);
                }
            }
        }

        private BackgroundWorker bgFocusWorker = null;
        public AutoFocusManager()
        {
            bgFocusWorker = new BackgroundWorker();
            bgFocusWorker.DoWork += worker_DoFocus;
            bgFocusWorker.WorkerSupportsCancellation = false;
            bgFocusWorker.WorkerReportsProgress = false;
        }

        public bool DoFocus()
        {
            if (!bgFocusWorker.IsBusy)
            {
                bgFocusWorker.RunWorkerAsync();
                return true;
            }
            else
            {
                MessageBox.Show("Focus already running!");
                return false;
            }
        }

        private Bitmap CropImage(Bitmap orgImg)
        {
            Bitmap rect = new Bitmap(roiRect.Width, roiRect.Height);
            try
            {
                if (orgImg == null)
                    return null;

                Rectangle cropArea = new Rectangle();
                cropArea.X = roiRect.X;
                cropArea.Y = roiRect.Y;
                cropArea.Width = roiRect.Width > orgImg.Width ? orgImg.Width : roiRect.Width;
                cropArea.Height = roiRect.Height > orgImg.Height ? orgImg.Height : roiRect.Height;
                rect = new Bitmap(cropArea.Width, cropArea.Height);
                rect = orgImg.Clone(cropArea, orgImg.PixelFormat);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return rect;
        }

        private void worker_DoFocus(object sender, DoWorkEventArgs e)
        {
            try
            {
                float[] zPos = new float[imagesToGrab];
                Bitmap[] imgBmp = new Bitmap[imagesToGrab];
                IntPtr[] imgPtr = new IntPtr[imagesToGrab];
                Stopwatch stopWatch = new Stopwatch();
                //1.设置运动参数

                stopWatch.Start();
                //2.move
                Manager.IMotorCtrl.SetSpeed(3, 10);
                Manager.IMotorCtrl.MoveWait(3, startPosition / 1000);
                //Thread.Sleep(200);
                Trace.WriteLine(string.Format("1.1 => Move to start Z position OK({0}ms)", stopWatch.ElapsedMilliseconds));

                Manager.IMotorCtrl.SetSpeed(3, moveSpeed / 1000);
                Manager.IMotorCtrl.Move(3, (startPosition + moveDistance) / 1000);

                stopWatch.Restart();
                //3.询问位置 + 图片数据保存
                float curPos = 0f;
                Thread.Sleep(150);
                for (int i = 0; i < imagesToGrab; i++)
                {
                    curPos = Manager.IMotorCtrl.GetAxisZPosUmEx();
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    byte[] bytImg = Manager.ICamCtrl.GrabImageBytes(PixelType.Mono8);
                    imgBmp[i] = (Bitmap)Utility.GlobalFunction.BytesToImage(bytImg);
                    zPos[i] = curPos;

                    //if (i != 0 && i != imagesToGrab - 1)
                    //{
                    //    if (zPos[i] == -0.1f)
                    //    {
                    //        zPos[i] = (zPos[i - 1] + zPos[i + 1])/2;
                    //    }
                    //}

                    Trace.WriteLine(string.Format("{0}_{1}", i, curPos));
                    Thread.Sleep(15);
                }
                Trace.WriteLine(string.Format("1.2 => Grab Images({0}ms)", stopWatch.ElapsedMilliseconds));

                stopWatch.Restart();
                for (int i = 0; i < imagesToGrab; i++)
                {
                    if (i != 0 && i != imagesToGrab - 1)
                    {
                        if (zPos[i] == -0.1f)
                        {
                            zPos[i] = (zPos[i - 1] + zPos[i + 1]) / 2;
                        }
                    }
                }
                //4.计算焦点位置
                Bitmap[] roiBmp = new Bitmap[imagesToGrab];
                BitmapData[] srcBmpData = new BitmapData[imagesToGrab];
                Rectangle rect = new Rectangle(0, 0, roiRect.Width, roiRect.Height);
                for (int i = 0; i < imagesToGrab; i++)
                {
                    roiBmp[i] = CropImage(imgBmp[i]);
                    srcBmpData[i] = roiBmp[i].LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    imgPtr[i] = srcBmpData[i].Scan0;
                }
                focusZPos = FocusHelper.GlobalFocus(imgPtr, zPos, imagesToGrab, roiRect.Width, roiRect.Height);
                for (int i = 0; i < imagesToGrab; i++)
                {
                    roiBmp[i].UnlockBits(srcBmpData[i]);
                }

                Trace.WriteLine(string.Format("1.3 => Focus Z pos: {0} ({1})", FocusZPos.ToString(), stopWatch.ElapsedMilliseconds));

                //stopWatch.Restart();
                //5.运动到焦平面
                if (/*focusZPos < 0 || */float.IsNaN(focusZPos))
                {
                    MessageBox.Show("GlobalFocus Error!");
                    NotifyFocusComplete(false);
                    return;
                }
                else
                {
                    Manager.IMotorCtrl.SetSpeed(3, 10);
                    //Manager.IMotorCtrl.MoveWait(3, focusZPos / 1000); //no need?
                    NotifyFocusComplete(true);
                }
                //Trace.WriteLine(string.Format("1.4 => Move to Focus Z pos OK({0}ms)", stopWatch.ElapsedMilliseconds));

                //6.保存图片到本地(optional)
                //if (ImageSaveEnable)
                //{
                //    for (int i = 0; i < imagesToGrab; i++)
                //    {
                //        roiBmp[i].Save(@"C:\Users\ruiqian\Desktop\pic\"+string.Format("{0}_{1}.bmp", i, zPos[i]), ImageFormat.Bmp);
                //    }
                //}

                stopWatch.Stop();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Focus Error Happen! " + ex.Message);
            }
        }
    }
}
