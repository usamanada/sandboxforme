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
            this.tbcInstall = new System.Windows.Forms.TabControl();
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
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.tbpProgress = new System.Windows.Forms.TabPage();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.dgvProgress = new System.Windows.Forms.DataGridView();
            this.dgcProgImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgcProgOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcProgApplication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcProgMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpAdmin = new System.Windows.Forms.TabPage();
            this.btnCreateWorkConfig = new System.Windows.Forms.Button();
            this.btnCopyBatchFiles = new System.Windows.Forms.Button();
            this.btnCleanAutoLogins = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.processWorker = new System.ComponentModel.BackgroundWorker();
            this.tbcInstall.SuspendLayout();
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
            this.lbxInstall.Size = new System.Drawing.Size(339, 264);
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
            this.btnInstall.Location = new System.Drawing.Point(342, 541);
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
            this.lbxAvailable.Location = new System.Drawing.Point(479, 67);
            this.lbxAvailable.Name = "lbxAvailable";
            this.lbxAvailable.Size = new System.Drawing.Size(339, 264);
            this.lbxAvailable.TabIndex = 4;
            // 
            // tbcInstall
            // 
            this.tbcInstall.Controls.Add(this.tbpInstall);
            this.tbcInstall.Controls.Add(this.tbpProgress);
            this.tbcInstall.Controls.Add(this.tbpAdmin);
            this.tbcInstall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcInstall.Location = new System.Drawing.Point(0, 0);
            this.tbcInstall.Name = "tbcInstall";
            this.tbcInstall.SelectedIndex = 0;
            this.tbcInstall.Size = new System.Drawing.Size(883, 631);
            this.tbcInstall.TabIndex = 9;
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
            this.gbxAutoLogin.Size = new System.Drawing.Size(336, 157);
            this.gbxAutoLogin.TabIndex = 11;
            this.gbxAutoLogin.TabStop = false;
            this.gbxAutoLogin.Text = "Auto Login";
            // 
            // lblInvalidUserCredentials
            // 
            this.lblInvalidUserCredentials.AutoSize = true;
            this.lblInvalidUserCredentials.ForeColor = System.Drawing.Color.Red;
            this.lblInvalidUserCredentials.Location = new System.Drawing.Point(40, 128);
            this.lblInvalidUserCredentials.Name = "lblInvalidUserCredentials";
            this.lblInvalidUserCredentials.Size = new System.Drawing.Size(200, 13);
            this.lblInvalidUserCredentials.TabIndex = 10;
            this.lblInvalidUserCredentials.Text = "InValid Username and Password Entered";
            this.lblInvalidUserCredentials.Visible = false;
            // 
            // lblValidCredentials
            // 
            this.lblValidCredentials.AutoSize = true;
            this.lblValidCredentials.Location = new System.Drawing.Point(49, 128);
            this.lblValidCredentials.Name = "lblValidCredentials";
            this.lblValidCredentials.Size = new System.Drawing.Size(191, 13);
            this.lblValidCredentials.TabIndex = 9;
            this.lblValidCredentials.Text = "Valid Username and Password Entered";
            this.lblValidCredentials.Visible = false;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(248, 123);
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
            this.label2.Location = new System.Drawing.Point(476, 48);
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
            // btnLeft
            // 
            this.btnLeft.Image = global::SandBox.Winform.SilentInstall.Properties.Resources.LeftGreen;
            this.btnLeft.Location = new System.Drawing.Point(423, 179);
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
            this.btnRight.Location = new System.Drawing.Point(423, 123);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(50, 50);
            this.btnRight.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnRight, "Remove");
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // tbpProgress
            // 
            this.tbpProgress.Controls.Add(this.rtbLog);
            this.tbpProgress.Controls.Add(this.dgvProgress);
            this.tbpProgress.Location = new System.Drawing.Point(4, 22);
            this.tbpProgress.Name = "tbpProgress";
            this.tbpProgress.Padding = new System.Windows.Forms.Padding(3);
            this.tbpProgress.Size = new System.Drawing.Size(875, 605);
            this.tbpProgress.TabIndex = 1;
            this.tbpProgress.Text = "Progress";
            this.tbpProgress.UseVisualStyleBackColor = true;
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(19, 200);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(834, 397);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // dgvProgress
            // 
            this.dgvProgress.AllowUserToAddRows = false;
            this.dgvProgress.AllowUserToDeleteRows = false;
            this.dgvProgress.AllowUserToResizeRows = false;
            this.dgvProgress.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProgress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcProgImage,
            this.dgcProgOrder,
            this.dgcProgApplication,
            this.dgcProgMessage});
            this.dgvProgress.Location = new System.Drawing.Point(19, 18);
            this.dgvProgress.MultiSelect = false;
            this.dgvProgress.Name = "dgvProgress";
            this.dgvProgress.Size = new System.Drawing.Size(834, 176);
            this.dgvProgress.TabIndex = 0;
            // 
            // dgcProgImage
            // 
            this.dgcProgImage.HeaderText = "Image";
            this.dgcProgImage.Name = "dgcProgImage";
            this.dgcProgImage.ReadOnly = true;
            // 
            // dgcProgOrder
            // 
            this.dgcProgOrder.HeaderText = "Order";
            this.dgcProgOrder.Name = "dgcProgOrder";
            this.dgcProgOrder.ReadOnly = true;
            this.dgcProgOrder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcProgApplication
            // 
            this.dgcProgApplication.HeaderText = "Application";
            this.dgcProgApplication.Name = "dgcProgApplication";
            this.dgcProgApplication.ReadOnly = true;
            this.dgcProgApplication.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcProgMessage
            // 
            this.dgcProgMessage.HeaderText = "Message";
            this.dgcProgMessage.Name = "dgcProgMessage";
            this.dgcProgMessage.ReadOnly = true;
            this.dgcProgMessage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tbpAdmin
            // 
            this.tbpAdmin.Controls.Add(this.btnCreateWorkConfig);
            this.tbpAdmin.Controls.Add(this.btnCopyBatchFiles);
            this.tbpAdmin.Controls.Add(this.btnCleanAutoLogins);
            this.tbpAdmin.Location = new System.Drawing.Point(4, 22);
            this.tbpAdmin.Name = "tbpAdmin";
            this.tbpAdmin.Size = new System.Drawing.Size(875, 605);
            this.tbpAdmin.TabIndex = 2;
            this.tbpAdmin.Text = "Admin";
            this.tbpAdmin.UseVisualStyleBackColor = true;
            // 
            // btnCreateWorkConfig
            // 
            this.btnCreateWorkConfig.Location = new System.Drawing.Point(28, 79);
            this.btnCreateWorkConfig.Name = "btnCreateWorkConfig";
            this.btnCreateWorkConfig.Size = new System.Drawing.Size(134, 23);
            this.btnCreateWorkConfig.TabIndex = 5;
            this.btnCreateWorkConfig.Text = "Create Work Config";
            this.btnCreateWorkConfig.UseVisualStyleBackColor = true;
            this.btnCreateWorkConfig.Click += new System.EventHandler(this.btnCreateWorkConfig_Click);
            // 
            // btnCopyBatchFiles
            // 
            this.btnCopyBatchFiles.Location = new System.Drawing.Point(28, 49);
            this.btnCopyBatchFiles.Name = "btnCopyBatchFiles";
            this.btnCopyBatchFiles.Size = new System.Drawing.Size(134, 23);
            this.btnCopyBatchFiles.TabIndex = 4;
            this.btnCopyBatchFiles.Text = "Copy Batch Files";
            this.btnCopyBatchFiles.UseVisualStyleBackColor = true;
            this.btnCopyBatchFiles.Click += new System.EventHandler(this.btnCopyBatchFiles_Click);
            // 
            // btnCleanAutoLogins
            // 
            this.btnCleanAutoLogins.Location = new System.Drawing.Point(28, 19);
            this.btnCleanAutoLogins.Name = "btnCleanAutoLogins";
            this.btnCleanAutoLogins.Size = new System.Drawing.Size(134, 23);
            this.btnCleanAutoLogins.TabIndex = 3;
            this.btnCleanAutoLogins.Text = "Clean Auto Login";
            this.btnCleanAutoLogins.UseVisualStyleBackColor = true;
            this.btnCleanAutoLogins.Click += new System.EventHandler(this.btnCleanAutoLogins_Click);
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
            this.Controls.Add(this.tbcInstall);
            this.Name = "Form1";
            this.Text = "Silent Install v1.3.0.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tbcInstall.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tbcInstall;
        private System.Windows.Forms.TabPage tbpInstall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbpProgress;
        private System.Windows.Forms.TabPage tbpAdmin;
        private System.Windows.Forms.ToolTip toolTip1;
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
        private System.Windows.Forms.DataGridView dgvProgress;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnCopyBatchFiles;
        private System.ComponentModel.BackgroundWorker processWorker;
        private System.Windows.Forms.DataGridViewImageColumn dgcProgImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcProgOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcProgApplication;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcProgMessage;
        private System.Windows.Forms.Button btnCreateWorkConfig;

    }
}

