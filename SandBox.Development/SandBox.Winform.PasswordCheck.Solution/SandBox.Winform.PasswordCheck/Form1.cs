using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SandBox.Winform.PasswordCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImpersonateUser iU = new ImpersonateUser();
            // TODO: Replace credentials
            if (iU.Impersonate("remoteMachine", txtUsername.Text, txtPassword.Text))
            {
                MessageBox.Show("Username and Password valid.");
                iU.Undo();
            }
            else
            {
                MessageBox.Show(iU.mszErrorMessage);
            }
        }
    }
}