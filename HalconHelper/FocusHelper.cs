using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using HalconDotNet;

namespace HalconHelper
{
    public static class FocusHelper
    {
        private static bool CalculateEdgeGradient(HImage image, ref HTuple mean)
        {
            bool ret = false;
            try
            {
                if (image == null)
                {
                    return ret;
                }
                HTuple deviation;
                HObject imageEdgeAmplitude = image.SobelAmp("sum_abs", 3);
                HOperatorSet.Intensity(imageEdgeAmplitude, imageEdgeAmplitude, out mean, out deviation);
                //mean += deviation;
                ret = true;
            }
            catch (HalconException hex)
            {
                Trace.WriteLine(hex.GetErrorMessage(), "HALCON error");
            }
            return ret;
        }

        private static bool CalculateEdgeGradientEx(HImage image, ref HTuple mean)
        {
            bool ret = false;
            try
            {
                if (image == null)
                {
                    return ret;
                }
                HTuple usedThreshold = 0;
                HRegion thresholdRegion = image.BinaryThreshold("max_separability", "dark", out usedThreshold);
                HImage imageReduced = image.ReduceDomain(thresholdRegion);
                //imageReduced.WriteImage("bmp", 0, "C:\\Users\\nuc\\Desktop\\FocusImage\\BinaryThreshold.bmp"); save image for verify
                ret = CalculateEdgeGradient(imageReduced, ref mean);
            }
            catch(HalconException hex)
            {
                Trace.WriteLine(hex.GetErrorMessage(), "HALCON error");
            }
            return ret;
        }

        #region 全局聚焦

        public static int GetMaxGradient(IntPtr[] imgPtr, int imgCount, int width, int height)
        {
            int ret = 0;
            try
            {
                HImage image = null;
                HTuple gradientValue = 0.0;
                double maxGradient = 0.0;
                double tempGradient = 0.0;
                int maxZIndex = 0;

                for (int i = 0; i < imgCount; i++)
                {
                    image.GenImage1("byte", width, height, imgPtr[i]);
                    if (CalculateEdgeGradient(image, ref gradientValue) == false)
                    {
                        Trace.WriteLine("CalculateEdgeGradient error");
                        return -1;
                    }

                    tempGradient = gradientValue.D;
                    if(tempGradient > maxGradient)
                    {
                        maxZIndex = i;
                        maxGradient = tempGradient;
                    }
                }
            }
            catch (HalconException hex)
            {
                ret = -1;
                Trace.WriteLine(hex.GetErrorMessage(), "HALCON error");
            }
            return ret;
        }

