namespace SandBox.Winform.WMI.Explorer
{
    partial class SplashScreenForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label statusLabel;
        static System.Windows.Forms.ProgressBar progressBar1;


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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusLabel = new System.Windows.Forms.Label();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(24, 32);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(232, 72);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Initializing the WMI Code Creator. Loading WMI classes...";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(24, 115);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(232, 23);
            progressBar1.TabIndex = 1;
            // 
            // SplashScreenForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 168);
            this.Controls.Add(progressBar1);
            this.Controls.Add(this.statusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SplashScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WMI Code Creator";
            this.ResumeLayout(false);
        }

        #endregion
    }
}