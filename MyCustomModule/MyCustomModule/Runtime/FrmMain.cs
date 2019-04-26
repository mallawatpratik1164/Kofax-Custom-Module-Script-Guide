using Kofax.Capture.SDK.CustomModule;
using MyCustomModule.Runtime;
using System;
using System.Windows.Forms;

namespace MyCustomModule
{
    public partial class FrmMain : Form
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private BatchProcessor batchProcessor;

        /// <summary>
        /// 
        /// </summary>
        private BatchManager batchManager;

        /// <summary>
        /// 
        /// </summary>
        private SessionManager sessionManager;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initialize the form
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Update the form's components
        /// </summary>
        private void UpdateUI()
        {
            IBatch currentActiveBatch = batchManager.CurrentActiveBatch;

            // ...
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Initialize the components and start the timer
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                batchProcessor = new BatchProcessor();
                sessionManager = new SessionManager();
                batchManager = new BatchManager(batchProcessor, sessionManager);

                UpdateUI();

                timerBatchPolling.Enabled = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Stop the timer and logout
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerBatchPolling.Enabled = false;

            try
            {
                sessionManager.Logout();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Execute a polling per timer tick
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void timerBatchPolling_Tick(object sender, EventArgs e)
        {
            timerBatchPolling.Enabled = false;

            batchManager.BatchPolling();
            UpdateUI();

            timerBatchPolling.Enabled = true;
        }

        #endregion Events
    }
}