namespace Sandbox.Winform.DataGridBinding
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDataTableBind = new System.Windows.Forms.Button();
            this.btnCustomBindingList = new System.Windows.Forms.Button();
            this.btnRemoveUsingNull = new System.Windows.Forms.Button();
            this.btnRemoveNormal = new System.Windows.Forms.Button();
            this.btnCustomListBind = new System.Windows.Forms.Button();
            this.btnMultiLine = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(323, 353);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnDataTableBind
            // 
            this.btnDataTableBind.Location = new System.Drawing.Point(359, 40);
            this.btnDataTableBind.Name = "btnDataTableBind";
            this.btnDataTableBind.Size = new System.Drawing.Size(155, 23);
            this.btnDataTableBind.TabIndex = 1;
            this.btnDataTableBind.Text = "DataTable Link";
            this.btnDataTableBind.UseVisualStyleBackColor = true;
            this.btnDataTableBind.Click += new System.EventHandler(this.btnDataTableBind_Click);
            // 
            // btnCustomBindingList
            // 
            this.btnCustomBindingList.Location = new System.Drawing.Point(359, 156);
            this.btnCustomBindingList.Name = "btnCustomBindingList";
            this.btnCustomBindingList.Size = new System.Drawing.Size(155, 23);
            this.btnCustomBindingList.TabIndex = 2;
            this.btnCustomBindingList.Text = "Custom Binding List Bind";
            this.btnCustomBindingList.UseVisualStyleBackColor = true;
            this.btnCustomBindingList.Click += new System.EventHandler(this.btnCustomBindingList_Click);
            // 
            // btnRemoveUsingNull
            // 
            this.btnRemoveUsingNull.Location = new System.Drawing.Point(359, 112);
            this.btnRemoveUsingNull.Name = "btnRemoveUsingNull";
            this.btnRemoveUsingNull.Size = new System.Drawing.Size(155, 23);
            this.btnRemoveUsingNull.TabIndex = 3;
            this.btnRemoveUsingNull.Text = "Remove using null";
            this.btnRemoveUsingNull.UseVisualStyleBackColor = true;
            this.btnRemoveUsingNull.Click += new System.EventHandler(this.btnRemoveUsingNull_Click);
            // 
            // btnRemoveNormal
            // 
            this.btnRemoveNormal.Location = new System.Drawing.Point(359, 185);
            this.btnRemoveNormal.Name = "btnRemoveNormal";
            this.btnRemoveNormal.Size = new System.Drawing.Size(155, 23);
            this.btnRemoveNormal.TabIndex = 4;
            this.btnRemoveNormal.Text = "Remove normal";
            this.btnRemoveNormal.UseVisualStyleBackColor = true;
            this.btnRemoveNormal.Click += new System.EventHandler(this.btnRemoveNormal_Click);
            // 
            // btnCustomListBind
            // 
            this.btnCustomListBind.Location = new System.Drawing.Point(359, 83);
            this.btnCustomListBind.Name = "btnCustomListBind";
            this.btnCustomListBind.Size = new System.Drawing.Size(155, 23);
            this.btnCustomListBind.TabIndex = 5;
            this.btnCustomListBind.Text = "Custom List Bind";
            this.btnCustomListBind.UseVisualStyleBackColor = true;
            this.btnCustomListBind.Click += new System.EventHandler(this.btnCustomListBind_Click);
            // 
            // btnMultiLine
            // 
            this.btnMultiLine.Location = new System.Drawing.Point(359, 234);
            this.btnMultiLine.Name = "btnMultiLine";
            this.btnMultiLine.Size = new System.Drawing.Size(155, 23);
            this.btnMultiLine.TabIndex = 6;
            this.btnMultiLine.Text = "Multi Line";
            this.btnMultiLine.UseVisualStyleBackColor = true;
            this.btnMultiLine.Click += new System.EventHandler(this.btnMultiLine_Click);
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 377);
            this.Controls.Add(this.btnMultiLine);
            this.Controls.Add(this.btnCustomListBind);
            this.Controls.Add(this.btnRemoveNormal);
            this.Controls.Add(this.btnRemoveUsingNull);
            this.Controls.Add(this.btnCustomBindingList);
            this.Controls.Add(this.btnDataTableBind);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDataTableBind;
        private System.Windows.Forms.Button btnCustomBindingList;
        private System.Windows.Forms.Button btnRemoveUsingNull;
        private System.Windows.Forms.Button btnRemoveNormal;
        private System.Windows.Forms.Button btnCustomListBind;
        private System.Windows.Forms.Button btnMultiLine;

    }
}

