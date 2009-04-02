using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Microsoft.BizTalk.ExplorerOM;

namespace SandBox.Winform.Biztalk.Administrator
{
    public class WMIMethods
    {
        public static void HostStop(Host host)
        {
            InvokeMethod("MSBTS_Host", host.Name, "Stop");
        }
        public static void HostStart(Host host)
        {
            InvokeMethod("MSBTS_Host", host.Name, "Start");
        }
        
        public static void HostInstanceStop(HostInstance hi)
        {
            InvokeMethod("MSBTS_HostInstance", hi.HostName, "Stop");            
        }
        public static void HostInstanceStart(HostInstance hi)
        {
            InvokeMethod("MSBTS_HostInstance", hi.HostName, "Start");
        }
        
        public static void AddHost(Host host)
        {
            PutOptions options = new PutOptions();
            options.Type = PutType.CreateOnly;

            // create a ManagementClass object and spawn a ManagementObject instance
            ManagementClass objHostSettingClass = new ManagementClass("root\\MicrosoftBizTalkServer", "MSBTS_HostSetting", null);
            ManagementObject objHostSetting = objHostSettingClass.CreateInstance();

            // set the properties for the Managementobject
            // Host Name
            objHostSetting["Name"] = host.Name;
            
            // Host Type
            if (host.Type == Constants.TYPE_ISOLATED)
                objHostSetting[Constants.MSBTS_HOSTSETTING_HOSTTYPE] = HostType.Isolated;
            else
                objHostSetting[Constants.MSBTS_HOSTSETTING_HOSTTYPE] = HostType.InProcess;

            objHostSetting[Constants.MSBTS_HOSTSETTING_NTGROUPNAME] = host.WindowsGroup;

            objHostSetting[Constants.MSBTS_HOSTSETTING_AUTHTRUSTED] = host.AuthenticationTrusted;

            objHostSetting[Constants.MSBTS_HOSTSETTING_HOSTTRACKING] = host.AllowHostTracking;

            objHostSetting[Constants.MSBTS_HOSTSETTING_ISHOST32BITONLY] = host.ThirtyTwoBitOnly;

            objHostSetting[Constants.MSBTS_HOSTSETTING_ISDEFAULT] = host.DefualtHost;

            //create the Managementobject
            objHostSetting.Put(options);
        }
        public static void AddHostinstance(HostInstance hostInstance, string hostName)
        {
            PutOptions options = new PutOptions();
            options.Type = PutType.CreateOnly;
            ObjectGetOptions bts_objOptions = new ObjectGetOptions();

            // Creating instance of BizTalk Host.
            ManagementClass bts_AdminObjClassServerHost = new ManagementClass("root\\MicrosoftBizTalkServer", "MSBTS_ServerHost", bts_objOptions);
            ManagementObject bts_AdminObjectServerHost = bts_AdminObjClassServerHost.CreateInstance();

            // Make sure to put correct Server Name,username and // password
            bts_AdminObjectServerHost[Constants.MSBTS_HOSTINSTANCE_SERVERNAME] = hostInstance.ServerName;
            bts_AdminObjectServerHost[Constants.MSBTS_HOSTINSTANCE_HOSTNAME] = hostName;
            bts_AdminObjectServerHost.InvokeMethod(Constants.MSBTS_HOSTINSTANCE_MAP, null);

            ManagementClass bts_AdminObjClassHostInstance = new ManagementClass("root\\MicrosoftBizTalkServer", "MSBTS_HostInstance", bts_objOptions);
            ManagementObject bts_AdminObjectHostInstance = bts_AdminObjClassHostInstance.CreateInstance();

            bts_AdminObjectHostInstance[Constants.MSBTS_HOSTINSTANCE_NAME] = "Microsoft BizTalk Server " + hostName + " " + hostInstance.ServerName;

            //Also provide correct user name and password.
            ManagementBaseObject inParams = bts_AdminObjectHostInstance.GetMethodParameters("Install");
            inParams["GrantLogOnAsService"] = false;
            inParams[Constants.MSBTS_HOSTINSTANCE_LOGON] = hostInstance.UserName;
            inParams[Constants.MSBTS_HOSTINSTANCE_PASSWORD] = hostInstance.Password;

            bts_AdminObjectHostInstance.InvokeMethod("Install", inParams, null);
            
            if(!hostInstance.Disable)
                bts_AdminObjectHostInstance.InvokeMethod("Start", null);

        }
        public static void AddReceiveHostHandler(Adapter adapter, string HostName)
        {
            PutOptions options = new PutOptions();
            options.Type = PutType.CreateOnly;

            ManagementClass objManagementClass = new ManagementClass("root\\MicrosoftBizTalkServer", "MSBTS_ReceiveHandler", null);
            ManagementObject objHandler = objManagementClass.CreateInstance();

            objHandler.SetPropertyValue("AdapterName", adapter.Name);
            objHandler.SetPropertyValue("HostName", HostName);
            objHandler.Put();
            
        }
        public static void AddSendHostHandler(Adapter adapter, string HostName)
        {
            PutOptions options = new PutOptions();
            options.Type = PutType.CreateOnly;

            ManagementClass objManagementClass = new ManagementClass("root\\MicrosoftBizTalkServer", "MSBTS_SendHandler2", null);
            ManagementObject objHandler = objManagementClass.CreateInstance();

            objHandler.SetPropertyValue("AdapterName", adapter.Name);
            objHandler.SetPropertyValue("HostName", HostName);
            objHandler.Put(options);
        }

