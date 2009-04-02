using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.Winform.Biztalk.Administrator
{
    public class PartySendPortRef
    {
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        private string mName;

        public string URI
        {
            get { return mUri; }
            set { mUri = value; }
        }
        private string mUri;
    }
}
