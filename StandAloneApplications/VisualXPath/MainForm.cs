using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Text;
using PerformanceDemo;
using System.Text.RegularExpressions;
using System.Xml.Xsl;
using System.Net;
using System.Diagnostics;

namespace VisualXPath
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtXmlFileName;
		private System.Windows.Forms.Button btnLoadXmlFile;
		private System.Windows.Forms.TextBox txtQuery;
		private System.Windows.Forms.Button btnExecuteQuery;

		// custom variable
		private XmlDocument _xmlDoc = new XmlDocument();
		private System.Windows.Forms.TreeView _treeResult;
		private TreeNode _selectedNode;
		private string _xmlFileName;
		private int _level;
		string _xPathExp;
		string _nsPrefix;
		string _nsValue;
		bool _hasNS;
		bool _hasDefNS;
		string _nsDefPrefix;
		XmlNamespaceManager _nsMgr;
		string _strNamespaces;
		bool _hasSiblings;
		string _xmlString = "<?xml version=\"1.0\"?>";
		Hashtable _attributes = new Hashtable(); // hashtable for attributes collection
		bool isContextNode = false; // this variable is used to keep track of the xml document. It is true
		// when the xpath query is executed successfully and the treeview is showing the child xml document.
		// we are using this variable to stop generating dynamic xpath queries in treeview_mousedown event

		bool isVerbose = false;

		BooleanOp boolOperation = new BooleanOp();
		
		private System.Windows.Forms.OpenFileDialog openXmlFile;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.Button btnSelXPath;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button btnShowNamespaces;
		private System.Windows.Forms.Button btnShowHelp;
		private System.Windows.Forms.Button btnCSCode;
		private System.Windows.Forms.Button btnNotepad;
		private System.Windows.Forms.ContextMenu ctxAttr;
		private System.Windows.Forms.Button btnOpenFile;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuOpen;
		private System.Windows.Forms.MenuItem mnuLoad;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem mnuNotepad;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem mnuGenCode;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem mnuVerbose;
		private System.Windows.Forms.MenuItem mnuAbbreviate;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuShowHelp;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_xmlFileName = txtXmlFileName.Text;

			// set the verbose menu item to uncheck
			mnuVerbose.RadioCheck = true;
			mnuAbbreviate.RadioCheck = true;

			mnuAbbreviate.Checked = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtXmlFileName = new System.Windows.Forms.TextBox();
			this.btnLoadXmlFile = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtQuery = new System.Windows.Forms.TextBox();
			this.btnExecuteQuery = new System.Windows.Forms.Button();
			this._treeResult = new System.Windows.Forms.TreeView();
			this.ctxAttr = new System.Windows.Forms.ContextMenu();
			this.openXmlFile = new System.Windows.Forms.OpenFileDialog();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.btnSelXPath = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btnShowNamespaces = new System.Windows.Forms.Button();
			this.btnShowHelp = new System.Windows.Forms.Button();
			this.btnCSCode = new System.Windows.Forms.Button();
			this.btnNotepad = new System.Windows.Forms.Button();
			this.btnOpenFile = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuOpen = new System.Windows.Forms.MenuItem();
			this.mnuLoad = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mnuNotepad = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.mnuGenCode = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.mnuExit = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.mnuVerbose = new System.Windows.Forms.MenuItem();
			this.mnuAbbreviate = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.mnuShowHelp = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Xml File:";
			// 
			// txtXmlFileName
			// 
			this.txtXmlFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtXmlFileName.Location = new System.Drawing.Point(82, 9);
			this.txtXmlFileName.Name = "txtXmlFileName";
			this.txtXmlFileName.Size = new System.Drawing.Size(516, 28);
			this.txtXmlFileName.TabIndex = 1;
			this.txtXmlFileName.Text = "";
			// 
			// btnLoadXmlFile
			// 
			this.btnLoadXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadXmlFile.Location = new System.Drawing.Point(617, 9);
			this.btnLoadXmlFile.Name = "btnLoadXmlFile";
			this.btnLoadXmlFile.Size = new System.Drawing.Size(82, 25);
			this.btnLoadXmlFile.TabIndex = 2;
			this.btnLoadXmlFile.Text = "&Load";
			this.btnLoadXmlFile.Click += new System.EventHandler(this.btnLoadXmlFile_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(0, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "Enter XPath Query:";
			// 
			// txtQuery
			// 
			this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtQuery.Location = new System.Drawing.Point(155, 44);
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.Size = new System.Drawing.Size(544, 28);
			this.txtQuery.TabIndex = 4;
			this.txtQuery.Text = "";
			// 
			// btnExecuteQuery
			// 
			this.btnExecuteQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExecuteQuery.Location = new System.Drawing.Point(708, 44);
			this.btnExecuteQuery.Name = "btnExecuteQuery";
			this.btnExecuteQuery.Size = new System.Drawing.Size(91, 26);
			this.btnExecuteQuery.TabIndex = 5;
			this.btnExecuteQuery.Text = "&Execute";
			this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
			// 
			// _treeResult
			// 
			this._treeResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._treeResult.ContextMenu = this.ctxAttr;
			this._treeResult.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._treeResult.ForeColor = System.Drawing.SystemColors.WindowText;
			this._treeResult.FullRowSelect = true;
			this._treeResult.HideSelection = false;
			this._treeResult.ImageIndex = -1;
			this._treeResult.Location = new System.Drawing.Point(9, 80);
			this._treeResult.Name = "_treeResult";
			this._treeResult.SelectedImageIndex = -1;
			this._treeResult.Size = new System.Drawing.Size(690, 247);
			this._treeResult.TabIndex = 6;
			this._treeResult.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeResult_MouseDown);
			this._treeResult.KeyUp += new System.Windows.Forms.KeyEventHandler(this._treeResult_KeyUp);
			// 
			// ctxAttr
			// 
			this.ctxAttr.Popup += new System.EventHandler(this.ctxAttr_Popup);
			// 
			// openXmlFile
			// 
			this.openXmlFile.Filter = "XML files|*.xml|All files|*.*";
			this.openXmlFile.InitialDirectory = "c:\\";
			this.openXmlFile.Title = "Open XML File";
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 362);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(808, 24);
			this.statusBar.TabIndex = 8;
			this.statusBar.Text = "Ready";
			// 
			// btnSelXPath
			// 
			this.btnSelXPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelXPath.Enabled = false;
			this.btnSelXPath.Location = new System.Drawing.Point(626, 336);
			this.btnSelXPath.Name = "btnSelXPath";
			this.btnSelXPath.Size = new System.Drawing.Size(173, 25);
			this.btnSelXPath.TabIndex = 9;
			this.btnSelXPath.Text = "&Use Selected XPath";
			this.btnSelXPath.Click += new System.EventHandler(this.btnSelXPath_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox1.Location = new System.Drawing.Point(86, 336);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(211, 27);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "Expand All/Collapse All";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// btnShowNamespaces
			// 
			this.btnShowNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnShowNamespaces.Location = new System.Drawing.Point(443, 336);
			this.btnShowNamespaces.Name = "btnShowNamespaces";
			this.btnShowNamespaces.Size = new System.Drawing.Size(174, 25);
			this.btnShowNamespaces.TabIndex = 11;
			this.btnShowNamespaces.Text = "&Show Namespaces";
			this.btnShowNamespaces.Click += new System.EventHandler(this.btnShowNamespaces_Click);
			// 
			// btnShowHelp
			// 
			this.btnShowHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnShowHelp.Location = new System.Drawing.Point(9, 336);
			this.btnShowHelp.Name = "btnShowHelp";
			this.btnShowHelp.Size = new System.Drawing.Size(28, 25);
			this.btnShowHelp.TabIndex = 12;
			this.btnShowHelp.Text = "?";
			this.btnShowHelp.Click += new System.EventHandler(this.btnShowHelp_Click);
			// 
			// btnCSCode
			// 
			this.btnCSCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCSCode.Location = new System.Drawing.Point(708, 301);
			this.btnCSCode.Name = "btnCSCode";
			this.btnCSCode.Size = new System.Drawing.Size(91, 25);
			this.btnCSCode.TabIndex = 13;
			this.btnCSCode.Text = "&C# Code";
			this.btnCSCode.Click += new System.EventHandler(this.btnCSCode_Click);
			// 
			// btnNotepad
			// 
			this.btnNotepad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNotepad.Location = new System.Drawing.Point(708, 80);
			this.btnNotepad.Name = "btnNotepad";
			this.btnNotepad.Size = new System.Drawing.Size(91, 25);
			this.btnNotepad.TabIndex = 14;
			this.btnNotepad.Text = "&Notepad";
			this.btnNotepad.Click += new System.EventHandler(this.btnNotepad_Click);
			// 
			// btnOpenFile
			// 
			this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenFile.Location = new System.Drawing.Point(708, 9);
			this.btnOpenFile.Name = "btnOpenFile";
			this.btnOpenFile.Size = new System.Drawing.Size(91, 25);
			this.btnOpenFile.TabIndex = 15;
			this.btnOpenFile.Text = "&Open File...";
			this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem10,
																					  this.mnuHelp});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuOpen,
																					  this.mnuLoad,
																					  this.menuItem4,
																					  this.mnuNotepad,
																					  this.menuItem6,
																					  this.mnuGenCode,
																					  this.menuItem8,
																					  this.mnuExit});
			this.menuItem1.Text = "&File";
			// 
			// mnuOpen
			// 
			this.mnuOpen.Index = 0;
			this.mnuOpen.Text = "&Open";
			this.mnuOpen.Click += new System.EventHandler(this.btnOpenFile_Click);
			// 
			// mnuLoad
			// 
			this.mnuLoad.Index = 1;
			this.mnuLoad.Text = "&Load";
			this.mnuLoad.Click += new System.EventHandler(this.btnLoadXmlFile_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "-";
			// 
			// mnuNotepad
			// 
			this.mnuNotepad.Index = 3;
			this.mnuNotepad.Text = "&Notepad";
			this.mnuNotepad.Click += new System.EventHandler(this.btnNotepad_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "-";
			// 
			// mnuGenCode
			// 
			this.mnuGenCode.Index = 5;
			this.mnuGenCode.Text = "&Generate Code";
			this.mnuGenCode.Click += new System.EventHandler(this.btnCSCode_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 6;
			this.menuItem8.Text = "-";
			// 
			// mnuExit
			// 
			this.mnuExit.Index = 7;
			this.mnuExit.Text = "E&xit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 1;
			this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mnuVerbose,
																					   this.mnuAbbreviate});
			this.menuItem10.Text = "&Options";
			// 
			// mnuVerbose
			// 
			this.mnuVerbose.Index = 0;
			this.mnuVerbose.Text = "Verbose";
			this.mnuVerbose.Click += new System.EventHandler(this.mnuDisplayform_Click);
			// 
			// mnuAbbreviate
			// 
			this.mnuAbbreviate.Index = 1;
			this.mnuAbbreviate.Text = "Abbreviate";
			this.mnuAbbreviate.Click += new System.EventHandler(this.mnuDisplayform_Click);
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 2;
			this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuShowHelp});
			this.mnuHelp.Text = "&Help";
			// 
			// mnuShowHelp
			// 
			this.mnuShowHelp.Index = 0;
			this.mnuShowHelp.Text = "&Show";
			this.mnuShowHelp.Click += new System.EventHandler(this.btnShowHelp_Click);
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnLoadXmlFile;
			this.AllowDrop = true;
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 21);
			this.ClientSize = new System.Drawing.Size(808, 386);
			this.Controls.Add(this.btnOpenFile);
			this.Controls.Add(this.btnNotepad);
			this.Controls.Add(this.btnCSCode);
			this.Controls.Add(this.btnShowHelp);
			this.Controls.Add(this.btnShowNamespaces);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.btnSelXPath);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this._treeResult);
			this.Controls.Add(this.btnExecuteQuery);
			this.Controls.Add(this.txtQuery);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnLoadXmlFile);
			this.Controls.Add(this.txtXmlFileName);
			this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.Text = "Visual XPath";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(String [] args) 
		{
			MainForm mf = new MainForm();
			
			if (args.Length > 0)
			{
				mf._xmlFileName = args[0];
			}
			Application.Run(mf);
		}

		/// <summary>
		/// This event is invoked from the Load button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLoadXmlFile_Click(object sender, System.EventArgs e)
		{
			OpenFile();
		}

		// Load File subroutine
		private void OpenFile()
		{
			// if the file is based on http then disable the Notepad button
			btnNotepad.Enabled = !txtXmlFileName.Text.StartsWith("http://");

			// open the dialog box if we see that there is some invalid file name in the text box
			// or else load the file
			OpenXmlFile(!txtXmlFileName.Text.EndsWith(".xml"));
		}

		/// <summary>
		/// Selects an Xml file through the OpenDialog.
		/// </summary>
		/// <param name="openDialog">This parameter determines whether to open the dialog to select the file or to open the file through the path defined in the text field.</param>
		
		private void OpenXmlFile(bool openDialog)
		{
			string _oldFilename = txtXmlFileName.Text;

			// show the dialog when true is passed
			if (openDialog)
			{
				// if there is a directory path in the text box, then show the dialogbox with that directory
				openXmlFile.InitialDirectory = txtXmlFileName.Text;

				// open the dialog
				DialogResult res = openXmlFile.ShowDialog();

				if (res == DialogResult.OK)
				{
					// set the xml file name. The file name is used from the LoadXmlFile() function
					_xmlFileName = txtXmlFileName.Text = openXmlFile.FileName;

					try
					{
						LoadXmlFile();
					}
					catch(XmlException xE)
					{
						_xmlFileName = txtXmlFileName.Text = _oldFilename;
						MessageBox.Show("Error loading Xml File\n" + xE.Message);
						//return;
					}
				}
			}
		}

		/// <summary>
		/// Also tests the performance of the XmlDocument for loading the Xml file. The FillXmlDocument() is then called at the end to fill the tree with the xml document
		/// </summary>
		private void LoadXmlFile()
		{
			// start performance counter
			PerformanceTimer t = new PerformanceTimer();
			t.Start();

			// load the xml into XmlDocument
			//_xmlDoc.LoadXml(_xmlFile);
			_xmlDoc.Load(_xmlFileName);

			// ending performance counter and display result in status bar
			t.Stop();
	
			statusBar.Text = "Xml Document Load Time: " + t.ElapsedTime.ToString() + " ms";
			btnExecuteQuery.Enabled = true;

			FillXmlDocument(_xmlDoc);
		}

		/// <summary>
		/// Fill the TreeView with the Xml element. The actual filling is performed in the Xml2Tree function, but this function cleans up the TreeView, adds the initial Xml tag to the tree.
		/// </summary>
		/// <param name="_xmlDoc">The XmlDocument to fill in the Tree</param>
		private void FillXmlDocument(XmlDocument _xmlDoc)
		{
            // Initializations made on part of Xml Document
			// start with fresh set of namespaces
			_strNamespaces = "";
			_hasDefNS = false;
			_hasNS = false;

			// enables the Use XPath button
			//btnSelXPath.Enabled = true;
			isContextNode = false;
			
			// clears the tree and begin filling the elements
			_treeResult.BeginUpdate();
			_treeResult.Nodes.Clear();

			// select the root element
			XmlNode docElement = _xmlDoc.DocumentElement;

			// set the XmlNamespaceManager
			_nsMgr= new XmlNamespaceManager(_xmlDoc.NameTable);

			TreeNode rootNode = new TreeNode(_xmlString);
			_treeResult.Nodes.Add(rootNode);

			// Call the actual function to fill in the xml
			Xml2Tree(rootNode, docElement);

			// update tree view
			_treeResult.EndUpdate();

			// expand the tree
			rootNode.Expand();
			
		}
		
		/// <summary>
		/// Perform the filling operation in the tree
		/// </summary>
		/// <param name="tNode">TBD</param>
		/// <param name="xNode">TBD</param>
		private void Xml2Tree( TreeNode tNode, XmlNode xNode)
		{
			
			switch(xNode.NodeType)
			{
				case XmlNodeType.Element:
					// MessageBox.Show("XmlNodeType.Element");
					
					TreeNode elemNode = new TreeNode("<" + xNode.Name);
					elemNode.ForeColor = Color.RoyalBlue;

					Font f = new Font("Tahoma",12);

					elemNode.NodeFont = f;

					tNode.Nodes.Add(elemNode);

					// iterate all attributes
					if (xNode.Attributes.Count > 0)
					{
						for (int i=0; i < xNode.Attributes.Count; i++)
						{
							// add the namespaces to the document
							string [] strTemp;
					
							// we are looking for attributes which are namespaces
							if (xNode.Attributes[i].Name.StartsWith("xmlns"))
							{
								// set the bool to true
								_hasNS = true;
                                
								// the attributes which are namespaces have the
								// following form .e.g.
								// xmlns:admin="http://webns.net/mvcb/"
								// and the default namespaces have the following
								// form e.g.
								// xmlns="http://purl.org/rss/1.0/"

								// parse on the '='
								strTemp = xNode.Attributes[i].Name.Split('=');

								if (!strTemp[0].Equals("xmlns")) // ie. other than the default namespace
								{
									string [] tag = strTemp[0].Split(':');
									
									// set the namespace prefix and value
									_nsPrefix = tag[1];
									_nsValue = xNode.Attributes[i].Value;

								}
								else
								{
									// set the namespace prefix and value
									_nsDefPrefix = _nsPrefix = "def";
									_hasDefNS = true;
									_nsValue = xNode.Attributes[i].Value;
								}

								// add to XmlNamespaceManager
								_nsMgr.AddNamespace(_nsPrefix,_nsValue);
								_strNamespaces += "Prefix = "+_nsPrefix+" : Uri = "+_nsValue+"\n";
							}
							
							// end of namespaces code
							elemNode.Text += " " + xNode.Attributes[i].Name + "=\"" + xNode.Attributes[i].Value + "\"";
						}
					}
					
					elemNode.Text += ">";
					
					// process all childrens
					if (xNode.HasChildNodes)
					{
						for(int i=0; i < xNode.ChildNodes.Count; i++)
						{
							Xml2Tree(elemNode, xNode.ChildNodes[i]);
						}
					}

					// add a child to end the tag
					TreeNode endTag = new TreeNode("</" + xNode.Name + ">");
					tNode.Nodes.Add(endTag);
					break;
				case XmlNodeType.Text:
					// add a child to display the value
					TreeNode textNode = new TreeNode(xNode.Value);
					textNode.ForeColor = Color.Red;
					f = new Font("Tahoma",12);
					textNode.NodeFont = f;
					tNode.Nodes.Add(textNode);
					break;
				case XmlNodeType.CDATA:
					//MessageBox.Show("TODO: XmlNodeType.CDATA");
					break;
				case XmlNodeType.ProcessingInstruction:
					//MessageBox.Show("TODO: XmlNodeType.ProcessingInstruction");
					break;
				case XmlNodeType.Comment:
					//MessageBox.Show("TODO: XmlNodeType.Comment");
					break;
				case XmlNodeType.Document:
					// load the xml document without opening the file dialog
					LoadXmlFile();
					// MessageBox.Show("XmlNodeType.Document");
					break;
				case XmlNodeType.Attribute:
					TreeNode attrNode = new TreeNode(xNode.Value);
					tNode.Nodes.Add(attrNode);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Execute the XPath query in the text box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExecuteQuery_Click(object sender, System.EventArgs e)
		{
			// Disables the btnXPath button
			btnSelXPath.Enabled = false;
			
			// Execute the XPathQuery on the Xml file
			_treeResult.BeginUpdate();
			_treeResult.Nodes.Clear();

			XmlNode docElement = _xmlDoc.DocumentElement;

			TreeNode rootNode = new TreeNode(_xmlString);
			_treeResult.Nodes.Add(rootNode);

			try
			{
			
				// start performance counter
				PerformanceTimer t = new PerformanceTimer();
				t.Start();

				// Selecting the node list
				XmlNodeList xPathNodes = null;
				
				if (_hasNS)
				{
					// set it into xpath query
					xPathNodes = docElement.SelectNodes(txtQuery.Text, _nsMgr);
				}
				else
				{
					xPathNodes = docElement.SelectNodes(txtQuery.Text);
				}
			
				// ending performance counter and display result in status bar
				t.Stop();
		
				statusBar.Text = "XPath Query -- Elapsed time: " + t.ElapsedTime.ToString() + " ms";
			
				for(int i=0; i < xPathNodes.Count; i++)
				{
					Xml2Tree(rootNode, xPathNodes[i]);
				}

				isContextNode = true;

			}
			catch(XPathException xE)
			{
				MessageBox.Show("Error in XPath Query:" + xE.Message);
				btnSelXPath.Enabled = true;
				isContextNode = false;
				return;
			}
			catch(XsltException xs)
			{
				MessageBox.Show(xs.Message);
				btnSelXPath.Enabled = true;
				isContextNode = false;
				return;
			}

			// update tree view
			_treeResult.EndUpdate();

			rootNode.Expand();

		} // end of function

		/// <summary>
		/// Catch the clicks on the TreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeResult_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
//			if (isContextNode)
//				return;

			// gets the node from the mouse dimensions
			_selectedNode = _treeResult.GetNodeAt(e.X, e.Y);
		
			// checks if the click is on some tree node or in the blank treeview
			// also checks the if it is the root node of the tree then do nothing
			if (_selectedNode == null || _selectedNode.Parent == null)
				return;

			_treeResult.SelectedNode = _selectedNode;
		
			// to show whichnode is selected and generate the XPath query for the node
			ClickAction();

		} // end of function

		/// <summary>
		/// Parse namespaces from the Tree node and Add it to Hashtable
		/// </summary>
		private Hashtable GetAttributes(string input)
		{
			Hashtable htAttr = new Hashtable();

			// if the End Tag is selected, then do nothing
			if (input.StartsWith("<") && input.EndsWith(">"))
			{
				// clear the hash table
				htAttr.Clear();
				
				// To parse the following
                // <customer a="a" b="b" c="c">

				// Regex match
				RegexOptions   options = RegexOptions.None;
				// The following regular expression is used to match attributename=attributevalue from
				// the xml tag. Therefore, if some attribute string is not matching then 
				// simple add the character to the following appropriate string
				string attrKeyMatchChar = @"\w:";
				string attrValueMatchChar = @"\w.\s-:?/=@#";
				string regexStr = "(["+ attrKeyMatchChar +"]*=\"["+ attrValueMatchChar +"]*\")";
				Regex          regex = new Regex(regexStr , options);

				// Check for match
				bool   isMatch = regex.IsMatch(input);
				if( isMatch )
				{
					// Get matches
					MatchCollection   matches = regex.Matches(input);
					for( int i = 0; i != matches.Count; ++i )
					{
						string keyvalue = matches[i].Groups[0].Value;

						// don't proceed it the element is a namespace 
						if (!keyvalue.StartsWith("xmlns"))
						{
							// split the input on '='
							string [] splitEqual = keyvalue.Split('=');

							// splitEqual[0] contains the attribute key
							// splitEqual[1] contains the attribute value
							htAttr.Add(splitEqual[0], splitEqual[1]);
						}
					}					
				}
					
			}

			return htAttr;
		}

		private bool LoadXmlAndVerify(string strXml)
		{
			XmlDocument xdocTemp = new XmlDocument();

			try
			{
				// checks the xml. If there is some err in the document then this code throws an exception
				xdocTemp.LoadXml(strXml);
			}
			catch(XmlException xE)
			{
				MessageBox.Show("Please double check the Xml Nodes you changed.\n" + xE.Message);
				return false;
			}

			return true;
		} // end of function

		/// <summary>
		/// This function is called from the TreeView_MouseDown function and used to generate the XPath query for the selected TreeNode
		/// </summary>
		private void ClickAction()
		{
			// called from TreeView_MouseDown
			if (isContextNode)
				return;

			// get the selected node
			TreeNode node = _treeResult.SelectedNode;
			
			if (node == null)
				return;

			_xPathExp = "";

			// iterate all parent node
			while(node.Parent != null)
			{
				// if the node is <...> i.e an xml tag, not the text value
				if (node.Text.StartsWith("<") && node.Text.EndsWith(">"))
				{
					// GetNodeLevel(node) returns the node _level
					// node _level: the position of the node in the current tree
					// _level. e.g. below
					/*
					 * <_level> <-- Node on _level 1
					 *		<sublevel></sublevel> <-- Node on _level 1
					 *		<sublevel></sublevel> <-- Node on _level 2
					 * </_level> <-- End Node of _level 1
					 * 
					 */

					_level = GetNodeLevel(node);
					
					// to hold the prefix in case the default namespace is applied
					// to the whole document
					string tmpDefPrefix = "";

					// check that the node contains a colon :
					string tagValue = MatchRegex(node.Text);

					// check whether the tag contains a colon
					// because a node with namespace have the syntax like rdf:RDF
					// so that contains a colon and does not use the default namespace
					if (_hasDefNS) // only enter if the document contains a default namespace
					{
						if (tagValue.IndexOf(":",0,tagValue.Length) == -1)
						{
							tmpDefPrefix = _nsDefPrefix + ":";
						}
					}

					if (_hasSiblings)
					{
						_xPathExp = DisplayForm.GetElementName(tmpDefPrefix,tagValue) + "[" + DisplayForm.GetPositionName(_level) + "]" + "/" + _xPathExp;
					}
					else
					{
						_xPathExp = DisplayForm.GetElementName(tmpDefPrefix,tagValue) + "/" + _xPathExp;
					}

				}
				else
				{
					// append the 'text()', if an end Tree node is selected
					_xPathExp += DisplayForm.GetTextName();
				}

				// go to parent node
				node = node.Parent;
			}

			_xPathExp= "/" + _xPathExp;

			// remove the '/' from the end of XPath query
			if (_xPathExp.EndsWith("/"))
			{
				_xPathExp = _xPathExp.Remove(_xPathExp.Length-1, 1);
			}

			// get namespaces here
			_attributes = GetAttributes(_treeResult.SelectedNode.Text);

			txtQuery.Text = _xPathExp;
		}
		
		
		private int GetNodeLevel(TreeNode node)
		{
			// iterate all siblings, and if found the same node
			// increment the levelVal
			int prevNodeCount = 1;
			int nextNodeCount = 1;

			// set _hasSiblings to false
			_hasSiblings = false;

			string nodeText = MatchRegex(node.Text);

			// count all nodes previous to current node
			TreeNode preNode = GetPrevTreeNode(node);

            string preNodeText = "";
			
			while(preNode != null)
			{
				preNodeText = MatchRegex(preNode.Text);

				if (nodeText.Equals(preNodeText))
				{
					prevNodeCount++;
				}
				
				preNode = GetPrevTreeNode(preNode);

			}

			if (prevNodeCount > 1)
				_hasSiblings = true;


			// count all nodes after to current node
			TreeNode nextNode = GetNextTreeNode(node);

			string nextNodeText = "";
			
			while(nextNode != null)
			{
				nextNodeText = MatchRegex(nextNode.Text);

				if (nodeText.Equals(nextNodeText))
				{
					nextNodeCount++;
				}
				
				nextNode = GetNextTreeNode(nextNode);

			}

			if (nextNodeCount > 1)
				_hasSiblings = true;

			return prevNodeCount;
		}

		/// <summary>
		/// Gets the previous node for the node passed.
		/// </summary>
		/// <param name="node">Previous node. and null if there is no previous node</param>
		/// <returns>TreeNode</returns>
		private TreeNode GetPrevTreeNode(TreeNode node)
		{
			if (node.PrevNode != null)
                return node.PrevNode.PrevNode;
			else
				return null;
		}

		private TreeNode GetNextTreeNode(TreeNode node)
		{
			if (node.NextNode != null)
				return node.NextNode.NextNode;
			else
				return null;
		}


		/// <summary>
		/// Returns the tag value from an Xml Tag while truncating the rest.
		/// </summary>
		/// <param name="matchStr">The Xml tag in the form of string</param>
		/// <returns></returns>
		/// <remarks>
		/// Passing "&lt;customers&gt;" as input return customer.
		/// Passing "&lt;customer id='2001'&gt;" as input return customer
		/// </remarks>
		private string MatchRegex(string matchStr)
		{
			//e.g. 
			// Regex match
			Regex          regex = new Regex(@"(<|</)?([\w:]+)(\s|>)?");
			string         input = matchStr;

			// Get match
			Match   match = regex.Match(input);
			Group group = match.Groups[2];

			return group.Value;
		}

		private void btnSelXPath_Click(object sender, System.EventArgs e)
		{
			txtQuery.Text = statusBar.Text;
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBox1.Checked)
			{
				_treeResult.ExpandAll();
			}
			else
			{
				_treeResult.CollapseAll();
			}
		}

		/// <summary>
		/// Display the namespaces in the Xml document
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnShowNamespaces_Click(object sender, System.EventArgs e)
		{
			// build a string for all the namespaces in the xml document and show in a 
			// message box
			string strMessage;
			
			if (_strNamespaces != "")
			{
				strMessage = _strNamespaces + "\n'def' is the prefix of Default Namespace";
			}
			else
			{
				strMessage = "No namespaces";
			}

			MessageBox.Show(strMessage);
		}

		private void btnShowHelp_Click(object sender, System.EventArgs e)
		{
			VisualXPath.HelpForm f = new VisualXPath.HelpForm();
			f.Show();
		}

		private void btnCSCode_Click(object sender, System.EventArgs e)
		{
			string fileName = _xmlFileName;
			string xPath = txtQuery.Text;
			string newLine = "\r\n";

			string csCode = "using System;" + newLine + "using System.Xml;" + newLine + "using System.Xml.XPath;" + newLine + "public static void RunXPath()" + newLine + "{" + newLine + "XmlDocument xmlDoc = new XmlDocument();" + newLine + "xmlDoc.Load(@\""+ fileName + "\");" + newLine + "";


			if (_hasNS)
			{
				csCode += "XmlNamespaceManager nsMgr= new XmlNamespaceManager(xmlDoc.NameTable);" + newLine;
				
				foreach(string str in _nsMgr)
				{
					if (_nsMgr.HasNamespace(str))
					{
						string strNS = _nsMgr.LookupNamespace(str);
						csCode += "nsMgr.AddNamespace(\""+ str +"\",\""+ strNS +"\");" + newLine + "";
					}					
				}

				csCode += "XmlNodeList nodes = xmlDoc.SelectNodes(\""+ xPath +"\",nsMgr);" + newLine + "foreach(XmlNode node in nodes)" + newLine + "{" + newLine + "// Do anything with node" + newLine + "Console.WriteLine(node.OuterXml);" + newLine + "}" + newLine + "}";

			}
			else
			{
				csCode = "using System;" + newLine + "using System.Xml;" + newLine + "using System.Xml.XPath;" + newLine + "public static void RunXPath()" + newLine + "{" + newLine + "XmlDocument xmlDoc = new XmlDocument();" + newLine + "xmlDoc.Load(@\""+ fileName + "\");" + newLine + "XmlNodeList nodes = xmlDoc.SelectNodes(\""+ xPath +"\");" + newLine + "foreach(XmlNode node in nodes)" + newLine + "{" + newLine + "// Do anything with node" + newLine + "Console.WriteLine(node.OuterXml);" + newLine + "}" + newLine + "}";
			}

			// Instantiate CodeForm and pass csCode to constructor
			VisualXPath.CodeForm codeForm = new VisualXPath.CodeForm();
			codeForm.ShowCode(csCode);
		}

		private void btnNotepad_Click(object sender, System.EventArgs e)
		{
			OpenNotePad(_xmlFileName);
			
		}

		private void OpenNotePad(string filename)
		{
			Process.Start("notepad.exe",filename);
		}

		private void ctxAttr_Popup(object sender, System.EventArgs e)
		{
			// first clear the context menu
			ctxAttr.MenuItems.Clear();

			if (isContextNode || (_treeResult.SelectedNode.Parent == null)) // if we r working with child(selected) xml document then return
				return;

			AddAttributesMenu(ctxAttr.MenuItems,new System.EventHandler(this.ctxAttr_Click));

			AddAllElementsMenu();

			// if the node has siblings then add the group functions like "Select all.."
			GetNodeLevel(_treeResult.SelectedNode);

			if (_hasSiblings)
			{
				if (_attributes.Count > 0)
				{
					MenuItem selAttrMenu = new MenuItem(("Select All " + MatchRegex(_treeResult.SelectedNode.Text) + "(s) for"));
					
					AddAttributesMenu(selAttrMenu.MenuItems,new System.EventHandler(this.mnuSelectAttributeValue_Click));

					ctxAttr.MenuItems.Add(selAttrMenu);
				}
			}

//			AddBooleanMenu();
//			EqualityMenu();
//			RelationalMenu();
//			NumericalMenu();
//			FunctionsMenu();
		}

