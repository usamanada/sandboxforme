namespace SandBox.Biztalk.Sample.Maps {
    
    
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"SandBox.Biztalk.Sample.Schema.Transform1", typeof(SandBox.Biztalk.Sample.Schema.Transform1))]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"SandBox.Biztalk.Sample.Schema.Transform2", typeof(SandBox.Biztalk.Sample.Schema.Transform2))]
    public sealed class Transform1_to_Transform2 : Microsoft.XLANGs.BaseTypes.TransformBase {
        
        private const string _strMap = @"<?xml version=""1.0"" encoding=""UTF-16""?>
<xsl:stylesheet xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"" xmlns:msxsl=""urn:schemas-microsoft-com:xslt"" xmlns:var=""http://schemas.microsoft.com/BizTalk/2003/var"" exclude-result-prefixes=""msxsl var s0 userCSharp"" version=""1.0"" xmlns:ns0=""http://SandBox.Biztalk.Sample.Schema.Transform2"" xmlns:s0=""http://SandBox.Biztalk.Sample.Schema.Transform1"" xmlns:userCSharp=""http://schemas.microsoft.com/BizTalk/2003/userCSharp"">
  <xsl:output omit-xml-declaration=""yes"" method=""xml"" version=""1.0"" />
  <xsl:template match=""/"">
    <xsl:apply-templates select=""/s0:Root"" />
  </xsl:template>
  <xsl:template match=""/s0:Root"">
    <xsl:variable name=""var:v1"" select=""userCSharp:StringConcat(&quot;Transform1_to_Transform2.btm completed&quot;)"" />
    <ns0:Root>
      <item>
        <xsl:if test=""Item/@Details"">
          <Description>
            <xsl:value-of select=""Item/@Details"" />
          </Description>
        </xsl:if>
        <Mapper1>
          <xsl:value-of select=""$var:v1"" />
        </Mapper1>
        <xsl:if test=""Item/@Mapper2"">
          <Mapper2>
            <xsl:value-of select=""Item/@Mapper2"" />
          </Mapper2>
        </xsl:if>
        <xsl:if test=""Item/@Mapper3"">
          <Mapper3>
            <xsl:value-of select=""Item/@Mapper3"" />
          </Mapper3>
        </xsl:if>
      </item>
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
        
        private const string _strSrcSchemasList0 = @"SandBox.Biztalk.Sample.Schema.Transform1";
        
        private const SandBox.Biztalk.Sample.Schema.Transform1 _srcSchemaTypeReference0 = null;
        
        private const string _strTrgSchemasList0 = @"SandBox.Biztalk.Sample.Schema.Transform2";
        
        private const SandBox.Biztalk.Sample.Schema.Transform2 _trgSchemaTypeReference0 = null;
        
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
                _SrcSchemas[0] = @"SandBox.Biztalk.Sample.Schema.Transform1";
                return _SrcSchemas;
            }
        }
        
        public override string[] TargetSchemas {
            get {
                string[] _TrgSchemas = new string [1];
                _TrgSchemas[0] = @"SandBox.Biztalk.Sample.Schema.Transform2";
                return _TrgSchemas;
            }
        }
    }
}
