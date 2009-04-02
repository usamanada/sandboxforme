using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizTalkSetUp;
using System.Xml;
using System.Management;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkSetUp
{
    public partial class BuildHosts : Form
    {
        enum HostPanels { HOST, HOSTINSTANCE, APAPTERS, HIDEALL };
        private List<string> mAllSendAdapters;
        private List<string> mAllReceiveAdapters;
        private TreeNode mTnTrvHostDrawSelectNode;
        private TreeNode mTnTrvHostAfterSelectNode;
        private Dictionary<string, Host> mHosts;
        private Dictionary<string, Party> mParties;

        
        public BuildHosts()
        {
            InitializeComponent();

            AddDataGridViewTextBoxColumn("Name", dgvPartyAliases);
            AddDataGridViewTextBoxColumn("Qualifier", dgvPartyAliases);
            AddDataGridViewTextBoxColumn("Value", dgvPartyAliases);
            SetDataGridViewColumnStyle(dgvPartyAliases);

            AddDataGridViewTextBoxColumn("Name", dgvPartySendPorts);
            AddDataGridViewTextBoxColumn("URI", dgvPartySendPorts);
            SetDataGridViewColumnStyle(dgvPartySendPorts);

            mAllSendAdapters = WMIMethods.GetAllSendAdapters();
            mAllReceiveAdapters = WMIMethods.GetAllReceiveAdapters();

            List<string> HostTypes = new List<string>(Enum.GetNames(typeof(HostType)));
            HostTypes.Remove(HostType.Invalid.ToString());
            cmbHostType.DataSource = HostTypes;

        }
        private void TrvHostSelectNode(TreeNode clickedNode)
        {
            mTnTrvHostDrawSelectNode = clickedNode;
            trvHost.SelectedNode = clickedNode;
        }
        private void SetDataGridViewColumnStyle(DataGridView dgv)
        {
            foreach (DataGridViewColumn dc in dgv.Columns)
            {
                dc.DefaultCellStyle.ForeColor = SystemColors.ControlDarkDark;
                dc.DefaultCellStyle.SelectionBackColor = SystemColors.ControlDarkDark;
            }
        }
        private void AddDataGridViewTextBoxColumn(string propertyName, DataGridView dgv)
        {
            DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
            colText.DataPropertyName = propertyName;
            colText.HeaderText = propertyName;
            colText.Name = propertyName;
            dgv.Columns.Add(colText);
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            //Dictionary<string, Host> Hosts = LoadHostsFromFile();
            //try
            //{
            //    foreach (KeyValuePair<string, Host> kHost in Hosts)
            //    {
            //        Host host = kHost.Value;
            //        WMIMethods.AddHost(host);
            //        if (host.SetHostInstancesAction)
            //        {
            //            foreach (KeyValuePair<string, HostInstance> kHostInstance in host.HostInstances)
            //            {
            //                HostInstance hostInstance = kHostInstance.Value;

            //                if (hostInstance.PasswordPrompt)
            //                {
            //                    frmPasswordInput inputForm = new frmPasswordInput();
            //                    inputForm.label1.Text = "Enter password for " + hostInstance.UserName;
            //                    if (inputForm.ShowDialog(this) == DialogResult.OK)
            //                    {
            //                        hostInstance.Password = inputForm.txtPassword.Text;
            //                    }

            //                }
            //                WMIMethods.AddHostinstance(hostInstance, host.Name);
            //            }
            //        }

            //        if (host.SetAdaptersAction)
            //        {
            //            foreach (KeyValuePair<string, Adapter> kAdapter in host.ReceiveAdapters)
            //            {
            //                WMIMethods.AddReceiveHostHandler(kAdapter.Value, host.Name);
            //            }
            //            foreach (KeyValuePair<string, Adapter> kAdapter in host.SendAdapters)
            //            {
            //                WMIMethods.AddSendHostHandler(kAdapter.Value, host.Name);
            //            }
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    txtMessage.Text += "\r\n Error: " + ex.Message;
            //}
        }

        private void btnResetAdapters_Click(object sender, EventArgs e)
        {
            // Load the Config XML
            XmlDocument oDoc = new XmlDocument();
            oDoc.Load(@"..\..\HostConfig.xml");

            string defaulInProcessHost = oDoc.DocumentElement.GetAttribute("defaultHost").ToString();
            string defaulIsoHost = oDoc.DocumentElement.GetAttribute("defaultIsoHost").ToString();

            try
            {
                PutOptions options = new PutOptions();
                options.Type = PutType.UpdateOnly;

                //Look for the target WMI Class MSBTS_ReceiveHandler instance
                string strWQL = "SELECT * FROM MSBTS_ReceiveHandler";
                ManagementObjectSearcher searcherReceiveHandler = new ManagementObjectSearcher(new ManagementScope("root\\MicrosoftBizTalkServer"), new WqlObjectQuery(strWQL), null);

                string recName;
                string recHost;
                string sndName;
                string sndHost;

                if (searcherReceiveHandler.Get().Count > 0)
                    foreach (ManagementObject objReceiveHandler in searcherReceiveHandler.Get())
                    {
                        //Get the Adapter Name
                        recName = objReceiveHandler["AdapterName"].ToString();

                        // Get the Current Host
                        recHost = objReceiveHandler["HostName"].ToString();


                        // Find the Host Type
                        string strWQLHost = "SELECT * FROM MSBTS_HostInstanceSetting where HostName = '" + recHost + "'";
                        ManagementObjectSearcher searcherHostHandler = new ManagementObjectSearcher(new ManagementScope("root\\MicrosoftBizTalkServer"), new WqlObjectQuery(strWQLHost), null);

                        foreach (ManagementObject objHostHandler in searcherHostHandler.Get())
                        {
                            // Type 1 is In Process
                            if (objHostHandler["HostType"].ToString() == "1")
                            {
                                objReceiveHandler.SetPropertyValue("HostNameToSwitchTo", defaulInProcessHost);
                                objReceiveHandler.Put();
                            }
                            // Otherwise it is Isolated
                            else
                            {
                                objReceiveHandler.SetPropertyValue("HostNameToSwitchTo", defaulIsoHost);
                                objReceiveHandler.Put();
                            }
                        }

                        txtMessage.Text += "Receive Adapters: - " + recName + " \r\n";
                    }

                //Look for the target WMI Class MSBTS_SendHandler instance
                string strWQLsnd = "SELECT * FROM MSBTS_SendHandler2";
                ManagementObjectSearcher searcherSendHandler = new ManagementObjectSearcher(new ManagementScope("root\\MicrosoftBizTalkServer"), new WqlObjectQuery(strWQLsnd), null);

                if (searcherSendHandler.Get().Count > 0)
                    foreach (ManagementObject objSendHandler in searcherSendHandler.Get())
                    {
                        //Get the Adapter Name
                        sndName = objSendHandler["AdapterName"].ToString();

                        // Get the Current Host
                        sndHost = objSendHandler["HostName"].ToString();

                        // Find the Host Type
                        string strWQLHost = "SELECT * FROM MSBTS_HostInstanceSetting where HostName = '" + sndHost + "'";
                        ManagementObjectSearcher searcherHostHandler = new ManagementObjectSearcher(new ManagementScope("root\\MicrosoftBizTalkServer"), new WqlObjectQuery(strWQLHost), null);

                        foreach (ManagementObject objHostHandler in searcherHostHandler.Get())
                        {
                            // Type 1 is In Process
                            if (objHostHandler["HostType"].ToString() == "1")
                            {
                                objSendHandler.SetPropertyValue("HostNameToSwitchTo", defaulInProcessHost);
                                objSendHandler.Put();
                            }
                            // Otherwise it is Isolated
                            else
                            {
                                objSendHandler.SetPropertyValue("HostNameToSwitchTo", defaulIsoHost);
                                objSendHandler.Put();
                            }
                        }

                        txtMessage.Text += "Send Adapters: - " + sndName + " \r\n";
                    }

                txtMessage.Text += "Done";
            }
            catch (Exception ex)
            {
                txtMessage.Text += ex.Message;
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Dictionary<string, Host> Hosts = Helper.GetBiztalkHosts();
            SaveToFile(Hosts);
            txtMessage.Text = "Export complete";
        }
        private void SaveToFile(Dictionary<string, Host> Hosts)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xml Files|*.xml";
            sfd.OverwritePrompt = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //XmlHelper.ExportHostsToFile(Hosts, sfd.FileName);
            }
        }
        private Dictionary<string, Host> LoadHostsFromFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml Files|*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return (new XmlHelper()).ImportHostsFromFile(ofd.FileName);
            }
            return null;
        }

        private void BuildHosts_Load(object sender, EventArgs e)
        {
            pnlHostDetails.Visible = false;
            pnlHostAdapters.Visible = false;
            pnlHostInstance.Visible = false;

            pnlHostDetails.Dock = DockStyle.Fill;
            pnlHostAdapters.Dock = DockStyle.Fill;
            pnlHostInstance.Dock = DockStyle.Fill;
        }
        private void trvHost_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Level != 0)
            {
                e.Cancel = true;
            }
        }

        private void trvHost_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            Console.WriteLine("trvHost_BeforeSelect");

            if (mTnTrvHostAfterSelectNode != null)
            {
                if (mTnTrvHostAfterSelectNode.Level == 0)
                {
                    SaveHostDetails((Host)mTnTrvHostAfterSelectNode.Tag);
                }
                else if (mTnTrvHostAfterSelectNode.Level == 2)
                {
                    SaveHostInstanceDetails((HostInstance)mTnTrvHostAfterSelectNode.Tag);
                }
            }
        }
        private void SaveHostInstanceDetails(HostInstance hi)
        {
            hi.ServerName = txtHostServer.Text;
            hi.UserName = txtHostLogon.Text;
            hi.Password = txtHostPassword.Text;
            hi.Disable = chbHostDisableHost.Checked;
        }
        private void SaveHostDetails(Host h)
        {
            h.Name = txtHostName.Text;
            h.Type = cmbHostType.SelectedItem.ToString();
            h.AllowHostTracking = chbHostAllowHostTracking.Checked;
            h.AuthenticationTrusted = chbHostAuthenticationiTrusted.Checked;
            h.ThirtyTwoBitOnly = chbHost32BitOnly.Checked;
            h.DefualtHost = chbMakeDefaultHostGroup.Checked;
            h.WindowsGroup = tbxHostWindowsGroup.Text;
        }
        private void SaveDetails()
        {
            switch (tbcBiztalk.SelectedTab.Name)
            {
                case "tbpHost":
                    if (mTnTrvHostAfterSelectNode.Tag is Host)
                    {
                        SaveHostDetails((Host)mTnTrvHostAfterSelectNode.Tag);
                    }
                    else if (mTnTrvHostAfterSelectNode.Tag is HostInstance)
                    {
                        SaveHostInstanceDetails((HostInstance)mTnTrvHostAfterSelectNode.Tag);
                    }
                    break;

                case "tbpParty":
                    throw new Exception("The method or operation is not implemented.");
                    break;
            }
            
        }

        private TreeNode GetRootParent(TreeNode child)
        {
            if (child.Parent == null)
            {
                return child;
            }
            else
            {
                return GetRootParent(child.Parent);
            }
        }

        private void trvHost_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Console.WriteLine("trvHost_AfterSelect");
            mTnTrvHostAfterSelectNode = e.Node;

            TreeNode tnRootParent = GetRootParent(e.Node);
            tssOrigination.Text = "Origination: " + ((Host)tnRootParent.Tag).Origination.ToString();
            setStatusStrip(((Host)tnRootParent.Tag).Status);

            if (e.Node.Level == 1 && e.Node.Text == Constants.FORM_ADAPTERS)
            {
                DisplayHostPanels(HostPanels.APAPTERS);
                DisplayHostApaters((Host)e.Node.Tag);
            }
            else if (e.Node.Level == 0)
            {
                DisplayHostPanels(HostPanels.HOST);
                DisplayHostDetails((Host)e.Node.Tag);
            }
            else if (e.Node.Level == 2)
            {
                DisplayHostPanels(HostPanels.HOSTINSTANCE);
                DisplayHostInstanceDetails((HostInstance)e.Node.Tag);
            }
            else
            {
                DisplayHostPanels(HostPanels.HIDEALL);
                TrvHostSelectNode(e.Node.FirstNode);
            }
        }

        private void DisplayHosts()
        {
            ClearHostControls();
            foreach (KeyValuePair<string, Host> kHost in mHosts)
            {
                Host host = kHost.Value;
                TreeNode tn = new TreeNode();
                tn.Tag = host;
                tn.Text = host.Name;
                tn.Name = host.Name;
                tn.Checked = host.Checked;

                TreeNode tnHeaderHostInstances = new TreeNode();
                tnHeaderHostInstances.Text = Constants.FORM_HOST_INSTANCES;
                foreach (KeyValuePair<string, HostInstance> kHostInstance in host.HostInstances)
                {
                    HostInstance hi = kHostInstance.Value;
                    TreeNode tnHostInstance = new TreeNode();
                    tnHostInstance.Tag = hi;
                    tnHostInstance.Text = hi.UserName;
                    tnHeaderHostInstances.Nodes.Add(tnHostInstance);
                }
                if (tnHeaderHostInstances.Nodes.Count > 0)
                {
                    tn.Nodes.Add(tnHeaderHostInstances);
                }

                TreeNode tnHeaderApapters = new TreeNode();
                tnHeaderApapters.Text = Constants.FORM_ADAPTERS;

                tnHeaderApapters.Tag = host;
                tn.Nodes.Add(tnHeaderApapters);

                trvHost.Nodes.Add(tn);
            }
        }
        private void DisplayParties()
        {
            foreach (KeyValuePair<string, Party> kParty in mParties)
            {
                Party p = kParty.Value;
                TreeNode tn = new TreeNode();
                tn.Tag = p;
                tn.Text = p.Name;

                trvPart.Nodes.Add(tn);
            }
        }
        private void DisplayHostPanels(HostPanels hp)
        {
            switch (hp)
            {
                case HostPanels.APAPTERS:
                    if (pnlHostAdapters.Visible == false)
                    {
                        pnlHostAdapters.Visible = true;
                        pnlHostDetails.Visible = false;
                        pnlHostInstance.Visible = false;
                    }
                    break;
                case HostPanels.HOST:
                    if (pnlHostDetails.Visible == false)
                    {
                        pnlHostAdapters.Visible = false;
                        pnlHostDetails.Visible = true;
                        pnlHostInstance.Visible = false;
                    }
                    break;
                case HostPanels.HOSTINSTANCE:
                    if (pnlHostInstance.Visible == false)
                    {
                        pnlHostAdapters.Visible = false;
                        pnlHostDetails.Visible = false;
                        pnlHostInstance.Visible = true;
                    }
                    break;
                case HostPanels.HIDEALL:
                    pnlHostAdapters.Visible = false;
                    pnlHostDetails.Visible = false;
                    pnlHostInstance.Visible = false;
                    break;
            }

        }
        private void DisplayHostDetails(Host h)
        {
            txtHostName.Text = h.Name;
            cmbHostType.SelectedItem = h.Type;
            chbHostAllowHostTracking.Checked = h.AllowHostTracking;
            chbHostAuthenticationiTrusted.Checked = h.AuthenticationTrusted;
            chbHost32BitOnly.Checked = h.ThirtyTwoBitOnly;
            chbMakeDefaultHostGroup.Checked = h.DefualtHost;
            tbxHostWindowsGroup.Text = h.WindowsGroup;
        }
        private void DisplayHostInstanceDetails(HostInstance hi)
        {
            txtHostServer.Text = hi.ServerName;
            txtHostLogon.Text = hi.UserName;
            txtHostPassword.Text = hi.Password;
            chbHostDisableHost.Checked = hi.Disable;
        }
        private void DisplayHostApaters(Host h)
        {
            lbxHostReceiveAdapters.Items.Clear();
            foreach (KeyValuePair<string, Adapter> kReceiveAdapter in h.ReceiveAdapters)
            {
                Adapter ReceiveAdapter = kReceiveAdapter.Value;

                lbxHostReceiveAdapters.Items.Add(ReceiveAdapter.Name);
            }

            lbxHostReceiveAvailAdapters.Items.Clear();
            foreach (string adapter in this.mAllReceiveAdapters)
            {
                if (!lbxHostReceiveAdapters.Items.Contains(adapter))
                {
                    lbxHostReceiveAvailAdapters.Items.Add(adapter);
                }
            }

            lbxHostSendAdapters.Items.Clear();
            foreach (KeyValuePair<string, Adapter> kSendAdapter in h.SendAdapters)
            {
                Adapter SendAdapter = kSendAdapter.Value;
                lbxHostSendAdapters.Items.Add(SendAdapter.Name);
            }

            lbxHostSendAvailAdapters.Items.Clear();
            foreach (string adapter in this.mAllSendAdapters)
            {
                if (!lbxHostSendAdapters.Items.Contains(adapter))
                {
                    lbxHostSendAvailAdapters.Items.Add(adapter);
                }
            }
        }
        private Rectangle NodeBounds(TreeNode node)
        {
            // Set the return value to the normal node bounds.
            Rectangle bounds = node.Bounds;
            bounds.X = bounds.X - 14;

            return bounds;

        }
        private void trvHost_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Console.WriteLine("trvHost_DrawNode");
            if (e.Node.Level == 0)
            {
                e.DrawDefault = true;
            }
            else
            {
                Console.WriteLine("DrawNode: " + e.State.ToString());
                if ((e.State & TreeNodeStates.Selected) != 0)
                {
                    Rectangle bounds = e.Node.Bounds;
                    bounds.X = bounds.X - 14;
                    e.Graphics.FillRectangle(SystemBrushes.ActiveCaption, bounds);

                    Font nodeFont = e.Node.NodeFont;
                    if (nodeFont == null)
                    {
                        nodeFont = ((TreeView)sender).Font;
                    }

                    e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.ActiveCaptionText,
                        Rectangle.Inflate(NodeBounds(e.Node), 2, 0));



                    Rectangle boundsBack = e.Node.Bounds;
                    boundsBack.X = e.Node.Bounds.X + e.Node.Bounds.Width - 16;

                    e.Graphics.FillRectangle(SystemBrushes.Window, boundsBack);
                }
                else
                {
                    //Focus has been lost draw grey box
                    if (e.Node == mTnTrvHostDrawSelectNode)
                    {
                        Rectangle bounds = e.Node.Bounds;
                        bounds.X -= 15;
                        bounds.Y -= 1;
                        bounds.Height += 1;
                        e.Graphics.FillRectangle(SystemBrushes.Control, bounds);

                        Font nodeFont = e.Node.NodeFont;
                        if (nodeFont == null)
                        {
                            nodeFont = ((TreeView)sender).Font;
                        }

                        e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.WindowText,
                            Rectangle.Inflate(NodeBounds(e.Node), 2, 0));
                    }
                    else
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Window, NodeBounds(e.Node));

                        Font nodeFont = e.Node.NodeFont;
                        if (nodeFont == null)
                        {
                            nodeFont = ((TreeView)sender).Font;
                        }

                        e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.WindowText,
                            Rectangle.Inflate(NodeBounds(e.Node), 2, 0));
                    }
                }
            }
        }
        private void trvHost_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("trvHost_MouseDown");
            TreeNode clickedNode = trvHost.GetNodeAt(e.X, e.Y);

            if (clickedNode != null && NodeBounds(clickedNode).Contains(e.X, e.Y))
            {
                TrvHostSelectNode(clickedNode);
            }
        }
        private void moveListBoxItem(ListBox AddItemTo, ListBox RemoveItemTo)
        {
            if (RemoveItemTo.SelectedItem != null)
            {
                string AdapterName = RemoveItemTo.SelectedItem.ToString();
                RemoveItemTo.Items.Remove(RemoveItemTo.SelectedItem);

                AddItemTo.Items.Add(AdapterName);
                AddItemTo.SelectedItem = AdapterName;
            }
        }
        private void btnHostReceiveAdd_Click(object sender, EventArgs e)
        {
            moveListBoxItem(lbxHostReceiveAdapters, lbxHostReceiveAvailAdapters);
            SaveHostAdapter(lbxHostReceiveAdapters, Constants.TYPE_RECEIVE, true);
        }

        private void SaveHostAdapter(ListBox adapterList, string type, bool add)
        {
            if (adapterList.SelectedItem != null)
            {
                string adapterName = adapterList.SelectedItem.ToString();

                if (mTnTrvHostAfterSelectNode.Tag is Host)
                {
                    Host h = (Host)mTnTrvHostAfterSelectNode.Tag;
                    if (type == Constants.TYPE_RECEIVE)
                    {
                        SaveHostAdapter(h.ReceiveAdapters, adapterName, Constants.TYPE_RECEIVE, add);
                    }
                    else if (type == Constants.TYPE_SEND)
                    {
                        SaveHostAdapter(h.SendAdapters, adapterName, Constants.TYPE_RECEIVE, add);
                    }
                }
            }
        }
        private void SaveHostAdapter(Dictionary<string, Adapter> Adapters, string adapterName, string type, bool add )
        {
            bool containsKey = Adapters.ContainsKey(adapterName);
            if (add && !containsKey)
            {
                Adapter a = new Adapter();
                a.Name = adapterName;
                a.Type = type;
                Adapters.Add(adapterName, a);
            }
            else if (containsKey)
            {
                Adapters.Remove(adapterName);
            }
        }

        private void btnHostReceiveRemove_Click(object sender, EventArgs e)
        {
            moveListBoxItem(lbxHostReceiveAvailAdapters, lbxHostReceiveAdapters);
            SaveHostAdapter(lbxHostReceiveAdapters, Constants.TYPE_RECEIVE, false);
        }
        private void btnHostSendAdd_Click(object sender, EventArgs e)
        {
            moveListBoxItem(lbxHostSendAdapters, lbxHostSendAvailAdapters);
            SaveHostAdapter(lbxHostSendAdapters, Constants.TYPE_SEND, true);
        }
        private void btnHostSendRemove_Click(object sender, EventArgs e)
        {
            moveListBoxItem(lbxHostSendAvailAdapters, lbxHostSendAdapters);
            SaveHostAdapter(lbxHostSendAdapters, Constants.TYPE_SEND, false);
        }
        private void trvPart_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Party p = (Party)e.Node.Tag;
            tbxPartyName.Text = p.Name;
            tbxPartyCommonName.Text = p.sc.ShortName;
            tbxPartyThumbprint.Text = p.sc.ThumbPrint;

            dgvPartyAliases.DataSource = p.Aliases;
            dgvPartyAliases.AutoResizeColumns();

            dgvPartySendPorts.DataSource = p.SendPorts;
            dgvPartySendPorts.AutoResizeColumns();
        }
        private void button3_Click(object sender, EventArgs e)
        {
         //   Helper.AddNewParty();
        }
        private void ClearHostControls()
        {
            trvHost.Nodes.Clear();
            DisplayHostPanels(HostPanels.HIDEALL);
        }
        
        private void tsbBiztalkDownLoad_Click(object sender, EventArgs e)
        {
            switch (tbcBiztalk.SelectedTab.Name)
            {
                case "tbpHost":
                    BizTalkHostDownload();
                    break;

                case "tbpParty":
                    mParties = Helper.GetParties();
                    DisplayParties();
                    break;
            }
        }

        private void BizTalkHostDownload()
        {
            mHosts = Helper.GetBiztalkHosts();
            DisplayHosts();
        }
        private void tsbBiztalkUpLoad_Click(object sender, EventArgs e)
        {
            SaveDetails();
            switch (tbcBiztalk.SelectedTab.Name)
            {
                case "tbpHost":                    
                    BiztalkHostUpload();
                    break;

                case "tbpParty":
                    throw new Exception("The method or operation is not implemented.");
                    break;
            }
        }
        private void BiztalkHostUpload()
        {
            string passwordResult = NoMissingPasswords();
            if (passwordResult.Length == 0)
            {
                frmSave frmS = new frmSave(mHosts, frmSave.SaveType.Server);
                if (frmS.ShowDialog() == DialogResult.OK)
                {
                    BizTalkHostDownload();
                }
            }
            else
            {
                MessageBox.Show(passwordResult, "Credential Issues", MessageBoxButtons.OK, MessageBoxIcon.Warning);            
            }            
        }

        private string NoMissingPasswords()
        {
            string ErrorMessage = "";
            foreach (KeyValuePair<string, Host> kHost in mHosts)
            {
                Host host = kHost.Value;
                if (host.Checked)
                {
                    foreach(KeyValuePair<string, HostInstance> kHostInstances in host.HostInstances)
                    {
                        HostInstance hostInstance = kHostInstances.Value;
                        string passwordError;
                        if (!passwordValid(hostInstance.UserName, hostInstance.Password, out passwordError))
                        {
                            if(ErrorMessage.Length == 0)
                            {
                                ErrorMessage = "The following Host Instances have credential issues:" + Environment.NewLine;
                            }
                            ErrorMessage += String.Format("Host: {0}, Host Instance: {1} - {2}", host.Name, hostInstance.UserName, passwordError) + Environment.NewLine;
                        }
                    }
                }
            }
            return ErrorMessage;
        }

        //private bool BiztalkUploadNew(Host h)
        //{
        //    if (!PromptForPasswords(h))
        //    {
        //        return false;
        //    }

        //    Helper.createHost(h);
        //    return true;
        //}

        //private bool PromptForPasswords(Host h)
        //{
        //    if (h.SetHostInstancesAction)
        //    {
        //        foreach (KeyValuePair<string, HostInstance> kHostInstance in h.HostInstances)
        //        {
        //            HostInstance hostInstance = kHostInstance.Value;

        //            if (hostInstance.PasswordPrompt)
        //            {
        //                frmPasswordInput inputForm = new frmPasswordInput();
        //                inputForm.Username = hostInstance.UserName;
        //                inputForm.label1.Text = "Enter password for " + hostInstance.UserName;
        //                if (inputForm.ShowDialog() == DialogResult.OK)
        //                {
        //                    hostInstance.Password = inputForm.txtPassword.Text;
        //                    hostInstance.PasswordPrompt = false;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}

        private void tsbFileSave_Click(object sender, EventArgs e)
        {
            SaveDetails();
            switch (tbcBiztalk.SelectedTab.Name)
            {
                case "tbpHost":
                    SaveHostToFile();
                    break;

                case "tbpParty":
                    throw new Exception("The method or operation is not implemented.");
                    break;
            }
        }
        private void SaveHostToFile()
        {
            frmSave frmS = new frmSave(mHosts,  frmSave.SaveType.File);
            frmS.ShowDialog();
        }
        private void tsbFileLoad_Click(object sender, EventArgs e)
        {
            switch (tbcBiztalk.SelectedTab.Name)
            {
                case "tbpHost":
                    mHosts = LoadHostsFromFile();
                    DisplayHosts();
                    break;

                case "tbpParty":
                    mParties = Helper.GetParties();
                    DisplayParties();
                    break;
            }
        }
        private void trvHost_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ((Host)e.Node.Tag).Checked = e.Node.Checked;
        }
        private void txtHostName_Validating(object sender, CancelEventArgs e)
        {
            Console.WriteLine("txtHostName_Validating");
            if (mTnTrvHostAfterSelectNode != null && ((Host)mTnTrvHostAfterSelectNode.Tag).Name != ((TextBox)sender).Text)
            {
                if (mHosts.ContainsKey(((TextBox)sender).Text))
                {
                    MessageBox.Show("Host already exists with the same name.");
                    e.Cancel = true;
                    return;
                }
                if (MessageBox.Show(String.Format("Changing Host name will require {0} to be deleted and replaced with {1}. The Action of {2} will be set to delete. If you manually uncheck the host in the treeview nothing will happen and a new host will be created. Would you like to continue?", ((Host)mTnTrvHostAfterSelectNode.Tag).Name, ((TextBox)sender).Text, ((Host)mTnTrvHostAfterSelectNode.Tag).Name),
                                "Host Name Changing", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    ((TextBox)sender).Text = ((Host)mTnTrvHostAfterSelectNode.Tag).Name;
                    e.Cancel = true;
                }
                else
                {
                    string name = ((Host)mTnTrvHostAfterSelectNode.Tag).Name;
                    SaveHostDetails((Host)mTnTrvHostAfterSelectNode.Tag);
                    ((Host)mTnTrvHostAfterSelectNode.Tag).Name = name;

                    Host host = mHosts[name];
                    Host newHost = (Host)host.Clone();
                    host.Checked = true;
                    host.Status = HostStatus.Delete;                    
                    
                    newHost.Name = ((TextBox)sender).Text;
                    newHost.Status = HostStatus.New;
                    newHost.Origination = OriginationStatus.New;

                    mHosts.Add(newHost.Name, newHost);

                    mTnTrvHostAfterSelectNode = null;

                    DisplayHosts();

                    TrvHostSelectNode(trvHost.Nodes[newHost.Name]);
                    trvHost.Focus();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Helper.HostRemove(mHosts["test"]);
        }

        //private void txtHostPassword_Validated(object sender, EventArgs e)
        //{
        //    if (((TextBox)sender).Text.Length > 0)
        //    {
        //        ((HostInstance)mTnTrvHostAfterSelectNode.Tag).PasswordPrompt = false;
        //    }
        //    else
        //    {
        //        ((HostInstance)mTnTrvHostAfterSelectNode.Tag).PasswordPrompt = true;
        //    }
        //}

        private bool passwordValid(string logonName, string password, out string ErrorMessage)
        {
            string[] aDomainUsername = logonName.Split(new char[] { '\\' });
            string domain = "";
            string username = "";
            ErrorMessage = "";
            if (aDomainUsername.Length > 2)
            {
                ErrorMessage = "Invalid Logon name";
                return false;
            }
            else if (aDomainUsername.Length == 1)
            {
                username = aDomainUsername[0];
            }
            else if (aDomainUsername.Length == 2)
            {
                domain = aDomainUsername[0];
                username = aDomainUsername[1];
            }

            ImpersonateUser impUser = new ImpersonateUser();

            if (impUser.Impersonate(domain, username, password))
            {   
                impUser.Undo();
                return true;
            }
            else
            {
                ErrorMessage = impUser.mszErrorMessage;
            }
            return false;
        }
        private void btnHostPasswordCheck_Click(object sender, EventArgs e)
        {
            string ErrorMessage;
            if(passwordValid(txtHostLogon.Text, txtHostPassword.Text, out ErrorMessage))
            {
                MessageBox.Show("Logon and password combination are valid.", "Correct", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (tbcBiztalk.SelectedTab.Name)
            {
                case "tbpHost":
                    TreeNode rootNode = GetRootParent(mTnTrvHostAfterSelectNode);
                    rootNode.Checked = true;

                    Host h = (Host)rootNode.Tag;
                    h.Checked = true;
                    h.Status = HostStatus.Delete;
                    setStatusStrip(h.Status);
                    break;

                case "tbpParty":
                    throw new Exception("The method or operation is not implemented.");
                    break;
            }
        }
        private void setStatusStrip(HostStatus status)
        {
            tssStatus.Text = "Status: " + status.ToString();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (tbcBiztalk.SelectedTab.Name)
            {
                case "tbpHost":
                    throw new Exception("The method or operation is not implemented.");
                    break;

                case "tbpParty":
                    throw new Exception("The method or operation is not implemented.");
                    break;
            }
        }
    }
}