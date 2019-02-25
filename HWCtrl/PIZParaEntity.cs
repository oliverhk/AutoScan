using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWCtrl
{
    public class PIZParaEntity
    {
        public PIZParaEntity()
        {
            MaxVoltage = 0;
            MinVoltage = 0;
            MaxShift = 0;
            MinShift = 0;
            Load = 0;
            Power = 0;
        }
        public float MaxVoltage { get; set; }
        public float MinVoltage { get; set; }
        public float MaxShift { get; set; }
        public float MinShift { get; set; }
        public float Load { get; set; }
        public float Power { get; set; }

    }
}
