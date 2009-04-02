using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.Winform.Biztalk.Administrator
{
    public class Adapter : ICloneable
    {
        public string Name;
        public string Type;
        public static string BuildKey(string adapterName, string hostName, string adapterType)
        {
            return string.Format("{0}:{1}:{2}", adapterName, hostName, adapterType);
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
