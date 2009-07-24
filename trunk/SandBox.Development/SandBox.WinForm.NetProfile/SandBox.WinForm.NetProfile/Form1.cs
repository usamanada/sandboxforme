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

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((NetworkInterfaceType.Tunnel != nic.NetworkInterfaceType) &&
                    NetworkInterfaceType.Loopback != nic.NetworkInterfaceType)
                {
                    cbxNetworkInterface.Items.Add(nic.Name);
                }
            }

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

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkInterface[] s = NetworkInterface.GetAllNetworkInterfaces();
            
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((NetworkInterfaceType.Tunnel != nic.NetworkInterfaceType) &&
                    NetworkInterfaceType.Loopback != nic.NetworkInterfaceType)
                {
                    Console.WriteLine(nic.Name);
                    Console.WriteLine(nic.Id);
                    Console.WriteLine(nic.NetworkInterfaceType.ToString());
                    if (nic.Supports(NetworkInterfaceComponent.IPv4) || nic.Supports(NetworkInterfaceComponent.IPv6))
                    {
                        Console.WriteLine("Support IPv4 and IPv6");
                    }
                    Console.WriteLine();
                    IPInterfaceProperties properties = nic.GetIPProperties();

                    
                    UnicastIPAddressInformationCollection uniCast = properties.UnicastAddresses;
                    if (uniCast != null)
                    {

                        foreach (UnicastIPAddressInformation uni in uniCast)
                        {
                            if (uni.Address.IsIPv6LinkLocal)
                            {
                                Console.WriteLine("IPv6");
                            }
                            else
                            {
                                Console.WriteLine("IPv4");
                            }
                            Console.WriteLine("IP ......................... : {0}", uni.Address);
                            Console.WriteLine("Subnet mask ......................... : {0}", uni.IPv4Mask);
                        }
                    }

                    foreach(GatewayIPAddressInformation gipAddInfo in properties.GatewayAddresses)
                    {
                        Console.WriteLine("Default Getway ......................... : {0}", gipAddInfo.Address);
                    }
                    

                    IPAddressCollection dnsServers = properties.DnsAddresses;
                    if (dnsServers != null)
                    {
                        foreach (IPAddress dns in dnsServers)
                        {
                            Console.WriteLine("DNS Servers ............................. : {0}",
                                dns.ToString()
                           );
                        }
                    }
                }
            }


        }


        private void button2_Click(object sender, EventArgs e)
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            Console.WriteLine("Interface information for {0}.{1}     ",
                    computerProperties.HostName, computerProperties.DomainName);
            if (nics == null || nics.Length < 1)
            {
                Console.WriteLine("  No network interfaces found.");
                return;
            }

            Console.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                Console.WriteLine();
                Console.WriteLine(adapter.Description);
                Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                Console.WriteLine("  Physical Address ........................ : {0}",
                           adapter.GetPhysicalAddress().ToString());
                Console.WriteLine("  Operational status ...................... : {0}",
                    adapter.OperationalStatus);
                string versions = "";

                // Create a display string for the supported IP versions.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";
                }
                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                }
                Console.WriteLine("  IP version .............................. : {0}", versions);
                ShowIPAddresses(properties);

                // The following information is not useful for loopback adapters.
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }
                Console.WriteLine("  DNS suffix .............................. : {0}",
                    properties.DnsSuffix);

                string label;
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
                    if (ipv4 != null)
                    {
                        Console.WriteLine("  MTU...................................... : {0}", ipv4.Mtu);
                        if (ipv4.UsesWins)
                        {

                            IPAddressCollection winsServers = properties.WinsServersAddresses;
                            if (winsServers.Count > 0)
                            {
                                label = "  WINS Servers ............................ :";
                                //ShowIPAddresses(label, winsServers);
                            }
                        }
                    }
                }

                Console.WriteLine("  DNS enabled ............................. : {0}",
                    properties.IsDnsEnabled);
                Console.WriteLine("  Dynamically configured DNS .............. : {0}",
                    properties.IsDynamicDnsEnabled);
                Console.WriteLine("  Receive Only ............................ : {0}",
                    adapter.IsReceiveOnly);
                Console.WriteLine("  Multicast ............................... : {0}",
                    adapter.SupportsMulticast);
                //ShowInterfaceStatistics(adapter);

                Console.WriteLine();

            }
        }

        public static void ShowIPAddresses(IPInterfaceProperties adapterProperties)
        {
            IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
            if (dnsServers != null)
            {
                foreach (IPAddress dns in dnsServers)
                {
                    Console.WriteLine("  DNS Servers ............................. : {0}",
                        dns.ToString()
                   );
                }
            }
            IPAddressInformationCollection anyCast = adapterProperties.AnycastAddresses;
            if (anyCast != null)
            {
                foreach (IPAddressInformation any in anyCast)
                {
                    Console.WriteLine("  Anycast Address .......................... : {0} {1} {2}",
                        any.Address,
                        any.IsTransient ? "Transient" : "",
                        any.IsDnsEligible ? "DNS Eligible" : ""
                    );
                }
                Console.WriteLine();
            }

            MulticastIPAddressInformationCollection multiCast = adapterProperties.MulticastAddresses;
            if (multiCast != null)
            {
                foreach (IPAddressInformation multi in multiCast)
                {
                    Console.WriteLine("  Multicast Address ....................... : {0} {1} {2}",
                        multi.Address,
                        multi.IsTransient ? "Transient" : "",
                        multi.IsDnsEligible ? "DNS Eligible" : ""
                    );
                }
                Console.WriteLine();
            }
            UnicastIPAddressInformationCollection uniCast = adapterProperties.UnicastAddresses;
            if (uniCast != null)
            {
                string lifeTimeFormat = "dddd, MMMM dd, yyyy  hh:mm:ss tt";
                foreach (UnicastIPAddressInformation uni in uniCast)
                {
                    DateTime when;

                    Console.WriteLine("  Unicast Address ......................... : {0}", uni.Address);
                    Console.WriteLine("     Prefix Origin ........................ : {0}", uni.PrefixOrigin);
                    Console.WriteLine("     Suffix Origin ........................ : {0}", uni.SuffixOrigin);
                    Console.WriteLine("     Duplicate Address Detection .......... : {0}",
                        uni.DuplicateAddressDetectionState);

                    // Format the lifetimes as Sunday, February 16, 2003 11:33:44 PM
                    // if en-us is the current culture.

                    // Calculate the date and time at the end of the lifetimes.    
                    when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.AddressValidLifetime);
                    when = when.ToLocalTime();
                    Console.WriteLine("     Valid Life Time ...................... : {0}",
                        when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
                    );
                    when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.AddressPreferredLifetime);
                    when = when.ToLocalTime();
                    Console.WriteLine("     Preferred life time .................. : {0}",
                        when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
                    );

                    when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.DhcpLeaseLifetime);
                    when = when.ToLocalTime();
                    Console.WriteLine("     DHCP Leased Life Time ................ : {0}",
                        when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
                    );
                }
                Console.WriteLine();
            }
        }

    }
}

