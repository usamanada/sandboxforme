using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TestDataBaseConnectivity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionStringTxt.Text);
                connection.Open();
                connection.Close();
                MessageBox.Show("Connection successful");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtExample.Text =
                "Sql Login" + Environment.NewLine +                
                "Data Source=[ServerName|\\Instance];Initial Catalog=[Databasename];User ID=[Username];Password=[Password]" + Environment.NewLine +
                "Integrated" + Environment.NewLine +                 
                "Data Source=[ServerName];Initial Catalog=[Database];Trusted_Connection=True;";
        }
    }
}