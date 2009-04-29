using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sandbox.Winform.DataGridBinding
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    class State : Attribute
    {
        public string statename { get; set; }
        public int id { get; set; }
        
    }
}
