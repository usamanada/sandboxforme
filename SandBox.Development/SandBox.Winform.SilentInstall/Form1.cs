using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using SandBox.dll.Common;

namespace SandBox.Winform.SilentInstall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private InstallHelper _iHelper = new InstallHelper();
        private EnvironmentsSection _es;
        private InstallApplicationsSection _ia;
        private readonly Dictionary<string, string> _dInstallApplications = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _dEnvironments = new Dictionary<string, string>();
        private bool _cbxEnvironmentsSelectedIndexChangedExecute;
        private const string ConstCustomDesc = "Create Custom Install";
        private const string ConstCustomKey = "Custom";
        private const string ConstReboot = "Reboot";
        private enum ControlState
        {
            ValidUser,
            InValidUser,
            HideAll,
            NoReboot,
            Reboot
        }
        private DataTable dtProcess = new DataTable();
        private void setLabelCredentials(ControlState cs)
        {
            switch (cs)
            {
                case ControlState.ValidUser:
                    lblInvalidUserCredentials.Visible = false;
                    lblValidCredentials.Visible = true;
                    break;
                case ControlState.InValidUser:

                    lblInvalidUserCredentials.Visible = true;
                    lblValidCredentials.Visible = false;
                    break;
                case ControlState.HideAll:
                    lblInvalidUserCredentials.Visible = false;
                    lblValidCredentials.Visible = false;
                    break;
                default:
                    break;
            }

        }
        private void setRebootRequired(ControlState cs)
        {
            switch (cs)
            {
                case ControlState.Reboot:
                    gbxAutoLogin.Enabled = true;
                    btnInstall.Enabled = false;
                    setLabelCredentials(ControlState.HideAll);
                    break;
                case ControlState.NoReboot:
                    gbxAutoLogin.Enabled = false;
                    btnInstall.Enabled = true;
                    setLabelCredentials(ControlState.HideAll);
                    break;
                default:
                    break;
            }

        }
        private void cbxEnvironments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cbxEnvironmentsSelectedIndexChangedExecute)
            {
                KeyValuePair<string, string> kp = (KeyValuePair<string, string>)cbxEnvironments.SelectedItem;
                List<string> lstInstalling = new List<string>();
                lbxInstall.Items.Clear();
                lbxAvailable.Items.Clear();
                lstInstalling.Clear();
                setRebootRequired(ControlState.NoReboot);

                EnvironmentElement en = _es.EnvironmentItems[kp.Key];

                if (en != null)
                {
                    foreach (ApplicationElement ae in en.ApplicationItems)
                    {
                        lstInstalling.Add(ae.Value);

                        lbxInstallAdd(new ListBoxDisplay() { Display = _ia.InstallApplicationsItems[ae.Value].Description, Value = ae.Value });
                    }
                }
                var o = _dInstallApplications.Keys.Except(lstInstalling);

                foreach (string application in o.ToArray())
                {
                    lbxAvailable.Items.Add(new ListBoxDisplay() { Display = _ia.InstallApplicationsItems[application].Description, Value = application });
                }


                if (kp.Key == ConstCustomKey)
                {
                    btnUp.Enabled = true;
                    btnDown.Enabled = true;
                    btnLeft.Enabled = true;
                    btnRight.Enabled = true;
                    lbxAvailable.Enabled = true;
                }
                else
                {
                    btnUp.Enabled = false;
                    btnDown.Enabled = false;
                    btnLeft.Enabled = false;
                    btnRight.Enabled = false;
                    lbxAvailable.Enabled = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _es = ConfigurationManager.GetSection("InstallChoice") as EnvironmentsSection;

                _ia = ConfigurationManager.GetSection("InstallApplications") as InstallApplicationsSection;

                if (_es != null)
                {
                    foreach (EnvironmentElement ee in _es.EnvironmentItems)
                    {
                        _dEnvironments.Add(ee.Name, ee.Description);
                    }
                }
                if (_ia != null)
                {
                    foreach (InstallApplicationsElement iae in _ia.InstallApplicationsItems)
                    {
                        _dInstallApplications.Add(iae.Name, iae.Description);
                    }
                }
                _dEnvironments.Add(ConstCustomKey, ConstCustomDesc);
                _cbxEnvironmentsSelectedIndexChangedExecute = false;
                cbxEnvironments.DataSource = new BindingSource(_dEnvironments, null);
                cbxEnvironments.DisplayMember = "Value";
                cbxEnvironments.ValueMember = "Key";

                _cbxEnvironmentsSelectedIndexChangedExecute = true;
                cbxEnvironments_SelectedIndexChanged(this, null);

                txtUserName.Text = Environment.UserName;
                txtDomain.Text = Environment.UserDomainName;

                btnInstall.Enabled = false;
                setLabelCredentials(ControlState.HideAll);

                dtProcess.Columns.Add("Status", typeof(string));
                dtProcess.Columns.Add("Order", typeof(int));
                dtProcess.Columns.Add("Description", typeof(string));

                dgvProgress.DataSource = dtProcess;

                lbxInstall.DisplayMember = "Display";
                lbxInstall.ValueMember = "Value";

                lbxAvailable.DisplayMember = "Display";
                lbxAvailable.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (gbxAutoLogin.Enabled)
            {
                _iHelper.SaveAutoLoginRegDetails();
                _iHelper.SetAutoLoginRegDetails(txtUserName.Text, txtDomain.Text, txtPassword.Text);
            }
            _iHelper.WorkConfigCreate((from ListBoxDisplay item in lbxInstall.Items select item).ToList());
            _iHelper.CopyBatchFiles();
            DoWork();
        }
        private void DoWork()
        {
            _iHelper.WorkConfigReadApplications();

            for (int index = 0; index < _iHelper.applications.Count; index++)
            {
                DataRow dr = dtProcess.NewRow();
                dr["Status"] = "";
                dr["Order"] = (index + 1).ToString();
                dr["Description"] = _ia.InstallApplicationsItems[_iHelper.applications[index + 1]].Description;
                dtProcess.Rows.Add(dr);
            }

            processWorker.RunWorkerAsync(1);
        }

        private void StartProcessInstallation()
        {
            while (_iHelper.currentOrder < _iHelper.applications.Count)
            {
                processInstallation(_iHelper.currentOrder++);
            }
        }

        private void processInstallation(int index)
        {
            string executeApp = _ia.InstallApplicationsItems[_iHelper.applications[index + 1]].Execute;
            executeApp = executeApp.Replace("[BASEINSTALLDIR]", _iHelper.createWorkDir());
            
            string message = DateTime.Now.ToString() + " Starting to Install " +
                             _ia.InstallApplicationsItems[_iHelper.applications[index + 1]].Description +
                             Environment.NewLine;
            
            processWorker.ReportProgress(0, message);
            
            ProcessStartInfo startInfo = new ProcessStartInfo();
            Process p = new Process();
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = executeApp;

            p.StartInfo = startInfo;


            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            startInfo.RedirectStandardInput = true;
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();

            processWorker.ReportProgress(0, DateTime.Now.ToString() + " Complete: " + _iHelper.applications[index + 1] + Environment.NewLine);

        }

        void p_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {

            if (!String.IsNullOrEmpty(e.Data))
            {
                processWorker.ReportProgress(0, DateTime.Now.ToString() + " Progress: " + e.Data + Environment.NewLine);
            }
        }


        private void btnRight_Click(object sender, EventArgs e)
        {
            if (lbxInstall.SelectedItem == null)
            {
                return;
            }

            lbxAvailable.Items.Add(lbxInstall.SelectedItem.ToString());
            lbxInstallRemove((ListBoxDisplay)lbxInstall.SelectedItem);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (lbxAvailable.SelectedItem != null)
            {
                lbxInstallAdd((ListBoxDisplay)lbxAvailable.SelectedItem);
                lbxAvailable.Items.Remove(lbxAvailable.SelectedItem);
            }
        }

        private void lbxInstallAdd(ListBoxDisplay application)
        {
            lbxInstall.Items.Add(application);
            if (application.Display == ConstReboot)
            {
                setRebootRequired(ControlState.Reboot);
            }
        }
        private void lbxInstallRemove(ListBoxDisplay application)
        {
            lbxInstall.Items.Remove(application);
            if (application.Display == ConstReboot)
            {
                setRebootRequired(ControlState.NoReboot);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lbxInstall.SelectedItem != null)
            {
                int index = lbxInstall.SelectedIndex;
                object swap = lbxInstall.SelectedItem;

                if (index > 0)
                {
                    lbxInstall.Items.RemoveAt(index);
                    lbxInstall.Items.Insert(index - 1, swap);
                    lbxInstall.SelectedItem = swap;
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lbxInstall.SelectedItem != null)
            {
                int index = lbxInstall.SelectedIndex;
                object swap = lbxInstall.SelectedItem;

                if (index < lbxInstall.Items.Count - 1)
                {
                    lbxInstall.Items.RemoveAt(index);
                    lbxInstall.Items.Insert(index + 1, swap);
                    lbxInstall.SelectedItem = swap;
                }
            }
        }

        private void btnCleanAutoLogins_Click(object sender, EventArgs e)
        {
            _iHelper.CleanAutoLoginRegDetails();
        }

        private void btnCleanContineBat_Click(object sender, EventArgs e)
        {
            _iHelper.CleanContinueBatch();
        }

        private void btnCopyContineBat_Click(object sender, EventArgs e)
        {
            _iHelper.CopyContineBatch();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblInvalidUserCredentials.Text = "Passwords do not match";
                setLabelCredentials(ControlState.InValidUser);
                return;
            }
            ImpersonateUser iU = new ImpersonateUser();
            //txtDomain.Text + @"\" +
            string domainUser = txtUserName.Text;
            if (iU.Impersonate("remoteMachine", domainUser, txtPassword.Text))
            {
                iU.Undo();
                setLabelCredentials(ControlState.ValidUser);
                btnInstall.Enabled = true;
            }
            else
            {
                lblInvalidUserCredentials.Text = "InValid Username and Password Entered";
                setLabelCredentials(ControlState.InValidUser);
            }
        }

        private void btnReadWorkAutoLogin_Click(object sender, EventArgs e)
        {
            _iHelper.ReadAutoLoginDetails();
        }

        private void btnIncreamentOrder_Click(object sender, EventArgs e)
        {
            _iHelper.SetOrder(1);
        }

        private void btnCopyFiles_Click(object sender, EventArgs e)
        {
            _iHelper.CopyBatchFiles();
        }


        private void btnProgress_Click(object sender, EventArgs e)
        {
            DoWork();
        }

        private void processWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            StartProcessInstallation();
        }

        private void processWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            rtbLog.AppendText(e.UserState.ToString());
        }
    }
}
