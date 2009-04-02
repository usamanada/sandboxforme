using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSetUp
{
    class PartyAlias
    {
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        private string mName;

        public string Qualifier
        {
            get { return mQualifier; }
            set { mQualifier = value; }
        }
        private string mQualifier;

        public string Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        private string mValue;

        public bool IsAutoCreated;
    }
}
