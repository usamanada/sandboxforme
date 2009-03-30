using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;
namespace _02_CreateUser
{
    public partial class Form1 : Form
    {
        public enum AdsUserFlags
        {
            Script = 1,                                     // 0x1
            AccountDisabled = 2,                            // 0x2
            HomeDirectoryRequired = 8,                      // 0x8
            //AccountLockedOut = 16,                          // 0x10
            PasswordNotRequired = 32,                       // 0x20
            //PasswordCannotChange = 64,                      // 0x40
            EncryptedTextPasswordAllowed = 128,             // 0x80
            TempDuplicateAccount = 256,                     // 0x100
            NormalAccount = 512,                            // 0x200
            InterDomainTrustAccount = 2048,                 // 0x800
            WorkstationTrustAccount = 4096,                 // 0x1000
            ServerTrustAccount = 8192,                      // 0x2000
            PasswordDoesNotExpire = 65536,                  // 0x10000
            MnsLogonAccount = 131072,                       // 0x20000
            SmartCardRequired = 262144,                     // 0x40000
            TrustedForDelegation = 524288,                  // 0x80000
            AccountNotDelegated = 1048576,                  // 0x100000
            UseDesKeyOnly = 2097152,                         // 0x200000
            DontRequirePreauth = 4194304,                    // 0x400000
            //PasswordExpired = 8388608,                      // 0x800000
            TrustedToAuthenticateForDelegation = 16777216,  // 0x1000000
            NoAuthDataRequired = 33554432                   // 0x2000000
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void ADCreateBtn_Click(object sender, EventArgs e)
        {

            try
            {
                //DirectoryEntry obDirEntry = new DirectoryEntry("LDAP://DC=corp,DC=uecomm,DC=com,DC=au", "admoszymczak", "ad1mad1m");
                //DirectoryEntry OUManagedAccounts = obDirEntry.Children.Find("OU=Managed Accounts");
                //DirectoryEntry OUBiztalk = OUManagedAccounts.Children.Find("OU=Biztalk");
                //DirectoryEntry OUDev = OUBiztalk.Children.Find("OU=Dev");

                using (DirectoryEntry OUDev = new DirectoryEntry("LDAP://OU=Dev,OU=Biztalk,OU=Managed Accounts,DC=corp,DC=uecomm,DC=com,DC=au", "admoszymczak", "ad1mad1m"))
                {
                    using (DirectoryEntry newUser = OUDev.Children.Add("CN=John Doe", "user"))
                    {
                        //add mandatory attribs
                        newUser.Properties["sAMAccountName"].Add("jdoe1");
                        newUser.Properties["userprincipalname"].Add("jdoe1@corp.uecomm.com.au");
                        //add optional ones
                        //Last Name
                        newUser.Properties["sn"].Add("Doe");
                        //First Name
                        newUser.Properties["givenName"].Add("John");
                        //description
                        newUser.Properties["description"].Add("Average Guy");
                        //telephone number
                        newUser.Properties["telephoneNumber"].Add("555-1212");
                        //mobile number
                        newUser.Properties["mobile"].Add("555-1111");
                        //fax number
                        newUser.Properties["facsimiletelephonenumber"].Add("555-5555");
                        //this is how it will be presented to users
                        newUser.Properties["displayName"].Add("Doe, John");
                        //email address
                        newUser.Properties["mail"].Add("jdoe1@uecomm.com.au");
                        //job Title
                        newUser.Properties["title"].Add("Sample Account");
                        //Company
                        newUser.Properties["company"].Add("Uecomm");
                        //department
                        newUser.Properties["department"].Add("Technology");
                        //update the directory
                        newUser.CommitChanges();

                        newUser.Invoke("SetPassword", new object[] { "newpassword" });
                        newUser.CommitChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ADDeleteBtn_Click(object sender, EventArgs e)
        {
            
            using (DirectoryEntry OUDev = new DirectoryEntry("LDAP://OU=Dev,OU=Biztalk,OU=Managed Accounts,DC=corp,DC=uecomm,DC=com,DC=au", "admoszymczak", "ad1mad1m"))
            {
                //find the child to remove
                using (DirectoryEntry child = OUDev.Children.Find("CN=John Doe"))
                {
                    //immediately delete
                    OUDev.Children.Remove(child);
                    Console.WriteLine("User Deleted.");
                }

            }


        }

        private void ADEnbableUserBtn_Click(object sender, EventArgs e)
        {
            using (DirectoryEntry OUDev = new DirectoryEntry("LDAP://OU=Dev,OU=Biztalk,OU=Managed Accounts,DC=corp,DC=uecomm,DC=com,DC=au", "admoszymczak", "ad1mad1m"))
            {
                //find the child to remove
                using (DirectoryEntry user = OUDev.Children.Find("CN=John Doe"))
                {
                    int userFlags = (int)user.Properties["userAccountControl"].Value;
                    userFlags = userFlags - (int)AdsUserFlags.AccountDisabled;
                    user.Properties["userAccountControl"].Value = userFlags;
                    user.CommitChanges();
                }
            }
        }

        private void ADDisableUserBtn_Click(object sender, EventArgs e)
        {
            using (DirectoryEntry OUDev = new DirectoryEntry("LDAP://OU=Dev,OU=Biztalk,OU=Managed Accounts,DC=corp,DC=uecomm,DC=com,DC=au", "admoszymczak", "ad1mad1m"))
            {
                //find the child to remove
                using (DirectoryEntry user = OUDev.Children.Find("CN=John Doe"))
                {
                    int userFlags = (int)user.Properties["userAccountControl"].Value;
                    userFlags = userFlags + (int)AdsUserFlags.AccountDisabled;
                    user.Properties["userAccountControl"].Value = userFlags;
                    user.CommitChanges();
                }
            }
        }
    }
}
