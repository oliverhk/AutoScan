using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class CellAIResult
    {
        public int Id { get; set; }

        public string SectionName { get; set; }

        public string ImageName { get; set; }

        public int PixelX { get; set; }

        public int PixelY { get; set; }

        public double Score { get; set; }

        public int ScanId { get; set; }
    }
}
