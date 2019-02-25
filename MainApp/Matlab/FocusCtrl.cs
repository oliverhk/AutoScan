using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using CommonLibrary;
using Utility;
using MathWorks.MATLAB.NET.Arrays;
using GetPeak;

namespace MainApp.Matlab
{
    public class FocusCtrl
    {
        private GetPeak.MGetPeak myDll;
        private GlobalFocusOfSinglePointROIForDll.Class1 myDll2;

        #region instance

        //Singleton instance
        private static FocusCtrl _instance;
        public static FocusCtrl Instance => _instance ?? (_instance = new FocusCtrl());
        public FocusCtrl()
        {
            myDll = new MGetPeak();
            myDll2 = new GlobalFocusOfSinglePointROIForDll.Class1();
        }

        #endregion

        #region method

        private Dictionary<double, double> dicValue = new Dictionary<double, double>();

        public double SetImage(double z_pos,Bitmap img)
        {
            double drec = 0d;
            try
            {
                byte[,] grayData = new byte[img.Height, img.Width];

                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        grayData[i, j] = img.GetPixel(j, i).B;                        
                    }
                }                
                Int32 stat = 1;
                MWNumericArray mwpos = z_pos;                
                MWNumericArray matrix = grayData;
                MWCharArray method = "BREN";
                int[] list = {0,0,256,256 };
                MWArray roi = new MWNumericArray(list);

                MWArray rect = myDll2.fmeasure(matrix,method, roi);
                
                double[,] Temp1 = (double[,])((MWNumericArray)rect).ToArray(MWArrayComponent.Real);
                dicValue.Add(z_pos,Temp1[0, 0]);
                drec = Temp1[0, 0];
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return drec;
        }

        public double GetPeak(Bitmap img)
        {
            double rect = 0;
            try
            {
                if (dicValue.Count == 0)
                    return rect;
                //for test
                byte[,] grayData = new byte[img.Height, img.Width];

                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        grayData[i, j] = img.GetPixel(j, i).B;
                    }
                }

                MWNumericArray matrix = grayData;

                double[] dx = new double[dicValue.Count];
                double[] dy = new double[dicValue.Count];
                List<double> lst = new List<double>(dicValue.Keys);
                for(int i=0;i<lst.Count;i++)
                {
                    dx[i] = lst[i];
                    dy[i] = dicValue[lst[i]];
                }

                MWArray mx = new MWNumericArray(dx);
                MWArray my = new MWNumericArray(dy);

                //MWArray mrect = myDll.GetPeak(mx,my, matrix);
                //MWArray mrect=myDll2.GlobalFocusOfSinglePointROIForDll(mx, my, matrix);

                //double[,] Temp1 = (double[,])((MWNumericArray)mrect).ToArray(MWArrayComponent.Real);                
                //rect = Temp1[0, 0];
                dicValue.Clear();
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }

        public double GetPeak2(Bitmap img)
        {
            double k = 0;
            double v = 0;
            foreach (KeyValuePair<double, double> kvp in dicValue)
            {
                double value = kvp.Value;
                if (value > v)
                {
                    v = value;
                    k = kvp.Key;
                }
            }
            return k;
        }
        
        #endregion
    }
}
