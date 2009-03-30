using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace ThreadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           try
           {
               // First get the user data (as numerical).
               int numbOne = 5;
               int numbTwo = 10;
               AddParams args = new AddParams(numbOne, numbTwo);
               // Now spin up the new thread and pass args variable.
               ProcessNumbersBackgroundWorker.WorkerReportsProgress = true;
               ProcessNumbersBackgroundWorker.WorkerSupportsCancellation = true;
               ProcessNumbersBackgroundWorker.RunWorkerAsync(args);
               
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
        }

        private void ProcessNumbersBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            AddParams args = (AddParams)e.Argument;
            ProcessNumbersBackgroundWorker.ReportProgress(0, "324234234234");
            // Artificial lag.
            System.Threading.Thread.Sleep(5000);
            // Return the value.
            e.Result = args.a + args.b;
        }

        private void ProcessNumbersBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(e.Result.ToString(), "Your result is");
        }

        private void ProcessNumbersBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.UserState);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessNumbersBackgroundWorker.CancelAsync();
        }
    }
    class AddParams
    {
        public int a, b;
        public AddParams(int numb1, int numb2)
        {
            a = numb1;
            b = numb2;
        }
    }
}