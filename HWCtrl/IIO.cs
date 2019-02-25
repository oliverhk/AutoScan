using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWCtrl
{
    public interface IIO
    {
        bool Open();
        bool Close();
        int ReadInput(int bit);
        int ReadInAll();
        int ReadOut(int bit);
        bool SetOutOn(int bit);
        bool SetOutOff(int bit);
        void SetOutIoAllOff();
        bool IStartOnOff();
        bool IStopOnOff();
        bool SetLightGreenOn();
        bool SetLightRedOn();
        bool SetLightOnAlways(bool isOnOff);
    }
}
