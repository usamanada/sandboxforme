@ECHO OFF
set iisSiteName=BizTalk SharePoint Services Adapter Site
%systemroot%\System32\inetsrv\appcmd list site /name:"%iisSiteName%" || GOTO INSTALL
GOTO INSTALLED

:INSTALL
set appPoolUser=[DOMAIN]\[USERNAME]
set appPoolPassword=[PASSWORD]
set portNumber=11000
set iisServerName=[SERVERNAME]

ECHO Install SharePoint Site %iisSiteName% %portNumber%

set appPoolName=BizTalkWssAdapterAppPool


set iisPortNo=%portNumber%
set iisSitePhysicalBasePath=C:\inetpub\wwwroot\wss\VirtualDirectories\

set wssWebAppContentDb=WSS_Content_BizTalkSharePointAdapterSite
set wssWebAppName=%iisSiteName%
set dbServerName=%iisServerName%
set dbUser=%appPoolUser%
set dbPassword=%appPoolPassword%

set wssSiteOwnerUser=%appPoolUser%
set wssSiteOwnerEmail=mysomething@blah.com
set wssSiteOwnerName=BizTalk
set wssSiteSecondaryOwnerUser=%appPoolUser%
set wssSiteSecondaryOwnerEmail=mysomething@blah.com
set wssSiteSecondaryOwnerName=BizTalk
set wssSiteCollectionTitle=%wssWebAppName%
set wssSiteCollectionDescription=%wssSiteCollectionTitle%
@ECHO ON
%systemroot%\System32\inetsrv\appcmd.exe add apppool /name:"%appPoolName%" /managedRuntimeVersion:"v2.0" /managedPipelineMode:Classic /processModel.identityType:SpecificUser /processModel.userName:"%appPoolUser%" /processModel.password:"%appPoolPassword%"
%systemroot%\System32\inetsrv\appcmd.exe add site /name:"%iisSiteName%" /id:%iisPortNo% /physicalPath:%iisSitePhysicalBasePath%%iisPortNo% /bindings:http/*:%iisPortNo%:
%systemroot%\System32\inetsrv\appcmd.exe set app "%iisSiteName%/" -applicationPool:"%appPoolName%"

%systemroot%\System32\inetsrv\appcmd.exe set config /section:anonymousAuthentication /enabled:false
%systemroot%\System32\inetsrv\appcmd.exe set config "BizTalk SharePoint Adapter Site/" /section:identity /impersonate:true
%systemroot%\System32\inetsrv\appcmd.exe set config /section:windowsAuthentication /enabled:true

%systemroot%\System32\inetsrv\appcmd.exe stop site "%iisSiteName%"
%systemroot%\System32\inetsrv\appcmd.exe start site "%iisSiteName%"


"%COMMONPROGRAMFILES%\Microsoft Shared\Web Server Extensions\12\bin\stsadm.exe" -o extendvs -url "http://%iisServerName%:%iisPortNo%" -exclusivelyusentlm -databaseserver "%dbServerName%" -databasename "%wssWebAppContentDb%" -lcid 1033 -donotcreatesite -description "%wssWebAppName%" -apidname "%appPoolName%" -apidtype ConfigurableID -apidlogin "%appPoolUser%"  -apidpwd "%appPoolPassword%"

"%COMMONPROGRAMFILES%\Microsoft Shared\Web Server Extensions\12\bin\stsadm.exe" -o createsite -url "http://%iisServerName%:%iisPortNo%" -ownerlogin "%wssSiteOwnerUser%" -owneremail "%wssSiteOwnerEmail%" -ownername "%wssSiteOwnerName%" -secondarylogin "%wssSiteSecondaryOwnerUser%" -secondaryemail "%wssSiteSecondaryOwnerEmail%" -secondaryname "%wssSiteSecondaryOwnerName%" -lcid 1033 -sitetemplate "STS#1" -title "%wssSiteCollectionTitle%" -description "%wssSiteCollectionDescription%"

IISRESET
ECHO Check has been extended http://%iisServerName%:%iisPortNo%
"%ProgramFiles%\Microsoft BizTalk Server 2009\Microsoft.BizTalk.KwTpm.StsOmInterop3.exe" http://%iisServerName%:%iisPortNo% || GOTO ERROR

GOTO END

:ERROR
ECHO Install Faild of http://%iisServerName%:%iisPortNo%
exit 1

:INSTALLED
ECHO SharePoint Site BizTalk SharePoint Services Adapter Site [PortNumber] Already Installed
:END



