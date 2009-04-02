using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using Sandbox.dll.CustomConfigHandler;
using Microsoft.Win32;

namespace Sandbox.WinService.Registry
{
    public partial class Registry : ServiceBase
    {
        private System.Timers.Timer serviceTimer;
        public Registry()
        {
            InitializeComponent();
            serviceTimer = new System.Timers.Timer();
            serviceTimer.Interval = Convert.ToDouble( ConfigurationManager.AppSettings["TimerInterval"]);

            serviceTimer.Elapsed += new System.Timers.ElapsedEventHandler(serviceTimer_Elapsed);
            serviceTimer.Enabled = true;
        }

        void serviceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<ConfigRegistry> ConfigRegistrys = (List<ConfigRegistry>)ConfigurationManager.GetSection("Registrys/Registry");
            foreach (ConfigRegistry cr in ConfigRegistrys)
            {
                writeRegValue(cr.RegKey, cr.ValueName, cr.Value, cr.Type);
            }
        }

        private void writeRegValue(string regKey, string valueName, string value, string type)
        {
            switch (type)
            {
                case "REG_DWORD":
                    Microsoft.Win32.Registry.SetValue(regKey, valueName, Convert.ToInt32(value));
                    break;
                case "REG_SZ":
                    Microsoft.Win32.Registry.SetValue(regKey, valueName, value);
                    break;
                default:
                    break;
            }
        }

        protected override void OnStart(string[] args)
        {
            serviceTimer.Start();
        }

        protected override void OnStop()
        {
            serviceTimer.Stop();
        }
    }
}
