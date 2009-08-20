using System;

using System.Windows.Forms;
using SandBox.dll.Common;

namespace SandBox.Winform.LogonTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtUsername.Text = Environment.UserName;
            txtDomain.Text = Environment.UserDomainName;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            txtErrorCode.Text = "";
            rtbMessage.Clear();

            ImpersonateUser iU = new ImpersonateUser();
            if(iU.Impersonate(txtDomain.Text, txtUsername.Text, txtPassword.Text))
            {
                iU.Undo();
                rtbMessage.AppendText("Valid user");
            }
            else
            {
                txtErrorCode.Text = iU.miErrorCode.ToString();
                rtbMessage.AppendText(iU.mszErrorMessage);
            }
        }
    }
}
