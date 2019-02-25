using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatlabHelper
{
    public interface IMFocus
    {
        bool Init();
        float MatlabPeek(List<float> zPosList, List<byte[,]> byteList, int maxCnt);
        float MatlabQPeek(List<float> zposList, List<byte[,]> bytelist, int max_cnt, ref float z_pos1, ref float z_pos2, ref float z_pos3, ref float new_z_pos_m, bool bDirty, ref int bFineFlag);
        byte[,] GetImageArray2D(Bitmap img);
        void MatlabFitting(Dictionary<Point, float> dicMap);
    }
}
