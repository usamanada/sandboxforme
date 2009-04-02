using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.Winform.DataGridViewComboBox
{
    class JobType
    {
        public int ValueID
        {
            get { return valueID; }
            set { valueID = value; }
        }
        private int valueID;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string name;
    }
}
