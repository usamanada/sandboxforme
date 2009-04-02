using System;
namespace SandBox.Winform.WMI.Explorer
{
    partial class InParameterWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label InputMessage;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CloseButton;

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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.InputMessage = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.TopMost = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "";
            this.textBox1.TextChanged += new EventHandler(TextBox_TextChanged);
            // 
            // InputMessage
            // 
            this.InputMessage.Location = new System.Drawing.Point(32, 16);
            this.InputMessage.Name = "InputMessage";
            this.InputMessage.Size = new System.Drawing.Size(224, 40);
            this.InputMessage.TabIndex = 1;
            this.InputMessage.Text = "";

            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(40, 104);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(96, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(152, 104);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(96, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // InParameterWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 146);
            this.ControlBox = false;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.InputMessage);
            this.Controls.Add(this.textBox1);
            this.Name = "InParameterWindow";
            this.Text = "Enter in-parameter";
            this.ResumeLayout(false);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        #endregion
    }
}