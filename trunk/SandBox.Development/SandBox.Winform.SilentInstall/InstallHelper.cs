using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Win32;
using SandBox.dll.Common;

namespace SandBox.Winform.SilentInstall
{
    class InstallHelper
    {
        #region Private variables
        private string _autoAdminLogon;
        private string _defaultUserName;
        private string _defaultPassword;
        private string _defaultDomainName;

        
        private const string ConstAutoAdminLogon = "AutoAdminLogon";
        private const string ConstDefaultUserName = "DefaultUserName";
        private const string ConstDefaultPassword = "DefaultPassword";
        private const string ConstDefaultDomainName = "DefaultDomainName";

        public int currentOrder;
        public Dictionary<int, string> applications = new Dictionary<int, string>();
        #endregion 

        public string createWorkDir()
        {
            string workDir = ConfigurationManager.AppSettings["WorkConfigDir"];
            if (!Directory.Exists(workDir))
            {
                Directory.CreateDirectory(workDir);
            }
            DirectoryInfo di = new DirectoryInfo(workDir);
            return di.FullName;
        }
        private string GetFilePathWorkConfig()
        {
            string workDir = createWorkDir();

            return Path.Combine(workDir, ConfigurationManager.AppSettings["WorkConfig"]);

        }
        private bool keyExists(string regKey, string regValue)
        {
            var result = Registry.GetValue(regKey, regValue, null);
            return result != null;
        }

