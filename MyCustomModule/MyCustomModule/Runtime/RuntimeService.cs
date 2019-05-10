using MyCustomModule.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomModule
{
    public partial class RuntimeService : ServiceBase
    {
        private BatchManager batchManager;

        public RuntimeService()
        {
            InitializeComponent();
        }

        public void RunDebugMode()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            batchManager = new BatchManager();
        }

        protected override void OnStop()
        {
            batchManager.KillProcess();
        }
    }
}
