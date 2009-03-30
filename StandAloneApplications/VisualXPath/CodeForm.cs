using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VisualXPath
{
	/// <summary>
	/// Summary description for CodeForm.
	/// </summary>
	public class CodeForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CodeForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		public void ShowCode(string code)
		{
			txtCode.Text = code;
			Show();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtCode = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtCode
			// 
			this.txtCode.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtCode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtCode.Size = new System.Drawing.Size(576, 344);
			this.txtCode.TabIndex = 0;
			this.txtCode.Text = "";
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnCopy.Location = new System.Drawing.Point(496, 348);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.TabIndex = 1;
			this.btnCopy.Text = "&Copy";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 352);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(472, 23);
			this.label1.TabIndex = 2;
			// 
			// CodeForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 373);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1,
																		  this.btnCopy,
																		  this.txtCode});
			this.MaximizeBox = false;
			this.Name = "CodeForm";
			this.Text = "CodeForm";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			if (txtCode.Text != "")
			{
				// copy the code to clipboard
				txtCode.SelectAll();
				txtCode.Copy();

				label1.Text = "Code is copied to the clipboard";
			}
		}
	}
}
