using Kofax.Capture.AdminModule.InteropServices;
using System.Windows.Forms;

namespace MyCustomModule.Setup
{
    public partial class FrmSetup : Form
    {
        private IBatchClass batchClass;

        public FrmSetup()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(IBatchClass batchClass)
        {
            this.batchClass = batchClass;
            LoadSettings();
            return this.ShowDialog();
        }

        private void LoadSettings()
        {
            // load previous settings

            // string value = batchClass.get_CustomStorageString("key");
        }

        private void SaveSettings()
        {
            // store settings

            // batchClass.set_CustomStorageString("key", "value");
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}