using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace MarkTool
{
    static class ConstDef
    {
        static ConstDef()
        {
            LogFile = ConfigurationManager.AppSettings["LogFile"];
            MSDN = "PATHOLOGY";
            DeviceNo = ConfigurationManager.AppSettings["DeviceNo"];
            SoftVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();            
            TIME_FORMAT = "yyyyMMdd HHmmss";
            UserID = "Operator";
            XCountUnit = 156.25f;        //纳米
            YCountUnit = 156.25f;        //纳米                   
            OperatorRole = true;    //operator role
            IsSysReady = false;
            ImageX = 0;
            ImageY = 0;

            //system config id
            SysInfoHospital = "Hospital";
            SysInfoImagePath = "ImagePath";
            SysInfoMicroType = "MicroType";
            SysInfoCameraPara = "CameraPara";
            SysInfoLensPara = "LensPara";
        }
        
        public static string LogFile { get; private set; }
        public static string DeviceNo { get; private set; }               
        public static string MSDN { get; private set; }
        public static string SoftVersion { get; private set; }
        public static string UserID { get; set; }           
        public static string TIME_FORMAT { get; set; }
        public static bool OperatorRole { get; set; }
        public static float XCountUnit { get; set; }
        public static float YCountUnit { get; set; }        
        public static bool IsSysReady { get; set; }
        public static string ImagePath { get; set; }
        public static int ImageX { get; set; }
        public static int ImageY { get; set; }

        //
        public static string SysInfoHospital { get; private set; }
        public static string SysInfoImagePath { get; private set; }
        public static string SysInfoMicroType { get; private set; }
        public static string SysInfoCameraPara { get; private set; }
        public static string SysInfoLensPara { get; private set; }
    }
}
