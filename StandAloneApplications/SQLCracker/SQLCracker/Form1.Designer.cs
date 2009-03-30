namespace SQLCracker
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
            this.RunBt = new System.Windows.Forms.Button();
            this.PassWordTxt = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SaltTxt = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.UpperHashTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ResultTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PassTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RunBt
            // 
            this.RunBt.Location = new System.Drawing.Point(631, 203);
            this.RunBt.Name = "RunBt";
            this.RunBt.Size = new System.Drawing.Size(75, 23);
            this.RunBt.TabIndex = 0;
            this.RunBt.Text = "Run";
            this.RunBt.UseVisualStyleBackColor = true;
            this.RunBt.Click += new System.EventHandler(this.RunBt_Click);
            // 
            // PassWordTxt
            // 
            this.PassWordTxt.Location = new System.Drawing.Point(83, 40);
            this.PassWordTxt.Name = "PassWordTxt";
            this.PassWordTxt.Size = new System.Drawing.Size(296, 20);
            this.PassWordTxt.TabIndex = 2;
            this.PassWordTxt.Text = "TEST";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(83, 66);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(629, 20);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "0x01005F6A417F93D281DB33291C4C1E00B307E95C8A6152FCDADA93D281DB33291C4C1E00B307E95" +
                "C8A6152FCDADA";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(83, 127);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(133, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "0x0100";
            // 
            // SaltTxt
            // 
            this.SaltTxt.Location = new System.Drawing.Point(83, 151);
            this.SaltTxt.Name = "SaltTxt";
            this.SaltTxt.Size = new System.Drawing.Size(133, 20);
            this.SaltTxt.TabIndex = 5;
            this.SaltTxt.Text = "5F6A417F";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(83, 175);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(287, 20);
            this.textBox5.TabIndex = 6;
            this.textBox5.Text = "93D281DB33291C4C1E00B307E95C8A6152FCDADA";
            // 
            // UpperHashTxt
            // 
            this.UpperHashTxt.Location = new System.Drawing.Point(83, 199);
            this.UpperHashTxt.Name = "UpperHashTxt";
            this.UpperHashTxt.Size = new System.Drawing.Size(287, 20);
            this.UpperHashTxt.TabIndex = 7;
            this.UpperHashTxt.Text = "93D281DB33291C4C1E00B307E95C8A6152FCDADA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Upper";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Lower";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Header";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Salt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Record";
            // 
            // ResultTxt
            // 
            this.ResultTxt.Location = new System.Drawing.Point(83, 259);
            this.ResultTxt.Name = "ResultTxt";
            this.ResultTxt.Size = new System.Drawing.Size(287, 20);
            this.ResultTxt.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Result";
            // 
            // PassTxt
            // 
            this.PassTxt.Location = new System.Drawing.Point(83, 298);
            this.PassTxt.Name = "PassTxt";
            this.PassTxt.Size = new System.Drawing.Size(100, 20);
            this.PassTxt.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 446);
            this.Controls.Add(this.PassTxt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ResultTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpperHashTxt);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.SaltTxt);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.PassWordTxt);
            this.Controls.Add(this.RunBt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RunBt;
        private System.Windows.Forms.TextBox PassWordTxt;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox SaltTxt;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox UpperHashTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ResultTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox PassTxt;
    }
}

