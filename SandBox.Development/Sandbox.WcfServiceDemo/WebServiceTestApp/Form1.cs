using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebServiceTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client context = new ServiceReference1.Service1Client();
            txtResult.Text = context.GetData(Int32.Parse(txtCall.Text));

        }
    }
}
