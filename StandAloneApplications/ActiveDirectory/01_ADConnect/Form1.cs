using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _01_ADConnect
{
    public partial class Form1 : Form
    {
        string userName;
        public Form1()
        {
            InitializeComponent();
            userName = Environment.GetEnvironmentVariable("UserName");
            ADLocalUserChk.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uname = string.Empty;
            if (ADLocalUserChk.Checked)
            {
                uname = userName;
                ADUsernameTxt.Text = uname;
            }
            else
            {
                uname = ADUsernameTxt.Text;
            }
            
            ADIdTxt.Text = ADUser.GetUserGUID(uname).ToString();
            ADUser.displayAll(uname);
        }

        private void ADLocalUserChk_CheckedChanged(object sender, EventArgs e)
        {
            ADUsernameTxt.Enabled =! ADLocalUserChk.Checked;
        }
    }
}
