using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWCtrl
{
    public delegate void UpdateEventHander(object msg);
    public interface IPIZ
    {
        bool Open();
        bool Close();
        bool StopAll();
        bool Zero();
        bool GetAxisName();     
        float MaxRange();
        float GetCurPos();
        bool MoveToAbsPos(double dbPos);
        bool MoveToRelativePos(double dbPos);
        int GetAxisStatus();
        bool bIsOnTarget();
        bool SetDistance(float distance);
        void SetCloseMode(bool isCloseLoop);
        bool GetCloseMode();
        PIZParaEntity GetPara();

        event UpdateEventHander UpdateMessage;
    }
}
