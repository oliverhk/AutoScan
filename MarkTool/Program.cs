using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MarkTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //实例运行1次
                Process thisProcess = Process.GetCurrentProcess();
                Process[] allProcesses = Process.GetProcessesByName(thisProcess.ProcessName);
                if (allProcesses.Length > 1)
                {
                    MessageBox.Show("程序已经运行", "睿仟医疗");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(WinUI.MainForm.Instance);
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统严重异常 " + ex.ToString(), "睿仟医疗");
            }
        }
    }
}
