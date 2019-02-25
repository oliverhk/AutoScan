using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using HWCtrl.CameraCtrl;
using MainApp.WinUC;
using DBCtrl.DAO;
using DBCtrl.Model;

namespace MainApp.WinUI
{
    public partial class MaintainForm : Form
    {
        #region 复查测试
        private bool grabFocusImages = false;
        private BackgroundWorker m_grabThread;
        private RoiPanel roiPanel;
        private AutoResetEvent grabThreadQuitEvent = new AutoResetEvent(false);
        private readonly Rectangle RoiRect = new Rectangle(768, 512, 512, 512);

        private void FakeCells()
        {
            roiPanel = new RoiPanel();
            roiPanel.Parent = pbReview;
            roiPanel.Location = new Point((pbReview.Width - roiPanel.Width) / 2,
                (pbReview.Height - roiPanel.Height) / 2);
            roiPanel.Visible = true;

            int[] x = new int[] { -6028, -7587, -7744, -5335, -6270, -4395, -8667, -11762, -11812, -15549 };
            int[] y = new int[] { 3665, -135, 2595, -1051, -127, -794, -982, -941, 2920, 2940 };
            //int[] x = new int[] { -7744 };
            //int[] y = new int[] { 2595 };
            int index = 0;
            for (int i = 0; i < x.Length; i++)
            {
                index = dgvCells.Rows.Add();
                dgvCells.Rows[index].Cells[0].Value = x[i];
                dgvCells.Rows[index].Cells[1].Value = y[i];
            }
        }

        public DisplayHandler updateFocusImg;
        private void UpdateFocusImgProc(Bitmap obj)
        {
            pbReview.Image = obj;
            Application.DoEvents();
        }

        private void StartGrabLoop()
        {
            grabFocusImages = true;
            m_grabThread = new BackgroundWorker();
            m_grabThread.DoWork += new DoWorkEventHandler(GrabLoop);
            m_grabThread.RunWorkerAsync();
        }

        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            try
            {
                Bitmap image = null;
                BackgroundWorker worker = sender as BackgroundWorker;
                while (grabFocusImages)
                {
                    Manager.ICamCtrl.SetOneSoftwareTrigger();
                    byte[] bytImg = Manager.ICamCtrl.GrabImageBytes(PixelType.Mono8);
                    image = (Bitmap)Utility.GlobalFunction.BytesToImage(bytImg);
                    pbReview.Invoke(updateFocusImg, image);
                    Thread.Sleep(40);
                }
                grabThreadQuitEvent.Set();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void btnSnapStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (grabFocusImages == true)
                {
                    return;
                }

                LoadAIResult();

                //init + set trigger mode
                if (Manager.ICamCtrl.Init() == false)
                {
                    MessageBox.Show("Camera Init Fail!");
                    return;
                }
                if (Manager.ICamCtrl.SetTriggerMode(true, TriggerType.Software) == false)
                {
                    MessageBox.Show("Camera SetTriggerMode Fail!");
                    Manager.ICamCtrl.Deinit();
                    return;
                }
                Manager.ICamCtrl.SetExposureTime(10);
                Manager.ICamCtrl.SetRoi(RoiRect.X, RoiRect.Y, RoiRect.Width, RoiRect.Height);
                //Manager.ICamCtrl.SetStreamBufferHandlingMode("NewestFirst");

                Manager.ICamCtrl.BeginAcquisition();
                StartGrabLoop();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void btnSnapStop_Click(object sender, EventArgs e)
        {
            if(grabFocusImages)
            {
                grabFocusImages = false;
                Manager.ICamCtrl.EndAcquisition();
                Manager.ICamCtrl.Deinit();
            }
        }

        private void UpdateFocusImage(object img)
        {
            dgvCells.ClearSelection();
            dgvCells.Rows[Manager.reviewCtrl.ReviewedCellCount - 1].Selected = true;
            Bitmap bitmap = (Bitmap)img;
            UpdateFocusImgProc(bitmap);
        }

        IList<CellAIResult> aiResultList = new List<CellAIResult>();
        private void LoadAIResult()
        {
            aiResultList.Clear();
            CellAIResultDao dao = new CellAIResultDao();
            aiResultList = dao.GetList();           
            dgvCells.DataSource = aiResultList;
            for (int i = 0; i < dgvCells.Columns.Count; i++)
            {
                dgvCells.Columns[i].Frozen = false;
            }
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            try
            {
                grabFocusImages = false;
                grabThreadQuitEvent.WaitOne(1000);
                grabThreadQuitEvent.Reset();

                //generate cells list to review
                //List<PointF> fakeList = new List<PointF>();
                //for (int i = 0; i < dgvCells.RowCount - 1; i++)
                //{
                //    fakeList.Add(new PointF((float)Convert.ToDouble(dgvCells.Rows[i].Cells[0].Value),
                //        (float)Convert.ToDouble(dgvCells.Rows[i].Cells[1].Value)));
                //}
                
                Manager.reviewCtrl.SuspiciousCellList = aiResultList;
                Manager.reviewCtrl.FocusStackSaveEnable = cbFocusStackSave.Checked;

                //ROI
                Manager.reviewCtrl.FocusManager.RoiRect = new Rectangle(0, 0, 512, 512);

                Manager.reviewCtrl.FocusManager.ImageSaveEnable = cbImageSave.Checked;
                Manager.reviewCtrl.FocusManager.StartPosition = (float)Convert.ToDouble(txtFocusStartPos.Text);
                Manager.reviewCtrl.FocusManager.MoveDistace = (float)Convert.ToDouble(txtFocusMoveDis.Text);
                Manager.reviewCtrl.FocusManager.MoveSpeed = (float)Convert.ToDouble(txtFocusZSpeed.Text);
                Manager.reviewCtrl.FocusManager.ImagesToGrab = Convert.ToInt32(txtFocusImageCount.Text);

                Manager.reviewCtrl.StartReview();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Do review error!\n" + ex.Message);
            }
        }

        private void cbRoiShow_CheckedChanged(object sender, EventArgs e)
        {
            roiPanel.Visible = cbRoiShow.Checked;
        }

        //stress test
        private void StartReviewAgain(bool reviewSuccess)
        {
            //start test again
            Thread.Sleep(1000);
            //btnReview.PerformClick();
        }

        private void btnStopReview_Click(object sender, EventArgs e)
        {
            Manager.reviewCtrl.StopReview();
        }
        #endregion
    }
}