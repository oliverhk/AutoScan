using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public enum HeaderStatus
    {
        Acivate=0,
        NotDefine,        
        Error,
    }
    public struct AxisPostion
    {
        public float axis_x;
        public float axis_y;
        public float axis_z;              
    }
    public enum HWEvent
    {
        AxisXPos=0,
        AxisYPos,
        AxisZPos,
        AxisXHome,
        AxisYHome,
        AxisZHome,
        AxisQ6,
        AxisQ8,         
    }
    public struct HWStatus
    {
        public HWEvent evtName;
        public object retMessage;
    }
    public enum FormEvent
    {
        Header1Status,
        Header2Status,
        Header3Status,
        AxisXPos,
        AxisYPos,
        AxisZPos,
        SwathNo,
    }
    public struct FormMessage
    {
        public FormEvent evtType;
        public object evtMessage;
    }
}
