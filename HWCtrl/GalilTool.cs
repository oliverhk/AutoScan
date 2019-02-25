/*Galil tool control ,version zr
 * create date 2017.12.26
 * create by changli.xiao
 * 
 * */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Utility;
namespace HWCtrl
{
    public class GalilTool:IMotor
    {
        #region envent
        public delegate void UpdateMessageHander(object msg);
        public static event UpdateMessageHander UpdateMessage;
        #endregion
        #region instance
        //Singleton instance
        private static GalilTool _instance;
        public static GalilTool Instance => _instance ?? (_instance = new GalilTool());
        #endregion

        #region property
        private string connIpAddr = "";
        private Galil.Galil gALIL;      //this connect
        private bool IsConnect;         // galil connect status.    
        private CommonLibrary.SystemConfig sysCfg = CommonLibrary.SystemConfig.Instance;    
        public string GalibVersion { get; set; }
        private System.Timers.Timer tmr_pos = new System.Timers.Timer();          // read x,y.pos timer.
        private DateTime lastGetPosTime;        //for remember last get pos time
        private const int MAX_STOP_TIME = 3;       //5 S
        private bool isInTimer = false;
        private float postion_x;
        private float postion_y;
        private float postion_z;
        /// <summary>
        /// lock对象X,20181213,顾叶俊
        /// </summary>
        private static readonly object SyncX = new object();
        /// <summary>
        /// lock对象Y,20181213，顾叶俊
        /// </summary>
        private static readonly object SyncY = new object();
        /// <summary>
        /// lock对象Z,20181213，顾叶俊
        /// </summary>
        private static readonly object SyncZ = new object();
        #endregion
        public GalilTool()
        { }
        ~GalilTool()
        {
            if (gALIL != null)
                gALIL = null;
        }
        #region Connect
        public bool Connect(string ipAddr)
        {
            bool ret = false;
            try
            {
                if (gALIL == null)               
                    gALIL = new Galil.Galil();
                if (tmr_pos != null)
                    tmr_pos.Stop();
                gALIL.address = ipAddr;
                connIpAddr = ipAddr;
                GalibVersion = gALIL.libraryVersion();
                string rec = gALIL.connection();
                if (rec.Contains(ipAddr))
                {
                    IsConnect = ret = true;
                    GalibVersion = rec;
                    gALIL.onMessage += new Galil.Events_onMessageEventHandler(g_onMessage);
                }
                else
                    IsConnect = ret = false;
                //set timer 
                SetTimer();

            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }        
        private bool Connect()
        {
            bool ret = false;
            try
            {
                if(!string.IsNullOrEmpty(connIpAddr))
                    return Connect(connIpAddr);
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public void Disconnect()
        {
            gALIL = null;
        }
        #endregion
        #region timer 
        private void SetTimer()
        {
            tmr_pos = new System.Timers.Timer(1000);
            tmr_pos.Elapsed += tmr_Elapsed;
            //tmr_pos.Start();
        }
        private void tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (isInTimer)
                    return;
                isInTimer = true;
                string rect = string.Empty;
                
                rect = ReadCommand("TPA");
                postion_x = DataUtility.ParseFloat(rect);

                rect = ReadCommand("TPB");
                postion_y = DataUtility.ParseFloat(rect);

                rect = ReadCommand("TPC");
                postion_z = DataUtility.ParseFloat(rect);

                CheckStopTime();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {
                isInTimer = false;
            }
        }
        private void RememberLastTime()
        {
            try
            {
                lastGetPosTime = DateTime.Now;
                if (!tmr_pos.Enabled)
                {
                    //tmr_pos.Start(); 201811213,不再用该Timer读取位置
                    LogHelper.AppLoger.Debug("galil postion timer start.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }

        }
        private void CheckStopTime()
        {
            try
            {
                TimeSpan useT = DateTime.Now - lastGetPosTime;
                if (useT.TotalSeconds > MAX_STOP_TIME)
                {
                    tmr_pos.Stop();
                    LogHelper.AppLoger.Debug("galil postion timer stop.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
        #region Control on event
        void g_onMessage(string message)
        { //handler for the onMessage event
            ThrowMessage(message);
        }
        #endregion

        #region read and write command
        private bool IsInRead = false;
        private bool IsInWrite = false;
        private string ReadCommand(string command)
        {
            string ret = string.Empty;
            if (IsInRead)
                return ret;
            IsInRead = true;
            try
            {
                if (gALIL == null)
                    Connect();
                //ret = gALIL.read(command);
                ret = gALIL.command(command, "\r", ":", true);
            }
            catch(Exception ex)
            { LogHelper.AppLoger.Error(ex); }
            finally { IsInRead = false; }
            return ret;
        }
        private string WriteCommand(string command)
        {
            string ret = string.Empty;
            if (IsInWrite)
                return ret;
            IsInWrite = true;
            try
            {
                //ret = gALIL.write(command+"\r");
                ret = gALIL.command(command, "\r", ":", true);
                LogHelper.AppLoger.DebugFormat("Galil command :{0}",command);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            finally { IsInWrite = false; }
            return ret;
        }
        #endregion
        #region method        
        public bool IsHome()
        {
            bool ret = false;
            try
            {
                string cm = WriteCommand("MG pHOME");
                if(DataUtility.ParseInt(cm.Trim())==100)                    
                    ret = true;

            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public bool ExecHome(int axis)
        {
            bool ret = false;
            try
            {
                if (gALIL == null)
                    return ret;
                switch(axis)
                {
                    case 1:
                        WriteCommand("XQ#HOMEX,1");
                        break;
                    case 2:
                        WriteCommand("XQ#HOMEY,2");
                        break;
                    case 3:
                        WriteCommand("XQ#HOMEZ,3");
                        break;                    
                }
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public void ExecHomeALL()
        {
            WriteCommand("XQ#HOMEALL");
        }
        public float GetAxisXPos()
        {
            RememberLastTime();
            return postion_x;            
        }
        public float GetAxisYPos()
        {
            RememberLastTime();
            return postion_y;
        }
        public float GetAxisZPos()
        {
            RememberLastTime();
            return postion_z;
        }
        public float GetAxisXPosUm()
        {
            float ret=-1;
            try
            {
                var p = postion_x * sysCfg.XCountUnit;
                ret = p==0?0:(float)Math.Round(p / 1000,0);
                RememberLastTime();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public float GetAxisYPosUm()
        {
            float ret = -1;
            try
            {
                var p = postion_y * sysCfg.YCountUnit;
                ret = p==0?0: (float)Math.Round(p / 1000,0);
                RememberLastTime();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public float GetAxisZPosUm()
        {
            float ret = -1;
            try
            {
                var p = postion_z * sysCfg.ZCountUnit;
                ret = p==0?0: (float)Math.Round(p / 1000,0);
                RememberLastTime();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        /// <summary>
        /// 获取X位置um，2081213，顾叶俊
        /// </summary>
        /// <returns></returns>
        public float GetAxisXPosUmEx()
        {
            lock (SyncX)
            {
                float ret = -1;
                try
                {
                    string rect = ReadCommand("TPA");
                    postion_x = DataUtility.ParseFloat(rect);
                    var p = postion_x * sysCfg.XCountUnit;
                    ret = (float)p / 1000;
                }
                catch (Exception ex)
                {
                    LogHelper.AppLoger.Error(ex);
                }
                return ret;
            }
        }

        /// <summary>
        /// 获取Y位置um，2081213，顾叶俊
        /// </summary>
        /// <returns></returns>
        public float GetAxisYPosUmEx()
        {
            lock (SyncY)
            {
                float ret = -1;
                try
                {
                    string rect = ReadCommand("TPB");
                    postion_y = DataUtility.ParseFloat(rect);
                    var p = postion_y * sysCfg.YCountUnit;
                    ret = (float)p / 1000;
                }
                catch (Exception ex)
                {
                    LogHelper.AppLoger.Error(ex);
                }
                return ret;
            }
        }
        /// <summary>
        /// 获取Z位置um，2081213，顾叶俊
        /// </summary>
        /// <returns></returns>
        public float GetAxisZPosUmEx()
        {
            lock (SyncZ)
            {
                float ret = -1;
                try
                {
                    string rect = ReadCommand("TPC");
                    postion_z = DataUtility.ParseFloat(rect);
                    var p = postion_z * sysCfg.ZCountUnit;
                    ret = (float)p / 1000;
                }
                catch (Exception ex)
                {
                    LogHelper.AppLoger.Error(ex);
                }
                return ret;
            }
        }

        public void StopAxis()
        {
            WriteCommand("ST");
        }
        public void LoadGlass()
        {
            string cmd = string.Format("pPOSY={0}", 0);
            WriteCommand(cmd);
            WriteCommand("XQ#MOVEY");
        }
        public void UnLoadGlass(float pos)
        {
            string cmd = string.Format("pPOSY={0}", pos);
            WriteCommand(cmd);
            WriteCommand("XQ#MOVEY");
        }
        public void SetSpeed(int axis,float speed)
        {
            string cmd = string.Empty;
            switch(axis)
            {
                case 1:
                    cmd = string.Format("pSPX={0}", speed);
                    WriteCommand(cmd);
                    WriteCommand("XQ#SPX");
                    break;
                case 2:
                    cmd = string.Format("pSPY={0}", speed);
                    WriteCommand(cmd);
                    WriteCommand("XQ#SPY");
                    break;
                case 3:
                    cmd = string.Format("pSPZ={0}", speed);
                    WriteCommand(cmd);
                    WriteCommand("XQ#SPZ");
                    break;
            }
        }
        public void Move(int axis,float dist)
        {
            string cmd = string.Empty;
            switch (axis)
            {
                case 1:
                    cmd = string.Format("pPOSX={0}", dist);
                    WriteCommand(cmd);
                    WriteCommand("XQ#MOVEX,4");
                    break;
                case 2:
                    cmd = string.Format("pPOSY={0}", dist);
                    WriteCommand(cmd);
                    WriteCommand("XQ#MOVEY,5");
                    break;
                case 3:                    
                    if(dist>sysCfg.Limit_Z)
                    {
                        return;
                    }
                    cmd = string.Format("pPOSZ={0}", dist);
                    WriteCommand(cmd);
                    WriteCommand("XQ#MOVEZ,6");
                    break;
            }
        }
        public void MoveWait(int axis, float dist)
        {
            string cmd = string.Empty;
            switch (axis)
            {
                case 1:
                    cmd = string.Format("pPOSX={0}", dist);
                    WriteCommand(cmd);
                    WriteCommand("XQ#MOVEX,4");
                    break;
                case 2:
                    cmd = string.Format("pPOSY={0}", dist);
                    WriteCommand(cmd);
                    WriteCommand("XQ#MOVEY,5");
                    break;
                case 3:                    
                    if (dist > sysCfg.Limit_Z)
                    {
                        return;
                    }
                    cmd = string.Format("pPOSZ={0}", dist);
                    WriteCommand(cmd);
                    WriteCommand("XQ#MOVEZ,6");
                    break;
            }
            IsOnPost(axis);
        }

        /// <summary>
        /// XY同动
        /// </summary>
        /// <param name="distX">X轴距离mm</param>
        /// <param name="distY">Y轴距离mm</param>
        /// <param name="needWait">是否需要等待XY运动完成</param>
        public void MoveXYAxis(float distX, float distY, bool needWait)
        {
            string cmd = string.Empty;

            cmd = string.Format("pPOSX={0}", distX);
            WriteCommand(cmd);
            WriteCommand("XQ#MOVEX,4");
            Thread.Sleep(20);
            cmd = string.Format("pPOSY={0}", distY);
            WriteCommand(cmd);
            WriteCommand("XQ#MOVEY,5");

            if (needWait)
            {
                IsOnPost(1);
                IsOnPost(2);
            }
        }
        private void IsOnPost(int axis)
        {            
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                //float curPos = GetAxisXPosUm() / 1000;20181213,顾叶俊
                float curPos = GetAxisXPosUmEx() / 1000;
                while (true)
                {
                    if(axis ==1 )
                    {
                        if (IsAxisRun(1))
                        {
                            Thread.Sleep(10);
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if(axis ==2)
                    {
                        if (IsAxisRun(2))
                        {
                            Thread.Sleep(10);
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if(axis ==3)
                    {
                        if (IsAxisRun(3))       //20181213，顾叶俊，统一逻辑，不要再取反
                        {
                            Thread.Sleep(10);
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }                    
                }               
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }            
        }

        public void XYToZero()
        {
            string cmd = string.Empty;
            cmd = string.Format("pPOSX={0}", 0);
            WriteCommand(cmd);
            WriteCommand("XQ#MOVEX,5");
            //System.Threading.Thread.Sleep(10);
            //while (true)
            //{
            //    if (!IsAxisRun(1))
            //        break;
            //    System.Threading.Thread.Sleep(10);
            //}
            cmd = string.Format("pPOSY={0}", 0);
            WriteCommand(cmd);            
            WriteCommand("XQ#MOVEY,6");
            //System.Threading.Thread.Sleep(10);
            //while (true)
            //{
            //    if (!IsAxisRun(2))
            //        break;
            //    System.Threading.Thread.Sleep(10);
            //}
        }
        public bool IsAxisRun(int axis)
        {
            string cmd = string.Empty;
            switch(axis)
            {
                case 1:
                    cmd = "MG _BGA";
                    break;
                case 2:
                    cmd = "MG _BGB";
                    break;
                case 3:
                    //cmd = "MG _BGC";
                    cmd = "MG pMOVEZOK";
                    break;
            }
            //20181213，顾叶俊：修改Z轴的逻辑，使调用统一
            if (axis == 1 || axis == 2)
                return Utility.DataUtility.ParseFloat(WriteCommand(cmd)) == 1 ? true : false;
            else
                return Utility.DataUtility.ParseFloat(WriteCommand(cmd)) == 0 ? true : false;
        }
        public void SetOca(string cmd)
        {
            WriteCommand(cmd);
            WriteCommand(cmd);
        }
        public void SetOcaOff()
        {
            WriteCommand("OCA=0");
            WriteCommand("OCA=0");
        }
        #endregion
        #region evt
        private void ThrowMessage(object msg)
        {
            if (UpdateMessage != null)
            {
                UpdateMessage(msg);
            }
        }
        #endregion
    }
}
