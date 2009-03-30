using System;

namespace SQLCracker2
{    
    public class User
    {

        public byte[] LowerHash = new byte[20];
        public byte[] UpperHash = new byte[20];
        public byte[] Salt = new byte[4];
        public string Hash;
        public string Password;
        public string UppercasePassword;
        public string Username;
        public string DatabaseName;
        public string DatabaseType;
        public User()
        {
        }
    }
    
   
}
