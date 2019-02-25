using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class ScanRecipe
    {
        public int InspRecipeId { get; set; }
        public int RecipeId { get; set; }
        public float ZeroX { get; set; }
        public float ZeroY { get; set; }
        public int SwathRows { get; set; }
        public int SwathColumns { get; set; }
        public float SwathWidth { get; set; }
        public float SwathHeight { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CreateBy { get; set; }
        public float ScanZPostion { get; set; }
    }
}
