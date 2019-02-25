using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIControl
{
    public class PI: IPIGCS
    {
        #region instance
        //Singleton instance
        private static PI _instance;
        public static PI Instance => _instance ?? (_instance = new PI());
        protected int m_iControllerId;
        public PI()
        {
            m_iControllerId = -1;
        }
        #endregion
        public bool IsConnected(int iControllerID)
        {
            bool bRet = false;
            bRet = PICommand.IsConnected(iControllerID) == 0 ? false : true;
            return bRet;
        }
        public bool Connect(ref string sErrMsg, ref string sIdInfo)
        {
            bool bRet = true;
            StringBuilder sIdn = new StringBuilder(1024);
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            //////////////////////////////////////////////////////////////////////////
            // connect to the controller over RS-232 (COM port 1, baudrate 115200). //
            //////////////////////////////////////////////////////////////////////////
            m_iControllerId = PICommand.ConnectRS232(1, 115200);
            if (m_iControllerId < 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From ConnectRS232 " + iError.ToString() + ": " + sErrorMessage.ToString();
                bRet = false;
            }
            else
            {
                ////////////////////////////////////
                // Get the IDeNtification string. //
                ////////////////////////////////////
                if (PICommand.qIDN(m_iControllerId, sIdn, sIdn.Capacity) == 0)
                {
                    iError = PICommand.GetError(m_iControllerId);
                    PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                    sErrMsg = "ERROR From IDN? " + iError.ToString() + ": " + sErrorMessage.ToString();
                    bRet = false;
                }
                else
                {
                    sIdInfo = sIdn.ToString();
                }
            }
            return bRet;
        }
        public void Disconnect()
        {
            if (m_iControllerId >= 0)
                PICommand.CloseConnection(m_iControllerId);
        }
        public void RebootSystem(ref string sErrMsg)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            if (PICommand.RBT(m_iControllerId) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From RBT " + iError.ToString() + ": " + sErrorMessage.ToString();
            }
        }
        public void GetAxisName(ref string axes, ref string sErrMsg)
        {
            StringBuilder sAxes = new StringBuilder(1024);
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;


            /////////////////////////////////////////
            // Get the name of the connected axis. //
            /////////////////////////////////////////
            if (PICommand.qSAI(m_iControllerId, sAxes, sAxes.Capacity) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From SAI? " + iError.ToString() + ": " + sErrorMessage.ToString();
                return;
            }
            axes = sAxes.ToString();
        }
        public void SetLoopMode(string axes, ref string sErrMsg, bool bIsCloseModeOn)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            int[] bFlags = new int[3];
            // Switch on the Servo for all axes
            // Set servo-control "on" or "off" (closed-loop/open-loop mode)
            bFlags[0] = bIsCloseModeOn == true ? 1 : 0;// servo on for the axis in the string 'axes'.
            // call the SerVO mode command.
            if (PICommand.SVO(m_iControllerId, axes, bFlags) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From SVO " + iError.ToString() + ": " + sErrorMessage.ToString();
                return;
            }
        }
        public double GetCurrentPos(string axes, ref string sErrMsg)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dPos = new double[3];
            // call the command to querry the current POSition of axes.
            if (PICommand.qPOS(m_iControllerId, axes, dPos) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From Pos?" + iError.ToString() + ": " + sErrorMessage.ToString();
                return 0.0;
            }
            return dPos[0];
        }
        public int IsAxisMoving(string axes, ref string sErrMsg)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            ////////////////////////////////////////
            // Read the moving state of the axes. //
            ////////////////////////////////////////
            int[] bIsMoving = new int[3];
            // if 'axes' = NULL or 'axis' is empty a general moving state of all axes ist returnd in 'bIsMoving[0]'
            // if 'bIsMoving[0]' = TRUE at least one axis of the controller ist still moving.
            // if 'bIsMoving[0]' = FALSE no axis of the contrller is moving.
            bIsMoving[0] = 1;
            // if 'axes != NULL and 'axis' is not empty the moving state of every axis in 'axes' is returned in
            // the arry bIsMoving.
            if (PICommand.IsMoving(m_iControllerId, axes, bIsMoving) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From IsMoving " + iError.ToString() + ": " + sErrorMessage.ToString();
                return -1;
            }
            return bIsMoving[0];
        }
        public void StopAll(ref string sErrMsg)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            if (PICommand.STP(m_iControllerId) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From STP " + iError.ToString() + ": " + sErrorMessage.ToString();
            }
        }
        /// <summary>
        /// pi stages doesn't have physical origin
        /// </summary>
        /// <param name="axes"></param>
        /// <param name="sErrMsg"></param>
        public void GoHome(string axes, ref string sErrMsg)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dPos = new double[3];         
            dPos[0] = 0;
            // call the MOV command (for closed servo loop).
            if (PICommand.MOV(m_iControllerId, axes, dPos) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From MOV " + iError.ToString() + ": " + sErrorMessage.ToString();
            }
        }
        public void MoveToAbsolutePos(string axes, double dbAbsPos, ref string sErrMsg)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dPos = new double[3];
            // move all axes the corresponding position in 'dPos'
            dPos[0] = dbAbsPos;
            // call the MOV command (for closed servo loop).
            if (PICommand.MOV(m_iControllerId, axes, dPos) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From MOV " + iError.ToString() + ": " + sErrorMessage.ToString();
            }
        }
        public void MoveToRelativePos(string axes, double dbRelPos, ref string sErrMsg)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dPos = new double[3];
            dPos[0] = dbRelPos;
            if (PICommand.MVR(m_iControllerId, axes, dPos) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "Error From MVR " + iError.ToString() + ": " + sErrorMessage.ToString();
            }
        }     
        public void SetVelocity(string axes, double dbvel, ref string sErrMsg)
        {
            StringBuilder sErrorMessage = new StringBuilder(1024);
            int iError;
            double[] dbVelocity = new double[3];
            dbVelocity[0] = dbvel;
            if (PICommand.VEL(m_iControllerId, axes, dbVelocity) == 0)
            {
                iError = PICommand.GetError(m_iControllerId);
                PICommand.TranslateError(iError, sErrorMessage, sErrorMessage.Capacity);
                sErrMsg = "ERROR From VEL " + iError.ToString() + ": " + sErrorMessage.ToString();
            }
        }
      
    }
}
