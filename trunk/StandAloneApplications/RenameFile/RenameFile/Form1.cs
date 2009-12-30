using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace RenameFile
{
    public partial class Form1 : Form
    {
        private const string mszOriginalPath = "Original Path";
        private const string mszOriginalFileName = "Original FileName";
        private const string mszModifiedFileName = "Modified FileName";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (Directory.Exists(txtDir.Text))
            {
                fbd.SelectedPath = txtDir.Text;
            }
            
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = fbd.SelectedPath;
            }
        }

        private string[] getFiles(string szDirectory)
        {
            if (!Directory.Exists(szDirectory))
            {
                MessageBox.Show("Directory not found");
                return null;
            }
            return Directory.GetFiles(szDirectory);
        }
        private string[] getFiles(string szDirectory, string szSearchPattern)
        {
            if (!Directory.Exists(szDirectory))
            {
                MessageBox.Show("Directory not found");
                return null;
            }
            return Directory.GetFiles(szDirectory, szSearchPattern);
        }
        private void btnBizTalkXMLGo_Click(object sender, EventArgs e)
        {
            string[] Files = getFiles(txtDir.Text);

            foreach (string file in Files)
            {
                if (chkBiztalkXmlFormat.Checked == true)
                {
                    XmlTextReader reader = new XmlTextReader(file);
                    reader.MoveToContent();
                    Encoding EncodeFileType = reader.Encoding;
                    
                    string TempXML = Guid.NewGuid().ToString() + ".xml";
                    XmlTextWriter writer = new XmlTextWriter(TempXML, EncodeFileType);
                    writer.Formatting = Formatting.Indented;
                    writer.WriteNode(reader, true);
                    writer.Close();
                    reader.Close();

                    File.Move(file, file + ".bak");
                    File.Move(TempXML, file);
                    File.Delete(file + ".bak");
                    File.Delete(TempXML);
                }
                if (chkBiztalkXMLRename.Checked == true)
                {
                    XmlDocument oDoc = new XmlDocument();
                    oDoc.Load(file);

                    string MessageType = oDoc.DocumentElement.LocalName;
                    FileInfo fi = new FileInfo(file);
                    string ReplaceFile = fi.Directory + @"\" + MessageType + fi.Name;
                    if (!File.Exists(ReplaceFile))
                    {
                        File.Move(file, ReplaceFile);
                    }
                }
            }
        }




        private void btnFRPreview_Click(object sender, EventArgs e)
        {
            dgvFRDetails.Rows.Clear();
            string[] Files = getFiles(txtDir.Text, txtFRFileFilter.Text);
            
            string filename;
            int iCount = 0;

            if (!String.IsNullOrEmpty(txtFRStartingCount.Text))
            {
                iCount = Convert.ToInt32(txtFRStartingCount.Text);
            }
            else
            {
                txtFRStartingCount.Text = "0";
            }

            foreach (string path in Files)
            {                
                filename = System.IO.Path.GetFileName(path);
                string modifiedFilename = "";

                if (rbtFRSimple.Checked)
                {
                    modifiedFilename = String.Format(tbxFRFileFormat.Text, iCount++);
                }
                else
                {
                    Match m = Regex.Match(filename, txtFRRegExpression.Text);
                    if(m.Success)
                    {
                        modifiedFilename = m.Result("${Filename}${Extension}");
                    }
                    else
                    {
                        modifiedFilename = "no match";
                    }
                }
                string[] row = { path, filename, modifiedFilename  };
                dgvFRDetails.Rows.Add(row);
            }
        }
        private void OutputResult(MatchCollection matches)
        {
            Console.Clear();
            try
            {
                if (matches.Count == 0)
                {
                    Console.WriteLine("No match found.");
                    return;
                }

                foreach (Match m in matches)
                {
                    if (m.Success && m.Groups.Count > 0)
                    {
                        Console.WriteLine("Count: " + m.Groups.Count);
                        foreach (Group g in m.Groups)
                        {
                            Console.WriteLine(g.ToString());
                        }

                    }
                    else
                    {
                        Console.WriteLine("No match found.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnFRModify_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow vr in dgvFRDetails.Rows)
            {
                FileInfo fi = new FileInfo(vr.Cells["dgvTxtColFROriginalPath"].Value.ToString());
                string modifiedFileName = vr.Cells["dgvTxtColModifiedFileName"].Value.ToString();
                string modfiedPath = fi.DirectoryName + "\\" + modifiedFileName;
                if (!File.Exists(modfiedPath))
                {
                    fi.MoveTo(modfiedPath);
                }
                else
                {
                    MessageBox.Show(String.Format("File exists when moving from {0} to {1}", vr.Cells["dgvTxtColFROriginalFileName"].Value.ToString(), modifiedFileName));
                    return;
                }                
            }           
            
            MessageBox.Show("Files Renamed");
        }

        private void btnFileModfiyRefresh_Click(object sender, EventArgs e)
        {
            string[] Files = getFiles(txtDir.Text, txtFRFileFilter.Text);
            lbxFileModify.Items.Clear();

            foreach (string path in Files)
            {
                string filename = System.IO.Path.GetFileName(path);
                lbxFileModify.Items.Add(filename);
            }
        }

        private void lbxFileModify_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filePath = txtDir.Text + "\\" + lbxFileModify.SelectedItem.ToString();
            if (File.Exists(filePath))
            {
                FileAttributes fa =  File.GetAttributes(filePath);
             
            }
            else
            {
                MessageBox.Show("File not found: " + filePath);
            }
        }

        private void rbtFRSimple_CheckedChanged(object sender, EventArgs e)
        {
            tbxFRFileFormat.Enabled = rbtFRSimple.Checked;
            txtFRStartingCount.Enabled = rbtFRSimple.Checked;
            txtFRRegExpression.Enabled =! rbtFRSimple.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rbtFRSimple.Checked = true;
        }

        private void btnREFindMatch_Click(object sender, EventArgs e)
        {
            try
            {
                Regex linkRegex = new Regex(tbxREExpression.Text);
                MatchCollection matches = linkRegex.Matches(rtbtbxREData.Text);
                OutputResult(matches);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.Clear();
        }

        private void btnPathsResult_Click(object sender, EventArgs e)
        {
            txtPathsResult.Text = FileHelper.RelativePath(tbxPathsAbsolutePath.Text, tbxPathsRelativeTo.Text);
        }

        private void btnPathsAbsolutePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (Directory.Exists(tbxPathsAbsolutePath.Text))
            {
                fbd.SelectedPath = tbxPathsAbsolutePath.Text;
            }

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbxPathsAbsolutePath.Text = fbd.SelectedPath;
            }
        }

        private void btnPathsRelativeTo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (Directory.Exists(tbxPathsRelativeTo.Text))
            {
                fbd.SelectedPath = tbxPathsRelativeTo.Text;
            }

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbxPathsRelativeTo.Text = fbd.SelectedPath;
            }
        }
    }
}
