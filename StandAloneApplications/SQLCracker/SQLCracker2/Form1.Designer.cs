namespace SQLCracker2
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
            this.txtMissingPasswordFile = new System.Windows.Forms.TextBox();
            this.PasswordBtn = new System.Windows.Forms.Button();
            this.txtCharacterSearch = new System.Windows.Forms.TextBox();
            this.DictionaryBtn = new System.Windows.Forms.Button();
            this.txtDictionaryFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFoundPasswordFile = new System.Windows.Forms.TextBox();
            this.OutputDirBtb = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.GoBtn = new System.Windows.Forms.Button();
            this.UsersMissingPassWordTrv = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PasswordsTP = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblMissingPasswordCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFoundPasswordCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UsersHavingPassWordTrv = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtServerFinish = new System.Windows.Forms.TextBox();
            this.txtServerStart = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxDictionary = new System.Windows.Forms.CheckBox();
            this.cbxBruteForce = new System.Windows.Forms.CheckBox();
            this.gbxAttacks = new System.Windows.Forms.GroupBox();
            this.txtCurrentPosition = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Worker = new System.ComponentModel.BackgroundWorker();
            this.cbxStandAlone = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.PasswordsTP.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbxAttacks.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMissingPasswordFile
            // 
            this.txtMissingPasswordFile.Location = new System.Drawing.Point(102, 13);
            this.txtMissingPasswordFile.Name = "txtMissingPasswordFile";
            this.txtMissingPasswordFile.ReadOnly = true;
            this.txtMissingPasswordFile.Size = new System.Drawing.Size(558, 20);
            this.txtMissingPasswordFile.TabIndex = 0;
            // 
            // PasswordBtn
            // 
            this.PasswordBtn.Location = new System.Drawing.Point(667, 13);
            this.PasswordBtn.Name = "PasswordBtn";
            this.PasswordBtn.Size = new System.Drawing.Size(30, 23);
            this.PasswordBtn.TabIndex = 1;
            this.PasswordBtn.Text = "...";
            this.PasswordBtn.UseVisualStyleBackColor = true;
            this.PasswordBtn.Click += new System.EventHandler(this.PasswordBtn_Click);
            // 
            // txtCharacterSearch
            // 
            this.txtCharacterSearch.Location = new System.Drawing.Point(102, 95);
            this.txtCharacterSearch.Name = "txtCharacterSearch";
            this.txtCharacterSearch.Size = new System.Drawing.Size(421, 20);
            this.txtCharacterSearch.TabIndex = 2;
            this.txtCharacterSearch.Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-=!@#$%^&*()_+[]\\{}|;\':\",./<>?";
            // 
            // DictionaryBtn
            // 
            this.DictionaryBtn.Location = new System.Drawing.Point(667, 42);
            this.DictionaryBtn.Name = "DictionaryBtn";
            this.DictionaryBtn.Size = new System.Drawing.Size(30, 23);
            this.DictionaryBtn.TabIndex = 4;
            this.DictionaryBtn.Text = "...";
            this.DictionaryBtn.UseVisualStyleBackColor = true;
            this.DictionaryBtn.Click += new System.EventHandler(this.DictionaryBtn_Click);
            // 
            // txtDictionaryFile
            // 
            this.txtDictionaryFile.Location = new System.Drawing.Point(102, 42);
            this.txtDictionaryFile.Name = "txtDictionaryFile";
            this.txtDictionaryFile.ReadOnly = true;
            this.txtDictionaryFile.Size = new System.Drawing.Size(558, 20);
            this.txtDictionaryFile.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Missing Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Dictionary";
            // 
            // txtFoundPasswordFile
            // 
            this.txtFoundPasswordFile.Location = new System.Drawing.Point(102, 69);
            this.txtFoundPasswordFile.Name = "txtFoundPasswordFile";
            this.txtFoundPasswordFile.ReadOnly = true;
            this.txtFoundPasswordFile.Size = new System.Drawing.Size(558, 20);
            this.txtFoundPasswordFile.TabIndex = 7;
            // 
            // OutputDirBtb
            // 
            this.OutputDirBtb.Location = new System.Drawing.Point(667, 67);
            this.OutputDirBtb.Name = "OutputDirBtb";
            this.OutputDirBtb.Size = new System.Drawing.Size(30, 23);
            this.OutputDirBtb.TabIndex = 8;
            this.OutputDirBtb.Text = "...";
            this.OutputDirBtb.UseVisualStyleBackColor = true;
            this.OutputDirBtb.Click += new System.EventHandler(this.OutputDirBtb_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Found Password";
            // 
            // GoBtn
            // 
            this.GoBtn.Location = new System.Drawing.Point(140, 13);
            this.GoBtn.Name = "GoBtn";
            this.GoBtn.Size = new System.Drawing.Size(75, 23);
            this.GoBtn.TabIndex = 10;
            this.GoBtn.Text = "Go";
            this.GoBtn.UseVisualStyleBackColor = true;
            this.GoBtn.Click += new System.EventHandler(this.GoBtn_Click);
            // 
            // UsersMissingPassWordTrv
            // 
            this.UsersMissingPassWordTrv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.UsersMissingPassWordTrv.Location = new System.Drawing.Point(0, 20);
            this.UsersMissingPassWordTrv.Name = "UsersMissingPassWordTrv";
            this.UsersMissingPassWordTrv.Size = new System.Drawing.Size(399, 466);
            this.UsersMissingPassWordTrv.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.PasswordsTP);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 168);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(790, 515);
            this.tabControl1.TabIndex = 12;
            // 
            // PasswordsTP
            // 
            this.PasswordsTP.Controls.Add(this.splitContainer1);
            this.PasswordsTP.Location = new System.Drawing.Point(4, 22);
            this.PasswordsTP.Name = "PasswordsTP";
            this.PasswordsTP.Padding = new System.Windows.Forms.Padding(3);
            this.PasswordsTP.Size = new System.Drawing.Size(782, 489);
            this.PasswordsTP.TabIndex = 0;
            this.PasswordsTP.Text = "Passwords";
            this.PasswordsTP.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblMissingPasswordCount);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.UsersMissingPassWordTrv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblFoundPasswordCount);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.UsersHavingPassWordTrv);
            this.splitContainer1.Size = new System.Drawing.Size(776, 483);
            this.splitContainer1.SplitterDistance = 399;
            this.splitContainer1.TabIndex = 13;
            // 
            // lblMissingPasswordCount
            // 
            this.lblMissingPasswordCount.AutoSize = true;
            this.lblMissingPasswordCount.Location = new System.Drawing.Point(106, 4);
            this.lblMissingPasswordCount.Name = "lblMissingPasswordCount";
            this.lblMissingPasswordCount.Size = new System.Drawing.Size(0, 13);
            this.lblMissingPasswordCount.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Passwords Missing";
            // 
            // lblFoundPasswordCount
            // 
            this.lblFoundPasswordCount.AutoSize = true;
            this.lblFoundPasswordCount.Location = new System.Drawing.Point(101, 4);
            this.lblFoundPasswordCount.Name = "lblFoundPasswordCount";
            this.lblFoundPasswordCount.Size = new System.Drawing.Size(0, 13);
            this.lblFoundPasswordCount.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Passwords Found";
            // 
            // UsersHavingPassWordTrv
            // 
            this.UsersHavingPassWordTrv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.UsersHavingPassWordTrv.Location = new System.Drawing.Point(0, 20);
            this.UsersHavingPassWordTrv.Name = "UsersHavingPassWordTrv";
            this.UsersHavingPassWordTrv.Size = new System.Drawing.Size(373, 466);
            this.UsersHavingPassWordTrv.TabIndex = 12;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.txtServerFinish);
            this.tabPage2.Controls.Add(this.txtServerStart);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(782, 489);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Server";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Finish";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Start";
            // 
            // txtServerFinish
            // 
            this.txtServerFinish.Location = new System.Drawing.Point(71, 57);
            this.txtServerFinish.Name = "txtServerFinish";
            this.txtServerFinish.Size = new System.Drawing.Size(100, 20);
            this.txtServerFinish.TabIndex = 1;
            // 
            // txtServerStart
            // 
            this.txtServerStart.Location = new System.Drawing.Point(71, 30);
            this.txtServerStart.Name = "txtServerStart";
            this.txtServerStart.Size = new System.Drawing.Size(100, 20);
            this.txtServerStart.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(140, 39);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(102, 121);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(44, 20);
            this.txtSize.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Character Search";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Size";
            // 
            // cbxDictionary
            // 
            this.cbxDictionary.AutoSize = true;
            this.cbxDictionary.Checked = true;
            this.cbxDictionary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxDictionary.Location = new System.Drawing.Point(6, 17);
            this.cbxDictionary.Name = "cbxDictionary";
            this.cbxDictionary.Size = new System.Drawing.Size(73, 17);
            this.cbxDictionary.TabIndex = 17;
            this.cbxDictionary.Text = "Dictionary";
            this.cbxDictionary.UseVisualStyleBackColor = true;
            // 
            // cbxBruteForce
            // 
            this.cbxBruteForce.AutoSize = true;
            this.cbxBruteForce.Checked = true;
            this.cbxBruteForce.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxBruteForce.Location = new System.Drawing.Point(6, 34);
            this.cbxBruteForce.Name = "cbxBruteForce";
            this.cbxBruteForce.Size = new System.Drawing.Size(81, 17);
            this.cbxBruteForce.TabIndex = 18;
            this.cbxBruteForce.Text = "Brute Force";
            this.cbxBruteForce.UseVisualStyleBackColor = true;
            // 
            // gbxAttacks
            // 
            this.gbxAttacks.Controls.Add(this.cbxStandAlone);
            this.gbxAttacks.Controls.Add(this.cbxDictionary);
            this.gbxAttacks.Controls.Add(this.cbxBruteForce);
            this.gbxAttacks.Controls.Add(this.GoBtn);
            this.gbxAttacks.Controls.Add(this.btnStop);
            this.gbxAttacks.Location = new System.Drawing.Point(558, 100);
            this.gbxAttacks.Name = "gbxAttacks";
            this.gbxAttacks.Size = new System.Drawing.Size(221, 84);
            this.gbxAttacks.TabIndex = 19;
            this.gbxAttacks.TabStop = false;
            this.gbxAttacks.Text = "Attacks";
            // 
            // txtCurrentPosition
            // 
            this.txtCurrentPosition.Location = new System.Drawing.Point(102, 146);
            this.txtCurrentPosition.Name = "txtCurrentPosition";
            this.txtCurrentPosition.Size = new System.Drawing.Size(278, 20);
            this.txtCurrentPosition.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Current Position";
            // 
            // Worker
            // 
            this.Worker.WorkerReportsProgress = true;
            this.Worker.WorkerSupportsCancellation = true;
            this.Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            this.Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_RunWorkerCompleted);
            this.Worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Worker_ProgressChanged);
            // 
            // cbxStandAlone
            // 
            this.cbxStandAlone.AutoSize = true;
            this.cbxStandAlone.Checked = true;
            this.cbxStandAlone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxStandAlone.Location = new System.Drawing.Point(6, 51);
            this.cbxStandAlone.Name = "cbxStandAlone";
            this.cbxStandAlone.Size = new System.Drawing.Size(84, 17);
            this.cbxStandAlone.TabIndex = 19;
            this.cbxStandAlone.Text = "Stand Alone";
            this.cbxStandAlone.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 683);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCurrentPosition);
            this.Controls.Add(this.gbxAttacks);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OutputDirBtb);
            this.Controls.Add(this.txtFoundPasswordFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DictionaryBtn);
            this.Controls.Add(this.txtDictionaryFile);
            this.Controls.Add(this.txtCharacterSearch);
            this.Controls.Add(this.PasswordBtn);
            this.Controls.Add(this.txtMissingPasswordFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.PasswordsTP.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.gbxAttacks.ResumeLayout(false);
            this.gbxAttacks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMissingPasswordFile;
        private System.Windows.Forms.Button PasswordBtn;
        private System.Windows.Forms.TextBox txtCharacterSearch;
        private System.Windows.Forms.Button DictionaryBtn;
        private System.Windows.Forms.TextBox txtDictionaryFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFoundPasswordFile;
        private System.Windows.Forms.Button OutputDirBtb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button GoBtn;
        private System.Windows.Forms.TreeView UsersMissingPassWordTrv;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PasswordsTP;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView UsersHavingPassWordTrv;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblMissingPasswordCount;
        private System.Windows.Forms.Label lblFoundPasswordCount;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbxDictionary;
        private System.Windows.Forms.CheckBox cbxBruteForce;
        private System.Windows.Forms.GroupBox gbxAttacks;
        private System.Windows.Forms.TextBox txtCurrentPosition;
        private System.Windows.Forms.Label label8;
        private System.ComponentModel.BackgroundWorker Worker;
        private System.Windows.Forms.TextBox txtServerFinish;
        private System.Windows.Forms.TextBox txtServerStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbxStandAlone;
    }
}

