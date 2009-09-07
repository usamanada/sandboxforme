using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Xml;

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

        public int CurrentOrder;
        public Dictionary<int, string> applications = new Dictionary<int, string>();
        #endregion 

        public string CreateWorkDir()
        {
            string workDir = ConfigurationManager.AppSettings["WorkConfigDir"];
            if (!Directory.Exists(workDir))
            {
                Directory.CreateDirectory(workDir);
            }
            DirectoryInfo di = new DirectoryInfo(workDir);
            return di.FullName;
        }

        private bool keyExists(string regKey, string regValue)
        {
            var result = Registry.GetValue(regKey, regValue, null);
            return result != null;
        }

        #region WorkConfig
        public string GetFilePathWorkConfig()
        {
            string workDir = CreateWorkDir();

            return Path.Combine(workDir, ConfigurationManager.AppSettings["WorkConfig"]);

        }
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
                applications.Clear();

                XmlDocument doc = new XmlDocument();
                doc.Load(filePathWorkConfig);
                XPathNavigator navigator = doc.CreateNavigator();
                XPathNodeIterator iter = navigator.Select("//Work/Order");
                if (iter.MoveNext())
                {
                    CurrentOrder = iter.Current.ValueAsInt;
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
        private void CleanWorkConfig()
        {
            if (File.Exists(GetFilePathWorkConfig()))
            {
                File.Delete(GetFilePathWorkConfig());
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
            _defaultUserName = "";
            _defaultPassword = "";
            _defaultDomainName = "";

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
                string workDir = CreateWorkDir();
                string filePathContuneSource = Path.Combine(workDir, fileContineBat);
                if (!File.Exists(filePathContuneSource))
                {
                    throw new FileNotFoundException("File " + filePathContuneSource + " not found.");
                }
                File.Copy(filePathContuneSource, filePathContinueDest);
            }
        }

        public void CopyBatchFiles(string domain, string username, string password, string servername)
        {
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["ConfigDir"]);

            string tempFile = Guid.NewGuid() + ".tmp";
            DirectoryInfo diCurrent = new DirectoryInfo(Environment.CurrentDirectory);

            foreach (string file in files)
            {
                File.Copy(file, tempFile, true);

                FileHelper.ReplaceInFile(tempFile, @"\[WORKINGDRIVE\]",
                                         diCurrent.Root.ToString().Substring(0,diCurrent.Root.ToString().Length - 1));
                FileHelper.ReplaceInFile(tempFile, @"\[BASEINSTALLDIR\]", diCurrent.FullName + "\\");
                FileHelper.ReplaceInFile(tempFile, @"\[DOMAIN\]", domain);
                FileHelper.ReplaceInFile(tempFile, @"\[USERNAME\]", username);
                FileHelper.ReplaceInFile(tempFile, @"\[PASSWORD\]", password);
                FileHelper.ReplaceInFile(tempFile, @"\[SERVERNAME\]", servername);
                
                
                FileInfo fi = new FileInfo(file);
                File.Copy(tempFile, Path.Combine(CreateWorkDir(), fi.Name), true);
            }

            File.Delete(tempFile);

            CopyContineBatch();
        }
        #endregion

        #region Log File
        public string LogFilePath()
        {
            return Path.Combine(CreateWorkDir(), ConfigurationManager.AppSettings["LogFile"]);
        }

        public void LogFileDelete()
        {
            if(File.Exists(LogFilePath()))
            {
                File.Delete(LogFilePath());
            }
        }
        public string ApplicationLogFilePath()
        {
            return Path.Combine(CreateWorkDir(), ConfigurationManager.AppSettings["ApplicationLogFile"]);
        }

        public void ApplicationLogFileDelete()
        {
            if(File.Exists(ApplicationLogFilePath()))
            {
                File.Delete(ApplicationLogFilePath());
            }
        }
        
        #endregion

        #region Clean
        public void CleanAll()
        {
            LogFileDelete();
            ApplicationLogFileDelete();
            CleanContinueBatch();
            CleanAutoLoginRegDetails();
            CleanWorkConfig();
            if(_defaultUserName == String.Empty)
            {
                SetAutoLoginRegDetails(_defaultUserName, _defaultDomainName, _defaultPassword);
            }
        }

        public void CleanAutoStart()
        {
            CleanContinueBatch();
            CleanAutoLoginRegDetails();
        }
        #endregion

        public bool ValidateConfigFile()
        {
            EnvironmentsSection es;
            InstallApplicationsSection ia;
            es = ConfigurationManager.GetSection("InstallChoice") as EnvironmentsSection;
            ia = ConfigurationManager.GetSection("InstallApplications") as InstallApplicationsSection;

            foreach (EnvironmentElement ee in es.EnvironmentItems)
            {
                foreach (ApplicationElement ae in ee.ApplicationItems)
                {
                    if (!ia.InstallApplicationsItems.HasKey(ae.Value))
                    {
                        Error = string.Format("Invalid value of {0} in Environment name {1}, application order {2} can not be found in InstallApplications section of the config file", ae.Value, ee.Name, ae.Order);
                        return false;
                    }    
                }
            }
            return true;
        }
        public string Error { set; get; }
    }
}
