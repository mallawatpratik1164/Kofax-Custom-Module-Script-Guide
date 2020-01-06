using Kofax.Capture.SDK.CustomModule;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MyCustomModule.Runtime
{
    class BatchManager
    {
        private readonly bool applicationIsRunningFromBatchManager;
        private readonly int selectedBatchId;
        private readonly SessionManager sessionManager;
        private readonly BatchProcessor batchProcessor;
        private readonly RecurringTask recurringTask;

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
                    KillProcess();
                }
                else
                {
                    recurringTask = new RecurringTask((taskToExecute) =>
                    {
                        taskToExecute.Enabled = false;

                        IBatch nextBatch = sessionManager.GetNextBatch();

                        if (nextBatch != null)
                        {
                            batchProcessor.ProcessBatch(nextBatch);
                        }

                        taskToExecute.Enabled = true;
                    }, 3000, true);
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
    }
}
