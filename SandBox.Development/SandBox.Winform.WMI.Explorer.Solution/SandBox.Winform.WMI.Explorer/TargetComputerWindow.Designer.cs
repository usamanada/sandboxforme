namespace SandBox.Winform.WMI.Explorer
{
    partial class TargetComputerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label remoteIntro;
        private System.Windows.Forms.Label computerNameLabel;
        private System.Windows.Forms.TextBox remoteComputerNameBox;
        private System.Windows.Forms.TextBox remoteComputerDomainBox;
        private System.Windows.Forms.Label computerDomainLabel;
        private System.Windows.Forms.TextBox arrayRemoteComputersBox;
        private System.Windows.Forms.Label arrayRemoteInfoLabel;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.okButton = new System.Windows.Forms.Button();
            this.remoteIntro = new System.Windows.Forms.Label();
            this.computerNameLabel = new System.Windows.Forms.Label();
            this.remoteComputerNameBox = new System.Windows.Forms.TextBox();
            this.remoteComputerDomainBox = new System.Windows.Forms.TextBox();
            this.computerDomainLabel = new System.Windows.Forms.Label();
            this.arrayRemoteComputersBox = new System.Windows.Forms.TextBox();
            this.arrayRemoteInfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(104, 224);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(136, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // remoteIntro
            // 
            this.remoteIntro.Location = new System.Drawing.Point(16, 24);
            this.remoteIntro.Name = "remoteIntro";
            this.remoteIntro.Size = new System.Drawing.Size(320, 72);
            this.remoteIntro.TabIndex = 1;
            this.remoteIntro.Text = "You have selected to perform a task using WMI on a remote computer. Fill in the i" +
                "nformation below about the remote computer. This information will be used in the" +
                " code created by the WMI Code Creator.";
            // 
            // computerNameLabel
            // 
            this.computerNameLabel.Location = new System.Drawing.Point(24, 104);
            this.computerNameLabel.Name = "computerNameLabel";
            this.computerNameLabel.Size = new System.Drawing.Size(300, 16);
            this.computerNameLabel.TabIndex = 2;
            this.computerNameLabel.Text = "Full Name (or IP Address) of the Remote Computer:";
            // 
            // remoteComputerNameBox
            // 
            this.remoteComputerNameBox.Location = new System.Drawing.Point(24, 120);
            this.remoteComputerNameBox.Name = "remoteComputerNameBox";
            this.remoteComputerNameBox.Size = new System.Drawing.Size(288, 20);
            this.remoteComputerNameBox.TabIndex = 3;
            this.remoteComputerNameBox.Text = "FullComputerName";
            this.remoteComputerNameBox.TextChanged += new System.EventHandler(this.remoteComputerNameBox_TextChanged);
            // 
            // remoteComputerDomainBox
            // 
            this.remoteComputerDomainBox.Location = new System.Drawing.Point(24, 168);
            this.remoteComputerDomainBox.Name = "remoteComputerDomainBox";
            this.remoteComputerDomainBox.Size = new System.Drawing.Size(288, 20);
            this.remoteComputerDomainBox.TabIndex = 5;
            this.remoteComputerDomainBox.Text = "DOMAIN";
            this.remoteComputerDomainBox.TextChanged += new System.EventHandler(this.remoteComputerDomainBox_TextChanged);
            // 
            // computerDomainLabel
            // 
            this.computerDomainLabel.Location = new System.Drawing.Point(24, 152);
            this.computerDomainLabel.Name = "computerDomainLabel";
            this.computerDomainLabel.Size = new System.Drawing.Size(240, 16);
            this.computerDomainLabel.TabIndex = 4;
            this.computerDomainLabel.Text = "Remote Computer Domain:";
            // 
            // arrayRemoteComputersBox
            // 
            this.arrayRemoteComputersBox.Location = new System.Drawing.Point(24, 128);
            this.arrayRemoteComputersBox.Multiline = true;
            this.arrayRemoteComputersBox.Name = "arrayRemoteComputersBox";
            this.arrayRemoteComputersBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.arrayRemoteComputersBox.Size = new System.Drawing.Size(288, 80);
            this.arrayRemoteComputersBox.TabIndex = 6;
            this.arrayRemoteComputersBox.Text = "";
            this.arrayRemoteComputersBox.Visible = false;
            this.arrayRemoteComputersBox.TextChanged += new System.EventHandler(this.arrayRemoteComputersBox_TextChanged);
            // 
            // arrayRemoteInfoLabel
            // 
            this.arrayRemoteInfoLabel.Location = new System.Drawing.Point(16, 96);
            this.arrayRemoteInfoLabel.Name = "arrayRemoteInfoLabel";
            this.arrayRemoteInfoLabel.Size = new System.Drawing.Size(320, 32);
            this.arrayRemoteInfoLabel.TabIndex = 7;
            this.arrayRemoteInfoLabel.Visible = false;
            // 
            // TargetComputerWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(344, 266);
            this.ControlBox = false;
            this.Controls.Add(this.arrayRemoteInfoLabel);
            this.Controls.Add(this.arrayRemoteComputersBox);
            this.Controls.Add(this.remoteComputerDomainBox);
            this.Controls.Add(this.computerDomainLabel);
            this.Controls.Add(this.remoteComputerNameBox);
            this.Controls.Add(this.computerNameLabel);
            this.Controls.Add(this.remoteIntro);
            this.Controls.Add(this.okButton);
            this.Name = "TargetComputerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remote Computer Information";
            this.ResumeLayout(false);
        }

        #endregion
    }
}