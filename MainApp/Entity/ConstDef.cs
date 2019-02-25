using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace MainApp
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
            ZCountUnit = 100f;           //纳米            
            IsScan = false;         //开始扫描
            OperatorRole = true;    //operator role
            IsSysReady = false;
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
        public static float ZCountUnit { get; set; }        
        public static bool IsScan { get; set; }
        public static bool IsSysReady { get; set; }        
    }
}
