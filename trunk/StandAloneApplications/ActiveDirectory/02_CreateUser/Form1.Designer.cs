namespace _02_CreateUser
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
            this.ADCreateBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ADDeleteBtn = new System.Windows.Forms.Button();
            this.ADEnbableUserBtn = new System.Windows.Forms.Button();
            this.ADDisableUserBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ADCreateBtn
            // 
            this.ADCreateBtn.Location = new System.Drawing.Point(286, 77);
            this.ADCreateBtn.Name = "ADCreateBtn";
            this.ADCreateBtn.Size = new System.Drawing.Size(75, 23);
            this.ADCreateBtn.TabIndex = 0;
            this.ADCreateBtn.Text = "Create";
            this.ADCreateBtn.UseVisualStyleBackColor = true;
            this.ADCreateBtn.Click += new System.EventHandler(this.ADCreateBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(88, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(88, 80);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(192, 20);
            this.textBox2.TabIndex = 4;
            // 
            // ADDeleteBtn
            // 
            this.ADDeleteBtn.Location = new System.Drawing.Point(286, 166);
            this.ADDeleteBtn.Name = "ADDeleteBtn";
            this.ADDeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.ADDeleteBtn.TabIndex = 5;
            this.ADDeleteBtn.Text = "Delete";
            this.ADDeleteBtn.UseVisualStyleBackColor = true;
            this.ADDeleteBtn.Click += new System.EventHandler(this.ADDeleteBtn_Click);
            // 
            // ADEnbableUserBtn
            // 
            this.ADEnbableUserBtn.Location = new System.Drawing.Point(286, 107);
            this.ADEnbableUserBtn.Name = "ADEnbableUserBtn";
            this.ADEnbableUserBtn.Size = new System.Drawing.Size(75, 23);
            this.ADEnbableUserBtn.TabIndex = 6;
            this.ADEnbableUserBtn.Text = "Enable User";
            this.ADEnbableUserBtn.UseVisualStyleBackColor = true;
            this.ADEnbableUserBtn.Click += new System.EventHandler(this.ADEnbableUserBtn_Click);
            // 
            // ADDisableUserBtn
            // 
            this.ADDisableUserBtn.Location = new System.Drawing.Point(286, 137);
            this.ADDisableUserBtn.Name = "ADDisableUserBtn";
            this.ADDisableUserBtn.Size = new System.Drawing.Size(75, 23);
            this.ADDisableUserBtn.TabIndex = 7;
            this.ADDisableUserBtn.Text = "Disable User";
            this.ADDisableUserBtn.UseVisualStyleBackColor = true;
            this.ADDisableUserBtn.Click += new System.EventHandler(this.ADDisableUserBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 266);
            this.Controls.Add(this.ADDisableUserBtn);
            this.Controls.Add(this.ADEnbableUserBtn);
            this.Controls.Add(this.ADDeleteBtn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ADCreateBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ADCreateBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button ADDeleteBtn;
        private System.Windows.Forms.Button ADEnbableUserBtn;
        private System.Windows.Forms.Button ADDisableUserBtn;
    }
}

