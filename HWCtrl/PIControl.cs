using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
namespace HWCtrl
{
    public class PIControl : IPIZ
    {
        //Singleton instance
        private static PIControl _instance;
        public static PIControl Instance => _instance ?? (_instance = new PIControl());
        private int m_iControllerId;
        private string m_sAxisName;
        private SystemConfig sysCfg = SystemConfig.Instance;
        public PIControl()
        {
            m_sAxisName = string.Empty;
            m_iControllerId = -1;
        }
        ~PIControl()
        {

        }
        #region define
        public event UpdateEventHander UpdateMessage;
        private bool IsUse;
        #endregion
        #region interface
        bool IPIZ.Open()
        {
            bool ret = true;
            try
            {
                string sErrMsg = string.Empty;
                string sIdInfo = string.Empty;             
                StringBuilder sIdn = new StringBuilder(1024);
                StringBuilder sErrorMessage = new StringBuilder(1024);
                int iError;
                //////////////////////////////////////////////////////////////////////////
                // connect to the controller over RS-232 (COM port 1, baudrate 115200). //
                //////////////////////////////////////////////////////////////////////////
                m_iControllerId = PI_GCS.ConnectRS232(sysCfg.PI_ComPort_Num, sysCfg.PI_ComBaudRate);
                if (m_iControllerId < 0)
                {
                    iError = PI_GCS.GetError(m_iControllerId);
                    PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                    sErrMsg = "ERROR From ConnectRS232 " + iError.ToString() + ": " + sErrorMessage.ToString();
                    ret = false;
                }
                else
                {
                    ////////////////////////////////////
                    // Get the IDeNtification string. //
                    ////////////////////////////////////
                    if (PI_GCS.qIDN(m_iControllerId, sIdn, sIdn.Capacity) == 0)
                    {
                        iError = PI_GCS.GetError(m_iControllerId);
                        PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                        sErrMsg = "ERROR From IDN? " + iError.ToString() + ": " + sErrorMessage.ToString();
                        ret = false;
                    }
                    else
                    {
                        sIdInfo = sIdn.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                ret = false;
            }
            return ret;
        }
        bool IPIZ.StopAll()
        {
            bool ret = true;
            string sErrMsg = string.Empty;
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            if (PI_GCS.STP(m_iControllerId) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From STP " + iError.ToString() + ": " + sErrorMessage.ToString();
                ret = false;
            }
            return ret;
        }
        bool IPIZ.Close()
        {
            if (m_iControllerId >= 0)
                PI_GCS.CloseConnection(m_iControllerId);
            return true;
        }
        bool IPIZ.GetAxisName()
        {
            bool ret = true;
            string sErrMsg = string.Empty;
            StringBuilder sAxes = new StringBuilder(1024);
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            /////////////////////////////////////////
            // Get the name of the connected axis. //
            /////////////////////////////////////////
            if (PI_GCS.qSAI(m_iControllerId, sAxes, sAxes.Capacity) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From SAI? " + iError.ToString() + ": " + sErrorMessage.ToString();
                ret = false;
            }
            m_sAxisName = sAxes.ToString();
            return ret;
        }
        bool IPIZ.Zero()
        {
            bool ret = true;
            string sErrMsg = string.Empty;
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dPos = new double[3];
            dPos[0] = 0;
            // call the MOV command (for closed servo loop).
            if (PI_GCS.MOV(m_iControllerId, m_sAxisName, dPos) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From MOV " + iError.ToString() + ": " + sErrorMessage.ToString();
                ret = false;
            }
            return ret;
        }
        float IPIZ.MaxRange()
        {
            throw new NotImplementedException();
        }
        float IPIZ.GetCurPos()
        {
            float fCurPos = 0f;
            string sErrMsg = string.Empty;
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dPos = new double[3];
            // call the command to querry the current POSition of axes.
            if (PI_GCS.qPOS(m_iControllerId, m_sAxisName, dPos) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From Pos?" + iError.ToString() + ": " + sErrorMessage.ToString();                         
            }
            fCurPos = (float)dPos[0];
            return fCurPos;
        }
        bool IPIZ.MoveToAbsPos(double dbPos)
        {
            bool ret = true;
            string sErrMsg = string.Empty;
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dPos = new double[3];
            // move all axes the corresponding position in 'dPos'
            dPos[0] = dbPos;
            // call the MOV command (for closed servo loop).
            if (PI_GCS.MOV(m_iControllerId, m_sAxisName, dPos) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From MOV " + iError.ToString() + ": " + sErrorMessage.ToString();
                ret = false;
            }
            return ret;
        }
        bool IPIZ.MoveToRelativePos(double dbPos)
        {
            bool ret = true;
            string sErrMsg = string.Empty;
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dPos = new double[3];
            dPos[0] = dbPos;
            if (PI_GCS.MVR(m_iControllerId, m_sAxisName, dPos) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "Error From MVR " + iError.ToString() + ": " + sErrorMessage.ToString();
                ret = false;
            }
            return ret;
        }
        int IPIZ.GetAxisStatus()
        {
            string sErrMsg = string.Empty;
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            ////////////////////////////////////////
            // Read the moving state of the axes. //
            ////////////////////////////////////////
            int[] bIsMoving = new int[3];
            // if 'axes' = NULL or 'axis' is empty a general moving state of all axes ist returnd in 'bIsMoving[0]'
            // if 'bIsMoving[0]' = TRUE at least one axis of the controller ist still moving.
            // if 'bIsMoving[0]' = FALSE no axis of the contrller is moving.
            //bIsMoving[0] = 1;
            // if 'axes != NULL and 'axis' is not empty the moving state of every axis in 'axes' is returned in
            // the arry bIsMoving.
            if (PI_GCS.IsMoving(m_iControllerId, m_sAxisName, bIsMoving) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From IsMoving " + iError.ToString() + ": " + sErrorMessage.ToString();
                return -1;
            }
            return bIsMoving[0];
        }
        bool IPIZ.bIsOnTarget()
        {
            bool ret = false;
            string sErrMsg = string.Empty;
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            ////////////////////////////////////////
            // Read the moving state of the axes. //
            ////////////////////////////////////////
            int[] bIsOnTarget = new int[3];          
            if (PI_GCS.qONT(m_iControllerId, m_sAxisName, bIsOnTarget) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From IsMoving " + iError.ToString() + ": " + sErrorMessage.ToString();
                ret = false;
            }
            else
                ret = bIsOnTarget[0] == 1 ? true : false;
            return ret;
        }    
        bool IPIZ.SetDistance(float distance)
        {
            throw new NotImplementedException();
        }
        void IPIZ.SetCloseMode(bool isCloseLoop)
        {
            string sErrMsg = string.Empty;
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            int[] bFlags = new int[3];
            // Switch on the Servo for all axes
            // Set servo-control "on" or "off" (closed-loop/open-loop mode)
            bFlags[0] = isCloseLoop == true ? 1 : 0;// servo on for the axis in the string 'axes'.
            // call the SerVO mode command.
            if (PI_GCS.SVO(m_iControllerId, m_sAxisName, bFlags) == 0)
            {
                iError = PI_GCS.GetError(m_iControllerId);
                PI_GCS.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From SVO " + iError.ToString() + ": " + sErrorMessage.ToString();
            }
            return;
        }
        public bool GetCloseMode()
        {
            throw new NotImplementedException();
        }
        public PIZParaEntity GetPara()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
