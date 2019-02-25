using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HOcrBarcodeInterface
{
    class OcrBarcodeFun
    {
        [DllImport("Decode.dll", EntryPoint = "bHDecoding", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool bHDecoding(IntPtr ImgData, int iImgWidth, int iImgHeight, int iDecodeMode, double dbRow, double dbCol, double dbPhi, double dbLen1, double dbLen2,
    int iFontWidth, int iFontHeight, double dbStrokeWidth, int iMaxLineNum, string chomcfilepath, int iCodeType, ref byte chOCRResult, ref byte chBarCodeRet, ref byte chErrMsg);
    }
}
