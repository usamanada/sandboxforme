using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace ExcelUpload
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ConnectionString;
        List<SheetDetails> lSheets = new List<SheetDetails>();

        public MainWindow()
        {
            InitializeComponent();

            cbxSheets.DisplayMemberPath = "Name";
            cbxSheets.SelectedValuePath = "SheetName";
            cbxSheets.ItemsSource = lSheets;
        }


        private void btnExcelOpen_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            OpenFileDialog fld = new OpenFileDialog();
            fld.Filter = "Excel|*.xls;*.xlsx";

            // Show open file dialog box 
            Nullable<bool> result = fld.ShowDialog(); 

            // Process open file dialog box results 
            if (result == true) 
            { 
                txtExcelFile.Text = fld.FileName;

                if (System.IO.Path.GetExtension(txtExcelFile.Text).Equals(".xls"))//for 97-03 Excel file
                {
                    ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\";", txtExcelFile.Text);
                }
                else if (System.IO.Path.GetExtension(txtExcelFile.Text).Equals(".xlsx"))  //for 2007 Excel file
                {
                    ConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;\";", txtExcelFile.Text);
                }

                GetExcelSheetNames(ConnectionString);
            }
        }

        private void ClearData()
        {
            ConnectionString = "";
            lSheets.Clear();
        }

        private void GetExcelSheetNames(string ExcelConnStr)
        {
            System.Data.OleDb.OleDbConnection OledbConnection = new System.Data.OleDb.OleDbConnection();
            OledbConnection.ConnectionString = ExcelConnStr;

            try
            {
                OledbConnection.Open();
                DataTable dtSheets = OledbConnection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);

                foreach (DataRow drSheet in dtSheets.Rows)
                {
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    {
                        lSheets.Add(new SheetDetails(drSheet["TABLE_NAME"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                OledbConnection.Close();
            }

        }
        private DataTable ReadExcelSheet(string ExcelConnStr, string SheetName)
        {
            System.Data.OleDb.OleDbConnection OledbConnection = new System.Data.OleDb.OleDbConnection();
            OledbConnection.ConnectionString = ExcelConnStr;
            System.Data.OleDb.OleDbDataAdapter OAdapter = new System.Data.OleDb.OleDbDataAdapter(String.Format("SELECT * FROM [{0}]", SheetName), OledbConnection);

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

            if (dsExcel.Tables.Count > 0)
            {
                return dsExcel.Tables[0].Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
            }
            else
            {
                return null;
            }
        }

        private void cbxSheets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0].GetType().FullName == Constants.MDSADMINISTRATION_SHEETDETAILS)
            {
                var sd = (SheetDetails)e.AddedItems[0];
                DataTable dt = ReadExcelSheet(ConnectionString, sd.SheetName);
                dgExcelSheet.ItemsSource = dt.DefaultView;
            }
            
        }
    }
}
