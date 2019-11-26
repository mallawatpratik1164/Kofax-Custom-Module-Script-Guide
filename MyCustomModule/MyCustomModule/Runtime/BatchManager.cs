using Kofax.Capture.SDK.CustomModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MyCustomModule.Runtime
{
    class BatchManager
    {
        private SessionManager sessionManager;
        private PollTimer batchPollingTimer;
        private BatchProcessor batchProcessor;

        public bool ApplicationIsRunningFromBatchManager { get; }

        public BatchManager(bool applicationIsRunningAsService = true)
        {
            if (!applicationIsRunningAsService)
            {
                string[] args = Environment.GetCommandLineArgs();

                // args[0] contains the path to the .exe file
                if (args.Length > 1)
                {
                    // When the application is running from the batch manager, the command line argument starts with -B###, where ### is the decimal batch ID
                    ApplicationIsRunningFromBatchManager = args[1].StartsWith("-B");
                }
            }
            
            try
            {
                sessionManager = new SessionManager();
                sessionManager.LoginToRuntimeSession();

                batchProcessor = new BatchProcessor();

                batchPollingTimer = new PollTimer(true, OnPollingTimerTick);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void KillProcess()
        {
            try
            {
                batchPollingTimer.SetTimerState(false);

                sessionManager.Logout();

                if (ApplicationIsRunningFromBatchManager)
                {
                    Application.Exit();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void OnPollingTimerTick(object sender, ElapsedEventArgs e)
        {
            try
            {
                batchPollingTimer.SetTimerState(false);

                IBatch currentActiveBatch = sessionManager.GetNextBatch();

                if (currentActiveBatch != null)
                {
                    batchProcessor.ProcessBatch(currentActiveBatch);
                }
                else
                {
                    if (ApplicationIsRunningFromBatchManager)
                    {
                        sessionManager.Logout();
                        Application.Exit();
                    }
                }

                batchPollingTimer.SetTimerState(true);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
