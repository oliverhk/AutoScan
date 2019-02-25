using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class Severities
    {
        public int SeverityId { get; set; }
        public string name { get; set; }
        public int ColorId { get; set; }
        public int LastUsed { get; set; }
    }
}
