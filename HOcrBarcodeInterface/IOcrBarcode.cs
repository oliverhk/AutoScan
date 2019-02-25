using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOcrBarcodeInterface
{
    public interface IOcrBarcode
    {
        int IDoOcrandReadCode(IntPtr ImgData, int iImgWidth, int iImgHeight, int iMode, double dbRow, double dbCol, double dbPhi, double dbLen1, double dbLen2,
    int iFontWidth, int iFontHeight, double dbStrokeWidth, int iMaxLineNum, string chomcfilepath, int iCodeType, ref string sOcrDecodeString, ref string sBarcodeString, ref string strErrMsg);
    }
}
