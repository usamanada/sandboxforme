"c:\Windows\Microsoft.NET\Framework\v3.0\Windows Communication Foundation\ServiceModelReg.exe" -i

IISRESET

"[BASEINSTALLDIR]Biztalk2009\UDDI_x86\Microsoft UDDI Services.msi" /passive
"C:\Program Files\Microsoft UDDI Services\config\Configuration.exe"

IISRESET

[BASEINSTALLDIR]BizTalkESBToolkit2_0\MSChart.exe /passive
[BASEINSTALLDIR]BizTalkESBToolkit2_0\VsSDK_sfx.exe /passive
[BASEINSTALLDIR]BizTalkESBToolkit2_0\sqlxml_40_SP1.msi /passive
"[BASEINSTALLDIR]BizTalkESBToolKit2_0\BizTalk ESB Toolkit 2.0 Docs.msi" /passive
"[BASEINSTALLDIR]BizTalkESBToolKit2_0\BizTalk ESB Toolkit 2.0-x86.msi" /passive

BTSTASK Importapp -Package:"c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Microsoft.Practices.ESB.ExceptionHandling.msi" /ApplicationName:Microsoft.Practices.ESB /Overwrite
"c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Microsoft.Practices.ESB.ExceptionHandling.msi" /passive

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
cd c:\install

powershell.exe [BASEINSTALLDIR]BizTalkESBToolKit2_0\unzip.ps1 'c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\ESBSource.zip' 'c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\'

attrib -r -s -h "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Keys\*.*" /d /s
attrib -r -s -h "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\*.*" /d /s

"C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\sn.exe" -k "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Keys\Microsoft.Practices.ESB.snk"

copy [BASEINSTALLDIR]BizTalkESBToolKit2_0\ProjectFiles\Web.config "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\Samples\Management Portal\ESB.Portal\" /Y
copy [BASEINSTALLDIR]BizTalkESBToolKit2_0\ProjectFiles\ESB.AlertService.csproj "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\Samples\Management Portal\ESB.AlertService\" /Y
copy [BASEINSTALLDIR]BizTalkESBToolKit2_0\ProjectFiles\Setup.vdproj "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\Samples\Management Portal\ESB.AlertService.Install\" /Y


"c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.com" "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\Samples\Management Portal\ESB.AlertService\ESB.AlertService.sln" /build Debug
"c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.com" "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\Samples\Management Portal\ESB.UDDI.PublisherService\ESB.UDDI.PublisherService.sln" /build Debug


"c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\Samples\Management Portal\ESB.AlertService.Install\Debug\setup.exe" /passive
"c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\Samples\Management Portal\\ESB.UDDI.PublisherService.Install\Debug\setup.exe" /passive


cd "c:\Program Files\Microsoft BizTalk ESB Toolkit 2.0\Source\Samples\Management Portal\Install\Scripts\"
Management_Install.cmd
