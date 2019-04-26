using Kofax.Capture.SDK.CustomModule;

namespace MyCustomModule.Runtime
{
    internal class BatchManager
    {
        private BatchProcessor batchProcessor;
        private SessionManager sessionManager;

        public IBatch CurrentActiveBatch { get; private set; }

        public BatchManager(BatchProcessor batchProcessor, SessionManager sessionManager)
        {
            this.batchProcessor = batchProcessor;
            this.sessionManager = sessionManager;

            this.sessionManager.LoginToRuntimeSession();
        }

        public void BatchPolling()
        {
            CurrentActiveBatch = sessionManager.GetNextBatch();

            if (CurrentActiveBatch != null)
            {
                batchProcessor.ProcessBatch(CurrentActiveBatch);
            }
            else
            {
                sessionManager.Logout();
            }
        }
    }
}