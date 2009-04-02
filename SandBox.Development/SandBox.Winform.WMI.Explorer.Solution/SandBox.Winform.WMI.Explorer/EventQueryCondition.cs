using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace SandBox.Winform.WMI.Explorer
{
    public partial class EventQueryCondition : Form
    {
        private string StoredValue;
        private string ParameterName;
        private bool OkButtonClicked;
        private WMICodeCreator ParentWMIToolForm;

        public EventQueryCondition()
        {
            //
            // Required for Windows Form Designer support.
            //
            InitializeComponent();

            this.OperatorBox.Items.Add("=");
            this.OperatorBox.Items.Add("<>");
            this.OperatorBox.Items.Add(">");
            this.OperatorBox.Items.Add("<");
            this.OperatorBox.Items.Add("ISA");
        }
        public EventQueryCondition(WMICodeCreator parent)
        {
            InitializeComponent();
            this.ParameterName = "";
            this.StoredValue = "";
            this.OkButtonClicked = false;
            this.ParentWMIToolForm = parent;
            this.OperatorBox.Items.Add("=");
            this.OperatorBox.Items.Add("<>");
            this.OperatorBox.Items.Add(">");
            this.OperatorBox.Items.Add("<");
            this.OperatorBox.Items.Add("ISA");
        }
        
        //-------------------------------------------------------------------------
        // Returns the value of OkButtonClicked
        //-------------------------------------------------------------------------
        public bool GetOkClicked()
        {
            return this.OkButtonClicked;
        }

        //-------------------------------------------------------------------------
        // Sets the value of OkButtonClicked
        //-------------------------------------------------------------------------
        public void SetOkClicked(bool setValue)
        {
            this.OkButtonClicked = setValue;
        }

        //-------------------------------------------------------------------------
        // Handles the event when the user clicks the OK button on the
        // EventQueryCondition form.
        //-------------------------------------------------------------------------
        private void OKButton_Click(object sender, System.EventArgs e)
        {

            // Check to see if it is a string value.
            // If it is a string value, add single quote marks.
            if (this.GetParameterType().Equals("String"))
            {
                this.StoredValue = "'" + this.TextBox.Text + "'";
            }
            else
            {
                this.StoredValue = this.TextBox.Text;
            }

            this.Visible = false;
            this.OkButtonClicked = true;

            for (int j = 0; j < this.ParentWMIToolForm.PropertyList_event.Items.Count; j++)
            {

                if (this.ParameterName.Equals(
                    this.ParentWMIToolForm.PropertyList_event.Items[j].ToString().Split(" ".ToCharArray())[0]))
                {
                    string conditionName = this.ParentWMIToolForm.PropertyList_event.Items[j].ToString().Split(" ".ToCharArray())[0];
                    // Update the PropertyList_event item with the input value.
                    this.ParentWMIToolForm.PropertyList_event.Items.RemoveAt(j);
                    this.ParentWMIToolForm.PropertyList_event.Items.Add(conditionName + " " + this.OperatorBox.Text + " " + this.StoredValue);
                    this.ParentWMIToolForm.PropertyList_event.Sorted = true;
                    this.ParentWMIToolForm.PropertyList_event.SetSelected(j, true);
                }

            }

            this.ParentWMIToolForm.GenerateEventCode();
        }

        //-------------------------------------------------------------------------
        // Handles the event when the user clicks the Cancel button on the
        // EventQueryCondition form.
        //-------------------------------------------------------------------------
        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            this.StoredValue = "";
            this.TextBox.Text = "";
            this.Visible = false;
            this.OkButtonClicked = false;

            for (int j = 0; j < this.ParentWMIToolForm.PropertyList_event.Items.Count; j++)
            {
                if (this.ParameterName.Equals(
                    this.ParentWMIToolForm.PropertyList_event.Items[j].ToString().Split(" ".ToCharArray())[0]))
                {
                    // Change the name back to no value.
                    string conditionName = this.ParentWMIToolForm.PropertyList_event.Items[j].ToString().Split(" ".ToCharArray())[0];
                    // Update the PropertyList_event item with the input value.
                    this.ParentWMIToolForm.PropertyList_event.Items.RemoveAt(j);
                    this.ParentWMIToolForm.PropertyList_event.Items.Add(conditionName);
                    this.ParentWMIToolForm.PropertyList_event.Sorted = true;

                    this.ParentWMIToolForm.PropertyList_event.SetSelected(j, false);
                }

            }

            this.ParentWMIToolForm.GenerateEventCode();
        }

        //-------------------------------------------------------------------------
        // Handles the event when the user types in a value for an
        // event query condition form.
        //-------------------------------------------------------------------------
        private void TextBox_TextChanged(object sender, System.EventArgs e)
        {
            this.StoredValue = this.TextBox.Text;
            this.ParentWMIToolForm.GenerateEventCode();
        }

        //-------------------------------------------------------------------------
        // Changes the text on the EventQueryCondition form (used as an
        // introduction on the form).
        //-------------------------------------------------------------------------
        public void ChangeText(string newText)
        {
            this.InputMessage.Text = newText;
        }

        //-------------------------------------------------------------------------
        // Changes the value of the event query condition.
        // 
        //-------------------------------------------------------------------------
        public void ChangeTextBoxValue(string textValue)
        {
            this.TextBox.Text = textValue;
        }

        //-------------------------------------------------------------------------
        // Changes the operator used in the event query condition.
        // 
        //-------------------------------------------------------------------------
        public void ChangeOperator(string operatorValue)
        {
            this.OperatorBox.Text = operatorValue;
            this.OperatorBox.SelectedText = operatorValue;

        }

        //-------------------------------------------------------------------------
        // Gets the name of the parameter used in the event query condition.
        // 
        //-------------------------------------------------------------------------
        public string GetParameterName()
        {
            return ParameterName;
        }

        //-------------------------------------------------------------------------
        // Sets the name of the parameter in the event query condition.
        // 
        //-------------------------------------------------------------------------
        public void SetParameterName(string inputName)
        {
            this.ParameterName = inputName;
        }

        //-------------------------------------------------------------------------
        // Gets the type of the parameter used in the event query condition.
        // 
        //-------------------------------------------------------------------------
        public string GetParameterType()
        {
            string type = "";
            try
            {
                ManagementClass c = new ManagementClass(this.ParentWMIToolForm.NamespaceList_event.Text, this.ParentWMIToolForm.ClassList_event.Text, null);

                foreach (PropertyData pData in c.Properties)
                {
                    if (pData.Name.Equals(this.ParameterName))
                    {
                        type = pData.Type.ToString();
                    }
                }


                if (type.Length == 0)
                {
                    ManagementClass c2 = new ManagementClass(this.ParentWMIToolForm.NamespaceList_event.Text, this.ParentWMIToolForm.TargetClassList_event.Text, null);

                    foreach (PropertyData p in c2.Properties)
                    {
                        if (p.Name.Equals(this.ParameterName.Split(".".ToCharArray())[1]))
                        {
                            type = p.Type.ToString();
                        }
                    }
                }
            }
            catch (ManagementException e)
            {
                MessageBox.Show("Error getting the type of the event class. The namespace name or event class name is incorrect.");
            }

            return type;
        }

    }
}