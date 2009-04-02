using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.Winform.Biztalk.Administrator
{
    public class PartyStandardAliases
    {
        public PartyStandardAliases(int id, string name)
        {
            this.Id = id; this.Name = name;
        }

        public int Id
        {
            get { return mId; }
            set { mId = value; }
        }
        private int mId;

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        private string mName;

        public PartyStandardAliases Self { get { return this; } }
    }
}
