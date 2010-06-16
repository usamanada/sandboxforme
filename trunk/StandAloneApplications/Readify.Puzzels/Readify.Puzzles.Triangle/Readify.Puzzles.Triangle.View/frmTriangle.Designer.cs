namespace Readify.Puzzles.Triangle.View
{
    partial class frmTriangle
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
            this.lblSideA = new System.Windows.Forms.Label();
            this.txtSideA = new System.Windows.Forms.TextBox();
            this.txtSideB = new System.Windows.Forms.TextBox();
            this.lblSideB = new System.Windows.Forms.Label();
            this.txtSideC = new System.Windows.Forms.TextBox();
            this.lblSideC = new System.Windows.Forms.Label();
            this.btnCheckTriangleType = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSideA
            // 
            this.lblSideA.AutoSize = true;
            this.lblSideA.Location = new System.Drawing.Point(12, 9);
            this.lblSideA.Name = "lblSideA";
            this.lblSideA.Size = new System.Drawing.Size(38, 13);
            this.lblSideA.TabIndex = 0;
            this.lblSideA.Text = "Side A";
            // 
            // txtSideA
            // 
            this.txtSideA.Location = new System.Drawing.Point(53, 6);
            this.txtSideA.Name = "txtSideA";
            this.txtSideA.Size = new System.Drawing.Size(100, 20);
            this.txtSideA.TabIndex = 1;
            // 
            // txtSideB
            // 
            this.txtSideB.Location = new System.Drawing.Point(53, 32);
            this.txtSideB.Name = "txtSideB";
            this.txtSideB.Size = new System.Drawing.Size(100, 20);
            this.txtSideB.TabIndex = 3;
            // 
            // lblSideB
            // 
            this.lblSideB.AutoSize = true;
            this.lblSideB.Location = new System.Drawing.Point(12, 35);
            this.lblSideB.Name = "lblSideB";
            this.lblSideB.Size = new System.Drawing.Size(38, 13);
            this.lblSideB.TabIndex = 2;
            this.lblSideB.Text = "Side B";
            // 
            // txtSideC
            // 
            this.txtSideC.Location = new System.Drawing.Point(53, 58);
            this.txtSideC.Name = "txtSideC";
            this.txtSideC.Size = new System.Drawing.Size(100, 20);
            this.txtSideC.TabIndex = 5;
            // 
            // lblSideC
            // 
            this.lblSideC.AutoSize = true;
            this.lblSideC.Location = new System.Drawing.Point(12, 61);
            this.lblSideC.Name = "lblSideC";
            this.lblSideC.Size = new System.Drawing.Size(38, 13);
            this.lblSideC.TabIndex = 4;
            this.lblSideC.Text = "Side C";
            // 
            // btnCheckTriangleType
            // 
            this.btnCheckTriangleType.Location = new System.Drawing.Point(53, 85);
            this.btnCheckTriangleType.Name = "btnCheckTriangleType";
            this.btnCheckTriangleType.Size = new System.Drawing.Size(75, 23);
            this.btnCheckTriangleType.TabIndex = 6;
            this.btnCheckTriangleType.Text = "Check";
            this.btnCheckTriangleType.UseVisualStyleBackColor = true;
            this.btnCheckTriangleType.Click += new System.EventHandler(this.btnCheckTriangleType_Click);
            // 
            // frmTriangle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 140);
            this.Controls.Add(this.btnCheckTriangleType);
            this.Controls.Add(this.txtSideC);
            this.Controls.Add(this.lblSideC);
            this.Controls.Add(this.txtSideB);
            this.Controls.Add(this.lblSideB);
            this.Controls.Add(this.txtSideA);
            this.Controls.Add(this.lblSideA);
            this.Name = "frmTriangle";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSideA;
        private System.Windows.Forms.TextBox txtSideA;
        private System.Windows.Forms.TextBox txtSideB;
        private System.Windows.Forms.Label lblSideB;
        private System.Windows.Forms.TextBox txtSideC;
        private System.Windows.Forms.Label lblSideC;
        private System.Windows.Forms.Button btnCheckTriangleType;
    }
}

