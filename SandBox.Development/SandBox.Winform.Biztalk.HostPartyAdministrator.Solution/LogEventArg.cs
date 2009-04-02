using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSetUp
{
    class LogEventArg : EventArgs
    {
        public string Message;
        public string HostName;
        public string HostMessage;
    }
}
