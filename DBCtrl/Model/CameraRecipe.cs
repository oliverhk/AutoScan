using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class CameraRecipe
    {
        public int CameraRecipeId { get; set; }
        public int RecipeId { get; set; }
        public string CameraType { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float ExposureTime { get; set; }
        public int TriggerSource { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdate { get; set; }
        public String CreateBy { get; set; }
        public float Gain { get; set; }
        public string ImagePath { get; set; }
        public int IsSaveImage { get; set; }
    }
}
