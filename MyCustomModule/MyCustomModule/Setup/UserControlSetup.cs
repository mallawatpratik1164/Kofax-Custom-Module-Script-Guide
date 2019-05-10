using Kofax.Capture.AdminModule.InteropServices;
using MyCustomModule.Properties;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyCustomModule.Setup
{
    public partial class UserControlSetup : UserControl
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
        [ProgId("MyCustomModule.Configuration" /* Resources.CUSTOM_MODULE_ID_SETUP */)]
        public partial class UserCtrlSetup : UserControl, ISetupForm
        {
            private AdminApplication adminApplication;

            public AdminApplication Application
            {
                set
                {
                    value.AddMenu(Resources.CUSTOM_MODULE_ID_SETUP, Resources.CUSTOM_MODULE_ID_SETUP, Resources.BATCH_CLASS);
                    adminApplication = value;
                }
            }

            public void ActionEvent(int EventNumber, object Argument, out int Cancel)
            {
                Cancel = 0;

                if ((KfxOcxEvent)EventNumber == KfxOcxEvent.KfxOcxEventMenuClicked && (string)Argument == Resources.CUSTOM_MODULE_ID_SETUP)
                {
                    FrmSetup form = new FrmSetup();
                    form.ShowDialog(adminApplication.ActiveBatchClass);
                }
            }
        }
    }
}