        public static void DeleteHost(Host host)
        {
            DeleteWMIObject("MSBTS_HostSetting.Name='" + host.Name + "'");
        }
        public static void DeleteHostInstacne(HostInstance hostInstance)
        {
            InvokeMethod("MSBTS_HostInstance", "HostName", "Uninstall");
        }
        public static void DeleteReceiveAdapters(Adapter adapter, string hostName)
        {
            DeleteWMIObject(String.Format("MSBTS_ReceiveHandler.AdapterName='{0}',HostName='{1}'", adapter.Name, hostName));
        }
        public static void DeleteSendAdapters(Adapter adapter, string hostName)
        {
            DeleteWMIObject(String.Format("MSBTS_SendHandler2.AdapterName='{0}',HostName='{1}'", adapter.Name, hostName));
        }
                
        public static void GetHostSettings(Dictionary<string, Host> Hosts)
        {
            //Create EnumerationOptions and run wql query
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.ReturnImmediately = false;
            //Search for all HostInstances of 'InProcess' type in the Biztalk namespace scope
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", "Select * from MSBTS_HostInstance", enumOptions);

            searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", "Select * from MSBTS_HostSetting", enumOptions);
            
            foreach (ManagementObject inst in searchObject.Get())
            {
                if (!Hosts.ContainsKey(inst.Properties[Constants.MSBTS_HOSTSETTING_NAME].Value.ToString()))
                {
                    Host host = new Host();
                    host.Status = HostStatus.None;
                    host.Origination = OriginationStatus.Server;

                    host.Name = inst.Properties[Constants.MSBTS_HOSTSETTING_NAME].Value.ToString();
                    HostType hostType = (HostType)((uint)inst.Properties[Constants.MSBTS_HOSTSETTING_HOSTTYPE].Value);
                    if (hostType == HostType.Isolated)
                    {
                        host.Type = hostType.ToString();
                    }
                    else if (hostType == HostType.InProcess)
                    {
                        host.Type = hostType.ToString();
                    }
                    
                    host.AllowHostTracking = Convert.ToBoolean(inst.Properties[Constants.MSBTS_HOSTSETTING_HOSTTRACKING].Value);
                    host.AuthenticationTrusted = Convert.ToBoolean(inst.Properties[Constants.MSBTS_HOSTSETTING_AUTHTRUSTED].Value);
                    host.ThirtyTwoBitOnly = Convert.ToBoolean(inst.Properties[Constants.MSBTS_HOSTSETTING_ISHOST32BITONLY].Value);
                    host.DefualtHost = Convert.ToBoolean(inst.Properties[Constants.MSBTS_HOSTSETTING_ISDEFAULT].Value);
                    host.WindowsGroup = inst.Properties[Constants.MSBTS_HOSTSETTING_NTGROUPNAME].Value.ToString();                    

                    Hosts.Add(host.Name, host);
                }
            }
            
        }
        public static void GetHostInstances(Dictionary<string, Host> Hosts)
        {
            //Create EnumerationOptions and run wql query
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.ReturnImmediately = false;
            //Search for all HostInstances of 'InProcess' type in the Biztalk namespace scope
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", "Select * from MSBTS_HostInstance", enumOptions);

            //Enumerate through the result set and start each HostInstance if it is already stopped
            foreach (ManagementObject inst in searchObject.Get())
            {
                if (Hosts.ContainsKey(inst.Properties[Constants.MSBTS_HOSTINSTANCE_HOSTNAME].Value.ToString()))
                {
                    Host host = Hosts[inst.Properties[Constants.MSBTS_HOSTINSTANCE_HOSTNAME].Value.ToString()];

                    if (!host.HostInstances.ContainsKey(inst.Properties[Constants.MSBTS_HOSTINSTANCE_RUNNINGSERVER].Value.ToString()))
                    {
                        HostInstance hostInstance = new HostInstance();
                        hostInstance.ServerName = inst.Properties[Constants.MSBTS_HOSTINSTANCE_RUNNINGSERVER].Value.ToString();
                        hostInstance.HostName = host.Name;
                        hostInstance.UserName = inst.Properties[Constants.MSBTS_HOSTINSTANCE_LOGON].Value.ToString();
                        hostInstance.Password = "";
                        hostInstance.Disable = Convert.ToBoolean(inst.Properties[Constants.MSBTS_HOSTINSTANCE_ISDISABLED].Value);
                        host.HostInstances.Add(hostInstance.HostName, hostInstance);
                    }
                }
            }
        }
        public static void GetReceiveAdapters(Dictionary<string, Host> Hosts)
        {
            //Create EnumerationOptions and run wql query
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.ReturnImmediately = false;
            //Search for all HostInstances of 'InProcess' type in the Biztalk namespace scope
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", "Select * from MSBTS_ReceiveHandler", enumOptions);

            //Enumerate through the result set and start each HostInstance if it is already stopped
            foreach (ManagementObject inst in searchObject.Get())
            {                
                if (Hosts.ContainsKey(inst.Properties[Constants.MSBTS_RECEIVEHANDLER_HOSTNAME].Value.ToString()))
                {
                    Host host = Hosts[inst.Properties[Constants.MSBTS_RECEIVEHANDLER_HOSTNAME].Value.ToString()];

                    string keySearch = Adapter.BuildKey(inst.Properties[Constants.MSBTS_RECEIVEHANDLER_ADAPTERNAME].Value.ToString(), inst.Properties[Constants.MSBTS_RECEIVEHANDLER_HOSTNAME].Value.ToString(), Constants.TYPE_RECEIVE);
                    if (!host.HostInstances.ContainsKey( keySearch))
                    {
                        Adapter adapter = new Adapter();
                        adapter.Name = inst.Properties[Constants.MSBTS_RECEIVEHANDLER_ADAPTERNAME].Value.ToString();
                        adapter.Type = Constants.TYPE_RECEIVE;
                        host.ReceiveAdapters.Add(keySearch, adapter);
                    }
                    else
                    {
                        throw new Exception("Duplicate Receive Adapters details found: " + keySearch);
                    }
                }                
            }
        }
        public static void GetSendAdapters(Dictionary<string, Host> Hosts)
        {
            //Create EnumerationOptions and run wql query
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.ReturnImmediately = false;
            //Search for all HostInstances of 'InProcess' type in the Biztalk namespace scope
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", "Select * from MSBTS_SendHandler2", enumOptions);

            //Enumerate through the result set and start each HostInstance if it is already stopped
            foreach (ManagementObject inst in searchObject.Get())
            {
                if (Hosts.ContainsKey(inst.Properties[Constants.MSBTS_SENDHANDLER_HOSTNAME].Value.ToString()))
                {
                    Host host = Hosts[inst.Properties[Constants.MSBTS_SENDHANDLER_HOSTNAME].Value.ToString()];

                    string keySearch = Adapter.BuildKey(inst.Properties[Constants.MSBTS_SENDHANDLER_ADAPTERNAME].Value.ToString(), inst.Properties[Constants.MSBTS_SENDHANDLER_HOSTNAME].Value.ToString(), Constants.TYPE_SEND);
                    if (!host.HostInstances.ContainsKey(keySearch))
                    {
                        Adapter adapter = new Adapter();
                        adapter.Name = inst.Properties[Constants.MSBTS_SENDHANDLER_ADAPTERNAME].Value.ToString();
                        adapter.Type = Constants.TYPE_SEND;
                        host.SendAdapters.Add(keySearch, adapter);
                    }
                    else
                    {
                        throw new Exception("Duplicate Send Adapters details found: " + keySearch);
                    }
                }   
            }
        }

