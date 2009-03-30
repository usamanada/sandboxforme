using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace LaunchingSeparateApplicationDomains
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LaunchApplications();
        }
        private void LaunchApplications()
        {
            //// The following code creates an application domain with a default security policy and  with the setup info we use to specify where the application is located
            //AppDomainSetup setupInfo = new AppDomainSetup();
            //ActivationArguments aa = new ActivationArguments();

            //setupInfo.ApplicationBase = Application.StartupPath;

            //AppDomain appDomain = AppDomain.CreateDomain("BuildStringsDomain", null, setupInfo);


            
            //// The next line of code execute the assembly
            //appDomain.ExecuteAssembly("TestApplication.exe");
            //appDomain.Ex
            //// Now get rid of the appDomain.  You cannot unload individual assemblies from an application domain.  You can only unload the entire domain object.

            //AppDomain.Unload(appDomain);

            LaunchApplication(@"\TestApplication.exe", new string[]{"Contract"});
            LaunchApplication(@"\TestApplication.exe", new string[]{"NetworkProject"});
            LaunchApplication(@"\TestApplication.exe", new string[]{"ServiceProject"});


        }
        private void LaunchApplication(string ApplicationName, string[] args)
        {
            ProcessStartInfo lProcessStartInfo = new ProcessStartInfo();
            lProcessStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            lProcessStartInfo.UseShellExecute = false;
            lProcessStartInfo.RedirectStandardOutput = true;
            lProcessStartInfo.FileName = Application.StartupPath + @"\TestApplication.exe";
            Console.WriteLine(lProcessStartInfo.FileName);

            StringBuilder sb = new StringBuilder();
            foreach (string arg in args)
            {
                sb.Append(arg);
                sb.Append(" ");
            }
            lProcessStartInfo.Arguments = sb.ToString().Substring(0, sb.ToString().Length - 1);

            Process lProcess = Process.Start(lProcessStartInfo);
        }

    }
}
