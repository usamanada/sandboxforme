using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VisualXPath
{
	/// <summary>
	/// Summary description for HelpForm.
	/// </summary>
	public class HelpForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HelpForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(424, 272);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "Note that this help file is continuously updated. \r\nTherefore, if you have any qu" +
				"estion which is not answered in this document, then please email at laghari78@ya" +
				"hoo.com.\r\n\r\nQ. Why the Use Selected XPath button gets disabled when I execute so" +
				"me query?\r\nA. Whenever you execute the query against a document, the parser proc" +
				"esses the query and only shows the result into the tree view. You should note he" +
				"re that only a fragment of the whole Xml document is shown instead of complete d" +
				"ocument. In the case, we can\'t generate Xml Queries dynamically or otherwise it\'" +
				"ll fail simply. Workaround: Execute \'/\' as XPath query to get the whole document" +
				" loaded again.\r\n\r\nQ. Please define how do I create my query in case there is a d" +
				"efault namespace?\r\nA. One thing should be cleared first that XPath doesn\'t suppo" +
				"rt default namespaces. It has to have some Prefix in order to execute Query over" +
				" any document containing namespaces. So, to overcome this, we added \'def\' as a p" +
				"refix for default namespaces. So for eg. you have any query which you should wor" +
				"k like this. Assuming that default namespace is defined as part of Xml document." +
				" \r\n\r\n/rdf:Rdf/current\r\n\r\nBut, it fails because the \'current\' element of xml foll" +
				"ows the default namespace. So, XPath needed a prefix to resolve any queries rega" +
				"rding Namespaces. Workaround: As we stated earlier, we defined \'def\' as the pref" +
				"ix for Default Namespace. Therefore, the above query will work if the current el" +
				"ement is prefixed with \'def\'.\r\n\r\n/rdf:Rdf/def:current\r\n\r\nYou can generate XPath " +
				"automatically by selecting any node with your mouse.\r\n\r\nQ. How can I generate qu" +
				"eries for XML Attributes?\r\nRight-Click on the Node on which your required attrib" +
				"ute is present. The context menu will show you all the attributes present in the" +
				" Node. Just select the node to generate the XPath Query.";
			// 
			// HelpForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1});
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HelpForm";
			this.ShowInTaskbar = false;
			this.Text = "Help";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
