using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace SandBox.Winform.Biztalk.Administrator
{
    public enum HostStatus { New = 0, Replace = 1, Delete = 2, None = 3 };
    public enum OriginationStatus { Server = 0, File =1, New = 2};
    public class Host : ICloneable
    {
        
        #region Host Properties
        public string Name;
        public string Type;
        public bool AllowHostTracking;
        public bool AuthenticationTrusted;
        public bool ThirtyTwoBitOnly;
        public bool DefualtHost;
        public string WindowsGroup;
        public Dictionary<string, HostInstance> HostInstances;
        public Dictionary<string, Adapter> ReceiveAdapters;
        public Dictionary<string, Adapter> SendAdapters;
        #endregion

        #region Host Manage Properties
        public bool Checked;
        private HostStatus mStatus;
        public HostStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
            }
        }
        public OriginationStatus Origination;
        #endregion
        public Host()
        {
            HostInstances = new Dictionary<string, HostInstance>();
            ReceiveAdapters = new Dictionary<string, Adapter>();
            SendAdapters = new Dictionary<string, Adapter>();
        }

        public static Dictionary<string, Host> MergeCheckedHostsToSourceHosts(Dictionary<string, Host> sourceHosts, Dictionary<string, Host> hosts)
        {
            if (sourceHosts != null && hosts != null)
            {
                Dictionary<string, Host> mergeHosts = new Dictionary<string,Host>();
                
                foreach (KeyValuePair<string, Host> kSourceHost in sourceHosts)
                {
                    Host sourceHost = kSourceHost.Value;

                    if (hosts.ContainsKey(sourceHost.Name)) //Replace
                    {
                        Host host = hosts[sourceHost.Name];

                        if (host.Checked == true)
                        {
                            if (host.Status == HostStatus.None)
                            {
                                host.Status = HostStatus.Replace;
                            }

                            mergeHosts.Add(host.Name, host);
                            continue;
                        }
                    }
                    //Existing no need to change Biztalk Server Hosts
                    if (sourceHost.Origination == OriginationStatus.File)
                    {
                        sourceHost.Status = HostStatus.None;
                        mergeHosts.Add(sourceHost.Name, sourceHost);
                    }
                    
                }

                //Add New
                foreach (KeyValuePair<string, Host> kHost in hosts)
                {
                    Host host = kHost.Value;
                    if (host.Checked == true)
                    {
                        if (!mergeHosts.ContainsKey(host.Name))
                        {
                            host.Status = HostStatus.New;
                            mergeHosts.Add(host.Name, host);
                        }
                    }
                }
                return mergeHosts;
            }
            return null;
        }

        #region ICloneable Members

        public object Clone()
        {            
            Host clone = new Host();
            clone.Name = this.Name;
            clone.Type = this.Type;
            clone.AllowHostTracking = this.AllowHostTracking;
            clone.AuthenticationTrusted = this.AuthenticationTrusted;
            clone.ThirtyTwoBitOnly = this.ThirtyTwoBitOnly;
            clone.DefualtHost = this.DefualtHost;
            clone.WindowsGroup = this.WindowsGroup;

            foreach (KeyValuePair<string, HostInstance> entry in this.HostInstances)
            {
                clone.HostInstances.Add(entry.Key, (HostInstance)entry.Value.Clone());
            }

            foreach (KeyValuePair<string, Adapter> entry in this.ReceiveAdapters)
            {
                clone.ReceiveAdapters.Add(entry.Key, (Adapter)entry.Value.Clone());
            }

            foreach (KeyValuePair<string, Adapter> entry in this.SendAdapters)
            {
                clone.SendAdapters.Add(entry.Key, (Adapter)entry.Value.Clone());
            }


            clone.Checked = this.Checked;
            clone.Status = this.Status;

            return clone;
        }

        #endregion
    }
}
