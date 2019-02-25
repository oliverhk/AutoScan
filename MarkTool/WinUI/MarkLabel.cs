using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBCtrl.Model;
using Utility;
using DBCtrl.DAO;

namespace MarkTool.WinUI
{
    public partial class MarkLabel : Label
    {
        private MarkCell markCell = null;
        ToolTip tip = new ToolTip();
        public MarkLabel(MarkCell mark, Control parent)
        {
            InitializeComponent();

            markCell = mark;
            this.Parent = parent;
            this.AutoSize = false;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new Size(40, 24);
            this.Font = new Font("宋体", 12, FontStyle.Regular);
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Visible = false;

            UpdateMark(mark);

            this.Click += new EventHandler(label_Click);
            this.Leave += new EventHandler(label_Leave);

            tip.IsBalloon = true;
        }

        //for Fake MarkLabel
        public MarkLabel()
        {
            InitializeComponent();

            markCell=null;
            this.Parent = null;
            this.AutoSize = false;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new Size(40, 24);
            this.Font = new Font("宋体", 12, FontStyle.Regular);
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Visible = false;

            this.Click += new EventHandler(label_Click);
            this.Leave += new EventHandler(label_Leave);
        }

        public void SetMarkLocation()
        {
            try
            {
                PictureBox pbImageView = (PictureBox)this.Parent;
                if (pbImageView.Image == null)
                {
                    return;
                }

                Point pos = new Point();
                pos.X = Convert.ToInt32(pbImageView.Width * ((double)markCell.ImageX / pbImageView.Image.Width));
                pos.Y = Convert.ToInt32(pbImageView.Height * ((double)markCell.ImageY / pbImageView.Image.Height));
                this.Location = pos;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        public void UpdateMark(MarkCell mark)
        {
            markCell = mark;

            Types cellType = MainForm.dicCellType[mark.TypeId];
            this.Text = cellType.CellType;
            if (cellType.ColorStr != null && cellType.ColorStr != "NULL")
            {
                this.ForeColor = ColorTranslator.FromHtml(cellType.ColorStr);
            }

            //Add tips
            Category cate = MainForm.dicCategory[cellType.CateId];
            string message = string.Format("编号:{0}\n细胞类型:{1}_{2}\n标记人:{3}\n备注:{4}",
                mark.Id, cate.CategoryName, cellType.CellType, mark.UserId, mark.Remarks);          
            tip.SetToolTip(this, message);

            SetMarkLocation();
        }

        private void label_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
            this.Focus();
        }

        private void label_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        public int GetMarkId()
        {
            return markCell == null ? 0 : markCell.Id;
        }
    }
}
