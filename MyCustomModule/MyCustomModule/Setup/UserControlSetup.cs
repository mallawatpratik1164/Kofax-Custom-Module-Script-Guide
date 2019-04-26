using Kofax.Capture.AdminModule.InteropServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyCustomModule.Setup
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ISetupForm
    {
        [DispId(1)]
        AdminApplication Application { set; }

        [DispId(2)]
        void ActionEvent(int EventNumber, object Argument, out int Cancel);
    }

    [ClassInterface(ClassInterfaceType.None)]
    [ProgId(CUSTOM_MODULE_NAME_SETUP)]
    public partial class UserControlSetup : UserControl, ISetupForm
    {
        private const string CUSTOM_MODULE_NAME_SETUP = "MyCustomModule.Setup";

        private AdminApplication adminApplication;

        public AdminApplication Application
        {
            set
            {
                value.AddMenu(CUSTOM_MODULE_NAME_SETUP, CUSTOM_MODULE_NAME_SETUP, "BatchClass");
                adminApplication = value;
            }
        }

        public void ActionEvent(int EventNumber, object Argument, out int Cancel)
        {
            Cancel = 0;

            if ((KfxOcxEvent)EventNumber == KfxOcxEvent.KfxOcxEventMenuClicked && (string)Argument == CUSTOM_MODULE_NAME_SETUP)
            {
                FrmSetup form = new FrmSetup();
                form.ShowDialog(adminApplication.ActiveBatchClass);
            }
        }
    }
}