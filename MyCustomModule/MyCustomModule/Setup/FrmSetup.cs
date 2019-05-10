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
            // Access Custom Storage Strings

            // batchClass.get_CustomStorageString("myCustomStringName");
        }

        private void SaveSettings()
        {
            // Save Custom Storage Strings

            // batchClass.set_CustomStorageString("myCustomStringName", "myCustomStringValue");
        }
    }
}