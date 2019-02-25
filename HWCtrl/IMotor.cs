using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWCtrl
{
    public interface IMotor
    {
        bool Connect(string ipAddr);
        void Disconnect();
        bool IsHome();
        bool ExecHome(int axis);
        void ExecHomeALL();
        float GetAxisXPos();
        float GetAxisYPos();
        float GetAxisZPos();
        float GetAxisXPosUm();
        float GetAxisYPosUm();
        float GetAxisZPosUm();
        float GetAxisXPosUmEx();
        float GetAxisYPosUmEx();
        float GetAxisZPosUmEx();
        void StopAxis();
        void LoadGlass();
        void UnLoadGlass(float pos);
        void SetSpeed(int axis, float speed);
        void Move(int axis, float dist);
        void MoveWait(int axis, float dist);
        void XYToZero();
        bool IsAxisRun(int axis);
        void SetOca(string cmd);
        void SetOcaOff();
        void MoveXYAxis(float distX, float distY, bool needWait);
    }
}
