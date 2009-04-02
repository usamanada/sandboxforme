using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
namespace SandBox.Winform.Registry.Solution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RegistryKey rk = Microsoft.Win32.Registry.CurrentUser;

            RegistryKey rkInSettings = rk.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
            tbxRegKey.Text = rkInSettings.Name;
            txtValue.Text = @"ProxyEnable";
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            object value = Microsoft.Win32.Registry.GetValue(tbxRegKey.Text, txtValue.Text, int.MinValue);
            lblResult.Text = value.ToString();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.Registry.SetValue(tbxRegKey.Text, txtValue.Text, Convert.ToInt32(tbxSet.Text));
        }

    }
}
