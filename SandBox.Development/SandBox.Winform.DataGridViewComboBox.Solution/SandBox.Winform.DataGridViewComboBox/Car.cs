using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.Winform.DataGridViewComboBox
{
    class Car
    {
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        private string mName;

        public ColorName Color
        {
            get { return mColor; }
            set { mColor = value; }
        }
        private ColorName mColor;
    }
}
