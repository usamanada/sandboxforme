using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;

namespace SandBox.Winform.Biztalk.Administrator
{
    public class Helper
    {
        public static Dictionary<string, Host> GetBiztalkHosts()
        {
            Dictionary<string, Host> Hosts = new Dictionary<string,Host>();
            try
            {
                WMIMethods.GetHostSettings( Hosts);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error in retrieving HostSettings: " + ex.Message);
            }
            try
            {
                WMIMethods.GetHostInstances( Hosts);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error in retrieving HostInstances: " + ex.Message);
            }
            try
            {
                WMIMethods.GetReceiveAdapters(Hosts);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error in retrieving Receive Adapters: " + ex.Message);
            }
            try
            {
                WMIMethods.GetSendAdapters(Hosts);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error in retrieving Send Adapters: " + ex.Message);
            }
            return Hosts;
        }
        private static BtsCatalogExplorer getCatalog()
        {
            BtsCatalogExplorer catalog = new BtsCatalogExplorer();
            catalog.ConnectionString = string.Format("SERVER={0};DATABASE={1};Integrated Security=SSPI", Environment.MachineName, "BizTalkMgmtDB");

            return catalog;
        }
        public static Dictionary<string, Party> GetParties()
        {
            PartyCollection pc = getCatalog().Parties;

            Dictionary<string, Party> Parties = new Dictionary<string, Party>();

            foreach (Microsoft.BizTalk.ExplorerOM.Party p in pc)
            {
                Administrator.Party bp = new Administrator.Party();
                
                bp.Name = p.Name;
                Parties.Add(bp.Name, bp);

                foreach (Microsoft.BizTalk.ExplorerOM.PartyAlias pa in p.Aliases)
                {
                    Administrator.PartyAlias bpa = new Administrator.PartyAlias();
                    bpa.IsAutoCreated = pa.IsAutoCreated;
                    bpa.Name = pa.Name;
                    bpa.Qualifier = pa.Qualifier;
                    bpa.Value = pa.Value;

                    bp.Aliases.Add(bpa);
                }

                foreach (Microsoft.BizTalk.ExplorerOM.SendPort sp in p.SendPorts)
                {
                    Administrator.PartySendPortRef bps = new Administrator.PartySendPortRef();
                    bps.Name = sp.Name;
                    bps.URI = sp.PrimaryTransport.Address;

                    bp.SendPorts.Add(bps);
                }
                if (p.SignatureCert != null)
                {
                    bp.sc.ShortName = p.SignatureCert.ShortName;
                    bp.sc.ThumbPrint = p.SignatureCert.ThumbPrint;
                }                
            }

            return Parties;
        }
        public static void AddNewParty(Party P)
        {
            BtsCatalogExplorer catalog = getCatalog();
            try
            {
                Microsoft.BizTalk.ExplorerOM.Party RemoveParty = catalog.Parties["Test4"];
                if (RemoveParty != null)
                {
                    catalog.RemoveParty(RemoveParty);
                }
                Microsoft.BizTalk.ExplorerOM.Party p = catalog.AddNewParty();
                p.Name = "Test4";


                Microsoft.BizTalk.ExplorerOM.PartyAlias pa = p.AddNewAlias();

                pa.Name = "D-U-N-S (Dun & Bradstreet)";
                pa.Value = "Value1";

                catalog.SaveChanges();
            }
            catch (Exception e)
            {
                catalog.DiscardChanges();
                throw e;
            }

        }
        public static void HostRemove(Host host)
        {
            if (host == null)
                return;
            foreach (KeyValuePair<string, Adapter> kAdapter in host.ReceiveAdapters)
            {
                WMIMethods.DeleteReceiveAdapters(kAdapter.Value, host.Name);
            }
            foreach (KeyValuePair<string, Adapter> kAdapter in host.SendAdapters)
            {
                WMIMethods.DeleteSendAdapters(kAdapter.Value, host.Name);
            }
            foreach (KeyValuePair<string, HostInstance> kHostInstance in host.HostInstances)
            {
                WMIMethods.HostInstanceStop(kHostInstance.Value);
                WMIMethods.DeleteHostInstacne(kHostInstance.Value);
            }
            WMIMethods.DeleteHost(host);

        }
        public static void HostCreate(Host host)
        {
            WMIMethods.AddHost(host);
            foreach (KeyValuePair<string, HostInstance> kHostInstance in host.HostInstances)
            {
                HostInstance hostInstance = kHostInstance.Value;
                WMIMethods.AddHostinstance(hostInstance, host.Name);
            }
            foreach (KeyValuePair<string, Adapter> kAdapter in host.ReceiveAdapters)
            {
                WMIMethods.AddReceiveHostHandler(kAdapter.Value, host.Name);
            }
            foreach (KeyValuePair<string, Adapter> kAdapter in host.SendAdapters)
            {
                WMIMethods.AddSendHostHandler(kAdapter.Value, host.Name);
            }
        }
        public static void HostReplace(Host mergeHost)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public static void ExportHostsToServer(Dictionary<string, Host> mergeHosts, Dictionary<string, Host> serverHosts)
        {
            foreach (KeyValuePair<string, Host> kMergeHost in mergeHosts)
            {
                Host mergeHost = kMergeHost.Value;
                switch (mergeHost.Status)
                {
                    case HostStatus.New:
                        HostCreate(mergeHost);
                        break;
                    case HostStatus.Delete:
                        HostRemove(mergeHost);
                        break;
                    case HostStatus.Replace:
                        HostReplace(mergeHost);
                        break;


                }
            }
        }

        
    }
   
}
