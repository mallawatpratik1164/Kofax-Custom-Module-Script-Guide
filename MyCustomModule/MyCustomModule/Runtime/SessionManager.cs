using Kofax.Capture.DBLite;
using Kofax.Capture.SDK.CustomModule;
using MyCustomModule.Properties;

namespace MyCustomModule.Runtime
{
    internal class SessionManager
    {
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
            login.ApplicationName = Resources.CUSTOM_MODULE_ID;
            login.Version = "1.0";
            login.ValidateUser($"{Resources.CUSTOM_MODULE_ID}.exe", false);

            session = login.RuntimeSession;
        }

        public void Logout()
        {
            session.Dispose();
            login.Logout();
        }
    }
}