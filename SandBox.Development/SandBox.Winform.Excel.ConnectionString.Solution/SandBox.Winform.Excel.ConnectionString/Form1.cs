using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SandBox.Winform.Excel.ConnectionString
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel 2007 (*.xslx)| *.xslx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = ofd.FileName;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(String.Format("Data Source={0};Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;", txtFile.Text));
            conn.Open();
            conn.Close();

            OleDbDataAdapter da = new OleDbDataAdapter("Select * from [Sheet1$]", conn);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dgvExcel.DataSource = dt;
        }
    }
}