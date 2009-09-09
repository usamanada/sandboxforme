namespace Sandbox.Biztalk.QueryNotification {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Sandbox.QueryNotification.Notification", typeof(Sandbox.QueryNotification.Notification))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"Sandbox.Biztalk.QueryNotification.Sandbox_dbo_employee_selectTableOperation_dbo_Employee+Select", typeof(Sandbox.Biztalk.QueryNotification.Sandbox_dbo_employee_selectTableOperation_dbo_Employee.Select))]
    public sealed class SelectEmployee : Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0 userCSharp"" version=""1.0"" xmlns:ns3=""http://schemas.microsoft.com/Sql/2008/05/Types/Tables/dbo"" xmlns:s0=""http://schemas.microsoft.com/Sql/2008/05/Notification/"" xmlns:ns0=""http://schemas.microsoft.com/Sql/2008/05/TableOp/dbo/Employee"" xmlns:userCSharp=""http://schemas.microsoft.com/BizTalk/2003/userCSharp"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:Notification"" />
  </xsl:template>
  <xsl:template match=""/s0:Notification"">
    <xsl:variable name=""var:v1"" select=""userCSharp:StringConcat(&quot;WHERE IsChanged=1;UPDATE dbo.Employee SET IsChanged=0 WHERE IsChanged=1&quot;)"" />
    <ns0:Select>
      <ns0:Columns>
        <xsl:text>*</xsl:text>
      </ns0:Columns>
      <ns0:Query>
        <xsl:value-of select=""$var:v1"" />
      </ns0:Query>
    </ns0:Select>
  </xsl:template>
  <msxsl:script language=""C#"" implements-prefix=""userCSharp""><![CDATA[
public string StringConcat(string param0)
{
   return param0;
}



]]></msxsl:script>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"Sandbox.QueryNotification.Notification";
        
        private const Sandbox.QueryNotification.Notification _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"Sandbox.Biztalk.QueryNotification.Sandbox_dbo_employee_selectTableOperation_dbo_Employee+Select";
        
        public override string XmlContent {
            get {
                return _strMap;
            }
        }
        
        public override string XsltArgumentListContent {
            get {
                return _strArgList;
            }
        }
        
        public override string[] SourceSchemas {
            get {
                string[] _SrcSchemas = new string [1];
                _SrcSchemas[0] = @"Sandbox.QueryNotification.Notification";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"Sandbox.Biztalk.QueryNotification.Sandbox_dbo_employee_selectTableOperation_dbo_Employee+Select";
                return _TrgSchemas;
            }
        }
    }
}
