using Kofax.Capture.SDK.CustomModule;
using MyCustomModule.Runtime;
using System;
using System.Windows.Forms;

namespace MyCustomModule
{
    public partial class FrmMain : Form
    {
        private BatchProcessor batchProcessor;
        private BatchManager batchManager;
        private SessionManager sessionManager;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void UpdateUI()
        {
            IBatch currentActiveBatch = batchManager.CurrentActiveBatch;

            // ...
        }

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

        private void timerBatchPolling_Tick(object sender, EventArgs e)
        {
            timerBatchPolling.Enabled = false;

            batchManager.BatchPolling();
            UpdateUI();

            timerBatchPolling.Enabled = true;
        }
    }
}