        public static Dictionary<string, AdapterSetting> GetAdapterSettings()
        {
            //Create EnumerationOptions and run wql query
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.ReturnImmediately = false;
            //Search for all HostInstances of 'InProcess' type in the Biztalk namespace scope
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", "Select * from MSBTS_AdapterSetting", enumOptions);

            Dictionary<string, AdapterSetting> AdapterSettings = new Dictionary<string, AdapterSetting>();
            //Enumerate through the result set and start each HostInstance if it is already stopped
            foreach (ManagementObject inst in searchObject.Get())
            {
                string AdapterName = inst.Properties[Constants.MSBTS_ADAPTERSETTING_NAME].Value.ToString();
                if (!AdapterSettings.ContainsKey(AdapterName))
                {
                    AdapterSetting aSettings = new AdapterSetting();
                    aSettings.Name = AdapterName;
                    aSettings.Constraints = (uint)inst.Properties[Constants.MSBTS_ADAPTERSETTING_CONSTRAINTS].Value;
                    AdapterSettings.Add(AdapterName, aSettings);
                }
            }
            return AdapterSettings;
        }
        private static void InvokeMethod(string className, string propertyName, string functionName)
        {
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.ReturnImmediately = false;
            //Search for all HostInstances of 'InProcess' type in the Biztalk namespace scope
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", String.Format("SELECT * FROM {0} WHERE HostName='{1}'", className, propertyName), enumOptions);
            foreach (ManagementObject inst in searchObject.Get())
            {
                inst.InvokeMethod(functionName, null);
            }
        }
        private static void DeleteWMIObject(string query)
        {
            ManagementObject objHostSetting = new ManagementObject();
            objHostSetting.Scope = new ManagementScope("root\\MicrosoftBizTalkServer");

            objHostSetting.Path = new ManagementPath(query);

            objHostSetting.Delete();
        }
    }
}
