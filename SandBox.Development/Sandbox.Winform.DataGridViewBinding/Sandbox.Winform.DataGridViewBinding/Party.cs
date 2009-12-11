using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Winform.DataGridViewBinding
{
    class Party
    {
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string name;

        public string Job
        {
            get { return job; }
            set { job = value; }
        }
        private string job;

    }
}
