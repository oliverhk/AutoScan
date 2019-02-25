using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class Recipes
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CreateBy { get; set; }
        public int IsDefault { get; set; }
    }
}
