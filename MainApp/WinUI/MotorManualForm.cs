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
using MaterialSkin.Controls;

namespace MainApp.WinUI
{
    public partial class MotorManualForm : MaterialForm
    {
        public MotorManualForm()
        {
            InitializeComponent();
        }

        private void MotorManualForm_Load(object sender, EventArgs e)
        {
            //init system
            Init();
        }
        #region property
        private Point OrginPos = new Point();       //pic piexl postion
        private SystemConfig sysCfg = SystemConfig.Instance;
        private bool IsConnection = false;
        private int IMAGE_HEIGHT = 75;
        private int LEFT_WIDTH = 60;
        private int RIGHT_WIDTH = 315;
        private const int SLIDE_WIDTH = 45;
        private const int SLIDE_HEIGHT = 25;
        private Bitmap backimg1;
        private Bitmap backimg2;
        #endregion
        #region method
        private void Init()
        {
            try
            {
                if (Manager.IMotorCtrl.Connect(sysCfg.Control_IP))
                //if(false)
                {
                    IsConnection = true;                    
                    timer1.Start();
                    LogHelper.AppLoger.Debug("Motor Connected Successed.");
                }
                else
                {
                    IsConnection = false;
                    LogHelper.AppLoger.Debug("Motor Connected Failed.");
                    //Dialogs.Show("控制器连接失败!!");
                }
                InitSlideImage();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void InitSlideImage()
        {
            try
            {
                backimg1 = new Bitmap(LEFT_WIDTH, IMAGE_HEIGHT);
                backimg2 = new Bitmap(RIGHT_WIDTH, IMAGE_HEIGHT);
                Graphics g1 = Graphics.FromImage(backimg1);
                Graphics g2 = Graphics.FromImage(backimg2);
                Brush bush_o = new SolidBrush(Color.Red);

                g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g1.Clear(Color.FromArgb(192, 193, 186));
                g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g2.Clear(Color.FromArgb(192, 193, 186));

                g2.FillEllipse(bush_o, 10,IMAGE_HEIGHT/2, 5, 5);

                picHead.Image = backimg1;
                picDraw.Image = backimg2;

                g1.Dispose();
                g2.Dispose();
                //backimg1.Dispose();
                //backimg2.Dispose();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void UpdateSlidePost(float x,float y)
        {
            try
            {
                float xMm = x / 1000;
                float yMm = y / 1000;
                float x_scale = RIGHT_WIDTH / SLIDE_WIDTH;
                float y_scale = IMAGE_HEIGHT / SLIDE_HEIGHT;

                int xPixel = (int)(xMm * x_scale) + RIGHT_WIDTH /2;
                int yPixel = (int)(yMm * y_scale) + IMAGE_HEIGHT / 2;

                Graphics g1 = Graphics.FromImage(backimg2);                
                Brush bush_o = new SolidBrush(Color.Red);

                g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g1.Clear(Color.FromArgb(192, 193, 186));

                g1.FillEllipse(bush_o, xPixel, yPixel, 5, 5);

                picDraw.Image = backimg2;
                materialLabel7.Text = string.Format("位置：（{0}，{1}）",Math.Round(xMm,2),Math.Round(yMm,2));
                g1.Dispose();

            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion

        #region click event

        private void btnYleft_Click(object sender, EventArgs e)
        {
            try
            {
                if(!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                var dist = float.Parse(txtYpos.Text) - float.Parse(txtYstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.MoveWait(2, dist);
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnYRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                var dist = float.Parse(txtYpos.Text) + float.Parse(txtYstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.MoveWait(2, dist);
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
}

        private void btnXLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                var dist = float.Parse(txtXpos.Text) - float.Parse(txtXstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.MoveWait(1, dist);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnXRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                var dist = float.Parse(txtXpos.Text) + float.Parse(txtXstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.MoveWait(1, dist);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnXHome_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                Manager.IMotorCtrl.ExecHome(1);
                System.Threading.Thread.Sleep(3000);               
                Manager.IMotorCtrl.ExecHome(2);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnZLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                var dist = float.Parse(txtZpos.Text) + float.Parse(txtZstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.MoveWait(3, dist);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnZRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                var dist = float.Parse(txtZpos.Text) - float.Parse(txtZstep.Text);
                dist = dist / 1000;
                Manager.IMotorCtrl.MoveWait(3, dist);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnZHome_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                Manager.IMotorCtrl.ExecHome(3);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnHomeAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                Manager.IMotorCtrl.ExecHomeALL();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                Manager.IMotorCtrl.StopAxis();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void txtXspeed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                if (!string.IsNullOrEmpty(txtXspeed.Text))
                {
                    float p = float.Parse(txtXspeed.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(1, p);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void txtYspeed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                if (!string.IsNullOrEmpty(txtYspeed.Text))
                {
                    float p = float.Parse(txtYspeed.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(2, p);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void txtZspeed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    return;
                }
                if (!string.IsNullOrEmpty(txtZspeed.Text))
                {
                    float p = float.Parse(txtZspeed.Text) / 1000;
                    Manager.IMotorCtrl.SetSpeed(3, p);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }        

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!IsConnection)
                {
                    Dialogs.Show("控制器连接失败，请检查连接");
                    timer1.Stop();
                    return;
                }
                float x;
                float y;
                //x = Manager.IMotorCtrl.GetAxisXPosUm();20181213，顾叶俊
                //y = Manager.IMotorCtrl.GetAxisYPosUm();
                x = Manager.IMotorCtrl.GetAxisXPosUmEx();
                y = Manager.IMotorCtrl.GetAxisYPosUmEx();
                txtXpos.Text = x.ToString("f1");
                txtYpos.Text = y.ToString("f1");
                //txtZpos.Text = Manager.IMotorCtrl.GetAxisZPosUm().ToString();
                txtZpos.Text = Manager.IMotorCtrl.GetAxisZPosUmEx().ToString("f1");
                UpdateSlidePost(x,y);
            }
            catch(Exception ex)
            { LogHelper.AppLoger.Error(ex); }
        }
        #endregion
    }
}
