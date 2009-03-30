namespace _01_ADConnect
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ADUsernameTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ADIdTxt = new System.Windows.Forms.TextBox();
            this.ADLocalUserChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(430, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "username";
            // 
            // ADUsernameTxt
            // 
            this.ADUsernameTxt.Location = new System.Drawing.Point(98, 43);
            this.ADUsernameTxt.Name = "ADUsernameTxt";
            this.ADUsernameTxt.Size = new System.Drawing.Size(180, 20);
            this.ADUsernameTxt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "AD UserID";
            // 
            // ADIdTxt
            // 
            this.ADIdTxt.Location = new System.Drawing.Point(98, 70);
            this.ADIdTxt.Name = "ADIdTxt";
            this.ADIdTxt.ReadOnly = true;
            this.ADIdTxt.Size = new System.Drawing.Size(407, 20);
            this.ADIdTxt.TabIndex = 4;
            // 
            // ADLocalUserChk
            // 
            this.ADLocalUserChk.AutoSize = true;
            this.ADLocalUserChk.Location = new System.Drawing.Point(284, 43);
            this.ADLocalUserChk.Name = "ADLocalUserChk";
            this.ADLocalUserChk.Size = new System.Drawing.Size(99, 17);
            this.ADLocalUserChk.TabIndex = 5;
            this.ADLocalUserChk.Text = "Logged In User";
            this.ADLocalUserChk.UseVisualStyleBackColor = true;
            this.ADLocalUserChk.CheckedChanged += new System.EventHandler(this.ADLocalUserChk_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 266);
            this.Controls.Add(this.ADLocalUserChk);
            this.Controls.Add(this.ADIdTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ADUsernameTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ADUsernameTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ADIdTxt;
        private System.Windows.Forms.CheckBox ADLocalUserChk;
    }
}

