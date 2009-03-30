namespace Graysonline.Winform
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbComputers = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.rtbComputerResult = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbUrl = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSearchPage = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtTest = new System.Windows.Forms.TextBox();
            this.rtbTest = new System.Windows.Forms.RichTextBox();
            this.txtTestSearch = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tbComputers.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(789, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Auto";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 22);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(771, 20);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "http://www.Graysonline.Winform.com.au";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tbComputers);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 241);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(986, 295);
            this.tabControl1.TabIndex = 3;
            // 
            // tbComputers
            // 
            this.tbComputers.Controls.Add(this.richTextBox1);
            this.tbComputers.Controls.Add(this.rtbComputerResult);
            this.tbComputers.Location = new System.Drawing.Point(4, 22);
            this.tbComputers.Name = "tbComputers";
            this.tbComputers.Padding = new System.Windows.Forms.Padding(3);
            this.tbComputers.Size = new System.Drawing.Size(978, 269);
            this.tbComputers.TabIndex = 0;
            this.tbComputers.Text = "Computers";
            this.tbComputers.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(9, 132);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(939, 131);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // rtbComputerResult
            // 
            this.rtbComputerResult.Location = new System.Drawing.Point(9, 7);
            this.rtbComputerResult.Name = "rtbComputerResult";
            this.rtbComputerResult.Size = new System.Drawing.Size(939, 118);
            this.rtbComputerResult.TabIndex = 1;
            this.rtbComputerResult.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtTestSearch);
            this.tabPage2.Controls.Add(this.rtbTest);
            this.tabPage2.Controls.Add(this.txtTest);
            this.tabPage2.Controls.Add(this.btnTest);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(978, 269);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtbUrl
            // 
            this.rtbUrl.Location = new System.Drawing.Point(13, 74);
            this.rtbUrl.Name = "rtbUrl";
            this.rtbUrl.ReadOnly = true;
            this.rtbUrl.Size = new System.Drawing.Size(770, 114);
            this.rtbUrl.TabIndex = 4;
            this.rtbUrl.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Search Page";
            // 
            // tbxSearchPage
            // 
            this.tbxSearchPage.Location = new System.Drawing.Point(87, 48);
            this.tbxSearchPage.Name = "tbxSearchPage";
            this.tbxSearchPage.Size = new System.Drawing.Size(217, 20);
            this.tbxSearchPage.TabIndex = 7;
            this.tbxSearchPage.Text = "Computers & IT";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(9, 18);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtTest
            // 
            this.txtTest.Location = new System.Drawing.Point(91, 45);
            this.txtTest.Multiline = true;
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(618, 50);
            this.txtTest.TabIndex = 1;
            // 
            // rtbTest
            // 
            this.rtbTest.Location = new System.Drawing.Point(9, 114);
            this.rtbTest.Name = "rtbTest";
            this.rtbTest.Size = new System.Drawing.Size(699, 133);
            this.rtbTest.TabIndex = 2;
            this.rtbTest.Text = "";
            // 
            // txtTestSearch
            // 
            this.txtTestSearch.Location = new System.Drawing.Point(91, 19);
            this.txtTestSearch.Name = "txtTestSearch";
            this.txtTestSearch.Size = new System.Drawing.Size(617, 20);
            this.txtTestSearch.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(105, 214);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(678, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "http://www.Graysonline.Winform.com.au/catalogue.asp?SALE_ID=135001&SALE_TYPE=THUMB";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 536);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxSearchPage);
            this.Controls.Add(this.rtbUrl);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnSearch);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tbComputers.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbComputers;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtbComputerResult;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox rtbUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSearchPage;
        private System.Windows.Forms.TextBox txtTest;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.RichTextBox rtbTest;
        private System.Windows.Forms.TextBox txtTestSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

