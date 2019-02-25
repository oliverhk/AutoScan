using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIControl
{
    public interface IPIGCS
    {
        bool IsConnected(int iControllerID);
        bool Connect(ref string sErrMsg, ref string sIdInfo);
        void Disconnect();
        void RebootSystem(ref string sErrMsg);
        void GetAxisName(ref string axes, ref string sErrMsg);
        void SetLoopMode(string axes, ref string sErrMsg, bool bIsCloseModeOn);
        double GetCurrentPos(string axes, ref string sErrMsg);
        int IsAxisMoving(string axes, ref string sErrMsg);
        //void HaltStop(string axes, ref string sErrMsg);
        void StopAll(ref string sErrMsg);
        void MoveToAbsolutePos(string axes, double dbAbsPos, ref string sErrMsg);
        void MoveToRelativePos(string axes, double dbRelPos, ref string sErrMsg);
        //void MoveToAbsPosbyVector(string axes, double dbAbsPos, ref string sErrMsg);
        void GoHome(string axes, ref string sErrMsg);
        //void DefineHome(string axes, ref string sErrMsg);
        //void SetCloseAcc(string axes, double acc, ref string sErrMsg);
        //void SetCloseDec(string axes, double dec, ref string sErrMsg);
        //void ControlVelocity(string axes, bool bIsOn, ref string sErrMsg);
        void SetVelocity(string axes, double dbvel, ref string sErrMsg);     
        //void SetStartSpeed(string axes, double dbVel, ref string sErrMsg);   
        //void TurnSoftLimit(string axes, bool bIsActivated, ref string sErrMsg);       
        //void SetLowSoftLimit(string axes, double dbL, ref string sErrMsg);      
        //void SetHighSoftLimit(string axes, double dbH, ref string sErrMsg);
        //void FastMoveToLSL(string axes, ref string sErrMsg);
        //void FasetMoveToHSL(string axes, ref string sErrMsg);
    }
}