//		private void AddBooleanMenu()
//		{
//			ctxAttr.MenuItems.Add(new MenuItem("-"));
//			MenuItem booleanMenu = new MenuItem("Boolean Expressions");
//
//			booleanMenu.MenuItems.Add(new MenuItem("or",new EventHandler(this.AddBooleanMenu_Click)));
//			booleanMenu.MenuItems.Add(new MenuItem("and",new EventHandler(this.AddBooleanMenu_Click)));
//
//			ctxAttr.MenuItems.Add(booleanMenu);
//		}
//
//		private void AddBooleanMenu_Click(object sender, System.EventArgs e)
//		{
//
//			switch(((MenuItem)sender).Text)
//			{
//				case "or":
//					statusBar.Text = boolOperation.Or(_xPathExp);
//					break;
//				case "and":
//					statusBar.Text = boolOperation.And(_xPathExp);
//					break;
//			}
//
//			Baloon("Select another node to apply the '"+ ((MenuItem)sender).Text +"' operation");
//		}
//

		private void AddAllElementsMenu()
		{
			//TODO:
			ctxAttr.MenuItems.Add(new MenuItem("-"));
			MenuItem addAllElementsMenu = new MenuItem("Select All <" + MatchRegex(_treeResult.SelectedNode.Text) + "> elements");

			if (_hasDefNS)
			{
				addAllElementsMenu.MenuItems.Add(new MenuItem(".//" + DisplayForm.GetElementName(_nsDefPrefix+":",MatchRegex(_treeResult.SelectedNode.Text)),new System.EventHandler(this.mnuSelectElements_Click)));
			}
			else
			{
                addAllElementsMenu.MenuItems.Add(new MenuItem(".//" + DisplayForm.GetElementName("",MatchRegex(_treeResult.SelectedNode.Text)),new System.EventHandler(this.mnuSelectElements_Click)));
			}

			ctxAttr.MenuItems.Add(addAllElementsMenu);
		}
		
		private void AddAttributesMenu(Menu.MenuItemCollection mnuColl,System.EventHandler e)
		{
			IDictionaryEnumerator myEnumerator = _attributes.GetEnumerator();
      
			while (myEnumerator.MoveNext())
			{
				string strVal = "";

				// add menu item to context menu
				MenuItem attr = new MenuItem(myEnumerator.Key.ToString() + strVal,e);
				mnuColl.Add(attr);
			}
		}
			
		private void ctxAttr_Click(Object sender, System.EventArgs e)
		{
			// add attribute string to the xpath string
			_xPathExp += "/" + DisplayForm.GetAttrName(((MenuItem)sender).Text);

			// show xpath query in status bar
			txtQuery.Text = _xPathExp;
		}

		private void btnOpenFile_Click(object sender, System.EventArgs e)
		{
			OpenXmlFile(true);
		}

		private void MainForm_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				String [] filenames = (String[])e.Data.GetData(DataFormats.FileDrop);
				_xmlFileName = txtXmlFileName.Text = filenames[0];
				LoadXmlFile();
			}
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			if (_xmlFileName.Equals(""))
				return;

			txtXmlFileName.Text = _xmlFileName;
			LoadXmlFile();
		}

		private void _treeResult_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (isContextNode)
				return;

			ClickAction();
		}

		private void ColorizeChildNodes(TreeNode tNode, Color c)
		{
			// Print each node recursively.
			TreeNodeCollection nodes = tNode.Nodes;

			foreach (TreeNode n in nodes)
			{
				ColorRecursive(n,c);
			}
		}

		private void ColorRecursive(TreeNode treeNode,Color c)
		{
			treeNode.BackColor = c;

			foreach (TreeNode tn in treeNode.Nodes)
			{
				ColorRecursive(tn,c);
			}
		}

		private void mnuSelectAttributeValue_Click(object sender, System.EventArgs e)
		{			
			string selectedAttr = ((MenuItem)sender).Text;
			txtQuery.Text = DisplayForm.GetAllElementsName(_xPathExp) + "/@" + selectedAttr;
		}

		private void mnuSelectElements_Click(object sender, System.EventArgs e)
		{
//			if (_xPathExp.EndsWith("]"))
//                txtQuery.Text = DisplayForm.GetAllElementsName(_xPathExp);
//			else
				txtQuery.Text = ((MenuItem)sender).Text;
		}

		private void mnuExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void mnuDisplayform_Click(object sender, System.EventArgs e)
		{
			mnuAbbreviate.Checked = !mnuAbbreviate.Checked;
			mnuVerbose.Checked = !mnuVerbose.Checked;

			DisplayForm.isVerbose = isVerbose = !isVerbose;
			ClickAction();
		}

		private void MainForm_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

	}
}
