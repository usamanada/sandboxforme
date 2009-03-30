using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

namespace Graysonline.Winform
{
    public partial class Form1 : Form
    {
        private string findlink = "HREF=\\\"([^\\\"]*)\\\".*\\b{0}\\b";
        private List<UrlPage> ItemDescription = new List<UrlPage>();
        private List<string> Urls = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            rtbComputerResult.Text = DownloadPage(txtUrl.Text);
            try
            {
                Regex linkRegex = new Regex(string.Format(findlink, HttpUtility.HtmlEncode(tbxSearchPage.Text)));

                MatchCollection matches = linkRegex.Matches(rtbComputerResult.Text);

                richTextBox1.Text = "";
                Urls.Clear();

                foreach (Match m in matches)
                {
                    if (m.Success && m.Groups.Count > 0)
                    {
                        richTextBox1.Text += "Count: " + m.Groups.Count + Environment.NewLine;
                        foreach (Group g in m.Groups)
                        {
                            richTextBox1.Text += g.ToString() + Environment.NewLine;
                        }
                        Urls.Add(m.Groups[1].ToString());
                    }
                    else
                    {
                        richTextBox1.Text = "No match found.";
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (Urls.Count > 0)
            {
                rtbUrl.Text += Urls[0];
                rtbComputerResult.Text = DownloadPage(Urls[0]);
                SearchComputerIT(rtbComputerResult.Text);
            }            

        }

        private void SearchComputerIT(string page)
        {
            List<string> items = find(page, "(document.location.href=\\'\\/)(s.*\\d)\\'", 2 );

            foreach (string url in items)
            {
                rtbUrl.Text += url + Environment.NewLine;
                SearchComputerITItem(txtUrl.Text + "/" + url);
            }
        }

        private List<string> find(string page, string find, int match)
        {
            Console.WriteLine(find);
            List<string> items = new List<string>();
            try
            {
                Regex linkRegex = new Regex(find);
                MatchCollection matches = linkRegex.Matches(page);


                items.Clear();

                foreach (Match m in matches)
                {
                    if (m.Success && m.Groups.Count > 0)
                    {
                        Console.WriteLine("Count: " + m.Groups.Count + Environment.NewLine);

                        foreach (Group g in m.Groups)
                        {
                            Console.WriteLine(g.ToString());
                        }
                        items.Add(m.Groups[match].ToString());
                    }
                    else
                    {
                        Console.WriteLine( "No match found.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return items;
        }

        private void SearchComputerITItem(string url)
        {
            string page = DownloadPage(url);
            List<string> items = find(page, "(href\\=\\\"/)(.*THUMB)(.*View product catalog and start bidding)", 2 );
            if(items.Count > 0 )
            {
                string itemUrl = HttpUtility.HtmlDecode(txtUrl.Text + "/" + items[0]);
                rtbUrl.Text += itemUrl + Environment.NewLine;
                SearchItems(txtUrl.Text + "/" + url);   
            }
        }

        private void SearchItems(string url)
        {
            string page = DownloadPage(url);
            UrlPage up = new UrlPage();
            up.page = page;
            up.url = url;
//            Urls.Add(up);
        }


        private string DownloadPage(string url)
        {
            WebClient client = new WebClient();
            byte[] bytedata = client.DownloadData(url);
            string szDownload = "";
            for (int index = 0; index < bytedata.Length; index++)
            {
                szDownload += (char)bytedata[index];
            }
            return szDownload;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                Regex linkRegex = new Regex(txtTestSearch.Text);

                List<String> links = new List<String>();
                MatchCollection matches = linkRegex.Matches(txtTest.Text);

                rtbTest.Text = "";
                Urls.Clear();

                if (matches.Count == 0)
                {
                    rtbTest.Text = "No match found.";
                    return;
                }

                foreach (Match m in matches)
                {
                    if (m.Success && m.Groups.Count > 0)
                    {
                        rtbTest.Text += "Count: " + m.Groups.Count + Environment.NewLine;
                        foreach (Group g in m.Groups)
                        {
                            rtbTest.Text += g.ToString() + Environment.NewLine;
                        }
                    }
                    else
                    {
                        rtbTest.Text = "No match found.";
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchComputerITItem(textBox1.Text);
        }
    }
}
