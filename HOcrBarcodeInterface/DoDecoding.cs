using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
namespace HOcrBarcodeInterface
{
    public class DoDecoding : IOcrBarcode
    {
        #region instance
        //Singleton instance
        private static DoDecoding _instance;
        public static DoDecoding Instance => _instance ?? (_instance = new DoDecoding());
        public DoDecoding()
        {

        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ImgData"></param>            位图图像数据首地址
        /// <param name="iImgWidth"></param>          输入的位图数据宽度
        /// <param name="iImgHeight"></param>         输入的位图数据高度
        /// <param name="iMode"></param>              读取模式（0，OCR+Barcode;1,只读OCR;2,只读Barcode)
        /// <param name="dbRow"></param>              用来读取OCR的旋转矩形中心行坐标
        /// <param name="dbCol"></param>              用来读取OCR的旋转矩形中心列坐标
        /// <param name="dbPhi"></param>              用来读取OCR的旋转矩形角度（-180~180）
        /// <param name="dbLen1"></param>             用来读取OCR的旋转矩形的长边/2
        /// <param name="dbLen2"></param>             用来读取OCR的旋转矩形的短边/2
        /// <param name="iFontWidth"></param>         要读取的字符字体宽度
        /// <param name="iFontHeight"></param>        要读取的字符字体高度
        /// <param name="dbStrokeWidth"></param>      要读取的字符字体笔画宽度
        /// <param name="iMaxLineNum"></param>        要读取的字符内容最大行数
        /// <param name="chomcfilepath"></param>      用来读取OCR的Halcon字库路径
        /// <param name="iCodeType"></param>          要读取的一维码类型（0,'auto';1,'2/5 Industrial';2,'2/5 Interleaved';3,'Codabar';4,'Code39';5,'Code93';6,'Code128';7,'EAN-13';8,'EAN-8')
        /// <param name="sOcrDecodeString"></param>   返回OCR结果
        /// <param name="sBarcodeString"></param>     返回一维码结果
        /// <param name="strErrMsg"></param>          返回错误信息
        /// <returns></returns>
        public int IDoOcrandReadCode(IntPtr ImgData, int iImgWidth, int iImgHeight, int iMode, double dbRow, double dbCol, double dbPhi, double dbLen1, double dbLen2,
    int iFontWidth, int iFontHeight, double dbStrokeWidth, int iMaxLineNum, string chomcfilepath, int iCodeType, ref string sOcrDecodeString, ref string sBarcodeString, ref string strErrMsg)
        {
            int iRet = 0;
            byte[] OcrResult = new byte[128];
            byte[] BarCodeRet = new byte[128];
            byte[] ErrorMsg = new byte[256];
            try
            {
                //string sOmcFilePath = "D:\\Program Files\\MVTec\\HALCON-12.0\\ocr\\Industrial_NoRej.omc";             
                if (OcrBarcodeFun.bHDecoding(ImgData, iImgWidth, iImgHeight, iMode, dbRow, dbCol, dbPhi, dbLen1, dbLen2, iFontWidth, iFontHeight, dbStrokeWidth, iMaxLineNum, chomcfilepath, iCodeType, ref OcrResult[0], ref BarCodeRet[0], ref ErrorMsg[0]))
                {
                    sOcrDecodeString = System.Text.Encoding.Default.GetString(OcrResult, 0, OcrResult.Length).TrimEnd('\0');
                    sBarcodeString = System.Text.Encoding.Default.GetString(BarCodeRet, 0, BarCodeRet.Length).TrimEnd('\0');
                }
                else
                {
                    iRet = -1;
                }
            }
            catch (Exception ex)
            {
                iRet = -1;
                strErrMsg = System.Text.Encoding.Default.GetString(ErrorMsg, 0, ErrorMsg.Length);
                LogHelper.AppLoger.Error(ex);
            }
            finally
            {

            }
            return iRet;
        }
    }
}