        private static void CalcParabolaVertex(float x1, float y1, float x2, float y2, float x3, float y3, ref float xv, ref float yv)
        {
            try
            {
                float denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
                float A = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
                float B = (x3 * x3 * (y1 - y2) + x2 * x2 * (y3 - y1) + x1 * x1 * (y2 - y3)) / denom;
                float C = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;
                xv = -B / (2 * A);  //x坐标
                yv = C - B * B / (4 * A);  //y坐标
            }
            catch (DivideByZeroException ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        public static float GlobalFocus(IntPtr[] imgPtr, float[] zPos, int imgCount, int width, int height)
        {
            float bestZPos = -1f;
            try
            {
                HImage image = new HImage();
                HTuple gradientValue = 0.0;
                HTuple gradientValueTuple = new HTuple();
                int maxZIndex = 0;
                int beginFitIndex = 0;
                double maxGradient = 0;
                double tempGradient = 0;

                for (int i = 0; i < imgCount; i++)
                {
                    image.GenImage1("byte", width, height, imgPtr[i]);
                    if (CalculateEdgeGradientEx(image, ref gradientValue) == false)
                    {
                        Trace.WriteLine("CalculateEdgeGradient error");
                        return -1;
                    }

                    gradientValueTuple = gradientValueTuple.TupleConcat(gradientValue);
                    tempGradient = gradientValue.D;
                    if (tempGradient > maxGradient)
                    {
                        maxZIndex = i;
                        maxGradient = tempGradient;
                    }
                    Trace.WriteLine(string.Format("{0}_{1}", zPos[i], gradientValue.D));
                }

                if (maxZIndex == 0)
                {
                    beginFitIndex = 1;
                }
                else if (maxZIndex == imgCount - 1)
                {
                    beginFitIndex = imgCount - 2;
                }
                else
                {
                    beginFitIndex = maxZIndex;
                }
                float xmax = 0.0f, ymax = 0.0f;
                CalcParabolaVertex(zPos[beginFitIndex - 1], gradientValueTuple[beginFitIndex - 1].F,
                                zPos[beginFitIndex], gradientValueTuple[beginFitIndex].F,
                                zPos[beginFitIndex + 1], gradientValueTuple[beginFitIndex + 1].F,
                                ref xmax, ref ymax);
                if (float.IsNaN(xmax))
                {
                    bestZPos = zPos[maxZIndex];
                    Trace.WriteLine("Focus error, choose current best Z!");
                }
                else
                {
                    bestZPos = xmax;
                }
            }
            catch(HalconException hex)
            {
                Trace.WriteLine(hex.GetErrorMessage(), "HALCON error");
            }
            return bestZPos;
        }
        #endregion

        #region 三点聚焦
        private static bool saveTempFiles = false;
        public static bool SaveTempFiles
        {
            get
            {
                return saveTempFiles;
            }
            set
            {
                saveTempFiles = value;
            }
        }

        private static bool preProcessEnable = false;
        public static bool PreProcessEnable
        {
            get
            {
                return preProcessEnable;
            }
            set
            {
                preProcessEnable = value;
            }
        }

        private static int loopCount = 0;
        /// <summary>
        /// 接口被调用的总次数
        /// </summary>
        public static int LoopCount
        {
            get
            {
                return loopCount;
            }
        }

        private static int coarseCount = 0;
        /// <summary>
        /// SFR在抛物线两侧时调用接口计数
        /// </summary>
        public static int CoarseCount
        {
            get
            {
                return coarseCount;
            }
        }

        private static int fineCount = 0;
        /// <summary>
        /// SFR在抛物线中间时调用接口计数
        /// </summary>
        public static int FineCount
        {
            get
            {
                return fineCount;
            }
        }

        private static int GetThreePointShapeType(HTuple sortedIndex)
        {
            int index = -1;

            HTuple hvRet = new HTuple();
            HTuple[] hv_SortedIndex = new HTuple[6];
            //1th.Input Value is xiao zhong da
            hv_SortedIndex[0] = hv_SortedIndex[0].TupleConcat(0);
            hv_SortedIndex[0] = hv_SortedIndex[0].TupleConcat(1);
            hv_SortedIndex[0] = hv_SortedIndex[0].TupleConcat(2);
            //2th.Input Value is da zhong xiao
            hv_SortedIndex[1] = hv_SortedIndex[1].TupleConcat(2);
            hv_SortedIndex[1] = hv_SortedIndex[1].TupleConcat(1);
            hv_SortedIndex[1] = hv_SortedIndex[1].TupleConcat(0);
            //3th.Input Value is zhong da xiao
            hv_SortedIndex[2] = hv_SortedIndex[2].TupleConcat(2);
            hv_SortedIndex[2] = hv_SortedIndex[2].TupleConcat(0);
            hv_SortedIndex[2] = hv_SortedIndex[2].TupleConcat(1);
            //4th.Input Value is xiao da zhong
            hv_SortedIndex[3] = hv_SortedIndex[3].TupleConcat(0);
            hv_SortedIndex[3] = hv_SortedIndex[3].TupleConcat(2);
            hv_SortedIndex[3] = hv_SortedIndex[3].TupleConcat(1);
            //5th.Input Value is zhong xiao da
            hv_SortedIndex[4] = hv_SortedIndex[4].TupleConcat(1);
            hv_SortedIndex[4] = hv_SortedIndex[4].TupleConcat(0);
            hv_SortedIndex[4] = hv_SortedIndex[4].TupleConcat(2);
            //6th.Input Value is da xiao zhong
            hv_SortedIndex[5] = hv_SortedIndex[5].TupleConcat(1);
            hv_SortedIndex[5] = hv_SortedIndex[5].TupleConcat(2);
            hv_SortedIndex[5] = hv_SortedIndex[5].TupleConcat(0);
            for (int i = 0; i < 6; i++)
            {
                index = i;
                hvRet = sortedIndex.TupleEqualElem(hv_SortedIndex[i]);
                if (hvRet[0] == 1 && hvRet[1] == 1 && hvRet[2] == 1)
                    break;
            }

            return index;
        }

        private static bool IsConvergent(HTuple hv_GradientTuple, double fStepGap)
        {
            bool ret = false;
            float xMax = 0.0f, yMax = 0.0f;

            CalcParabolaVertex((float)(curZPos - fStepGap), hv_GradientTuple[0].F,
                            (float)curZPos, hv_GradientTuple[1].F,
                            (float)(curZPos + fStepGap), hv_GradientTuple[2].F, 
                            ref xMax, ref yMax);
            if (hv_GradientTuple[1].D > fitPercent * yMax 
                && Math.Abs(curZPos - xMax) < fStepGap * fitPosDifPercent)
            {
                ret = true;
            }

            return ret;
        }

        private static int roiWidth = 256;
        private static int roiHeight = 256;
        private static double fitPercent = 20.0;  //收敛条件:当前中心位置对应的清晰度值占拟合抛物线峰值的百分比阈值（下限）
        private static double fitPosDifPercent = 1.0; //收敛条件:当前中心位置与拟合抛物线峰值对应的X轴位置之间的差异占景深的比例阈值(上限）

        private static double maxGradient = 0.0;
        private static double bestZCenPos = 0.0;  //未收敛情况下，获取记录的最佳Z位置
        public static double BestZCenPos
        {
            get
            {
                return bestZCenPos;
            }
        }

        private static double curZPos = 0.0;
        /// <summary>
        /// 当前Z位置
        /// </summary>
        public static double CurZPos
        {
            get
            {
                return curZPos;
            }
            set
            {
                curZPos = value;
            }
        }

        private static double curGap = 3 * 8.8;
        public static double CurGap
        {
            get
            {
                return curGap;
            }
            set
            {
                curGap = value;
            }
        }

        private static int direction = 0;
        public static int Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public static void QFocusInit(int RoiWidth, int RoiHeight, double FitPercent, double FitPosDifPercent)
        {
            roiWidth = RoiWidth;
            roiHeight = RoiHeight;
            fitPercent = FitPercent;
            fitPosDifPercent = FitPosDifPercent;
            bestZCenPos = 0.0;
            maxGradient = 0.0;
            curGap = 3 * 8.8;
            direction = 0;

            loopCount = 0;
            coarseCount = 0;
            fineCount = 0;
        }

        /// <summary>
        /// 三点聚焦
        /// </summary>
        /// <returns>焦点位置</returns>
        public static bool QFocus(IntPtr ImgData0, IntPtr ImgData1, IntPtr ImgData2)
        {
            bool ret = false;//是否收敛至最佳位置标志
            try
            {
                double stepGap = 2 * 8.8;
                HTuple gradientValue = 0;
                HTuple gradientValueTuple = new HTuple();
                HImage image0 = null;
                HImage image1 = null;
                HImage image2 = null;
                image0.GenImage1("byte", roiWidth, roiHeight, ImgData0);
                image1.GenImage1("byte", roiWidth, roiHeight, ImgData1);
                image2.GenImage1("byte", roiWidth, roiHeight, ImgData2);

                if (preProcessEnable)
                {
                    if (CalculateEdgeGradientEx(image0, ref gradientValue))
                        gradientValueTuple = gradientValueTuple.TupleConcat(gradientValue);
                    if (CalculateEdgeGradientEx(image1, ref gradientValue))
                        gradientValueTuple = gradientValueTuple.TupleConcat(gradientValue);
                    if (CalculateEdgeGradientEx(image2, ref gradientValue))
                        gradientValueTuple = gradientValueTuple.TupleConcat(gradientValue);
                }
                else
                {
                    if (CalculateEdgeGradient(image0, ref gradientValue))
                        gradientValueTuple = gradientValueTuple.TupleConcat(gradientValue);
                    if (CalculateEdgeGradient(image1, ref gradientValue))
                        gradientValueTuple = gradientValueTuple.TupleConcat(gradientValue);
                    if (CalculateEdgeGradient(image2, ref gradientValue))
                        gradientValueTuple = gradientValueTuple.TupleConcat(gradientValue);
                }

                if (gradientValueTuple.Length == 3)
                {
                    if (gradientValueTuple[1].D > maxGradient)
                    {
                        bestZCenPos = curZPos;
                        maxGradient = gradientValueTuple[1].D;
                    }
                    HTuple hv_Indices = gradientValueTuple.TupleSortIndex();
                    int res = GetThreePointShapeType(hv_Indices);

                    //判断是否满足结束条件
                    if (Math.Abs(curGap) > 0.05 * stepGap
                        && loopCount < 10)
                    {
                        if (res == 2 || res == 3)
                        {
                            ret = IsConvergent(gradientValueTuple, stepGap);
                        }
                        if (ret == false)
                        {
                            //如不满足则进行排序，按照3个数值大小关系来做相应的处理
                            //0:XZD;1:DZX;2:ZDX;3:XDZ;4:ZXD;5:DXZ
                            //0:Dir 1:Anti-Dir 2:Anti-Dir 3:Dir 4:err 5:err
                            switch (res)
                            {
                                case 0:
                                    curGap = (direction == 1 ? Math.Abs(curGap / 2) : Math.Abs(curGap));
                                    curZPos += curGap;
                                    coarseCount++;
                                    direction = 0;
                                    break;
                                case 1:
                                    curGap = (direction == 0 ? Math.Abs(curGap / 2) : Math.Abs(curGap));
                                    curGap = -curGap;
                                    curZPos += curGap;
                                    coarseCount++;
                                    direction = 1;
                                    break;
                                case 2:
                                    curGap = (fineCount == 0 ? Math.Abs(curGap / 3) : Math.Abs(curGap));
                                    curGap = (direction == 0 ? -curGap / 2 : -curGap);
                                    curZPos = (curZPos + curZPos - stepGap / 2) / 2;
                                    fineCount++;
                                    direction = 1;
                                    break;
                                case 3:
                                    curGap = (fineCount == 0 ? Math.Abs(curGap / 3) : Math.Abs(curGap));
                                    curGap = (direction == 1 ? curGap / 2 : curGap);
                                    curZPos = (curZPos + curZPos + stepGap / 2) / 2;
                                    fineCount++;
                                    direction = 0;
                                    break;
                                case 4:
                                case 5:
                                    loopCount--;
                                    curZPos = curZPos + 2 * curGap;
                                    direction = 0;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (res != 0 && res != 1)
                        {
                            curZPos = bestZCenPos;
                            ret = true;
                        }
                        else
                        {
                            loopCount--;
                            curGap = (res == 0 ? stepGap : (-1 * stepGap));
                            curZPos = curZPos + curGap;
                        }
                    }
                }
            }
            catch(HalconException hex)
            {
                Trace.WriteLine(hex.GetErrorMessage(), "HALCON error");
            }
            return ret;
        }
        #endregion

        #region Depth From Focus
        // Short Description: Get all image files under the given path 
        private static void ListImageFiles(ref HTuple hv_ImageFiles, string imgSaveFolder)
        {
            string[] imageNames = Directory.GetFiles(imgSaveFolder, "*.bmp");
            for (int i = 0; i < imageNames.Length; i++)
            {
                hv_ImageFiles.Append(imageNames[i]);
            }
        }

        private static bool DepthFromFocusProc(HImage image, string imgSaveFolder,string imgSaveName)
        {
            bool ret = false;
            try
            {
                HObject ho_img1 = new HObject();
                HObject ho_img2 = new HObject();
                HObject ho_img3 = new HObject();
                HOperatorSet.Decompose3(image, out ho_img1, out ho_img2, out ho_img3);

                HTuple filter = new HTuple("highpass");
                filter.Append(7);
                filter.Append(7);
                HTuple selection = new HTuple("next_maximum");

                HOperatorSet.ChannelsToImage(ho_img1, out ho_img1);
                HObject ho_Depth1 = new HObject();
                HObject ho_Confidence1 = new HObject();
                HOperatorSet.DepthFromFocus(ho_img1, out ho_Depth1, out ho_Confidence1, filter, selection);
                //Construct sharp image
                HObject ho_DepthHighConf1 = new HObject();
                HOperatorSet.MeanImage(ho_Depth1, out ho_DepthHighConf1, 11, 11);
                HObject ho_SharpImage1 = new HObject();
                HOperatorSet.SelectGrayvaluesFromChannels(ho_img1, ho_DepthHighConf1, out ho_SharpImage1);
                //Smooth depth map
                //HObject ho_ImageScaleMax1 = new HObject();
                //HOperatorSet.ScaleImageMax(ho_DepthHighConf1, out ho_ImageScaleMax1);
                //HObject ho_DepthMean1 = new HObject();
                //HOperatorSet.MeanImage(ho_ImageScaleMax1, out ho_DepthMean1, 51, 51);
                //HOperatorSet.WriteImage(ho_SharpImage1, "bmp", 0, imgSaveFolder + "\\SharpImage1.bmp");

                HOperatorSet.ChannelsToImage(ho_img2, out ho_img2);
                HObject ho_Depth2 = new HObject();
                HObject ho_Confidence2 = new HObject();
                HOperatorSet.DepthFromFocus(ho_img2, out ho_Depth2, out ho_Confidence2, filter, selection);
                //Construct sharp image
                HObject ho_DepthHighConf2 = new HObject();
                HOperatorSet.MeanImage(ho_Depth2, out ho_DepthHighConf2, 11, 11);
                HObject ho_SharpImage2 = new HObject();
                HOperatorSet.SelectGrayvaluesFromChannels(ho_img2, ho_DepthHighConf2, out ho_SharpImage2);
                //Smooth depth map
                //HObject ho_ImageScaleMax2 = new HObject();
                //HOperatorSet.ScaleImageMax(ho_DepthHighConf2, out ho_ImageScaleMax2);
                //HObject ho_DepthMean2 = new HObject();
                //HOperatorSet.MeanImage(ho_ImageScaleMax2, out ho_DepthMean2, 51, 51);
                //HOperatorSet.WriteImage(ho_SharpImage2, "bmp", 0, imgSaveFolder + "\\SharpImage2.bmp");

                HOperatorSet.ChannelsToImage(ho_img3, out ho_img3);
                HObject ho_Depth3 = new HObject();
                HObject ho_Confidence3 = new HObject();
                HOperatorSet.DepthFromFocus(ho_img3, out ho_Depth3, out ho_Confidence3, filter, selection);
                //Construct sharp image
                HObject ho_DepthHighConf3 = new HObject();
                HOperatorSet.MeanImage(ho_Depth3, out ho_DepthHighConf3, 11, 11);
                HObject ho_SharpImage3 = new HObject();
                HOperatorSet.SelectGrayvaluesFromChannels(ho_img3, ho_DepthHighConf3, out ho_SharpImage3);
                //Smooth depth map
                //HObject ho_ImageScaleMax3 = new HObject();
                //HOperatorSet.ScaleImageMax(ho_DepthHighConf3, out ho_ImageScaleMax3);
                //HObject ho_DepthMean3 = new HObject();
                //HOperatorSet.MeanImage(ho_ImageScaleMax3, out ho_DepthMean3, 51, 51);
                //HOperatorSet.WriteImage(ho_SharpImage3, "bmp", 0, imgSaveFolder + "\\SharpImage3.bmp");

                HObject ho_SharpImage = new HObject();
                HOperatorSet.Compose3(ho_SharpImage1, ho_SharpImage2, ho_SharpImage3, out ho_SharpImage);
                HOperatorSet.WriteImage(ho_SharpImage, "bmp", 0, imgSaveFolder + "\\" + imgSaveName);
                ho_SharpImage.Dispose();

                ret = true;
            }
            catch (HalconException hex)
            {
                Trace.WriteLine(hex.GetErrorMessage(), "HALCON error");
            }
            return ret;
        }

        public static bool DoDepthFromFocus(string imgSaveFolder, string imgSaveName)
        {
            bool ret = false;
            try
            {
                if (!Directory.Exists(imgSaveFolder))
                {
                    Trace.WriteLine(imgSaveFolder + " not exist!");
                    return ret;
                }

                HTuple hv_Names = new HTuple();
                ListImageFiles(ref hv_Names, imgSaveFolder);
                HImage ho_Image = new HImage(hv_Names);

                ret = DepthFromFocusProc(ho_Image, imgSaveFolder, imgSaveName);
                ho_Image.Dispose();
            }
            catch (HalconException hex)
            {
                Trace.WriteLine(hex.GetErrorMessage(), "HALCON error");
            }
            return ret;
        }

        public static bool DoDepthFromFocus(IntPtr[] imgPtr, int imgCount, int width, int height, string imgSaveFolder, string imgSaveName)
        {
            bool ret = false;
            try
            {
                if (imgPtr == null || imgCount < 1)
                {
                    return false;
                }

                HImage ho_Image = new HImage();
                ho_Image.GenImageInterleaved(imgPtr[0], "bgr", width, height, 0, "byte", width, height, 0, 0, -1, 0);
                for (int i = 1; i < imgCount; i++)
                {
                    HImage temp = new HImage();
                    temp.GenImageInterleaved(imgPtr[i], "bgr", width, height, 0, "byte", width, height, 0, 0, -1, 0);
                    ho_Image = ho_Image.ConcatObj(temp);
                }

                ret = DepthFromFocusProc(ho_Image, imgSaveFolder, imgSaveName);
                ho_Image.Dispose();
            }
            catch (HalconException hex)
            {
                Trace.WriteLine(hex.GetErrorMessage(), "HALCON error");
            }
            return ret;
        }
        #endregion
    }
}
