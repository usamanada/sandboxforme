using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SandBox.Winform.SilentInstall
{
    public class InstallApplicationsSection : ConfigurationSection
    {
        public InstallApplicationsSection()
        {
        }
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public InstallApplicationCollection InstallApplicationsItems
        {
            get
            {
                return (InstallApplicationCollection)base[""];
            }
        }
    }

    public sealed class InstallApplicationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new InstallApplicationsElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((InstallApplicationsElement)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        public InstallApplicationsElement this[int idx]
        {
            get
            {
                return (InstallApplicationsElement)BaseGet(idx);
            }
        }
        new public InstallApplicationsElement this[string key]
        {
            get
            {
                return (InstallApplicationsElement)BaseGet(key);
            }
        }
        public bool HasKey(string key)
        {
            ConfigurationElement ce = this.BaseGet(key);
            if (ce != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override string ElementName
        {
            get
            {
                return "application";
            }
        }
        public string[] Keys
        {
            get
            {
                object[] oKeys = this.BaseGetAllKeys();
                string[] skeys = Array.ConvertAll<object, string>(oKeys, delegate(object os) { return os.ToString(); });
                return skeys;
            }
        }
    }

   


    public class InstallApplicationsElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return ((string)(base["name"]));
            }
            set
            {
                base["name"] = value;
            }
        }
        [ConfigurationProperty("Execute", IsRequired = true)]
        public string Execute
        {
            get
            {
                return ((string)(base["Execute"]));
            }
            set
            {
                base["Execute"] = value;
            }
        }
        [ConfigurationProperty("Description", IsRequired = true)]
        public string Description
        {
            get
            {
                return ((string)(base["Description"]));
            }
            set
            {
                base["Description"] = value;
            }
        }

        [ConfigurationProperty("ExitCode", IsKey = false, IsRequired = true)]
        public string ExitCode
        {
            get
            {
                return (string)base["ExitCode"];
            }
            set
            {
                base["ExitCode"] = value;
            }
        }

        [ConfigurationProperty("UserPassRequired", IsKey = false, IsRequired = false)]
        public string UserPassRequired
        {
            get
            {
                return (string)base["UserPassRequired"];
            }
            set
            {
                base["UserPassRequired"] = value;
            }
        }

        
    }
}
