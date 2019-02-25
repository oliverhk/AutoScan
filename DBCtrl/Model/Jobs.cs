using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class Jobs
    {
        public long JobId { get; set; }
        public string Name { get; set; }
        public int RecipeId { get; set; }
        public string LotId { get; set; }
        public string BatchId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Operator { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public string EqptId { get; set; }
        public string EqptNo { get; set; }
        public string Hospital { get; set; }
        public string PatientInfo { get; set; }
    }
}
