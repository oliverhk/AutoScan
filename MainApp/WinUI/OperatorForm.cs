using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;
using Utility;
using DBCtrl.Model;
using System.Threading;

namespace MainApp.WinUI
{
    public partial class OperatorForm : Form
    {
        #region property
        private const int MAX_MESSAGE_COUNT = 100;
        private List<MessageBody> lstMsgBody = new List<MessageBody>();
        private DateTime sysStartTime;
        private DateTime oneScanTime;        
        private long SCAN_TOTAL_CNT = 0;
        private bool IsShowScanTime = false;
        private List<Jobs> lstJob = new List<Jobs>();
        private const int MAX_JOB_CNT = 50;
        private SystemConfig sysCfg = SystemConfig.Instance;

        private static OCRLiveForm frmOCRLive;

        #endregion
        #region instance
        //Singleton instance
        private static OperatorForm _instance;
        public static OperatorForm Instance => _instance ?? (_instance = new OperatorForm());
        #endregion
        public OperatorForm()
        {
            InitializeComponent();
            frmOCRLive = new OCRLiveForm();
        }

        private void OperatorForm_Load(object sender, EventArgs e)
        {
            //form load
            InitSys();
            this.kpImgView.ImagePath = @"c:\imgs\data.bmp";
        }
        #region method
        private void InitSys()
        {
            try
            {
                Manager.IAutoCtrl.UpdateMessage += IAutoCtl_UpdateMessage;
                Manager.IAutoCtrl.UpdateStat += IAutoCtl_UpdateStat;
                Manager.UpdateMessage += IAutoCtl_UpdateMessage;
                                
                Manager.ICamCtrl.UpdateImage += CameraCtrl_UpdateImage;
                Manager.IAutoCtrl.UpdateJob += IAutoCtrl_UpdateJob;
                updateImg = this.UpdateImgMethod;
                updateRlt = this.UpdateRltMethod;
                dgvResult.AutoGenerateColumns = false;
                InitalMessage();
                CheckStatEnable();
                sysStartTime = DateTime.Now;

                //StartCheckIO();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void IAutoCtrl_UpdateJob(Jobs job)
        {
            AddJob(job);
            DisplyResult();
        }

        long gIndex = 0;
        //private Bitmap curBitdata;
        private void CameraCtrl_UpdateImage(object img)
        {
            //curBitdata = (Bitmap)img;
            //for save img
            //int id = 0;
            //string path = string.Format("{0}\\{1}\\", @"c:\image", id.ToString("d3"));
            //string file = string.Format("{0}_{1}.{2}", id.ToString("d3"), DateTime.Now.ToString("yyyyMMdd_HHmmss_ff"), "bmp");
            //curBitdata.Save(path + file);
            if (Manager.ICamCtrl.IsLiveMode)
            {
                byte[] t = (byte[])img;
                Bitmap bit = (Bitmap)Utility.GlobalFunction.BytesToImage(t);
                DisplayImage(bit);
            }
            else
            {
                if (gIndex % 3 == 0)
                {
                    byte[] t = (byte[])img;
                    Bitmap bit = (Bitmap)Utility.GlobalFunction.BytesToImage(t);
                    DisplayImage(bit);
                }
            }
            gIndex++;
            if (gIndex > long.MaxValue - 1)
                gIndex = 0;
        }

        private void IAutoCtl_UpdateStat(EnumStat stat)
        {
            EvtStat(stat);
        }

        private void IAutoCtl_UpdateMessage(MessageBody message)
        {
            UpdateMessageBody(message);
        }

        private void CheckStatEnable()
        {
            if(ConstDef.IsScan)
            {
                btnSelect.Enabled = false;
                btnScan.Enabled = false;
                btnStop.Enabled = true;
                btnAbortStop.Enabled = true;
                btnInit.Enabled = false;

            }
            else
            {
                btnSelect.Enabled = true;
                btnScan.Enabled = true;
                btnStop.Enabled = false;
                btnAbortStop.Enabled = false;
                btnInit.Enabled = true;
            }
        }
        private void EvtStat(EnumStat stat)
        {
            try
            {
                if (stat == EnumStat.StartPos)
                {
                    oneScanTime = DateTime.Now;
                    IsShowScanTime = true;
                    this.Invoke(new Action(delegate ()
                    {
                        chkStartPos.Checked = true;
                        chkStopPos.Checked = false;
                        chkSwathStatPos.Checked = false;
                        chkSwathEndPos.Checked = false;
                    }));
                }else if(stat == EnumStat.HomePos)
                {
                    this.Invoke(new Action(delegate ()
                    {
                        chkStartPos.Checked = false;
                        chkStopPos.Checked = true;
                        chkSwathStatPos.Checked = false;
                        chkSwathEndPos.Checked = false;
                    }));
                }else if(stat == EnumStat.SwathStart)
                {
                    this.Invoke(new Action(delegate ()
                    {
                        chkStartPos.Checked = false;
                        chkStopPos.Checked = false;
                        chkSwathStatPos.Checked = true;
                        chkSwathEndPos.Checked = false;
                    }));
                }else if(stat == EnumStat.SwathEnd)
                {                    
                    this.Invoke(new Action(delegate ()
                    {
                        chkStartPos.Checked = false;
                        chkStopPos.Checked = false;
                        chkSwathStatPos.Checked = false;
                        chkSwathEndPos.Checked = true;
                    }));
                }else if(stat == EnumStat.ScanFinish)
                {
                    IsShowScanTime = false;
                    SCAN_TOTAL_CNT++;
                }

            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
        #region message 
        private void InitalMessage()
        {
            try
            {
                lstMessage.View = View.Details;
                lstMessage.GridLines = true;
                lstMessage.FullRowSelect = true;
                lstMessage.Sorting = SortOrder.Ascending;
                lstMessage.Columns.Add("时间", -1, HorizontalAlignment.Left);
                lstMessage.Columns.Add("消息类型 ", -1, HorizontalAlignment.Left);
                lstMessage.Columns.Add("消息内容", -1, HorizontalAlignment.Left);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public void DisplayMessage(MsgType type, string message)
        {
            try
            {
                //delete first and add new message
                if (lstMessage.Items.Count >= MAX_MESSAGE_COUNT)
                {
                    for (int i = 0; i < lstMessage.Items.Count - MAX_MESSAGE_COUNT; i++)
                    {
                        lstMessage.Items[i].Remove();
                    }
                }
                //add new message
                ListViewItem item1 = new ListViewItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff"));
                item1.SubItems.Add(type.ToString());
                item1.SubItems.Add(message.ToString());

                lstMessage.Items.AddRange(new ListViewItem[] { item1 });
                switch (type)
                {
                    case MsgType.Error:
                        item1.SubItems[0].ForeColor = Color.Red;
                        item1.SubItems[1].ForeColor = Color.Red;
                        item1.SubItems[2].ForeColor = Color.Red;
                        break;
                    case MsgType.Warning:
                        item1.SubItems[0].ForeColor = Color.Blue;
                        item1.SubItems[1].ForeColor = Color.Blue;
                        item1.SubItems[2].ForeColor = Color.Blue;
                        break;
                }
                lstMessage.Items[lstMessage.Items.Count - 1].EnsureVisible();
                lstMessage.Refresh();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void UpdateMessageBody(MessageBody msg)
        {
            try
            {                
                lstMsgBody.Add(msg);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion

        #region menu click
        private void btnSelect_Click(object sender, EventArgs e)
        {
            
            //var fmr3 = new FocusManualForm();
            //fmr3.Show();
            var frm = new RecipeSelectForm();
            frm.ShowDialog();            
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            //1.check rcp

            //2.check is ready
            if(!ConstDef.IsSysReady)
            {
                Dialogs.Show("系统还没有准备好，不能启动!", false);
                return;
            }
            if(string.IsNullOrEmpty(txtPersonId.Text))
            {
                Dialogs.Show("请输入编号!", false);
                txtPersonId.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPersonName.Text))
            {
                Dialogs.Show("请输入姓名!", false);
                txtPersonName.Focus();
                return;
            }
            if(Manager.IRcp.RecipeId==0 && string.IsNullOrEmpty(Manager.IRcp.RecipeName))
            {
                Dialogs.Show("请选择配置参数!", false);                
                return;
            }
            SystemStatus.Instance.ScanID = txtPersonId.Text.Trim();
            SystemStatus.Instance.ZStackID = txtPersonName.Text.Trim();
            //start 
            ConstDef.IsScan = true;
            Manager.ICamCtrl.Init();
            //Manager.IInspCtrl.Start();
            Manager.IAutoCtrl.LotId = txtPersonId.Text.Trim();
            Manager.IAutoCtrl.BatchId = txtPersonName.Text.Trim();
            Manager.IAutoCtrl.Start();            
            CheckStatEnable();
            LogHelper.AppLoger.Info("user click scan button.");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ConstDef.IsScan = false;
            Manager.ICamCtrl.Stop();
            Manager.IAutoCtrl.Stop();
            CheckStatEnable();
            LogHelper.AppLoger.Info("user click stop button.");
        }
        private void btnAbortStop_Click(object sender, EventArgs e)
        {
            ConstDef.IsScan = false;
            Manager.IAutoCtrl.AbortScan();
            Manager.IMotorCtrl.StopAxis();
            CheckStatEnable();
            LogHelper.AppLoger.Info("user click abort stop button.");
        }
        private void btnInit_Click(object sender, EventArgs e)
        {
            DBCtrl.DAO.UserDao usrD = new DBCtrl.DAO.UserDao();
            DBCtrl.Model.User u = new DBCtrl.Model.User();
            usrD.Insert(u);
            Manager.ICamCtrl.UpdateImage -= CameraCtrl_UpdateImage;
            FullImageForm frm = new FullImageForm();
            frm.Show();
        }
        #endregion

        private void msg_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lstMsgBody.Count > 0)
                {
                    DisplayMessage(lstMsgBody[0].type, lstMsgBody[0].msg.ToString());
                    lstMsgBody.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void OperatorForm_Resize(object sender, EventArgs e)
        {
            try
            {
                int msgWidth = this.pnlbuton.Width;
                int msgHeight = this.pnlbuton.Height;

                lstMessage.Width = msgWidth;
                if (lstMessage.Columns.Count > 0)
                {
                    lstMessage.Columns[0].Width = msgWidth / 6;
                    lstMessage.Columns[1].Width = msgWidth / 6;
                    lstMessage.Columns[2].Width = 2 * msgWidth / 3;
                }
                int rlt_height = this.pnlStat.Height;
                pnlResult.Height = rlt_height / 2;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #region stat sumary
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                TimeSpan ustT = DateTime.Now - sysStartTime;
                lblSysHour.Text = ustT.TotalHours.ToString("f2");
                lblSysCnt.Text = SCAN_TOTAL_CNT.ToString("d3");
                ustT = DateTime.Now - oneScanTime;
                if (IsShowScanTime)
                    lblScanTime.Text = ustT.TotalSeconds < 1.5 ? ustT.TotalSeconds.ToString("f2") : (ustT.TotalSeconds-1.5).ToString("f2");
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            sysStartTime = DateTime.Now;
            SCAN_TOTAL_CNT = 0;
        }

        #endregion
        #region 显示图像  
        public delegate void DisplayHandler(Bitmap obj);
        //定义一个委托变量
        public DisplayHandler updateImg;
        //修改Imgae值的方法。        
        public void UpdateImgMethod(Bitmap obj)
        {
            this.kpImgView.Image = obj;
        }
        //此为在非创建线程中的调用方法，其实是使用TextBox的Invoke方法。
        public void ThreadMethodImg(Bitmap obj)
        {
            this.kpImgView.Invoke(updateImg, obj);
        }
        public void DisplayImage(Bitmap obj)
        {
            try
            {
                ThreadMethodImg(obj);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
        #region JobList fun
        private void AddJob(Jobs job)
        {
            try
            {
                if (lstJob.Count > MAX_JOB_CNT)
                    lstJob.RemoveAt(0);
                lstJob.Add(job);
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }        
        public delegate void DisplayResultHandler();
        //定义一个委托变量
        public DisplayResultHandler updateRlt;
        //修改Imgae值的方法。        
        public void UpdateRltMethod()
        {
            //dgvResult.DataSource = lstJob;
            dgvResult.DataSource = new BindingList<Jobs>(lstJob);
        }
        //此为在非创建线程中的调用方法，其实是使用TextBox的Invoke方法。
        public void ThreadMethodRlt()
        {
            this.dgvResult.Invoke(updateRlt);
        }
        public void DisplyResult()
        {
            try
            {
                ThreadMethodRlt();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
              

        #region Check IO 
        Thread thCheckIO;
        bool isCheckIO = true;     
        private void StartCheckIO()
        {
            if (thCheckIO == null)
            {
                thCheckIO = new Thread(new ThreadStart(IOCheck));
                thCheckIO.Name = "IO Thread";
                thCheckIO.IsBackground = true;
                thCheckIO.Start();
            }
        }
        private void IOCheck()
        {
            while (true)
            {
                if (isCheckIO)
                {
                    IOCheckFun();                    
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        private void IOCheckFun()
        {
            
            try
            {

                if (Manager.IioCard.IStartOnOff())
                {
                    Manager.IioCard.SetOutOff(sysCfg.IO_Output_Stop_Light);
                    Manager.IioCard.SetOutOn(sysCfg.IO_Output_Start_Light);
                }
                if (Manager.IioCard.IStopOnOff())
                {
                    Manager.IioCard.SetOutOff(sysCfg.IO_Output_Start_Light);
                    Manager.IioCard.SetOutOn(sysCfg.IO_Output_Stop_Light);
                }

            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }            
        }
        #endregion

        private void txtPersonId_DoubleClick(object sender, EventArgs e)
        {
            if (frmOCRLive.Visible == false)
            {
                Point p = new Point(0, 0);
                frmOCRLive = new OCRLiveForm();
                if (frmOCRLive.ShowDialog() == DialogResult.OK)
                {
                    txtPersonId.Text = frmOCRLive.OcrDecodeString;
                }
                //frmOCRLive.ShowDialog();
            }
            else
            {
                frmOCRLive.BringToFront();
            }
        }
    }
}
