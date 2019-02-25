using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCtrl.Model
{
    public class Cell
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 切片编号
        /// </summary>
        public string SectionNo { get; set; }

        /// <summary>
        /// 细胞编号
        /// </summary>
        public string CellNo { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double GLat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double GLng { get; set; }

        /// <summary>
        /// 像素X
        /// </summary>
        public double PixelX { get; set; }

        /// <summary>
        /// 像素Y
        /// </summary>
        public double PixelY { get; set; }

        /// <summary>
        /// 微米X
        /// </summary>
        public double MicX { get; set; }

        /// <summary>
        /// 微米Y
        /// </summary>
        public double MicY { get; set; }

        /// <summary>
        /// 图像路径
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
