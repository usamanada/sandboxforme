using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SandBox.Winform.WMI.Explorer
{
    //---------------------------------------------------------------------------------------
    // The TargetComputerWindow class creates the windows form used
    // to enter in the target computer information used in the WMICodeCreator.
    // The TargetComputerWindow class takes in information (name and domain)
    // about a remote computer, or the name of a list of remote computers in the same domain.
    //---------------------------------------------------------------------------------------
    public partial class TargetComputerWindow : Form
    {
        private WMICodeCreator controlWindow;
        public TargetComputerWindow()
        {
            InitializeComponent();
        }
        public TargetComputerWindow(WMICodeCreator form)
        {
            this.controlWindow = form;

            InitializeComponent();
        }
        //-------------------------------------------------------------------------
        // Handles the event when the user types in the name of a remote computer.
        // 
        //-------------------------------------------------------------------------
        private void remoteComputerNameBox_TextChanged(object sender, System.EventArgs e)
        {
            this.controlWindow.GenerateEventCode();
            this.controlWindow.GenerateQueryCode();
            this.controlWindow.GenerateMethodCode();
        }

        //-------------------------------------------------------------------------
        // Handles the event when the user types in the domain of a remote computer.
        // 
        //-------------------------------------------------------------------------
        private void remoteComputerDomainBox_TextChanged(object sender, System.EventArgs e)
        {
            this.controlWindow.GenerateEventCode();
            this.controlWindow.GenerateQueryCode();
            this.controlWindow.GenerateMethodCode();
        }

        //-------------------------------------------------------------------------
        // Handles the event when the user clicks the OK button on the form.
        // 
        //-------------------------------------------------------------------------
        private void okButton_Click(object sender, System.EventArgs e)
        {
            this.Visible = false;

            this.controlWindow.GenerateEventCode();
            this.controlWindow.GenerateQueryCode();
            this.controlWindow.GenerateMethodCode();
        }

        //-------------------------------------------------------------------------
        // Handles the event when the user types in the names for a
        // group of remote computers.
        //-------------------------------------------------------------------------
        private void arrayRemoteComputersBox_TextChanged(object sender, System.EventArgs e)
        {
            this.controlWindow.GenerateEventCode();
            this.controlWindow.GenerateQueryCode();
            this.controlWindow.GenerateMethodCode();
        }

        //-------------------------------------------------------------------------
        // Sets the window up to allow the user to type in information
        // for a single remote computer.
        //-------------------------------------------------------------------------
        public void SetForRemoteComputerInfo()
        {
            this.remoteIntro.Text = "You have selected to perform a task using WMI on a remote computer. Fill in the i" +
                "nformation below about the remote computer. This information will be used in the" +
                " code created by the WMI Code Creator.";
            this.remoteIntro.Visible = true;
            this.computerDomainLabel.Visible = true;
            this.computerNameLabel.Visible = true;
            this.remoteComputerDomainBox.Visible = true;
            this.remoteComputerNameBox.Visible = true;
            this.arrayRemoteInfoLabel.Visible = false;
            this.arrayRemoteComputersBox.Visible = false;
        }

        //-------------------------------------------------------------------------
        // Sets the window up to allow the user to type in information
        // for a group of remote computers.
        //-------------------------------------------------------------------------
        public void SetForGroupComputerInfo()
        {
            this.remoteIntro.Text = "You have selected to perform a task using WMI on a group of remote computers. " +
                "Your credentials (user name, password, and domain) will be used to connect to each computer. Make sure you are an Administrator on each computer.";
            this.remoteIntro.Visible = true;
            this.computerDomainLabel.Visible = false;
            this.computerNameLabel.Visible = false;
            this.remoteComputerDomainBox.Visible = false;
            this.remoteComputerNameBox.Visible = false;
            this.arrayRemoteInfoLabel.Visible = true;
            this.arrayRemoteInfoLabel.Text = "List one computer name per line with no blank lines between computer names.";
            this.arrayRemoteComputersBox.Visible = true;
        }

        //-------------------------------------------------------------------------
        // Gets the list of the group of remote computers.
        // 
        //-------------------------------------------------------------------------
        public string GetArrayOfComputers()
        {
            return this.arrayRemoteComputersBox.Text;
        }

        //-------------------------------------------------------------------------
        // Gets the name for a single remote computer.
        //
        //-------------------------------------------------------------------------
        public string GetRemoteComputerName()
        {
            return this.remoteComputerNameBox.Text;
        }

        //-------------------------------------------------------------------------
        // Gets the domain for a single remote computer.
        // 
        //-------------------------------------------------------------------------
        public string GetRemoteComputerDomain()
        {
            return this.remoteComputerDomainBox.Text;
        }
    }
}