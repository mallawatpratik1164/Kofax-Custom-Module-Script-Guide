using Kofax.Capture.SDK.CustomModule;

namespace MyCustomModule.Runtime
{
    internal class BatchManager
    {
        #region Variables

        /// <summary>
        /// Processes the current active batch
        /// </summary>
        private BatchProcessor batchProcessor;

        /// <summary>
        /// Manages the login / logout process
        /// </summary>
        private SessionManager sessionManager;

        #endregion Variables

        #region Properties

        /// <summary>
        /// The current active batch to process
        /// </summary>
        public IBatch CurrentActiveBatch { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Executes the runtime login
        /// </summary>
        /// <param name="batchProcessor"></param>
        /// <param name="sessionManager"></param>
        public BatchManager(BatchProcessor batchProcessor, SessionManager sessionManager)
        {
            this.batchProcessor = batchProcessor;
            this.sessionManager = sessionManager;

            this.sessionManager.LoginToRuntimeSession();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Tries to retrieve a new batch to process and exits the process if no batch is available
        /// </summary>
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

        #endregion Methods
    }
}