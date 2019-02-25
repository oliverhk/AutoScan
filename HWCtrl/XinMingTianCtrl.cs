/****
 * 芯明天 E17 通讯控制 RS232
 * changli.xiao
 * 2018.6.5
 * 
 * */
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
    public class XinMingTianCtrl : IPIZ
    {
        #region instance
        //Singleton instance
        private static XinMingTianCtrl _instance;
        public static XinMingTianCtrl Instance => _instance ?? (_instance = new XinMingTianCtrl());
        public XinMingTianCtrl()
        {
            try
            {
                //open seril port                
                ioSerialPort = new SerialPort();
                ioSerialPort.PortName = sysCfg.Pize_Serial_Port;
                ioSerialPort.BaudRate = 9600;
                ioSerialPort.DataBits = 8;
                ioSerialPort.Parity = System.IO.Ports.Parity.None;
                ioSerialPort.StopBits = System.IO.Ports.StopBits.One;
                ioSerialPort.ReadTimeout = 1200;
                ioSerialPort.WriteTimeout = 1200;
                ioSerialPort.Open();
                basicPara = new PIZParaEntity();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        event UpdateEventHander IPIZ.UpdateMessage
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        ~XinMingTianCtrl()
        {

        }
        #endregion
        #region define
        public event UpdateEventHander UpdateMessage;
        private SerialPort ioSerialPort;        //for serial port
        private SystemConfig sysCfg = SystemConfig.Instance;
        private bool IsUse;
        private bool bCloseLoopMode { get; set; } //开闭环
        private PIZParaEntity basicPara { get; set; }  //基础参数

        /// <summary>
        /// 串口异步返回字符串，20181213，顾叶俊
        /// </summary>
        public static string recvStr = string.Empty;
        #endregion
        #region interface
        public bool Open()
        {
            bool ret = false;
            try
            {
                ret = Init();                
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }

        public bool Close()
        {
            if (ioSerialPort != null && ioSerialPort.IsOpen)
                ioSerialPort.Close();
            return true;
        }

        public bool Zero()
        {
            return MoveAxis(0);
        }

        public float MaxRange()
        {
           return basicPara != null ? basicPara.MaxShift : 0f;
        }

        public float GetCurPos()
        {
            float rect = 0f;
            try
            {
                string cmd = "@RB1XXXX";
                string outStr = string.Empty;
                if (Write(cmd, ref outStr))
                {
                    if (outStr.Length > 6)
                    {
                        rect = Utility.DataUtility.ParseFloat(outStr.Substring(2, 4));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }

        public bool SetDistance(float distance)
        {
            return MoveAxis(distance);
        }
        public void SetCloseMode(bool isCloseLoop)
        {
            throw new NotImplementedException();
        }
        public bool GetCloseMode()
        {
            return bCloseLoopMode;
        }
        public PIZParaEntity GetPara()
        {
            return basicPara;
        }
        #endregion
        #region common
        private byte[] BuildCommand(string cmd)
        {                        
            byte[] rect = StrToByte(cmd);
            byte chk = GetXOR(rect);            
            byte[] re = new byte[rect.Length + 1];
            for (int i = 0; i < rect.Length; i++)
            {                
                re[i] = rect[i];
            }            
            re[rect.Length] = chk;
            return re;
        }
        /// <summary>  
        /// 计算按位异或校验和（返回校验和值）  
        /// </summary>  
        /// <param name="Cmd">命令数组</param>  
        /// <returns>校验和值</returns>  
        private byte GetXOR(byte[] Cmd)
        {
            byte check = (byte)(Cmd[0] ^ Cmd[1]);
            for (int i = 2; i < Cmd.Length; i++)
            {
                check = (byte)(check ^ Cmd[i]);
            }
            return check;
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
        /// <summary>
        /// 字节数组转10进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ByteToDECString(byte[] bytes)
        {
            string str = string.Empty;
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    str += bytes[i].ToString();
                }
            }
            return str;
        }
        /// <summary>
        /// 字节数组转10进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ByteToString(byte[] bytes)
        {
            string str = string.Empty;
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    str += bytes[i].ToString();
                }
            }
            return str;
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
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        /// <summary>
        /// 16字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] StrToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                string hexstr = hexString.Substring(i * 2, 2);
                returnBytes[i] = Convert.ToByte(Convert.ToInt32(hexstr, 16));

                //returnBytes[i] = Convert.ToByte((Convert.ToInt32(, 10)).ToString(), 16);
            }
                
            return returnBytes;
        }

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
                Thread.Sleep(50);

                int count = ioSerialPort.BytesToRead;
                if (count <= 0)
                    return true;
                byte[] readBuffer = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    readBuffer[i] = Convert.ToByte(ioSerialPort.ReadByte());
                    //Thread.Sleep(1);
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

        /// <summary>
        /// 读取串口命令的重载函数，增加超时报警功能，20181213，顾叶俊
        /// </summary>
        /// <param name="cmd">命令串</param>
        /// <param name="recvStr">返回串</param>
        /// <param name="millSecond">超时时间设置，毫秒</param>
        /// <returns>是否读取成功</returns>
        private bool Write(string cmd, ref string recvStr, int milliSecond)
        {
            Timeout timeout = new Timeout();
            timeout.Do = Write;
            recvStr = string.Empty;
            bool rt = timeout.DoWithTimeout(cmd, ref recvStr, new TimeSpan(0, 0, 0, 0, milliSecond));

            return rt;
        }
        #endregion
        #region method        
        private bool Init()
        {
            bool rect = false;
            try
            {                
                SoftCtrl();
                GetOpenStat();
                rect = ReadCalib();      //20181213，顾叶俊，判断是否未上电          
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool SoftCtrl()
        {
            bool rect = false;
            try
            {
                string cmd = "4041424358585858";    //@ABCXXXX
                string outStr = string.Empty;
                if (Write(cmd, ref outStr))
                {
                    rect = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private bool ReadCalib()
        {
            bool rect = false;
            try
            {
                string cmd = "4052414158585858";   //@RAAXXXX
                string outStr = string.Empty;
                if (Write(cmd, ref outStr))
                {
                    //获取基础参数                    
                    basicPara = SplitPara(outStr);
                    //判断设备是否未上电，20181213，顾叶俊
                    if (basicPara.Load == 0 && basicPara.MaxShift == 0 &&
                        basicPara.MaxVoltage == 0 && basicPara.MinShift == 0 &&
                        basicPara.MinVoltage == 0 && basicPara.Power == 0)
                    {
                        rect = false;
                    }
                    else
                    {
                        rect = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        private PIZParaEntity SplitPara(string strData)
        {
            PIZParaEntity rect = new PIZParaEntity();
            try
            {
                List<string> lstSplit = new List<string>();
                int max = strData.Length / 14;
                for(int i=0;i<max;i++)
                {
                    lstSplit.Add(strData.Substring(14 * i, 14));
                }
                int HBit = 0;
                int LBit = 0;
                foreach(var v  in lstSplit)
                {
                    switch(v.Substring(0,4))
                    {
                        case "4041":
                            //最小电压
                            HBit = Convert.ToInt16(v.Substring(4, 4), 16);
                            LBit = Convert.ToInt16(v.Substring(8,4), 16);
                            if(HBit > 0 || LBit>0)
                            {
                                rect.MinVoltage = float.Parse(string.Format("{0}.{1}",HBit,LBit));
                            }
                            break;
                        case "4042":
                            //最大电压
                            HBit = Convert.ToInt16(v.Substring(4, 4), 16);
                            LBit = Convert.ToInt16(v.Substring(8, 4), 16);
                            if (HBit > 0 || LBit > 0)
                            {
                                rect.MaxVoltage = float.Parse(string.Format("{0}.{1}", HBit, LBit));
                            }                            
                            break;
                        case "4043":
                            //最大位移
                            HBit = Convert.ToInt16(v.Substring(4, 4), 16);
                            LBit = Convert.ToInt16(v.Substring(8, 4), 16);
                            if (HBit > 0 || LBit > 0)
                            {
                                rect.MaxShift = float.Parse(string.Format("{0}.{1}", HBit, LBit));
                            }
                            break;
                        case "4044":
                            //负载
                            HBit = Convert.ToInt16(v.Substring(4, 4), 16);
                            LBit = Convert.ToInt16(v.Substring(8, 4), 16);
                            if (HBit > 0 || LBit > 0)
                            {
                                rect.Load = float.Parse(string.Format("{0}.{1}", HBit, LBit));
                            }
                            break;
                        case "4045":
                            //最小位移
                            HBit = Convert.ToInt16(v.Substring(4, 4), 16);
                            LBit = Convert.ToInt16(v.Substring(8, 4), 16);
                            if (HBit > 0 || LBit > 0)
                            {
                                rect.MinShift = float.Parse(string.Format("{0}.{1}", HBit, LBit));
                            }
                            break;
                        case "4046":
                            //功率
                            HBit = Convert.ToInt16(v.Substring(4, 4), 16);
                            LBit = Convert.ToInt16(v.Substring(8, 4), 16);
                            if (HBit > 0 || LBit > 0)
                            {
                                rect.Power = float.Parse(string.Format("{0}.{1}", HBit, LBit));
                            }
                            break;
                    }
                }

            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        /// <summary>
        /// 获取开闭环状态
        /// </summary>
        /// <returns></returns>
        private bool GetOpenStat()
        {
            bool rect = false;
            try
            {
                string cmd = "4052433158585858";  //@RC1XXXX`
                string outStr = string.Empty;
                if (Write(cmd, ref outStr))
                {
                    //判断开闭环
                    if(outStr.Length >13)
                    {                        
                        bCloseLoopMode = outStr.Substring(6, 2) == "00" ? true : false;
                    }
                    rect = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }

        private bool MoveAxis(float dist)
        {
            bool rect = false;
            try
            {
                string cmd = string.Empty; //@RT
                int Hbit = 0;
                int Lbit = 0;
                int pIndex = 0;
                dist = (float)Math.Round(dist, 4);
                if(bCloseLoopMode)
                {
                    //闭环模式
                    if (dist > basicPara.MaxShift)
                    {
                        LogHelper.AppLoger.ErrorFormat("XinMingT PIZ dist is too long .value :{0}",dist);
                        return rect;
                    }
                    if(dist.ToString().Contains("."))
                    {                        
                        pIndex = dist.ToString().IndexOf(".");
                        Hbit = int.Parse(dist.ToString().Substring(0, pIndex));
                        Lbit = int.Parse(dist.ToString().Substring(pIndex + 1, dist.ToString().Length - pIndex - 1).PadRight(3,'0'));
                        //Lbit *= 10; 
                    }
                    else
                    {
                        Hbit = (int)dist;
                        Lbit = 0;
                    }
                    //set to command
                    cmd = string.Format("40540053{0}{1}",Hbit.ToString("X").PadLeft(4,'0'),Lbit.ToString("X").PadLeft(4,'0'));
                }
                else
                {
                    //开环模式
                    if (dist > basicPara.MaxVoltage)
                    {
                        LogHelper.AppLoger.ErrorFormat("XinMingT PIZ dist is too long .value :{0}", dist);
                        return rect;
                    }

                    if (dist.ToString().Contains("."))
                    {
                        pIndex = dist.ToString().IndexOf(".");
                        Hbit = int.Parse(dist.ToString().Substring(0, pIndex));
                        Lbit = int.Parse(dist.ToString().Substring(pIndex+1, dist.ToString().Length - pIndex-1).PadRight(3,'0'));
                        //Lbit *= 10;
                    }
                    else
                    {
                        Hbit = (int)dist;
                        Lbit = 0;
                    }
                    //set to command
                    cmd = string.Format("40540056{0}{1}", Hbit.ToString("X").PadLeft(4, '0'), Lbit.ToString("X").PadLeft(4, '0'));
                }
                string outStr = string.Empty;
                if (Write(cmd, ref outStr))
                {                    
                    rect = true;
                }
            }
            catch(Exception ex )
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        public bool getAxisName()
        {
            throw new NotImplementedException();
        }

        public bool MoveToAbsPos(double dbPos)
        {
            throw new NotImplementedException();
        }

        public bool MoveToRelativePos(double dbPos)
        {
            throw new NotImplementedException();
        }

        public int GetAxisStatus()
        {
            throw new NotImplementedException();
        }

        public bool bIsOnTarget()
        {
            throw new NotImplementedException();
        }
        public bool StopAll()
        {
            throw new NotImplementedException();
        }

        public bool GetAxisName()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    /// <summary>
    /// 串口读取委托，20181213，顾叶俊
    /// </summary>
    public delegate bool DoHandler(string cmd, ref string recvStr);
    /// <summary>
    /// 串口读取超时判断类，20181213，顾叶俊
    /// </summary>
    public class Timeout
    {
        private ManualResetEvent mTimeoutObject;
        //标记变量
        private bool mBoTimeout;
        public DoHandler Do;



        public Timeout()
        {
            //  初始状态为 停止
            this.mTimeoutObject = new ManualResetEvent(true);
        }
        ///<summary>
        /// 指定超时时间 异步执行某个方法
        ///</summary>
        ///<returns>执行 是否超时</returns>
        public bool DoWithTimeout(string cmd, ref string recvStr, TimeSpan timeSpan)
        {
            if (this.Do == null)
            {
                return false;
            }

            this.mTimeoutObject.Reset();
            this.mBoTimeout = true; //标记
            //this.Do.BeginInvoke(DoAsyncCallBack, null);
            this.Do.BeginInvoke(cmd, ref recvStr, DoAsyncCallBack, null);
            // 等待 信号Set
            if (!this.mTimeoutObject.WaitOne(timeSpan, false))
            {
                this.mBoTimeout = true;
            }
            return this.mBoTimeout;
        }
        ///<summary>
        /// 异步委托 回调函数
        ///</summary>
        ///<param name="result"></param>
        private void DoAsyncCallBack(IAsyncResult result)
        {
            try
            {
                this.Do.EndInvoke(ref XinMingTianCtrl.recvStr, result);
                // 指示方法的执行未超时
                this.mBoTimeout = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.mBoTimeout = true;
            }
            finally
            {
                this.mTimeoutObject.Set();
            }
        }
    }
}
