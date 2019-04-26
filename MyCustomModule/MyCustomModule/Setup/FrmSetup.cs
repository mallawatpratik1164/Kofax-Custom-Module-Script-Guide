using Kofax.Capture.AdminModule.InteropServices;
using System.Windows.Forms;
using System;

namespace MyCustomModule.Setup
{
    public partial class FrmSetup : Form
    {
        #region Variables

        /// <summary>
        /// The batch class for the setup
        /// </summary>
        private IBatchClass batchClass;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initialize the GUI
        /// </summary>
        public FrmSetup()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Show the setup form
        /// </summary>
        /// <param name="batchClass">The batch class for the setup</param>
        /// <returns></returns>
        public DialogResult ShowDialog(IBatchClass batchClass)
        {
            this.batchClass = batchClass;
            LoadSettings();
            return this.ShowDialog();
        }

        /// <summary>
        /// Load the previous settings on start
        /// </summary>
        private void LoadSettings()
        {
            // load previous settings

            // string value = batchClass.get_CustomStorageString("key");
        }

        /// <summary>
        /// Save the settings to the custom storage of the batch class
        /// </summary>
        private void SaveSettings()
        {
            // store settings

            // batchClass.set_CustomStorageString("key", "value");
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Save the settings and exit the dialog
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        /// <summary>
        /// Cancel the configuration and exit the dialog
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion Events
    }
}