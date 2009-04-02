using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.Winform.Biztalk.Administrator
{
    public class Party
    {
        public string Name;
        public List<PartyAlias> Aliases;
        public List<PartySendPortRef> SendPorts;
        public SignatureCert sc;
        public Party()
        {
            sc = new SignatureCert();
            Aliases = new List<PartyAlias>();
            SendPorts = new List<PartySendPortRef>();
        }
    }
}
