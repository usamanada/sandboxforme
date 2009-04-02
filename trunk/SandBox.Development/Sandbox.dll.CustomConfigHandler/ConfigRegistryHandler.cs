using System;
using System.Configuration;
using System.Xml;
using System.Collections.Generic;

namespace Sandbox.dll.CustomConfigHandler
{
    public class ConfigRegistryHandler : IConfigurationSectionHandler
    {
        public ConfigRegistryHandler() { }

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {

            List<ConfigRegistry> cr = new List<ConfigRegistry>();


            foreach( XmlNode cNode in section.ChildNodes)
            {
                ConfigRegistry cReg = new ConfigRegistry();
                cReg.ValueName = cNode.Attributes["ValueName"].Value;
                cReg.RegKey = cNode.Attributes["RegKey"].Value;
                cReg.Value = cNode.Attributes["Value"].Value;
                cReg.Type = cNode.Attributes["Type"].Value;
                cr.Add(cReg);
            }
            
            return cr;
        }
    }

    public class ConfigRegistry
    {
        #region private variables
        private string _ValueName;
        private string _RegKey;
        private string _Value;
        private string _Type;
        #endregion

        public ConfigRegistry()
        {
            _ValueName = "";
            _RegKey = "";
            _Value = "";
        }

        public string ValueName
		{
			get{
				return _ValueName;
			}
			set{
				_ValueName = value;
			}
		}

        public string RegKey
		{
			get{
				return _RegKey;
			}
			set{
				_RegKey = value;
			}
		}

        public string Value
		{
			get{
				return _Value;
			}
			set{
				_Value = value;
			}
		}
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
    }
}
