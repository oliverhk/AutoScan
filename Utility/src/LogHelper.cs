using System;
using System.IO;
using log4net;

namespace Utility
{
    public static class LogHelper
    {
        public static ILog AppLoger { get; private set; }
        //public static ILog L1 { get; private set; }
        private static String ConfigName;
        static LogHelper()
        {
            var temp = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "\\";
            var file = new FileInfo(temp + "\\configure\\Log4Net.xml");            
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);
            AppLoger = LogManager.GetLogger("AppLog");
            //L1 = LogManager.GetLogger("L1");          
        }
        public static void SetConfig(string configName)
        {
            var temp = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName+"\\";
            var file = new FileInfo(temp + configName);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);
            AppLoger = LogManager.GetLogger("AppLog");
        }
    }
}
