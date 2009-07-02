namespace SandBox.Biztalk.Sample.Maps {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"SandBox.Biztalk.Sample.Schema.Transform3", typeof(SandBox.Biztalk.Sample.Schema.Transform3))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"SandBox.Biztalk.Sample.Schema.Transform4", typeof(SandBox.Biztalk.Sample.Schema.Transform4))]
    public sealed class Transform3_to_Transform4 : Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0 userCSharp"" version=""1.0"" xmlns:ns0=""http://SandBox.Biztalk.Sample.Schema.Transform4"" xmlns:s0=""http://SandBox.Biztalk.Sample.Schema.Transform3"" xmlns:userCSharp=""http://schemas.microsoft.com/BizTalk/2003/userCSharp"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:Root"" />
  </xsl:template>
  <xsl:template match=""/s0:Root"">
    <xsl:variable name=""var:v1"" select=""userCSharp:StringConcat(&quot;Transform3_to_Transform4 complete&quot;)"" />
    <ns0:Root>
      <Details>
        <Mapper1>
          <xsl:value-of select=""item/Mapper1/text()"" />
        </Mapper1>
        <Mapper2>
          <xsl:value-of select=""item/Mapper2/text()"" />
        </Mapper2>
        <Mapper3>
          <xsl:value-of select=""$var:v1"" />
        </Mapper3>
        <xsl:value-of select=""item/Details/text()"" />
      </Details>
    </ns0:Root>
  </xsl:template>
  <msxsl:script language=""C#"" implements-prefix=""userCSharp""><![CDATA[
public string StringConcat(string param0)
{
   return param0;
}



]]></msxsl:script>
</xsl:stylesheet>";
        
        private const string _strArgList = @"<ExtensionObjects />";
        
        private const string _strSrcSchemasList0 = @"SandBox.Biztalk.Sample.Schema.Transform3";
        
        private const SandBox.Biztalk.Sample.Schema.Transform3 _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"SandBox.Biztalk.Sample.Schema.Transform4";
        
        private const SandBox.Biztalk.Sample.Schema.Transform4 _trgSchemaTypeReference0 = null;
        
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
                _SrcSchemas[0] = @"SandBox.Biztalk.Sample.Schema.Transform3";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"SandBox.Biztalk.Sample.Schema.Transform4";
                return _TrgSchemas;
            }
        }
    }
}
