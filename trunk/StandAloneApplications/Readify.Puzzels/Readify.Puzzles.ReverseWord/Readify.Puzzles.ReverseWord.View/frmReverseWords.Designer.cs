namespace Readify.Puzzles.ReverseWord.View
{
    partial class frmReverseWords
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
            this.btnReverseWord = new System.Windows.Forms.Button();
            this.txtReverseWords = new System.Windows.Forms.TextBox();
            this.txtReverseWordsResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnReverseWord
            // 
            this.btnReverseWord.Location = new System.Drawing.Point(12, 38);
            this.btnReverseWord.Name = "btnReverseWord";
            this.btnReverseWord.Size = new System.Drawing.Size(75, 23);
            this.btnReverseWord.TabIndex = 0;
            this.btnReverseWord.Text = "Reverse";
            this.btnReverseWord.UseVisualStyleBackColor = true;
            this.btnReverseWord.Click += new System.EventHandler(this.btnReverseWord_Click);
            // 
            // txtReverseWords
            // 
            this.txtReverseWords.Location = new System.Drawing.Point(12, 12);
            this.txtReverseWords.Name = "txtReverseWords";
            this.txtReverseWords.Size = new System.Drawing.Size(285, 20);
            this.txtReverseWords.TabIndex = 1;
            // 
            // txtReverseWordsResult
            // 
            this.txtReverseWordsResult.Location = new System.Drawing.Point(12, 71);
            this.txtReverseWordsResult.Name = "txtReverseWordsResult";
            this.txtReverseWordsResult.Size = new System.Drawing.Size(285, 20);
            this.txtReverseWordsResult.TabIndex = 2;
            // 
            // frmReverseWords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 102);
            this.Controls.Add(this.txtReverseWordsResult);
            this.Controls.Add(this.txtReverseWords);
            this.Controls.Add(this.btnReverseWord);
            this.Name = "frmReverseWords";
            this.Text = "frmReverseWords";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReverseWord;
        private System.Windows.Forms.TextBox txtReverseWords;
        private System.Windows.Forms.TextBox txtReverseWordsResult;
    }
}