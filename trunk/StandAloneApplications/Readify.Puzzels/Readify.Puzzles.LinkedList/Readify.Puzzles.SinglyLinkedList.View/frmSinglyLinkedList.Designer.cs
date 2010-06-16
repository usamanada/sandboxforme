namespace Readify.Puzzles.SinglyLinkedList.View
{
    partial class frmSinglyLinkedList
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
            this.btnAddToList = new System.Windows.Forms.Button();
            this.txtAddToList = new System.Windows.Forms.TextBox();
            this.btnFindFromTails = new System.Windows.Forms.Button();
            this.txtFindFromTail = new System.Windows.Forms.TextBox();
            this.gbxFindTail = new System.Windows.Forms.GroupBox();
            this.gbxAddToList = new System.Windows.Forms.GroupBox();
            this.gbxFindTail.SuspendLayout();
            this.gbxAddToList.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddToList
            // 
            this.btnAddToList.Location = new System.Drawing.Point(6, 19);
            this.btnAddToList.Name = "btnAddToList";
            this.btnAddToList.Size = new System.Drawing.Size(68, 23);
            this.btnAddToList.TabIndex = 0;
            this.btnAddToList.Text = "Add";
            this.btnAddToList.UseVisualStyleBackColor = true;
            this.btnAddToList.Click += new System.EventHandler(this.btnAddToList_Click);
            // 
            // txtAddToList
            // 
            this.txtAddToList.Location = new System.Drawing.Point(80, 21);
            this.txtAddToList.Name = "txtAddToList";
            this.txtAddToList.Size = new System.Drawing.Size(117, 20);
            this.txtAddToList.TabIndex = 1;
            // 
            // btnFindFromTails
            // 
            this.btnFindFromTails.Location = new System.Drawing.Point(6, 19);
            this.btnFindFromTails.Name = "btnFindFromTails";
            this.btnFindFromTails.Size = new System.Drawing.Size(68, 23);
            this.btnFindFromTails.TabIndex = 2;
            this.btnFindFromTails.Text = "Find";
            this.btnFindFromTails.UseVisualStyleBackColor = true;
            this.btnFindFromTails.Click += new System.EventHandler(this.btnFindFromTails_Click);
            // 
            // txtFindFromTail
            // 
            this.txtFindFromTail.Location = new System.Drawing.Point(80, 21);
            this.txtFindFromTail.Name = "txtFindFromTail";
            this.txtFindFromTail.Size = new System.Drawing.Size(123, 20);
            this.txtFindFromTail.TabIndex = 3;
            // 
            // gbxFindTail
            // 
            this.gbxFindTail.Controls.Add(this.btnFindFromTails);
            this.gbxFindTail.Controls.Add(this.txtFindFromTail);
            this.gbxFindTail.Location = new System.Drawing.Point(13, 60);
            this.gbxFindTail.Name = "gbxFindTail";
            this.gbxFindTail.Size = new System.Drawing.Size(214, 51);
            this.gbxFindTail.TabIndex = 1;
            this.gbxFindTail.TabStop = false;
            this.gbxFindTail.Text = "Frind From Tail";
            // 
            // gbxAddToList
            // 
            this.gbxAddToList.Controls.Add(this.btnAddToList);
            this.gbxAddToList.Controls.Add(this.txtAddToList);
            this.gbxAddToList.Location = new System.Drawing.Point(13, 3);
            this.gbxAddToList.Name = "gbxAddToList";
            this.gbxAddToList.Size = new System.Drawing.Size(214, 51);
            this.gbxAddToList.TabIndex = 0;
            this.gbxAddToList.TabStop = false;
            this.gbxAddToList.Text = "Add To Singly Linked List";
            // 
            // frmPuzzlesinglyLinkedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 123);
            this.Controls.Add(this.gbxAddToList);
            this.Controls.Add(this.gbxFindTail);
            this.Name = "frmPuzzlesinglyLinkedList";
            this.Text = "Puzzle Singly Linked List";
            this.gbxFindTail.ResumeLayout(false);
            this.gbxFindTail.PerformLayout();
            this.gbxAddToList.ResumeLayout(false);
            this.gbxAddToList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddToList;
        private System.Windows.Forms.TextBox txtAddToList;
        private System.Windows.Forms.Button btnFindFromTails;
        private System.Windows.Forms.TextBox txtFindFromTail;
        private System.Windows.Forms.GroupBox gbxFindTail;
        private System.Windows.Forms.GroupBox gbxAddToList;
    }
}

