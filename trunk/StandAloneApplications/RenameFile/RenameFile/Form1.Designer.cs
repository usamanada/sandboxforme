namespace RenameFile
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
            this.btnDir = new System.Windows.Forms.Button();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpBizTalkXml = new System.Windows.Forms.TabPage();
            this.chkBiztalkXmlFormat = new System.Windows.Forms.CheckBox();
            this.chkBiztalkXMLRename = new System.Windows.Forms.CheckBox();
            this.btnBizTalkXMLGo = new System.Windows.Forms.Button();
            this.tbpFileRename = new System.Windows.Forms.TabPage();
            this.dgvFRDetails = new System.Windows.Forms.DataGridView();
            this.dgvTxtColFROriginalPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtColFROriginalFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtColModifiedFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbtFileRenameRegExpression = new System.Windows.Forms.RadioButton();
            this.rbtFRSimple = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFRRegExpression = new System.Windows.Forms.TextBox();
            this.txtFRStartingCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFRFileFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFRModify = new System.Windows.Forms.Button();
            this.btnFRPreview = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxFRFileFormat = new System.Windows.Forms.TextBox();
            this.tbpFileModify = new System.Windows.Forms.TabPage();
            this.btnFileModfiyRefresh = new System.Windows.Forms.Button();
            this.lbxFileModify = new System.Windows.Forms.ListBox();
            this.tbpRegularExpression = new System.Windows.Forms.TabPage();
            this.rtbtbxREData = new System.Windows.Forms.RichTextBox();
            this.tbxREExpression = new System.Windows.Forms.TextBox();
            this.btnREFindMatch = new System.Windows.Forms.Button();
            this.tbpPaths = new System.Windows.Forms.TabPage();
            this.btnPathsResult = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPathsResult = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPathsAbsolutePath = new System.Windows.Forms.Button();
            this.btnPathsRelativeTo = new System.Windows.Forms.Button();
            this.tbxPathsAbsolutePath = new System.Windows.Forms.TextBox();
            this.tbxPathsRelativeTo = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tbpBizTalkXml.SuspendLayout();
            this.tbpFileRename.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFRDetails)).BeginInit();
            this.tbpFileModify.SuspendLayout();
            this.tbpRegularExpression.SuspendLayout();
            this.tbpPaths.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(13, 13);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(32, 23);
            this.btnDir.TabIndex = 0;
            this.btnDir.Text = "...";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // txtDir
            // 
            this.txtDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDir.Location = new System.Drawing.Point(51, 15);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(716, 20);
            this.txtDir.TabIndex = 1;
            this.txtDir.Text = "C:\\Temp\\photos";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tbpBizTalkXml);
            this.tabControl1.Controls.Add(this.tbpFileRename);
            this.tabControl1.Controls.Add(this.tbpFileModify);
            this.tabControl1.Controls.Add(this.tbpRegularExpression);
            this.tabControl1.Controls.Add(this.tbpPaths);
            this.tabControl1.Location = new System.Drawing.Point(0, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(775, 768);
            this.tabControl1.TabIndex = 2;
            // 
            // tbpBizTalkXml
            // 
            this.tbpBizTalkXml.Controls.Add(this.chkBiztalkXmlFormat);
            this.tbpBizTalkXml.Controls.Add(this.chkBiztalkXMLRename);
            this.tbpBizTalkXml.Controls.Add(this.btnBizTalkXMLGo);
            this.tbpBizTalkXml.Location = new System.Drawing.Point(4, 22);
            this.tbpBizTalkXml.Name = "tbpBizTalkXml";
            this.tbpBizTalkXml.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBizTalkXml.Size = new System.Drawing.Size(767, 742);
            this.tbpBizTalkXml.TabIndex = 0;
            this.tbpBizTalkXml.Text = "Biztalk Xml";
            this.tbpBizTalkXml.UseVisualStyleBackColor = true;
            // 
            // chkBiztalkXmlFormat
            // 
            this.chkBiztalkXmlFormat.AutoSize = true;
            this.chkBiztalkXmlFormat.Location = new System.Drawing.Point(81, 11);
            this.chkBiztalkXmlFormat.Name = "chkBiztalkXmlFormat";
            this.chkBiztalkXmlFormat.Size = new System.Drawing.Size(58, 17);
            this.chkBiztalkXmlFormat.TabIndex = 2;
            this.chkBiztalkXmlFormat.Text = "Format";
            this.chkBiztalkXmlFormat.UseVisualStyleBackColor = true;
            // 
            // chkBiztalkXMLRename
            // 
            this.chkBiztalkXMLRename.AutoSize = true;
            this.chkBiztalkXMLRename.Location = new System.Drawing.Point(9, 11);
            this.chkBiztalkXMLRename.Name = "chkBiztalkXMLRename";
            this.chkBiztalkXMLRename.Size = new System.Drawing.Size(66, 17);
            this.chkBiztalkXMLRename.TabIndex = 1;
            this.chkBiztalkXMLRename.Text = "Rename";
            this.chkBiztalkXMLRename.UseVisualStyleBackColor = true;
            // 
            // btnBizTalkXMLGo
            // 
            this.btnBizTalkXMLGo.Location = new System.Drawing.Point(145, 7);
            this.btnBizTalkXMLGo.Name = "btnBizTalkXMLGo";
            this.btnBizTalkXMLGo.Size = new System.Drawing.Size(75, 23);
            this.btnBizTalkXMLGo.TabIndex = 0;
            this.btnBizTalkXMLGo.Text = "Go";
            this.btnBizTalkXMLGo.UseVisualStyleBackColor = true;
            this.btnBizTalkXMLGo.Click += new System.EventHandler(this.btnBizTalkXMLGo_Click);
            // 
            // tbpFileRename
            // 
            this.tbpFileRename.Controls.Add(this.dgvFRDetails);
            this.tbpFileRename.Controls.Add(this.rbtFileRenameRegExpression);
            this.tbpFileRename.Controls.Add(this.rbtFRSimple);
            this.tbpFileRename.Controls.Add(this.label2);
            this.tbpFileRename.Controls.Add(this.txtFRRegExpression);
            this.tbpFileRename.Controls.Add(this.txtFRStartingCount);
            this.tbpFileRename.Controls.Add(this.label4);
            this.tbpFileRename.Controls.Add(this.txtFRFileFilter);
            this.tbpFileRename.Controls.Add(this.label3);
            this.tbpFileRename.Controls.Add(this.btnFRModify);
            this.tbpFileRename.Controls.Add(this.btnFRPreview);
            this.tbpFileRename.Controls.Add(this.label1);
            this.tbpFileRename.Controls.Add(this.tbxFRFileFormat);
            this.tbpFileRename.Location = new System.Drawing.Point(4, 22);
            this.tbpFileRename.Name = "tbpFileRename";
            this.tbpFileRename.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFileRename.Size = new System.Drawing.Size(767, 742);
            this.tbpFileRename.TabIndex = 1;
            this.tbpFileRename.Text = "File Rename";
            this.tbpFileRename.UseVisualStyleBackColor = true;
            // 
            // dgvFRDetails
            // 
            this.dgvFRDetails.AllowUserToAddRows = false;
            this.dgvFRDetails.AllowUserToDeleteRows = false;
            this.dgvFRDetails.AllowUserToOrderColumns = true;
            this.dgvFRDetails.AllowUserToResizeRows = false;
            this.dgvFRDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFRDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFRDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFRDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTxtColFROriginalPath,
            this.dgvTxtColFROriginalFileName,
            this.dgvTxtColModifiedFileName});
            this.dgvFRDetails.Location = new System.Drawing.Point(3, 97);
            this.dgvFRDetails.Name = "dgvFRDetails";
            this.dgvFRDetails.Size = new System.Drawing.Size(761, 642);
            this.dgvFRDetails.TabIndex = 15;
            // 
            // dgvTxtColFROriginalPath
            // 
            this.dgvTxtColFROriginalPath.DataPropertyName = "Original Path";
            this.dgvTxtColFROriginalPath.HeaderText = "Original Path";
            this.dgvTxtColFROriginalPath.Name = "dgvTxtColFROriginalPath";
            this.dgvTxtColFROriginalPath.ReadOnly = true;
            this.dgvTxtColFROriginalPath.Width = 85;
            // 
            // dgvTxtColFROriginalFileName
            // 
            this.dgvTxtColFROriginalFileName.DataPropertyName = "Original FileName";
            this.dgvTxtColFROriginalFileName.HeaderText = "Original FileName";
            this.dgvTxtColFROriginalFileName.Name = "dgvTxtColFROriginalFileName";
            this.dgvTxtColFROriginalFileName.ReadOnly = true;
            this.dgvTxtColFROriginalFileName.Width = 105;
            // 
            // dgvTxtColModifiedFileName
            // 
            this.dgvTxtColModifiedFileName.DataPropertyName = "Modified FileName";
            this.dgvTxtColModifiedFileName.HeaderText = "Modified FileName";
            this.dgvTxtColModifiedFileName.Name = "dgvTxtColModifiedFileName";
            this.dgvTxtColModifiedFileName.Width = 109;
            // 
            // rbtFileRenameRegExpression
            // 
            this.rbtFileRenameRegExpression.AutoSize = true;
            this.rbtFileRenameRegExpression.Location = new System.Drawing.Point(520, 31);
            this.rbtFileRenameRegExpression.Name = "rbtFileRenameRegExpression";
            this.rbtFileRenameRegExpression.Size = new System.Drawing.Size(116, 17);
            this.rbtFileRenameRegExpression.TabIndex = 14;
            this.rbtFileRenameRegExpression.TabStop = true;
            this.rbtFileRenameRegExpression.Text = "Regular Expression";
            this.rbtFileRenameRegExpression.UseVisualStyleBackColor = true;
            // 
            // rbtFRSimple
            // 
            this.rbtFRSimple.AutoSize = true;
            this.rbtFRSimple.Location = new System.Drawing.Point(520, 10);
            this.rbtFRSimple.Name = "rbtFRSimple";
            this.rbtFRSimple.Size = new System.Drawing.Size(56, 17);
            this.rbtFRSimple.TabIndex = 13;
            this.rbtFRSimple.TabStop = true;
            this.rbtFRSimple.Text = "Simple";
            this.rbtFRSimple.UseVisualStyleBackColor = true;
            this.rbtFRSimple.CheckedChanged += new System.EventHandler(this.rbtFRSimple_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Format Reg";
            // 
            // txtFRRegExpression
            // 
            this.txtFRRegExpression.Location = new System.Drawing.Point(78, 33);
            this.txtFRRegExpression.Name = "txtFRRegExpression";
            this.txtFRRegExpression.Size = new System.Drawing.Size(422, 20);
            this.txtFRRegExpression.TabIndex = 11;
            this.txtFRRegExpression.Text = "(?<Filename>\\d.*?\\..*?)\\..*?(?<Extension>\\.\\w+)$";
            // 
            // txtFRStartingCount
            // 
            this.txtFRStartingCount.Location = new System.Drawing.Point(327, 9);
            this.txtFRStartingCount.Name = "txtFRStartingCount";
            this.txtFRStartingCount.Size = new System.Drawing.Size(173, 20);
            this.txtFRStartingCount.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Starting Count";
            // 
            // txtFRFileFilter
            // 
            this.txtFRFileFilter.Location = new System.Drawing.Point(78, 56);
            this.txtFRFileFilter.Name = "txtFRFileFilter";
            this.txtFRFileFilter.Size = new System.Drawing.Size(162, 20);
            this.txtFRFileFilter.TabIndex = 8;
            this.txtFRFileFilter.Text = "*.jpg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "File Filter";
            // 
            // btnFRModify
            // 
            this.btnFRModify.Location = new System.Drawing.Point(425, 59);
            this.btnFRModify.Name = "btnFRModify";
            this.btnFRModify.Size = new System.Drawing.Size(75, 23);
            this.btnFRModify.TabIndex = 5;
            this.btnFRModify.Text = "Modify";
            this.btnFRModify.UseVisualStyleBackColor = true;
            this.btnFRModify.Click += new System.EventHandler(this.btnFRModify_Click);
            // 
            // btnFRPreview
            // 
            this.btnFRPreview.Location = new System.Drawing.Point(344, 59);
            this.btnFRPreview.Name = "btnFRPreview";
            this.btnFRPreview.Size = new System.Drawing.Size(75, 23);
            this.btnFRPreview.TabIndex = 4;
            this.btnFRPreview.Text = "Preview";
            this.btnFRPreview.UseVisualStyleBackColor = true;
            this.btnFRPreview.Click += new System.EventHandler(this.btnFRPreview_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Format";
            // 
            // tbxFRFileFormat
            // 
            this.tbxFRFileFormat.Location = new System.Drawing.Point(78, 10);
            this.tbxFRFileFormat.Name = "tbxFRFileFormat";
            this.tbxFRFileFormat.Size = new System.Drawing.Size(162, 20);
            this.tbxFRFileFormat.TabIndex = 0;
            this.tbxFRFileFormat.Text = "DSC{0:0000}.jpg";
            // 
            // tbpFileModify
            // 
            this.tbpFileModify.Controls.Add(this.btnFileModfiyRefresh);
            this.tbpFileModify.Controls.Add(this.lbxFileModify);
            this.tbpFileModify.Location = new System.Drawing.Point(4, 22);
            this.tbpFileModify.Name = "tbpFileModify";
            this.tbpFileModify.Size = new System.Drawing.Size(767, 742);
            this.tbpFileModify.TabIndex = 2;
            this.tbpFileModify.Text = "File Modify";
            this.tbpFileModify.UseVisualStyleBackColor = true;
            // 
            // btnFileModfiyRefresh
            // 
            this.btnFileModfiyRefresh.Location = new System.Drawing.Point(9, 195);
            this.btnFileModfiyRefresh.Name = "btnFileModfiyRefresh";
            this.btnFileModfiyRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnFileModfiyRefresh.TabIndex = 1;
            this.btnFileModfiyRefresh.Text = "Refresh";
            this.btnFileModfiyRefresh.UseVisualStyleBackColor = true;
            this.btnFileModfiyRefresh.Click += new System.EventHandler(this.btnFileModfiyRefresh_Click);
            // 
            // lbxFileModify
            // 
            this.lbxFileModify.FormattingEnabled = true;
            this.lbxFileModify.Location = new System.Drawing.Point(9, 3);
            this.lbxFileModify.Name = "lbxFileModify";
            this.lbxFileModify.Size = new System.Drawing.Size(213, 186);
            this.lbxFileModify.TabIndex = 0;
            this.lbxFileModify.SelectedIndexChanged += new System.EventHandler(this.lbxFileModify_SelectedIndexChanged);
            // 
            // tbpRegularExpression
            // 
            this.tbpRegularExpression.Controls.Add(this.rtbtbxREData);
            this.tbpRegularExpression.Controls.Add(this.tbxREExpression);
            this.tbpRegularExpression.Controls.Add(this.btnREFindMatch);
            this.tbpRegularExpression.Location = new System.Drawing.Point(4, 22);
            this.tbpRegularExpression.Name = "tbpRegularExpression";
            this.tbpRegularExpression.Size = new System.Drawing.Size(767, 742);
            this.tbpRegularExpression.TabIndex = 3;
            this.tbpRegularExpression.Text = "RegularExpression";
            this.tbpRegularExpression.UseVisualStyleBackColor = true;
            // 
            // rtbtbxREData
            // 
            this.rtbtbxREData.Location = new System.Drawing.Point(9, 50);
            this.rtbtbxREData.Name = "rtbtbxREData";
            this.rtbtbxREData.Size = new System.Drawing.Size(478, 96);
            this.rtbtbxREData.TabIndex = 2;
            this.rtbtbxREData.Text = "man.vs.wild.101.ws.dsr.xvid-omicron.avi";
            // 
            // tbxREExpression
            // 
            this.tbxREExpression.Location = new System.Drawing.Point(9, 24);
            this.tbxREExpression.Name = "tbxREExpression";
            this.tbxREExpression.Size = new System.Drawing.Size(478, 20);
            this.tbxREExpression.TabIndex = 1;
            this.tbxREExpression.Text = "(?<Filename>\\d+.\\w*)|(?<Extension>\\.\\w+)$";
            // 
            // btnREFindMatch
            // 
            this.btnREFindMatch.Location = new System.Drawing.Point(557, 24);
            this.btnREFindMatch.Name = "btnREFindMatch";
            this.btnREFindMatch.Size = new System.Drawing.Size(75, 23);
            this.btnREFindMatch.TabIndex = 0;
            this.btnREFindMatch.Text = "Match";
            this.btnREFindMatch.UseVisualStyleBackColor = true;
            this.btnREFindMatch.Click += new System.EventHandler(this.btnREFindMatch_Click);
            // 
            // tbpPaths
            // 
            this.tbpPaths.Controls.Add(this.btnPathsResult);
            this.tbpPaths.Controls.Add(this.label7);
            this.tbpPaths.Controls.Add(this.txtPathsResult);
            this.tbpPaths.Controls.Add(this.label6);
            this.tbpPaths.Controls.Add(this.label5);
            this.tbpPaths.Controls.Add(this.btnPathsAbsolutePath);
            this.tbpPaths.Controls.Add(this.btnPathsRelativeTo);
            this.tbpPaths.Controls.Add(this.tbxPathsAbsolutePath);
            this.tbpPaths.Controls.Add(this.tbxPathsRelativeTo);
            this.tbpPaths.Location = new System.Drawing.Point(4, 22);
            this.tbpPaths.Name = "tbpPaths";
            this.tbpPaths.Size = new System.Drawing.Size(767, 742);
            this.tbpPaths.TabIndex = 4;
            this.tbpPaths.Text = "Paths";
            this.tbpPaths.UseVisualStyleBackColor = true;
            // 
            // btnPathsResult
            // 
            this.btnPathsResult.Location = new System.Drawing.Point(552, 75);
            this.btnPathsResult.Name = "btnPathsResult";
            this.btnPathsResult.Size = new System.Drawing.Size(75, 23);
            this.btnPathsResult.TabIndex = 8;
            this.btnPathsResult.Text = "Result";
            this.btnPathsResult.UseVisualStyleBackColor = true;
            this.btnPathsResult.Click += new System.EventHandler(this.btnPathsResult_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Result";
            // 
            // txtPathsResult
            // 
            this.txtPathsResult.Location = new System.Drawing.Point(82, 78);
            this.txtPathsResult.Name = "txtPathsResult";
            this.txtPathsResult.ReadOnly = true;
            this.txtPathsResult.Size = new System.Drawing.Size(463, 20);
            this.txtPathsResult.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Relative To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Absolute";
            // 
            // btnPathsAbsolutePath
            // 
            this.btnPathsAbsolutePath.Location = new System.Drawing.Point(552, 20);
            this.btnPathsAbsolutePath.Name = "btnPathsAbsolutePath";
            this.btnPathsAbsolutePath.Size = new System.Drawing.Size(31, 23);
            this.btnPathsAbsolutePath.TabIndex = 3;
            this.btnPathsAbsolutePath.Text = "...";
            this.btnPathsAbsolutePath.UseVisualStyleBackColor = true;
            this.btnPathsAbsolutePath.Click += new System.EventHandler(this.btnPathsAbsolutePath_Click);
            // 
            // btnPathsRelativeTo
            // 
            this.btnPathsRelativeTo.Location = new System.Drawing.Point(552, 49);
            this.btnPathsRelativeTo.Name = "btnPathsRelativeTo";
            this.btnPathsRelativeTo.Size = new System.Drawing.Size(31, 23);
            this.btnPathsRelativeTo.TabIndex = 3;
            this.btnPathsRelativeTo.Text = "...";
            this.btnPathsRelativeTo.UseVisualStyleBackColor = true;
            this.btnPathsRelativeTo.Click += new System.EventHandler(this.btnPathsRelativeTo_Click);
            // 
            // tbxPathsAbsolutePath
            // 
            this.tbxPathsAbsolutePath.Location = new System.Drawing.Point(82, 22);
            this.tbxPathsAbsolutePath.Name = "tbxPathsAbsolutePath";
            this.tbxPathsAbsolutePath.Size = new System.Drawing.Size(463, 20);
            this.tbxPathsAbsolutePath.TabIndex = 1;
            // 
            // tbxPathsRelativeTo
            // 
            this.tbxPathsRelativeTo.Location = new System.Drawing.Point(82, 52);
            this.tbxPathsRelativeTo.Name = "tbxPathsRelativeTo";
            this.tbxPathsRelativeTo.Size = new System.Drawing.Size(463, 20);
            this.tbxPathsRelativeTo.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 810);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtDir);
            this.Controls.Add(this.btnDir);
            this.Name = "Form1";
            this.Text = "Rename";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbpBizTalkXml.ResumeLayout(false);
            this.tbpBizTalkXml.PerformLayout();
            this.tbpFileRename.ResumeLayout(false);
            this.tbpFileRename.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFRDetails)).EndInit();
            this.tbpFileModify.ResumeLayout(false);
            this.tbpRegularExpression.ResumeLayout(false);
            this.tbpRegularExpression.PerformLayout();
            this.tbpPaths.ResumeLayout(false);
            this.tbpPaths.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpBizTalkXml;
        private System.Windows.Forms.Button btnBizTalkXMLGo;
        private System.Windows.Forms.TabPage tbpFileRename;
        private System.Windows.Forms.CheckBox chkBiztalkXmlFormat;
        private System.Windows.Forms.CheckBox chkBiztalkXMLRename;
        private System.Windows.Forms.Button btnFRModify;
        private System.Windows.Forms.Button btnFRPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFRFileFormat;
        private System.Windows.Forms.TextBox txtFRFileFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFRStartingCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tbpFileModify;
        private System.Windows.Forms.Button btnFileModfiyRefresh;
        private System.Windows.Forms.ListBox lbxFileModify;
        private System.Windows.Forms.RadioButton rbtFileRenameRegExpression;
        private System.Windows.Forms.RadioButton rbtFRSimple;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFRRegExpression;
        private System.Windows.Forms.TabPage tbpRegularExpression;
        private System.Windows.Forms.RichTextBox rtbtbxREData;
        private System.Windows.Forms.TextBox tbxREExpression;
        private System.Windows.Forms.Button btnREFindMatch;
        private System.Windows.Forms.DataGridView dgvFRDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtColFROriginalPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtColFROriginalFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtColModifiedFileName;
        private System.Windows.Forms.TabPage tbpPaths;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPathsResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPathsAbsolutePath;
        private System.Windows.Forms.Button btnPathsRelativeTo;
        private System.Windows.Forms.TextBox tbxPathsAbsolutePath;
        private System.Windows.Forms.TextBox tbxPathsRelativeTo;
        private System.Windows.Forms.Button btnPathsResult;
    }
}

