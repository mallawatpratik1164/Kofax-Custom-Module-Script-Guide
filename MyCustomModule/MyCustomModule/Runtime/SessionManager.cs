using Kofax.Capture.DBLite;
using Kofax.Capture.SDK.CustomModule;
using System.Windows.Forms;

namespace MyCustomModule.Runtime
{
    internal class SessionManager
    {
        private const string CUSTOM_MODULE_ID = "MyCustomModule";

        private Login login;
        private IRuntimeSession session;

        public IBatch GetNextBatch()
        {
            return session.NextBatchGet(login.ProcessID);
        }

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

        public void Logout()
        {
            session.Dispose();
            login.Logout();
            Application.Exit();
        }
    }
}