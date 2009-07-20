using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SandBox.Winform.SilentInstall
{
    class EnvironmentsSection : ConfigurationSection
    {
        [ConfigurationProperty("Environments")]
        [ConfigurationCollection(typeof(EnvironmentCollection), AddItemName = "Environment")]
        public EnvironmentCollection EnvironmentItems
        {
            get
            {
                return (EnvironmentCollection)base["Environments"];
            }
        }
    }
    public sealed class EnvironmentCollection : ConfigurationElementCollection
    {        
        protected override ConfigurationElement CreateNewElement()
        {
            return new EnvironmentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EnvironmentElement)element).Name;            
        }
        public EnvironmentElement this[int idx]
        {
            get { return (EnvironmentElement)BaseGet(idx); }
        }
        new public EnvironmentElement this[string key]
        {
            get
            {
                return (EnvironmentElement)BaseGet(key);
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
    public class ApplicationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ApplicationElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApplicationElement)element).Order;
        }
        public ApplicationElement this[int idx]
        {
            get { return (ApplicationElement)BaseGet(idx); }
        }
    }
    
    public class EnvironmentElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }
        [ConfigurationProperty("Description", IsKey = false, IsRequired = true)]
        public string Description
        {
            get
            {
                return (string)base["Description"];
            }
            set
            {
                base["Description"] = value;
            }
        }
        [ConfigurationProperty("applications")]
        [ConfigurationCollection(typeof(EnvironmentCollection), AddItemName = "application")]
        public ApplicationCollection ApplicationItems
        {
            get { return ((ApplicationCollection)(base["applications"])); }
        }
    }

    public class ApplicationElement : ConfigurationElement
    {
        [ConfigurationProperty("order", IsKey = true, IsRequired = true)]
        public string Order
        {
            get
            {
                return (string)base["order"];
            }
            set
            {
                base["order"] = value;
            }
        }
        [ConfigurationProperty("value", IsKey = false, IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)base["value"];
            }
            set
            {
                base["value"] = value;
            }
        }
    }
}
