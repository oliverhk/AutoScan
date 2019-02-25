using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWCtrl
{
    public interface ILight
    {
        bool InitSys();
        bool SetTriggerModel();
        bool SetCycle();
        bool SetDelayTime(int time, int channel);
        bool SetPlusTime(int time, int channel);
        bool SetDelayTime38(int time);
        bool SetPlusTime38(int time);
        bool SetSource();
        bool SetDirect();
        bool SetEnable();
        bool SetFilter(int time);
    }
}
