using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.Model
{
    public class ControlRecipe
    {
        public int CtrlRecipeId { get; set; }
        public int RecipeId { get; set; }
        public float CamDelayTime { get; set; }
        public float CamPlusTime { get; set; }
        public float LightDelayTime { get; set; }
        public float LightPlusTime { get; set; }
    }
}
