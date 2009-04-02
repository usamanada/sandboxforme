using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkSetUp
{
    class WMIMethods
    {
        public static void HostStop(Host host)
        {
            InvokeMethod("MSBTS_Host", host.Name, "Stop");
        }
        public static void HostStart(Host host)
        {
            InvokeMethod("MSBTS_Host", host.Name, "Start");
        }
        private static void InvokeMethod(string className, string instanceName, string functionName)
        {
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.ReturnImmediately = false;
            //Search for all HostInstances of 'InProcess' type in the Biztalk namespace scope
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", String.Format("SELECT * FROM {0} WHERE Name='{1}'", className, instanceName), enumOptions);
            foreach (ManagementObject inst in searchObject.Get())
            {
                inst.InvokeMethod(functionName, null);
            }
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

            ////Look for the target WMI Class MSBTS_ReceiveHandler instance
            //string strWQL = "SELECT * FROM MSBTS_ReceiveHandler WHERE AdapterName = '" + adapter.Name + "'";
            //ManagementObjectSearcher searcherReceiveHandler = new ManagementObjectSearcher(new ManagementScope("root\\MicrosoftBizTalkServer"), new WqlObjectQuery(strWQL), null);

            //foreach (ManagementObject objReceiveHandler in searcherReceiveHandler.Get())
            //{
            //    objReceiveHandler.SetPropertyValue("HostName", HostName);
            //    objReceiveHandler.Put(options);
            //}

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

            ManagementClass objManagementClass = new ManagementClass("root\\MicrosoftBizTalkServer", "MSBTS_SendHandler", null);
            ManagementObject objHandler = objManagementClass.CreateInstance();

            objHandler.SetPropertyValue("AdapterName", adapter.Name);
            objHandler.SetPropertyValue("HostName", HostName);
            objHandler.Put();
        }

        public static void DeleteHost(Host host)
        {
            ManagementObject objHostSetting = new ManagementObject();
            objHostSetting.Scope = new ManagementScope("root\\MicrosoftBizTalkServer");

            //define lookup query
            string strQuery = "MSBTS_HostSetting.Name='" + host.Name + "'";
            objHostSetting.Path = new ManagementPath(strQuery);

            //delete the Managementobject
            objHostSetting.Delete();

        }
        public static void DeleteHostInstacte(HostInstance hostInstance)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public static void DeleteReceiveAdapters(Adapter adapter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public static void DeleteSendAdapters(Adapter adapter)
        {
            throw new Exception("The method or operation is not implemented.");
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
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", "Select * from MSBTS_SendHandler", enumOptions);

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

        public static List<string> GetAllSendAdapters()
        {
            return GetAllAdapters("Select * from MSBTS_SendHandler");            
        }
        public static List<string> GetAllReceiveAdapters()
        {
            return GetAllAdapters("Select * from MSBTS_ReceiveHandler");
        }
        private static List<string> GetAllAdapters(string query)
        {
            //Create EnumerationOptions and run wql query
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.ReturnImmediately = false;
            //Search for all HostInstances of 'InProcess' type in the Biztalk namespace scope
            ManagementObjectSearcher searchObject = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", query, enumOptions);

            List<string> Adapters = new List<string>();
            //Enumerate through the result set and start each HostInstance if it is already stopped
            foreach (ManagementObject inst in searchObject.Get())
            {
                string AdapterName = inst.Properties[Constants.MSBTS_SENDHANDLER_ADAPTERNAME].Value.ToString();
                if (Adapters.Contains(AdapterName) == false)
                {
                    Adapters.Add(AdapterName);
                }
                else
                {
                    throw new Exception("Duplicate Receive Adapter found: " + AdapterName);
                }
            }
            return Adapters;
        }
    }
}
