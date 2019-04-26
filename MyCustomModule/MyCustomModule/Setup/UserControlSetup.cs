using Kofax.Capture.AdminModule.InteropServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyCustomModule.Setup
{
    /// <summary>
    ///
    /// </summary>
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ISetupForm
    {
        [DispId(1)]
        AdminApplication Application { set; }

        [DispId(2)]
        void ActionEvent(int EventNumber, object Argument, out int Cancel);
    }

    /// <summary>
    /// Control to initialize the setup form
    /// </summary>
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId(CUSTOM_MODULE_NAME_SETUP)]
    public partial class UserControlSetup : UserControl, ISetupForm
    {
        #region Constants

        /// <summary>
        /// Name of the setup of the custom module
        /// </summary>
        private const string CUSTOM_MODULE_NAME_SETUP = "MyCustomModule.Setup";

        #endregion Constants

        #region Variables

        /// <summary>
        ///
        /// </summary>
        private AdminApplication adminApplication;

        #endregion Variables

        #region Properties

        /// <summary>
        /// Attach the custom module to the context menu of the batch class
        /// </summary>
        public AdminApplication Application
        {
            set
            {
                value.AddMenu(CUSTOM_MODULE_NAME_SETUP, CUSTOM_MODULE_NAME_SETUP, "BatchClass");
                adminApplication = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialize the setup form
        /// </summary>
        /// <param name="EventNumber"></param>
        /// <param name="Argument"></param>
        /// <param name="Cancel"></param>
        public void ActionEvent(int EventNumber, object Argument, out int Cancel)
        {
            Cancel = 0;

            if ((KfxOcxEvent)EventNumber == KfxOcxEvent.KfxOcxEventMenuClicked && (string)Argument == CUSTOM_MODULE_NAME_SETUP)
            {
                FrmSetup form = new FrmSetup();
                form.ShowDialog(adminApplication.ActiveBatchClass);
            }
        }

        #endregion Methods
    }
}