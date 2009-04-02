using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
namespace SandBox.Winform.Biztalk.Administrator
{
    public delegate void LogEventHandler(object sender, LogEventArg e);
    public class XmlHelper
    {
        #region OnLogMessage
        /// <summary>
        /// Triggers the LogMessage event.
        /// </summary>
        /// 
        public void Log(string message)
        {
            if (LogMessage != null)
            {
                LogEventArg ea = new LogEventArg();
                ea.Message = message;
                LogMessage(this, ea);
            }
        }

        public void Log(string message, string HostName, string HostMessage)
        {
            if (LogMessage != null)
            {
                LogEventArg ea = new LogEventArg();
                ea.Message = message;
                ea.HostName = HostName;
                ea.HostMessage = HostMessage;
                LogMessage(this, ea);
            }
        }
        public event LogEventHandler LogMessage;
        #endregion
        #region Export To File
        

        public void ExportHostsToFile(Dictionary<string, Host> Hosts, string file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("Invalid File Passed");
            }

            Log("Valid File found: " + file);

            XmlDocument doc = new XmlDocument();
            XPathNavigator navigator = doc.CreateNavigator();
            XmlWriter writer = navigator.AppendChild();
            
            writer.WriteStartElement(Constants.XML_HOSTS);
                
            foreach (KeyValuePair<string, Host> kHost in Hosts)
            {
                Host host = kHost.Value;
                AddHostNode(writer, host);
            }

            writer.WriteEndElement();
            writer.Close();
            doc.Save(file);
        }
        private void AddHostNode(XmlWriter writer, Host host)
        {
            Log("Adding Host: " + host.Name, host.Name, "Starting");
            writer.WriteStartElement(Constants.XML_HOST);
            writer.WriteAttributeString(Constants.XML_NAME, host.Name);
            writer.WriteAttributeString(Constants.XML_TYPE, host.Type);

            writer.WriteStartElement(Constants.XML_ALLOWHOSTTRACKING);
            writer.WriteValue(host.AllowHostTracking);
            writer.WriteEndElement();

            writer.WriteStartElement(Constants.XML_AUTHENTICATIONTRUSTED);
            writer.WriteValue(host.AuthenticationTrusted);
            writer.WriteEndElement();

            writer.WriteStartElement(Constants.XML_THIRTYTWOBITONLY);
            writer.WriteValue(host.ThirtyTwoBitOnly);
            writer.WriteEndElement();

            writer.WriteStartElement(Constants.XML_DEFUALTHOST);
            writer.WriteValue(host.DefualtHost);
            writer.WriteEndElement();

            writer.WriteStartElement(Constants.XML_WINDOWSGROUP);
            writer.WriteValue(host.WindowsGroup);
            writer.WriteEndElement();

            AddHostInstances(writer, host);
            AddAdapters(writer, host);
            writer.WriteEndElement();
            Log("Added Host: " + host.Name, host.Name, "Completed");
        }
        private void AddHostInstances(XmlWriter writer, Host host)
        {
            if (host.HostInstances.Count > 0)
            {
                foreach (KeyValuePair<string, HostInstance> kHostInstance in host.HostInstances)
                {
                    Log("Adding Hostinstance: " + host.Name);
                    HostInstance hostInstance = kHostInstance.Value;

                    writer.WriteStartElement(Constants.XML_SERVER);
                    writer.WriteAttributeString(Constants.XML_NAME, hostInstance.ServerName);
                    writer.WriteAttributeString(Constants.XML_DISABLEHOSTINSTANCE, hostInstance.Disable.ToString());

                    writer.WriteStartElement(Constants.XML_USERNAME);
                    writer.WriteValue(hostInstance.UserName);
                    writer.WriteEndElement();

                    writer.WriteStartElement(Constants.XML_PASSWORD);
                    writer.WriteValue(hostInstance.Password);                    
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    Log("Added Hostinstance: " + host.Name);
                }
                writer.WriteEndElement();
            }
            
        }
        private void AddAdapters(XmlWriter writer, Host host)
        {
            if (host.SendAdapters.Count > 0 || host.ReceiveAdapters.Count > 0)
            {
                AddAdapters(writer, host.ReceiveAdapters);
                AddAdapters(writer, host.SendAdapters);
                
                writer.WriteEndElement();
            }
        }
        private void AddAdapters(XmlWriter writer, Dictionary<string, Adapter> Adapters)
        {
            foreach (KeyValuePair<string, Adapter> kAdapter in Adapters)
            {
                Adapter adapter = kAdapter.Value;

                writer.WriteStartElement(Constants.XML_ADAPTER);
                writer.WriteAttributeString(Constants.XML_NAME, adapter.Name);
                writer.WriteAttributeString(Constants.XML_TYPE, adapter.Type);
                writer.WriteEndElement();
                Log("Added Adapter: " + adapter.Name + " of type: " + adapter.Type);
            }
        }
        #endregion

