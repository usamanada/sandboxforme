using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Management;
using System.Data;
using System.IO;
using System.Text;

using System.Runtime.InteropServices;

namespace SandBox.Winform.WMI.Explorer
{
    public partial class WMICodeCreator : Form
    {
        public WMICodeCreator()
        {
            NamespaceCount = 0;
            QueryCounter = 0;

            // Holds the event queries that are supported by event providers.
            SupportedEventQueries = new string[MAXEVENTQUERIES];
            SupportedEventQueries.Initialize();

            // Generates the start-up screen.
            SplashScreenForm.ShowSplashScreen();

            InitializeComponent();

            // Creates the window for the target computer information.
            this.TargetWindow = new TargetComputerWindow(this);
            this.TargetWindow.Visible = false;

            // Creates the array of windows for method in-parameters.
            this.InParameterArray = new InParameterWindow[MAXINPARAMS];
            InParameterArray.Initialize();

            // Creates the array of windows for event conditions.
            this.EventConditionArray = new EventQueryCondition[MAXQUERYCONDITIONS];
            EventConditionArray.Initialize();

            // Populates the class lists on the form.
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.InitialAddClassesToList));
            this.InitialAddClassesToList(this);

            // Populates the namespace list on the form.
            this.AddNamespacesToList(this);
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddNamespacesToList));
        }
         //-------------------------------------------------------------------------
        // When the form is created, this method adds all the WMI classes to
        // the lists of classes on each tab in the WMICodeCreator form. This method
        // should only be called in the WMICodeCreator constructor.
        //-------------------------------------------------------------------------
        private void InitialAddClassesToList(object o) 
        {
            // Start the progress bar on the splash screen.
            SplashScreenForm.SetProgressMax(330);   

            // Variables for counting the class on each tab
            // and for status.
            int queryClassCount = 0;
            this.ClassStatus.Text = "Searching...";
            int classCount_m = 0;
            this.ClassStatus_m.Text = "Searching...";
            int classCount_event = 0;
            this.ClassStatus_event.Text = "Searching...";

            try 
            {
                // Performs WMI object query on 
                // the selected namespace.
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(
                    new ManagementScope(
                    "root\\CIMV2"),
                    new WqlObjectQuery(
                    "select * from meta_class"),
                    null);

                foreach (ManagementClass wmiClass in
                    searcher.Get()) 
                {
                    // If the class is derived from the __Event class, add it
                    // to the event class list.
                    if(wmiClass.Derivation.Contains("__Event"))
                    {
                        this.ClassList_event.Items.Add(
                            wmiClass["__CLASS"].ToString());
                        classCount_event++;
                    }

                    foreach (QualifierData qd in wmiClass.Qualifiers)
                    {
                        // If the class is dynamic or static, add it to the class
                        // list on the query tab.
                        if(qd.Name.Equals("dynamic") || qd.Name.Equals("static"))
                        {
                            this.ClassList.Items.Add(
                                wmiClass["__CLASS"].ToString());
                            queryClassCount++;

                            // Increment the progress bar on the splash screen.
                            if(queryClassCount < 199)
                            {
                                //SplashScreenForm.IncrementProgress();
                            }

                            // If the class has methods, add it to the method class list.
                            if(wmiClass.Methods.Count > 0)
                            {
                                this.ClassList_m.Items.Add(
                                    wmiClass["__CLASS"].ToString());
                                classCount_m++;

                                // Increment the progress bar on the splash screen.
                                if(classCount_m < 110)
                                {
                                    //SplashScreenForm.IncrementProgress();
                                }
                            }
                        }

                    }
                }
                // Report the number of WMI classes found.
                this.ClassStatus.Text = 
                    queryClassCount + " classes (dynamic or static) found.";
                this.ClassStatus_m.Text = 
                    classCount_m + " classes with methods found.";
                this.ClassStatus_event.Text = 
                    classCount_event + " classes derived from the __Event class found.";

                SplashScreenForm.CloseForm();
                
            }
                // Report problems during the population of the class lists.
            catch (System.Management.ManagementException ex) 
            {
                this.ClassStatus.Text = ex.Message;
                this.ClassStatus_m.Text = ex.Message;
                this.ClassStatus_event.Text = ex.Message;
            }
            catch (System.ArgumentOutOfRangeException rangeException)
            {
                this.ClassStatus.Text = rangeException.Message;
                this.ClassStatus_m.Text = rangeException.Message;
                this.ClassStatus_event.Text = rangeException.Message;
            }
            catch (System.ArgumentException argException)
            {
                this.ClassStatus.Text = argException.Message;
                this.ClassStatus_m.Text = argException.Message;
                this.ClassStatus_event.Text = argException.Message;
            }
        }

        //-------------------------------------------------------------------------
        // Adds the namespaces to the namespace lists
        // starting from the "root" namespace.
        //-------------------------------------------------------------------------
        private void AddNamespacesToList(object o) 
        {
            this.NamespaceCount = 0;
            AddNamespacesToListRecursive("root");
        }

        //-------------------------------------------------------------------------
        // Adds the namespaces to the namespace starting from the root
        // namespace passed into the root in-parameter.
        //-------------------------------------------------------------------------
        private void AddNamespacesToListRecursive(string root) 
        {
            
            this.BrowseNamespaceResults.Text = "Searching...";
            try 
            {
                // Enumerates all WMI instances of 
                // __namespace WMI class.
                ManagementClass nsClass = 
                    new ManagementClass(
                    new ManagementScope(root),
                    new ManagementPath("__namespace"),
                    null);
                foreach(ManagementObject ns in 
                    nsClass.GetInstances())
                {
                    // Adds the namespaces to the namespace lists.
                    string namespaceName = root + "\\" + ns["Name"].ToString();
                    this.BrowseNamespaceList.Items.
                        Add(namespaceName);
                    this.NamespaceValue_m.Items.Add(
                        namespaceName);
                    this.NamespaceValue.Items.Add(
                        namespaceName);
                    this.NamespaceList_event.Items.Add(
                        namespaceName);
                    //SplashScreenForm.IncrementProgress();
                    NamespaceCount++;
                    AddNamespacesToListRecursive(namespaceName);
                }
                // Reports the number of namespaces found.
                this.BrowseNamespaceResults.Text = 
                    NamespaceCount + " namespaces found.";
            }
            catch (ManagementException e) 
            {
                this.BrowseNamespaceResults.Text = e.Message;
            }
        }

        //-------------------------------------------------------------------------
        // Calls the AddNamespacesToTargetListRecursive method to start with the
        // "root" namespace.
        //-------------------------------------------------------------------------
        private void AddNamespacesToTargetList(object o)
        {
            AddNamespacesToTargetListRecursive("root");
        }

        //-------------------------------------------------------------------------
        // Adds the namespaces to the TargetClassList_event list on the event tab
        // when the user selects the __Namespace*Event class.
        //-------------------------------------------------------------------------
        private void AddNamespacesToTargetListRecursive(string root)
        {
            try 
            {
                // Enumerates all WMI instances of 
                // __namespace WMI class.
                ManagementClass nsClass = 
                    new ManagementClass(
                    new ManagementScope(root),
                    new ManagementPath("__namespace"),
                    null);
                foreach(ManagementObject ns in 
                    nsClass.GetInstances())
                {
                    // Add namespaces to the list.
                    string namespaceName = root + "\\" + ns["Name"].ToString();
                    this.TargetClassList_event.Items.
                        Add(namespaceName);
                    
                    AddNamespacesToTargetListRecursive(namespaceName);
                }
                
            }
            catch (ManagementException e)
            {
                MessageBox.Show("Error creating a list of namespaces: " + e.Message);
            }


        }

        //-------------------------------------------------------------------------
        // Populates the query tab's class list.
        //
        //-------------------------------------------------------------------------
        private void AddClassesToList(object o) 
        {   

            int classCount = 0;
            this.ClassStatus.Text = "Searching...";
            try 
            {
                // Performs WMI object query on 
                // selected namespace.
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(
                    new ManagementScope(
                    NamespaceValue.Text),
                    new WqlObjectQuery(
                    "select * from meta_class"),
                    null);

                foreach (ManagementClass wmiClass in
                    searcher.Get()) 
                {
                    foreach (QualifierData qd in wmiClass.Qualifiers)
                    {
                        // If the class is dynamic, add it to the list.
                        if(qd.Name.Equals("dynamic") || qd.Name.Equals("static"))
                        {
                            this.ClassList.Items.Add(
                                wmiClass["__CLASS"].ToString());
                            classCount++;
                        }
                    }
                }
                // Report the number of classes found.
                this.ClassStatus.Text = 
                    classCount + " classes (dynamic or static) found.";
                
            }
            catch (ManagementException ex) 
            {
                this.ClassStatus.Text = ex.Message;
            }			
        }

        //-------------------------------------------------------------------------
        // Populates the method tab's class list.
        //
        //-------------------------------------------------------------------------
        private void AddClassesToMethodPageList(object o) 
        {

            int classCount_m = 0;
            this.ClassStatus_m.Text = "Searching...";
            try 
            {
                // Performs WMI object query on the
                // selected namespace.
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(
                    new ManagementScope(
                    NamespaceValue_m.Text),
                    new WqlObjectQuery(
                    "select * from meta_class"),
                    null);				
                foreach (ManagementClass wmiClass in
                    searcher.Get()) 
                {
                    foreach (QualifierData qd in wmiClass.Qualifiers)
                    {
                        if(qd.Name.Equals("dynamic") || qd.Name.Equals("static"))
                        { 
                            // If the class has methods, add it to the list.
                            if(wmiClass.Methods.Count > 0)
                            {
                                this.ClassList_m.Items.Add(
                                    wmiClass["__CLASS"].ToString());
                                classCount_m++;
                            }
                        }
                    }
                }
                this.ClassStatus_m.Text = 
                    classCount_m + " classes with methods found.";
                
            }
            catch (ManagementException ex) 
            {
                this.ClassStatus_m.Text = ex.Message;
            }			
        }

        //-------------------------------------------------------------------------
        // Populates the event tab's TargetClassList_event list with classes
        // that contain methods. This method should be called when the user
        // selects the __MethodInvocationEvent event class.
        //-------------------------------------------------------------------------
        private void AddMethodClassesToTargetClassList(object o)
        {
            try 
            {
                // Performs WMI object query on the
                // selected namespace.
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(
                    new ManagementScope(
                    this.NamespaceList_event.Text),
                    new WqlObjectQuery(
                    "select * from meta_class"),
                    null);				
                foreach (ManagementClass wmiClass in
                    searcher.Get()) 
                {
                    foreach (QualifierData qd in wmiClass.Qualifiers)
                    {
                        if(qd.Name.Equals("dynamic") || qd.Name.Equals("static"))
                        { 
                            // If the class has methods, send it to the list.
                            if(wmiClass.Methods.Count > 0)
                            {
                                this.TargetClassList_event.Items.Add(
                                    wmiClass["__CLASS"].ToString());
                            }
                        }
                    }
                }
                
            }
            catch (ManagementException e)
            {
                MessageBox.Show("Error creating a list of classes with methods: " + e.Message);
            }
			
        }

        //-------------------------------------------------------------------------
        // Populates the event tab's class list with classes derived from the
        // __Event class.
        //-------------------------------------------------------------------------
        private void AddClassesToEventPageList(object o) 
        {
            int classCount_event = 0;
            this.ClassStatus_event.Text = "Searching...";
            try 
            {
                // Performs WMI object query on the
                // selected namespace.
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(
                    new ManagementScope(
                    NamespaceList_event.Text),
                    new WqlObjectQuery(
                    "select * from meta_class"),
                    null);
				
                foreach (ManagementClass wmiClass in
                    searcher.Get()) 
                { 
                    // If the class is derived from an event class,
                    // send it to the list.
                    if(wmiClass.Derivation.Contains("__Event"))
                    {
                        this.ClassList_event.Items.Add(
                            wmiClass["__CLASS"].ToString());
                        classCount_event++;
                    }
                }
                this.ClassStatus_event.Text = 
                    classCount_event + " classes derived from the __Event class found.";
                
            }
            catch (ManagementException ex) 
            {
                this.ClassStatus_event.Text = ex.Message;
            }			
        }

        //-------------------------------------------------------------------------
        // Populates the event tab's target class list with classes
        // that contain methods.
        //-------------------------------------------------------------------------
        private void AddClassesToTargetClassList(object o) 
        {
            try 
            {
                // Performs WMI object query on the
                // selected namespace.
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(
                    new ManagementScope(
                    this.NamespaceList_event.Text),
                    new WqlObjectQuery(
                    "select * from meta_class"),
                    null);				
                foreach (ManagementClass wmiClass in
                    searcher.Get()) 
                {
                    this.TargetClassList_event.Items.Add(
                        wmiClass["__CLASS"].ToString());
                }
                
            }
            catch (ManagementException e)
            {
                MessageBox.Show("Error creating a list of classes: " + e.Message);
            }		
        }

        //-------------------------------------------------------------------------
        // Populates the browse tab's class list with all the classes
        // from the selected namespace.
        //-------------------------------------------------------------------------
        private void AddClassesToBrowserList(object o) 
        {
            int classCount_b = 0;
            this.BrowseClassResults.Text = "Searching...";
            try 
            {
                // Performs WMI object query on the
                // selected namespace.
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(
                    new ManagementScope(
                    this.BrowseNamespaceList.SelectedItem.ToString()),
                    new WqlObjectQuery(
                    "select * from meta_class"),
                    null);				
                foreach (ManagementClass wmiClass in
                    searcher.Get()) 
                {
                    this.BrowseClassList.Items.Add(
                        wmiClass["__CLASS"].ToString());
                    classCount_b++;
                }
                this.BrowseClassResults.Text = 
                    classCount_b + " classes found.";
            }
            catch (ManagementException ex) 
            {
                this.BrowseClassResults.Text = ex.Message;
            }			
        }

        //-------------------------------------------------------------------------
        // Populates the query tab's property list with properties from 
        // the class in the class list.
        //-------------------------------------------------------------------------
        private void AddPropertiesToList(object o)
        {
	
            int propertyCount = 0;
            this.PropertyStatus.Text = "Searching...";

            try
            {
                // Gets the property qualifiers.
                ObjectGetOptions op = new ObjectGetOptions(null, System.TimeSpan.MaxValue, true);

                ManagementClass mc = new ManagementClass(this.NamespaceValue.Text,
                    this.ClassList.Text, op);
                mc.Options.UseAmendedQualifiers = true;

                foreach (PropertyData dataObject in
                    mc.Properties)
                {
                    this.PropertyList.Items.Add(
                        dataObject.Name);
                    propertyCount++;
                }

                this.PropertyStatus.Text = 
                    propertyCount + " properties found.";
            }
            catch (ManagementException ex) 
            {
                this.PropertyStatus.Text = ex.Message;
            }			
        }

        //-------------------------------------------------------------------------
        // Populates the browse tab's property list with properties from the
        // class in the class list.
        //-------------------------------------------------------------------------
        private void AddPropertiesToBrowserList(object o)
        {
            int propertyCount_b = 0;
            this.BrowsePropertyStatus.Text = "Searching...";

            try
            {
                // Gets the property qualifiers.
                ObjectGetOptions op = new ObjectGetOptions(null, System.TimeSpan.MaxValue, true);

                ManagementClass mc = new ManagementClass(this.BrowseNamespaceList.Text,
                    this.BrowseClassList.Text, op);
                mc.Options.UseAmendedQualifiers = true;

                foreach (PropertyData dataObject in
                    mc.Properties)
                {
                    this.BrowsePropertyList.Items.Add(
                        dataObject.Name);
                    propertyCount_b++;
                }

                this.BrowsePropertyStatus.Text = 
                    propertyCount_b + " properties found.";
            }
            catch (ManagementException ex) 
            {
                this.BrowsePropertyStatus.Text = ex.Message;
            }			
        }

        //-------------------------------------------------------------------------
        // Populates the browse tab's qualifier list with qualifiers from the
        // class in the class list.
        //-------------------------------------------------------------------------
        private void AddQualifiersToBrowserList(object o)
        {
            int qualifierCount_b = 0;
            this.BrowseQualiferStatus.Text = "Searching...";

            try
            {
                // Gets the property qualifiers.
                ObjectGetOptions op = new ObjectGetOptions(null, System.TimeSpan.MaxValue, true);

                ManagementClass mc = new ManagementClass(this.BrowseNamespaceList.Text,
                    this.BrowseClassList.Text, op);
                mc.Options.UseAmendedQualifiers = true;

                foreach (QualifierData dataObject in
                    mc.Qualifiers)
                {
                    this.BrowseQualifierList.Items.Add(
                        dataObject.Name);
                    qualifierCount_b++;
                }


                this.BrowseQualiferStatus.Text = 
                    qualifierCount_b + " qualifiers found.";
            }
            catch (ManagementException ex) 
            {
                this.BrowsePropertyStatus.Text = ex.Message;
            }			
        }

        //-------------------------------------------------------------------------
        // Populates the method tab's method list with methods from the
        // class in the class list.
        //-------------------------------------------------------------------------
        private void AddMethodsToList(object o)
        {
            int methodCount = 0;
            this.MethodStatus.Text = "Searching...";

            try 
            {
                ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);
                foreach (MethodData m in c.Methods)
                {
                    this.MethodList.Items.Add(
                        m.Name);
                    methodCount++;

                }
				
                this.MethodStatus.Text = 
                    methodCount + " methods found.";
            }
            catch (ManagementException ex) 
            {
                this.MethodStatus.Text = ex.Message;
            }
            
        }

        //-------------------------------------------------------------------------
        // Populates the browse tab's method list with methods from the
        // class in the class list.
        //-------------------------------------------------------------------------
        private void AddMethodsToBrowserList(object o)
        {
            int methodCount_b = 0;
            this.BrowseMethodStatus.Text = "Searching...";

            try 
            {   
                ObjectGetOptions options = new ObjectGetOptions();
                ManagementClass c = new ManagementClass(this.BrowseNamespaceList.Text,
                    this.BrowseClassList.Text, options);
                c.Options.UseAmendedQualifiers = true;

                foreach (MethodData m in c.Methods)
                {
                    this.BrowseMethodList.Items.Add(
                        m.Name);
                    methodCount_b++;

                }
				
                this.BrowseMethodStatus.Text = 
                    methodCount_b + " methods found.";
            }
            catch (ManagementException ex) 
            {
                this.BrowseMethodStatus.Text = ex.Message;
            }
            
        }

        //-------------------------------------------------------------------------
        // Populates the query tab's property value list with values from the
        // selected properties in the property list.
        //-------------------------------------------------------------------------
        private void AddValuesToList(object o)
        {
            string buffer = "";
            int valueCount = 0;
            this.ValueStatus.Text = "Searching...";
            try 
            {
                // Performs WMI object query on the
                // selected class.
                string query = "select * from " + this.ClassList.Text;
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher(
                    new ManagementScope(
                    NamespaceValue.Text),
                    new WqlObjectQuery(query),
                    null);
  
                bool instanceCounter = true;
                foreach (ManagementObject wmiObject in
                    searcher.Get()) 
                {
                    foreach (object property in this.PropertyList.SelectedItems)
                    {
                        if(wmiObject.Properties[property.ToString()].IsArray)
                        {
                            // Do nothing.
                        }
                        else
                        {
                            // Set buffer string used to separate instances in the list.
                            if(instanceCounter)
                            {
                                buffer = "";
                            }
                            else
                            {
                                buffer = "          ";
                            }

                            // Property is not an array.
                            if(wmiObject.Properties[property.ToString()].Type.ToString().Equals("String"))
                            {	
                                // Property is a string.
                                this.ValueList.Items.Add(buffer + property.ToString() + " = '" +
                                    wmiObject.GetPropertyValue(
                                    property.ToString()) + "'" );
								
                                valueCount++;
                            }
                            else
                            {
                                // Property is not a string.
                                this.ValueList.Items.Add(buffer + property.ToString() + " = " +
                                    wmiObject.GetPropertyValue(
                                    property.ToString()));
                                valueCount++;
                            }
                        }
                    }
                    
                    if(instanceCounter)
                    {
                        instanceCounter = false;
                    }
                    else
                    {
                        instanceCounter = true;
                    }
                }
                this.ValueStatus.Text = 
                    valueCount + " values found. Properties with an array data type are not listed (can't be used in a query).";
            }
            catch (ManagementException ex) 
            {
                this.ValueStatus.Text = ex.Message;
            }			
        }

        //-------------------------------------------------------------------------
        // Handles the event when a property is selected in the query tab property
        // list.
        //-------------------------------------------------------------------------
        private void PropertyList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(this.PropertyList.SelectedItems.Count.Equals(0))
            {
                this.CodeText.Clear();
                this.ValueList.Items.Clear();
            }
            else if(this.PropertyList.SelectedItems.Count >= 1)
            {
                GenerateQueryCode();
            }
     
        }

        //-------------------------------------------------------------------------
        // Generates the code in the query tab's generated code text box.
        // 
        //-------------------------------------------------------------------------
        public void GenerateQueryCode()
        {
            try
            {
                if(!this.ClassList.Text.Equals("")) 
                {
                    if(this.VbNetMenuItem.Checked)
                    {
                        GenerateVBNetQueryCode();
                    }
                    else if(this.CSharpMenuItem.Checked)
                    {
                        GenerateCSharpQueryCode();
                    }
                    else if(this.VbsMenuItem.Checked)
                    {
                        GenerateVBSQueryCode();
                    }
                }
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("WMI class or method not found.");
                else
                    MessageBox.Show(mErr.Message.ToString());
            }
        }

        //-------------------------------------------------------------------------
        // Generates the VBScript in the query tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateVBSQueryCode()
        {
            try 
            {
                string code = "";

                if(this.RemoteComputerMenu.Checked)
                {
                    code = code + "strComputer = \"" + this.TargetWindow.GetRemoteComputerName() + "\" " 
                        + Environment.NewLine +
                        "strDomain = \"" + this.TargetWindow.GetRemoteComputerDomain() + "\" " 
                        + Environment.NewLine +
                        "Wscript.StdOut.Write \"Please enter your user name:\"" +
                        Environment.NewLine +
                        "strUser = Wscript.StdIn.ReadLine "
                        + Environment.NewLine +
                        "Set objPassword = CreateObject(\"ScriptPW.Password\")" 
                        + Environment.NewLine +
                        "Wscript.StdOut.Write \"Please enter your password:\""
                        + Environment.NewLine +
                        "strPassword = objPassword.GetPassword()"
                        + Environment.NewLine +
                        "Wscript.Echo"
                        + Environment.NewLine + Environment.NewLine +
                        "Set objSWbemLocator = CreateObject(\"WbemScripting.SWbemLocator\") " 
                        + Environment.NewLine +
                        "Set objWMIService = objSWbemLocator.ConnectServer(strComputer, _ "
                        + Environment.NewLine +
                        "    \"" + this.NamespaceValue.Text +"\", _ " 
                        + Environment.NewLine +
                        "    strUser, _ "
                        + Environment.NewLine +
                        "    strPassword, _ "
                        + Environment.NewLine +
                        "    \"MS_409\", _ "
                        + Environment.NewLine +
                        "    \"ntlmdomain:\" + strDomain) "
                        + Environment.NewLine;
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    string delimStr = " ,\n";
                    char [] delimiter = delimStr.ToCharArray();
                    string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);


                    code = code + "arrComputers = Array(\"";
                    foreach (string s in split) 
                    {
                        code = code + s.Trim() + "\",\"";
                    }
                    string trimStr = ",\"";
                    char [] trim = trimStr.ToCharArray();
                    code = code.TrimEnd(trim) + "\")" +
                        Environment.NewLine +
                        "For Each strComputer In arrComputers" +
                        Environment.NewLine +
                        "   WScript.Echo" +
                        Environment.NewLine +
                        "   WScript.Echo \"==========================================\"" +
                        Environment.NewLine +
                        "   WScript.Echo \"Computer: \" & strComputer" +
                        Environment.NewLine +
                        "   WScript.Echo \"==========================================\"" +
                        Environment.NewLine + 
                        Environment.NewLine +
                       
                        "Set objWMIService = GetObject(\"winmgmts:\\\\\" & strComputer & \"\\" +
                        this.NamespaceValue.Text + "\") " 
                        + Environment.NewLine;
                }
                else
                {
                
                    code = code + "strComputer = \".\" " 
                        + Environment.NewLine +
                        "Set objWMIService = GetObject(\"winmgmts:\\\\\" & strComputer & \"\\" +
                        this.NamespaceValue.Text + "\") " 
                        + Environment.NewLine;
                }
 
                code = code + "Set colItems = objWMIService.ExecQuery( _" + Environment.NewLine +
                    "    \"SELECT * FROM " +
                    // Class from selection.
                    this.ClassList.Text;

                if(this.ValueList.SelectedItems.Count >= 1)
                {
                    string updatedValue = ValueList.SelectedItems[0].ToString().Replace("\\", "\\\\").Trim();
                    code = code + " WHERE " + updatedValue;
                }
                
                code = code + "\",,48) " + Environment.NewLine +
                    "For Each objItem in colItems " + Environment.NewLine +
                    "    Wscript.Echo \"-----------------------------------\"" +
                    Environment.NewLine +
                    "    Wscript.Echo \"" + this.ClassList.Text + " instance\"" +
                    Environment.NewLine +
                    "    Wscript.Echo \"-----------------------------------\"" +
                    Environment.NewLine;

                ManagementClass m = new ManagementClass(this.NamespaceValue.Text, this.ClassList.Text, null);

                
                for( int i=0; i < PropertyList.SelectedItems.Count; i++)
                {
                    if(m.Properties[PropertyList.SelectedItems[i].ToString()].IsArray)
                    {
                        code = code + "    If isNull(objItem." + PropertyList.SelectedItems[i].ToString() + ") Then" + Environment.NewLine +
                            "        Wscript.Echo \"" + PropertyList.SelectedItems[i].ToString() + ": \"" + Environment.NewLine +
                            "    Else" + Environment.NewLine +
                            "        Wscript.Echo \"" + PropertyList.SelectedItems[i].ToString() + ": \" & Join(objItem." + PropertyList.SelectedItems[i].ToString() +
                            ", \",\")" + System.Environment.NewLine +
                            "    End If" +
                            Environment.NewLine;
                    }
                    else
                    {
                        code = code + "    Wscript.Echo \"" + 
                            // Property from selection.
                            this.PropertyList.SelectedItems[i].ToString() +
                            ": \" & objItem." +
                            this.PropertyList.SelectedItems[i].ToString() +
                            Environment.NewLine;
                    }
                }

                if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code + "Next" + Environment.NewLine;
                }

                code = code + "Next";

                this.CodeText.Text = code;
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("Error creating code: WMI class not found.");
                else
                    MessageBox.Show("Error creating code: " + mErr.Message.ToString());
            }
			
        }

        //-------------------------------------------------------------------------
        // Generates the C# code in the query tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateCSharpQueryCode()
        {
            try
            {
                string code = "";

                if(this.LocalComputerMenu.Checked)
                {
                    code =  
                        "using System;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class MyWMIQuery" + Environment.NewLine +
                        "    {" + Environment.NewLine +
                        "        public static void Main()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            try" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                ManagementObjectSearcher searcher = " + Environment.NewLine +
                        "                    new ManagementObjectSearcher(\"" + this.NamespaceValue.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                        "                    \"SELECT * FROM " + this.ClassList.Text;

                    if(this.ValueList.SelectedItems.Count >= 1)
                    {
                        string updatedValue = ValueList.SelectedItems[0].ToString().Replace("\\", "\\\\").Trim();
                        code = code + " WHERE " + updatedValue;
                    }
                
                    code = code + "\"); " + Environment.NewLine + Environment.NewLine +
                        "                foreach (ManagementObject queryObj in searcher.Get())" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                    Console.WriteLine(\"-----------------------------------\");" + Environment.NewLine +
                        "                    Console.WriteLine(\"" + this.ClassList.Text + " instance\");" + Environment.NewLine +
                        "                    Console.WriteLine(\"-----------------------------------\");" + Environment.NewLine;

                    ManagementClass m = new ManagementClass(this.NamespaceValue.Text, this.ClassList.Text, null);
                
                    for( int i=0; i < PropertyList.SelectedItems.Count; i++)
                    {
                        if(m.Properties[PropertyList.SelectedItems[i].ToString()].IsArray)
                        {
                            // Determines the type of the array.
                            string type = "";
                            switch (m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString())
                            {
                                case "Char16":
                                    type = "Char";
                                    break;
                                case "Real64":
                                    type = "Double";
                                    break;
                                case "Real32":
                                    type = "Single";
                                    break;
                                case "SInt16":
                                    type = "Int16";
                                    break;
                                case "SInt32":
                                    type = "Int32";
                                    break;
                                case "SInt64":
                                    type = "Int64";
                                    break;
                                case "SInt8":
                                    type = "SByte";
                                    break;
                                case "UInt8":
                                    type = "Byte";
                                    break;
                                default:
                                    type = m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString();
                                    break;
                            }

                            code = code + Environment.NewLine + 
                                "                    if(queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"] == null)" + Environment.NewLine +
                                "                        Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"]);" + Environment.NewLine +
                                "                    else" + System.Environment.NewLine +
                                "                    {" + System.Environment.NewLine +
                                "                        " + type + "[] arr" + PropertyList.SelectedItems[i].ToString() + " = (" + type + "[])(queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"]);" + Environment.NewLine +
                                "                        foreach (" + type + " arrValue in arr" + PropertyList.SelectedItems[i].ToString() + ")" + System.Environment.NewLine +
                                "                        {" + System.Environment.NewLine +
                                "                            Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", arrValue);" + Environment.NewLine +
                                "                        }" + System.Environment.NewLine +
                                "                    }" +
                                Environment.NewLine;
                        }
                        else
                        {
                            code = code + 
                                "                    Console.WriteLine(\"" + 
                                // Property from selection.
                                this.PropertyList.SelectedItems[i].ToString() +
                                ": {0}\", queryObj[\"" +
                                this.PropertyList.SelectedItems[i].ToString() + "\"]);" +
                                Environment.NewLine;
                        }
                    }

                    code = code + 
                        "                }" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            catch (ManagementException e)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while querying for WMI data: \" + e.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}";
                }
                else if(this.RemoteComputerMenu.Checked)
                {
                    code = "using System;" + Environment.NewLine +
                        "using System.Drawing;" + Environment.NewLine +
                        "using System.Collections;" + Environment.NewLine +
                        "using System.ComponentModel;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        "using System.Data;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class MyQuerySample : System.Windows.Forms.Form" + Environment.NewLine +
                        "    {" + Environment.NewLine +
                        "        private System.Windows.Forms.Label userNameLabel;" + Environment.NewLine +
                        "        private System.Windows.Forms.TextBox userNameBox;" + Environment.NewLine +
                        "        private System.Windows.Forms.TextBox passwordBox;" + Environment.NewLine +
                        "        private System.Windows.Forms.Label passwordLabel;" + Environment.NewLine +
                        "        private System.Windows.Forms.Button OKButton;" + Environment.NewLine +
                        "        private System.Windows.Forms.Button cancelButton;" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        private System.ComponentModel.Container components = null;" + Environment.NewLine +
                        Environment.NewLine +
                        "        public MyQuerySample()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            InitializeComponent();" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        protected override void Dispose( bool disposing )" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            if( disposing )" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                if (components != null)" + Environment.NewLine + 
                        "                {" + Environment.NewLine +
                        "                    components.Dispose();" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            base.Dispose( disposing );" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        private void InitializeComponent()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            this.userNameLabel = new System.Windows.Forms.Label();" + Environment.NewLine +
                        "            this.userNameBox = new System.Windows.Forms.TextBox();" + Environment.NewLine +
                        "            this.passwordBox = new System.Windows.Forms.TextBox();" + Environment.NewLine +
                        "            this.passwordLabel = new System.Windows.Forms.Label();" + Environment.NewLine +
                        "            this.OKButton = new System.Windows.Forms.Button();" + Environment.NewLine +
                        "            this.cancelButton = new System.Windows.Forms.Button();" + Environment.NewLine +
                        "            this.SuspendLayout();" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // userNameLabel" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.userNameLabel.Location = new System.Drawing.Point(16, 8);" + Environment.NewLine +
                        "            this.userNameLabel.Name = \"userNameLabel\";" + Environment.NewLine +
                        "            this.userNameLabel.Size = new System.Drawing.Size(160, 32);" + Environment.NewLine +
                        "            this.userNameLabel.TabIndex = 0;" + Environment.NewLine +
                        "            this.userNameLabel.Text = \"Enter the user name for the remote computer:\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // userNameBox" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.userNameBox.Location = new System.Drawing.Point(160, 16);" + Environment.NewLine +
                        "            this.userNameBox.Name = \"userNameBox\";" + Environment.NewLine +
                        "            this.userNameBox.Size = new System.Drawing.Size(192, 20);" + Environment.NewLine +
                        "            this.userNameBox.TabIndex = 1;" + Environment.NewLine +
                        "            this.userNameBox.Text = \"\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // passwordBox" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.passwordBox.Location = new System.Drawing.Point(160, 48);" + Environment.NewLine +
                        "            this.passwordBox.Name = \"passwordBox\";" + Environment.NewLine +
                        "            this.passwordBox.PasswordChar = '*';" + Environment.NewLine +
                        "            this.passwordBox.Size = new System.Drawing.Size(192, 20);" + Environment.NewLine +
                        "            this.passwordBox.TabIndex = 3;" + Environment.NewLine +
                        "            this.passwordBox.Text = \"\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // passwordLabel" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.passwordLabel.Location = new System.Drawing.Point(16, 48);" + Environment.NewLine +
                        "            this.passwordLabel.Name = \"passwordLabel\";" + Environment.NewLine +
                        "            this.passwordLabel.Size = new System.Drawing.Size(160, 32);" + Environment.NewLine +
                        "            this.passwordLabel.TabIndex = 2;" + Environment.NewLine +
                        "            this.passwordLabel.Text = \"Enter the password for the remote computer:\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // OKButton" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.OKButton.Location = new System.Drawing.Point(40, 88);" + Environment.NewLine +
                        "            this.OKButton.Name = \"OKButton\";" + Environment.NewLine +
                        "            this.OKButton.Size = new System.Drawing.Size(128, 23);" + Environment.NewLine +
                        "            this.OKButton.TabIndex = 4;" + Environment.NewLine +
                        "            this.OKButton.Text = \"OK\";" + Environment.NewLine +
                        "            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // cancelButton" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;" + Environment.NewLine +
                        "            this.cancelButton.Location = new System.Drawing.Point(200, 88);" + Environment.NewLine +
                        "            this.cancelButton.Name = \"cancelButton\";" + Environment.NewLine +
                        "            this.cancelButton.Size = new System.Drawing.Size(128, 23);" + Environment.NewLine +
                        "            this.cancelButton.TabIndex = 5;" + Environment.NewLine +
                        "            this.cancelButton.Text = \"Cancel\";" + Environment.NewLine +
                        "            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // MyQuerySample" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.AcceptButton = this.OKButton;" + Environment.NewLine +
                        "            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);" + Environment.NewLine +
                        "            this.CancelButton = this.cancelButton;" + Environment.NewLine +
                        "            this.ClientSize = new System.Drawing.Size(368, 130);" + Environment.NewLine +
                        "            this.ControlBox = false;" + Environment.NewLine +
                        "            this.Controls.Add(this.cancelButton);" + Environment.NewLine +
                        "            this.Controls.Add(this.OKButton);" + Environment.NewLine +
                        "            this.Controls.Add(this.passwordBox);" + Environment.NewLine +
                        "            this.Controls.Add(this.passwordLabel);" + Environment.NewLine +
                        "            this.Controls.Add(this.userNameBox);" + Environment.NewLine +
                        "            this.Controls.Add(this.userNameLabel);" + Environment.NewLine +
                        "            this.Name = \"MyQuerySample\";" + Environment.NewLine +
                        "            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;" + Environment.NewLine +
                        "            this.Text = \"Remote Connection\";" + Environment.NewLine +
                        "            this.ResumeLayout(false);" + Environment.NewLine +
                        Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        [STAThread]" + Environment.NewLine +
                        "        static void Main() " + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            Application.Run(new MyQuerySample());" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        private void OKButton_Click(object sender, System.EventArgs e)" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            try" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                ConnectionOptions connection = new ConnectionOptions();" + Environment.NewLine +
                        "                connection.Username = userNameBox.Text;" + Environment.NewLine +
                        "                connection.Password = passwordBox.Text;" + Environment.NewLine +
                        "                connection.Authority = \"ntlmdomain:" + this.TargetWindow.GetRemoteComputerDomain() + "\";" + Environment.NewLine +
                        Environment.NewLine +
                        "                ManagementScope scope = new ManagementScope(" + Environment.NewLine +
                        "                    \"\\\\\\\\" + this.TargetWindow.GetRemoteComputerName() + "\\\\" + this.NamespaceValue.Text.Replace("\\", "\\\\") + "\", connection);" + Environment.NewLine +
                        "                scope.Connect();" + Environment.NewLine +
                        Environment.NewLine +
                        "                ObjectQuery query= new ObjectQuery(" + Environment.NewLine +
                        "                    \"SELECT * FROM " + this.ClassList.Text;
				
                    if(this.ValueList.SelectedItems.Count >= 1)
                    {
                        string updatedValue = ValueList.SelectedItems[0].ToString().Replace("\\", "\\\\").Trim();
                        code = code + " WHERE " + updatedValue;
                    }
                
                    code = code + "\"); " + Environment.NewLine + Environment.NewLine +
                        "                ManagementObjectSearcher searcher = " + Environment.NewLine +
                        "                    new ManagementObjectSearcher(scope, query);" + Environment.NewLine + Environment.NewLine +
                        "                foreach (ManagementObject queryObj in searcher.Get())" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                    Console.WriteLine(\"-----------------------------------\");" + Environment.NewLine +
                        "                    Console.WriteLine(\"" + this.ClassList.Text + " instance\");" + Environment.NewLine +
                        "                    Console.WriteLine(\"-----------------------------------\");" + Environment.NewLine;

                    ManagementClass m = new ManagementClass(this.NamespaceValue.Text, this.ClassList.Text, null);
                
                    for( int i=0; i < PropertyList.SelectedItems.Count; i++)
                    {
                        if(m.Properties[PropertyList.SelectedItems[i].ToString()].IsArray)
                        {
                            // Determines the type of the array.
                            string type = "";
                            switch (m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString())
                            {
                                case "Char16":
                                    type = "Char";
                                    break;
                                case "Real64":
                                    type = "Double";
                                    break;
                                case "Real32":
                                    type = "Single";
                                    break;
                                case "SInt16":
                                    type = "Int16";
                                    break;
                                case "SInt32":
                                    type = "Int32";
                                    break;
                                case "SInt64":
                                    type = "Int64";
                                    break;
                                case "SInt8":
                                    type = "SByte";
                                    break;
                                case "UInt8":
                                    type = "Byte";
                                    break;
                                default:
                                    type = m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString();
                                    break;
                            }

                            code = code + Environment.NewLine + 
                                "                    if(queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"] == null)" + Environment.NewLine +
                                "                        Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"]);" + Environment.NewLine +
                                "                    else" + System.Environment.NewLine +
                                "                    {" + System.Environment.NewLine +
                                "                        " + type + "[] arr" + PropertyList.SelectedItems[i].ToString() + " = (" + type + "[])(queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"]);" + Environment.NewLine +
                                "                        foreach (" + type + " arrValue in arr" + PropertyList.SelectedItems[i].ToString() + ")" + System.Environment.NewLine +
                                "                        {" + System.Environment.NewLine +
                                "                            Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", arrValue);" + Environment.NewLine +
                                "                        }" + System.Environment.NewLine +
                                "                    }" +
                                Environment.NewLine;
                        }
                        else
                        {
                            code = code + "                    Console.WriteLine(\"" + 
                                // Property from selection.
                                this.PropertyList.SelectedItems[i].ToString() +
                                ": {0}\", queryObj[\"" +
                                this.PropertyList.SelectedItems[i].ToString() + "\"]);" +
                                Environment.NewLine;
                        }
                    }

                    code = code + 
                        "                }" + Environment.NewLine + 
                        "                Close();" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            catch(ManagementException err)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while querying for WMI data: \" + err.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            catch(System.UnauthorizedAccessException unauthorizedErr)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"Connection error (user name or password might be incorrect): \" + unauthorizedErr.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        private void cancelButton_Click(object sender, System.EventArgs e)" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            Close();" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}" + Environment.NewLine;

                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    code =  
                        "using System;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class MyWMIQuery" + Environment.NewLine +
                        "    {" + Environment.NewLine +
                        "        public static void Main()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            try" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                string[] arrComputers = {\"";

                    string delimStr = " ,\n";
                    char [] delimiter = delimStr.ToCharArray();
                    string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);

                    foreach (string s in split) 
                    {
                        code = code + s.Trim() + "\",\"";
                    }
                    string trimStr = ",\"";
                    char [] trim = trimStr.ToCharArray();
                    code = code.TrimEnd(trim) + "\"};" +
                        Environment.NewLine + 
                        "                foreach (string strComputer in arrComputers)" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine +
                        "                    Console.WriteLine(\"Computer: \" + strComputer);" + Environment.NewLine +
                        "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine + Environment.NewLine +
                        "                    ManagementObjectSearcher searcher = " + Environment.NewLine +
                        "                        new ManagementObjectSearcher(" + Environment.NewLine +
                        "                        \"\\\\\\\\\" + strComputer + \"\\\\" + this.NamespaceValue.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                        "                        \"SELECT * FROM " + this.ClassList.Text;

                    if(this.ValueList.SelectedItems.Count >= 1)
                    {
                        string updatedValue = ValueList.SelectedItems[0].ToString().Replace("\\", "\\\\").Trim();
                        code = code + " WHERE " + updatedValue;
                    }
                
                    code = code + "\"); " + Environment.NewLine + Environment.NewLine +
                        "                    foreach (ManagementObject queryObj in searcher.Get())" + Environment.NewLine +
                        "                    {" + Environment.NewLine +
                        "                        Console.WriteLine(\"-----------------------------------\");" + Environment.NewLine +
                        "                        Console.WriteLine(\"" + this.ClassList.Text + " instance\");" + Environment.NewLine +
                        "                        Console.WriteLine(\"-----------------------------------\");" + Environment.NewLine;

                    ManagementClass m = new ManagementClass(this.NamespaceValue.Text, this.ClassList.Text, null);
                
                    for( int i=0; i < PropertyList.SelectedItems.Count; i++)
                    {
                        if(m.Properties[PropertyList.SelectedItems[i].ToString()].IsArray)
                        {
                            // Determines the type of the array.
                            string type = "";
                            switch (m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString())
                            {
                                case "Char16":
                                    type = "Char";
                                    break;
                                case "Real64":
                                    type = "Double";
                                    break;
                                case "Real32":
                                    type = "Single";
                                    break;
                                case "SInt16":
                                    type = "Int16";
                                    break;
                                case "SInt32":
                                    type = "Int32";
                                    break;
                                case "SInt64":
                                    type = "Int64";
                                    break;
                                case "SInt8":
                                    type = "SByte";
                                    break;
                                case "UInt8":
                                    type = "Byte";
                                    break;
                                default:
                                    type = m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString();
                                    break;
                            }

                            code = code + Environment.NewLine + 
                                "                        if(queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"] == null)" + Environment.NewLine +
                                "                            Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"]);" + Environment.NewLine +
                                "                        else" + System.Environment.NewLine +
                                "                        {" + System.Environment.NewLine +
                                "                            " + type + "[] arr" + PropertyList.SelectedItems[i].ToString() + " = (" + type + "[])(queryObj[\"" + PropertyList.SelectedItems[i].ToString() + "\"]);" + Environment.NewLine +
                                "                            foreach (" + type + " arrValue in arr" + PropertyList.SelectedItems[i].ToString() + ")" + System.Environment.NewLine +
                                "                            {" + System.Environment.NewLine +
                                "                                Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", arrValue);" + Environment.NewLine +
                                "                            }" + System.Environment.NewLine +
                                "                        }" +
                                Environment.NewLine;
                        }
                        else
                        {
                            code = code + "                        Console.WriteLine(\"" + 
                                // Property from selections.
                                this.PropertyList.SelectedItems[i].ToString() +
                                ": {0}\", queryObj[\"" +
                                this.PropertyList.SelectedItems[i].ToString() + "\"]);" +
                                Environment.NewLine;
                        }
                    }

                    code = code + "                    }" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            catch(ManagementException err)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while querying for WMI data: \" + err.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}";
                }

                this.CodeText.Text = code;
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("Error creating code: WMI class not found.");
                else
                    MessageBox.Show("Error creating code: " + mErr.Message.ToString());
            }
			
        }

        //-------------------------------------------------------------------------
        // Generates the VB code in the query tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateVBNetQueryCode()
        {
            try
            {
                string code = "";

                if(this.LocalComputerMenu.Checked)
                {
                    code =  
                        "Imports System" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        "Imports System.Windows.Forms" +Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class MyWMIQuery" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                        Environment.NewLine +
                        "            Try" + Environment.NewLine +
                        "                Dim searcher As New ManagementObjectSearcher( _" + Environment.NewLine +
                        "                    \"" + this.NamespaceValue.Text + "\", _" + Environment.NewLine +
                        "                    \"SELECT * FROM " + this.ClassList.Text;

                    if(this.ValueList.SelectedItems.Count >= 1)
                    {
                        string updatedValue = ValueList.SelectedItems[0].ToString().Replace("\\", "\\\\").Trim();
                        code = code + " WHERE " + updatedValue;
                    }
                
                    code = code + "\") " + Environment.NewLine + Environment.NewLine +
                        "                For Each queryObj As ManagementObject in searcher.Get()" + Environment.NewLine +
                        Environment.NewLine +
                        "                    Console.WriteLine(\"-----------------------------------\")" + Environment.NewLine +
                        "                    Console.WriteLine(\"" + this.ClassList.Text + " instance\")" + Environment.NewLine +
                        "                    Console.WriteLine(\"-----------------------------------\")" + Environment.NewLine;

                    ManagementClass m = new ManagementClass(this.NamespaceValue.Text, this.ClassList.Text, null);
                
                    for( int i=0; i < PropertyList.SelectedItems.Count; i++)
                    {
                        if(m.Properties[PropertyList.SelectedItems[i].ToString()].IsArray)
                        {
                            // Determines the type of the array.
                            string type = "";
                            switch (m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString())
                            {
                                case "Char16":
                                    type = "Char";
                                    break;
                                case "Real64":
                                    type = "Double";
                                    break;
                                case "Real32":
                                    type = "Single";
                                    break;
                                case "SInt16":
                                    type = "Int16";
                                    break;
                                case "SInt32":
                                    type = "Int32";
                                    break;
                                case "SInt64":
                                    type = "Int64";
                                    break;
                                case "SInt8":
                                    type = "SByte";
                                    break;
                                case "UInt8":
                                    type = "Byte";
                                    break;
                                default:
                                    type = m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString();
                                    break;
                            }

                            code = code + Environment.NewLine + "                    If queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\") Is Nothing Then" + Environment.NewLine +
                                "                        Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\"))" + Environment.NewLine +
                                "                    Else" + System.Environment.NewLine +
                                "                        Dim arr" + PropertyList.SelectedItems[i].ToString() + " As " + type + "()" + Environment.NewLine +
                                "                        arr" + PropertyList.SelectedItems[i].ToString() + " = queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\")" + Environment.NewLine +
                                "                        For Each arrValue As " + type + " In arr" + PropertyList.SelectedItems[i].ToString() + "" + System.Environment.NewLine +
                                "                            Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", arrValue)" + Environment.NewLine +
                                "                        Next" + System.Environment.NewLine +
                                "                    End If" +
                                Environment.NewLine;
                        }
                        else
                        {
                            code = code + "                    Console.WriteLine(\"" + 
                                // Property from selection.
                                this.PropertyList.SelectedItems[i].ToString() +
                                ": {0}\", queryObj(\"" +
                                this.PropertyList.SelectedItems[i].ToString() + "\"))" +
                                Environment.NewLine;
                        }
                    }

                    code = code + "                Next" + Environment.NewLine +
                        "            Catch err As ManagementException" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while querying for WMI data: \" & err.Message)" + Environment.NewLine +
                        "            End Try" + Environment.NewLine +
                        "        End Function" + Environment.NewLine +
                        "    End Class" + Environment.NewLine +
                        "End Namespace";
                }
                else if(this.RemoteComputerMenu.Checked)
                {
                    code = "Imports System" + Environment.NewLine +
                        "Imports System.Drawing" + Environment.NewLine +
                        "Imports System.Collections" + Environment.NewLine +
                        "Imports System.ComponentModel" + Environment.NewLine +
                        "Imports System.Windows.Forms" + Environment.NewLine +
                        "Imports System.Data" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class MyQuerySample " + Environment.NewLine + "        Inherits System.Windows.Forms.Form" + Environment.NewLine +
                        Environment.NewLine +
                        "        Friend WithEvents userNameLabel As System.Windows.Forms.Label" + Environment.NewLine +
                        "        Friend WithEvents userNameBox As System.Windows.Forms.TextBox" + Environment.NewLine +
                        "        Friend WithEvents passwordBox As System.Windows.Forms.TextBox" + Environment.NewLine +
                        "        Friend WithEvents passwordLabel As System.Windows.Forms.Label" + Environment.NewLine +
                        "        Friend WithEvents OKButton As System.Windows.Forms.Button" + Environment.NewLine +
                        "        Friend WithEvents closeButton As System.Windows.Forms.Button" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        Private components As System.ComponentModel.IContainer" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Sub New()" + Environment.NewLine + 
                        "            MyBase.New()" + Environment.NewLine +
                        Environment.NewLine +
                        "            InitializeComponent()" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "            If disposing Then" + Environment.NewLine +
						
                        "                If Not (components Is Nothing) Then" + Environment.NewLine + 
						
                        "                    components.Dispose()" + Environment.NewLine +
                        "                End If" + Environment.NewLine +
                        "            End If" + Environment.NewLine +
                        "            MyBase.Dispose(disposing)" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()" + Environment.NewLine +
                        Environment.NewLine +
                        "            Me.userNameLabel = new System.Windows.Forms.Label" + Environment.NewLine +
                        "            Me.userNameBox = new System.Windows.Forms.TextBox" + Environment.NewLine +
                        "            Me.passwordBox = new System.Windows.Forms.TextBox" + Environment.NewLine +
                        "            Me.passwordLabel = new System.Windows.Forms.Label" + Environment.NewLine +
                        "            Me.OKButton = new System.Windows.Forms.Button" + Environment.NewLine +
                        "            Me.closeButton = new System.Windows.Forms.Button" + Environment.NewLine +
                        "            Me.SuspendLayout()" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' userNameLabel" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.userNameLabel.Location = New System.Drawing.Point(16, 8)" + Environment.NewLine +
                        "            Me.userNameLabel.Name = \"userNameLabel\"" + Environment.NewLine +
                        "            Me.userNameLabel.Size = New System.Drawing.Size(160, 32)" + Environment.NewLine +
                        "            Me.userNameLabel.TabIndex = 0" + Environment.NewLine +
                        "            Me.userNameLabel.Text = \"Enter the user name for the remote computer:\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' userNameBox" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.userNameBox.Location = New System.Drawing.Point(160, 16)" + Environment.NewLine +
                        "            Me.userNameBox.Name = \"userNameBox\"" + Environment.NewLine +
                        "            Me.userNameBox.Size = New System.Drawing.Size(192, 20)" + Environment.NewLine +
                        "            Me.userNameBox.TabIndex = 1" + Environment.NewLine +
                        "            Me.userNameBox.Text = \"\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' passwordBox" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.passwordBox.Location = New System.Drawing.Point(160, 48)" + Environment.NewLine +
                        "            Me.passwordBox.Name = \"passwordBox\"" + Environment.NewLine +
                        "            Me.passwordBox.PasswordChar = \"*\"" + Environment.NewLine +
                        "            Me.passwordBox.Size = new System.Drawing.Size(192, 20)" + Environment.NewLine +
                        "            Me.passwordBox.TabIndex = 3" + Environment.NewLine +
                        "            Me.passwordBox.Text = \"\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' passwordLabel" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.passwordLabel.Location = new System.Drawing.Point(16, 48)" + Environment.NewLine +
                        "            Me.passwordLabel.Name = \"passwordLabel\"" + Environment.NewLine +
                        "            Me.passwordLabel.Size = new System.Drawing.Size(160, 32)" + Environment.NewLine +
                        "            Me.passwordLabel.TabIndex = 2" + Environment.NewLine +
                        "            Me.passwordLabel.Text = \"Enter the password for the remote computer:\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' OKButton" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.OKButton.Location = New System.Drawing.Point(40, 88)" + Environment.NewLine +
                        "            Me.OKButton.Name = \"OKButton\"" + Environment.NewLine +
                        "            Me.OKButton.Size = new System.Drawing.Size(128, 23)" + Environment.NewLine +
                        "            Me.OKButton.TabIndex = 4" + Environment.NewLine +
                        "            Me.OKButton.Text = \"OK\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' closeButton" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel" + Environment.NewLine +
                        "            Me.closeButton.Location = New System.Drawing.Point(200, 88)" + Environment.NewLine +
                        "            Me.closeButton.Name = \"closeButton\"" + Environment.NewLine +
                        "            Me.closeButton.Size = New System.Drawing.Size(128, 23)" + Environment.NewLine +
                        "            Me.closeButton.TabIndex = 5" + Environment.NewLine +
                        "            Me.closeButton.Text = \"Cancel\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' MyQuerySample" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.AcceptButton = Me.OKButton" + Environment.NewLine +
                        "            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)" + Environment.NewLine +
                        "            Me.CancelButton = Me.closeButton" + Environment.NewLine +
                        "            Me.ClientSize = New System.Drawing.Size(368, 130)" + Environment.NewLine +
                        "            Me.ControlBox = false" + Environment.NewLine +
                        "            Me.Controls.Add(Me.closeButton)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.OKButton)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.passwordBox)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.passwordLabel)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.userNameBox)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.userNameLabel)" + Environment.NewLine +
                        "            Me.Name = \"MyQuerySample\"" + Environment.NewLine +
                        "            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen" + Environment.NewLine +
                        "            Me.Text = \"Remote Connection\"" + Environment.NewLine +
                        "            Me.ResumeLayout(false)" + Environment.NewLine +
                        Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                        Environment.NewLine +
                        "            Application.Run(New MyQuerySample)" + Environment.NewLine +
                        "        End Function" + Environment.NewLine +
                        Environment.NewLine +
                        "        Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "            Try" + Environment.NewLine +
                        "                Dim connection As New ConnectionOptions" + Environment.NewLine +
                        "                connection.Username = userNameBox.Text" + Environment.NewLine +
                        "                connection.Password = passwordBox.Text" + Environment.NewLine +
                        "                connection.Authority = \"ntlmdomain:" + this.TargetWindow.GetRemoteComputerDomain() + "\"" + Environment.NewLine +
                        Environment.NewLine +
                        "                Dim scope As New ManagementScope( _" + Environment.NewLine +
                        "                    \"\\\\" + this.TargetWindow.GetRemoteComputerName() + "\\" + this.NamespaceValue.Text + "\", connection)" + Environment.NewLine +
                        "                scope.Connect()" + Environment.NewLine +
                        Environment.NewLine +
                        "                Dim query As New ObjectQuery( _" + Environment.NewLine +
                        "                    \"SELECT * FROM " + this.ClassList.Text;
				
                    if(this.ValueList.SelectedItems.Count >= 1)
                    {
                        string updatedValue = ValueList.SelectedItems[0].ToString().Replace("\\", "\\\\").Trim();
                        code = code + " WHERE " + updatedValue;
                    }
                
                    code = code + "\") " + Environment.NewLine + Environment.NewLine +
                        "                Dim searcher As New ManagementObjectSearcher(scope, query) " + Environment.NewLine +
                        Environment.NewLine +
                        "                For Each queryObj As ManagementObject in searcher.Get()" + Environment.NewLine +
                        Environment.NewLine +
                        "                    Console.WriteLine(\"-----------------------------------\")" + Environment.NewLine +
                        "                    Console.WriteLine(\"" + this.ClassList.Text + " instance\")" + Environment.NewLine +
                        "                    Console.WriteLine(\"-----------------------------------\")" + Environment.NewLine;

                    ManagementClass m = new ManagementClass(this.NamespaceValue.Text, this.ClassList.Text, null);
                
                    for( int i=0; i < PropertyList.SelectedItems.Count; i++)
                    {
                        if(m.Properties[PropertyList.SelectedItems[i].ToString()].IsArray)
                        {
                            // Determines the type of the array.
                            string type = "";
                            switch (m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString())
                            {
                                case "Char16":
                                    type = "Char";
                                    break;
                                case "Real64":
                                    type = "Double";
                                    break;
                                case "Real32":
                                    type = "Single";
                                    break;
                                case "SInt16":
                                    type = "Int16";
                                    break;
                                case "SInt32":
                                    type = "Int32";
                                    break;
                                case "SInt64":
                                    type = "Int64";
                                    break;
                                case "SInt8":
                                    type = "SByte";
                                    break;
                                case "UInt8":
                                    type = "Byte";
                                    break;
                                default:
                                    type = m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString();
                                    break;
                            }

                            code = code + Environment.NewLine + "                    If queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\") Is Nothing Then" + Environment.NewLine +
                                "                        Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\"))" + Environment.NewLine +
                                "                    Else" + System.Environment.NewLine +
                                "                        Dim arr" + PropertyList.SelectedItems[i].ToString() + " As " + type + "()" + Environment.NewLine +
                                "                        arr" + PropertyList.SelectedItems[i].ToString() + " = queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\")" + Environment.NewLine +
                                "                        For Each arrValue As " + type + " In arr" + PropertyList.SelectedItems[i].ToString() + "" + System.Environment.NewLine +
                                "                            Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", arrValue)" + Environment.NewLine +
                                "                        Next" + System.Environment.NewLine +
                                "                    End If" +
                                Environment.NewLine;
                        }
                        else
                        {
                            code = code + "                    Console.WriteLine(\"" + 
                                // Property from selection.
                                this.PropertyList.SelectedItems[i].ToString() +
                                ": {0}\", queryObj(\"" +
                                this.PropertyList.SelectedItems[i].ToString() + "\"))" +
                                Environment.NewLine;
                        }
                    }

                    code = code + "                Next" + Environment.NewLine + Environment.NewLine + "                Close()" + Environment.NewLine +
                        "            Catch err As ManagementException" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while querying for WMI data: \" & err.Message)" + Environment.NewLine +
                        "            Catch unauthorizedErr As System.UnauthorizedAccessException" + Environment.NewLine +
                        Environment.NewLine +
                        "                MessageBox.Show(\"Connection error (user name or password might be incorrect): \" & unauthorizedErr.Message)" + Environment.NewLine +
                        "            End Try" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        Private Sub closeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles closeButton.Click" + Environment.NewLine +
                        Environment.NewLine +
                        "            Close()" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        "    End Class" + Environment.NewLine +
                        "End Namespace" + Environment.NewLine;

                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    code =  
                        "Imports System" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        "Imports System.Windows.Forms" + Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class MyWMIQuery" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                        Environment.NewLine +
                        "            Try" + Environment.NewLine +
                        "                Dim arrComputers As String() = _ " + Environment.NewLine +
                        "                    {\"";

                    string delimStr = " ,\n";
                    char [] delimiter = delimStr.ToCharArray();
                    string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);

                    foreach (string s in split) 
                    {
                        code = code + s.Trim() + "\",\"";
                    }
                    string trimStr = ",\"";
                    char [] trim = trimStr.ToCharArray();
                    code = code.TrimEnd(trim) + "\"}" +
                        Environment.NewLine +
                        "                For Each strComputer As String In arrComputers" + Environment.NewLine +
                        Environment.NewLine +
                        "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine +
                        "                    Console.WriteLine(\"Computer: \" & strComputer)" + Environment.NewLine +
                        "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine + Environment.NewLine +
                        "                    Dim searcher As New ManagementObjectSearcher( _" + Environment.NewLine +
                        "                        \"\\\\\" + strComputer + \"\\" + this.NamespaceValue.Text + "\", _" + Environment.NewLine +
                        "                        \"SELECT * FROM " + this.ClassList.Text;

                    if(this.ValueList.SelectedItems.Count >= 1)
                    {
                        string updatedValue = ValueList.SelectedItems[0].ToString().Replace("\\", "\\\\").Trim();
                        code = code + " WHERE " + updatedValue;
                    }
                
                    code = code + "\") " + Environment.NewLine + Environment.NewLine +
                        "                    For Each queryObj As ManagementObject in searcher.Get()" + Environment.NewLine +
                        Environment.NewLine +
                        "                        Console.WriteLine(\"-----------------------------------\")" + Environment.NewLine +
                        "                        Console.WriteLine(\"" + this.ClassList.Text + " instance\")" + Environment.NewLine +
                        "                        Console.WriteLine(\"-----------------------------------\")" + Environment.NewLine;

                    ManagementClass m = new ManagementClass(this.NamespaceValue.Text, this.ClassList.Text, null);
                
                    for( int i=0; i < PropertyList.SelectedItems.Count; i++)
                    {
                        if(m.Properties[PropertyList.SelectedItems[i].ToString()].IsArray)
                        {
                            // Determines the type of the array.
                            string type = "";
                            switch (m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString())
                            {
                                case "Char16":
                                    type = "Char";
                                    break;
                                case "Real64":
                                    type = "Double";
                                    break;
                                case "Real32":
                                    type = "Single";
                                    break;
                                case "SInt16":
                                    type = "Int16";
                                    break;
                                case "SInt32":
                                    type = "Int32";
                                    break;
                                case "SInt64":
                                    type = "Int64";
                                    break;
                                case "SInt8":
                                    type = "SByte";
                                    break;
                                case "UInt8":
                                    type = "Byte";
                                    break;
                                default:
                                    type = m.Properties[PropertyList.SelectedItems[i].ToString()].Type.ToString();
                                    break;
                            }

                            code = code + Environment.NewLine + "                        If queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\") Is Nothing Then" + Environment.NewLine +
                                "                            Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\"))" + Environment.NewLine +
                                "                        Else" + System.Environment.NewLine +
                                "                            Dim arr" + PropertyList.SelectedItems[i].ToString() + " As " + type + "()" + Environment.NewLine +
                                "                            arr" + PropertyList.SelectedItems[i].ToString() + " = queryObj(\"" + PropertyList.SelectedItems[i].ToString() + "\")" + Environment.NewLine +
                                "                            For Each arrValue As " + type + " In arr" + PropertyList.SelectedItems[i].ToString() + "" + System.Environment.NewLine +
                                "                                Console.WriteLine(\"" + PropertyList.SelectedItems[i].ToString() + ": {0}\", arrValue)" + Environment.NewLine +
                                "                            Next" + System.Environment.NewLine +
                                "                        End If" +
                                Environment.NewLine;
                        }
                        else
                        {
                            code = code + "                        Console.WriteLine(\"" + 
                                // Property from selection.
                                this.PropertyList.SelectedItems[i].ToString() +
                                ": {0}\", queryObj(\"" +
                                this.PropertyList.SelectedItems[i].ToString() + "\"))" +
                                Environment.NewLine;
                        }
                    }

                    code = code + "                    Next" + Environment.NewLine +
                        "                Next" + Environment.NewLine +
                        "            Catch err As ManagementException" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while querying for WMI data: \" & err.Message)" + Environment.NewLine +
                        "            End Try" + Environment.NewLine +
                        "        End Function" + Environment.NewLine +
                        "    End Class" + Environment.NewLine +
                        "End Namespace";
                }

                this.CodeText.Text = code;
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("Error creating code: WMI class not found.");
                else
                    MessageBox.Show("Error creating code: " + mErr.Message.ToString());
            }
			
        }


        //-------------------------------------------------------------------------
        // Returns true if a static method is selected in the method tab's 
        // method list, and returns false otherwise.
        //-------------------------------------------------------------------------
        private bool IsStaticMethodSelected()
        {
            bool staticFlag = false;
            // Checks to see if a static method is selected in the method list.
            
            ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);
            MethodData mData = c.Methods[this.MethodList.Text];

            // Check each qualifier to see if it is static.
            foreach ( System.Management.QualifierData qualifier in mData.Qualifiers)
            {
                if(qualifier.Name.Equals("Static"))
                {
                    staticFlag = true;
                }
            }       
            return staticFlag;
        }

        //-------------------------------------------------------------------------
        // Generates the code in the method tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        public void GenerateMethodCode()
        {
            try
            {
                if(!this.ClassList_m.Text.Equals("")) 
                {
                    if(this.VbNetMenuItem.Checked)
                    {
                        GenerateVBNetMethodCode();
                    }
                    else if(this.CSharpMenuItem.Checked)
                    {
                        GenerateCSharpMethodCode();
                    }
                    else if(this.VbsMenuItem.Checked)
                    {
                        GenerateVBSMethodCode();
                    }
                }
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("Error creating code: WMI class or method not found.");
                else
                    MessageBox.Show("Error creating code: " + mErr.Message.ToString());
            }
        }

        //-------------------------------------------------------------------------
        // Generates the VB code in the method tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateVBNetMethodCode()
        {
            bool staticFlag = this.IsStaticMethodSelected();
            string buffer = "";
            if(this.GroupRemoteComputerMenu.Checked)
                buffer = "    ";

            if(this.MethodList.Items.Count > 0) 
            {
                string code = "";

                if(this.RemoteComputerMenu.Checked)
                {
                    code = "Imports System" + Environment.NewLine +
                        "Imports System.Drawing" + Environment.NewLine +
                        "Imports System.Collections" + Environment.NewLine +
                        "Imports System.ComponentModel" + Environment.NewLine +
                        "Imports System.Windows.Forms" + Environment.NewLine +
                        "Imports System.Data" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class CallWMIMethod " + Environment.NewLine + "        Inherits System.Windows.Forms.Form" + Environment.NewLine +
                        Environment.NewLine +
                        "        Friend WithEvents userNameLabel As System.Windows.Forms.Label" + Environment.NewLine +
                        "        Friend WithEvents userNameBox As System.Windows.Forms.TextBox" + Environment.NewLine +
                        "        Friend WithEvents passwordBox As System.Windows.Forms.TextBox" + Environment.NewLine +
                        "        Friend WithEvents passwordLabel As System.Windows.Forms.Label" + Environment.NewLine +
                        "        Friend WithEvents OKButton As System.Windows.Forms.Button" + Environment.NewLine +
                        "        Friend WithEvents closeButton As System.Windows.Forms.Button" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        Private components As System.ComponentModel.IContainer" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Sub New()" + Environment.NewLine + 
                        "            MyBase.New()" + Environment.NewLine +
                        Environment.NewLine +
                        "            InitializeComponent()" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "            If disposing Then" + Environment.NewLine +
						
                        "                If Not (components Is Nothing) Then" + Environment.NewLine + 
						
                        "                    components.Dispose()" + Environment.NewLine +
                        "                End If" + Environment.NewLine +
                        "            End If" + Environment.NewLine +
                        "            MyBase.Dispose(disposing)" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()" + Environment.NewLine +
                        Environment.NewLine +
                        "            Me.userNameLabel = new System.Windows.Forms.Label" + Environment.NewLine +
                        "            Me.userNameBox = new System.Windows.Forms.TextBox" + Environment.NewLine +
                        "            Me.passwordBox = new System.Windows.Forms.TextBox" + Environment.NewLine +
                        "            Me.passwordLabel = new System.Windows.Forms.Label" + Environment.NewLine +
                        "            Me.OKButton = new System.Windows.Forms.Button" + Environment.NewLine +
                        "            Me.closeButton = new System.Windows.Forms.Button" + Environment.NewLine +
                        "            Me.SuspendLayout()" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' userNameLabel" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.userNameLabel.Location = New System.Drawing.Point(16, 8)" + Environment.NewLine +
                        "            Me.userNameLabel.Name = \"userNameLabel\"" + Environment.NewLine +
                        "            Me.userNameLabel.Size = New System.Drawing.Size(160, 32)" + Environment.NewLine +
                        "            Me.userNameLabel.TabIndex = 0" + Environment.NewLine +
                        "            Me.userNameLabel.Text = \"Enter the user name for the remote computer:\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' userNameBox" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.userNameBox.Location = New System.Drawing.Point(160, 16)" + Environment.NewLine +
                        "            Me.userNameBox.Name = \"userNameBox\"" + Environment.NewLine +
                        "            Me.userNameBox.Size = New System.Drawing.Size(192, 20)" + Environment.NewLine +
                        "            Me.userNameBox.TabIndex = 1" + Environment.NewLine +
                        "            Me.userNameBox.Text = \"\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' passwordBox" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.passwordBox.Location = New System.Drawing.Point(160, 48)" + Environment.NewLine +
                        "            Me.passwordBox.Name = \"passwordBox\"" + Environment.NewLine +
                        "            Me.passwordBox.PasswordChar = \"*\"" + Environment.NewLine +
                        "            Me.passwordBox.Size = new System.Drawing.Size(192, 20)" + Environment.NewLine +
                        "            Me.passwordBox.TabIndex = 3" + Environment.NewLine +
                        "            Me.passwordBox.Text = \"\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' passwordLabel" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.passwordLabel.Location = new System.Drawing.Point(16, 48)" + Environment.NewLine +
                        "            Me.passwordLabel.Name = \"passwordLabel\"" + Environment.NewLine +
                        "            Me.passwordLabel.Size = new System.Drawing.Size(160, 32)" + Environment.NewLine +
                        "            Me.passwordLabel.TabIndex = 2" + Environment.NewLine +
                        "            Me.passwordLabel.Text = \"Enter the password for the remote computer:\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' OKButton" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.OKButton.Location = New System.Drawing.Point(40, 88)" + Environment.NewLine +
                        "            Me.OKButton.Name = \"OKButton\"" + Environment.NewLine +
                        "            Me.OKButton.Size = new System.Drawing.Size(128, 23)" + Environment.NewLine +
                        "            Me.OKButton.TabIndex = 4" + Environment.NewLine +
                        "            Me.OKButton.Text = \"OK\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' closeButton" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel" + Environment.NewLine +
                        "            Me.closeButton.Location = New System.Drawing.Point(200, 88)" + Environment.NewLine +
                        "            Me.closeButton.Name = \"closeButton\"" + Environment.NewLine +
                        "            Me.closeButton.Size = New System.Drawing.Size(128, 23)" + Environment.NewLine +
                        "            Me.closeButton.TabIndex = 5" + Environment.NewLine +
                        "            Me.closeButton.Text = \"Cancel\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' MyQuerySample" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.AcceptButton = Me.OKButton" + Environment.NewLine +
                        "            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)" + Environment.NewLine +
                        "            Me.CancelButton = Me.closeButton" + Environment.NewLine +
                        "            Me.ClientSize = New System.Drawing.Size(368, 130)" + Environment.NewLine +
                        "            Me.ControlBox = false" + Environment.NewLine +
                        "            Me.Controls.Add(Me.closeButton)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.OKButton)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.passwordBox)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.passwordLabel)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.userNameBox)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.userNameLabel)" + Environment.NewLine +
                        "            Me.Name = \"MyQuerySample\"" + Environment.NewLine +
                        "            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen" + Environment.NewLine +
                        "            Me.Text = \"Remote Connection\"" + Environment.NewLine +
                        "            Me.ResumeLayout(false)" + Environment.NewLine +
                        Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                        Environment.NewLine +
                        "            Application.Run(New CallWMIMethod)" + Environment.NewLine +
                        "        End Function" + Environment.NewLine +
                        Environment.NewLine +
                        "        Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click" + Environment.NewLine +
                        "            Try" + Environment.NewLine +
                        "                Dim connection As New ConnectionOptions" + Environment.NewLine +
                        "                connection.Username = userNameBox.Text" + Environment.NewLine +
                        "                connection.Password = passwordBox.Text" + Environment.NewLine +
                        "                connection.Authority = \"ntlmdomain:" + this.TargetWindow.GetRemoteComputerDomain() + "\"" + Environment.NewLine +
                        Environment.NewLine +
                        "                Dim scope As New ManagementScope( _" + Environment.NewLine +
                        "                    \"\\\\" + this.TargetWindow.GetRemoteComputerName() + "\\" + this.NamespaceValue_m.Text + "\", connection)" + Environment.NewLine +
                        "                scope.Connect()" + Environment.NewLine +Environment.NewLine;
					
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code +
                        "Imports System" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        "Imports System.Windows.Forms" + Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class CallWMIMethod" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                        Environment.NewLine +
                        "            Try" + Environment.NewLine +
                        Environment.NewLine +
                        "                Dim arrComputers As String() = {\"";

                    string delimStr = " ,\n";
                    char [] delimiter = delimStr.ToCharArray();
                    string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);

                    foreach (string s in split) 
                    {
                        code = code + s.Trim() + "\",\"";
                    }
                    string trimStr = ",\"";
                    char [] trim = trimStr.ToCharArray();
                    code = code.TrimEnd(trim) + "\"}" +
                        Environment.NewLine +
                        "                For Each strComputer As String In arrComputers" + Environment.NewLine +
                        Environment.NewLine;

                }
                else
                {
                    code = code +
                        "Imports System" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        "Imports System.Windows.Forms" + Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class CallWMIMethod" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                        Environment.NewLine +
                        "            Try" + Environment.NewLine +
                        Environment.NewLine;
                }

                
                if(staticFlag) // The method is static.
                {
                    if(this.GroupRemoteComputerMenu.Checked)
                    {
                        code = code +
                            "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine +
                            "                    Console.WriteLine(\"  Computer: \" & strComputer)" + Environment.NewLine +
                            "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine + Environment.NewLine +
                            "                    Dim classInstance As New ManagementClass( _" + Environment.NewLine +
                            "                        \"\\\\\" & strComputer & \"\\" + this.NamespaceValue_m.Text + "\", _" + Environment.NewLine +
                            "                        \"" + this.ClassList_m.Text + "\", Nothing)" +
                            Environment.NewLine +
                            Environment.NewLine;
                    }
                    else if(this.LocalComputerMenu.Checked)
                    {
                        code = code +
                            "                Dim classInstance As New ManagementClass( _" + Environment.NewLine +
                            "                    \"" + this.NamespaceValue_m.Text + "\", _" + Environment.NewLine +
                            "                    \"" + this.ClassList_m.Text + "\", Nothing)" +
                            Environment.NewLine +
                            Environment.NewLine;
                    }
                    else if(this.RemoteComputerMenu.Checked)
                    {
                        code = code +
                            "                Dim classInstance As New ManagementClass( _" + Environment.NewLine +
                            "                    scope, _" + Environment.NewLine +
                            "                    New ManagementPath(\"" + this.ClassList_m.Text + "\"), Nothing)" +
                            Environment.NewLine +
                            Environment.NewLine;
                    }
                }
                else // The method is not a static method, and must be executed on an instance.
                {
                    if(this.KeyValueBox.SelectedItems.Count.Equals(0))
                    {
                        if(this.KeyValueBox.Items.Count.Equals(0))
                        {
                            if(this.GroupRemoteComputerMenu.Checked)
                            {
                                code = code +
                                    "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine +
                                    "                    Console.WriteLine(\"  Computer: \" & strComputer)" + Environment.NewLine +
                                    "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine + Environment.NewLine +
                                    "                    Dim classInstance As New ManagementClass( _" + Environment.NewLine +
                                    "                        \"\\\\\" & strComputer & \"\\" + this.NamespaceValue_m.Text + "\", _" + Environment.NewLine +
                                    "                        \"" + this.ClassList_m.Text + "\", Nothing)" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                            else if(this.LocalComputerMenu.Checked)
                            {
                                code = code +
                                    "                Dim classInstance As New ManagementObject( _" + Environment.NewLine +
                                    "                    \"" + this.NamespaceValue_m.Text + "\", _" + Environment.NewLine +
                                    "                    \"" + this.ClassList_m.Text + "\", Nothing)" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                            else if(this.RemoteComputerMenu.Checked)
                            {
                                code = code +
                                    "                Dim classInstance As New ManagementObject(scope, _" + Environment.NewLine +
                                    "                    New ManagementPath(\"" + this.ClassList_m.Text + "\"), Nothing)" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                        }
                        else
                        {
                            if(this.GroupRemoteComputerMenu.Checked)
                            {
                                code = code +
                                    "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine +
                                    "                    Console.WriteLine(\"  Computer: \" & strComputer)" + Environment.NewLine +
                                    "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine + Environment.NewLine +
                                    "                    Dim classInstance As New ManagementObject( _" + Environment.NewLine +
                                    "                        \"\\\\\" & strComputer & \"\\" + this.NamespaceValue_m.Text + "\", _" + Environment.NewLine +
                                    "                        \"" + this.ClassList_m.Text + ".ReplaceKeyPropery='ReplaceKeyPropertyValue'\", _" +
                                    Environment.NewLine + 
                                    "                        Nothing)" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
							
                            else if(this.LocalComputerMenu.Checked)
                            {
                                code = code + 
                                    "                Dim classInstance As New ManagementObject( _" + Environment.NewLine +
                                    "                    \"" + this.NamespaceValue_m.Text + "\", _" + Environment.NewLine +
                                    "                    \"" + this.ClassList_m.Text + ".ReplaceKeyPropery='ReplaceKeyPropertyValue'\", _" +
                                    Environment.NewLine + 
                                    "                    Nothing)" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                            else if(this.RemoteComputerMenu.Checked)
                            {
                                code = code +
                                    "                Dim classInstance As New ManagementObject(scope, _" + Environment.NewLine +
                                    "                    New ManagementPath(\"" + this.ClassList_m.Text + ".ReplaceKeyPropery='ReplaceKeyPropertyValue'\"), _" +
                                    Environment.NewLine + 
                                    "                    Nothing)" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                        }
                    }
                    else
                    {
                        if(this.GroupRemoteComputerMenu.Checked)
                        {
                            code = code +
                                "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine +
                                "                    Console.WriteLine(\"  Computer: \" & strComputer)" + Environment.NewLine +
                                "                    Console.WriteLine(\"==========================================\")" + Environment.NewLine + Environment.NewLine + 
                                "                    Dim classInstance As New ManagementObject( _" + Environment.NewLine +
                                "                        \"\\\\\" & strComputer & \"\\" + this.NamespaceValue_m.Text + "\", _" + Environment.NewLine +
                                "                        \"" + this.ClassList_m.Text + "." + this.KeyValueBox.SelectedItem.ToString() + "\", _" +
                                Environment.NewLine + 
                                "                        Nothing)" +
                                Environment.NewLine +
                                Environment.NewLine;
                        }
                        else if(this.LocalComputerMenu.Checked)
                        {
                            code = code + 
                                "                Dim classInstance As New ManagementObject( _" + Environment.NewLine +
                                "                    \"" + this.NamespaceValue_m.Text + "\", _" + Environment.NewLine +
                                "                    \"" + this.ClassList_m.Text + "." + this.KeyValueBox.SelectedItem.ToString() + "\", _" +
                                Environment.NewLine + 
                                "                    Nothing)" +
                                Environment.NewLine +
                                Environment.NewLine;
                        }
                        else if(this.RemoteComputerMenu.Checked)
                        {
                            code = code +
                                "               Dim classInstance As New ManagementObject(scope, _" + Environment.NewLine +
                                "                    New ManagementPath(\"" + this.ClassList_m.Text + "." + this.KeyValueBox.SelectedItem.ToString() + "\"), _" +
                                Environment.NewLine + 
                                "                    Nothing)" +
                                Environment.NewLine +
                                Environment.NewLine;
                        }
                    }
                }

                try
                {
                    ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);

                    foreach (MethodData mData in c.Methods)
                    {
                        if(mData.Name.Equals(this.MethodList.Text))
                        {
                            if(mData.InParameters.Properties.Count.Equals(0))
                            {
                                code = code + buffer + "                ' no method [in] parameters to define" + Environment.NewLine
                                    + Environment.NewLine;	
                            }
                            else
                            {
                                code = code + buffer + "                ' Obtain [in] parameters for the method" +
                                    Environment.NewLine + buffer +
                                    "                Dim inParams As ManagementBaseObject = _" +
                                    Environment.NewLine + buffer +
                                    "                    classInstance.GetMethodParameters(\"" + this.MethodList.Text	+ "\")" +
                                    Environment.NewLine + Environment.NewLine + buffer +
                                    "                ' Add the input parameters." + Environment.NewLine;

                                for(int i = 0; i < InParameterBox.Items.Count; i ++)
                                {
                                    if(InParameterBox.SelectedIndices.Contains(i) && !InParameterArray[i].ReturnParameterValue().Equals(""))
                                    {
										
                                        code = code + buffer +
                                            "                inParams(\"" + InParameterBox.Items[i].ToString().Split(" ".ToCharArray())[0] +
                                            "\") =  " + InParameterArray[i].ReturnParameterValue() + 
                                            Environment.NewLine;
										
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.NullReferenceException nullError2)
                {	
                    code = code + buffer + "                ' no method [in] parameters to define" + Environment.NewLine
                        + Environment.NewLine;
                }

                code = code + Environment.NewLine + buffer +
                    "                ' Execute the method and obtain the return values." +
                    Environment.NewLine;
				
                if(this.InParameterBox.Items.Count.Equals(0))
                {
                    code = code + buffer + "                Dim outParams As ManagementBaseObject = _" +
                        Environment.NewLine + buffer +
                        "                    classInstance.InvokeMethod(\"" + this.MethodList.Text + "\", Nothing, Nothing)" +
                        Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    code = code + buffer + "                Dim outParams As ManagementBaseObject = _" +
                        Environment.NewLine + buffer +
                        "                    classInstance.InvokeMethod(\"" + this.MethodList.Text + "\", inParams, Nothing)" +
                        Environment.NewLine + Environment.NewLine;
                }

                try
                {
                    ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);
                    foreach (MethodData mData in c.Methods)
                    {
                        if(mData.Name.Equals(this.MethodList.Text))
                        {

                            if(mData.OutParameters.Properties.Count.Equals(0))
                            {
                                code = code + Environment.NewLine + buffer + "                ' No outParams" + Environment.NewLine;
                            }
                            else
                            {
								
                                code = code + buffer +
                                    "                ' List outParams" + Environment.NewLine + buffer +
                                    "                Console.WriteLine(\"Out parameters:\")" + Environment.NewLine;
								

                                foreach(PropertyData p in mData.OutParameters.Properties)
                                {
                                    // Check to see if the out-parameter is not a basic type.
                                    if(p.Type.ToString().Equals("Object"))
                                    {
                                        code = code + buffer + "                Console.WriteLine(\"The " + p.Name +
                                            " out-parameter contains an object.\")" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        code = code + buffer + "                Console.WriteLine(\"" + p.Name +
                                            ": {0}\", outParams(\"" +
                                            p.Name + "\"))" + Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.NullReferenceException nullError)
                {
					
                    code = code + Environment.NewLine + buffer + "                ' No outParams" + Environment.NewLine;
					
                }

                if(this.RemoteComputerMenu.Checked)
                {
                    code = code + Environment.NewLine + "                Close()" + Environment.NewLine +
                        Environment.NewLine +
                        "            Catch err As ManagementException" + Environment.NewLine +
                        Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while trying to execute the WMI method: \" & err.Message)" + Environment.NewLine +
                        Environment.NewLine +
                        "            Catch unauthorizedErr As System.UnauthorizedAccessException" + Environment.NewLine +
                        Environment.NewLine +
                        "                MessageBox.Show(\"Connection error (user name or password might be incorrect): \" & unauthorizedErr.Message)" + Environment.NewLine +
                        "            End Try" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        Private Sub closeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles closeButton.Click" + Environment.NewLine +
                        Environment.NewLine +
                        "            Close()" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        "    End Class" + Environment.NewLine +
                        "End Namespace" + Environment.NewLine;
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code + "                Next" +
                        Environment.NewLine + 
                        Environment.NewLine + "            Catch err As ManagementException" + Environment.NewLine +
                        Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while trying to execute the WMI method: \" & err.Message)" + Environment.NewLine +
                        "            End Try" + Environment.NewLine +
                        Environment.NewLine + "        End Function" +
                        Environment.NewLine + "    End Class" +
                        Environment.NewLine + "End Namespace";
                }
                else
                {
                    code = code + 
                        Environment.NewLine + 
                        "            Catch err As ManagementException" + Environment.NewLine +
                        Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while trying to execute the WMI method: \" & err.Message)" + Environment.NewLine +
                        "            End Try" + Environment.NewLine +
                        "        End Function" + Environment.NewLine +
                        "    End Class" + Environment.NewLine +
                        "End Namespace";
                }

                this.CodeText_m.Text = code;
            }
        }

        //-------------------------------------------------------------------------
        // Generates the C# code in the method tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateCSharpMethodCode()
        {
            bool staticFlag = this.IsStaticMethodSelected();
            string buffer = "";
            if(this.GroupRemoteComputerMenu.Checked)
                buffer = "    ";

            if(this.MethodList.Items.Count > 0) 
            {
                string code = "";

                if(this.RemoteComputerMenu.Checked)
                {
                    code = "using System;" + Environment.NewLine +
                        "using System.Drawing;" + Environment.NewLine +
                        "using System.Collections;" + Environment.NewLine +
                        "using System.ComponentModel;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        "using System.Data;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class CallWMIMethod : System.Windows.Forms.Form" + Environment.NewLine +
                        "    {" + Environment.NewLine +
                        "        private System.Windows.Forms.Label userNameLabel;" + Environment.NewLine +
                        "        private System.Windows.Forms.TextBox userNameBox;" + Environment.NewLine +
                        "        private System.Windows.Forms.TextBox passwordBox;" + Environment.NewLine +
                        "        private System.Windows.Forms.Label passwordLabel;" + Environment.NewLine +
                        "        private System.Windows.Forms.Button OKButton;" + Environment.NewLine +
                        "        private System.Windows.Forms.Button cancelButton;" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        private System.ComponentModel.Container components = null;" + Environment.NewLine +
                        Environment.NewLine +
                        "        public CallWMIMethod()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            InitializeComponent();" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        protected override void Dispose( bool disposing )" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            if( disposing )" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                if (components != null)" + Environment.NewLine + 
                        "                {" + Environment.NewLine +
                        "                    components.Dispose();" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            base.Dispose( disposing );" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        private void InitializeComponent()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            this.userNameLabel = new System.Windows.Forms.Label();" + Environment.NewLine +
                        "            this.userNameBox = new System.Windows.Forms.TextBox();" + Environment.NewLine +
                        "            this.passwordBox = new System.Windows.Forms.TextBox();" + Environment.NewLine +
                        "            this.passwordLabel = new System.Windows.Forms.Label();" + Environment.NewLine +
                        "            this.OKButton = new System.Windows.Forms.Button();" + Environment.NewLine +
                        "            this.cancelButton = new System.Windows.Forms.Button();" + Environment.NewLine +
                        "            this.SuspendLayout();" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // userNameLabel" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.userNameLabel.Location = new System.Drawing.Point(16, 8);" + Environment.NewLine +
                        "            this.userNameLabel.Name = \"userNameLabel\";" + Environment.NewLine +
                        "            this.userNameLabel.Size = new System.Drawing.Size(160, 32);" + Environment.NewLine +
                        "            this.userNameLabel.TabIndex = 0;" + Environment.NewLine +
                        "            this.userNameLabel.Text = \"Enter the user name for the remote computer:\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // userNameBox" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.userNameBox.Location = new System.Drawing.Point(160, 16);" + Environment.NewLine +
                        "            this.userNameBox.Name = \"userNameBox\";" + Environment.NewLine +
                        "            this.userNameBox.Size = new System.Drawing.Size(192, 20);" + Environment.NewLine +
                        "            this.userNameBox.TabIndex = 1;" + Environment.NewLine +
                        "            this.userNameBox.Text = \"\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // passwordBox" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.passwordBox.Location = new System.Drawing.Point(160, 48);" + Environment.NewLine +
                        "            this.passwordBox.Name = \"passwordBox\";" + Environment.NewLine +
                        "            this.passwordBox.PasswordChar = '*';" + Environment.NewLine +
                        "            this.passwordBox.Size = new System.Drawing.Size(192, 20);" + Environment.NewLine +
                        "            this.passwordBox.TabIndex = 3;" + Environment.NewLine +
                        "            this.passwordBox.Text = \"\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // passwordLabel" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.passwordLabel.Location = new System.Drawing.Point(16, 48);" + Environment.NewLine +
                        "            this.passwordLabel.Name = \"passwordLabel\";" + Environment.NewLine +
                        "            this.passwordLabel.Size = new System.Drawing.Size(160, 32);" + Environment.NewLine +
                        "            this.passwordLabel.TabIndex = 2;" + Environment.NewLine +
                        "            this.passwordLabel.Text = \"Enter the password for the remote computer:\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // OKButton" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.OKButton.Location = new System.Drawing.Point(40, 88);" + Environment.NewLine +
                        "            this.OKButton.Name = \"OKButton\";" + Environment.NewLine +
                        "            this.OKButton.Size = new System.Drawing.Size(128, 23);" + Environment.NewLine +
                        "            this.OKButton.TabIndex = 4;" + Environment.NewLine +
                        "            this.OKButton.Text = \"OK\";" + Environment.NewLine +
                        "            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // cancelButton" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;" + Environment.NewLine +
                        "            this.cancelButton.Location = new System.Drawing.Point(200, 88);" + Environment.NewLine +
                        "            this.cancelButton.Name = \"cancelButton\";" + Environment.NewLine +
                        "            this.cancelButton.Size = new System.Drawing.Size(128, 23);" + Environment.NewLine +
                        "            this.cancelButton.TabIndex = 5;" + Environment.NewLine +
                        "            this.cancelButton.Text = \"Cancel\";" + Environment.NewLine +
                        "            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // MyQuerySample" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.AcceptButton = this.OKButton;" + Environment.NewLine +
                        "            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);" + Environment.NewLine +
                        "            this.CancelButton = this.cancelButton;" + Environment.NewLine +
                        "            this.ClientSize = new System.Drawing.Size(368, 130);" + Environment.NewLine +
                        "            this.ControlBox = false;" + Environment.NewLine +
                        "            this.Controls.Add(this.cancelButton);" + Environment.NewLine +
                        "            this.Controls.Add(this.OKButton);" + Environment.NewLine +
                        "            this.Controls.Add(this.passwordBox);" + Environment.NewLine +
                        "            this.Controls.Add(this.passwordLabel);" + Environment.NewLine +
                        "            this.Controls.Add(this.userNameBox);" + Environment.NewLine +
                        "            this.Controls.Add(this.userNameLabel);" + Environment.NewLine +
                        "            this.Name = \"MyQuerySample\";" + Environment.NewLine +
                        "            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;" + Environment.NewLine +
                        "            this.Text = \"Remote Connection\";" + Environment.NewLine +
                        "            this.ResumeLayout(false);" + Environment.NewLine +
                        Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        [STAThread]" + Environment.NewLine +
                        "        static void Main() " + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            Application.Run(new CallWMIMethod());" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        private void OKButton_Click(object sender, System.EventArgs e)" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            try" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                ConnectionOptions connection = new ConnectionOptions();" + Environment.NewLine +
                        "                connection.Username = userNameBox.Text;" + Environment.NewLine +
                        "                connection.Password = passwordBox.Text;" + Environment.NewLine +
                        "                connection.Authority = \"ntlmdomain:" + this.TargetWindow.GetRemoteComputerDomain() + "\";" + Environment.NewLine +
                        Environment.NewLine +
                        "                ManagementScope scope = new ManagementScope(" + Environment.NewLine +
                        "                    \"\\\\\\\\" + this.TargetWindow.GetRemoteComputerName() + "\\\\" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", connection);" + Environment.NewLine +
                        "                scope.Connect();" + Environment.NewLine +Environment.NewLine;
					
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code +
                        "using System;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class CallWMIMethod" + Environment.NewLine +
                        "    {" + Environment.NewLine +
                        "        public static void Main()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            try" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                string[] arrComputers = {\"";

                    string delimStr = " ,\n";
                    char [] delimiter = delimStr.ToCharArray();
                    string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);

                    foreach (string s in split) 
                    {
                        code = code + s.Trim() + "\",\"";
                    }
                    string trimStr = ",\"";
                    char [] trim = trimStr.ToCharArray();
                    code = code.TrimEnd(trim) + "\"};" +
                        Environment.NewLine +
                        "                foreach (string strComputer in arrComputers)" + Environment.NewLine +
                        "                {" + Environment.NewLine;

                }
                else
                {
                    code = code +
                        "using System;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class CallWMIMethod" + Environment.NewLine +
                        "    {" + Environment.NewLine +
                        "        public static void Main()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            try" + Environment.NewLine +
                        "            {" + Environment.NewLine;
                }

                
                if(staticFlag)
                {
                    if(this.GroupRemoteComputerMenu.Checked)
                    {
                        code = code +
                            "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine +
                            "                    Console.WriteLine(\"  Computer: \" + strComputer);" + Environment.NewLine +
                            "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine + Environment.NewLine +
                            "                    ManagementClass classInstance = " + Environment.NewLine +
                            "                        new ManagementClass(\"\\\\\\\\\" + strComputer + \"\\\\" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                            "                        \"" + this.ClassList_m.Text + "\", null);" +
                            Environment.NewLine +
                            Environment.NewLine;
                    }
                    else if(this.LocalComputerMenu.Checked)
                    {
                        code = code +
                            "                ManagementClass classInstance = " + Environment.NewLine +
                            "                    new ManagementClass(\"" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                            "                    \"" + this.ClassList_m.Text + "\", null);" +
                            Environment.NewLine +
                            Environment.NewLine;
                    }
                    else if(this.RemoteComputerMenu.Checked)
                    {
                        code = code +
                            "                ManagementClass classInstance = " + Environment.NewLine +
                            "                    new ManagementClass(scope, " + Environment.NewLine +
                            "                    new ManagementPath(\"" + this.ClassList_m.Text + "\"), null);" +
                            Environment.NewLine +
                            Environment.NewLine;
                    }
                }
                else
                {
                    if(this.KeyValueBox.SelectedItems.Count.Equals(0))
                    {
                        if(this.KeyValueBox.Items.Count.Equals(0))
                        {
                            if(this.GroupRemoteComputerMenu.Checked)
                            {
                                code = code +
                                    "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine +
                                    "                    Console.WriteLine(\"  Computer: \" + strComputer);" + Environment.NewLine +
                                    "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine + Environment.NewLine +
                                    "                    ManagementClass classInstance = " + Environment.NewLine +
                                    "                        new ManagementClass(\"\\\\\\\\\" + strComputer + \"\\\\" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                                    "                        \"" + this.ClassList_m.Text + "\", null);" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                            else if(this.LocalComputerMenu.Checked)
                            {
                                code = code +
                                    "                ManagementObject classInstance = " + Environment.NewLine +
                                    "                    new ManagementObject(\"" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                                    "                    \"" + this.ClassList_m.Text + "\", null);" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                            else if(this.RemoteComputerMenu.Checked)
                            {
                                code = code +
                                    "                ManagementObject classInstance = " + Environment.NewLine +
                                    "                    new ManagementObject(scope, " + Environment.NewLine +
                                    "                    new ManagementPath(\"" + this.ClassList_m.Text + "\"), null);" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                        }
                        else
                        {
                            if(this.GroupRemoteComputerMenu.Checked)
                            {
                                code = code +
                                    "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine +
                                    "                    Console.WriteLine(\"  Computer: \" + strComputer);" + Environment.NewLine +
                                    "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine + Environment.NewLine +
                                    "                    ManagementObject classInstance = " + Environment.NewLine +
                                    "                        new ManagementObject(\"\\\\\\\\\" + strComputer + \"\\\\" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                                    "                        \"" + this.ClassList_m.Text + ".ReplaceKeyPropery='ReplaceKeyPropertyValue'\"," +
                                    Environment.NewLine + "                        null);" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
							
                            else if(this.LocalComputerMenu.Checked)
                            {
                                code = code + 
                                    "                ManagementObject classInstance = " + Environment.NewLine +
                                    "                    new ManagementObject(\"" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                                    "                    \"" + this.ClassList_m.Text + ".ReplaceKeyPropery='ReplaceKeyPropertyValue'\"," +
                                    Environment.NewLine + "                    null);" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                            else if(this.RemoteComputerMenu.Checked)
                            {
                                code = code +
                                    "                ManagementObject classInstance = " + Environment.NewLine +
                                    "                    new ManagementObject(scope, " + Environment.NewLine +
                                    "                    new ManagementPath(\"" + this.ClassList_m.Text + ".ReplaceKeyPropery='ReplaceKeyPropertyValue'\")," +
                                    Environment.NewLine + "                    null);" +
                                    Environment.NewLine +
                                    Environment.NewLine;
                            }
                        }
                    }
                    else
                    {
                        if(this.GroupRemoteComputerMenu.Checked)
                        {
                            code = code +
                                "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine +
                                "                    Console.WriteLine(\"  Computer: \" + strComputer);" + Environment.NewLine +
                                "                    Console.WriteLine(\"==========================================\");" + Environment.NewLine + Environment.NewLine + 
                                "                    ManagementObject classInstance = " + Environment.NewLine +
                                "                        new ManagementObject(\"\\\\\\\\\" + strComputer + \"\\\\" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                                "                        \"" + this.ClassList_m.Text + "." + this.KeyValueBox.SelectedItem.ToString() + "\"," +
                                Environment.NewLine + "                        null);" +
                                Environment.NewLine +
                                Environment.NewLine;
                        }
                        else if(this.LocalComputerMenu.Checked)
                        {
                            code = code + 
                                "                ManagementObject classInstance = " + Environment.NewLine +
                                "                    new ManagementObject(\"" + this.NamespaceValue_m.Text.Replace("\\", "\\\\") + "\", " + Environment.NewLine +
                                "                    \"" + this.ClassList_m.Text + "." + this.KeyValueBox.SelectedItem.ToString() + "\"," +
                                Environment.NewLine + "                    null);" +
                                Environment.NewLine +
                                Environment.NewLine;
                        }
                        else if(this.RemoteComputerMenu.Checked)
                        {
                            code = code +
                                "                ManagementObject classInstance = " + Environment.NewLine +
                                "                    new ManagementObject(scope, " + Environment.NewLine +
                                "                    new ManagementPath(\"" + this.ClassList_m.Text + "." + this.KeyValueBox.SelectedItem.ToString() + "\")," +
                                Environment.NewLine + "                    null);" +
                                Environment.NewLine +
                                Environment.NewLine;
                        }
                    }
                }

                try
                {
                    ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);

                    foreach (MethodData mData in c.Methods)
                    {
                        if(mData.Name.Equals(this.MethodList.Text))
                        {

                            if(mData.InParameters.Properties.Count.Equals(0))
                            {
								
                                code = code + buffer + "                // no method in-parameters to define" + Environment.NewLine
                                    + Environment.NewLine;
								
                            }
                            else
                            {
                                code = code + buffer +"                // Obtain in-parameters for the method" +
                                    Environment.NewLine + buffer +
                                    "                ManagementBaseObject inParams = " +
                                    Environment.NewLine + buffer +
                                    "                    classInstance.GetMethodParameters(\"" + this.MethodList.Text	+ "\");" +
                                    Environment.NewLine + Environment.NewLine + buffer +
                                    "                // Add the input parameters." + Environment.NewLine;

                                for(int i = 0; i < InParameterBox.Items.Count; i ++)
                                {
                                    if(InParameterBox.SelectedIndices.Contains(i) && !InParameterArray[i].ReturnParameterValue().Equals(""))
                                    {
                                        code = code + buffer +
                                            "                inParams[\"" + InParameterBox.Items[i].ToString().Split(" ".ToCharArray())[0] +
                                            "\"] =  " + InParameterArray[i].ReturnParameterValue() + ";" +
                                            Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.NullReferenceException nullError2)
                {
                    code = code + buffer+ "                // no method in-parameters to define" + Environment.NewLine
                        + Environment.NewLine;
                }

                code = code + Environment.NewLine + buffer +
                    "                // Execute the method and obtain the return values." +
                    Environment.NewLine;
				

                if(this.InParameterBox.Items.Count.Equals(0))
                {
					
                    code = code + buffer + "                ManagementBaseObject outParams = " +
                        Environment.NewLine + buffer +
                        "                    classInstance.InvokeMethod(\"" + this.MethodList.Text + "\", null, null);" +
                        Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    code = code + buffer + "                ManagementBaseObject outParams = " +
                        Environment.NewLine + buffer +
                        "                    classInstance.InvokeMethod(\"" + this.MethodList.Text + "\", inParams, null);" +
                        Environment.NewLine + Environment.NewLine;
                }

                try
                {
                    ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);
                    foreach (MethodData mData in c.Methods)
                    {
                        if(mData.Name.Equals(this.MethodList.Text))
                        {

                            if(mData.OutParameters.Properties.Count.Equals(0))
                            {
                                code = code + Environment.NewLine + buffer + "                // No outParams" + Environment.NewLine;
                            }
                            else
                            {
                                code = code + buffer + 
                                    "                // List outParams" + Environment.NewLine + buffer +
                                    "                Console.WriteLine(\"Out parameters:\");" + Environment.NewLine;
								
                                foreach(PropertyData p in mData.OutParameters.Properties)
                                {
                                    // Check to see if the out-parameter is not a basic type
                                    if(p.Type.ToString().Equals("Object"))
                                    {
                                        code = code + buffer + "                Console.WriteLine(\"The " + p.Name +
                                            " out-parameter contains an object.\");" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        code = code + buffer + "                Console.WriteLine(\"" + p.Name +
                                            ": \" + outParams[\"" +
                                            p.Name + "\"]);" + Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.NullReferenceException nullError)
                {
                    code = code + Environment.NewLine + buffer + "                // No outParams" + Environment.NewLine;
                }

                if(this.RemoteComputerMenu.Checked)
                {
                    code = code + Environment.NewLine + "                Close();" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            catch(ManagementException err)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while trying to execute the WMI method: \" + err.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            catch(System.UnauthorizedAccessException unauthorizedErr)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"Connection error (user name or password might be incorrect): \" + unauthorizedErr.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        private void cancelButton_Click(object sender, System.EventArgs e)" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            Close();" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}" + Environment.NewLine;
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code + "                }" +
                        Environment.NewLine + "            }" +
                        Environment.NewLine + "            catch(ManagementException err)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while trying to execute the WMI method: \" + err.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        Environment.NewLine + "        }" +
                        Environment.NewLine + "    }" +
                        Environment.NewLine + "}";
                }
                else
                {
                    code = code + 
                        "            }" + Environment.NewLine + 
                        "            catch(ManagementException err)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while trying to execute the WMI method: \" + err.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}";
                }

                this.CodeText_m.Text = code;
            }
        }

        //-------------------------------------------------------------------------
        // Generates the VBScript script in the method tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateVBSMethodCode()
        {

            bool staticFlag = this.IsStaticMethodSelected();

            if(this.MethodList.Items.Count > 0) 
            {
                string code = Environment.NewLine;

                if(this.RemoteComputerMenu.Checked)
                {
                    code = code + "strComputer = \"" + this.TargetWindow.GetRemoteComputerName() + "\" " 
                        + Environment.NewLine +
                        "strDomain = \"" + this.TargetWindow.GetRemoteComputerDomain() + "\" " 
                        + Environment.NewLine +
                        "Wscript.StdOut.Write \"Please enter your user name:\"" +
                        Environment.NewLine +
                        "strUser = Wscript.StdIn.ReadLine "
                        + Environment.NewLine +
                        "Set objPassword = CreateObject(\"ScriptPW.Password\")" 
                        + Environment.NewLine +
                        "Wscript.StdOut.Write \"Please enter your password:\""
                        + Environment.NewLine +
                        "strPassword = objPassword.GetPassword()"
                        + Environment.NewLine +
                        "Wscript.Echo"
                        + Environment.NewLine + Environment.NewLine +
                        "Set objSWbemLocator = CreateObject(\"WbemScripting.SWbemLocator\") " 
                        + Environment.NewLine +
                        "Set objWMIService = objSWbemLocator.ConnectServer(strComputer, _ "
                        + Environment.NewLine +
                        "    \"" + this.NamespaceValue_m.Text +"\", _ " 
                        + Environment.NewLine +
                        "    strUser, _ "
                        + Environment.NewLine +
                        "    strPassword, _ "
                        + Environment.NewLine +
                        "    \"MS_409\", _ "
                        + Environment.NewLine +
                        "    \"ntlmdomain:\" + strDomain) "
                        + Environment.NewLine;
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    string delimStr = " ,\n";
                    char [] delimiter = delimStr.ToCharArray();
                    string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);


                    code = code + "arrComputers = Array(\"";
                    foreach (string s in split) 
                    {
                        code = code + s.Trim() + "\",\"";
                    }
                    string trimStr = ",\"";
                    char [] trim = trimStr.ToCharArray();
                    code = code.TrimEnd(trim) + "\")" +
                        Environment.NewLine +
                        "For Each strComputer In arrComputers" +
                        Environment.NewLine +
                        "   WScript.Echo" +
                        Environment.NewLine +
                        "   WScript.Echo \"==========================================\"" +
                        Environment.NewLine +
                        "   WScript.Echo \"Computer: \" & strComputer" +
                        Environment.NewLine +
                        "   WScript.Echo \"==========================================\"" +
                        Environment.NewLine + 
                        Environment.NewLine +
                       
                        "Set objWMIService = GetObject(\"winmgmts:\\\\\" & strComputer & \"\\" +
                        this.NamespaceValue_m.Text + "\") " 
                        + Environment.NewLine;

                }
                else
                {
                    code = code + "strComputer = \".\" " 
                        + Environment.NewLine +
                        "Set objWMIService = GetObject(\"winmgmts:\\\\\" & strComputer & \"\\" +
                        this.NamespaceValue_m.Text + "\") " 
                        + Environment.NewLine;
                }
                
                if(staticFlag) // If true, the method is static.
                {
                    code = code + "' Obtain the definition of the class." +
                        Environment.NewLine +
                        "Set objShare = objWMIService.Get(\"" + this.ClassList_m.Text + "\")" +
                        Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    // The method is not static and must be executed on an instance of the WMI class.
                    if(this.KeyValueBox.SelectedItems.Count.Equals(0))
                    {
                        if(this.KeyValueBox.Items.Count.Equals(0))
                        {
                            code = code + "' Obtain an instance of the the class " +
                                Environment.NewLine +
                                "' using a key property value." +
                                Environment.NewLine +
                                "Set objShare = objWMIService.Get(\"" + this.ClassList_m.Text + "\")" +
                                Environment.NewLine + Environment.NewLine;
                        }
                        else
                        {
                            code = code + "' Obtain an instance of the the class " +
                                Environment.NewLine +
                                "' using a key property value." +
                                Environment.NewLine +
                                "Set objShare = objWMIService.Get(\"" + this.ClassList_m.Text + ".ReplaceKeyProperty=ReplacePropertyValue\")" +
                                Environment.NewLine + Environment.NewLine;
                        }
                    }
                    else
                    {
                        code = code + "' Obtain an instance of the the class " +
                            Environment.NewLine +
                            "' using a key property value." +
                            Environment.NewLine +
                            "Set objShare = objWMIService.Get(\"" + this.ClassList_m.Text + "." + this.KeyValueBox.SelectedItem.ToString() + "\")" +
                            Environment.NewLine + Environment.NewLine;
                    }
                }


                try
                {
                    ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);

                    foreach (MethodData mData in c.Methods)
                    {
                        if(mData.Name.Equals(this.MethodList.Text))
                        {

                            if(mData.InParameters.Properties.Count.Equals(0))
                            {
                                code = code + "' no InParameters to define" + Environment.NewLine
                                    + Environment.NewLine;
                            }
                            else
                            {
                                code = code + "' Obtain an InParameters object specific" +
                                    Environment.NewLine +
                                    "' to the method." +
                                    Environment.NewLine +
                                    "Set objInParam = objShare.Methods_(\"" + this.MethodList.SelectedItem.ToString() + "\"). _" +
                                    Environment.NewLine +
                                    "    inParameters.SpawnInstance_()" + Environment.NewLine +
                                    Environment.NewLine + Environment.NewLine +
                                    "' Add the input parameters." + Environment.NewLine;


								
                                for(int i = 0; i < InParameterBox.Items.Count; i ++)
                                {
                                    if(InParameterBox.SelectedIndices.Contains(i) && !InParameterArray[i].ReturnParameterValue().Equals(""))
                                    {
                                        code = code +
                                            "objInParam.Properties_.Item(\"" + InParameterBox.Items[i].ToString().Split(" ".ToCharArray())[0] +
                                            "\") =  " + InParameterArray[i].ReturnParameterValue() +
                                            Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.NullReferenceException nullError)
                {
                    code = code + "' no InParameters to define"
                        + Environment.NewLine; 
                }

                code = code + Environment.NewLine +
                    "' Execute the method and obtain the return status." +
                    Environment.NewLine +
                    "' The OutParameters object in objOutParams" + 
                    Environment.NewLine +
                    "' is created by the provider." + 
                    Environment.NewLine;
 
                if(staticFlag)
                {
                    code = code + "Set objOutParams = objWMIService.ExecMethod(\"" + this.ClassList_m.Text + "\", \"";     
                }
                else
                {
                    if(!this.KeyValueBox.SelectedItems.Count.Equals(0))
                    {
                        code = code + "Set objOutParams = objWMIService.ExecMethod(\"" + this.ClassList_m.Text + "." + this.KeyValueBox.SelectedItem.ToString() + "\", \"";         
                    }
                    else
                    {
                        if(this.KeyValueBox.Items.Count.Equals(0))
                        {
                            code = code + "Set objOutParams = objWMIService.ExecMethod(\"" + this.ClassList_m.Text + "\", \"";     
                        }
                        else
                        {
                            code = code + "Set objOutParams = objWMIService.ExecMethod(\"" + this.ClassList_m.Text + ".ReplaceKeyProperty=ReplacePropertyValue\", \"";       
                        }
                    }
                }

                if(this.InParameterBox.Items.Count.Equals(0))
                {
                    code = code + this.MethodList.Text +
                        "\")" +
                        Environment.NewLine + Environment.NewLine;

                }
                else
                {
                    code = code + this.MethodList.Text +
                        "\", objInParam)" +
                        Environment.NewLine + Environment.NewLine;
                }



                try
                {
                    ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);
                    foreach (MethodData mData in c.Methods)
                    {
                        if(mData.Name.Equals(this.MethodList.Text))
                        {

                            if(mData.OutParameters.Properties.Count.Equals(0))
                            {
                                code = code + Environment.NewLine + "' No outParams" + Environment.NewLine;
                            }
                            else
                            {
                                code = code + 
                                    "' List OutParams" + Environment.NewLine +
                                    "Wscript.Echo \"Out Parameters: \"" + Environment.NewLine;

                                foreach(PropertyData p in mData.OutParameters.Properties)
                                {
                                    // Check to see if the out-parameter is not a basic type.
                                    if(p.Type.ToString().Equals("Object"))
                                    {
                                        code = code + "Wscript.echo \"The objOutParams." +
                                            p.Name + " variable contains an object.\"" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        code = code + "Wscript.echo \"" + p.Name +
                                            ": \" & objOutParams." +
                                            p.Name + Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.NullReferenceException nullError2)
                {
                    code = code + Environment.NewLine + "' No outParams" + Environment.NewLine;
                }

                if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code + "Next" + Environment.NewLine;
                }

                this.CodeText_m.Text = code;

            }

        }

        //-------------------------------------------------------------------------
        // Generates the code in the event tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        public void GenerateEventCode()
        {
            try
            {
                if(!this.ClassList_event.Text.Equals("")) 
                {
                    if(this.VbNetMenuItem.Checked)
                    {
                        GenerateVBNetEventCode();
                    }
                    else if(this.CSharpMenuItem.Checked)
                    {
                        GenerateCSharpEventCode();
                    }
                    else if(this.VbsMenuItem.Checked)
                    {
                        GenerateVBSEventCode();
                    }
                }
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("WMI class or method not found.");
                else
                    MessageBox.Show(mErr.Message.ToString());
            }
        }

        //-------------------------------------------------------------------------
        // Generates the VBScript script in the event tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateVBSEventCode()
        {
            if(!this.ClassList_event.Text.Equals("")) 
            {
                string code = "";
                string eventQuery = "";

                if(this.RemoteComputerMenu.Checked)
                {
                    code = code + "strComputer = \"" + this.TargetWindow.GetRemoteComputerName() + "\" " 
                        + Environment.NewLine +
                        "strDomain = \"" + this.TargetWindow.GetRemoteComputerDomain() + "\" " 
                        + Environment.NewLine +
                        "Wscript.StdOut.Write \"Please enter your user name:\"" +
                        Environment.NewLine +
                        "strUser = Wscript.StdIn.ReadLine "
                        + Environment.NewLine +
                        "Set objPassword = CreateObject(\"ScriptPW.Password\")" 
                        + Environment.NewLine +
                        "Wscript.StdOut.Write \"Please enter your password:\""
                        + Environment.NewLine +
                        "strPassword = objPassword.GetPassword()"
                        + Environment.NewLine +
                        "Wscript.Echo"
                        + Environment.NewLine + Environment.NewLine +
                        "Set objSWbemLocator = CreateObject(\"WbemScripting.SWbemLocator\") " 
                        + Environment.NewLine +
                        "objSWbemLocator.Security_.ImpersonationLevel = 3  ' wbemImpersonationLevelImpersonate" + 
                        Environment.NewLine +
                        "objSWbemLocator.Security_.Privileges.AddAsString \"SeSecurityPrivilege\", True" + Environment.NewLine +
                        Environment.NewLine +
                        "Set objWMIService = objSWbemLocator.ConnectServer(strComputer, _ "
                        + Environment.NewLine +
                        "    \"" + this.NamespaceList_event.Text +"\", _ " 
                        + Environment.NewLine +
                        "    strUser, _ "
                        + Environment.NewLine +
                        "    strPassword, _ "
                        + Environment.NewLine +
                        "    \"MS_409\", _ "
                        + Environment.NewLine +
                        "    \"ntlmdomain:\" + strDomain) "
                        + Environment.NewLine;
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    string delimStr = " ,\n";
                    char [] delimiter = delimStr.ToCharArray();
                    string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);


                    code = code + "strComputer = \"";
                    
                    code = code + split[0].Trim() + "\"";
                  
                    code = code +
                        Environment.NewLine +
                        "   WScript.Echo" +
                        Environment.NewLine +
                        "   WScript.Echo \"==========================================\"" +
                        Environment.NewLine +
                        "   WScript.Echo \"Computer: \" & strComputer" +
                        Environment.NewLine +
                        "   WScript.Echo \"==========================================\"" +
                        Environment.NewLine + 
                        Environment.NewLine +
                       
                        "Set objWMIService = GetObject(\"winmgmts:\\\\\" & strComputer & \"\\" +
                        this.NamespaceList_event.Text + "\") " 
                        + Environment.NewLine;

                }
                else // The target computer is the local computer.
                {
                
                    code = code + "strComputer = \".\" " 
                        + Environment.NewLine +
                        "Set objWMIService = GetObject(\"winmgmts:\\\\\" & strComputer & \"\\" +
                        this.NamespaceList_event.Text + "\") " 
                        + Environment.NewLine;
                }

                if(!this.Asynchronous.Checked)  // Semisynchronous or synchrounous event notification.
                {
                    code = code + "Set objEvents = objWMIService.ExecNotificationQuery _" +
                        Environment.NewLine +
                        "(\"SELECT * FROM " + this.ClassList_event.Text ;
                    eventQuery = "select * from " + this.ClassList_event.Text;
											
                    if(this.PropertyList_event.SelectedItems.Count.Equals(1))
                    {
                        code = code + " WHERE " + PropertyList_event.SelectedItem.ToString();
                        eventQuery = eventQuery + " where " + PropertyList_event.SelectedItem.ToString();
                    }
                    else if(this.PropertyList_event.SelectedItems.Count > 0)
                    {		
                        code = code + " WHERE \" & _" + Environment.NewLine + "                    ";
                        eventQuery = eventQuery + " where ";

                        int flag = -1;
                        string instance = "";
                        for(int i=0; i < PropertyList_event.SelectedItems.Count; i++)
                        {
                            // If PropertyList_event contains a selected item that contains ISA.
                            if(PropertyList_event.SelectedItems[i].ToString().IndexOf("ISA", 1, PropertyList_event.SelectedItems[i].ToString().Length -1 ) > 0)
                            {
                                flag = i;
                                instance = PropertyList_event.SelectedItems[i].ToString();
                            }
                        }
                        if(flag > -1)
                        {
                            code = code + "\"" + instance;
                            eventQuery = eventQuery + instance;
                        }
						
                        for(int i=0; i < PropertyList_event.SelectedItems.Count; i++)
                        {
                            if(flag.Equals(-1) && i.Equals(0)) //Do not start off with quotes.
                            {
                                code = code + "\"" + PropertyList_event.SelectedItems[i].ToString();
                                eventQuery = eventQuery + "\"" + PropertyList_event.SelectedItems[i].ToString();
                            }
                            else if(!i.Equals(flag))
                            {
                                code = code + "\" & _" + Environment.NewLine +
                                    "                    \" AND " + PropertyList_event.SelectedItems[i].ToString();
                                eventQuery = eventQuery + " and " + PropertyList_event.SelectedItems[i].ToString();
                            }
                        }
                    }
                    
                    code = code + "\")" + Environment.NewLine;

					// Check to see if the event class is supported by an event provider.
                    if(this.QueryCounter == 0)
                    {
                        EventQuerySupportedByProvider();
                        this.PollLabel.Visible = false;
                        this.SecondsBox.Visible = false;
                        this.PollLabelEnd.Visible = false;
                    }
                    
                    if(this.QueryCounter > 0)
                    {
                        bool addWITHINStatement = true;

                        // If the user selected event query is in the list of event provider supported
                        // event queries, then the WITHIN statement does not need to be used in
                        // the user selected event query.
                        for(int i=0; i < this.QueryCounter; i++)
                        {
                            if(eventQuery.IndexOf(this.SupportedEventQueries[i].
                                Replace("\"", "'").
                                Replace("isa", "ISA")) != -1)
                            {
                                addWITHINStatement = false; // Do not add the WITHIN statement.
                                break; // Get out of the for loop.
                            }
                        }

                        if(addWITHINStatement && !this.ExtrinsicEvent(this.ClassList_event.Text))
                        {
                            code = code.Replace(("SELECT * FROM " + this.ClassList_event.Text), 
                                ("SELECT * FROM " + this.ClassList_event.Text + " WITHIN " + this.SecondsBox.Text));
                            this.PollLabel.Visible = true;
                            this.SecondsBox.Visible = true;
                            this.PollLabelEnd.Visible = true;
                        }
                        else
                        {
                            this.PollLabel.Visible = false;
                            this.SecondsBox.Visible = false;
                            this.PollLabelEnd.Visible = false;
                        }
                    }
                    
                    code = code + 
                        Environment.NewLine +
                        "Wscript.Echo \"Waiting for events ...\"" + Environment.NewLine +
                        "Do While(True)" +
                        Environment.NewLine +
                        "    Set objReceivedEvent = objEvents.NextEvent" +
                        Environment.NewLine + Environment.NewLine +
                        "    'report an event" +
                        Environment.NewLine +
                        "    Wscript.Echo \"" + this.ClassList_event.Text + " event has occurred.\"" +
                        Environment.NewLine + Environment.NewLine +
                        "Loop" + Environment.NewLine;
                }
                else if(this.Asynchronous.Checked) // Asynchronous event notification.
                {
                    code = code + "Set MySink = WScript.CreateObject( _" +
                        Environment.NewLine +
                        "    \"WbemScripting.SWbemSink\",\"SINK_\")" +
                        Environment.NewLine + Environment.NewLine +
                        "objWMIservice.ExecNotificationQueryAsync MySink, _" +
                        Environment.NewLine +
                        "    \"SELECT * FROM " + this.ClassList_event.Text;
                    eventQuery = "select * from " + this.ClassList_event.Text;
						
                    if(this.PropertyList_event.SelectedItems.Count.Equals(1))
                    {
                        code = code + " WHERE " + PropertyList_event.SelectedItem.ToString();
                        eventQuery = eventQuery + " where " + PropertyList_event.SelectedItem.ToString();
                    }
                    else if(this.PropertyList_event.SelectedItems.Count > 0)
                    {		
                        code = code + " WHERE \" & _" + Environment.NewLine + "                    ";
                        eventQuery = eventQuery + " where ";

                        int flag = -1;
                        string instance = "";
                        for(int i=0; i < PropertyList_event.SelectedItems.Count; i++)
                        {
                            // If PropertyList_event contains a selected item that contains ISA.
                            if(PropertyList_event.SelectedItems[i].ToString().IndexOf("ISA", 1, PropertyList_event.SelectedItems[i].ToString().Length -1 ) > 0)
                            {
                                flag = i;
                                instance = PropertyList_event.SelectedItems[i].ToString();
                            }
                        }
                        if(flag > -1)
                        {
                            code = code + "\"" + instance;
                            eventQuery = eventQuery + instance;
                        }
						
                        for(int i=0; i < PropertyList_event.SelectedItems.Count; i++)
                        {
                            if(flag.Equals(-1) && i.Equals(0)) //Do not start off with quotes.
                            {
                                code = code + "\"" + PropertyList_event.SelectedItems[i].ToString();
                                eventQuery = eventQuery + "\"" + PropertyList_event.SelectedItems[i].ToString();
                            }
                            else if(!i.Equals(flag))
                            {
                                code = code + "\" & _" + Environment.NewLine +
                                    "                    \" AND " + PropertyList_event.SelectedItems[i].ToString();
                                eventQuery = eventQuery + " and " + PropertyList_event.SelectedItems[i].ToString();
                            }
                        }
                    }
                    code = code + "\"" + Environment.NewLine;
                    
                    // Check to see if the event class is supported by an event provider.
                    if(this.QueryCounter == 0)
                    {
                        EventQuerySupportedByProvider();
                        this.PollLabel.Visible = false;
                        this.SecondsBox.Visible = false;
                        this.PollLabelEnd.Visible = false;
                    }
                    
                    if(this.QueryCounter > 0)
                    {
                        bool addWITHINStatement = true;

                        // If the user selected event query is in the list of event provider supported
                        // event queries, then the WITHIN statement does not need to be used in
                        // the user selected event query.
                        for(int i=0; i < this.QueryCounter; i++)
                        {
                            if(eventQuery.IndexOf(this.SupportedEventQueries[i].
                                Replace("\"", "'").
                                Replace("isa", "ISA")) != -1)
                            {
                                addWITHINStatement = false; // Do not add the WITHIN statement.
                                break; // Get out of the for loop.
                            }
                        }

                        if(addWITHINStatement && !this.ExtrinsicEvent(this.ClassList_event.Text))
                        {
                            code = code.Replace(("SELECT * FROM " + this.ClassList_event.Text), 
                                ("SELECT * FROM " + this.ClassList_event.Text + " WITHIN " + this.SecondsBox.Text));
                            this.PollLabel.Visible = true;
                            this.SecondsBox.Visible = true;
                            this.PollLabelEnd.Visible = true;
                        }
                        else
                        {
                            this.PollLabel.Visible = false;
                            this.SecondsBox.Visible = false;
                            this.PollLabelEnd.Visible = false;
                        }
                    }
					
                    code = code + Environment.NewLine + Environment.NewLine +
                        "WScript.Echo \"Waiting for events...\"" +
                        Environment.NewLine + Environment.NewLine +
                        "While (True)" + System.Environment.NewLine + "    Wscript.Sleep(1000)" + System.Environment.NewLine + "Wend" + System.Environment.NewLine + System.Environment.NewLine +
                        "Sub SINK_OnObjectReady(objObject, objAsyncContext)" +
                        Environment.NewLine +
                        "    Wscript.Echo \"" + this.ClassList_event.Text + " event has occurred.\"" +
                        Environment.NewLine +
                        "End Sub" +
                        Environment.NewLine + Environment.NewLine +
                        "Sub SINK_OnCompleted(objObject, objAsyncContext)" +
                        Environment.NewLine +
                        "    WScript.Echo \"Event call complete.\"" +
                        Environment.NewLine +
                        "End Sub" +
                        Environment.NewLine;
                }

                this.CodeText_event.Text = code;

            }
        }

		//-------------------------------------------------------------------------
		// If an event query is supported by an event provider for a given namespace,
		// then the event query is stored in the SupportedEventQueries array.
		//-------------------------------------------------------------------------
        public void EventQuerySupportedByProvider()
        {
            try
            {
				ManagementObjectSearcher searcher = 
					new ManagementObjectSearcher(this.NamespaceList_event.Text, 
					"SELECT * FROM __EventProviderRegistration"); 

                foreach (ManagementObject objItem in searcher.Get())
                {
                    string[] queryList = (string[])objItem.Properties["EventQueryList"].Value;

                    foreach (string query in queryList)
                    {
                        // Store the query that is supported by an event provider
                        // in the SupportedEventQueries array.
                        this.SupportedEventQueries[QueryCounter] = query;
                        this.QueryCounter++;
                    }
                }
            }
            catch (ManagementException e)
            {
                 MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
            } 
        }

        //-------------------------------------------------------------------------
        // Returns true if the eventClass is derived from __ExtrinsicEvent, and 
        // returns false otherwise.
        //-------------------------------------------------------------------------
        public bool ExtrinsicEvent(string eventClass)
        {
            ObjectGetOptions options = new ObjectGetOptions();
            options.UseAmendedQualifiers = true;
            ManagementClass testClass = 
                new ManagementClass(this.NamespaceList_event.Text, 
                eventClass, options);
            
            if(testClass.SystemProperties["__DERIVATION"].Value != null &&
                testClass.SystemProperties["__DERIVATION"].IsArray)
            {
                
                string[] derivationList = (string[])testClass.SystemProperties["__DERIVATION"].Value;

                foreach (string derivationClass in derivationList)
                {
                    // If the event class is derived from __ExtrinsicEvent, then
                    // return true.
                    if(derivationClass.Equals("__ExtrinsicEvent"))
                    {
                        
                        return true;
                    }
                }
            }
            return false;
        }

        //-------------------------------------------------------------------------
        // Generates the VB code in the event tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateVBNetEventCode()
        {

            if(!this.ClassList_event.Text.Equals("")) 
            {
                string code = "";

                if(this.RemoteComputerMenu.Checked)
                {
                    code = "Imports System" + Environment.NewLine +
                        "Imports System.Drawing" + Environment.NewLine +
                        "Imports System.Collections" + Environment.NewLine +
                        "Imports System.ComponentModel" + Environment.NewLine +
                        "Imports System.Windows.Forms" + Environment.NewLine +
                        "Imports System.Data" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class WMIReceiveEvent " + Environment.NewLine + "        Inherits System.Windows.Forms.Form" + Environment.NewLine +
                        Environment.NewLine +
                        "        Friend WithEvents userNameLabel As System.Windows.Forms.Label" + Environment.NewLine +
                        "        Friend WithEvents userNameBox As System.Windows.Forms.TextBox" + Environment.NewLine +
                        "        Friend WithEvents passwordBox As System.Windows.Forms.TextBox" + Environment.NewLine +
                        "        Friend WithEvents passwordLabel As System.Windows.Forms.Label" + Environment.NewLine +
                        "        Friend WithEvents OKButton As System.Windows.Forms.Button" + Environment.NewLine +
                        "        Friend WithEvents closeButton As System.Windows.Forms.Button" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        Private components As System.ComponentModel.IContainer" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Sub New()" + Environment.NewLine + 
                        "            MyBase.New()" + Environment.NewLine +
                        Environment.NewLine +
                        "            InitializeComponent()" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "            If disposing Then" + Environment.NewLine +
						
                        "                If Not (components Is Nothing) Then" + Environment.NewLine + 
						
                        "                    components.Dispose()" + Environment.NewLine +
                        "                End If" + Environment.NewLine +
                        "            End If" + Environment.NewLine +
                        "            MyBase.Dispose(disposing)" + Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()" + Environment.NewLine +
                        Environment.NewLine +
                        "            Me.userNameLabel = new System.Windows.Forms.Label" + Environment.NewLine +
                        "            Me.userNameBox = new System.Windows.Forms.TextBox" + Environment.NewLine +
                        "            Me.passwordBox = new System.Windows.Forms.TextBox" + Environment.NewLine +
                        "            Me.passwordLabel = new System.Windows.Forms.Label" + Environment.NewLine +
                        "            Me.OKButton = new System.Windows.Forms.Button" + Environment.NewLine +
                        "            Me.closeButton = new System.Windows.Forms.Button" + Environment.NewLine +
                        "            Me.SuspendLayout()" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' userNameLabel" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.userNameLabel.Location = New System.Drawing.Point(16, 8)" + Environment.NewLine +
                        "            Me.userNameLabel.Name = \"userNameLabel\"" + Environment.NewLine +
                        "            Me.userNameLabel.Size = New System.Drawing.Size(160, 32)" + Environment.NewLine +
                        "            Me.userNameLabel.TabIndex = 0" + Environment.NewLine +
                        "            Me.userNameLabel.Text = \"Enter the user name for the remote computer:\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' userNameBox" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.userNameBox.Location = New System.Drawing.Point(160, 16)" + Environment.NewLine +
                        "            Me.userNameBox.Name = \"userNameBox\"" + Environment.NewLine +
                        "            Me.userNameBox.Size = New System.Drawing.Size(192, 20)" + Environment.NewLine +
                        "            Me.userNameBox.TabIndex = 1" + Environment.NewLine +
                        "            Me.userNameBox.Text = \"\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' passwordBox" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.passwordBox.Location = New System.Drawing.Point(160, 48)" + Environment.NewLine +
                        "            Me.passwordBox.Name = \"passwordBox\"" + Environment.NewLine +
                        "            Me.passwordBox.PasswordChar = \"*\"" + Environment.NewLine +
                        "            Me.passwordBox.Size = new System.Drawing.Size(192, 20)" + Environment.NewLine +
                        "            Me.passwordBox.TabIndex = 3" + Environment.NewLine +
                        "            Me.passwordBox.Text = \"\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' passwordLabel" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.passwordLabel.Location = new System.Drawing.Point(16, 48)" + Environment.NewLine +
                        "            Me.passwordLabel.Name = \"passwordLabel\"" + Environment.NewLine +
                        "            Me.passwordLabel.Size = new System.Drawing.Size(160, 32)" + Environment.NewLine +
                        "            Me.passwordLabel.TabIndex = 2" + Environment.NewLine +
                        "            Me.passwordLabel.Text = \"Enter the password for the remote computer:\"" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            ' OKButton" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.OKButton.Location = New System.Drawing.Point(40, 88)" + Environment.NewLine +
                        "            Me.OKButton.Name = \"OKButton\"" + Environment.NewLine +
                        "            Me.OKButton.Size = new System.Drawing.Size(128, 23)" + Environment.NewLine +
                        "            Me.OKButton.TabIndex = 4" + Environment.NewLine +
                        "            Me.OKButton.Text = \"OK\"" + Environment.NewLine +
						
                        "            ' " + Environment.NewLine +
                        "            ' closeButton" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel" + Environment.NewLine +
                        "            Me.closeButton.Location = New System.Drawing.Point(200, 88)" + Environment.NewLine +
                        "            Me.closeButton.Name = \"closeButton\"" + Environment.NewLine +
                        "            Me.closeButton.Size = New System.Drawing.Size(128, 23)" + Environment.NewLine +
                        "            Me.closeButton.TabIndex = 5" + Environment.NewLine +
                        "            Me.closeButton.Text = \"Cancel\"" + Environment.NewLine +
						
                        "            ' " + Environment.NewLine +
                        "            ' MyQuerySample" + Environment.NewLine +
                        "            ' " + Environment.NewLine +
                        "            Me.AcceptButton = Me.OKButton" + Environment.NewLine +
                        "            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)" + Environment.NewLine +
                        "            Me.cancelButton = Me.closeButton" + Environment.NewLine +
                        "            Me.ClientSize = New System.Drawing.Size(368, 130)" + Environment.NewLine +
                        "            Me.ControlBox = false" + Environment.NewLine +
                        "            Me.Controls.Add(Me.closeButton)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.OKButton)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.passwordBox)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.passwordLabel)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.userNameBox)" + Environment.NewLine +
                        "            Me.Controls.Add(Me.userNameLabel)" + Environment.NewLine +
                        "            Me.Name = \"MyQuerySample\"" + Environment.NewLine +
                        "            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen" + Environment.NewLine +
                        "            Me.Text = \"Remote Connection\"" + Environment.NewLine +
                        "            Me.ResumeLayout(false)" + Environment.NewLine +
                        Environment.NewLine +
                        "        End Sub" + Environment.NewLine +
                        Environment.NewLine +
                        "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                        Environment.NewLine +
                        "            Application.Run(New WMIReceiveEvent)" + Environment.NewLine +
                        "        End Function" + Environment.NewLine +
                        Environment.NewLine +
                        "        Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "            Try" + Environment.NewLine +
                        Environment.NewLine +
                        "                Dim connection As New ConnectionOptions()" + Environment.NewLine +
                        "                connection.Username = userNameBox.Text" + Environment.NewLine +
                        "                connection.Password = passwordBox.Text" + Environment.NewLine +
                        "                connection.Authority = \"ntlmdomain:" + this.TargetWindow.GetRemoteComputerDomain() + "\"" + Environment.NewLine +
                        Environment.NewLine +
                        "                Dim scope As New ManagementScope( _" + Environment.NewLine +
                        "                    \"\\\\" + this.TargetWindow.GetRemoteComputerName() + "\\" + this.NamespaceList_event.Text + "\", connection)" + Environment.NewLine +
                        "                scope.Connect()" + Environment.NewLine +Environment.NewLine;
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code +
                        "Imports System" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        "Imports System.Windows.Forms" + Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class WMIReceiveEvent" + Environment.NewLine +
                        Environment.NewLine;
                    if(this.Asynchronous.Checked)
                    {
                        code = code +
                            "        Public Sub New()" + Environment.NewLine +
                            Environment.NewLine +
                            "            Try" + Environment.NewLine +
                            Environment.NewLine +
                            "                Dim strComputer As String" + Environment.NewLine;

                        string delimStr = " ,\n";
                        char [] delimiter = delimStr.ToCharArray();
                        string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);


                        code = code + "                strComputer = \"";
                    
                        code = code + split[0].Trim() + "\"" + Environment.NewLine + Environment.NewLine;
                    }
                    else
                    {
                        code = code +
                            "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                            Environment.NewLine +
                            "            Try" + Environment.NewLine +
                            Environment.NewLine +
                            "                Dim strComputer As String" + Environment.NewLine;

                        string delimStr = " ,\n";
                        char [] delimiter = delimStr.ToCharArray();
                        string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);


                        code = code + "                strComputer = \"";
                    
                        code = code + split[0].Trim() + "\"" + Environment.NewLine + Environment.NewLine;
                    }

                }
                else
                {
                    // Target computer is the local computer. 
                    code = code +
                        "Imports System" + Environment.NewLine +
                        "Imports System.Management" + Environment.NewLine +
                        "Imports System.Windows.Forms" + Environment.NewLine +
                        Environment.NewLine +
                        "Namespace WMISample" + Environment.NewLine +
                        Environment.NewLine +
                        "    Public Class WMIReceiveEvent" + Environment.NewLine +
                        Environment.NewLine;
                    if(this.Asynchronous.Checked) // Asynchronous event notification.
                    {
                        code = code + 
                            "        Public Sub New()" + Environment.NewLine +
                            Environment.NewLine +
                            "            Try" + Environment.NewLine +
                            Environment.NewLine ;
                    }
                    else
                    {
                        code = code + 
                            "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                            Environment.NewLine +
                            "            Try" + Environment.NewLine +
                            Environment.NewLine;
                    }
                }

                string eventQuery = "";

                if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code + 
                        "                Dim scope As String = \"\\\\\" & strComputer & \"\\" + this.NamespaceList_event.Text + "\"" + Environment.NewLine + Environment.NewLine +
                        "                Dim query As String = _" + Environment.NewLine +
                        "                    \"SELECT * FROM " + this.ClassList_event.Text ;
                }
                else
                {
                    code = code + 
                        "                Dim query As New WqlEventQuery( _" + Environment.NewLine +
                        "                    \"SELECT * FROM " + this.ClassList_event.Text ;
                }
                eventQuery = "select * from " + this.ClassList_event.Text;
				
                if(this.PropertyList_event.SelectedItems.Count.Equals(1))
                {
                    code = code + " WHERE " + PropertyList_event.SelectedItem.ToString();
                    eventQuery = eventQuery + " where " + PropertyList_event.SelectedItem.ToString();
                }
                else if(this.PropertyList_event.SelectedItems.Count > 0)
                {		
                    code = code + " WHERE \" & _" + Environment.NewLine + "                    ";
                    eventQuery = eventQuery + " where ";

                    int flag = -1;
                    string instance = "";
                    for(int i=0; i < PropertyList_event.SelectedItems.Count; i++)
                    {
                        // If PropertyList_event contains a selected item that contains ISA.
                        if(PropertyList_event.SelectedItems[i].ToString().IndexOf("ISA", 1, PropertyList_event.SelectedItems[i].ToString().Length -1 ) > 0)
                        {
                            flag = i;
                            instance = PropertyList_event.SelectedItems[i].ToString();
                        }
                    }
                    if(flag > -1)
                    {
                        code = code + "\"" + instance;
                        eventQuery = eventQuery + instance;
                    }
						
                    for(int i=0; i < PropertyList_event.SelectedItems.Count; i++)
                    {
                        if(flag.Equals(-1) && i.Equals(0)) //Do not start off with quotes.
                        {
                            code = code + "\"" + PropertyList_event.SelectedItems[i].ToString();
                            eventQuery = eventQuery + "\"" + PropertyList_event.SelectedItems[i].ToString();
                        }
                        else if(!i.Equals(flag))
                        {
                            code = code + "\" & _" + Environment.NewLine +
                                "                    \" AND " + PropertyList_event.SelectedItems[i].ToString();
                            eventQuery = eventQuery + " and " + PropertyList_event.SelectedItems[i].ToString();
                        }
                    }
                }

                if(this.GroupRemoteComputerMenu.Checked)
                    code = code + "\"";
                else
                    code = code + "\")";

                // Check to see if the event class is supported by an event provider.
                if(this.QueryCounter == 0)
                {
                    EventQuerySupportedByProvider();
                    this.PollLabel.Visible = false;
                    this.SecondsBox.Visible = false;
                    this.PollLabelEnd.Visible = false;
                }
                    
                if(this.QueryCounter > 0)
                {
                    bool addWITHINStatement = true;

                    // If the user selected event query is in the list of event provider supported
                    // event queries, then the WITHIN statement does not need to be used in
                    // the user selected event query.
                    for(int i=0; i < this.QueryCounter; i++)
                    {
                        if(eventQuery.IndexOf(this.SupportedEventQueries[i].
                            Replace("\"", "'").
                            Replace("isa", "ISA")) != -1)
                        {
                            addWITHINStatement = false; // Do not add the WITHIN statement.
                            break; // Get out of the for loop.
                        }
                    }

                    if(addWITHINStatement && !this.ExtrinsicEvent(this.ClassList_event.Text))
                    {
                        code = code.Replace(("SELECT * FROM " + this.ClassList_event.Text), 
                            ("SELECT * FROM " + this.ClassList_event.Text + " WITHIN " + this.SecondsBox.Text));
                        this.PollLabel.Visible = true;
                        this.SecondsBox.Visible = true;
                        this.PollLabelEnd.Visible = true;
                    }
                    else
                    {
                        this.PollLabel.Visible = false;
                        this.SecondsBox.Visible = false;
                        this.PollLabelEnd.Visible = false;
                    }
                }

                if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code + Environment.NewLine + Environment.NewLine +
                        "                Dim watcher As New ManagementEventWatcher(scope, query)" + Environment.NewLine +
                        "                Console.WriteLine(\"Waiting for an event on \" & strComputer & \" ...\")" + Environment.NewLine + Environment.NewLine;
                }
                else if(this.RemoteComputerMenu.Checked)
                {
                    code = code + Environment.NewLine + Environment.NewLine +
                        "                Dim watcher As New ManagementEventWatcher(scope, query)" + Environment.NewLine +
                        "                Console.WriteLine(\"Waiting for an event on " + this.TargetWindow.GetRemoteComputerName() + " ...\")" + Environment.NewLine + Environment.NewLine;
                }
                else // Target computer is the local computer.
                {
                    code = code + Environment.NewLine + Environment.NewLine +
                        "                Dim watcher As New ManagementEventWatcher(query)" + Environment.NewLine +
                        "                Console.WriteLine(\"Waiting for an event...\")" + Environment.NewLine + Environment.NewLine;
                }

                // Semisynchronous or synchronous event.
                if(!this.Asynchronous.Checked)
                {
                    
                    code = code +
                        "                Dim eventObj As ManagementBaseObject = watcher.WaitForNextEvent()" + Environment.NewLine + Environment.NewLine +
                        "                Console.WriteLine(\"{0} event occurred.\", eventObj(\"__CLASS\"))" + Environment.NewLine + Environment.NewLine +
                        "                ' Cancel the event subscription" + Environment.NewLine +
                        "                watcher.Stop()" + Environment.NewLine;
                        
					
                    if(this.RemoteComputerMenu.Checked)
                    {
                        code = code + Environment.NewLine +
                            "                Close()" + Environment.NewLine +
                            "                Return " + Environment.NewLine +
                            Environment.NewLine +
                            "            Catch err As ManagementException" + Environment.NewLine +
                            Environment.NewLine +
                            "                MessageBox.Show(\"An error occurred while trying to receive an event: \" & err.Message)" + Environment.NewLine +
                            Environment.NewLine +
                            "            Catch unauthorizedErr As System.UnauthorizedAccessException" + Environment.NewLine +
                            Environment.NewLine +
                            "                MessageBox.Show(\"Connection error (user name or password might be incorrect): \" & unauthorizedErr.Message)" + Environment.NewLine +
                            Environment.NewLine +
                            "            End Try" + Environment.NewLine +
                            "        End Sub" + Environment.NewLine +
                            Environment.NewLine +
                            "        Private Sub closeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles closeButton.Click" + Environment.NewLine +
                            Environment.NewLine +
                            "            Close()" + Environment.NewLine +
                            "        End Sub" + Environment.NewLine +
                            "    End Class" + Environment.NewLine +
                            "End Namespace" + Environment.NewLine;                 
                    }
                    else 
                    {
                        code = code +
                            "                Return 0" + Environment.NewLine +
                            Environment.NewLine +
                            "            Catch err As ManagementException" + Environment.NewLine +
                            Environment.NewLine +
                            "                MessageBox.Show(\"An error occurred while trying to receive an event: \" & err.Message)" + Environment.NewLine +
                            "            End Try" + Environment.NewLine +
                            "        End Function" + Environment.NewLine +
                            Environment.NewLine + "    End Class" +
                            Environment.NewLine + "End Namespace";
                    }
                }
                else   // Asyncronous event.
                {

                    code = code +
                        "                AddHandler watcher.EventArrived, _" + Environment.NewLine +
                        "                    AddressOf HandleEvent" + Environment.NewLine + Environment.NewLine +
                        "                ' Start listening for events" + Environment.NewLine +
                        "                watcher.Start()"  + Environment.NewLine + Environment.NewLine +
                        "                ' Do something while waiting for events" + Environment.NewLine +
                        "                System.Threading.Thread.Sleep(10000)" + Environment.NewLine + Environment.NewLine +
                        "                ' Stop listening for events" + Environment.NewLine +
                        "                watcher.Stop()" + Environment.NewLine +
                        "                Return" + Environment.NewLine +
                        Environment.NewLine +
                        "            Catch err As ManagementException" + Environment.NewLine +
                        Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while trying to receive an event: \" & err.Message)" + Environment.NewLine +
                        Environment.NewLine;
                        
					
                    if(this.RemoteComputerMenu.Checked)
                    {
                        code = code +
                            "            Catch unauthorizedErr As System.UnauthorizedAccessException" + Environment.NewLine +
                            Environment.NewLine +
                            "                MessageBox.Show(\"Connection error (user name or password might be incorrect): \" & unauthorizedErr.Message)" + Environment.NewLine +
                            Environment.NewLine +
                            "            End Try" + Environment.NewLine +
                            "        End Sub" + Environment.NewLine +
                            Environment.NewLine +
                            Environment.NewLine +
                            "        Private Sub closeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles closeButton.Click" + Environment.NewLine +
                            Environment.NewLine +
                            "            Close()" + Environment.NewLine +
                            "        End Sub" + Environment.NewLine + Environment.NewLine +
                            "        Private Sub HandleEvent(sender As Object, e As EventArrivedEventArgs)" + Environment.NewLine +
                            Environment.NewLine +
                            "            Console.WriteLine(\"" + this.ClassList_event.Text + " event occurred.\")" + Environment.NewLine +
                            "        End Sub" + Environment.NewLine +
                            "    End Class" + Environment.NewLine +
                            "End Namespace" + Environment.NewLine;
                    }
                    else
                    {
                        code = code +
                            "            End Try" + Environment.NewLine +
                            "        End Sub" + Environment.NewLine +
                            Environment.NewLine +
                            "        Private Sub HandleEvent(sender As Object, e As EventArrivedEventArgs)" + Environment.NewLine +
                            Environment.NewLine +
                            "            Console.WriteLine(\"" + this.ClassList_event.Text + " event occurred.\")" + Environment.NewLine +
                            "        End Sub" + Environment.NewLine + Environment.NewLine +
                            "        Public Overloads Shared Function Main() As Integer" + Environment.NewLine +
                            Environment.NewLine +
                            "            Dim receiveEvent As New WMIReceiveEvent" + Environment.NewLine +
                            "            Return 0" + Environment.NewLine +
                            "        End Function" + Environment.NewLine +
                            Environment.NewLine + "    End Class" +
                            Environment.NewLine + "End Namespace";
                    }

                }
                this.CodeText_event.Text = code;

            }
        }

        //-------------------------------------------------------------------------
        // Generates the C# code in the event tab's generated code area.
        // 
        //-------------------------------------------------------------------------
        private void GenerateCSharpEventCode()
        {
            if(!this.ClassList_event.Text.Equals("")) 
            {
                string code = "";

                if(this.RemoteComputerMenu.Checked)
                {
                    code = "using System;" + Environment.NewLine +
                        "using System.Drawing;" + Environment.NewLine +
                        "using System.Collections;" + Environment.NewLine +
                        "using System.ComponentModel;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        "using System.Data;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class WMIReceiveEvent : System.Windows.Forms.Form" + Environment.NewLine +
                        "    {" + Environment.NewLine +
                        "        private System.Windows.Forms.Label userNameLabel;" + Environment.NewLine +
                        "        private System.Windows.Forms.TextBox userNameBox;" + Environment.NewLine +
                        "        private System.Windows.Forms.TextBox passwordBox;" + Environment.NewLine +
                        "        private System.Windows.Forms.Label passwordLabel;" + Environment.NewLine +
                        "        private System.Windows.Forms.Button OKButton;" + Environment.NewLine +
                        "        private System.Windows.Forms.Button cancelButton;" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        private System.ComponentModel.Container components = null;" + Environment.NewLine +
                        Environment.NewLine +
                        "        public WMIReceiveEvent()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            InitializeComponent();" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        protected override void Dispose( bool disposing )" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            if( disposing )" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                if (components != null)" + Environment.NewLine + 
                        "                {" + Environment.NewLine +
                        "                    components.Dispose();" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            base.Dispose( disposing );" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        private void InitializeComponent()" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            this.userNameLabel = new System.Windows.Forms.Label();" + Environment.NewLine +
                        "            this.userNameBox = new System.Windows.Forms.TextBox();" + Environment.NewLine +
                        "            this.passwordBox = new System.Windows.Forms.TextBox();" + Environment.NewLine +
                        "            this.passwordLabel = new System.Windows.Forms.Label();" + Environment.NewLine +
                        "            this.OKButton = new System.Windows.Forms.Button();" + Environment.NewLine +
                        "            this.cancelButton = new System.Windows.Forms.Button();" + Environment.NewLine +
                        "            this.SuspendLayout();" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // userNameLabel" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.userNameLabel.Location = new System.Drawing.Point(16, 8);" + Environment.NewLine +
                        "            this.userNameLabel.Name = \"userNameLabel\";" + Environment.NewLine +
                        "            this.userNameLabel.Size = new System.Drawing.Size(160, 32);" + Environment.NewLine +
                        "            this.userNameLabel.TabIndex = 0;" + Environment.NewLine +
                        "            this.userNameLabel.Text = \"Enter the user name for the remote computer:\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // userNameBox" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.userNameBox.Location = new System.Drawing.Point(160, 16);" + Environment.NewLine +
                        "            this.userNameBox.Name = \"userNameBox\";" + Environment.NewLine +
                        "            this.userNameBox.Size = new System.Drawing.Size(192, 20);" + Environment.NewLine +
                        "            this.userNameBox.TabIndex = 1;" + Environment.NewLine +
                        "            this.userNameBox.Text = \"\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // passwordBox" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.passwordBox.Location = new System.Drawing.Point(160, 48);" + Environment.NewLine +
                        "            this.passwordBox.Name = \"passwordBox\";" + Environment.NewLine +
                        "            this.passwordBox.PasswordChar = '*';" + Environment.NewLine +
                        "            this.passwordBox.Size = new System.Drawing.Size(192, 20);" + Environment.NewLine +
                        "            this.passwordBox.TabIndex = 3;" + Environment.NewLine +
                        "            this.passwordBox.Text = \"\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // passwordLabel" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.passwordLabel.Location = new System.Drawing.Point(16, 48);" + Environment.NewLine +
                        "            this.passwordLabel.Name = \"passwordLabel\";" + Environment.NewLine +
                        "            this.passwordLabel.Size = new System.Drawing.Size(160, 32);" + Environment.NewLine +
                        "            this.passwordLabel.TabIndex = 2;" + Environment.NewLine +
                        "            this.passwordLabel.Text = \"Enter the password for the remote computer:\";" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // OKButton" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.OKButton.Location = new System.Drawing.Point(40, 88);" + Environment.NewLine +
                        "            this.OKButton.Name = \"OKButton\";" + Environment.NewLine +
                        "            this.OKButton.Size = new System.Drawing.Size(128, 23);" + Environment.NewLine +
                        "            this.OKButton.TabIndex = 4;" + Environment.NewLine +
                        "            this.OKButton.Text = \"OK\";" + Environment.NewLine +
                        "            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // cancelButton" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;" + Environment.NewLine +
                        "            this.cancelButton.Location = new System.Drawing.Point(200, 88);" + Environment.NewLine +
                        "            this.cancelButton.Name = \"cancelButton\";" + Environment.NewLine +
                        "            this.cancelButton.Size = new System.Drawing.Size(128, 23);" + Environment.NewLine +
                        "            this.cancelButton.TabIndex = 5;" + Environment.NewLine +
                        "            this.cancelButton.Text = \"Cancel\";" + Environment.NewLine +
                        "            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            // MyQuerySample" + Environment.NewLine +
                        "            // " + Environment.NewLine +
                        "            this.AcceptButton = this.OKButton;" + Environment.NewLine +
                        "            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);" + Environment.NewLine +
                        "            this.CancelButton = this.cancelButton;" + Environment.NewLine +
                        "            this.ClientSize = new System.Drawing.Size(368, 130);" + Environment.NewLine +
                        "            this.ControlBox = false;" + Environment.NewLine +
                        "            this.Controls.Add(this.cancelButton);" + Environment.NewLine +
                        "            this.Controls.Add(this.OKButton);" + Environment.NewLine +
                        "            this.Controls.Add(this.passwordBox);" + Environment.NewLine +
                        "            this.Controls.Add(this.passwordLabel);" + Environment.NewLine +
                        "            this.Controls.Add(this.userNameBox);" + Environment.NewLine +
                        "            this.Controls.Add(this.userNameLabel);" + Environment.NewLine +
                        "            this.Name = \"MyQuerySample\";" + Environment.NewLine +
                        "            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;" + Environment.NewLine +
                        "            this.Text = \"Remote Connection\";" + Environment.NewLine +
                        "            this.ResumeLayout(false);" + Environment.NewLine +
                        Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        [STAThread]" + Environment.NewLine +
                        "        static void Main() " + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            Application.Run(new WMIReceiveEvent());" + Environment.NewLine +
                        "        }" + Environment.NewLine +
                        Environment.NewLine +
                        "        private void OKButton_Click(object sender, System.EventArgs e)" + Environment.NewLine +
                        "        {" + Environment.NewLine +
                        "            try" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                ConnectionOptions connection = new ConnectionOptions();" + Environment.NewLine +
                        "                connection.Username = userNameBox.Text;" + Environment.NewLine +
                        "                connection.Password = passwordBox.Text;" + Environment.NewLine +
                        "                connection.Authority = \"ntlmdomain:" + this.TargetWindow.GetRemoteComputerDomain() + "\";" + Environment.NewLine +
                        Environment.NewLine +
                        "                ManagementScope scope = new ManagementScope(" + Environment.NewLine +
                        "                    \"\\\\\\\\" + this.TargetWindow.GetRemoteComputerName() + "\\\\" + this.NamespaceList_event.Text.Replace("\\", "\\\\") + "\", connection);" + Environment.NewLine +
                        "                scope.Connect();" + Environment.NewLine +Environment.NewLine;
                }
                else if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code +
                        "using System;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class WMIReceiveEvent" + Environment.NewLine +
                        "    {" + Environment.NewLine;
                    if(this.Asynchronous.Checked)
                    {
                        code = code +
                            "        public WMIReceiveEvent()" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            try" + Environment.NewLine +
                            "            {" + Environment.NewLine +
                            "                string ";

                        string delimStr = " ,\n";
                        char [] delimiter = delimStr.ToCharArray();
                        string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);


                        code = code + "strComputer = \"";
                    
                        code = code + split[0].Trim() + "\";" + Environment.NewLine + Environment.NewLine;
                    }
                    else
                    {
                        code = code +
                            "        public static void Main()" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            try" + Environment.NewLine +
                            "            {" + Environment.NewLine +
                            "                string ";

                        string delimStr = " ,\n";
                        char [] delimiter = delimStr.ToCharArray();
                        string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);


                        code = code + "strComputer = \"";
                    
                        code = code + split[0].Trim() + "\";" + Environment.NewLine + Environment.NewLine;
                    }

                }
                else
                {
                    // The target computer is the local computer. 
                    code = code +
                        "using System;" + Environment.NewLine +
                        "using System.Management;" + Environment.NewLine +
                        "using System.Windows.Forms;" + Environment.NewLine +
                        Environment.NewLine +
                        "namespace WMISample" + Environment.NewLine +
                        "{" + Environment.NewLine +
                        "    public class WMIReceiveEvent" + Environment.NewLine +
                        "    {" + Environment.NewLine;
                    if(this.Asynchronous.Checked)
                    {
                        code = code + 
                            "        public WMIReceiveEvent()" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            try" + Environment.NewLine +
                            "            {" + Environment.NewLine ;
                    }
                    else
                    {
                        code = code + 
                            "        public static void Main()" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            try" + Environment.NewLine +
                            "            {" + Environment.NewLine;
                    }
                }

                string eventQuery = "";

                if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code + 
                        "                string scope = \"\\\\\\\\\" + strComputer + \"\\\\" + this.NamespaceList_event.Text.Replace("\\", "\\\\") + "\";" + Environment.NewLine + Environment.NewLine +
                        "                string query = " + Environment.NewLine +
                        "                    \"SELECT * FROM " + this.ClassList_event.Text ;
                }
                else
                {
                    code = code + 
                        "                WqlEventQuery query = new WqlEventQuery(" + Environment.NewLine +
                        "                    \"SELECT * FROM " + this.ClassList_event.Text ;
                }
                eventQuery = "select * from " + this.ClassList_event.Text;
				
                if(this.PropertyList_event.SelectedItems.Count.Equals(1))
                {
                    code = code + " WHERE " + PropertyList_event.SelectedItem.ToString().Replace("\\", "\\\\");
                    eventQuery = eventQuery + " where " + PropertyList_event.SelectedItem.ToString();
                }
                else if(this.PropertyList_event.SelectedItems.Count > 0)
                {
						
                    code = code + " WHERE \" +" + Environment.NewLine + "                    ";
                    eventQuery = eventQuery + " where ";

                    int flag = -1;
                    string instance = "";
                    for(int i=0; i < PropertyList_event.SelectedItems.Count; i++)
                    {
                        // If PropertyList_event contains a selected item that contains ISA.
                        if(PropertyList_event.SelectedItems[i].ToString().IndexOf("ISA", 1, PropertyList_event.SelectedItems[i].ToString().Length -1 ) > 0)
                        {
                            flag = i;
                            instance = PropertyList_event.SelectedItems[i].ToString().Replace("\\", "\\\\");
                        }
                    }
                    if(flag > -1)
                    {
                        code = code + "\"" + instance;
                        eventQuery = eventQuery + instance;
                    }
						
                    for(int i=0; i < PropertyList_event.SelectedItems.Count; i++)
                    {
                        if(flag.Equals(-1) && i.Equals(0)) //Do not start off with quotes.
                        {
                            code = code + "\"" + PropertyList_event.SelectedItems[i].ToString().Replace("\\", "\\\\");
                            eventQuery = eventQuery + "\"" + PropertyList_event.SelectedItems[i].ToString();
                        }
                        else if(!i.Equals(flag))
                        {
                            code = code + "\" +" + Environment.NewLine +
                                "                    \" AND " + PropertyList_event.SelectedItems[i].ToString().Replace("\\", "\\\\");
                            eventQuery = eventQuery + " and " + PropertyList_event.SelectedItems[i].ToString();
                        }
                    }		
                }

                if(this.GroupRemoteComputerMenu.Checked)
                    code = code + "\";";
                else
                    code = code + "\");";
				
                // Check to see if the event class is supported by an event provider.
                if(this.QueryCounter == 0)
                {
                    EventQuerySupportedByProvider();
                    this.PollLabel.Visible = false;
                    this.SecondsBox.Visible = false;
                    this.PollLabelEnd.Visible = false;
                }
                    
                if(this.QueryCounter > 0)
                {
                    bool addWITHINStatement = true;

                    // If the user selected event query is in the list of event provider supported
                    // event queries, then the WITHIN statement does not need to be used in
                    // the user selected event query.
                    for(int i=0; i < this.QueryCounter; i++)
                    {
                        if(eventQuery.IndexOf(this.SupportedEventQueries[i].
                            Replace("\"", "'").
                            Replace("isa", "ISA")) != -1)
                        {
                            addWITHINStatement = false; // Do not add the WITHIN statement.
                            break; // Get out of the for loop.
                        }
                    }

                    if(addWITHINStatement && !this.ExtrinsicEvent(this.ClassList_event.Text))
                    {
                        code = code.Replace(("SELECT * FROM " + this.ClassList_event.Text), 
                            ("SELECT * FROM " + this.ClassList_event.Text + " WITHIN " + this.SecondsBox.Text));
                        this.PollLabel.Visible = true;
                        this.SecondsBox.Visible = true;
                        this.PollLabelEnd.Visible = true;
                    }
                    else
                    {
                        this.PollLabel.Visible = false;
                        this.SecondsBox.Visible = false;
                        this.PollLabelEnd.Visible = false;
                    }
                }

                if(this.GroupRemoteComputerMenu.Checked)
                {
                    code = code + Environment.NewLine + Environment.NewLine +
                        "                ManagementEventWatcher watcher = new ManagementEventWatcher(scope, query);" + Environment.NewLine +
                        "                Console.WriteLine(\"Waiting for an event on \" + strComputer + \" ...\");" + Environment.NewLine + Environment.NewLine;
                }
                else if(this.RemoteComputerMenu.Checked)
                {
                    code = code + Environment.NewLine + Environment.NewLine +
                        "                ManagementEventWatcher watcher = new ManagementEventWatcher(scope, query);" + Environment.NewLine +
                        "                Console.WriteLine(\"Waiting for an event on " + this.TargetWindow.GetRemoteComputerName() + " ...\");" + Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    code = code + Environment.NewLine + Environment.NewLine +
                        "                ManagementEventWatcher watcher = new ManagementEventWatcher(query);" + Environment.NewLine +
                        "                Console.WriteLine(\"Waiting for an event...\");" + Environment.NewLine + Environment.NewLine;
                }

                // Semisynchronous or synchronous event.
                if(!this.Asynchronous.Checked)
                {
                    
                    code = code +
                        "                ManagementBaseObject eventObj = watcher.WaitForNextEvent();" + Environment.NewLine + Environment.NewLine +
                        "                Console.WriteLine(\"{0} event occurred.\", eventObj[\"__CLASS\"]);" + Environment.NewLine + Environment.NewLine +
                        "                // Cancel the event subscription" + Environment.NewLine +
                        "                watcher.Stop();" + Environment.NewLine;
					
                    if(this.RemoteComputerMenu.Checked)
                    {
                        code = code + Environment.NewLine + 
                            "                Close();" + Environment.NewLine +
                            "                return;" + Environment.NewLine +
                            "            }" + Environment.NewLine +
                            "            catch(ManagementException err)" + Environment.NewLine +
                            "            {" + Environment.NewLine +
                            "                MessageBox.Show(\"An error occurred while trying to receive an event: \" + err.Message);" + Environment.NewLine +
                            "            }" + Environment.NewLine +
                            "            catch(System.UnauthorizedAccessException unauthorizedErr)" + Environment.NewLine +
                            "            {" + Environment.NewLine +
                            "                MessageBox.Show(\"Connection error (user name or password might be incorrect): \" + unauthorizedErr.Message);" + Environment.NewLine +
                            "            }" + Environment.NewLine +
                            "        }" + Environment.NewLine +
                            Environment.NewLine +
                            "        private void cancelButton_Click(object sender, System.EventArgs e)" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            Close();" + Environment.NewLine +
                            "        }" + Environment.NewLine +
                            "    }" + Environment.NewLine +
                            "}" + Environment.NewLine;
                    }
                    else 
                    {
                        code = code +
                            "                return;" + Environment.NewLine +
                            "            }" + Environment.NewLine +
                            "            catch(ManagementException err)" + Environment.NewLine +
                            "            {" + Environment.NewLine +
                            "                MessageBox.Show(\"An error occurred while trying to receive an event: \" + err.Message);" + Environment.NewLine +
                            "            }" + Environment.NewLine +
                            "        }" +
                            Environment.NewLine + "    }" +
                            Environment.NewLine + "}";
                    }
                }
                else   // Asyncronous event.
                {

                    code = code +
                        "                watcher.EventArrived += " + Environment.NewLine +
                        "                    new EventArrivedEventHandler(" + Environment.NewLine +
                        "                    HandleEvent);" + Environment.NewLine + Environment.NewLine +
                        "                // Start listening for events" + Environment.NewLine +
                        "                watcher.Start();"  + Environment.NewLine + Environment.NewLine +
                        "                // Do something while waiting for events" + Environment.NewLine +
                        "                System.Threading.Thread.Sleep(10000);" + Environment.NewLine + Environment.NewLine +
                        "                // Stop listening for events" + Environment.NewLine +
                        "                watcher.Stop();" + Environment.NewLine +
                        "                return;" + Environment.NewLine +
                        "            }" + Environment.NewLine +
                        "            catch(ManagementException err)" + Environment.NewLine +
                        "            {" + Environment.NewLine +
                        "                MessageBox.Show(\"An error occurred while trying to receive an event: \" + err.Message);" + Environment.NewLine +
                        "            }" + Environment.NewLine;
                        
					
                    if(this.RemoteComputerMenu.Checked)
                    {
                        code = code +
                            "            catch(System.UnauthorizedAccessException unauthorizedErr)" + Environment.NewLine +
                            "            {" + Environment.NewLine +
                            "                MessageBox.Show(\"Connection error (user name or password might be incorrect): \" + unauthorizedErr.Message);" + Environment.NewLine +
                            "            }" + Environment.NewLine +
                            "        }" +
                            Environment.NewLine +
                            Environment.NewLine +
                            "        private void cancelButton_Click(object sender, System.EventArgs e)" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            Close();" + Environment.NewLine +
                            "        }" + Environment.NewLine + Environment.NewLine +
                            "        private void HandleEvent(object sender," + Environment.NewLine +
                            "            EventArrivedEventArgs e)" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            Console.WriteLine(\"" + this.ClassList_event.Text + " event occurred.\");" + Environment.NewLine +
                            "        }" + Environment.NewLine +
                            "    }" + Environment.NewLine +
                            "}" + Environment.NewLine;
                    }
                    else
                    {
                        code = code +
                            "        }" + Environment.NewLine +
                            "        " + Environment.NewLine +
                            "        private void HandleEvent(object sender," + Environment.NewLine +
                            "            EventArrivedEventArgs e)" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            Console.WriteLine(\"" + this.ClassList_event.Text + " event occurred.\");" + Environment.NewLine +
                            "        }" + Environment.NewLine + Environment.NewLine +
                            "        public static void Main()" + Environment.NewLine +
                            "        {" + Environment.NewLine +
                            "            WMIReceiveEvent receiveEvent = new WMIReceiveEvent();" + Environment.NewLine +
                            "            return;" + Environment.NewLine +
                            "        }" + Environment.NewLine +
                            Environment.NewLine + "    }" +
                            Environment.NewLine + "}";
                    }

                }
                this.CodeText_event.Text = code;

            }
        }
		
        //-------------------------------------------------------------------------
        // Handles the form's load event.
        // 
        //-------------------------------------------------------------------------
        private void WMICodeBuddy_Load(object sender, System.EventArgs e)
        {
        
        }

        //-------------------------------------------------------------------------
        // Handles the event when the ValueButton is clicked. Adds values to the
        // query tab's list of property values.
        //-------------------------------------------------------------------------
        private void ValueButton_Click(object sender, System.EventArgs e)
        {
            
            this.ValueList.Items.Clear();
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddValuesToList));
            this.AddValuesToList(this);
        }

        //-------------------------------------------------------------------------
        // Generates code whenever a value is selected in the query tab's
        // property value list.
        //-------------------------------------------------------------------------
        private void ValueList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GenerateQueryCode();
        }

        //-------------------------------------------------------------------------
        // Adds in-parameters (from the selected method in the method list) to the
        // in-parameter list in the method tab.
        //-------------------------------------------------------------------------
        private void AddInParams()
        {
            try
            {
                ManagementClass c = new ManagementClass(this.NamespaceValue_m.Text, this.ClassList_m.Text, null);

                foreach (MethodData mData in c.Methods)
                {
                    if(mData.Name.Equals(this.MethodList.SelectedItem.ToString()))
                    {
                        if(mData.InParameters.Properties.Count.Equals(0))
                        {
                            // No in-parameters to define.
                        }
                        else
                        {
                            foreach (PropertyData p in mData.InParameters.Properties)
                            {
                                this.InParameterBox.Items.Add(
                                    p.Name + " = " +
                                    " a value of type: " + p.Type.ToString());
                            }
                        }
                    }

                }
            }
            catch (System.NullReferenceException e)
            {
                // No in-parameters to define.
            }
        }

        //-------------------------------------------------------------------------
        // Handles the event when the "list all properties in the class" 
        // button is clicked on the browse tab.
        //-------------------------------------------------------------------------
        private void BrowsePropertyButton_Click(object sender, System.EventArgs e)
        {
            this.PropertyInformation.Text = "";
            this.BrowsePropertyList.Items.Clear();
            this.BrowsePropertyStatus.Text = "";

            // Populate the class list.
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddPropertiesToBrowserList));
            this.AddPropertiesToBrowserList(this);
        }

        //-------------------------------------------------------------------------
        // Handles the event when the "list all methods in the class" 
        // button is clicked on the browse tab.
        //-------------------------------------------------------------------------
        private void BrowseMethodButton_Click(object sender, System.EventArgs e)
        {
            this.BrowseMethodList.Items.Clear();
            this.BrowseMethodStatus.Text = "";
            this.MethodInformation.Text = "";

            // Populate the class list.
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddMethodsToBrowserList));
            this.AddMethodsToBrowserList(this);
        }

        //-------------------------------------------------------------------------
        // Handles the event when the namespace is changed on the method tab.
        //
        //-------------------------------------------------------------------------
        private void NamespaceValue_m_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ClassList_m.Items.Clear();
            this.ClassList_m.Text = "";
            this.MethodList.Items.Clear();
            this.MethodList.Text = "";
            this.InParameterBox.Items.Clear();
            this.KeyValueBox.Items.Clear();
            this.KeyValueBox.Visible = false;
            this.KeyValueLabel.Visible = false;
            this.MethodLinkLabel.Visible = false;
            this.MethodStatus.Text = "";
            this.CodeText_m.Text = "";

            // Populate the class list.
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddClassesToMethodPageList));
            this.AddClassesToMethodPageList(this);
        }

        //-------------------------------------------------------------------------
        // Handles the event when the namespace is changed on the query tab.
        //
        //-------------------------------------------------------------------------
        private void NamespaceValue_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ClassList.Items.Clear();
            this.ClassList.Text = "";
            this.PropertyList.Items.Clear();
            this.ValueList.Items.Clear();
            this.CodeText.Text = "";
            this.ClassStatus.Text = "";
            this.PropertyStatus.Text = "";
            this.ValueStatus.Text = "";
            this.QueryLinkLabel.Visible = false;

            // Populate the class list.
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddClassesToList));
            this.AddClassesToList(this);
        }

        private void MainTabControl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        
        }

        //-------------------------------------------------------------------------
        // Handles the event when the class is changed on the query tab.
        //
        //-------------------------------------------------------------------------
        private void ClassList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Clears out all the other information forms.
            this.PropertyList.Items.Clear();
            this.ValueList.Items.Clear();
            this.PropertyStatus.Text = "";
            this.ValueStatus.Text = "";
            this.CodeText.Text = "";

            // Display the link to the class documentation.
            // Note: This link changes if MSDN changes the URL for the WMI SDK documentation.
            if(this.QueryLinkLabel.Links.Count > 0)
            {
                this.QueryLinkLabel.Links[0].LinkData = "www.msdn.microsoft.com/library/default.asp?url=/library/en-us/wmisdk/wmi/" + this.ClassList.Text + ".asp";
            }
            else
            {
                this.QueryLinkLabel.Links.Add(0, this.MethodLinkLabel.Text.Length, "www.msdn.microsoft.com/library/default.asp?url=/library/en-us/wmisdk/wmi/" + this.ClassList.Text + ".asp");
            }

            // All the Win32 classes are documented and have links to the documentation.
            if(this.ClassList.Text.StartsWith("Win32"))
            {
                this.QueryLinkLabel.Visible = true;
            }
            else
            {
                this.QueryLinkLabel.Visible = false;
            }

            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddPropertiesToList));
            this.AddPropertiesToList(this);
        }

        //-------------------------------------------------------------------------
        // Handles the event when the class is changed on the method tab.
        //
        //-------------------------------------------------------------------------
        private void ClassList_m_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.MethodList.Items.Clear();
            this.MethodList.Text = "";
            this.CodeText_m.Text = "";
            this.InParameterBox.Items.Clear();
            this.KeyValueBox.Items.Clear();
            this.KeyValueBox.Visible = false;
            this.KeyValueLabel.Visible = false;
            this.MethodStatus.Text = "";

            // Display the link to the class documentation.
            // Note: This link changes if MSDN changes the URL for the WMI SDK documentation.
            if(this.MethodLinkLabel.Links.Count > 0)
            {
                this.MethodLinkLabel.Links[0].LinkData = "www.msdn.microsoft.com/library/default.asp?url=/library/en-us/wmisdk/wmi/" + this.ClassList_m.Text + ".asp";
            }
            else
            {
                this.MethodLinkLabel.Links.Add(0, this.MethodLinkLabel.Text.Length, "www.msdn.microsoft.com/library/default.asp?url=/library/en-us/wmisdk/wmi/" + this.ClassList_m.Text + ".asp");
            }

            // All the Win32 classes are documented, and have links to the documentation.
            if(this.ClassList_m.Text.StartsWith("Win32"))
            {   
                this.MethodLinkLabel.Visible = true;
            }
            else
            {
                this.MethodLinkLabel.Visible = false;
            }

            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddMethodsToList));
            this.AddMethodsToList(this);

        }

        //-------------------------------------------------------------------------
        // Handles the event when the method is changed on the method tab.
        //
        //-------------------------------------------------------------------------
        private void MethodList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.CodeText_m.Text = "";
                this.InParameterBox.Items.Clear();
                this.KeyValueBox.Items.Clear();

                AddInParams();

                if(InParameterBox.Items.Count > 0) 
                {
                    // Create a new InParameterArray for all the items in the list. 
                    if(InParameterBox.Items.Count < MAXINPARAMS)
                    {
                        System.Array.Clear(this.InParameterArray, 0, InParameterArray.Length);
                        this.InParameterArray = new InParameterWindow[InParameterBox.Items.Count];

                        for(int i = 0; i < InParameterBox.Items.Count; i++)
                        {
                            InParameterArray[i] = new InParameterWindow(this);
                            InParameterArray[i].SetParameterName((InParameterBox.Items[i].ToString().Split(" ".ToCharArray()))[0]);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Method has too many in-Parameters.  Choose a different method.");
                    }
                }

            
                if(this.IsStaticMethodSelected())
                {
                    GenerateMethodCode();
                }
                else
                {
                    this.KeyValueLabel.Visible = true;
                    this.KeyValueBox.Visible = true;
                    //System.Threading.ThreadPool.
                    //    QueueUserWorkItem(
                    //    new System.Threading.WaitCallback(
                    //    this.AddKeyValues_m));
                    this.AddKeyValues_m(this);
                }
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("Error creating code: WMI class not found.");
                else
                    MessageBox.Show("Error creating code: " + mErr.Message.ToString());
            }
            
        }


        //-------------------------------------------------------------------------
        // Adds the key property values to a list on the method tab.
        //
        //-------------------------------------------------------------------------
        private void AddKeyValues_m(object o)
        {
			
            this.KeyValueLabel.Text = "Gathering data ...";

            string keyValues = "";

            try 
            {

                ObjectGetOptions options = new ObjectGetOptions();
                ManagementClass wmiObject = new ManagementClass(this.NamespaceValue_m.Text,
                    this.ClassList_m.Text, options);
                wmiObject.Options.UseAmendedQualifiers = true;

                foreach ( ManagementObject c in wmiObject.GetInstances())
                {
                
                    foreach (PropertyData p in c.Properties)
                    {
                        foreach (QualifierData q in p.Qualifiers)
                        {
                            // Gets the key property values.
                            if(q.Name.Equals("key"))
                            {
                                if(keyValues.Length == 0)
                                {
                                    keyValues = p.Name + "='" +
                                        c.GetPropertyValue(
                                        p.Name) + "'";
                                }
                                else
                                {
                                    keyValues = keyValues + "," + p.Name + "='" +
                                        c.GetPropertyValue(
                                        p.Name) + "'";
                                }
                            }
                        }

                    }

                    this.KeyValueBox.Items.Add(keyValues);
                    keyValues = "";
                }
            }
            catch (ManagementException ex) 
            {
                this.KeyValueLabel.Text = ex.Message;
            }
			
            if(this.KeyValueBox.Items.Count > 0)
            {
                this.KeyValueLabel.Text = "Select the instance you want to execute the query on. The values in the list are the values of the key property for this class.";
            }
            else
            {
                this.KeyValueLabel.Visible = false;
                this.KeyValueBox.Visible = false;
            }
			
        }


        //-------------------------------------------------------------------------
        // Handles the event when the namespace is changed on the event tab.
        //
        //-------------------------------------------------------------------------
        private void NamespaceList_event_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ClassList_event.Items.Clear();
            this.ClassList_event.Text = "";
            this.PropertyList_event.Items.Clear();
            this.PropertyList_event.Text = "";
            this.TargetClassList_event.Items.Clear();
            this.TargetClassList_event.Text = "";
            this.CodeText_event.Text = "";
            this.EventLinkLabel.Visible = false;

            // Reset the QueryCounter so the list of supported event queries is namespace
            // specific
            this.QueryCounter = 0;

            // Populates the class list on the event page.
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddClassesToEventPageList));
            this.AddClassesToEventPageList(this);

        }

        //-------------------------------------------------------------------------
        // Handles the event when the class is changed on the event tab.
        //
        //-------------------------------------------------------------------------
        private void ClassList_event_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.PropertyList_event.Items.Clear();
            this.PropertyList_event.Text = "";
            this.CodeText_event.Text = "";
            this.PropertyValueLabel.Text = "";
            this.PropertyValueLabel.Visible = false;
            this.TargetClassList_event.Items.Clear();
            this.TargetClassList_event.Text = "";
            this.TargetClassList_event.Visible = false;
            this.PropertyList_event.Items.Clear();


            if(this.ClassList_event.Text.StartsWith("__Class"))
            {
                this.PropertyValueLabel.Text = "TargetClass:";
                this.PropertyValueLabel.Visible = true;
                this.TargetClassList_event.Visible = true;
                
                // Populates the class list on the event page.
                //System.Threading.ThreadPool.
                //    QueueUserWorkItem(
                //    new System.Threading.WaitCallback(
                //    this.AddClassesToTargetClassList));
                this.AddClassesToTargetClassList(this);
            }
            if(this.ClassList_event.Text.StartsWith("__MethodInvocationEvent"))
            {
                this.PropertyValueLabel.Text = "TargetInstance:";
                this.PropertyValueLabel.Visible = true;
                this.TargetClassList_event.Visible = true;
                
                // Populates the class list on the event page.
                //System.Threading.ThreadPool.
                //    QueueUserWorkItem(
                //    new System.Threading.WaitCallback(
                //    this.AddMethodClassesToTargetClassList));
                this.AddMethodClassesToTargetClassList(this);
            }
            else if(this.ClassList_event.Text.StartsWith("__Namespace"))
            {
                this.PropertyValueLabel.Text = "TargetNamespace:";
                this.PropertyValueLabel.Visible = true;
                this.TargetClassList_event.Visible = true;				

                // Populates the class list on the event page.
                //System.Threading.ThreadPool.
                //    QueueUserWorkItem(
                //    new System.Threading.WaitCallback(
                //    this.AddNamespacesToTargetList));
                this.AddNamespacesToTargetList(this);
            }
            else if(this.ClassList_event.Text.StartsWith("__Instance"))
            {
                this.PropertyValueLabel.Text = "TargetInstance:";
                this.PropertyValueLabel.Visible = true;
                this.TargetClassList_event.Visible = true;

                // Populates the class list on the event page.
                //System.Threading.ThreadPool.
                //    QueueUserWorkItem(
                //    new System.Threading.WaitCallback(
                //    this.AddClassesToTargetClassList));
                this.AddClassesToTargetClassList(this);
            }
            else
            {
	
                AddEventClassProperties();


                if(this.PropertyList_event.Items.Count > 0) 
                {
                    System.Array.Clear(this.EventConditionArray, 0, this.EventConditionArray.Length);
                    this.EventConditionArray = new EventQueryCondition[PropertyList_event.Items.Count];

                    for(int i = 0; i < PropertyList_event.Items.Count; i++)
                    {
                        EventConditionArray[i] = new EventQueryCondition(this);
                        EventConditionArray[i].SetParameterName((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[0]);

                        if(PropertyList_event.Items[i].ToString().StartsWith("TargetClass ISA") ||
                            PropertyList_event.Items[i].ToString().StartsWith("PreviousClass ISA") ||
                            PropertyList_event.Items[i].ToString().StartsWith("TargetInstance ISA") ||
                            PropertyList_event.Items[i].ToString().StartsWith("PreviousInstance ISA") ||
                            PropertyList_event.Items[i].ToString().StartsWith("TargetNamespace =") ||
                            PropertyList_event.Items[i].ToString().StartsWith("PreviousNamespace ="))
                        {
                            EventConditionArray[i].ChangeOperator((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[1]);
                            EventConditionArray[i].ChangeTextBoxValue((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[2]);
                        }	
						
                    }
					
                }
            }

            // Display the link to the class documentation.
            // Note: This link changes if MSDN changes the URL for the WMI SDK documentation.
            if(this.EventLinkLabel.Links.Count > 0)
            {
                this.EventLinkLabel.Links[0].LinkData = "www.msdn.microsoft.com/library/default.asp?url=/library/en-us/wmisdk/wmi/" + this.ClassList_event.Text + ".asp";
            }
            else
            {
                this.EventLinkLabel.Links.Add(0, this.EventLinkLabel.Text.Length, "www.msdn.microsoft.com/library/default.asp?url=/library/en-us/wmisdk/wmi/" + this.ClassList_event.Text + ".asp");
            }

            // All the event classes in the root\cimv2 namespace are documented.
            if(this.NamespaceList_event.Text.Equals("root\\CIMV2"))
            {   
                this.EventLinkLabel.Visible = true;
            }
            else
            {
                this.EventLinkLabel.Visible = false;
            }

            GenerateEventCode();
        }

        //-------------------------------------------------------------------------
        // Adds properties (from an event class on the event tab) to a list on
        // the event tab.
        //-------------------------------------------------------------------------
        private void AddEventClassProperties()
        {
            try
            {
                ManagementClass c = new ManagementClass(this.NamespaceList_event.Text, this.ClassList_event.Text, null);

                foreach (PropertyData p in c.Properties)
                {
                    this.PropertyList_event.Items.Add(p.Name);
                }
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("WMI class not found.");
                else
                    MessageBox.Show(mErr.Message.ToString());
            }
        }

        //-------------------------------------------------------------------------
        // Adds properties (from a target class on the event tab) to a list on
        // the event tab.
        //-------------------------------------------------------------------------
        private void AddTargetClassProperties()
        {
            try
            {
                ManagementClass c = new ManagementClass(this.NamespaceList_event.Text, this.ClassList_event.Text, null);

                foreach (PropertyData p in c.Properties)
                {
                    this.PropertyList_event.Items.Add(p.Name);
                }

                if(this.ClassList_event.Text.StartsWith("__Instance"))
                {
                    ManagementClass c2 = new ManagementClass(this.NamespaceList_event.Text, this.TargetClassList_event.Text, null);

                    foreach (PropertyData p2 in c2.Properties)
                    {
                    
                        this.PropertyList_event.Items.Add("TargetInstance." + p2.Name);
                    
                        if(this.ClassList_event.Text.StartsWith("__InstanceModification"))
                        {
                            this.PropertyList_event.Items.Add("PreviousInstance." + p2.Name);
                        }
                    } 
                }

                if(this.PropertyList_event.Items.Contains("TargetInstance"))
                {
                    this.PropertyList_event.Items.Remove("TargetInstance");
                    this.PropertyList_event.Items.Add("TargetInstance ISA '" + 
                        this.TargetClassList_event.Text + "'");
                }
                if(this.PropertyList_event.Items.Contains("PreviousInstance"))
                {
                    this.PropertyList_event.Items.Remove("PreviousInstance");
                    this.PropertyList_event.Items.Add("PreviousInstance ISA '" + 
                        this.TargetClassList_event.Text + "'");
                }
                if(this.PropertyList_event.Items.Contains("TargetClass"))
                {
                    this.PropertyList_event.Items.Remove("TargetClass");
                    this.PropertyList_event.Items.Add("TargetClass ISA '" + 
                        this.TargetClassList_event.Text + "'");
                }
                if(this.PropertyList_event.Items.Contains("PreviousClass"))
                {
                    this.PropertyList_event.Items.Remove("PreviousClass");
                    this.PropertyList_event.Items.Add("PreviousClass ISA '" + 
                        this.TargetClassList_event.Text + "'");
                }
                if(this.PropertyList_event.Items.Contains("TargetNamespace"))
                {
                    this.PropertyList_event.Items.Remove("TargetNamespace");
                    this.PropertyList_event.Items.Add("TargetNamespace = '" + 
                        this.TargetClassList_event.Text + "'");
                }
                if(this.PropertyList_event.Items.Contains("PreviousNamespace"))
                {
                    this.PropertyList_event.Items.Remove("PreviousNamespace");
                    this.PropertyList_event.Items.Add("PreviousNamespace = '" + 
                        this.TargetClassList_event.Text + "'");
                }
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("WMI class not found.");
                else
                    MessageBox.Show(mErr.Message.ToString());
            }
        }

        //-------------------------------------------------------------------------
        // Handles the event when the link to the WMI class documentation on MSDN
        // is clicked.
        //-------------------------------------------------------------------------
        private void MethodLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;

            // If the value looks like a URL, navigate to it.
            // Otherwise, display it in a message box.
            if(null != target && target.StartsWith("www"))
            {
                System.Diagnostics.Process.Start(target);
            }
            else
            {    
                MessageBox.Show("Item clicked: " + target);
            }
        }

        //-------------------------------------------------------------------------
        // Handles the event when the link to the WMI class documentation on MSDN
        // is clicked.
        //-------------------------------------------------------------------------
        private void QueryLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;

            // If the value looks like a URL, navigate to it.
            // Otherwise, display it in a message box.
            if(null != target && target.StartsWith("www"))
            {
                System.Diagnostics.Process.Start(target);
            }
            else
            {    
                MessageBox.Show("Item clicked: " + target);
				
            }
        }

        //-------------------------------------------------------------------------
        // Handles the event when a method in-parameter is selected.
        // 
        //-------------------------------------------------------------------------
        private void InParameterBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
            for(int i = 0; i < InParameterBox.Items.Count; i++)
            {
                try
                {
                    if(InParameterBox.SelectedIndices.Contains(i) && !InParameterArray[i].GetOkClicked())
                    {
                    
                        InParameterArray[i].Visible = true;
                        InParameterArray[i].ChangeText(
                            "Assign a value to the " + InParameterArray[i].GetParameterName()
                            + " parameter. The parameter is of type: " +
                            InParameterArray[i].GetParameterType() + ".");
				
                    }
                }
                catch (System.NullReferenceException nullError)
                {
                    // Catches the case if the window was closed.
                    InParameterArray[i] = new InParameterWindow(this);
                    InParameterArray[i].SetParameterName((InParameterBox.Items[i].ToString().Split(" ".ToCharArray()))[0]);
                    InParameterArray[i].Visible = true;
                    InParameterArray[i].ChangeText(
                        "Assign a value to the " + InParameterArray[i].GetParameterName()
                        + " parameter. The parameter is of type: " +
                        InParameterArray[i].GetParameterType() + ".");
                }
                
                
                if(!InParameterBox.SelectedIndices.Contains(i))
                {
                    try
                    {
                        InParameterArray[i].Visible = false;
                        InParameterArray[i].SetOkClicked(false);
                    }
                    catch (System.NullReferenceException nullError)
                    {
                        // Catches the case if the window was closed.
                        InParameterArray[i] = new InParameterWindow(this);
                        InParameterArray[i].SetParameterName((InParameterBox.Items[i].ToString().Split(" ".ToCharArray()))[0]);
                        InParameterArray[i].Visible = false;
                        InParameterArray[i].SetOkClicked(false);
                    }
                }
            }

            this.GenerateMethodCode();
        }

        //-------------------------------------------------------------------------
        // Handles the event when the link to the WMI class documentation on MSDN
        // is clicked.
        //-------------------------------------------------------------------------
        private void EventLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;

            // If the value looks like a URL, navigate to it.
            // Otherwise, display it in a message box.
            if(null != target && target.StartsWith("www"))
            {
                System.Diagnostics.Process.Start(target);
            }
            else
            {    
                MessageBox.Show("Item clicked: " + target);
				
            }
        }

        //-------------------------------------------------------------------------
        // Handles the event when the user changes the event polling interval.
        //
        //-------------------------------------------------------------------------
        private void SecondsBox_TextChanged(object sender, System.EventArgs e)
        {
            GenerateEventCode();
        }

        //-------------------------------------------------------------------------
        // Handles the event when the namespace is changed on the browse tab.
        //
        //-------------------------------------------------------------------------
        private void BrowseNamespaceList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.BrowseClassList.Items.Clear();
            this.BrowseClassList.Text = "";
            this.BrowseClassResults.Text = "";
            this.BrowseMethodList.Items.Clear();
            this.BrowseMethodStatus.Text = "";
            this.BrowsePropertyList.Items.Clear();
            this.BrowsePropertyStatus.Text = "";
            this.BrowseQualiferStatus.Text = "";
            this.BrowseQualifierList.Items.Clear();
            this.PropertyInformation.Text = "";
            this.BrowseClassDescription.Text = "";
            this.MethodInformation.Text = "";

            // Populates the class list.
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddClassesToBrowserList));
            this.AddClassesToBrowserList(this);
        }

        //-------------------------------------------------------------------------
        // Handles the event when the class is changed on the browse tab.
        // 
        //-------------------------------------------------------------------------
        private void BrowseClassList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.BrowseMethodList.Items.Clear();
            this.BrowseMethodStatus.Text = "";
            this.BrowsePropertyList.Items.Clear();
            this.BrowsePropertyStatus.Text = "";
            this.BrowseQualiferStatus.Text = "";
            this.BrowseQualifierList.Items.Clear();
            this.PropertyInformation.Text = "";
            this.BrowseClassDescription.Text = "";
            this.MethodInformation.Text = "";

            // Gets the class description.
            try
            {
                // Gets the property qualifiers.
                ObjectGetOptions op = new ObjectGetOptions(null, System.TimeSpan.MaxValue, true);

                ManagementClass mc = new ManagementClass(this.BrowseNamespaceList.Text,
                    this.BrowseClassList.Text, op);
                mc.Options.UseAmendedQualifiers = true;

                foreach (QualifierData dataObject in
                    mc.Qualifiers)
                {
                    if(dataObject.Name.Equals("Description"))
                    {
                        this.BrowseClassDescription.Text = 
                            dataObject.Value.ToString();
                    }
                }
            }
            catch (ManagementException mErr)
            {
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("WMI class or not found.");
                else
                    MessageBox.Show(mErr.Message.ToString());
            }


        }

        //-------------------------------------------------------------------------
        // Handles the event when the OpenQueryText button is clicked. This opens
        // the code (in the CodeText text box) in Notepad. 
        //-------------------------------------------------------------------------
        private void OpenQueryText_Click(object sender, System.EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIQuery.vbs";

            if(this.VbNetMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIQuery.vb";
            }
            else if(this.CSharpMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIQuery.cs";
            }
            else if(this.VbsMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIQuery.vbs";
            };

	
            OpenTextInNotepad(path, this.CodeText.Text);
        }

        //-------------------------------------------------------------------------
        // Handles the event when the OpenMethodText button is clicked. This opens
        // the code (in the CodeText_m text box) in Notepad.
        //-------------------------------------------------------------------------
        private void OpenMethodText_Click(object sender, System.EventArgs e)
        {
            // Creates the file path.
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod.vbs";

            if(this.VbNetMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod.vb";
            }
            else if(this.CSharpMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod.cs";
            }
            else if(this.VbsMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod.vbs";
            };


            OpenTextInNotepad(path, this.CodeText_m.Text);
        }

        //-------------------------------------------------------------------------
        // Handles the event when the OpenEventText button is clicked.  This opens
        // the code (in the CodeText_event text box) in Notepad.
        //-------------------------------------------------------------------------
        private void OpenEventText_Click(object sender, System.EventArgs e)
        {
            // Creates the path to the file to open in Notepad.
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent.vbs";

            if(this.VbNetMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent.vb";
            }
            else if(this.CSharpMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent.cs";
            }
            else if(this.VbsMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent.vbs";
            };

            OpenTextInNotepad(path, this.CodeText_event.Text);
        }

        //-------------------------------------------------------------------------
        // Opens the specified code text in a specified file (path) in
        // Notepad.
        //-------------------------------------------------------------------------
        private void OpenTextInNotepad(string path, string text)
        {
            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator");
            try 
            {
                // Determines whether the directory exists.
                if (di.Exists) 
                {
                    //Do nothing.
                    ;
                }
                else
                {
                    // Creates the directory.
                    di.Create();
                }

                // Deletes the file if it exists.
                if (File.Exists(path)) 
                {
                    File.Delete(path);
                }

                // Creates the file.
                using (FileStream fs = File.Create(path)) 
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(text);
                    // Add information to the file.
                    fs.Write(info, 0, info.Length);
                }

                //Get the object on which the method is invoked.
                ManagementClass processClass = new ManagementClass("Win32_Process");

                //Get an in-parameter object for this method
                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                //Fill in the in-parameter values.
                inParams["CommandLine"] = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\notepad.exe \"" + path + "\"";
                
                //Execute the method.
                ManagementBaseObject outParams = processClass.InvokeMethod ("Create", inParams, null);
            }
            catch (System.IO.IOException error)
            {
                MessageBox.Show("Failed to create process. " + error.Message);
            }
            catch (System.Management.ManagementException mError)
            {
                MessageBox.Show("Failed to create process. " + mError.Message);
            }
        }

        //-------------------------------------------------------------------------
        // Handles the event when the ExecuteQueryButton button is clicked.  This 
        // compiles the code (in C# or VB .NET) and runs it. 
        //-------------------------------------------------------------------------
        private void ExecuteQueryButton_Click(object sender, System.EventArgs e)
        {
            // Generates the file that contains the code.
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIQuery_Script.vbs";

            if(this.VbNetMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIQuery_VB.vb";
            }
            else if(this.CSharpMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIQuery_CS.cs";
            }
            else if(this.VbsMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIQuery_Script.vbs";
            };

            
            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator");
            try 
            {
                // Determines whether the directory exists.
                if (di.Exists) 
                {
                    //Do nothing.
                    ;
                }
                else
                {
                    // Creates the directory.
                    di.Create();
                }
                // Deletes the file if it exists.
                if (File.Exists(path)) 
                {
                    File.Delete(path);
                }

                // Creates the file.
                using (FileStream fs = File.Create(path)) 
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(this.CodeText.Text);
                    // Add information to the file.
                    fs.Write(info, 0, info.Length);
                }
			
                //Gets the object on which the method is invoked.
                ManagementClass processClass = new ManagementClass("Win32_Process");

                //Gets an in-parameter object for this method.
                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                if(this.VbsMenuItem.Checked)
                {
                    //Fill in the in-parameter values.
                    inParams["CommandLine"] = "cmd /k cscript.exe \"" + path + "\"";
                }
                else if(this.CSharpMenuItem.Checked)
                {
                    if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyQuery_CS.exe"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyQuery_CS.exe");
                    }

                    string frameworkVersion = NativeMethods.SystemDirectory();

                    //Fill in the in-parameter values.
                    inParams["CommandLine"] = "cmd /k cd " + frameworkVersion + " & csc.exe /target:exe /r:System.Management.dll /r:System.Data.dll /r:System.Drawing.dll /r:System.Drawing.Design.dll /r:System.Windows.Forms.dll /r:System.dll /out:\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyQuery_CS.exe\" \"" + path +
                        "\" & \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyQuery_CS.exe\"";
                }
                else if(this.VbNetMenuItem.Checked)
                {
                    if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyQuery_VB.exe"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyQuery_VB.exe");
                    }

                    string frameworkVersion = NativeMethods.SystemDirectory();

                    //Fill in the in-parameter values.
                    inParams["CommandLine"] = "cmd /k cd " + frameworkVersion + " & vbc.exe /target:exe /r:System.Management.dll /r:System.Data.dll /r:System.Drawing.dll /r:System.Drawing.Design.dll /r:System.Windows.Forms.dll /r:System.dll /out:\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyQuery_VB.exe\" \"" + path +
                        "\" & \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyQuery_VB.exe\"";
                }
                // Executes the process Create method and runs the code.
                ManagementBaseObject outParams = processClass.InvokeMethod ("Create", inParams, null);
            }
            catch (System.IO.IOException error)
            {
                MessageBox.Show("Failed to create process. " + error.Message);
            }
            catch (System.Management.ManagementException mError)
            {
                MessageBox.Show("Failed to create process. " + mError.Message);
            }
        }

        //-------------------------------------------------------------------------
        // Handles the event when the ExecuteMethodButton button is clicked. This 
        // compiles the code (in C# or VB .NET) and runs it. 
        //-------------------------------------------------------------------------
        private void ExecuteMethodButton_Click(object sender, System.EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_Script.vbs";

            if(this.VbNetMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_VB.vb";
            }
            else if(this.CSharpMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_CS.cs";
            }
            else if(this.VbsMenuItem.Checked)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_Script.vbs";
            };


            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator");
            try 
            {
                // Determines whether the directory exists.
                if (di.Exists) 
                {
                    //Do nothing.
                    ;
                }
                else
                {
                    // Creates the directory.
                    di.Create();
                }

                // Deletes the file if it exists.
                if (File.Exists(path)) 
                {
                    File.Delete(path);
                }

                // Creates the file.
                using (FileStream fs = File.Create(path)) 
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(this.CodeText_m.Text);
                    // Add information to the file.
                    fs.Write(info, 0, info.Length);
                }
 
                //Gets the object on which the method isinvoked.
                ManagementClass processClass = new ManagementClass("Win32_Process");

                //Get an in-parameter object for this method.
                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                if(this.VbsMenuItem.Checked)
                {
                    //Fills in the in-parameter values.
                    inParams["CommandLine"] = "cmd /k cscript.exe \"" + path + "\"";
                }
                else if(this.CSharpMenuItem.Checked)
                {
                    if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_CS.exe"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_CS.exe");
                    }

                    string frameworkVersion = NativeMethods.SystemDirectory();

                    //Fills in the in-parameter values.
                    inParams["CommandLine"] = "cmd /k cd " + frameworkVersion + " & csc.exe /target:exe /r:System.Management.dll /r:System.Data.dll /r:System.Drawing.dll /r:System.Drawing.Design.dll /r:System.Windows.Forms.dll /r:System.dll /out:\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_CS.exe\" \"" + path +
                        "\" & \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_CS.exe\"";
                }
                else if(this.VbNetMenuItem.Checked)
                {
                    if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_VB.exe"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_VB.exe");
                    }

                    string frameworkVersion = NativeMethods.SystemDirectory();

                    //Fills in the in-parameter values.
                    inParams["CommandLine"] = "cmd /k cd " + frameworkVersion + " & vbc.exe /target:exe /r:System.Management.dll /r:System.Data.dll /r:System.Drawing.dll /r:System.Drawing.Design.dll /r:System.Windows.Forms.dll /r:System.dll /out:\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_VB.exe\" \"" + path +
                        "\" & \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIMethod_VB.exe\"";
                }

                //Executes the method.
                ManagementBaseObject outParams = processClass.InvokeMethod ("Create", inParams, null);
            }
            catch (System.IO.IOException error)
            {
                MessageBox.Show("Failed to create process. " + error.Message);
            }
            catch (System.Management.ManagementException mError)
            {
                MessageBox.Show("Failed to create process. " + mError.Message);
            }
        }

		//-------------------------------------------------------------------------
		// Handles the event when the ExecuteEventCodeButton button is clicked. This 
		// compiles the code (in C# or VB .NET) and runs it. 
		//-------------------------------------------------------------------------
        private void ExecuteEventCodeButton_Click(object sender, System.EventArgs e)
        {
            string code = this.CodeText_event.Text;

			if(this.GroupRemoteComputerMenu.Checked)
			{
				string delimStr = " ,\n";
				char [] delimiter = delimStr.ToCharArray();
				string [] split = this.TargetWindow.GetArrayOfComputers().Split(delimiter);
              
                string newStrComputer = "";
                string oldStrComputer = "";

                if(split.Length <= 25)
                {
                    for(int i=0; i < split.Length; i++)
                    {
                        if(split[i].Trim().Length == 0 || split[i].Trim().Equals(" ") || split[i].Trim().Equals(",") || split[i].Trim().Equals("\n"))
                        {
                            ;
                        }
                        else
                        {

                            if(this.CSharpMenuItem.Checked)
                            {
                                newStrComputer = "string strComputer = \"" + split[i].Trim() + "\";";
                            }
                            else
                            {
                                newStrComputer = "strComputer = \"" + split[i].Trim() + "\"";
                            }
                            

                            string path = "";

                            if(this.VbNetMenuItem.Checked)
                            {
                                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_VB" + i + ".vb";
                            }
                            else if(this.CSharpMenuItem.Checked)
                            {
                                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_CS" + i + ".cs";
                            }
                            else if(this.VbsMenuItem.Checked)
                            {
                                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_Script" + i + ".vbs";
                            }

                            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator");
                            try 
                            {
                                // Determines whether the directory exists.
                                if (di.Exists) 
                                {
                                    //Do nothing
                                    ;
                                }
                                else
                                {
                                    // Create the directory.
                                    di.Create();
                                }

                                // Deletes the file if it exists.
                                if (File.Exists(path)) 
                                {
                                    File.Delete(path);
                                }

                                if(i > 0)
                                {
                                    this.CodeText_event.Text = this.CodeText_event.Text.Replace(oldStrComputer, newStrComputer);
                                    oldStrComputer = newStrComputer;     
                                }
                                else
                                {
                                    oldStrComputer = newStrComputer;
                                }

                                // Creates the file.
                                using (FileStream fs = File.Create(path)) 
                                {
                                    Byte[] info = new UTF8Encoding(true).GetBytes(this.CodeText_event.Text);
                                    // Add information to the file.
                                    fs.Write(info, 0, info.Length);
                                }
				
						
                                //Get the object on which the method is invoked.
                                ManagementClass processClass = new ManagementClass("Win32_Process");

                                //Get an in-parameter object for this method.
                                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                                if(this.VbsMenuItem.Checked)
                                {
                                    //Fill in the in-parameter values.
                                    inParams["CommandLine"] = "cmd /k cscript.exe \"" + path + "\"";
                                }
                                else if(this.CSharpMenuItem.Checked)
                                {
                                    if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent" + i + "_CS.exe"))
                                    {
                                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent" + i + "_CS.exe");
                                    }

                                    string frameworkVersion = NativeMethods.SystemDirectory();

                                    //Fills in the in-parameter values.
                                    inParams["CommandLine"] = "cmd /k cd " + frameworkVersion + " & csc.exe /target:exe /r:System.Management.dll /r:System.Data.dll /r:System.Drawing.dll /r:System.Drawing.Design.dll /r:System.Windows.Forms.dll /r:System.dll /out:\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent" + i + "_CS.exe\" \"" + path +
                                        "\" & \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent" + i + "_CS.exe\"";
                                }
                                else if(this.VbNetMenuItem.Checked)
                                {
                                    if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent" + i + "_VB.exe"))
                                    {
                                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent" + i + "_VB.exe");
                                    }

                                    string frameworkVersion = NativeMethods.SystemDirectory();

                                    //Fills in the in-parameter values.
                                    inParams["CommandLine"] = "cmd /k cd " + frameworkVersion + " & vbc.exe /target:exe /r:System.Management.dll /r:System.Data.dll /r:System.Drawing.dll /r:System.Drawing.Design.dll /r:System.Windows.Forms.dll /r:System.dll /out:\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent" + i + "_VB.exe\" \"" + path +
                                        "\" & \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent" + i + "_VB.exe\"";
                                }

                                // Executes the method.
                                ManagementBaseObject outParams = processClass.InvokeMethod ("Create", inParams, null);
                            }
                            catch (System.IO.IOException error)
                            {
                                MessageBox.Show("Failed to create process. " + error.Message);
                            }
                            catch (System.Management.ManagementException mError)
                            {
                                MessageBox.Show("Failed to create process. " + mError.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Too many computers in the list. Only 25 computers in the list are allowed.");
                    return;
                }
			}
			else
			{
				
				string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_Script.vbs";

				if(this.VbNetMenuItem.Checked)
				{
					path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_VB.vb";
				}
				else if(this.CSharpMenuItem.Checked)
				{
					path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_CS.cs";
				}
				else if(this.VbsMenuItem.Checked)
				{
					path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_Script.vbs";
				}

					DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator");
					try 
					{
						// Determines whether the directory exists.
						if (di.Exists) 
						{
							//Do nothing.
							;
						}
						else
						{
							// Try to create the directory.
							di.Create();
						}
					// Deletes the file if it exists.
					if (File.Exists(path)) 
					{
						File.Delete(path);
					}

					// Creates the file.
					using (FileStream fs = File.Create(path)) 
					{
						Byte[] info = new UTF8Encoding(true).GetBytes(this.CodeText_event.Text);
						// Add information to the file.
						fs.Write(info, 0, info.Length);
					}
			
					// Get the object on which the method is invoked.
					ManagementClass processClass = new ManagementClass("Win32_Process");

					// Get an in-parameter object for this method.
					ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

						if(this.VbsMenuItem.Checked)
						{
							// Fill in the in-parameter values.
							inParams["CommandLine"] = "cmd /k cscript.exe \"" + path + "\"";
						}
						else if(this.CSharpMenuItem.Checked)
						{
							if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_CS.exe"))
							{
								File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_CS.exe");
							}

                            string frameworkVersion = NativeMethods.SystemDirectory();

							// Fill in the in-parameter values.
							inParams["CommandLine"] = "cmd /k cd " + frameworkVersion + " & csc.exe /target:exe /r:System.Management.dll /r:System.Data.dll /r:System.Drawing.dll /r:System.Drawing.Design.dll /r:System.Windows.Forms.dll /r:System.dll /out:\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_CS.exe\" \"" + path +
								"\" & \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_CS.exe\"";
						}
						else if(this.VbNetMenuItem.Checked)
						{
							if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_VB.exe"))
							{
								File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_VB.exe");
							}

                            string frameworkVersion = NativeMethods.SystemDirectory();

							// Fill in the in-parameter values.
							inParams["CommandLine"] = "cmd /k cd " + frameworkVersion + " & vbc.exe /target:exe /r:System.Management.dll /r:System.Data.dll /r:System.Drawing.dll /r:System.Drawing.Design.dll /r:System.Windows.Forms.dll /r:System.dll /out:\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_VB.exe\" \"" + path +
								"\" & \"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\MyWMIEvent_VB.exe\"";
						}

					// Execute the method.
					ManagementBaseObject outParams = processClass.InvokeMethod ("Create", inParams, null);
				    }
                    catch (System.IO.IOException error)
                    {
                        MessageBox.Show("Failed to create process. " + error.Message);
                    }
                    catch (System.Management.ManagementException mError)
                    {
                        MessageBox.Show("Failed to create process. " + mError.Message);
                    }
			}

            this.CodeText_event.Text = code;
        }

		//-------------------------------------------------------------------------
		// Handles the event when the BrowseQualifierButton button is clicked. 
		// This method populates the BrowseQualifierList with class qualifiers.
		//-------------------------------------------------------------------------
        private void BrowseQualifierButton_Click(object sender, System.EventArgs e)
        {
            this.BrowseQualifierList.Items.Clear();
            this.BrowseQualiferStatus.Text = "";

            // Populates the class list.
            //System.Threading.ThreadPool.
            //    QueueUserWorkItem(
            //    new System.Threading.WaitCallback(
            //    this.AddQualifiersToBrowserList));
            this.AddQualifiersToBrowserList(this);

            
        }

		//-------------------------------------------------------------------------
		// Handles the event when the File->Exit menu item is selected.
		//
		//-------------------------------------------------------------------------
        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

		//-------------------------------------------------------------------------
		// Handles the event when the Help->Query For WMI Data 
		// menu item is selected. This method opens the help in a .txt file.
		//-------------------------------------------------------------------------
        private void QueryHelpMenuItem_Click(object sender, System.EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\QueryHelp.txt";

            // Help text.
            string queryHelp = System.Environment.NewLine + System.Environment.NewLine +
                "***************************************" + System.Environment.NewLine + 
                "WMI Code Creator Help" + System.Environment.NewLine +
                System.Environment.NewLine +
                "Querying for Data Using WMI" + System.Environment.NewLine +
                "***************************************" + System.Environment.NewLine +
                System.Environment.NewLine +
                System.Environment.NewLine +
                "One of the main tasks in WMI is querying WMI for information about computer components and software. For example, you can request that WMI return the name and version of an operating system, or the amount of free disk space on a hard disk. The information that you query is made available through WMI classes that are installed in the WMI repository on a computer.  Each class is a part of a namespace, with each namespace holding similar classes.  For example, the root\\CIMV2 namespace contains classes that hold information about the Windows platform and your computer components." + System.Environment.NewLine +
                System.Environment.NewLine +
                "To locate management information through WMI, you use a language similar to SQL called the WMI Query Language (WQL). A basic WQL query remains fairly understandable for people with a basic knowledge of SQL. Therefore, WQL is dedicated to WMI and is designed to perform queries against the WMI repository to retrieve information or receive event notifications." + System.Environment.NewLine +
                System.Environment.NewLine +
                "The following steps describe how to use the WMI Code Creator to query WMI for data:" + System.Environment.NewLine +
                System.Environment.NewLine +
                "1. Select a namespace.  Each namespace holds classes that expose different types of information. The most commonly used namespace is root\\CIMV2 because it contains most of the classes that model Windows managed resources." + System.Environment.NewLine +
                System.Environment.NewLine +
                "2. Select a class from the namespace.  The class list is populated with classes from the selected namespace that have a dynamic qualifier (classes that are instantiated and expose data) or static qualifier." + System.Environment.NewLine +
                System.Environment.NewLine +
                "3. Select each property (from the list of class properties) that you want to get a value for.  You can select multiple properties by using either the SHIFT key or the CTRL key in combination with a left-click." + System.Environment.NewLine +
                System.Environment.NewLine +
                "4. (Optional) Click the Search for Property Values button to get all the values for the properties you selected in the property list.  If the property value list contains more than one value for a property, then there are multiple instances of the class you selected, and each instance has a value displayed in the property value list.  Properties with an array data type are not listed because they cannot be used in a WQL query." + System.Environment.NewLine +
                System.Environment.NewLine +
                "5. (Optional) Narrow the scope of your query. Select one value out of the property value list that you want to include in your WQL query.  By including a value in your query, you can refine your query to return information only from the instances that contain the value you have selected." + System.Environment.NewLine +
                System.Environment.NewLine +
                "6. Select the data source for your query. You can query for information about the computer you are using by selecting Local Computer from the Target Computer menu.  You can query for information about a remote computer by selecting Remote Computer from the Target Computer Menu, or you can query for information about a group of computers by selecting Group of Remote Computers from the Target Computer menu.  If you get the data from a group of computers, each computer must be in the same domain, and you need to be an administrator on each computer in the group (unless you alter the code).  When querying for information about a remote computer, you must enter the full name (or the IP address) of the remote computer. The full computer name can be found by clicking the Start button, right-clicking on My Computer and selecting Properties, and then selecting the Computer Name tab." + System.Environment.NewLine +
                System.Environment.NewLine +
                "7. Select a code language (for the generated code) from the Code Language menu." + System.Environment.NewLine;

            OpenTextInNotepad(path, queryHelp);
        }

		//-------------------------------------------------------------------------
		// Handles the event when the Help->Executing a Method in WMI 
		// menu item is selected. This method opens the help in a .txt file.
		//-------------------------------------------------------------------------
        private void MethodHelpMenuItem_Click(object sender, System.EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\ExecutingAMethodHelp.txt";
            
            // Help text.
            string methodHelp = System.Environment.NewLine + System.Environment.NewLine +
                "***************************************" + System.Environment.NewLine + 
                "WMI Code Creator Help" + System.Environment.NewLine +
                System.Environment.NewLine +
                "Executing a Method from a WMI Class" + System.Environment.NewLine +
                "***************************************" + System.Environment.NewLine +
                System.Environment.NewLine +
                System.Environment.NewLine +
                "One of the main tasks in WMI is executing a method from a WMI class. For example, you can execute the Reboot method in the Win32_OperatingSystem class to reboot a computer. There is a variety of executable methods available through WMI classes installed in the WMI repository on a computer.  Each class is in a namespace, with each namespace holding similar classes.  For example, the root\\CIMV2 namespace contains classes that hold information about the Windows platform and your computer components." + System.Environment.NewLine +
                System.Environment.NewLine +
                "When executing a method in WMI, you are executing either a static method of a WMI class or a method of a WMI class instance.  When you are executing a method of a class instance, you must specify which instance of the class you will use to execute the method. Each class instance has a set of properties, which includes a key property or a set of key properties.  Each separate instance has a different value for its key property. You specify which instance of the class you want to execute the method from by specifying a specific value of the class' key property." + System.Environment.NewLine +
                System.Environment.NewLine +
                "You must also assign values to a method's in-parameters before you execute a method (unless the method does not have any in-parameters). Not all in-parameters of a method require a value (some can be optional). For example, if you are trying to execute the Create method of the Win32_Process class to start a new process, you can specify a value for the CommandLine in-parameter (such as \"notepad.exe\" to start notepad), but you do not need to assign values to the CurrentDirectory or ProcessStartupInformation in-parameters." + System.Environment.NewLine +
                System.Environment.NewLine +
                "The following steps describe how to use the WMI Code Creator to execute a method from a WMI class:" + System.Environment.NewLine +
                System.Environment.NewLine +
                "1. Select a namespace.  Each namespace holds classes that expose different data. The most commonly used namespace is root\\CIMV2 because it contains most of the classes that model Windows managed resources." + System.Environment.NewLine +
                System.Environment.NewLine +
                "2. Select a class from the namespace.  The class list is populated with classes (only classes that contain methods) from the selected namespace." + System.Environment.NewLine +
                System.Environment.NewLine +
                "3. Select the method you want to execute from the Methods drop-down list. This will populate the in-parameter list with all the in-parameters for the method you selected. If the method you selected is not static, this will also bring up a list of key property values for all the instances of the class." + System.Environment.NewLine +
                System.Environment.NewLine +
                "4. Assign values to the in-parameters.  You must assign a value to each of the required in-parameters that are passed into the method to successfully call the method. For some methods, not all in-parameters in the list may be require a value.  When you select an in-parameter in the list, an input window for the in-parameter will appear.  When you enter the value for the in-parameter into the input window and click the Ok button, the value is entered in the generated code in the WMI Code Creator." + System.Environment.NewLine +
                System.Environment.NewLine +
                "5. Select the instance you want to execute the query on. The values in the list are the values of the key property for this class. The values are gathered from the local computer; thus, if you want to run the code on a remote computer, you may want to enter a value into the code manually." + System.Environment.NewLine +
                System.Environment.NewLine +
                "6. Select the computer you want to execute the method on. You can execute the method on the computer you are using by selecting Local Computer from the Target Computer menu. You can execute a method on one remote computer by selecting Remote Computer from the Target Computer menu, or you can execute a method on a group of computers by selecting Group of Remote Computers from the Target Computer menu.  If you execute a method on a group of computers, each of the computers need to be in the same domain, and you need to be an administrator on each computer in the group (unless you alter the code).  When executing a method on a remote computer, you need to enter in the full name (or the IP address) of the remote computer. The full computer name can be found by clicking the Start button, right-clicking on My Computer and selecting Properties, and then selecting the Computer Name tab." + System.Environment.NewLine +
                System.Environment.NewLine +
                "7. Select a code language (for the generated code) from the Code Language menu." + System.Environment.NewLine;

            OpenTextInNotepad(path, methodHelp);
                
        }

		//-------------------------------------------------------------------------
		// Handles the event when the Help->Browsing WMI namespaces 
		// menu item is selected. This method opens the help in a .txt file.
		//-------------------------------------------------------------------------
        private void BrowseHelpMenuItem_Click(object sender, System.EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\BrowsingWMINamespacesHelp.txt";

            // Help text
            string browseHelp = System.Environment.NewLine + System.Environment.NewLine +
                "***************************************************" + System.Environment.NewLine + 
                "WMI Code Creator Help" + System.Environment.NewLine +
                System.Environment.NewLine +
                "Browsing the Namespaces on the Local Computer" + System.Environment.NewLine +
                "***************************************************" + System.Environment.NewLine +
                System.Environment.NewLine +
                System.Environment.NewLine +
                "Each class in WMI is in a namespace, with each namespace holding similar classes.  For example, the root\\CIMV2 namespace contains classes that hold information about the Windows platform and your computer components. Each WMI class can have properties, methods, and qualifiers. A qualifier is a modifier that contains information that describes a class, instance, property, method, or parameter. Qualifiers are defined by the Common Information Model (CIM), by the CIM Object Manager, and by developers who create new classes." + System.Environment.NewLine +
                System.Environment.NewLine +
                "The following steps describe how to use the WMI Code Creator to browse the namespaces on a local computer:" + System.Environment.NewLine +
                System.Environment.NewLine +
                "1. Select a namespace.  Each namespace holds classes that expose different data. The most commonly used namespace is root\\CIMV2." + System.Environment.NewLine +
                System.Environment.NewLine +
                "2. Select a class from the namespace.  The class list is populated with all the classes from the selected namespace. If the selected class has a Description qualifier, then the value of that qualifier is displayed in the Class Description text box." + System.Environment.NewLine +
                System.Environment.NewLine +
                "3. Click the List all the properties in the class button to populate the property list with all the properties from the selected class.  When you select a property in the property list, the property description is displayed. The property description comes from the value of the Description qualifier for the selected property." + System.Environment.NewLine +
                System.Environment.NewLine +
                "4. Click the List all the methods in the class button to populate the method list with all the methods from the selected class. When you select a method in the method list, the method description is displayed.  The method description comes from the value of the Description qualifier for the selected method." + System.Environment.NewLine +
                System.Environment.NewLine +
                "5. Click the List all the qualifiers for the class button to populate the qualifier list will all the qualifiers from the selected class." + System.Environment.NewLine +
                System.Environment.NewLine;  

            OpenTextInNotepad(path, browseHelp);
        }

		//-------------------------------------------------------------------------
		// Handles the event when the Help->Receiving an event 
		// menu item is selected. This method opens the help in a .txt file
		//-------------------------------------------------------------------------
        private void EventHelpMenuItem_Click(object sender, System.EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WMICodeCreator\\ReceivingAnEventHelp.txt";

			// Help text
			string eventHelp = System.Environment.NewLine + System.Environment.NewLine +
				"***************************************" + System.Environment.NewLine + 
				"WMI Code Creator Help" + System.Environment.NewLine +
				System.Environment.NewLine +
				"Receiving Event Notifications" + System.Environment.NewLine +
				"***************************************" + System.Environment.NewLine +
				System.Environment.NewLine +
				System.Environment.NewLine +
                "One of the main tasks in WMI is receiving an event notification that specifies something has happened or changed on a computer. For example, you can receive a notification every time a new process is started, a remote computer is shut down, or when a service is stopped. Event classes in WMI monitor when a specified event happens. Events are monitored either by WMI (intrinsic event classes) or by an event provider (extrinsic events classes). WMI monitors events by polling for changes on a computer during a polling interval.  For example, if you want WMI to notify you every time a process is created, WMI will poll the list of processes on a computer, and if the amount of processes in the list increases, then WMI sends an event notification.  You specify how often WMI polls for an event by specifying a polling interval in an event query. The more often you tell WMI to poll for an event, the more the CPU resources will be used. Some events are monitored by an event provider, in which case you do not have to specify a polling interval because the event provider will take care of all the event monitoring." + Environment.NewLine +
                Environment.NewLine +
                "Each event class is in a namespace, with each namespace holding similar classes.  For example, the root\\CIMV2 namespace contains classes that hold information about the Windows platform and your computer components. To receive an event, you create an event query that specifies an event class and, if necessary, the values of event class properties. The WMI Code Creator walks you through the steps of creating an event query.  A basic event query is formatted as follows: “SELECT * FROM <EventClass> <OptionalPollingInterval> WHERE <EventClassProperty> <operator> <UserDefinedValue>”. For example the event query, “SELECT * FROM __InstanceCreationEvent WITHIN 5 WHERE TargetInstance ISA ‘Win32_Process’ AND TargetInstance.Name = ‘notepad.exe’”, is an event query that polls WMI every 5 seconds for an event where an instance of the Win32_Process class is created (a process is created) that has the Win32_Process.Name property (the process name) equal to notepad.exe." + Environment.NewLine +
                Environment.NewLine +
                "The following steps describe how to use the WMI Code Creator to receive event notifications:" + Environment.NewLine +
                Environment.NewLine +
                "1. Select a namespace.  Each namespace holds classes that expose different data. The most commonly used namespace is root\\CIMV2." + Environment.NewLine +
                Environment.NewLine +
                "2. Select a class from the namespace.  The event class list is populated with classes from the selected namespace that are derived from the __Event class.  These classes can be used to receive event notifications." + Environment.NewLine +
                Environment.NewLine +
                "3. If a new drop-down list appears below the event class drop-down list (after completing step two), select a value for the event class property that is specified to the left of the list. If no new drop-down list appears below the event class drop-down list after completing step two, skip to step 4." + Environment.NewLine +
                Environment.NewLine +
                "If you selected the __ClassCreationEvent, __ClassDeletionEvent, __ClassModificationEvent, or the __ClassOperationEvent event class in step two, select a value for the TargetClass property." + Environment.NewLine +
                Environment.NewLine +
                "If you selected the __InstanceCreationEvent, __InstanceDeletionEvent, __InstanceModificationEvent, or the __InstanceOperationEvent event class in step two, select a value for the TargetInstance property." + Environment.NewLine +
                Environment.NewLine +
                "If you selected the __NamespaceCreationEvent, __NamespaceDeletionEvent, __NamespaceModificationEvent, or the __NamespaceOperationEvent event class in step two, select a value for the TargetNamespace property." + Environment.NewLine +
                Environment.NewLine +
                "If you selected the __MethodInvocationEvent event class in step two, select a value for the TargetInstance property." + Environment.NewLine +
                Environment.NewLine +
                "4. Assign values to event query conditions.  You must select and assign a value to all the event query conditions you want to use in your WQL event query. Not all the event query conditions are required.  Each event query condition in the list that you select will bring up an input window for the event query condition.  When you enter the value for the event query condition into the input window and click the Ok button on the input window, the value is inserted into the generated code in the WMI Code Creator." + Environment.NewLine +
                Environment.NewLine +
                "5. (optional) If prompted, enter the polling interval (how often WMI will poll for the event notification).  The polling interval is defined by the WITHIN statement in the event query in the generated code." + Environment.NewLine +
                Environment.NewLine +
				"6. Select if you want to receive event notifications asynchronously or not.  Receiving event notifications asynchronously allows you to execute code while receiving events (without waiting for a notification)." + Environment.NewLine +
				Environment.NewLine +
                "7. Select the target computer you want to receive events from by selecting a menu item from the Target Computer menu.  You can receive event notifications on the computer you are using by selecting the Local Computer from the Target Computer menu.  You can receive event notifications from a remote computer by selecting Remote Computer from the Target Computer menu, or you can receive event notifications from a group of computers by selecting Group of Remote Computers from the Target Computer menu.  If you receive event notifications from a group of computers, each of the computers need to be in the same domain, and you need to be an administrator on each computer in the group (unless you alter the code).  When receiving event notifications from a remote computer, you need to enter in the full name (or the IP Address) of the remote computer. The full computer name can be found by clicking the Start button, right-clicking on My Computer and selecting Properties, and then selecting the Computer Name tab." + Environment.NewLine +
                Environment.NewLine +
                "8. Select a code language (for the generated code) from the Code Language menu." + Environment.NewLine +
                Environment.NewLine;
					
	            OpenTextInNotepad(path, eventHelp);
        }

		//-------------------------------------------------------------------------
		// Handles the event when the user selects a key property value on 
		// the method tab (from the KeyValueBox list).
		//-------------------------------------------------------------------------
		private void KeyValueBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GenerateMethodCode();
		}

		//-------------------------------------------------------------------------
		// Handles the event when the user selects one of the properties in the
		// BrowsePropertyList on the browse tab. This displays the property information.
		//-------------------------------------------------------------------------
		private void BrowsePropertyList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string propertyInfo = "Qualifiers: " + System.Environment.NewLine;
			string description = "Description: " + System.Environment.NewLine;

			try
			{
				// Gets the property qualifiers.
				ObjectGetOptions op = new ObjectGetOptions(null, System.TimeSpan.MaxValue, true);

				ManagementClass mc = new ManagementClass(this.BrowseNamespaceList.Text,
					this.BrowseClassList.Text, op);
				mc.Options.UseAmendedQualifiers = true;

				foreach (PropertyData p in mc.Properties)
				{

					if(p.Name.Equals(this.BrowsePropertyList.SelectedItem))
					{
						foreach (QualifierData q in p.Qualifiers)
						{
							propertyInfo = propertyInfo + q.Name + System.Environment.NewLine;


							if(q.Name.Equals("Description"))
							{
								description = description +
									mc.GetPropertyQualifierValue(p.Name, q.Name) + System.Environment.NewLine;
							}
						}
					}             

					this.PropertyInformation.Text = description + System.Environment.NewLine + propertyInfo;
				}
			}
			catch  (ManagementException mErr)
			{
				this.PropertyInformation.Text = "Could not get property information";
            
                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("WMI class not found.");
                else
                    MessageBox.Show(mErr.Message.ToString());
			}
		}

		//-------------------------------------------------------------------------
		// Handles the event when the user selects one of the methods in the
		// BrowseMethodList on the browse tab. This displays the method information.
		//-------------------------------------------------------------------------
		private void BrowseMethodList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string methodInfo = "Qualifiers: " + System.Environment.NewLine;
			string description = "Description: " + System.Environment.NewLine;

			try
			{
				// Gets the property qualifiers.
				ObjectGetOptions op = new ObjectGetOptions(null, System.TimeSpan.MaxValue, true);

				ManagementClass mc = new ManagementClass(this.BrowseNamespaceList.Text,
					this.BrowseClassList.Text, op);
				mc.Options.UseAmendedQualifiers = true;

				foreach (MethodData m in mc.Methods)
				{

					if(m.Name.Equals(this.BrowseMethodList.SelectedItem))
					{
						foreach (QualifierData q in m.Qualifiers)
						{
							methodInfo = methodInfo + q.Name + System.Environment.NewLine;


							if(q.Name.Equals("Description"))
							{
								description = description +
									q.Value + System.Environment.NewLine;
							}
						}
					}             

					this.MethodInformation.Text = description + System.Environment.NewLine + methodInfo;
				}
			}
			catch  (ManagementException mErr)
			{
				this.MethodInformation.Text = "Could not get method information";

                if(mErr.Message.Equals("Not found "))
                    MessageBox.Show("WMI class or method not found.");
                else
                    MessageBox.Show(mErr.Message.ToString());
			}
		}

		//-------------------------------------------------------------------------
		// Handles the event when the user selects an item in the targetClassList
		// list on the event tab.
		//-------------------------------------------------------------------------
        private void TargetClassList_event_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.PropertyList_event.Items.Clear();
            AddTargetClassProperties();

			if(this.PropertyList_event.Items.Count > 0) 
			{
				System.Array.Clear(this.EventConditionArray, 0, this.EventConditionArray.Length);
				this.EventConditionArray = new EventQueryCondition[PropertyList_event.Items.Count];

				for(int i = 0; i < PropertyList_event.Items.Count; i++)
				{
					EventConditionArray[i] = new EventQueryCondition(this);
					EventConditionArray[i].SetParameterName((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[0]);
				
					if(PropertyList_event.Items[i].ToString().StartsWith("TargetClass ISA") ||
						PropertyList_event.Items[i].ToString().StartsWith("PreviousClass ISA") ||
						PropertyList_event.Items[i].ToString().StartsWith("TargetInstance ISA") ||
						PropertyList_event.Items[i].ToString().StartsWith("PreviousInstance ISA") ||
						PropertyList_event.Items[i].ToString().StartsWith("TargetNamespace =") ||
						PropertyList_event.Items[i].ToString().StartsWith("PreviousNamespace ="))
					{
						EventConditionArray[i].ChangeOperator((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[1]);
						EventConditionArray[i].ChangeTextBoxValue((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[2]);
					}
				}	
			}
        }

		//-------------------------------------------------------------------------
		// Handles the event when the user selects an event class property
		// (from the PropertyList_event list) to include in an event query.
		//-------------------------------------------------------------------------
		private void PropertyList_event_SelectedIndexChanged(object sender, System.EventArgs e)
		{	

			for(int i = 0; i < PropertyList_event.Items.Count; i++)
			{
				try
				{

					if(this.PropertyList_event.SelectedIndices.Contains(i) && !EventConditionArray[i].GetOkClicked())
					{
					
						EventConditionArray[i].Visible = true;
						EventConditionArray[i].ChangeText(
                            "Assign a value to the " + EventConditionArray[i].GetParameterName()
							+ " property. The property is of type: " +
							EventConditionArray[i].GetParameterType() + ".");
					}
				}
				catch (System.NullReferenceException nullError)
				{
					// Catches the case if the window was closed.
					EventConditionArray[i] = new EventQueryCondition(this);
					EventConditionArray[i].SetParameterName((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[0]);
					EventConditionArray[i].Visible = true;
					EventConditionArray[i].ChangeText(
						"Assign a value to the " + EventConditionArray[i].GetParameterName()
						+ " property. The property is of type: " +
						EventConditionArray[i].GetParameterType() + ".");
				
					if(PropertyList_event.Items[i].ToString().StartsWith("TargetClass ISA") ||
						PropertyList_event.Items[i].ToString().StartsWith("PreviousClass ISA") ||
						PropertyList_event.Items[i].ToString().StartsWith("TargetInstance ISA") ||
						PropertyList_event.Items[i].ToString().StartsWith("PreviousInstance ISA") ||
						PropertyList_event.Items[i].ToString().StartsWith("TargetNamespace =") ||
						PropertyList_event.Items[i].ToString().StartsWith("PreviousNamespace ="))
					{
						EventConditionArray[i].ChangeOperator((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[1]);
						EventConditionArray[i].ChangeTextBoxValue((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[2]);
					}
				}
			
				if(!this.PropertyList_event.SelectedIndices.Contains(i))
				{
					try
					{
						EventConditionArray[i].Visible = false;
						EventConditionArray[i].SetOkClicked(false);
					}
					catch (System.NullReferenceException nullError)
					{
						// Catches the case if the window was closed.
						EventConditionArray[i] = new EventQueryCondition(this);
						EventConditionArray[i].SetParameterName((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[0]);
						EventConditionArray[i].Visible = false;
						EventConditionArray[i].SetOkClicked(false);

						if(PropertyList_event.Items[i].ToString().StartsWith("TargetClass ISA") ||
							PropertyList_event.Items[i].ToString().StartsWith("PreviousClass ISA") ||
							PropertyList_event.Items[i].ToString().StartsWith("TargetInstance ISA") ||
							PropertyList_event.Items[i].ToString().StartsWith("PreviousInstance ISA") ||
							PropertyList_event.Items[i].ToString().StartsWith("TargetNamespace =") ||
							PropertyList_event.Items[i].ToString().StartsWith("PreviousNamespace ="))
						{
							EventConditionArray[i].ChangeOperator((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[1]);
							EventConditionArray[i].ChangeTextBoxValue((PropertyList_event.Items[i].ToString().Split(" ".ToCharArray()))[2]);
						}
					}
				}
			}

			this.GenerateEventCode();
		}

		//-------------------------------------------------------------------------
		// Handles the event when the user selects the code language output to
		// be C#.
		//-------------------------------------------------------------------------
		private void CSharpMenuItem_Click(object sender, System.EventArgs e)
		{
            this.CSharpMenuItem.Checked = true;
            this.VbsMenuItem.Checked = false;
            this.VbNetMenuItem.Checked = false;
        
            this.GenerateEventCode();
            this.GenerateQueryCode();
            this.GenerateMethodCode();
		}

		//-------------------------------------------------------------------------
		// Handles the event when the user selects the code language output to
		// be VB .NET.
		//-------------------------------------------------------------------------
		private void VbNetMenuItem_Click(object sender, System.EventArgs e)
		{
			this.CSharpMenuItem.Checked = false;
			this.VbsMenuItem.Checked = false;
			this.VbNetMenuItem.Checked = true;
		
			this.GenerateEventCode();
			this.GenerateQueryCode();
			this.GenerateMethodCode();
		}

		//-------------------------------------------------------------------------
		// Handles the event when the user selects the code language output to
		// be VBScript.
		//-------------------------------------------------------------------------
		private void VbsMenuItem_Click(object sender, System.EventArgs e)
		{
			this.CSharpMenuItem.Checked = false;
			this.VbsMenuItem.Checked = true;
			this.VbNetMenuItem.Checked = false;

			this.GenerateEventCode();
			this.GenerateQueryCode();
			this.GenerateMethodCode();
		}

		//-------------------------------------------------------------------------
		// Handles the event when the user checks or unchecks the Asynchronous
		// check box on the event tab.
		//-------------------------------------------------------------------------
		private void Asynchronous_CheckedChanged(object sender, System.EventArgs e)
		{
		    this.GenerateEventCode();
		}

		//-------------------------------------------------------------------------
		// Handles the event when the user selects the Target Computer-> Local Computer
		// menu item.
		//-------------------------------------------------------------------------
        private void LocalComputerMenu_Click(object sender, System.EventArgs e)
        {
            this.RemoteComputerMenu.Checked = false;
            this.GroupRemoteComputerMenu.Checked = false;
            this.LocalComputerMenu.Checked = true;

            this.GenerateEventCode();
            this.GenerateQueryCode();
            this.GenerateMethodCode();
        }

		//-------------------------------------------------------------------------
		// Handles the event when the user selects the Target Computer-> Remote Computer
		// menu item.
		//-------------------------------------------------------------------------
        private void RemoteComputerMenu_Click(object sender, System.EventArgs e)
        {
            this.RemoteComputerMenu.Checked = true;
            this.GroupRemoteComputerMenu.Checked = false;
            this.LocalComputerMenu.Checked = false;

            this.TargetWindow.SetForRemoteComputerInfo();

            this.TargetWindow.Visible = true;

            this.GenerateEventCode();
            this.GenerateQueryCode();
            this.GenerateMethodCode();
        }

		//-------------------------------------------------------------------------
		// Handles the event when the user selects the Target Computer-> Group of Remote Computers
		// menu item.
		//-------------------------------------------------------------------------
        private void GroupRemoteComputerMenu_Click(object sender, System.EventArgs e)
        {
            this.RemoteComputerMenu.Checked = false;
            this.GroupRemoteComputerMenu.Checked = true;
            this.LocalComputerMenu.Checked = false;

            this.TargetWindow.SetForGroupComputerInfo();

            this.TargetWindow.Visible = true;

            this.GenerateEventCode();
            this.GenerateQueryCode();
            this.GenerateMethodCode();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            bool builtGrid = false;
            ManagementObjectSearcher searcher =
                   new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", rtbQuery.Text);

            dgvResult.DataSource = null;
            dgvResult.Columns.Clear();

            DataTable table = new DataTable("table1");

            foreach (System.Management.ManagementObject mObj in searcher.Get())
            {
                if (builtGrid == false)
                {

                    

                    foreach (System.Management.PropertyData property in mObj.Properties)
                    {
                        DataColumn column = new DataColumn(property.Name, typeof(string));
                        table.Columns.Add(column);
                         Console.WriteLine(property.Name);
                            
                    }
                    builtGrid = true;
                }
                DataRow dr = table.NewRow();
                foreach (System.Management.PropertyData property in mObj.Properties)
                {
                    

                    if (property.Value != null)
                    {
                        dr[property.Name] = property.Value.ToString();
                    }
                    else
                    {
                        dr[property.Name] = "null";
                    }
                }
                table.Rows.Add(dr);

            }

            dgvResult.DataSource = table;
        }
   




    }

 }
