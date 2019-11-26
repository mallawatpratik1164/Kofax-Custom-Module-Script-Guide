using Kofax.Capture.SDK.CustomModule;
using System;
using System.Timers;
using System.Windows.Forms;

namespace MyCustomModule.Runtime
{
    class BatchManager
    {
        private SessionManager sessionManager;
        private PollTimer batchPollingTimer;
        private BatchProcessor batchProcessor;
        private readonly bool applicationIsRunningFromBatchManager;
        private readonly int selectedBatchId;

        public BatchManager(bool applicationIsRunningAsService = true)
        {
            if (!applicationIsRunningAsService)
            {
                string[] args = Environment.GetCommandLineArgs();

                // args[0] contains the path to the .exe file
                if (args.Length > 1)
                {
                    string batchIdCommandLineArgument = args[1];

                    // When the application is running from the batch manager, the command line argument starts with -B###, where ### is the decimal batch ID
                    applicationIsRunningFromBatchManager = batchIdCommandLineArgument.StartsWith("-B");

                    if (applicationIsRunningFromBatchManager)
                    {
                        // Extract the selected Batch ID from -B###
                        string rawBatchId = batchIdCommandLineArgument.Substring(2);
                        selectedBatchId = Convert.ToInt32(rawBatchId);
                    }
                }
            }
            
            try
            {
                sessionManager = new SessionManager();
                sessionManager.LoginToRuntimeSession();

                batchProcessor = new BatchProcessor();

                if (applicationIsRunningFromBatchManager)
                {
                    // Process the selected batch only
                    IBatch selectedBatch = sessionManager.GetBatchById(selectedBatchId);
                    batchProcessor.ProcessBatch(selectedBatch);

                    sessionManager.Logout();
                    Application.Exit();
                }
                else
                {
                    batchPollingTimer = new PollTimer(true, OnPollingTimerTick);
                }
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

                if (applicationIsRunningFromBatchManager)
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

                IBatch nextBatch = sessionManager.GetNextBatch();

                if (nextBatch != null)
                {
                    batchProcessor.ProcessBatch(nextBatch);
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
