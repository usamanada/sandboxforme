using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BizTalkSetUp
{
    public partial class frmPasswordInput : Form
    {
        public string Username;
        private bool validPassword;
        public frmPasswordInput()
        {
            InitializeComponent();
        }

         private void btnEnter_Click(object sender, EventArgs e)
        {
            ImpersonateUser iU = new ImpersonateUser();
            // TODO: Replace credentials
            if (iU.Impersonate("remoteMachine", Username, txtPassword.Text))
            {
                validPassword = true;
                iU.Undo();
            }
            else
            {
                MessageBox.Show(iU.mszErrorMessage);
            }
        }

        private void frmPasswordInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!validPassword)
            {
                e.Cancel = true;
            }
        }
    }
}