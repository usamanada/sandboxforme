using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ConsoleApp_LaunchDebugWindow
{
    class Program
    {
        private delegate void AppendTextHandler(string s);
        static FrmDebug fb;
        static void Main(string[] args)
        {
            Console.WriteLine("Hit Key to start Debug window");
            Console.ReadKey();
            fb = new FrmDebug();
            FormLauncher fl = FormLauncher.Launch(fb);

            AppendText("Test");

            Console.WriteLine("Form Started");
            Console.ReadKey();
        }
        static void AppendText(string s)
        {
            if (fb.rtbTest.InvokeRequired)
                fb.rtbTest.Invoke(new AppendTextHandler(AppendText), new object[] { s });
            else
                fb.rtbTest.Text += s;
        }
    }
}
