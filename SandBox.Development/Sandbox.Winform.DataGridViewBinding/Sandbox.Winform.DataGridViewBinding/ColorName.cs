using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Winform.DataGridViewBinding
{
    class ColorName
    {
        public ColorName(int id, string name)
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

        public ColorName Self { get { return this; } }
    }
}
