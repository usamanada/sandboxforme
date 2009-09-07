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
        #region private Members
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
            NoUserDetailsRequired,
            UserDetailsRequired,
            Disable,
            Enable
        }
        private enum ProcessExit
        {
            Complete,
            Error,
            Reboot
        }
        #endregion

        private void SetLabelCredentials(ControlState cs)
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
        private void SetRebootRequired(ControlState cs)
        {
            switch (cs)
            {
                case ControlState.UserDetailsRequired:
                    gbxAutoLogin.Enabled = true;
                    btnInstall.Enabled = false;
                    SetLabelCredentials(ControlState.HideAll);
                    break;
                case ControlState.NoUserDetailsRequired:
                    gbxAutoLogin.Enabled = false;
                    btnInstall.Enabled = true;
                    SetLabelCredentials(ControlState.HideAll);
                    break;
                default:
                    break;
            }
        }
        private void SetProcesingControl(ControlState cs)
        {
            switch (cs)
            {
                case ControlState.Disable:
                    tbpInstall.Enabled = false;
                    tbpAdmin.Enabled = false;
                    break;
                case ControlState.Enable:
                    tbpInstall.Enabled = true;
                    tbpAdmin.Enabled = true;
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
                SetRebootRequired(ControlState.NoUserDetailsRequired);

                EnvironmentElement en = _es.EnvironmentItems[kp.Key];

                if (en != null)
                {
                    foreach (ApplicationElement ae in en.ApplicationItems)
                    {
                        lstInstalling.Add(ae.Value);

                        LbxInstallAdd(new ListBoxDisplay { Display = _ia.InstallApplicationsItems[ae.Value].Description, Value = ae.Value });
                    }
                }
                var o = _dInstallApplications.Keys.Except(lstInstalling);

                foreach (string application in o.ToArray())
                {
                    lbxAvailable.Items.Add(new ListBoxDisplay { Display = _ia.InstallApplicationsItems[application].Description, Value = application });
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
                if (!_iHelper.ValidateConfigFile())
                {
                    MessageBox.Show(_iHelper.Error, "Error in the App Configuration File");
                    return;
                }
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

                //btnInstall.Enabled = false;
                SetLabelCredentials(ControlState.HideAll);

                lbxInstall.DisplayMember = "Display";
                lbxInstall.ValueMember = "Value";

                lbxAvailable.DisplayMember = "Display";
                lbxAvailable.ValueMember = "Value";

                CheckToContinue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        private void SetDataGridViewSelectRow(int rowIndex)
        {
            dgvProgress.ClearSelection();
            dgvProgress.FirstDisplayedScrollingRowIndex = rowIndex;
            dgvProgress.CurrentCell = dgvProgress[0, rowIndex];
            dgvProgress.Rows[rowIndex].Selected = false;
        }
        private void CheckToContinue()
        {
            if (File.Exists(_iHelper.GetFilePathWorkConfig()))
            {
                _iHelper.WorkConfigReadApplications();
                if (_iHelper.CurrentOrder > 0)
                {
                    DoWork();
                }
            }
        }
        private bool CheckBatchFilesExist()
        {
            bool result = true;
            List<ListBoxDisplay> installApplications = (from ListBoxDisplay item in lbxInstall.Items select item).ToList();
            for (int rowIndex = 0; rowIndex < installApplications.Count; rowIndex++)
            {
                string executeApp = _ia.InstallApplicationsItems[installApplications[rowIndex].Value.ToString()].Execute;
                executeApp = executeApp.Replace("[BASEINSTALLDIR]", _iHelper.CreateWorkDir());
                if (!File.Exists(executeApp))
                {
                    dgvProgress.Rows[rowIndex].Cells["dgcProgImage"].Value = Properties.Resources.Critical;
                    dgvProgress.Rows[rowIndex].Cells["dgcProgMessage"].Value = string.Format("Batch file {0} not found.", executeApp);
                    result = false;
                }
                else
                {
                    if (rowIndex >= _iHelper.CurrentOrder)
                    {
                        dgvProgress.Rows[rowIndex].Cells["dgcProgImage"].Value = Properties.Resources.empty;
                    }
                }
                SetDataGridViewSelectRow(rowIndex);
            }
            if (result == false)
            {
                MessageBox.Show("Address the following errors before continuing", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            return result;
        }
        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (gbxAutoLogin.Enabled)
            {
                _iHelper.SaveAutoLoginRegDetails();
                _iHelper.SetAutoLoginRegDetails(txtUserName.Text, txtDomain.Text, txtPassword.Text);
            }
            _iHelper.WorkConfigCreate((from ListBoxDisplay item in lbxInstall.Items select item).ToList());
            _iHelper.CopyBatchFiles(txtDomain.Text, txtUserName.Text, txtPassword.Text, Environment.MachineName);
            DoWork();
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblInvalidUserCredentials.Text = "Passwords do not match";
                SetLabelCredentials(ControlState.InValidUser);
                return;
            }
            ImpersonateUser iU = new ImpersonateUser();

            if (iU.Impersonate(txtDomain.Text, txtUserName.Text, txtPassword.Text))
            {
                iU.Undo();
                SetLabelCredentials(ControlState.ValidUser);
                btnInstall.Enabled = true;
            }
            else
            {
                lblInvalidUserCredentials.Text = "InValid Username and Password Entered";
                SetLabelCredentials(ControlState.InValidUser);
            }
        }

        #region Run Processes
        private void DoWork()
        {
            try
            {
                tbcInstall.SelectedIndex = tbcInstall.TabPages["tbpProgress"].TabIndex;

                _iHelper.WorkConfigReadApplications();

                rtbLog.Clear();
                dgvProgress.Rows.Clear();

                if (File.Exists(_iHelper.GetFilePathWorkConfig()))
                {
                    if (File.Exists(_iHelper.LogFilePath()))
                    {
                        rtbLog.LoadFile(_iHelper.LogFilePath());
                        rtbLog.Refresh();
                    }

                    for (int index = 0; index < _iHelper.applications.Count; index++)
                    {
                        int rowId = dgvProgress.Rows.Add();
                        if (index < _iHelper.CurrentOrder)
                        {
                            dgvProgress.Rows[rowId].Cells["dgcProgImage"].Value = Properties.Resources.OK;
                        }
                        else
                        {
                            dgvProgress.Rows[rowId].Cells["dgcProgImage"].Value = Properties.Resources.empty;
                        }
                        dgvProgress.Rows[rowId].Cells["dgcProgOrder"].Value = (index + 1).ToString();
                        dgvProgress.Rows[rowId].Cells["dgcProgApplication"].Value =
                            _ia.InstallApplicationsItems[_iHelper.applications[index + 1]].Description;
                        SetDataGridViewSelectRow(rowId);
                    }
                    dgvProgress.AutoResizeColumns();
                    if (CheckBatchFilesExist())
                    {
                        SetProcesingControl(ControlState.Disable);
                        processWorker.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void processWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage == 1)
                {
                    int rowIndex = ((ProgressObject)e.UserState).index - 1;
                    dgvProgress.Rows[rowIndex].Cells["dgcProgImage"].Value = Properties.Resources.OK;
                    SetDataGridViewSelectRow(rowIndex);
                }
                else if (e.ProgressPercentage == 2)
                {
                    int rowIndex = ((ProgressObject)e.UserState).index - 1;
                    dgvProgress.Rows[rowIndex].Cells["dgcProgImage"].Value = Properties.Resources.Run;
                    SetDataGridViewSelectRow(rowIndex);
                }
                else if (e.ProgressPercentage == 3)
                {
                    ProgressObject po = ((ProgressObject)e.UserState);
                    int rowIndex = po.index - 1;
                    dgvProgress.Rows[rowIndex].Cells["dgcProgImage"].Value = Properties.Resources.Critical;
                    dgvProgress.Rows[rowIndex].Cells["dgcProgMessage"].Value = po.message;
                    SetDataGridViewSelectRow(rowIndex);
                }
                else
                {
                    rtbLog.AppendText(e.UserState.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void processWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetProcesingControl(ControlState.Enable);
        }
        #region Background Thread

        private ProcessExit StartProcessInstallation()
        {
            ProcessExit pe = ProcessExit.Complete;
            try
            {

                while (_iHelper.CurrentOrder < _iHelper.applications.Count)
                {
                    _iHelper.SetOrder(++_iHelper.CurrentOrder);

                    pe = ProcessInstallation(_iHelper.CurrentOrder);

                    if (pe != ProcessExit.Complete)
                        return pe;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return pe;
        }

        private ProcessExit ProcessInstallation(int index)
        {
            string executeApp = _ia.InstallApplicationsItems[_iHelper.applications[index]].Execute;
            executeApp = executeApp.Replace("[BASEINSTALLDIR]", _iHelper.CreateWorkDir());

            processWorker.ReportProgress(2, new ProgressObject { index = index, message = "" });
            processWorker.ReportProgress(0, string.Format("{0} Step: {1}{2}", DateTime.Now, index, Environment.NewLine));
            string message = string.Format("{0} Starting to Install {1}{2}", DateTime.Now, _ia.InstallApplicationsItems[_iHelper.applications[index]].Description, Environment.NewLine);

            processWorker.ReportProgress(0, message);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            Process p = new Process();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = executeApp;
            startInfo.Arguments = " >> " + _iHelper.ApplicationLogFilePath();
            p.StartInfo = startInfo;

            p.Start();
            p.WaitForExit();

            if (File.Exists(_iHelper.ApplicationLogFilePath()))
            {
                using (StreamReader sr = new StreamReader(_iHelper.ApplicationLogFilePath()))
                {
                    processWorker.ReportProgress(0, sr.ReadToEnd());
                    processWorker.ReportProgress(0, Environment.NewLine);
                }
            }

            _iHelper.ApplicationLogFileDelete();

            processWorker.ReportProgress(0, DateTime.Now + " Exit Code: " + p.ExitCode + Environment.NewLine);
            string exitCodes = _ia.InstallApplicationsItems[_iHelper.applications[index]].ExitCode;

            List<string> lstExitCodes = exitCodes.Split(new char[] { ',' }).ToList();
            if (!lstExitCodes.Contains(p.ExitCode.ToString()))
            {
                processWorker.ReportProgress(3, new ProgressObject { index = index, message = "The install application has returned an invalid exit code of: " + p.ExitCode + ". Valid exit codes are : " + exitCodes });
                return ProcessExit.Error;
            }

            processWorker.ReportProgress(1, new ProgressObject { index = index, message = "" });
            processWorker.ReportProgress(0, DateTime.Now + " Complete: " + _iHelper.applications[index] + Environment.NewLine + Environment.NewLine);

            if (_iHelper.applications[index] == ConstReboot)
            {
                rtbLog.BeginInvoke(new MethodInvoker(delegate { rtbLog.SaveFile(_iHelper.LogFilePath()); }));
                return ProcessExit.Reboot;
            }
            return ProcessExit.Complete;
        }

        private void processWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ProcessExit pe = StartProcessInstallation();
                if (pe == ProcessExit.Reboot)
                {
                    Application.Exit();
                    return;
                }
                else if (pe == ProcessExit.Error)
                {
                    MessageBox.Show("An error has been detected during the installation process.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _iHelper.CleanAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion
        #endregion
        #region Move List Items
        private void btnRight_Click(object sender, EventArgs e)
        {
            if (lbxInstall.SelectedItem == null)
            {
                return;
            }

            lbxAvailable.Items.Add(lbxInstall.SelectedItem.ToString());
            LbxInstallRemove((ListBoxDisplay)lbxInstall.SelectedItem);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (lbxAvailable.SelectedItem != null)
            {
                LbxInstallAdd((ListBoxDisplay)lbxAvailable.SelectedItem);
                lbxAvailable.Items.Remove(lbxAvailable.SelectedItem);
            }
        }

        private void LbxInstallAdd(ListBoxDisplay application)
        {
            lbxInstall.Items.Add(application);

            if (checkUserDetailsRequired())
            {
                SetRebootRequired(ControlState.UserDetailsRequired);
            }

        }
        private void LbxInstallRemove(ListBoxDisplay application)
        {
            lbxInstall.Items.Remove(application);
            if (!checkUserDetailsRequired())
            {
                SetRebootRequired(ControlState.NoUserDetailsRequired);
            }
        }
        private bool checkUserDetailsRequired()
        {
            foreach (ListBoxDisplay item in lbxInstall.Items)
            {
                if (_ia.InstallApplicationsItems[item.Value.ToString()].UserPassRequired == "1" || item.Value.ToString() == "Reboot")
                {
                    return true;
                }
            }
            return false;
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
        #endregion
        #region Admin Function
        private void btnCreateWorkConfig_Click(object sender, EventArgs e)
        {
            _iHelper.WorkConfigCreate((from ListBoxDisplay item in lbxInstall.Items select item).ToList());
        }
        private void btnCopyBatchFiles_Click(object sender, EventArgs e)
        {
            _iHelper.CopyBatchFiles(txtDomain.Text, txtUserName.Text, txtPassword.Text, Environment.MachineName);
        }
        private void btnCleanAutoLogins_Click(object sender, EventArgs e)
        {
            _iHelper.CleanAutoStart();
        }
        #endregion

    }
}
