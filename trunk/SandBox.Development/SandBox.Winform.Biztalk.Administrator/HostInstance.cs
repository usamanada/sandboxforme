using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.Winform.Biztalk.Administrator
{
    public class HostInstance : ICloneable
    {
        public string ServerName;
        public string HostName;
        public string UserName;
        public string Password;
        public bool Disable;

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
