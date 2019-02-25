using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class Types
    {
        public int TypeId { get; set; }
        public int CateId { get; set; }
        public string CellType { get; set; }
        public string CellCode { get; set; }
        public string Description { get; set; }
        public string ColorStr { get; set; }
    }
}
