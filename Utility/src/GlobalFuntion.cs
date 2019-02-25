using System;
using System.IO;
using System.Drawing;
using System.Management;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;

namespace Utility
{
    public delegate void SetTextCallback(Control control, string text);
    public delegate void SetEnableCallback(Control control, bool enable);
    public delegate void SetVisibleCallback(Control control, bool visible);
    public delegate void SetCursorCallback(Control control, Cursor cursor);
    public delegate void SetAddItemCallback(ListBox control, string item);
    public delegate void SetGridViewDataSourceCallback(DataGridView control, object dataSource);
    public delegate void SetAddRowCallback(DataGridView control, DataGridViewRow row);
    public delegate void SetClearRowsCallback(DataGridView control);
    public delegate void ClearControlsCallback(Control control);
    public delegate void AddControlCallback(Control parent, Control child);
    public delegate void RefreshControlsCallback(Control control);

    public static class GlobalFunction
    {
        public static TabControl FindParentTabControl(Control c)
        {
            TabControl finded = null;
            
            Control temp = c.Parent;
            while (temp != null)
            {
                if (temp is TabControl)
                {
                    finded = temp as TabControl;
                    break; 
                }
                temp = temp.Parent;
            }

            return finded;
        }

        public static TabPage FindParentTabPage(Control c)
        {
            TabPage finded = null;

            Control temp = c.Parent;
            while (temp != null)
            {
                if (temp is TabPage)
                {
                    finded = temp as TabPage;
                    break;
                }
                temp = temp.Parent;
            }

            return finded;
        }

        public static bool Is64BitProcess()
        {
            return IntPtr.Size == 8;
        }

        public static void SetText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                SetTextCallback callback = new SetTextCallback(SetText);
                control.BeginInvoke(callback, new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
        }

        public static void SetEnable(Control control, bool enable)
        {
            if (control.InvokeRequired)
            {
                SetEnableCallback callback = new SetEnableCallback(SetEnable);
                control.BeginInvoke(callback, new object[] { control, enable });
            }
            else
            {
                control.Enabled = enable;
            }
        }

        public static void SetVisible(Control control, bool visible)
        {
            if (control.InvokeRequired)
            {
                SetVisibleCallback callback = new SetVisibleCallback(SetVisible);
                control.BeginInvoke(callback, new object[] { control, visible });
            }
            else
            {
                control.Visible = visible;
            }
        }

        public static void SetCursor(Control control, Cursor cursor)
        {
            if (control.InvokeRequired)
            {
                SetCursorCallback callback = new SetCursorCallback(SetCursor);
                control.BeginInvoke(callback, new object[] { control, cursor });
            }
            else
            {
                control.Cursor = cursor;
            }
        }

        public static void AddListBoxItem(ListBox control, string item)
        {
            if (control.InvokeRequired)
            {
                SetAddItemCallback callback = new SetAddItemCallback(AddListBoxItem);
                control.BeginInvoke(callback, new object[] { control, item });
            }
            else
            {
                control.Items.Add(item);
                control.SelectedIndex = control.Items.Count - 1;
            }
        }

        public static void SetGridViewDataSource(DataGridView control, object dataSource)
        {
            if (control.InvokeRequired)
            {
                SetGridViewDataSourceCallback callback = new SetGridViewDataSourceCallback(SetGridViewDataSource);
                control.BeginInvoke(callback, new object[] { control, dataSource });
            }
            else
            {
                control.DataSource = dataSource;
            }
        }

        public static void AddDataGridViewRow(DataGridView control, DataGridViewRow row)
        {
            if (control.InvokeRequired)
            {
                SetAddRowCallback callback = new SetAddRowCallback(AddDataGridViewRow);
                control.BeginInvoke(callback, new object[] { control, row });
            }
            else
            {
                control.Rows.Add(row);
            }
        }

