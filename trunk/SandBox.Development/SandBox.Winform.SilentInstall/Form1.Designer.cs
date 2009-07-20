namespace SandBox.Winform.SilentInstall
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
            this.components = new System.ComponentModel.Container();
            this.lbxInstall = new System.Windows.Forms.ListBox();
            this.cbxEnvironments = new System.Windows.Forms.ComboBox();
            this.btnInstall = new System.Windows.Forms.Button();
            this.lbxAvailable = new System.Windows.Forms.ListBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpInstall = new System.Windows.Forms.TabPage();
            this.gbxAutoLogin = new System.Windows.Forms.GroupBox();
            this.lblInvalidUserCredentials = new System.Windows.Forms.Label();
            this.lblValidCredentials = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbpProgress = new System.Windows.Forms.TabPage();
            this.btnProgress = new System.Windows.Forms.Button();
            this.dgvProgress = new System.Windows.Forms.DataGridView();
            this.tbpAdmin = new System.Windows.Forms.TabPage();
            this.btnCopyFiles = new System.Windows.Forms.Button();
            this.btnIncreamentOrder = new System.Windows.Forms.Button();
            this.btnReadWorkAutoLogin = new System.Windows.Forms.Button();
            this.btnCleanAutoLogins = new System.Windows.Forms.Button();
            this.btnCopyContineBat = new System.Windows.Forms.Button();
            this.btnCleanContineBat = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.processWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tbpInstall.SuspendLayout();
            this.gbxAutoLogin.SuspendLayout();
            this.tbpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgress)).BeginInit();
            this.tbpAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxInstall
            // 
            this.lbxInstall.FormattingEnabled = true;
            this.lbxInstall.Location = new System.Drawing.Point(78, 67);
            this.lbxInstall.Name = "lbxInstall";
            this.lbxInstall.Size = new System.Drawing.Size(310, 264);
            this.lbxInstall.TabIndex = 0;
            // 
            // cbxEnvironments
            // 
            this.cbxEnvironments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEnvironments.FormattingEnabled = true;
            this.cbxEnvironments.Location = new System.Drawing.Point(78, 18);
            this.cbxEnvironments.Name = "cbxEnvironments";
            this.cbxEnvironments.Size = new System.Drawing.Size(310, 21);
            this.cbxEnvironments.TabIndex = 2;
            this.cbxEnvironments.SelectedIndexChanged += new System.EventHandler(this.cbxEnvironments_SelectedIndexChanged);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(384, 542);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 3;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // lbxAvailable
            // 
            this.lbxAvailable.FormattingEnabled = true;
            this.lbxAvailable.Location = new System.Drawing.Point(450, 67);
            this.lbxAvailable.Name = "lbxAvailable";
            this.lbxAvailable.Size = new System.Drawing.Size(309, 264);
            this.lbxAvailable.TabIndex = 4;
            // 
            // btnDown
            // 
            this.btnDown.Image = global::SandBox.Winform.SilentInstall.Properties.Resources.DownGreen;
            this.btnDown.Location = new System.Drawing.Point(22, 123);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(50, 50);
            this.btnDown.TabIndex = 8;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::SandBox.Winform.SilentInstall.Properties.Resources.UpGreen;
            this.btnUp.Location = new System.Drawing.Point(22, 67);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(50, 50);
            this.btnUp.TabIndex = 7;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::SandBox.Winform.SilentInstall.Properties.Resources.LeftGreen;
            this.btnLeft.Location = new System.Drawing.Point(394, 182);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(50, 50);
            this.btnLeft.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnLeft, "Add");
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Image = global::SandBox.Winform.SilentInstall.Properties.Resources.Right3Green;
            this.btnRight.Location = new System.Drawing.Point(394, 126);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(50, 50);
            this.btnRight.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnRight, "Remove");
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpInstall);
            this.tabControl1.Controls.Add(this.tbpProgress);
            this.tabControl1.Controls.Add(this.tbpAdmin);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(883, 631);
            this.tabControl1.TabIndex = 9;
            // 
            // tbpInstall
            // 
            this.tbpInstall.Controls.Add(this.gbxAutoLogin);
            this.tbpInstall.Controls.Add(this.label2);
            this.tbpInstall.Controls.Add(this.label1);
            this.tbpInstall.Controls.Add(this.btnUp);
            this.tbpInstall.Controls.Add(this.btnDown);
            this.tbpInstall.Controls.Add(this.lbxInstall);
            this.tbpInstall.Controls.Add(this.cbxEnvironments);
            this.tbpInstall.Controls.Add(this.btnLeft);
            this.tbpInstall.Controls.Add(this.btnInstall);
            this.tbpInstall.Controls.Add(this.btnRight);
            this.tbpInstall.Controls.Add(this.lbxAvailable);
            this.tbpInstall.Location = new System.Drawing.Point(4, 22);
            this.tbpInstall.Name = "tbpInstall";
            this.tbpInstall.Padding = new System.Windows.Forms.Padding(3);
            this.tbpInstall.Size = new System.Drawing.Size(875, 605);
            this.tbpInstall.TabIndex = 0;
            this.tbpInstall.Text = "Install";
            this.tbpInstall.UseVisualStyleBackColor = true;
            // 
            // gbxAutoLogin
            // 
            this.gbxAutoLogin.Controls.Add(this.lblInvalidUserCredentials);
            this.gbxAutoLogin.Controls.Add(this.lblValidCredentials);
            this.gbxAutoLogin.Controls.Add(this.btnCheck);
            this.gbxAutoLogin.Controls.Add(this.label6);
            this.gbxAutoLogin.Controls.Add(this.txtConfirmPassword);
            this.gbxAutoLogin.Controls.Add(this.label5);
            this.gbxAutoLogin.Controls.Add(this.label4);
            this.gbxAutoLogin.Controls.Add(this.label3);
            this.gbxAutoLogin.Controls.Add(this.txtPassword);
            this.gbxAutoLogin.Controls.Add(this.txtDomain);
            this.gbxAutoLogin.Controls.Add(this.txtUserName);
            this.gbxAutoLogin.Location = new System.Drawing.Point(81, 366);
            this.gbxAutoLogin.Name = "gbxAutoLogin";
            this.gbxAutoLogin.Size = new System.Drawing.Size(388, 157);
            this.gbxAutoLogin.TabIndex = 11;
            this.gbxAutoLogin.TabStop = false;
            this.gbxAutoLogin.Text = "Auto Login";
            // 
            // lblInvalidUserCredentials
            // 
            this.lblInvalidUserCredentials.AutoSize = true;
            this.lblInvalidUserCredentials.ForeColor = System.Drawing.Color.Red;
            this.lblInvalidUserCredentials.Location = new System.Drawing.Point(92, 128);
            this.lblInvalidUserCredentials.Name = "lblInvalidUserCredentials";
            this.lblInvalidUserCredentials.Size = new System.Drawing.Size(200, 13);
            this.lblInvalidUserCredentials.TabIndex = 10;
            this.lblInvalidUserCredentials.Text = "InValid Username and Password Entered";
            this.lblInvalidUserCredentials.Visible = false;
            // 
            // lblValidCredentials
            // 
            this.lblValidCredentials.AutoSize = true;
            this.lblValidCredentials.Location = new System.Drawing.Point(101, 128);
            this.lblValidCredentials.Name = "lblValidCredentials";
            this.lblValidCredentials.Size = new System.Drawing.Size(191, 13);
            this.lblValidCredentials.TabIndex = 9;
            this.lblValidCredentials.Text = "Valid Username and Password Entered";
            this.lblValidCredentials.Visible = false;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(303, 128);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 8;
            this.btnCheck.Text = "check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Confirm Password";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(101, 100);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(139, 20);
            this.txtConfirmPassword.TabIndex = 6;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Domain";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(101, 73);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(139, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(101, 46);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(139, 20);
            this.txtDomain.TabIndex = 1;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(101, 19);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(139, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(450, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Availabe Products";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Install Order of Products";
            // 
            // tbpProgress
            // 
            this.tbpProgress.Controls.Add(this.rtbLog);
            this.tbpProgress.Controls.Add(this.btnProgress);
            this.tbpProgress.Controls.Add(this.dgvProgress);
            this.tbpProgress.Location = new System.Drawing.Point(4, 22);
            this.tbpProgress.Name = "tbpProgress";
            this.tbpProgress.Padding = new System.Windows.Forms.Padding(3);
            this.tbpProgress.Size = new System.Drawing.Size(875, 605);
            this.tbpProgress.TabIndex = 1;
            this.tbpProgress.Text = "Progress";
            this.tbpProgress.UseVisualStyleBackColor = true;
            // 
            // btnProgress
            // 
            this.btnProgress.Location = new System.Drawing.Point(59, 201);
            this.btnProgress.Name = "btnProgress";
            this.btnProgress.Size = new System.Drawing.Size(75, 23);
            this.btnProgress.TabIndex = 1;
            this.btnProgress.Text = "button1";
            this.btnProgress.UseVisualStyleBackColor = true;
            this.btnProgress.Click += new System.EventHandler(this.btnProgress_Click);
            // 
            // dgvProgress
            // 
            this.dgvProgress.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProgress.Location = new System.Drawing.Point(19, 18);
            this.dgvProgress.Name = "dgvProgress";
            this.dgvProgress.Size = new System.Drawing.Size(834, 150);
            this.dgvProgress.TabIndex = 0;
            // 
            // tbpAdmin
            // 
            this.tbpAdmin.Controls.Add(this.btnCopyFiles);
            this.tbpAdmin.Controls.Add(this.btnIncreamentOrder);
            this.tbpAdmin.Controls.Add(this.btnReadWorkAutoLogin);
            this.tbpAdmin.Controls.Add(this.btnCleanAutoLogins);
            this.tbpAdmin.Controls.Add(this.btnCopyContineBat);
            this.tbpAdmin.Controls.Add(this.btnCleanContineBat);
            this.tbpAdmin.Location = new System.Drawing.Point(4, 22);
            this.tbpAdmin.Name = "tbpAdmin";
            this.tbpAdmin.Size = new System.Drawing.Size(875, 605);
            this.tbpAdmin.TabIndex = 2;
            this.tbpAdmin.Text = "Admin";
            this.tbpAdmin.UseVisualStyleBackColor = true;
            // 
            // btnCopyFiles
            // 
            this.btnCopyFiles.Location = new System.Drawing.Point(28, 108);
            this.btnCopyFiles.Name = "btnCopyFiles";
            this.btnCopyFiles.Size = new System.Drawing.Size(75, 23);
            this.btnCopyFiles.TabIndex = 6;
            this.btnCopyFiles.Text = "CopyFiles";
            this.btnCopyFiles.UseVisualStyleBackColor = true;
            this.btnCopyFiles.Click += new System.EventHandler(this.btnCopyFiles_Click);
            // 
            // btnIncreamentOrder
            // 
            this.btnIncreamentOrder.Location = new System.Drawing.Point(28, 78);
            this.btnIncreamentOrder.Name = "btnIncreamentOrder";
            this.btnIncreamentOrder.Size = new System.Drawing.Size(75, 23);
            this.btnIncreamentOrder.TabIndex = 5;
            this.btnIncreamentOrder.Text = "Increament Order";
            this.btnIncreamentOrder.UseVisualStyleBackColor = true;
            this.btnIncreamentOrder.Click += new System.EventHandler(this.btnIncreamentOrder_Click);
            // 
            // btnReadWorkAutoLogin
            // 
            this.btnReadWorkAutoLogin.Location = new System.Drawing.Point(174, 19);
            this.btnReadWorkAutoLogin.Name = "btnReadWorkAutoLogin";
            this.btnReadWorkAutoLogin.Size = new System.Drawing.Size(125, 23);
            this.btnReadWorkAutoLogin.TabIndex = 4;
            this.btnReadWorkAutoLogin.Text = "Read Work Auto Login";
            this.btnReadWorkAutoLogin.UseVisualStyleBackColor = true;
            this.btnReadWorkAutoLogin.Click += new System.EventHandler(this.btnReadWorkAutoLogin_Click);
            // 
            // btnCleanAutoLogins
            // 
            this.btnCleanAutoLogins.Location = new System.Drawing.Point(28, 19);
            this.btnCleanAutoLogins.Name = "btnCleanAutoLogins";
            this.btnCleanAutoLogins.Size = new System.Drawing.Size(134, 23);
            this.btnCleanAutoLogins.TabIndex = 3;
            this.btnCleanAutoLogins.Text = "Clean Auto Logins";
            this.btnCleanAutoLogins.UseVisualStyleBackColor = true;
            this.btnCleanAutoLogins.Click += new System.EventHandler(this.btnCleanAutoLogins_Click);
            // 
            // btnCopyContineBat
            // 
            this.btnCopyContineBat.Location = new System.Drawing.Point(174, 48);
            this.btnCopyContineBat.Name = "btnCopyContineBat";
            this.btnCopyContineBat.Size = new System.Drawing.Size(118, 23);
            this.btnCopyContineBat.TabIndex = 2;
            this.btnCopyContineBat.Text = "Copy Contine Bat";
            this.btnCopyContineBat.UseVisualStyleBackColor = true;
            this.btnCopyContineBat.Click += new System.EventHandler(this.btnCopyContineBat_Click);
            // 
            // btnCleanContineBat
            // 
            this.btnCleanContineBat.Location = new System.Drawing.Point(28, 48);
            this.btnCleanContineBat.Name = "btnCleanContineBat";
            this.btnCleanContineBat.Size = new System.Drawing.Size(125, 23);
            this.btnCleanContineBat.TabIndex = 1;
            this.btnCleanContineBat.Text = "Clean Continue Bat";
            this.btnCleanContineBat.UseVisualStyleBackColor = true;
            this.btnCleanContineBat.Click += new System.EventHandler(this.btnCleanContineBat_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(19, 246);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(834, 231);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // processWorker
            // 
            this.processWorker.WorkerReportsProgress = true;
            this.processWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.processWorker_DoWork);
            this.processWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.processWorker_ProgressChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 631);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbpInstall.ResumeLayout(false);
            this.tbpInstall.PerformLayout();
            this.gbxAutoLogin.ResumeLayout(false);
            this.gbxAutoLogin.PerformLayout();
            this.tbpProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgress)).EndInit();
            this.tbpAdmin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxInstall;
        private System.Windows.Forms.ComboBox cbxEnvironments;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.ListBox lbxAvailable;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpInstall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbpProgress;
        private System.Windows.Forms.TabPage tbpAdmin;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCleanContineBat;
        private System.Windows.Forms.Button btnCopyContineBat;
        private System.Windows.Forms.GroupBox gbxAutoLogin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnCleanAutoLogins;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblValidCredentials;
        private System.Windows.Forms.Label lblInvalidUserCredentials;
        private System.Windows.Forms.Button btnReadWorkAutoLogin;
        private System.Windows.Forms.Button btnIncreamentOrder;
        private System.Windows.Forms.Button btnCopyFiles;
        private System.Windows.Forms.Button btnProgress;
        private System.Windows.Forms.DataGridView dgvProgress;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.ComponentModel.BackgroundWorker processWorker;

    }
}

