/****************************************
 * IO CARD :SRND-CM-RY1
 * AUTHOR : CHANGLI.XIAO
 * DATE : 2018/8/14
 * VERSION : V1.0
 * 
 * ************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using Utility;
using System.IO.Ports;
using System.Threading;

namespace HWCtrl
{
    public class IOCtrl:IIO
    {
        #region instance
        //Singleton instance
        private static IOCtrl _instance;
        public static IOCtrl Instance => _instance ?? (_instance = new IOCtrl());
        public IOCtrl()
        {
            Open();
        }        
        ~IOCtrl()
        {
            Close();
        }
        #endregion
        #region define
        private uint[] auchCRCHi = new uint[] {
                    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
                    0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
                    0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
                    0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
                    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
                    0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
                    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
                    0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
                    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
                    0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
                    0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
                    0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
                    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
                    0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
                    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
                    0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
                    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
                    0x40
                    };

        private uint[] auchCRCLo = new uint[] {
                    0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
                    0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
                    0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,
                    0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
                    0x12, 0xD1, 0xD0, 0x12, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
                    0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
                    0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
                    0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
                    0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
                    0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
                    0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
                    0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
                    0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,
                    0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
                    0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,
                    0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
                    0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,
                    0x40
                    };
        #endregion
        private SerialPort ioSerialPort;        //for serial port
        private SystemConfig sysCfg = SystemConfig.Instance;

        #region INTERFACE
        public bool Open()
        {
            bool ret = false;
            try
            {
                //open seril port                
                ioSerialPort = new SerialPort();
                ioSerialPort.PortName = sysCfg.IOCard_Serial_Port;
                ioSerialPort.BaudRate = 9600;
                ioSerialPort.DataBits = 8;
                ioSerialPort.Parity = System.IO.Ports.Parity.None;
                ioSerialPort.StopBits = System.IO.Ports.StopBits.One;
                ioSerialPort.ReadTimeout = 500;
                ioSerialPort.WriteTimeout = 500;
                ioSerialPort.Open();
                ret = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        public bool Close()
        {
            if (ioSerialPort !=null && ioSerialPort.IsOpen)
                ioSerialPort.Close();
            return true;
        }

        public int ReadInput(int bit)
        {
            int rect = 0;
            try
            {
                int iout = ReadInAll();
                string strbin = Convert.ToString(iout, 2).PadLeft(8,'0');
                if (bit > 8)
                {
                    rect = 0;
                }
                {
                    string rev = strbin.Substring(7 - bit, 1);
                    rect = int.Parse(rev);
                }
            }catch(Exception ex)
            { LogHelper.AppLoger.Error(ex); }
            return rect;
            
        }

        public int ReadOut(int bit)
        {
            int rect = 0;
            try
            {
                int iout = ReadOutAll();
                string strbin = Convert.ToString(iout, 2);
                if (bit > 8)
                {
                    rect = 0;
                }
                {
                    string rev = strbin.Substring(7 - bit, 1);
                    rect = int.Parse(rev);
                }
            }
            catch (Exception ex)
            { LogHelper.AppLoger.Error(ex); }
            return rect;
        }

        public bool SetOutOn(int bit)
        {
            return SetOutIo(bit, true);
        }

        public bool SetOutOff(int bit)
        {
            return SetOutIo(bit, false);
        }
        public bool IStartOnOff()
        {
            return ReadInput(sysCfg.IO_Input_Start) == 1 ? true : false;
        }
        public bool IStopOnOff()
        {
            return ReadInput(sysCfg.IO_Input_Stop) == 1 ? true : false;                 
        }
        public bool SetLightGreenOn()
        {
            bool rect = false;
            rect = SetOutIo(sysCfg.IO_Output_Green_Light, true);  //set on 
            rect = SetOutIo(sysCfg.IO_Output_Red_Light, false);   //set off
            return rect;
        }
        public bool SetLightOnAlways(bool isOnOff)
        {
            return SetOutIo(sysCfg.IO_Light_On_Alway, isOnOff);
        }
        public bool SetLightRedOn()
        {
            bool rect = false;
            rect = SetOutIo(sysCfg.IO_Output_Green_Light, false);  //set on 
            rect = SetOutIo(sysCfg.IO_Output_Red_Light, true);   //set off
            return rect;
        }
        #endregion
        #region method
        /// <summary>
        /// 设置所有的输出IO OFF
        /// </summary>
        public void SetOutIoAllOff()
        {           
            try
            {
                string cmd = "010F00000008020000";
                string rev = string.Empty;
                if (!Write(cmd, ref rev))
                    return ;
                if (string.IsNullOrEmpty(rev))
                    return ;
                if (rev.Length < 12)
                    return ;              

            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }            
        }
        /// <summary>
        /// 设置输出
        /// </summary>
        /// <param name="bit">io bit</param>
        /// <param name="isOn"></param>
        /// <returns></returns>
        private bool SetOutIo(int bit,bool isOn)
        {
            bool rect = false;
            try
            {
                string cmd = string.Format("010500{0}{1}00",bit.ToString().PadLeft(2,'0'),isOn?"FF":"00");
                string rev = string.Empty;
                if (!Write(cmd, ref rev))
                    return rect;
                if (string.IsNullOrEmpty(rev))
                    return rect;
                if (rev.Length < 12)
                    return rect;
                string reccode = rev.Substring(8, 2);
                if (reccode == (isOn ? "FF" : "00"))
                    rect = true;
                else
                    rect = false;

            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        /// <summary>
        /// 读取所有输入IO的状态，共8个输入
        /// </summary>
        /// <returns></returns>
        public int ReadInAll()
        {
            int rect = 0;
            try
            {
                string cmd = "010200000008";    //read all in io status           

                string rev = string.Empty;
                if (!Write(cmd, ref rev))
                    return rect;
                if (string.IsNullOrEmpty(rev))
                    return rect;
                if (rev.Length < 12)
                    return rect;
                string reccode = rev.Substring(6, 2);
                rect = int.Parse(reccode);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        /// <summary>
        /// 读取所有输出IO的状态，共8bit
        /// </summary>
        /// <returns></returns>
        private int ReadOutAll()
        {
            int rect = 0;
            try
            {
                string cmd = "010100000008";    //read all out io status           

                string rev = string.Empty;
                if (!Write(cmd, ref rev))
                    return rect;
                if (string.IsNullOrEmpty(rev))
                    return rect;
                if (rev.Length < 12)
                    return rect;
                string reccode = rev.Substring(6, 2);
                rect = int.Parse(reccode);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private byte[] BuildCommand(string cmd)
        {
            byte[] hexcmd = strToToHexByte(cmd);
            byte[] crc = GetCRC(cmd);
            byte[] bcmd = new byte[hexcmd.Length+crc.Length];
            Array.Copy(hexcmd, bcmd, hexcmd.Length);
            for(int i=0;i<crc.Length;i++)
            {
                bcmd[hexcmd.Length + i] = crc[i];
            }
            return bcmd;
        }
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2),16);
            return returnBytes;
        }
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ByteToHexString(byte[] bytes)
        {
            string str = string.Empty;
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    str += bytes[i].ToString("X2");
                }
            }
            return str;
        }
        private bool IsUse = false;
        private bool Write(string cmd, ref string recvStr)
        {
            bool ret = false;
            try
            {
                if (IsUse)
                    return ret;
                IsUse = true;
                byte[] bytcmd = BuildCommand(cmd);
                if (!ioSerialPort.IsOpen)
                {
                    ioSerialPort.Open();
                }
                ioSerialPort.DiscardInBuffer();
                ioSerialPort.Write(bytcmd, 0, bytcmd.Length);
                Thread.Sleep(100);

                int count = ioSerialPort.BytesToRead;
                if (count <= 0)
                    return ret;
                byte[] readBuffer = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    readBuffer[i] = Convert.ToByte(ioSerialPort.ReadByte());
                    Thread.Sleep(1);
                }
                recvStr = ByteToHexString(readBuffer);

                ret = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                IsUse = false;
            }
            return ret;
        }
        private byte[] GetCRC(string message)
        {
            char chCRCHi = (char)0xFF;
            char chCRCLo = (char)0xFF;
            int index;
            uint len = Convert.ToUInt32(message.Length);
            int i = 0;
            while (len > 0)
            {
                index = chCRCHi ^ Convert.ToChar(Convert.ToInt32(message.Substring(i, 2), 16)); /*计算CRC */
                chCRCHi = (char)(chCRCLo ^ (char)auchCRCHi[index]);
                chCRCLo = (char)auchCRCLo[index];
                len--;
                len--;
                i = i + 2;
            }
            byte[] CRC = new byte[2];
            CRC[0] = Convert.ToByte(chCRCHi);
            CRC[1] = Convert.ToByte(chCRCLo);
            return CRC;
        }
        #endregion
    }
}