        public static void ClearDataGridViewRows(DataGridView control)
        {
            if (control.InvokeRequired)
            {
                SetClearRowsCallback callback = new SetClearRowsCallback(ClearDataGridViewRows);
                control.BeginInvoke(callback, new object[] { control });
            }
            else
            {
                control.Rows.Clear();
            }
        }

        public static void ClearControls(Control control)
        {
            if (control.InvokeRequired)
            {
                ClearControlsCallback callback = new ClearControlsCallback(ClearControls);
                control.BeginInvoke(callback, new object[] { control });
            }
            else
            {
                control.Controls.Clear();
            }
        }

        public static void AddControl(Control parent, Control child)
        {
            if (parent.InvokeRequired)
            {
                AddControlCallback callback = new AddControlCallback(AddControl);
                parent.BeginInvoke(callback, new object[] { parent, child });
            }
            else
            {
                parent.Controls.Add(child);
            }
        }

        public static void RefreshControl(Control control)
        {
            if (control.InvokeRequired)
            {
                RefreshControlsCallback callback = new RefreshControlsCallback(RefreshControl);
                control.BeginInvoke(callback, new object[] { control });
            }
            else
            {
                control.Refresh();
            }
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="inBytes"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        public static string ByteToHexString(byte[] inBytes)
        {
            string stringOut = "";
            foreach (byte inByte in inBytes)
            {
                stringOut += string.Format("{0:X2} ", inByte);
            }
            return stringOut.TrimEnd(" ".ToCharArray());
        }

        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="inString"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        public static byte[] HexStringToByte(string inString)
        {
            string[] byteStrings = inString.Split(" ".ToCharArray());
            byte[] byteOut = new byte[byteStrings.Length];
            for (int i = 0; i <= byteStrings.Length - 1; i++)
            {
                byteOut[i] = Convert.ToByte((byteStrings[i]), 16);
            }
            return byteOut;
        }

        /// <summary>
        /// Convert a string to a byte array of hex digits.
        /// </summary>
        /// <param name="text"> The string</param>
        /// <returns> Returns an array of hex bytes. </returns>
        public static byte[] TextToHexByte(string text)
        {
            string hex = "";
            foreach (char c in text)
            {
                hex += Convert.ToString((int)c, 16);
                hex += " ";
            }

            hex = hex.TrimEnd(" ".ToCharArray());
            return HexStringToByte(hex);
        }

        /// <summary>
        /// Convert a string to a byte array of hex digits.
        /// </summary>
        /// <param name="text"> The string</param>
        /// <returns> Returns an array of hex bytes. </returns>
        public static string HexByteToChar(byte[] inBytes)
        {
            string charString = "";
            for (int i = 0; i <= inBytes.Length - 1; i++)
            {
                charString += Convert.ToChar(inBytes[i]).ToString();
            }

            return charString;
        }

        public static double GetDistance(PointF start, PointF end)
        {
            return Math.Abs(Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y)));
        }

        public static bool CheckUnziped(string name, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            string[] dirs = Directory.GetDirectories(destFolder, "*", SearchOption.TopDirectoryOnly);
            DirectoryInfo di = null;
            foreach (string item in dirs)
            {
                di = new DirectoryInfo(item);
                if (di.Name == name) return true;
            }
            return false;
        }

