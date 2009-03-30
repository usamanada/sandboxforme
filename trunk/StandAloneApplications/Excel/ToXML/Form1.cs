using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace ToXML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fld = new OpenFileDialog();
            fld.Filter = "Excel|*.xls;*.xlsx";
            if (fld.ShowDialog() == DialogResult.OK)
            {
                txtOpen.Text = fld.FileName;
                txtConnection.Text = String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;""", txtOpen.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dsProjects = new DataSet();
            dsProjects.ReadXmlSchema("Account.xsd");
            
            StringBuilder AccountFields = new StringBuilder();
            foreach (DataColumn dc in dsProjects.Tables["account"].Columns)
            {
                if (dc.ColumnMapping != MappingType.Hidden)
                {
                    AccountFields.Append(" " + dc.ColumnName + ",");
                }
            }
            AccountFields = AccountFields.Remove(AccountFields.Length - 1, 1);
            
            StringBuilder ContactFields = new StringBuilder();
            foreach (DataColumn dc in dsProjects.Tables["Contact"].Columns)
            {
                if (dc.ColumnMapping != MappingType.Hidden)
                {
                    ContactFields.Append(" " + dc.ColumnName + ",");
                }
            }

            ContactFields = ContactFields.Remove(ContactFields.Length - 1, 1);

            System.Data.OleDb.OleDbConnection OledbConnection = new System.Data.OleDb.OleDbConnection();
            OledbConnection.ConnectionString = txtConnection.Text;
            System.Data.OleDb.OleDbDataAdapter OAdapter = new System.Data.OleDb.OleDbDataAdapter(String.Format("SELECT * FROM {0}", "[bms2394$]"), OledbConnection);
            
            DataSet dsExcel = new DataSet();
            try
            {
                OledbConnection.Open();
                OAdapter.Fill(dsExcel);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                OledbConnection.Close();
            }
            
            var distinctDuns = (from row in dsExcel.Tables[0].AsEnumerable() select row.Field<string>("duns")).Distinct();

            Console.WriteLine(distinctDuns.Count());
            foreach (var duns in distinctDuns)
            {
                Console.WriteLine(duns.ToString());
                var Account = (from row in dsExcel.Tables[0].AsEnumerable() where row.Field<string>("duns") == duns.ToString() select row).Distinct();
                if (Account.Count() > 0)
                {
                    DataRow dr = Account.First();
                    foreach (DataColumn dc in dsProjects.Tables["account"].Columns)
                    {
                        if (dc.ColumnMapping != MappingType.Hidden)
                        {
                            Console.WriteLine(String.Format("Account {0}: {1}", dc.ColumnName, dr[dc.ColumnName].ToString()));
                        }
                    }
                }
                var Contacts = (from row in dsExcel.Tables[0].AsEnumerable() where row.Field<string>("duns") == duns.ToString() select row);
                if (Account.Count() > 0)
                {
                    if (Contacts.Count() > 0)
                    {
                        foreach (DataRow dr in Contacts)
                        {
                            foreach (DataColumn dc in dsProjects.Tables["Contact"].Columns)
                            {
                                if (dc.ColumnMapping != MappingType.Hidden)
                                {
                                    Console.WriteLine(String.Format("\tContact {0}: {1}", dc.ColumnName, dr[dc.ColumnName].ToString()));
                                }
                            }
                        }
                    }
                }
            }            
            
            
            //DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            //using (DbConnection connection = factory.CreateConnection())
            //{                
            //    connection.ConnectionString = txtConnection.Text;
            //    using (DbCommand command = connection.CreateCommand())
            //    {
            //        command.CommandText = String.Format("SELECT{0} FROM {1}", AccountFields.ToString(), "[bms2394$]");
            //        connection.Open();
            //        using (DbDataReader dr = command.ExecuteReader())
            //        {
            //            int count = 0;
            //            while (dr.Read())
            //            {
            //                Console.WriteLine(dr["duns"].ToString());
            //                count++;
            //            }
            //            Console.WriteLine(String.Format("Total Count: {0}", count));
            //        }
            //    }
            //}
        }
    }
}
