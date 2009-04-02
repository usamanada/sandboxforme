using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSetUp
{
    class Constants
    {
        public const string XML_HOSTS = "Hosts";
        public const string XML_HOST = "Host";
        public const string XML_NAME = "Name";
        public const string XML_TYPE = "Type";
        public const string XML_AUTHENTICATIONTRUSTED = "AuthenticationTrusted";
        public const string XML_ALLOWHOSTTRACKING = "AllowHostTracking";
        public const string XML_THIRTYTWOBITONLY = "ThirtyTwoBitOnly";
        public const string XML_DEFUALTHOST = "DefualtHost";
        public const string XML_WINDOWSGROUP = "WindowsGroup";
        public const string XML_HOSTINSTANCES = "HostInstances";
        public const string XML_SERVER = "Server";
        public const string XML_USERNAME = "UserName";
        public const string XML_PASSWORD = "Password";
        public const string XML_PROMPT = "Prompt";
        public const string XML_ACTION = "Action";
        public const string XML_DISABLEHOSTINSTANCE = "Disable";
        public const string XML_SETADAPTERS = "SetAdapters";
        public const string XML_ADAPTER = "Adapter";

        public const string MSBTS_HOSTSETTING_NAME = "Name";
        public const string MSBTS_HOSTSETTING_HOSTTYPE = "HostType";
        public const string MSBTS_HOSTSETTING_HOSTTRACKING = "HostTracking";
        public const string MSBTS_HOSTSETTING_AUTHTRUSTED = "AuthTrusted";
        public const string MSBTS_HOSTSETTING_ISHOST32BITONLY = "IsHost32BitOnly";
        public const string MSBTS_HOSTSETTING_ISDEFAULT = "IsDefault";
        public const string MSBTS_HOSTSETTING_NTGROUPNAME = "NTGroupName";

        public const string MSBTS_HOSTINSTANCE_HOSTNAME = "HostName";
        public const string MSBTS_HOSTINSTANCE_NAME = "Name";
        public const string MSBTS_HOSTINSTANCE_MAP = "Map";
        public const string MSBTS_HOSTINSTANCE_RUNNINGSERVER = "RunningServer";
        public const string MSBTS_HOSTINSTANCE_LOGON = "Logon";
        public const string MSBTS_HOSTINSTANCE_PASSWORD = "Password";
        public const string MSBTS_HOSTINSTANCE_ISDISABLED = "IsDisabled";
        public const string MSBTS_HOSTINSTANCE_SERVERNAME = "ServerName";

        public const string MSBTS_RECEIVEHANDLER_ADAPTERNAME = "AdapterName";
        public const string MSBTS_RECEIVEHANDLER_HOSTNAME = "HostName";
        public const string MSBTS_RECEIVEHANDLER = "Receive";

        public const string MSBTS_SENDHANDLER_ADAPTERNAME = "AdapterName";
        public const string MSBTS_SENDHANDLER_HOSTNAME = "HostName";
        public const string MSBTS_SENDHANDLER = "Send";

        public const string TYPE_ISOLATED = "Isolated";
        public const string TYPE_INPROCESS = "InProcess";
        public const string TYPE_RECEIVE = "Receive";
        public const string TYPE_SEND = "Send";

        public const string FORM_ADAPTERS = "Adapters";
        public const string FORM_HOST_INSTANCES = "Host Instances";
    }
}