        public static string GetODBppPCBNameInMiscFolder(string pcbName, string destFolder)
        {
            string pcbNameInMisc = "";

            string jobNameFile = Path.GetFullPath(destFolder + @"\" + pcbName + @"\misc\") + "job_name";
            if (File.Exists(jobNameFile))
            {
                using (StreamReader sr = new StreamReader(jobNameFile))
                {
                    pcbNameInMisc = sr.ReadLine();
                }
            }
            return pcbNameInMisc;
        }

        /// <summary>
        /// Convert a byte array of hex digits to a string.
        /// </summary>
        /// <param name="bytes">An array of hex bytes.</param>
        /// <returns>The string</returns>
        public static string HexByteToText(byte[] bytes)
        {
            string hex = ByteToHexString(bytes);
            string[] hexArray = hex.Split(" ".ToCharArray());
            string text = "";
            for (int i = 0; i < hexArray.Length; i++)
            {
                int n = Convert.ToInt32(hexArray[i], 16);
                text += ((char)n).ToString();
            }
            return text;
        }

        public static bool IsIPv4(string ip)
        {
            if (string.IsNullOrEmpty(ip) || ip.Length < 7 || ip.Length > 15)
                return false;

            string[] subIP = ip.Split(".".ToCharArray());

            if (subIP.Length != 4)
            {
                return false;
            }
            else
            {
                string regformat1 = @"^((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|[0-9])$";
                Regex regex1 = new Regex(regformat1, RegexOptions.IgnoreCase);

                string regformat2 = @"^((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|[1-9])$";
                Regex regex2 = new Regex(regformat2, RegexOptions.IgnoreCase);

                for (int i = 0; i < subIP.Length; i++)
                {
                    if (i == 0 || i == subIP.Length - 1)
                    {
                        if (!regex2.IsMatch(subIP[i]))
                            return false;
                    }
                    else
                    {
                        if (!regex1.IsMatch(subIP[i]))
                            return false;
                    }
                }
            }
            return true;
        }

        public static void SendKeyDown(short key)
        {
            Input[] input = new Input[1];
            input[0].type = INPUT.KEYBOARD;
            input[0].ki.wVk = key;
            input[0].ki.time = Win32API.GetTickCount();

            if (Win32API.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public static void SendKeyUp(short key)
        {
            Input[] input = new Input[1];
            input[0].type = INPUT.KEYBOARD;
            input[0].ki.wVk = key;
            input[0].ki.dwFlags = KeyboardConstaint.KEYEVENTF_KEYUP;
            input[0].ki.time = Win32API.GetTickCount();

            if (Win32API.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /**/
        /// <summary>
        /// 判断窗体是否被遮挡
        /// </summary>
        /// <param name="hWnd">窗体句柄</param>
        /// <returns>返回窗体是否被遮挡</returns>
        public static bool IsWindowSheltered(IntPtr AHandle)
        {
            if (!Win32API.IsWindowVisible(AHandle)) return true; // 窗体不可见
            IntPtr pDC = Win32API.GetWindowDC(AHandle);
            try
            {
                Rectangle rect = new Rectangle();
                Win32API.GetClipBox(pDC, ref rect);
                return rect.Width - rect.Left <= 0 && rect.Height - rect.Top <= 0;
            }
            finally
            {
                Win32API.ReleaseDC(AHandle, pDC);
            }
        }

        private static List<char> _invalidPathChars = null;
        public static bool IsInvalidPathChars(string path)
        {
            if (_invalidPathChars == null)
            {
                _invalidPathChars = new List<char>();
                char[] chars = Path.GetInvalidFileNameChars();
                foreach (char item in chars)
                {
                    _invalidPathChars.Add(item);
                }
            }

            foreach (char item in path)
            {
                if (_invalidPathChars.Contains(item)) return true;
            }
            return false;
        }

        public static string GetLanguageText(string text, params object[] paras)
        {
            string temp = Resource.GetString(text);
            if (string.IsNullOrEmpty(temp))
            {
                if (paras != null && paras.Length > 0)
                {
                    text = string.Format(text, paras);
                }
                return text;
            }

            if (paras != null && paras.Length > 0)
            {
                temp = string.Format(temp, paras);
            }
            return temp;
        }

        public static bool IsIntersect(Rectangle rect1, Rectangle rect2)
        {
            return IsIntersect((RectangleF)rect1, (RectangleF)rect2);
        }

        public static bool IsIntersect(RectangleF rect1, RectangleF rect2)
        {
            if (rect1 == null || rect2 == null)
            {
                return false;
            }

            if (rect1.X + rect1.Width < rect2.X)
                return false;
            if (rect1.X > rect2.X + rect2.Width)
                return false;
            if (rect1.Y + rect1.Height < rect2.Y)
                return false;
            if (rect1.Y > rect2.Y + rect2.Height)
                return false;

            return true;
        }

        public static string GetCpuID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                string cpuID = "";
                foreach (ManagementObject mo in moc)
                {
                    cpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return cpuID;
            }
            catch
            {
                return "";
            }
        }

        public static string GetDiskDriveModel()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                string diskModel = "";
                foreach (ManagementObject mo in moc)
                {
                    diskModel = mo.Properties["Model"].Value.ToString();
                    break;
                }

                moc = new ManagementClass("Win32_PhysicalMedia").GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (mo.Properties["SerialNumber"].Value != null)
                    {
                        diskModel = mo.Properties["SerialNumber"].Value.ToString();
                    }
                    break;
                }

                return diskModel;
            }
            catch
            {
                return "";
            }
        }

        public static string GetBiosID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_BIOS");
                ManagementObjectCollection moc = mc.GetInstances();
                string biosID = "";
                foreach (ManagementObject mo in moc)
                {
                    biosID = mo.Properties["SerialNumber"].Value.ToString();
                    break;
                }
                return biosID;
            }
            catch
            {
                return "";
            }
        }

        public static string GetBoardID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_BaseBoard");
                ManagementObjectCollection moc = mc.GetInstances();
                string boardID = "";
                foreach (ManagementObject mo in moc)
                {
                    boardID = mo.Properties["SerialNumber"].Value.ToString();
                    break;
                }
                return boardID;
            }
            catch
            {
                return "";
            }
        }

        public static string GetMacAddress()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                string macAddress = "";
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"])
                    {
                        macAddress = mo.Properties["MacAddress"].Value.ToString();
                        break;
                    }
                    mo.Dispose();
                }
                return macAddress;
            }
            catch
            {
                return "";
            }
        }

        public static T DepthClone<T>(T obj)
        {
            System.Diagnostics.Debug.Assert(obj != null);
            T cloneObj = default(T);
            if (obj != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter serializer = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                    serializer.Serialize(ms, obj);
                    ms.Position = 0;
                    cloneObj = (T)serializer.Deserialize(ms);
                }
            }
            return cloneObj;
        }

        /// <summary>
        /// 是否能 Ping 通指定的主机，默认TimeOut时间为0.1秒
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <returns>true 通，false 不通</returns>
        public static bool Ping(string ip)
        {
            return Ping(ip, 100);
        }

        /// <summary>
        /// 是否能 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <param name="timeOut">TimeOut时间，单位为毫秒</param>
        /// <returns>true 通，false 不通</returns>
        public static bool Ping(string ip, int timeOut)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            int timeout = timeOut;
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }
        #region 图像转换
        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image)
        {
            byte[] buffer = null;
            try
            {
                //ImageFormat format = image.RawFormat;
                ImageFormat format = ImageFormat.Bmp;
                using (MemoryStream ms = new MemoryStream())
                {
                    if (format.Equals(ImageFormat.Jpeg))
                    {
                        image.Save(ms, ImageFormat.Jpeg);
                    }
                    else if (format.Equals(ImageFormat.Png))
                    {
                        image.Save(ms, ImageFormat.Png);
                    }
                    else if (format.Equals(ImageFormat.Bmp))
                    {
                        image.Save(ms, ImageFormat.Bmp);
                    }
                    else if (format.Equals(ImageFormat.Gif))
                    {
                        image.Save(ms, ImageFormat.Gif);
                    }
                    else if (format.Equals(ImageFormat.Icon))
                    {
                        image.Save(ms, ImageFormat.Icon);
                    }
                    buffer = new byte[ms.Length];
                    //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(buffer, 0, buffer.Length);
                    return buffer;
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return buffer;
        }

        /// <summary>
        /// Convert Byte[] to Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image BytesToImage(byte[] buffer)
        {
            Image rect = null;
            try
            {
                MemoryStream ms = new MemoryStream(buffer);
                rect = System.Drawing.Image.FromStream(ms);
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
