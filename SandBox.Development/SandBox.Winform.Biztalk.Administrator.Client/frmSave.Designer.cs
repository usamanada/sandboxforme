namespace SandBox.Winform.Biztalk.Administrator.Client
{
    partial class frmSave
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
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Host = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpTask = new System.Windows.Forms.TabPage();
            this.tbpLog = new System.Windows.Forms.TabPage();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlFile = new System.Windows.Forms.Panel();
            this.pnlTabControl = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tbpTask.SuspendLayout();
            this.tbpLog.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFile.SuspendLayout();
            this.pnlTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(35, 6);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(442, 20);
            this.txtFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(452, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(371, 247);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.Location = new System.Drawing.Point(483, 4);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(29, 23);
            this.btnFile.TabIndex = 5;
            this.btnFile.Text = "...";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Action,
            this.Host,
            this.Status});
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(3, 3);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Size = new System.Drawing.Size(494, 68);
            this.dgvResults.TabIndex = 6;
            // 
            // Action
            // 
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            // 
            // Host
            // 
            this.Host.HeaderText = "Host";
            this.Host.Name = "Host";
            this.Host.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpTask);
            this.tabControl1.Controls.Add(this.tbpLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(515, 194);
            this.tabControl1.TabIndex = 7;
            // 
            // tbpTask
            // 
            this.tbpTask.Controls.Add(this.dgvResults);
            this.tbpTask.Location = new System.Drawing.Point(4, 22);
            this.tbpTask.Name = "tbpTask";
            this.tbpTask.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTask.Size = new System.Drawing.Size(500, 74);
            this.tbpTask.TabIndex = 1;
            this.tbpTask.Text = "Task";
            this.tbpTask.UseVisualStyleBackColor = true;
            // 
            // tbpLog
            // 
            this.tbpLog.Controls.Add(this.rtbLog);
            this.tbpLog.Location = new System.Drawing.Point(4, 22);
            this.tbpLog.Name = "tbpLog";
            this.tbpLog.Size = new System.Drawing.Size(507, 168);
            this.tbpLog.TabIndex = 2;
            this.tbpLog.Text = "Log";
            this.tbpLog.UseVisualStyleBackColor = true;
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(507, 168);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(452, 247);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlTabControl);
            this.panel1.Controls.Add(this.pnlFile);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 229);
            this.panel1.TabIndex = 9;
            // 
            // pnlFile
            // 
            this.pnlFile.Controls.Add(this.btnFile);
            this.pnlFile.Controls.Add(this.txtFile);
            this.pnlFile.Controls.Add(this.label1);
            this.pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFile.Location = new System.Drawing.Point(0, 0);
            this.pnlFile.Name = "pnlFile";
            this.pnlFile.Size = new System.Drawing.Size(515, 35);
            this.pnlFile.TabIndex = 0;
            // 
            // pnlTabControl
            // 
            this.pnlTabControl.Controls.Add(this.tabControl1);
            this.pnlTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabControl.Location = new System.Drawing.Point(0, 35);
            this.pnlTabControl.Name = "pnlTabControl";
            this.pnlTabControl.Size = new System.Drawing.Size(515, 194);
            this.pnlTabControl.TabIndex = 1;
            // 
            // frmSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 273);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmSave";
            this.Text = "SaveToFile";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tbpTask.ResumeLayout(false);
            this.tbpLog.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlFile.ResumeLayout(false);
            this.pnlFile.PerformLayout();
            this.pnlTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn Host;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpTask;
        private System.Windows.Forms.TabPage tbpLog;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTabControl;
        private System.Windows.Forms.Panel pnlFile;

    }
}