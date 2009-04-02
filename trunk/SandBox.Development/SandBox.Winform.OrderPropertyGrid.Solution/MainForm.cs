//
// (C) Paul Tingey 2004 
//
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace OrderedPropertyGrid
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private Assembly _assembly;
        private System.Windows.Forms.PropertyGrid _propertyGrid;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.Panel _panelTopLeft;
        private System.Windows.Forms.Panel _panelTopRight;
        private System.Windows.Forms.Splitter _splitter;
        private System.Windows.Forms.Button _btnShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox _lbObjects;
        private System.Windows.Forms.Label label2;
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
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._splitter = new System.Windows.Forms.Splitter();
            this._panelTopLeft = new System.Windows.Forms.Panel();
            this._panelTopRight = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this._lbObjects = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this._btnShow = new System.Windows.Forms.Button();
            this._panelTopLeft.SuspendLayout();
            this._panelTopRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this._propertyGrid.CommandsVisibleIfAvailable = true;
            this._propertyGrid.Cursor = System.Windows.Forms.Cursors.HSplit;
            this._propertyGrid.LargeButtons = false;
            this._propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this._propertyGrid.Location = new System.Drawing.Point(8, 176);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.Size = new System.Drawing.Size(328, 476);
            this._propertyGrid.TabIndex = 0;
            this._propertyGrid.Text = "_propertyGrid";
            this._propertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
            this._propertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
            // 
            // _tabControl
            // 
            this._tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this._tabControl.Location = new System.Drawing.Point(12, 12);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(472, 640);
            this._tabControl.TabIndex = 3;
            this._tabControl.SelectedIndexChanged += new System.EventHandler(this._tabControl_SelectedIndexChanged);
            // 
            // _splitter
            // 
            this._splitter.Dock = System.Windows.Forms.DockStyle.Right;
            this._splitter.Location = new System.Drawing.Point(487, 0);
            this._splitter.Name = "_splitter";
            this._splitter.Size = new System.Drawing.Size(5, 662);
            this._splitter.TabIndex = 4;
            this._splitter.TabStop = false;
            // 
            // _panelTopLeft
            // 
            this._panelTopLeft.Controls.Add(this._tabControl);
            this._panelTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelTopLeft.Location = new System.Drawing.Point(0, 0);
            this._panelTopLeft.Name = "_panelTopLeft";
            this._panelTopLeft.Size = new System.Drawing.Size(487, 662);
            this._panelTopLeft.TabIndex = 6;
            // 
            // _panelTopRight
            // 
            this._panelTopRight.Controls.Add(this.label2);
            this._panelTopRight.Controls.Add(this._lbObjects);
            this._panelTopRight.Controls.Add(this.label1);
            this._panelTopRight.Controls.Add(this._btnShow);
            this._panelTopRight.Controls.Add(this._propertyGrid);
            this._panelTopRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._panelTopRight.Location = new System.Drawing.Point(492, 0);
            this._panelTopRight.Name = "_panelTopRight";
            this._panelTopRight.Size = new System.Drawing.Size(344, 662);
            this._panelTopRight.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(8, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(328, 28);
            this.label2.TabIndex = 14;
            this.label2.Text = "Select one or more of the available objects to display in the Property Grid";
            // 
            // _lbObjects
            // 
            this._lbObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this._lbObjects.Location = new System.Drawing.Point(8, 72);
            this._lbObjects.Name = "_lbObjects";
            this._lbObjects.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this._lbObjects.Size = new System.Drawing.Size(328, 56);
            this._lbObjects.TabIndex = 13;
            this._lbObjects.SelectedIndexChanged += new System.EventHandler(this._lbObjects_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Available objects:";
            // 
            // _btnShow
            // 
            this._btnShow.Location = new System.Drawing.Point(8, 12);
            this._btnShow.Name = "_btnShow";
            this._btnShow.Size = new System.Drawing.Size(200, 28);
            this._btnShow.TabIndex = 10;
            this._btnShow.Text = "Re-compile and Instantiate Objects";
            this._btnShow.Click += new System.EventHandler(this.CompileAndShow);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(836, 662);
            this.Controls.Add(this._panelTopLeft);
            this.Controls.Add(this._splitter);
            this.Controls.Add(this._panelTopRight);
            this.Name = "MainForm";
            this.Text = "Ordered Property Grid Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this._panelTopLeft.ResumeLayout(false);
            this._panelTopRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new MainForm());
        }

        private void CompileAndShow(object sender, System.EventArgs e)
        {
            string sourceCode = ((TextBox)_tabControl.SelectedTab.Controls[0]).Text;
            _assembly = CompileEngine.CreateAssembly(sourceCode);
            if (_assembly != null)
            {
                //
                // Add the the types found into the listbox
                //
                _lbObjects.Items.Clear();
                ArrayList typeNames = new ArrayList();
                foreach (Type type in _assembly.GetTypes())
                {
                    typeNames.Add(type.FullName);
                    
                }
                typeNames.Reverse();
                _lbObjects.Items.AddRange(typeNames.ToArray());
                //
                // Select the first item
                //
                if (_lbObjects.Items.Count > 0)
                {
                    _lbObjects.SelectedIndex = -1;
                    _lbObjects.SelectedIndex = 0;
                }
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            //
            // Load the code examples
            //
            LoadExamples();            
            //
            // Compile the first tab and show
            //
            CompileAndShow(sender,e);
        }

        private void AddTabPage(string sourceCode, string filename)
        {
            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Text = sourceCode;
            TabPage tabPage = new TabPage(filename);
            tabPage.Controls.Add(textBox);
            textBox.Dock = DockStyle.Fill;
            _tabControl.TabPages.Add(tabPage);
        }

        private void LoadExamples()
        {
            //
            // Go through the examples folder
            //
            Assembly assembly = GetType().Module.Assembly;
            ArrayList names = new ArrayList(assembly.GetManifestResourceNames());
            names.Sort();
            foreach (string name in names)
            {
              if (name.IndexOf("Examples.") != -1)
              {
                using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream(name)))
                {
                    string[] parts = name.Split(new char[] {'.'});                    
                    string sourceCode = sr.ReadToEnd();
                    AddTabPage(sourceCode,parts[parts.Length-2]);
                }
              }
            }

        }

        private void _lbObjects_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_lbObjects.SelectedItems.Count == 0)
            {
                return;
            }
            if (_assembly == null)
            {
                return;
            }
            //
            // Create an object for each type
            //
            ArrayList objects = new ArrayList();
            foreach (string typeName in _lbObjects.SelectedItems)
            {
                try
                {
                    objects.Add(_assembly.CreateInstance(typeName));
                }
                catch
                {
                    MessageBox.Show(string.Format("Error creating type {0}",typeName) ,"CreateInstance failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            //
            // Show the selected item in the grid
            //
            _propertyGrid.SelectedObjects = objects.ToArray();        
        }
    
        private void _tabControl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //
            // Compile on tab change
            //
            CompileAndShow(sender,e);
        }

        private void _tbCode3_TextChanged(object sender, System.EventArgs e)
        {
        
        }
	}
}
