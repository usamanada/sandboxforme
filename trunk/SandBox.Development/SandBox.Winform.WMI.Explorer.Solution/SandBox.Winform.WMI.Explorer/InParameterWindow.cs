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
    public partial class InParameterWindow : Form
    {
        private string StoredValue;
        private string ParameterName;
        private bool OkButtonClicked;
        private WMICodeCreator ParentWMIToolForm;
    		
        public InParameterWindow()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------------------------
        // Initializes the InParameterWindow object, creating a pointer 
        // back to the parent WMICodeCreator form.
        //-------------------------------------------------------------------------
        public InParameterWindow(WMICodeCreator parent)
        {
            InitializeComponent();
            this.ParameterName = "";
            this.StoredValue = "";
            this.OkButtonClicked = false;
            this.ParentWMIToolForm = parent;
        }
        //-------------------------------------------------------------------------
        // Handles the event when the user clicks the OK button on the
        // InParameterWindow form.
        //-------------------------------------------------------------------------
        private void OKButton_Click(object sender, System.EventArgs e)
        {
            if (this.GetParameterType().Equals("String"))
            {
                this.StoredValue = "\"" + this.textBox1.Text + "\"";
            }
            else
            {
                this.StoredValue = this.textBox1.Text;
            }

            this.Visible = false;
            this.OkButtonClicked = true;

            for (int j = 0; j < this.ParentWMIToolForm.InParameterBox.Items.Count; j++)
            {

                if (this.ParameterName.Equals(
                    this.ParentWMIToolForm.InParameterBox.Items[j].ToString().Split(" ".ToCharArray())[0]))
                {
                    string conditionName = this.ParentWMIToolForm.InParameterBox.Items[j].ToString().Split(" ".ToCharArray())[0];
                    // Updates the PropertyList_event item with the input value.
                    this.ParentWMIToolForm.InParameterBox.Items.RemoveAt(j);
                    this.ParentWMIToolForm.InParameterBox.Items.Add(conditionName + " = " + this.StoredValue);
                    this.ParentWMIToolForm.InParameterBox.Sorted = true;
                    this.ParentWMIToolForm.InParameterBox.SetSelected(j, true);
                }
            }

            this.ParentWMIToolForm.GenerateMethodCode();
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
        // Returns the type of the method in-parameter.
        // 
        //-------------------------------------------------------------------------
        public string GetParameterType()
        {
            string type = " ";

            try
            {
                ManagementClass c = new ManagementClass(this.ParentWMIToolForm.NamespaceValue_m.Text, this.ParentWMIToolForm.ClassList_m.Text, null);

                ManagementBaseObject m = c.Methods[this.ParentWMIToolForm.MethodList.Text].InParameters;
                type = m.Properties[this.ParameterName].Type.ToString();
            }
            catch (ManagementException mErr)
            {
                if (mErr.Message.Equals("Not found "))
                    MessageBox.Show("WMI class or method not found.");
                else
                    MessageBox.Show(mErr.Message.ToString());
            }

            return type;
        }

        //-------------------------------------------------------------------------
        // Handles the event when the user clicks the Cancel button on the
        // InParameterWindow form.
        //-------------------------------------------------------------------------
        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            this.StoredValue = "";
            this.textBox1.Text = "";
            this.Visible = false;
            this.OkButtonClicked = false;

            for (int j = 0; j < this.ParentWMIToolForm.InParameterBox.Items.Count; j++)
            {
                if (this.ParameterName.Equals(
                    this.ParentWMIToolForm.InParameterBox.Items[j].ToString().Split(" ".ToCharArray())[0]))
                {
                    // Change the name back to no value.
                    string conditionName = this.ParentWMIToolForm.InParameterBox.Items[j].ToString().Split(" ".ToCharArray())[0];
                    // Update the PropertyList_event item with the input value.
                    this.ParentWMIToolForm.InParameterBox.Items.RemoveAt(j);
                    this.ParentWMIToolForm.InParameterBox.Items.Add(conditionName);
                    this.ParentWMIToolForm.InParameterBox.Sorted = true;

                    this.ParentWMIToolForm.InParameterBox.SetSelected(j, false);
                }
            }

            this.ParentWMIToolForm.GenerateMethodCode();

        }

        //-------------------------------------------------------------------------
        // Handles the event when the user enters in a value for a method
        // in-parameter.
        //-------------------------------------------------------------------------
        private void TextBox_TextChanged(object sender, System.EventArgs e)
        {
            this.StoredValue = this.textBox1.Text;
            this.ParentWMIToolForm.GenerateMethodCode();
        }

        //-------------------------------------------------------------------------
        // Changes the introductory text on the
        // InParameterWindow form.
        //-------------------------------------------------------------------------
        public void ChangeText(string newText)
        {
            this.InputMessage.Text = newText;
        }

        //-------------------------------------------------------------------------
        // Returns the in-parameter value that has been entered by a user.
        //
        //-------------------------------------------------------------------------
        public string ReturnParameterValue()
        {
            return StoredValue;
        }

        //-------------------------------------------------------------------------
        // Gets the name of the method in-parameter.
        // 
        //-------------------------------------------------------------------------
        public string GetParameterName()
        {
            return ParameterName;
        }

        //-------------------------------------------------------------------------
        // Sets the name of the method in-parameter.
        // 
        //-------------------------------------------------------------------------
        public void SetParameterName(string inputName)
        {
            this.ParameterName = inputName;
        }
    }
}