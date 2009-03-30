using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace ConsoleApp_LaunchDebugWindow
{
    public class FormLauncher

    {
        public Form form = null;
        private Thread thread = null;

        public FormLauncher(Form f)
        {
            form = f;
            thread = new Thread(new ThreadStart(LaunchForm));
            thread.Start();
        }

        private void LaunchForm()
        {
            Application.Run(form);
        }

        public static FormLauncher Launch(Form form)
        {
            return new FormLauncher(form);

        }
    }
}
