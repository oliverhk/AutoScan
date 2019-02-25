using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp.WinUC
{
    public enum MouseDirection
    {
        East,
        West,
        South,
        North,
        Southeast,
        Southwest,
        Northeast,
        Northwest,
        None
    }
    public partial class RoiPanel : Panel
    {
        private Color panelBorderColor = Color.Red;
        private Point mouseDownPoint;
        public static int PanelDefaultSize = 512;
        MouseDirection direction;

        public RoiPanel()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new Size(PanelDefaultSize, PanelDefaultSize);

            this.Paint += new PaintEventHandler(panel_Paint);
            this.Click += new EventHandler(panel_Click);
            this.LostFocus += new EventHandler(panel_LostFocus);
            this.MouseDown += new MouseEventHandler(panel_MouseDown);
            this.MouseMove += new MouseEventHandler(panel_MouseMove);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            if (this.Focused)
            {
                ControlPaint.DrawBorder(e.Graphics,
                    this.ClientRectangle,
                    panelBorderColor, 2, ButtonBorderStyle.Dotted,
                    panelBorderColor, 2, ButtonBorderStyle.Dotted,
                    panelBorderColor, 2, ButtonBorderStyle.Dotted,
                    panelBorderColor, 2, ButtonBorderStyle.Dotted
                );
            }
        }

        private void panel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.SizeAll;
            this.Focus();
            this.Invalidate();
            this.Update();
        }

        private void panel_LostFocus(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            this.Invalidate();
            this.Update();
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint = e.Location;
            }
        }

        Point GetPointToParent(Point p)
        {
            Point res = new Point();
            res.X = p.X + this.Location.X;
            res.Y = p.Y + this.Location.Y;

            return res;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox parent = (PictureBox)this.Parent;
            Point point = GetPointToParent(e.Location);

            if (this.Focused == false)
            {
                return;
            }

            int validPixel = 8;
            if (e.Location.X >= this.Width - validPixel && e.Location.Y >= this.Height - validPixel)
            {
                this.Cursor = Cursors.SizeNWSE;
                direction = MouseDirection.Southeast;
            }
            else if (e.Location.X <= validPixel && e.Location.Y <= validPixel)
            {
                this.Cursor = Cursors.SizeNWSE;
                direction = MouseDirection.Northwest;
            }
            else if (e.Location.X <= validPixel && e.Location.Y >= this.Height - validPixel)
            {
                this.Cursor = Cursors.SizeNESW;
                direction = MouseDirection.Southwest;
            }
            else if (e.Location.X >= this.Width - validPixel && e.Location.Y <= validPixel)
            {
                this.Cursor = Cursors.SizeNESW;
                direction = MouseDirection.Northeast;
            }
            else
            {
                this.Cursor = Cursors.SizeAll;
                direction = MouseDirection.None;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (direction != MouseDirection.None)//改变大小
                {
                    Point oldLocation = this.Location;
                    ResizeControl(e.Location);
                }
                else
                {
                    Point oldLocation = this.Location;
                    this.Cursor = Cursors.SizeAll;
                    Point location = GetPointToParent(new Point(e.Location.X - mouseDownPoint.X, e.Location.Y - mouseDownPoint.Y));
                    RelocationControl(location);
                }
            }
        }

        private void RelocationControl(Point location)
        {
            if (location.X < 0)
            {
                location.X = 0;
            }
            else if (location.X > this.Parent.Width - this.Width)
            {
                location.X = this.Parent.Width - this.Width;
            }
            if (location.Y < 0)
            {
                location.Y = 0;
            }
            else if (location.Y > this.Parent.Height - this.Height)
            {
                location.Y = this.Parent.Height - this.Height;
            }
            this.Location = location;
        }

        private void ResizeControl(Point p)
        {
            Point mousePosition = GetPointToParent(p);//将控件内坐标转换为父容器坐标
            Point location = this.Location;
            if (direction == MouseDirection.Southeast)//东南
            {
                this.Cursor = Cursors.SizeNWSE;
                if (mousePosition.X - this.Left <= 10)
                {
                    mousePosition.X = this.Left + 10;
                    this.Width = 10;
                }
                else if (mousePosition.X - this.Left >= this.Parent.Width - this.Left)
                {
                    mousePosition.X = this.Parent.Width;
                    this.Width = this.Parent.Width - this.Left;
                }
                else
                {
                    this.Width = mousePosition.X - this.Left;
                }
                if (mousePosition.Y - this.Top <= 10)
                {
                    mousePosition.Y = this.Top + 10;
                    this.Height = 10;
                }
                else if (mousePosition.Y - this.Top >= this.Parent.Height - this.Top)
                {
                    mousePosition.Y = this.Parent.Height;
                    this.Height = this.Parent.Height - this.Top;
                }
                else
                {
                    this.Height = mousePosition.Y - this.Top;
                }
            }
            else if (direction == MouseDirection.Southwest)//西南
            {
                this.Cursor = Cursors.SizeNESW;
                if (this.Right - mousePosition.X <= 10)
                {
                    mousePosition.X = this.Right - 10;
                    this.Width = 10;
                }
                else if (this.Right - mousePosition.X >= this.Right)
                {
                    this.Width = this.Right;
                }
                else
                {
                    this.Width = this.Right - mousePosition.X;
                }
                this.Left = mousePosition.X;
                if (mousePosition.Y - this.Top <= 10)
                {
                    mousePosition.Y = this.Top + 10;
                    this.Height = 10;
                }
                else if (mousePosition.Y - this.Top >= this.Parent.Height - this.Top)
                {
                    mousePosition.Y = this.Parent.Height;
                    this.Height = this.Parent.Height - this.Top;
                }
                else
                {
                    this.Height = mousePosition.Y - this.Top;
                }
            }
            else if (direction == MouseDirection.Northeast)//东北
            {
                this.Cursor = Cursors.SizeNESW;
                if (mousePosition.X - this.Left <= 10)
                {
                    mousePosition.X = this.Left + 10;
                    this.Width = 10;
                }
                else if (mousePosition.X - this.Left >= this.Parent.Width - this.Left)
                {
                    mousePosition.X = this.Parent.Width;
                    this.Width = this.Parent.Width - this.Left;
                }
                else
                {
                    this.Width = mousePosition.X - this.Left;
                }
                if (this.Bottom - mousePosition.Y <= 10)
                {
                    mousePosition.Y = this.Bottom - 10;
                    this.Height = 10;
                }
                else if (this.Bottom - mousePosition.Y >= this.Bottom)
                {
                    mousePosition.Y = 0;
                    this.Height = this.Bottom;
                }
                else
                {
                    this.Height = this.Bottom - mousePosition.Y;
                }
                this.Top = mousePosition.Y;
            }
            else if (direction == MouseDirection.Northwest)//西北
            {
                this.Cursor = Cursors.SizeNWSE;
                if (this.Right - mousePosition.X <= 10)
                {
                    mousePosition.X = this.Right - 10;
                    this.Width = 10;
                }
                else if (this.Right - mousePosition.X >= this.Right)
                {
                    this.Width = this.Right;
                }
                else
                {
                    this.Width = this.Right - mousePosition.X;
                }
                this.Left = mousePosition.X;
                if (this.Bottom - mousePosition.Y <= 10)
                {
                    mousePosition.Y = this.Bottom - 10;
                    this.Height = 10;
                }
                else if (this.Bottom - mousePosition.Y >= this.Bottom)
                {
                    mousePosition.Y = 0;
                    this.Height = this.Bottom;
                }
                else
                {
                    this.Height = this.Bottom - mousePosition.Y;
                }
                this.Top = mousePosition.Y;
            }
        }
    }
}
