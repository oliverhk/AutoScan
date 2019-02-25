using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
    public partial class BaseForm : Form
    {
        public enum InfoType
        {
            Messages,
            Alarm
        }
        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            SetAllTextBoxEnter();
        }

        #region method
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="infor">信息类型</param>
        /// <param name="Message">消息内容</param>
        public void ShowMessage(InfoType infor, string Message)
        {
            lblMsg.Visible = true;            
            if (infor == InfoType.Alarm)
            {
                picError.Visible = true;
                picInfo.Visible = false;
                lblMsg.ForeColor = Color.Red;
            }
            else if (infor == InfoType.Messages)
            {
                picError.Visible = false;
                picInfo.Visible = true;
                lblMsg.ForeColor = Color.Black;
            }
            else
            {
                picError.Visible = false;
                picInfo.Visible = false;
                lblMsg.ForeColor = Color.Black;
            }
            lblMsg.Text = Message;
            pnlMsg.Visible = true;
            sys_timer.Start();
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="infor">信息类型</param>
        /// <param name="Message">消息内容</param>
        /// <param name="isOnLive">一直存在标志</param>
        public void ShowMessage(InfoType infor, string Message, bool isOnLive)
        {
            lblMsg.Visible = true;
            if (infor == InfoType.Alarm)
            {
                picError.Visible = true;
                picInfo.Visible = false;
                lblMsg.ForeColor = Color.Red;
            }
            else if (infor == InfoType.Messages)
            {
                picError.Visible = false;
                picInfo.Visible = true;
                lblMsg.ForeColor = Color.Black;
            }
            else
            {
                picError.Visible = false;
                picInfo.Visible = false;
                lblMsg.ForeColor = Color.Black;
            }
            lblMsg.Text = Message;
            pnlMsg.Visible = true;
            sys_timer.Enabled = isOnLive;
        }
        /// <summary>
        /// 隐藏消息
        /// </summary>
        public void HidMessage()
        {
            pnlMsg.Visible = false;
        }
        private Color c;
        private void SetAllTextBoxEnter()
        {
            c = Color.White;
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    control.Enter += TextBox1_Enter;
                    control.Leave += TextBox1_Leave;
                }
                if (control is ComboBox)
                {
                    control.Enter += ComBox1_Enter;
                    control.Leave += ComBox1_Leave;
                }
                if (control is GroupBox || control is Panel)
                {
                    foreach (Control ctr in control.Controls)
                    {
                        if (ctr is TextBox)
                        {
                            ctr.Enter += TextBox1_Enter;
                            ctr.Leave += TextBox1_Leave;
                        }
                        if (ctr is ComboBox)
                        {
                            ctr.Enter += ComBox1_Enter;
                            ctr.Leave += ComBox1_Leave;
                        }
                    }
                }
                if (control is SplitContainer)
                {
                    foreach (Control spCtr in ((SplitContainer)(control)).Panel1.Controls)
                    {
                        if (spCtr is TextBox)
                        {
                            spCtr.Enter += TextBox1_Enter;
                            spCtr.Leave += TextBox1_Leave;
                        }
                        if (spCtr is ComboBox)
                        {
                            spCtr.Enter += ComBox1_Enter;
                            spCtr.Leave += ComBox1_Leave;
                        }
                    }
                    foreach (Control spCtr in ((SplitContainer)(control)).Panel2.Controls)
                    {
                        if (spCtr is TextBox)
                        {
                            spCtr.Enter += TextBox1_Enter;
                            spCtr.Leave += TextBox1_Leave;
                        }
                        if (spCtr is ComboBox)
                        {
                            spCtr.Enter += ComBox1_Enter;
                            spCtr.Leave += ComBox1_Leave;
                        }
                        if (spCtr is Panel)
                        {
                            foreach (Control ctr in ((Panel)spCtr).Controls)
                            {
                                if (ctr is TextBox)
                                {
                                    ctr.Enter += TextBox1_Enter;
                                    ctr.Leave += TextBox1_Leave;
                                }
                                if (ctr is ComboBox)
                                {
                                    ctr.Enter += ComBox1_Enter;
                                    ctr.Leave += ComBox1_Leave;
                                }
                            }
                        }
                    }
                }

            }
        }
        private void TextBox1_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.FromArgb(224, 255, 255);
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = c;
        }
        private void ComBox1_Enter(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = Color.FromArgb(224, 255, 255);
        }

        private void ComBox1_Leave(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = c;
        }
        #endregion

        private void picClose_Click(object sender, EventArgs e)
        {
            pnlMsg.Visible = false;
        }

        private void picClose_MouseUp(object sender, MouseEventArgs e)
        {
            picClose.BorderStyle = BorderStyle.Fixed3D;
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            picClose.BorderStyle = BorderStyle.None;
        }

        private void picClose_MouseHover(object sender, EventArgs e)
        {
            picClose.BorderStyle = BorderStyle.Fixed3D;
        }

        private void sys_timer_Tick(object sender, EventArgs e)
        {
            pnlMsg.Visible = false;
            sys_timer.Stop();
        }
    }
}
