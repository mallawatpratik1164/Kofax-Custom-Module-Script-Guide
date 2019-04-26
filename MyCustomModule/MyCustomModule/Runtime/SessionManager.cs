using Kofax.Capture.DBLite;
using Kofax.Capture.SDK.CustomModule;
using System.Windows.Forms;

namespace MyCustomModule.Runtime
{
    internal class SessionManager
    {
        #region Constants

        /// <summary>
        /// ID of the custom module
        /// </summary>
        private const string CUSTOM_MODULE_ID = "MyCustomModule";

        #endregion Constants

        #region Variables

        /// <summary>
        /// Handles the login process
        /// </summary>
        private Login login;

        /// <summary>
        /// The current active session
        /// </summary>
        private IRuntimeSession session;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Get the next batch to process
        /// </summary>
        /// <returns></returns>
        public IBatch GetNextBatch()
        {
            return session.NextBatchGet(login.ProcessID);
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Login and get a session
        /// </summary>
        public void LoginToRuntimeSession()
        {
            login = new Login();
            login.EnableSecurityBoost = true;
            login.Login();
            login.ApplicationName = CUSTOM_MODULE_ID;
            login.Version = "1.0";
            login.ValidateUser($"{CUSTOM_MODULE_ID}.exe", false);

            session = login.RuntimeSession;
        }

        /// <summary>
        /// Logout from Kofax and exit the process
        /// </summary>
        public void Logout()
        {
            session.Dispose();
            login.Logout();
            Application.Exit();
        }

        #endregion Methods
    }
}