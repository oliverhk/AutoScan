using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Entity
{
    [Serializable]
    public class ParaEntity
    {
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public float StartPost { get; set; }
        public float Count { get; set; }
        public float Gap { get; set; }        
    }
}
