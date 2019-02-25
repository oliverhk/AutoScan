using MathWorks.MATLAB.NET.Arrays;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace MatlabHelper
{
    public class FocusHelper : IMFocus
    {
        #region property
        private GfPremap.GfPremap gfPrep;
        private QuickFocus.QuickFocus qfprep;
        #endregion
        #region instance
        //Singleton instance
        private static FocusHelper _instance;
        public static FocusHelper Instance => _instance ?? (_instance = new FocusHelper());
        public FocusHelper()
        {

        }
        #endregion
        public bool Init()
        {
            bool rect = false;
            try
            {
                gfPrep = new GfPremap.GfPremap();
                qfprep = new QuickFocus.QuickFocus();
                rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }

        #region method
        public float MatlabQPeek(List<float> zposList, List<byte[,]> bytelist, int max_cnt, ref float z_pos1, ref float z_pos2, ref float z_pos3, ref float new_z_pos_m, bool bDirty, ref int bFineFlag)
        {
            DateTime starT = DateTime.Now;
            int loop_cnt = 0;
            float rect_cnt = 0;
            float rect = 0;
            try
            {
                MWCellArray mwcell = new MWCellArray(max_cnt, 1);
                for (int i = 0; i < max_cnt; i++)
                {
                    MWNumericArray mwarr = bytelist[i];
                    mwcell[i + 1, 1] = mwarr;
                }
                MWNumericArray mwz = zposList.ToArray();
                loop_cnt = bDirty == false ? 0 : 1;
                MWNumericArray mwCnt = loop_cnt;
                //MWArray mwret = this.qFocus.QuickFocusOfSinglePointROIForDll(mwz, mwcell);
                MWArray mwret = this.qfprep.QuickFocusOfSinglePointROIForDll(mwz, mwcell, mwCnt);
                float[,] ret = (float[,])((MWNumericArray)mwret).ToArray(MWArrayComponent.Real);
                z_pos1 = ret[0, 0];
                z_pos2 = ret[0, 1];
                z_pos3 = ret[0, 2];
                new_z_pos_m = ret[0, 3];
                bFineFlag = (int)ret[0, 4];
                z_pos1 = z_pos1 > 0.4 ? 0 : z_pos1;
                z_pos2 = z_pos2 > 0.4 ? 0 : z_pos2;
                z_pos3 = z_pos3 > 0.4 ? 0 : z_pos3;
                if (rect_cnt == 1)
                {
                    loop_cnt++;
                }
                else
                {
                    loop_cnt = 0;
                }
                rect = new_z_pos_m;
            }
            catch (Exception ex)
            { LogHelper.AppLoger.Error(ex); }
            finally
            {

            }
            return rect;
        }
        public float MatlabPeek(List<float> zPosList, List<byte[,]> byteList, int maxCnt)
        {
            float rect = 0;
            try
            {
                MWCellArray mwcell = new MWCellArray(maxCnt, 1);
                for (int i = 0; i < maxCnt; i++)
                {
                    MWNumericArray mwarr = byteList[i];
                    mwcell[i + 1, 1] = mwarr;
                }
                MWNumericArray mwz = zPosList.ToArray();
                MWArray mwret = this.gfPrep.GlobalFocusOfSinglePointROIForDll(mwz, mwcell);
                float[,] ret = (float[,])((MWNumericArray)mwret).ToArray(MWArrayComponent.Real);
                float new_z_pos_m = ret[0, 0];
                rect = new_z_pos_m;
            }
            catch (Exception ex)
            { LogHelper.AppLoger.Error(ex); }
            return rect;
        }
        /// <summary>
        /// need to modified zhian 0705
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public byte[,] GetImageArray2D(Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            byte[,] grayData = new byte[width, height];
            try
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        grayData[i, j] = img.GetPixel(j, i).B;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return grayData;
        }
        public void MatlabFitting(Dictionary<Point, float> dicMap)
        {
            try
            {
                int max = dicMap.Count;
                double[,] dbx = new double[max, 3];
                MWCellArray mwary = new MWCellArray(max, 3);
                List<Point> lst = new List<Point>(dicMap.Keys);
                StringBuilder strDat = new StringBuilder();
                for (int i = 0; i < lst.Count; i++)
                {
                    Point p = lst[i];
                    float v = dicMap[p];
                    //float[] fv = new float[3] { (float)p.X, (float)p.Y, v };
                    dbx[i, 0] = (double)p.X;
                    dbx[i, 1] = (double)p.Y;
                    dbx[i, 2] = (double)v;
                    strDat.AppendFormat("{0},{1},{2}", p.X, p.Y, v);
                    strDat.AppendFormat(System.Environment.NewLine);
                }

                //save result to file
                string path = @"c:\map\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                FileStream fs = new FileStream(path + "final_data.csv", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, utf8WithoutBom);
                sw.Write(strDat.ToString());
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();

                MWNumericArray x = new MWNumericArray(dbx);
                MWArray mwret = this.gfPrep.PreMapFunction(x);
                //float[,] ret = (float[,])((MWNumericArray)mwret).ToArray(MWArrayComponent.Real);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion
    }
}
