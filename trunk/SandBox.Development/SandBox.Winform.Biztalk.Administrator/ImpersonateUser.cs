using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security.Permissions;

namespace SandBox.Winform.Biztalk.Administrator
{
    public class ImpersonateUser
    {
        public int miErrorCode;
        public string mszErrorMessage;

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(
                String lpszUsername,
                String lpszDomain,
                String lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        private static IntPtr tokenHandle = new IntPtr(0);
        private static WindowsImpersonationContext impersonatedUser;

        // If you incorporate this code into a DLL, be sure to demand that it
        // runs with FullTrust.
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public bool Impersonate(string domainName, string userName, string password)
        {
            miErrorCode = 0;
            mszErrorMessage = "";
            try
            {
                // Use the unmanaged LogonUser function to get the user token for
                // the specified user, domain, and password.
                const int LOGON32_PROVIDER_DEFAULT = 0;
                // Passing this parameter causes LogonUser to create a primary token.
                const int LOGON32_LOGON_INTERACTIVE = 2;
                tokenHandle = IntPtr.Zero;

                // ---- Step - 1 
                // Call LogonUser to obtain a handle to an access token.
                bool returnValue = LogonUser(
                                    userName,
                                    domainName,
                                    password,
                                    LOGON32_LOGON_INTERACTIVE,
                                    LOGON32_PROVIDER_DEFAULT,
                                    ref tokenHandle);         // tokenHandle - new security token

                if (false == returnValue)
                {
                    miErrorCode = Marshal.GetLastWin32Error();
                    throw new System.ComponentModel.Win32Exception(miErrorCode);
                }

                // ---- Step - 2 
                WindowsIdentity newId = new WindowsIdentity(tokenHandle);

                // ---- Step - 3 
                impersonatedUser = newId.Impersonate();


            }
            catch (Exception ex)
            {
                mszErrorMessage = ex.Message;
                return false;
            }
            return true;
        }

        // Stops impersonation
        public void Undo()
        {
            impersonatedUser.Undo();
            // Free the tokens.
            if (tokenHandle != IntPtr.Zero)
                CloseHandle(tokenHandle);
        }

    }
}