        #region WorkConfig
        public void WorkConfigCreate(IList<ListBoxDisplay> installApplications)
        {
            string filePathWorkConfig = GetFilePathWorkConfig();

            XmlTextWriter writer = new XmlTextWriter(filePathWorkConfig, null) { Formatting = Formatting.Indented };

            writer.WriteStartDocument();
            writer.WriteStartElement("Work");
            writer.WriteStartElement("Order");
            writer.WriteString("0");
            writer.WriteEndElement();

            if (_autoAdminLogon == "1")
            {
                writer.WriteStartElement(ConstAutoAdminLogon);
                writer.WriteStartElement(ConstDefaultUserName);
                writer.WriteString(_defaultUserName);
                writer.WriteEndElement();
                writer.WriteStartElement(ConstDefaultPassword);
                writer.WriteString(_defaultPassword);
                writer.WriteEndElement();
                writer.WriteStartElement(ConstDefaultDomainName);
                writer.WriteString(_defaultDomainName);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }

            writer.WriteStartElement("ApplicationsToInstall");

            for (int index = 0; index < installApplications.Count; index++)
            {
                writer.WriteStartElement("application");
                writer.WriteAttributeString("order", (index + 1).ToString());
                writer.WriteAttributeString("value", installApplications[index].Value.ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.Close();
        }
        public void SetOrder(int order)
        {
            string filePathWorkConfig = GetFilePathWorkConfig();
            if (File.Exists(filePathWorkConfig))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePathWorkConfig);
                XPathNavigator navigator = doc.CreateNavigator();
                XPathNodeIterator iter = navigator.Select("//Work/Order");
                if (iter.MoveNext())
                {
                    iter.Current.InnerXml = order.ToString();
                }
                doc.Save(filePathWorkConfig);
            }
        }
        public void WorkConfigReadApplications()
        {
            string filePathWorkConfig = GetFilePathWorkConfig();
            if (File.Exists(filePathWorkConfig))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePathWorkConfig);
                XPathNavigator navigator = doc.CreateNavigator();
                XPathNodeIterator iter = navigator.Select("//Work/Order");
                if (iter.MoveNext())
                {
                    currentOrder = iter.Current.ValueAsInt;
                }
                iter = navigator.Select("//Work/ApplicationsToInstall/application");
                while(iter.MoveNext())
                {
                    iter.Current.MoveToFirstAttribute();
                    int ordervalue = iter.Current.ValueAsInt;
                    iter.Current.MoveToNextAttribute();
                    string description = iter.Current.Value;
                    applications.Add(ordervalue, description);
                }
            }
        }

        #endregion

        #region Auto Login
        public void SaveAutoLoginRegDetails()
        {
            string regkeyWinlogin = Path.Combine(ConfigurationManager.AppSettings["RegKeyBaseMachine"],
                                                 ConfigurationManager.AppSettings["RegKeyWinlogon"]);

            var temp = Registry.GetValue(regkeyWinlogin, ConstAutoAdminLogon, null);
            if (temp != null)
            {
                _autoAdminLogon = temp.ToString();
            }

            temp = Registry.GetValue(regkeyWinlogin, ConstDefaultUserName, null);
            if (temp != null)
            {
                _defaultUserName = temp.ToString();
            }

            temp = Registry.GetValue(regkeyWinlogin, ConstDefaultPassword, null);
            if (temp != null)
            {
                _defaultPassword = temp.ToString();
            }

            temp = Registry.GetValue(regkeyWinlogin, ConstDefaultDomainName, null);
            if (temp != null)
            {
                _defaultDomainName = temp.ToString();
            }
        }
        public void CleanAutoLoginRegDetails()
        {
            string regkeyBase = ConfigurationManager.AppSettings["RegKeyBaseMachine"];
            string regkeyWinlogin = Path.Combine(regkeyBase, ConfigurationManager.AppSettings["RegKeyWinlogon"]);

            if (regkeyBase == "HKEY_LOCAL_MACHINE")
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(ConfigurationManager.AppSettings["RegKeyWinlogon"], true);
                if (rk != null)
                {
                    if (keyExists(regkeyWinlogin, ConstAutoAdminLogon))
                    {
                        rk.DeleteValue(ConstAutoAdminLogon);
                    }
                    if (keyExists(regkeyWinlogin, ConstDefaultUserName))
                    {
                        rk.DeleteValue(ConstDefaultUserName);
                    }
                    if (keyExists(regkeyWinlogin, ConstDefaultPassword))
                    {
                        rk.DeleteValue(ConstDefaultPassword);
                    }
                    if (keyExists(regkeyWinlogin, ConstDefaultDomainName))
                    {
                        rk.DeleteValue(ConstDefaultDomainName);
                    }
                }
            }
        }
        public void SetAutoLoginRegDetails(string userName, string domain, string password)
        {
            string regkeyWinlogin = Path.Combine(ConfigurationManager.AppSettings["RegKeyBaseMachine"],
                                                 ConfigurationManager.AppSettings["RegKeyWinlogon"]);
            
            Registry.SetValue(regkeyWinlogin, ConstAutoAdminLogon, "1");
            Registry.SetValue(regkeyWinlogin, ConstDefaultUserName, userName);
            Registry.SetValue(regkeyWinlogin, ConstDefaultPassword, password);
            Registry.SetValue(regkeyWinlogin, ConstDefaultDomainName, domain);
        }
        public void ReadAutoLoginDetails()
        {
            string filePathWorkConfig = GetFilePathWorkConfig();
            if (File.Exists(filePathWorkConfig))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePathWorkConfig);
                XPathNavigator navigator = doc.CreateNavigator();
                XPathNodeIterator iter = navigator.Select("//Work/" + ConstAutoAdminLogon + "/" + ConstDefaultUserName);
                if(iter.MoveNext())
                {
                    _defaultUserName = iter.Current.Value;
                }

                iter = navigator.Select("//Work/" + ConstAutoAdminLogon + "/" + ConstDefaultPassword);
                if (iter.MoveNext())
                {
                    _defaultPassword = iter.Current.Value;
                }

                iter = navigator.Select("//Work/" + ConstAutoAdminLogon + "/" + ConstDefaultDomainName);
                if (iter.MoveNext())
                {
                    _defaultDomainName = iter.Current.Value;
                }
            }
        }
        #endregion

        #region Batch Files
        public void CleanContinueBatch()
        {
            string dirStartUp = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string filePathContinue = Path.Combine(dirStartUp, ConfigurationManager.AppSettings["ContinueBat"]);
            if(File.Exists(filePathContinue))
            {
                File.Delete(filePathContinue);
            }
        }
        public void CopyContineBatch()
        {
            string dirStartUp = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string fileContineBat = ConfigurationManager.AppSettings["ContinueBat"];
            string filePathContinueDest = Path.Combine(dirStartUp, fileContineBat);
            if (!File.Exists(filePathContinueDest))
            {
                string workDir = createWorkDir();
                string filePathContuneSource = Path.Combine(workDir, fileContineBat);
                if (!File.Exists(filePathContuneSource))
                {
                    throw new FileNotFoundException("File " + filePathContuneSource + " not found.");
                }
                File.Copy(filePathContuneSource, filePathContinueDest);
            }
        }

        public void CopyBatchFiles()
        {
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["ConfigDir"]);

            string tempFile = Guid.NewGuid() + ".tmp";
            DirectoryInfo diConfig = new DirectoryInfo(ConfigurationManager.AppSettings["ConfigDir"]);

            foreach (string file in files)
            {
                File.Copy(file, tempFile, true);

                FileHelper.ReplaceInFile(tempFile, @"\[WORKINGDRIVE\]", diConfig.Root.ToString());
                FileHelper.ReplaceInFile(tempFile, @"\[BASEINSTALLDIR\]", diConfig.FullName);

                FileInfo fi = new FileInfo(file);
                File.Copy(tempFile, Path.Combine(createWorkDir(), fi.Name), true);
            }

            File.Delete(tempFile);
        }
        #endregion

       
    }
}
