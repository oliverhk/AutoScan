using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using Utility;
using CommonLibrary;
namespace MainApp
{
    internal class Sysmgmt
    {
        #region 单例模式
        private static readonly Lazy<Sysmgmt> Lazy = new Lazy<Sysmgmt>(() => new Sysmgmt());
        public static Sysmgmt Instance { get { return Lazy.Value; } }
        private SystemConfig sysCfg = SystemConfig.Instance;
        private Sysmgmt()
        {            
        }
        #endregion

        #region 属性
        private readonly Timer tmrLogger = new Timer(1000 * 60 * 60 * 1);

        

        #endregion

        #region method
        public void Initialize()
        {
            try
            {
                tmrLogger.Elapsed += tmrLogger_Elapsed;
                tmrLogger.Start();
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void tmrLogger_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = dir + "/logs";
                if (Directory.Exists(dir))
                {
                    var lst = Directory.GetDirectories(dir);
                    foreach (var s in lst)
                    {
                        var lst2 = Directory.GetDirectories(s);
                        foreach (var s1 in lst2)
                        {
                            DeleteFiles(s1, sysCfg.Max_logs_day);
                        }
                    }
                }
                //Delete data file
                dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = dir + "/LocalBackPath";
                if (Directory.Exists(dir))
                {
                    var lst = Directory.GetDirectories(dir);
                    foreach (var s in lst)
                    {
                        try
                        {
                            var info = new DirectoryInfo(s);
                            if (info.GetFiles().Length == 0 &&
                                info.GetDirectories().Length == 0)
                            {
                                if (info.LastWriteTime < DateTime.Now.AddDays(-30))
                                {
                                    Directory.Delete(s, false);
                                    LogHelper.AppLoger.Info("Delete Directory: " + s);
                                }
                            }
                            else
                            {
                                DeleteFiles(s, sysCfg.Max_logs_day);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.AppLoger.Error(ex);
                        }
                    }
                }

                dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = dir + "/LocalPath";
                if (Directory.Exists(dir))
                {
                    var lst = Directory.GetDirectories(dir);
                    foreach (var s in lst)
                    {
                        try
                        {
                            var info = new DirectoryInfo(s);
                            if (info.GetFiles().Length == 0 &&
                                info.GetDirectories().Length == 0 &&
                                info.LastWriteTime < DateTime.Now.AddDays(-30))
                            {
                                info.Delete();
                                LogHelper.AppLoger.Info("Delete Directory: " + s);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.AppLoger.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void DeleteFiles(string logPath, int maxAgeInDays)
        {
            if (Directory.Exists(logPath))
            {
                var now = DateTime.Now;
                now = now.AddDays(0 - maxAgeInDays);
                var lst = Directory.GetFiles(logPath);
                foreach (var file in lst)
                {
                    var create = File.GetLastWriteTime(file);
                    if (create < now)
                    {
                        File.Delete(file);
                        LogHelper.AppLoger.Info("Delete File: " + file);
                    }
                }
            }
        }
        #endregion
    }
}
