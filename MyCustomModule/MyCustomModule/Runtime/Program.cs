using MyCustomModule.Runtime;
using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace MyCustomModule
{
    internal static class Program
    {
        private static void Main()
        {
            if (Environment.UserInteractive)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
            else
            {
                ServiceBase.Run(new RuntimeService());
            }
        }
    }
}
