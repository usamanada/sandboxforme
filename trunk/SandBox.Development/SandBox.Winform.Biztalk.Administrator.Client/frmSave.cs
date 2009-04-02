using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SandBox.Winform.Biztalk.Administrator;

namespace SandBox.Winform.Biztalk.Administrator.Client
{
    public partial class frmSave : Form
    {
        #region members
        public enum SaveType { File = 0, Server =1};
        private Dictionary<string, Host> mHosts = new Dictionary<string,Host>();
        private Dictionary<string, Host> mFileHosts;
        private Dictionary<string, Host> mServerHosts;
        private Dictionary<string, Host> mMergeHosts;
        private Dictionary<string, Party> mParty = new Dictionary<string, Party>();
        private XmlHelper mXmlHelper = new XmlHelper();
        private SaveType mSaveType;
        #endregion

        public frmSave(object SaveObject, SaveType st)
        {            
            InitializeComponent();
            mSaveType = st;
            btnClose.Visible = false;
            mXmlHelper.LogMessage += new LogEventHandler(xmlHelper_LogMessage);

            if (SaveObject.GetType() == mHosts.GetType())
            {
                mHosts = (Dictionary<string, Host>)SaveObject;
                DisplaySetup("Host");

                mServerHosts = Helper.GetBiztalkHosts();
                mMergeHosts = Administrator.Host.MergeCheckedHostsToSourceHosts(mServerHosts, mHosts);
                DisplayHostMergedResult();
            }
            else if (SaveObject.GetType() == mParty.GetType())
            {
                mParty = (Dictionary<string, Party>)SaveObject;
                DisplaySetup("Party");
            }


        }
        void xmlHelper_LogMessage(object sender, LogEventArg e)
        {
            rtbLog.AppendText(e.Message + Environment.NewLine);
            if (e.HostName != String.Empty)
            {
                int rowCount = dgvResults.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    if (dgvResults.Rows[i].Cells["Host"].Value.ToString() == e.HostName)
                    {
                        dgvResults.Rows[i].Cells["Status"].Value = e.HostMessage;
                    }
                }
            }
        }
        #region Display
        private void DisplaySetup(string objectType)
        {
            this.Text = String.Format("Save {0} information to {1}", objectType, mSaveType.ToString());
            if (mSaveType == SaveType.File)
            {
                pnlFile.Visible = true;
            }
            else
            {
                pnlFile.Visible = false;
            }
        }
        private void DisplayHostMergedResult()
        {
            if (mMergeHosts != null)
            {
                foreach (KeyValuePair<string, Host> kMergeHost in mMergeHosts)
                {
                    Host mergeHost = kMergeHost.Value;

                    if (mSaveType == SaveType.File)
                    {
                        dgvResults.Rows.Add(new object[] { mergeHost.Status.ToString(), mergeHost.Name });
                    }
                    else
                    {
                        if (mergeHost.Status != HostStatus.None)
                        {
                            dgvResults.Rows.Add(new object[] { mergeHost.Status.ToString(), mergeHost.Name });
                        }
                    }
                }
            }
        }
        #endregion

        

        private void btSave_Click(object sender, EventArgs e)
        {
            rtbLog.Text = "";
            if (mSaveType == SaveType.File)
            {
                mXmlHelper.ExportHostsToFile(mMergeHosts, txtFile.Text);
            }
            else
            {
                Helper.ExportHostsToServer(mMergeHosts, mServerHosts);
            }
            btnCancel.Visible = false;
            btnClose.Visible = true;
            btnSave.Visible = false;
        }

        #region File and Merge
        private void GetFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xml Files|*.xml";
            sfd.OverwritePrompt = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = sfd.FileName;
            }
        }
        private void LoadFileAndMergeHosts()
        {
            if (mHosts != null)
            {
                mFileHosts = mXmlHelper.ImportHostsFromFile(txtFile.Text);
                if (mFileHosts == null)
                {
                    mFileHosts = new Dictionary<string, Host>();
                }
            }
            mMergeHosts = Administrator.Host.MergeCheckedHostsToSourceHosts(mFileHosts, mHosts);
        }
        private void btnFile_Click(object sender, EventArgs e)
        {
            GetFile();
            LoadFileAndMergeHosts();
            DisplayHostMergedResult();
        }        
        #endregion
    }
}