c:
"c:\Windows\Microsoft.NET\Framework\v3.0\Windows Communication Foundation\ServiceModelReg.exe" -i

IISRESET

[BASEINSTALLDIR]Biztalk2009\setup /SetupXml [BASEINSTALLDIR]Biztalk2009\UDDI_x86\Setup.xml
"C:\Program Files\Microsoft UDDI Services\config\Configuration.exe"

IISRESET

[BASEINSTALLDIR]BizTalkESBToolkit2_0\MSChart.exe /passive
[BASEINSTALLDIR]BizTalkESBToolkit2_0\VsSDK_sfx.exe /passive
[BASEINSTALLDIR]BizTalkESBToolkit2_0\sqlxml_40_SP1.msi /passive
"[BASEINSTALLDIR]BizTalkESBToolKit2_0\BizTalk ESB Toolkit 2.0 Docs.msi" /passive
"[BASEINSTALLDIR]BizTalkESBToolKit2_0\BizTalk ESB Toolkit 2.0-x86.msi" /passive

BTSTASK Importapp -Package:"c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Microsoft.Practices.ESB.CORE.msi" /ApplicationName:Microsoft.Practices.ESB /overwrite
"c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Microsoft.Practices.ESB.Core.msi" /passive

BTSTASK Importapp -Package:"c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Microsoft.Practices.ESB.JMS.msi" /ApplicationName:Microsoft.Practices.JMS /overwrite
"c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Microsoft.Practices.ESB.JMS.msi" /passive

"c:\Program Files\Microsoft BizTalk Server 2009\Tracking\bm.exe" deploy-all -DefinitionFile:"C:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Bam\Microsoft.BizTalk.ESB.BAM.Exceptions.xml"
"c:\Program Files\Microsoft BizTalk Server 2009\Tracking\bm.exe" deploy-all -DefinitionFile:"C:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Bam\Microsoft.BizTalk.ESB.BAM.Itinerary.xml"

cd "C:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Bin\"
ESBConfigurationTool.exe

cd "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Bin"
Microsoft.Practices.ESB.UDDIPublisher.exe
cd [BASEINSTALLDIR]

powershell.exe [BASEINSTALLDIR]BizTalkESBToolKit2_0\unzip.ps1 'c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\ESBSource.zip' 'C:\Projects\Microsoft.Practices.ESB\'

attrib -r -s -h "C:\Projects\Microsoft.Practices.ESB\\Keys\*.*" /d /s
attrib -r -s -h "C:\Projects\Microsoft.Practices.ESB\\Source\*.*" /d /s

[BASEINSTALLDIR]BizTalkESBToolKit2_0\sn.exe -k "C:\Projects\Microsoft.Practices.ESB\Keys\Microsoft.Practices.ESB.snk"

copy [BASEINSTALLDIR]BizTalkESBToolKit2_0\ProjectFiles\Web.config "C:\Projects\Microsoft.Practices.ESB\Source\Samples\Management Portal\ESB.Portal\" /Y
copy [BASEINSTALLDIR]BizTalkESBToolKit2_0\ProjectFiles\ESB.AlertService.csproj "C:\Projects\Microsoft.Practices.ESB\Source\Samples\Management Portal\ESB.AlertService\" /Y
copy [BASEINSTALLDIR]BizTalkESBToolKit2_0\ProjectFiles\Setup.vdproj "C:\Projects\Microsoft.Practices.ESB\Source\Samples\Management Portal\ESB.AlertService.Install\" /Y


"c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.com" "C:\Projects\Microsoft.Practices.ESB\Source\Samples\Management Portal\ESB.AlertService\ESB.AlertService.sln" /build Debug
"c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.com" "C:\Projects\Microsoft.Practices.ESB\Source\Samples\Management Portal\ESB.UDDI.PublisherService\ESB.UDDI.PublisherService.sln" /build Debug


"C:\Projects\Microsoft.Practices.ESB\Source\Samples\Management Portal\ESB.AlertService.Install\Debug\setup.exe" /passive
"C:\Projects\Microsoft.Practices.ESB\Source\Samples\Management Portal\\ESB.UDDI.PublisherService.Install\Debug\setup.exe" /passive


cd "C:\Projects\Microsoft.Practices.ESB\Source\Samples\Management Portal\Install\Scripts\"
Management_Install.cmd

cd "c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\"
devenv.exe /InstallVSTemplates "c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\ItemTemplates\CSharp\1033\ItineraryDsl.zip"
devenv.exe /InstallVSTemplates "c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\ItemTemplates\VisualBasic\1033\ItineraryDsl.zip"

