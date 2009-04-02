using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SandBox.Winform.WMI.Explorer
{
    public partial class SplashScreenForm : Form
    {
        static SplashScreenForm sSForm;
        static Thread splashScreenThread;
        private double opacityIncrease = .05;
        private double opacityDecrease = .1;
        private const int TIMER_INTERVAL = 50;
        private string introText;

        public SplashScreenForm()
        {
            sSForm = null;
            splashScreenThread = null;
            InitializeComponent();

            this.Opacity = .5;
            timer1.Interval = TIMER_INTERVAL;
            timer1.Start();
            introText = "Initializing the WMI Code Creator. Loading WMI classes...";
            progressBar1.Maximum = 41;
            this.ShowInTaskbar = false;
        }
        //-------------------------------------------------------------------------
        // A static entry point to launch the splash screen.
        //
        //-------------------------------------------------------------------------
        static private void ShowForm()
        {
            sSForm = new SplashScreenForm();
            Application.Run(sSForm);
        }

        //-------------------------------------------------------------------------
        // A static entry point to close the splash screen.
        //
        //-------------------------------------------------------------------------
        static public void CloseForm()
        {
            if (sSForm != null)
            {
                // Start to close.
                sSForm.opacityIncrease = -sSForm.opacityDecrease;
            }
            sSForm = null;
            splashScreenThread = null;  // Not necessary at this point.
        }

        //-------------------------------------------------------------------------
        // A static method that shows the splash screen.
        //
        //-------------------------------------------------------------------------
        static public void ShowSplashScreen()
        {
            // Only launch once.
            if (sSForm != null)
                return;
            splashScreenThread = new Thread(new ThreadStart(SplashScreenForm.ShowForm));
            splashScreenThread.IsBackground = true;
            splashScreenThread.ApartmentState = ApartmentState.STA;
            splashScreenThread.Start();
        }

        //-------------------------------------------------------------------------
        // A static method to set the status of the splash screen.
        //
        //-------------------------------------------------------------------------
        static public void SetStatus(string newStatus)
        {
            if (sSForm == null)
                return;
            sSForm.introText = newStatus;
        }

        //-------------------------------------------------------------------------
        // A static entry point to launch SplashScreen.
        //
        //-------------------------------------------------------------------------
        private void timer1_Tick_1(object sender, System.EventArgs e)
        {
            if (opacityIncrease > 0.0)
            {
                if (this.Opacity < 1)
                    this.Opacity += opacityIncrease;
            }
            else
            {
                if (this.Opacity > 0.0)
                    this.Opacity += opacityIncrease;
                else
                    this.timer1.Stop();
            }

        }

        static public void IncrementProgress()
        {
            progressBar1.Increment(1);
        }

        static public void SetProgressMax(int max)

        {
            if (progressBar1 != null && progressBar1.Maximum != null)
            {
                progressBar1.Maximum = max;
            }
        }
    }
}