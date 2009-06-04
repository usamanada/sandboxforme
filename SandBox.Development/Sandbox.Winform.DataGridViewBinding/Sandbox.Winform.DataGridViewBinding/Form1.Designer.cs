namespace Sandbox.Winform.DataGridViewBinding
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(323, 353);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnDataTableBind
            // 
            this.btnDataTableBind.Location = new System.Drawing.Point(374, 50);
            this.btnDataTableBind.Name = "btnDataTableBind";
            this.btnDataTableBind.Size = new System.Drawing.Size(155, 23);
            this.btnDataTableBind.TabIndex = 1;
            this.btnDataTableBind.Text = "DataTable Link";
            this.btnDataTableBind.UseVisualStyleBackColor = true;
            this.btnDataTableBind.Click += new System.EventHandler(this.btnDataTableBind_Click);
            // 
            // btnCustomBindingList
            // 
            this.btnCustomBindingList.Location = new System.Drawing.Point(374, 166);
            this.btnCustomBindingList.Name = "btnCustomBindingList";
            this.btnCustomBindingList.Size = new System.Drawing.Size(155, 23);
            this.btnCustomBindingList.TabIndex = 2;
            this.btnCustomBindingList.Text = "Custom Binding List Bind";
            this.btnCustomBindingList.UseVisualStyleBackColor = true;
            this.btnCustomBindingList.Click += new System.EventHandler(this.btnCustomBindingList_Click);
            // 
            // btnRemoveUsingNull
            // 
            this.btnRemoveUsingNull.Location = new System.Drawing.Point(374, 122);
            this.btnRemoveUsingNull.Name = "btnRemoveUsingNull";
            this.btnRemoveUsingNull.Size = new System.Drawing.Size(155, 23);
            this.btnRemoveUsingNull.TabIndex = 3;
            this.btnRemoveUsingNull.Text = "Remove using null";
            this.btnRemoveUsingNull.UseVisualStyleBackColor = true;
            this.btnRemoveUsingNull.Click += new System.EventHandler(this.btnRemoveUsingNull_Click);
            // 
            // btnRemoveNormal
            // 
            this.btnRemoveNormal.Location = new System.Drawing.Point(374, 195);
            this.btnRemoveNormal.Name = "btnRemoveNormal";
            this.btnRemoveNormal.Size = new System.Drawing.Size(155, 23);
            this.btnRemoveNormal.TabIndex = 4;
            this.btnRemoveNormal.Text = "Remove normal";
            this.btnRemoveNormal.UseVisualStyleBackColor = true;
            this.btnRemoveNormal.Click += new System.EventHandler(this.btnRemoveNormal_Click);
            // 
            // btnCustomListBind
            // 
            this.btnCustomListBind.Location = new System.Drawing.Point(374, 93);
            this.btnCustomListBind.Name = "btnCustomListBind";
            this.btnCustomListBind.Size = new System.Drawing.Size(155, 23);
            this.btnCustomListBind.TabIndex = 5;
            this.btnCustomListBind.Text = "Custom List Bind";
            this.btnCustomListBind.UseVisualStyleBackColor = true;
            this.btnCustomListBind.Click += new System.EventHandler(this.btnCustomListBind_Click);
            // 
            // btnMultiLine
            // 
            this.btnMultiLine.Location = new System.Drawing.Point(374, 244);
            this.btnMultiLine.Name = "btnMultiLine";
            this.btnMultiLine.Size = new System.Drawing.Size(155, 23);
            this.btnMultiLine.TabIndex = 6;
            this.btnMultiLine.Text = "Multi Line";
            this.btnMultiLine.UseVisualStyleBackColor = true;
            this.btnMultiLine.Click += new System.EventHandler(this.btnMultiLine_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 491);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.btnMultiLine);
            this.tabPage1.Controls.Add(this.btnDataTableBind);
            this.tabPage1.Controls.Add(this.btnCustomListBind);
            this.tabPage1.Controls.Add(this.btnCustomBindingList);
            this.tabPage1.Controls.Add(this.btnRemoveNormal);
            this.tabPage1.Controls.Add(this.btnRemoveUsingNull);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(698, 465);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(698, 465);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(23, 23);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(596, 150);
            this.dataGridView2.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(209, 188);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(128, 188);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 491);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView2;

    }
}