        #region Import Host from File
        public Dictionary<string, Host> ImportHostsFromFile(string file)
        {
            if (!checkFile(file))
            {
                return null;
            }
            XmlDocument doc = new XmlDocument();
            //Handle empty file.
            try
            {
                doc.Load(file);
            }
            catch (XmlException xmlExe)
            {
                if (xmlExe.Message == "Root element is missing.")
                {
                    return null;
                }
                throw xmlExe;
            }
            


            Dictionary<string, Host> Hosts = new Dictionary<string, Host>();


            foreach (XmlNode xnHost in doc.DocumentElement.ChildNodes)
            {
                Host host = new Host();

                host.Name = xnHost.Attributes[Constants.XML_NAME].Value;
                host.Type = xnHost.Attributes[Constants.XML_TYPE].Value;
                foreach (XmlNode xnHostElements in xnHost.ChildNodes)
                {
                    switch (xnHostElements.Name)
                    {
                        case Constants.XML_ALLOWHOSTTRACKING:
                        {
                            host.AllowHostTracking = Convert.ToBoolean(xnHostElements.InnerText);
                            break;
                        }
                        case Constants.XML_AUTHENTICATIONTRUSTED:
                        {
                            host.AuthenticationTrusted = Convert.ToBoolean(xnHostElements.InnerText);
                            break;
                        }
                        case Constants.XML_THIRTYTWOBITONLY:
                        {
                            host.ThirtyTwoBitOnly = Convert.ToBoolean(xnHostElements.InnerText);
                            break;
                        }
                        case Constants.XML_DEFUALTHOST:
                        {
                            host.DefualtHost = Convert.ToBoolean(xnHostElements.InnerText);
                            break;
                        }
                        case Constants.XML_WINDOWSGROUP:
                        {
                            host.WindowsGroup = xnHostElements.InnerText;
                            break;
                        }
                        case Constants.XML_HOSTINSTANCES:
                        {
                            LoadHostInstances(xnHostElements, host);
                            break;
                        }
                        case Constants.XML_SETADAPTERS:
                        {
                            LoadAdapters(xnHostElements, host);
                            break;
                        }
                    }
                }
                if (!Hosts.ContainsKey(host.Name))
                {
                    Hosts.Add(host.Name, host);
                }
                else
                {
                    throw new Exception("Duplicate Host details found: " + host.Name);
                }
            }
            return Hosts;
        }
        private void LoadAdapters(XmlNode xnHostElements, Host host)
        {
            foreach (XmlNode xnAdapters in xnHostElements.ChildNodes)
            {
                Adapter adapter = new Adapter();
                adapter.Name = xnAdapters.Attributes[Constants.XML_NAME].Value;
                adapter.Type = xnAdapters.Attributes[Constants.XML_TYPE].Value;

                string keySearch = Adapter.BuildKey(adapter.Name, host.Name, adapter.Type);
                if(adapter.Type == Constants.TYPE_RECEIVE)
                {
                    if (!host.ReceiveAdapters.ContainsKey(keySearch))
                    {
                        host.ReceiveAdapters.Add(keySearch, adapter);
                    }
                    else
                    {
                        throw new Exception("Duplicate Adapter found: " + keySearch);
                    }
                }
                else if (adapter.Type == Constants.TYPE_SEND)
                {
                    if (!host.SendAdapters.ContainsKey(keySearch))
                    {
                        host.SendAdapters.Add(keySearch, adapter);
                    }
                    else
                    {
                        throw new Exception("Duplicate Adapter found: " + keySearch);
                    }
                }
                else
                {
                     throw new Exception("Invalid Adapter Type: " + adapter.Type);
                }
            }
            
        }
        private void LoadHostInstances(XmlNode xnHostElements, Host host)
        {
            foreach (XmlNode xnServer in xnHostElements.ChildNodes)
            {
                HostInstance hostInstance = new HostInstance();
                hostInstance.ServerName = xnServer.Attributes[Constants.XML_NAME].Value;
                hostInstance.Disable = Convert.ToBoolean(xnServer.Attributes[Constants.XML_DISABLEHOSTINSTANCE].Value); ;
                foreach (XmlNode xnServerElments in xnServer.ChildNodes)
                {
                    switch (xnServerElments.Name)
                    {
                        case Constants.XML_USERNAME:
                            {
                                hostInstance.UserName = xnServerElments.InnerText;
                                break;
                            }
                        case Constants.XML_PASSWORD:
                            {
                                hostInstance.Password = xnServerElments.InnerText;
                                break;
                            }
                    }
                }
                if (!host.HostInstances.ContainsKey(hostInstance.ServerName))
                {
                    host.HostInstances.Add(hostInstance.ServerName, hostInstance);
                }
                else
                {
                    throw new Exception("Duplicate host Instance found: " + hostInstance.ServerName);
                }
            }
        }
        private bool checkFile(string szFile)
        {
            if (szFile == null || szFile.Length == 0)
            {
                throw new Exception("Invalid File Passed");
            }
            if (File.Exists(szFile))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
