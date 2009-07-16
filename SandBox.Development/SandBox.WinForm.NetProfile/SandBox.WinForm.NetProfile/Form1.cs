using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;

namespace SandBox.WinForm.NetProfile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.rbpDNS.AddCheckEventListeners();
            this.rbpIP.AddCheckEventListeners();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ipAddressControl8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strHostName = "";
            IPHostEntry ipEntry = System.Net.Dns.GetHostByName(strHostName);
 
            IPAddress[] addr = ipEntry.AddressList;
            

            for (int i = 0; i < addr.Length; i++)
            {
                Console.WriteLine("IP Address {0}: {1} ", i, addr[i].ToString());
            }
            //System.Net.NetworkInformation.IPv4InterfaceProperties
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
            
                Console.WriteLine(nic.Name);
                Console.WriteLine(nic.NetworkInterfaceType.ToString());   
            }
            

        }
    }
}
