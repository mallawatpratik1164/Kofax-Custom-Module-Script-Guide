using System;
using System.Windows.Forms;

namespace MyCustomModule.Runtime
{
    public partial class FrmMain : Form
    {
        private BatchManager batchManager;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            batchManager = new BatchManager(false);
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            batchManager.KillProcess();
        }
    }
}