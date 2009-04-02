namespace SandBox.Winform.WMI.Explorer
{
    partial class EventQueryCondition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label InputMessage;
        private System.Windows.Forms.TextBox TextBox;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ComboBox OperatorBox;
        
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
            this.TextBox = new System.Windows.Forms.TextBox();
            this.InputMessage = new System.Windows.Forms.Label();
            this.OKbutton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.OperatorBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(112, 64);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(152, 20);
            this.TextBox.TabIndex = 0;
            this.TextBox.Text = "";
            this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // InputMessage
            // 
            this.InputMessage.Location = new System.Drawing.Point(32, 16);
            this.InputMessage.Name = "InputMessage";
            this.InputMessage.Size = new System.Drawing.Size(240, 40);
            this.InputMessage.TabIndex = 1;
            this.InputMessage.Text = "";
            // 
            // OKbutton
            // 
            this.OKbutton.Location = new System.Drawing.Point(40, 104);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(96, 23);
            this.OKbutton.TabIndex = 2;
            this.OKbutton.Text = "OK";
            this.OKbutton.Click += new System.EventHandler(this.OKButton_Click);
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
            // OperatorBox
            // 
            this.OperatorBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.OperatorBox.Location = new System.Drawing.Point(32, 64);
            this.OperatorBox.Name = "OperatorBox";
            this.OperatorBox.Size = new System.Drawing.Size(56, 21);
            this.OperatorBox.TabIndex = 4;
            this.OperatorBox.Text = "=";
            // 
            // EventQueryCondition
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(296, 146);
            this.ControlBox = false;
            this.Controls.Add(this.OperatorBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.InputMessage);
            this.Controls.Add(this.TextBox);
            this.Name = "EventQueryCondition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter property value";
            this.ResumeLayout(false);

        }

        #endregion
    }
}