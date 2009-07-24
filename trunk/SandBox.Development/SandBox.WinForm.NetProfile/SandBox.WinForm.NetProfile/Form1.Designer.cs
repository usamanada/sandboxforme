namespace SandBox.WinForm.NetProfile
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvSettings = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.cbxNetworkInterface = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbpIP = new UIToolbox.RadioButtonPanel();
            this.rgbIPStatic = new UIToolbox.RadioGroupBox();
            this.lblDefaultGateway = new System.Windows.Forms.Label();
            this.iacDefaultGateway = new IPAddressControlLib.IPAddressControl();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.iacIPAddress = new IPAddressControlLib.IPAddressControl();
            this.lblSubnetMask = new System.Windows.Forms.Label();
            this.iacSubnetMask = new IPAddressControlLib.IPAddressControl();
            this.rbnIPDynamic = new System.Windows.Forms.RadioButton();
            this.rbpDNS = new UIToolbox.RadioButtonPanel();
            this.rgbDnsStatic = new UIToolbox.RadioGroupBox();
            this.lblPreferredDnsServer = new System.Windows.Forms.Label();
            this.iacPreferredDnsServer = new IPAddressControlLib.IPAddressControl();
            this.lblAlternativeDnsServer = new System.Windows.Forms.Label();
            this.iacAlternativeDnsServer = new IPAddressControlLib.IPAddressControl();
            this.rbnDNSDynamic = new System.Windows.Forms.RadioButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.rbpIP.SuspendLayout();
            this.rgbIPStatic.SuspendLayout();
            this.rbpDNS.SuspendLayout();
            this.rgbDnsStatic.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvSettings);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.cbxNetworkInterface);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(498, 518);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvSettings
            // 
            this.tvSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSettings.Location = new System.Drawing.Point(0, 24);
            this.tvSettings.Name = "tvSettings";
            this.tvSettings.Size = new System.Drawing.Size(165, 494);
            this.tvSettings.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(165, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.activateToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(45, 20);
            this.toolStripMenuItem1.Text = "Menu";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // activateToolStripMenuItem
            // 
            this.activateToolStripMenuItem.Name = "activateToolStripMenuItem";
            this.activateToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.activateToolStripMenuItem.Text = "Activate";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(138, 429);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbxNetworkInterface
            // 
            this.cbxNetworkInterface.FormattingEnabled = true;
            this.cbxNetworkInterface.Location = new System.Drawing.Point(7, 15);
            this.cbxNetworkInterface.Name = "cbxNetworkInterface";
            this.cbxNetworkInterface.Size = new System.Drawing.Size(301, 21);
            this.cbxNetworkInterface.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbpIP);
            this.groupBox1.Controls.Add(this.rbpDNS);
            this.groupBox1.Location = new System.Drawing.Point(3, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 320);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP Settings";
            // 
            // rbpIP
            // 
            this.rbpIP.Controls.Add(this.rgbIPStatic);
            this.rbpIP.Controls.Add(this.rbnIPDynamic);
            this.rbpIP.Location = new System.Drawing.Point(1, 19);
            this.rbpIP.Name = "rbpIP";
            this.rbpIP.Size = new System.Drawing.Size(298, 160);
            this.rbpIP.TabIndex = 0;
            // 
            // rgbIPStatic
            // 
            this.rgbIPStatic.Controls.Add(this.lblDefaultGateway);
            this.rgbIPStatic.Controls.Add(this.iacDefaultGateway);
            this.rgbIPStatic.Controls.Add(this.lblIPAddress);
            this.rgbIPStatic.Controls.Add(this.iacIPAddress);
            this.rgbIPStatic.Controls.Add(this.lblSubnetMask);
            this.rgbIPStatic.Controls.Add(this.iacSubnetMask);
            this.rgbIPStatic.Location = new System.Drawing.Point(3, 26);
            this.rgbIPStatic.Name = "rgbIPStatic";
            this.rgbIPStatic.Size = new System.Drawing.Size(289, 128);
            this.rgbIPStatic.TabIndex = 1;
            this.rgbIPStatic.TabStop = false;
            this.rgbIPStatic.Text = "Use the following IP address:";
            // 
            // lblDefaultGateway
            // 
            this.lblDefaultGateway.AutoSize = true;
            this.lblDefaultGateway.Location = new System.Drawing.Point(4, 85);
            this.lblDefaultGateway.Name = "lblDefaultGateway";
            this.lblDefaultGateway.Size = new System.Drawing.Size(87, 13);
            this.lblDefaultGateway.TabIndex = 11;
            this.lblDefaultGateway.Text = "Default gateway:";
            // 
            // iacDefaultGateway
            // 
            this.iacDefaultGateway.AllowInternalTab = false;
            this.iacDefaultGateway.AutoHeight = true;
            this.iacDefaultGateway.BackColor = System.Drawing.SystemColors.Window;
            this.iacDefaultGateway.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iacDefaultGateway.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.iacDefaultGateway.Location = new System.Drawing.Point(152, 82);
            this.iacDefaultGateway.MinimumSize = new System.Drawing.Size(87, 20);
            this.iacDefaultGateway.Name = "iacDefaultGateway";
            this.iacDefaultGateway.ReadOnly = false;
            this.iacDefaultGateway.Size = new System.Drawing.Size(131, 20);
            this.iacDefaultGateway.TabIndex = 2;
            this.iacDefaultGateway.Text = "...";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(4, 33);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(60, 13);
            this.lblIPAddress.TabIndex = 9;
            this.lblIPAddress.Text = "IP address:";
            // 
            // iacIPAddress
            // 
            this.iacIPAddress.AllowInternalTab = false;
            this.iacIPAddress.AutoHeight = true;
            this.iacIPAddress.BackColor = System.Drawing.SystemColors.Window;
            this.iacIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iacIPAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.iacIPAddress.Location = new System.Drawing.Point(152, 30);
            this.iacIPAddress.MinimumSize = new System.Drawing.Size(87, 20);
            this.iacIPAddress.Name = "iacIPAddress";
            this.iacIPAddress.ReadOnly = false;
            this.iacIPAddress.Size = new System.Drawing.Size(131, 20);
            this.iacIPAddress.TabIndex = 0;
            this.iacIPAddress.Text = "...";
            // 
            // lblSubnetMask
            // 
            this.lblSubnetMask.AutoSize = true;
            this.lblSubnetMask.Location = new System.Drawing.Point(4, 59);
            this.lblSubnetMask.Name = "lblSubnetMask";
            this.lblSubnetMask.Size = new System.Drawing.Size(72, 13);
            this.lblSubnetMask.TabIndex = 7;
            this.lblSubnetMask.Text = "Subnet mask:";
            // 
            // iacSubnetMask
            // 
            this.iacSubnetMask.AllowInternalTab = false;
            this.iacSubnetMask.AutoHeight = true;
            this.iacSubnetMask.BackColor = System.Drawing.SystemColors.Window;
            this.iacSubnetMask.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iacSubnetMask.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.iacSubnetMask.Location = new System.Drawing.Point(152, 56);
            this.iacSubnetMask.MinimumSize = new System.Drawing.Size(87, 20);
            this.iacSubnetMask.Name = "iacSubnetMask";
            this.iacSubnetMask.ReadOnly = false;
            this.iacSubnetMask.Size = new System.Drawing.Size(131, 20);
            this.iacSubnetMask.TabIndex = 1;
            this.iacSubnetMask.Text = "...";
            // 
            // rbnIPDynamic
            // 
            this.rbnIPDynamic.AutoSize = true;
            this.rbnIPDynamic.Location = new System.Drawing.Point(13, 3);
            this.rbnIPDynamic.Name = "rbnIPDynamic";
            this.rbnIPDynamic.Size = new System.Drawing.Size(188, 17);
            this.rbnIPDynamic.TabIndex = 0;
            this.rbnIPDynamic.TabStop = true;
            this.rbnIPDynamic.Text = "Obtain an IP address automatically";
            this.rbnIPDynamic.UseVisualStyleBackColor = true;
            // 
            // rbpDNS
            // 
            this.rbpDNS.Controls.Add(this.rgbDnsStatic);
            this.rbpDNS.Controls.Add(this.rbnDNSDynamic);
            this.rbpDNS.Location = new System.Drawing.Point(1, 179);
            this.rbpDNS.Name = "rbpDNS";
            this.rbpDNS.Size = new System.Drawing.Size(298, 133);
            this.rbpDNS.TabIndex = 1;
            // 
            // rgbDnsStatic
            // 
            this.rgbDnsStatic.Controls.Add(this.lblPreferredDnsServer);
            this.rgbDnsStatic.Controls.Add(this.iacPreferredDnsServer);
            this.rgbDnsStatic.Controls.Add(this.lblAlternativeDnsServer);
            this.rgbDnsStatic.Controls.Add(this.iacAlternativeDnsServer);
            this.rgbDnsStatic.Location = new System.Drawing.Point(3, 26);
            this.rgbDnsStatic.Name = "rgbDnsStatic";
            this.rgbDnsStatic.Size = new System.Drawing.Size(289, 100);
            this.rgbDnsStatic.TabIndex = 1;
            this.rgbDnsStatic.TabStop = false;
            this.rgbDnsStatic.Text = "Use the following DNS server addresses:";
            // 
            // lblPreferredDnsServer
            // 
            this.lblPreferredDnsServer.AutoSize = true;
            this.lblPreferredDnsServer.Location = new System.Drawing.Point(4, 33);
            this.lblPreferredDnsServer.Name = "lblPreferredDnsServer";
            this.lblPreferredDnsServer.Size = new System.Drawing.Size(111, 13);
            this.lblPreferredDnsServer.TabIndex = 9;
            this.lblPreferredDnsServer.Text = "Preferred DNS server:";
            // 
            // iacPreferredDnsServer
            // 
            this.iacPreferredDnsServer.AllowInternalTab = false;
            this.iacPreferredDnsServer.AutoHeight = true;
            this.iacPreferredDnsServer.BackColor = System.Drawing.SystemColors.Window;
            this.iacPreferredDnsServer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iacPreferredDnsServer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.iacPreferredDnsServer.Location = new System.Drawing.Point(152, 30);
            this.iacPreferredDnsServer.MinimumSize = new System.Drawing.Size(87, 20);
            this.iacPreferredDnsServer.Name = "iacPreferredDnsServer";
            this.iacPreferredDnsServer.ReadOnly = false;
            this.iacPreferredDnsServer.Size = new System.Drawing.Size(131, 20);
            this.iacPreferredDnsServer.TabIndex = 0;
            this.iacPreferredDnsServer.Text = "...";
            // 
            // lblAlternativeDnsServer
            // 
            this.lblAlternativeDnsServer.AutoSize = true;
            this.lblAlternativeDnsServer.Location = new System.Drawing.Point(4, 59);
            this.lblAlternativeDnsServer.Name = "lblAlternativeDnsServer";
            this.lblAlternativeDnsServer.Size = new System.Drawing.Size(118, 13);
            this.lblAlternativeDnsServer.TabIndex = 7;
            this.lblAlternativeDnsServer.Text = "Alternative DNS server:";
            // 
            // iacAlternativeDnsServer
            // 
            this.iacAlternativeDnsServer.AllowInternalTab = false;
            this.iacAlternativeDnsServer.AutoHeight = true;
            this.iacAlternativeDnsServer.BackColor = System.Drawing.SystemColors.Window;
            this.iacAlternativeDnsServer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.iacAlternativeDnsServer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.iacAlternativeDnsServer.Location = new System.Drawing.Point(152, 56);
            this.iacAlternativeDnsServer.MinimumSize = new System.Drawing.Size(87, 20);
            this.iacAlternativeDnsServer.Name = "iacAlternativeDnsServer";
            this.iacAlternativeDnsServer.ReadOnly = false;
            this.iacAlternativeDnsServer.Size = new System.Drawing.Size(131, 20);
            this.iacAlternativeDnsServer.TabIndex = 1;
            this.iacAlternativeDnsServer.Text = "...";
            // 
            // rbnDNSDynamic
            // 
            this.rbnDNSDynamic.AutoSize = true;
            this.rbnDNSDynamic.Location = new System.Drawing.Point(13, 3);
            this.rbnDNSDynamic.Name = "rbnDNSDynamic";
            this.rbnDNSDynamic.Size = new System.Drawing.Size(218, 17);
            this.rbnDNSDynamic.TabIndex = 0;
            this.rbnDNSDynamic.TabStop = true;
            this.rbnDNSDynamic.Text = "Obtain DNS server address automatically";
            this.rbnDNSDynamic.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 518);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.rbpIP.ResumeLayout(false);
            this.rbpIP.PerformLayout();
            this.rgbIPStatic.ResumeLayout(false);
            this.rgbIPStatic.PerformLayout();
            this.rbpDNS.ResumeLayout(false);
            this.rbpDNS.PerformLayout();
            this.rgbDnsStatic.ResumeLayout(false);
            this.rgbDnsStatic.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbnDNSDynamic;
        private UIToolbox.RadioButtonPanel rbpDNS;
        private UIToolbox.RadioGroupBox rgbDnsStatic;
        private System.Windows.Forms.Label lblPreferredDnsServer;
        private IPAddressControlLib.IPAddressControl iacPreferredDnsServer;
        private System.Windows.Forms.Label lblAlternativeDnsServer;
        private IPAddressControlLib.IPAddressControl iacAlternativeDnsServer;
        private UIToolbox.RadioButtonPanel rbpIP;
        private UIToolbox.RadioGroupBox rgbIPStatic;
        private System.Windows.Forms.Label lblDefaultGateway;
        private IPAddressControlLib.IPAddressControl iacDefaultGateway;
        private System.Windows.Forms.Label lblIPAddress;
        private IPAddressControlLib.IPAddressControl iacIPAddress;
        private System.Windows.Forms.Label lblSubnetMask;
        private IPAddressControlLib.IPAddressControl iacSubnetMask;
        private System.Windows.Forms.RadioButton rbnIPDynamic;
        private System.Windows.Forms.TreeView tvSettings;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activateToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxNetworkInterface;
        private System.Windows.Forms.Button button2;
    }
}

