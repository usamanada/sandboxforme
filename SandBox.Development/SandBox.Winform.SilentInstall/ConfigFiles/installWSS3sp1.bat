@ECHO OFF
IF EXIST "%ProgramFiles%\Common Files\microsoft shared\Web Server Extensions\12\ISAPI\Microsoft.SharePoint.dll" GOTO INSTALLED
GOTO INSTALL

:INSTALL
ECHO Install WSS3 with SP1
@ECHO ON
[BASEINSTALLDIR]WSS_3_sp1\SharePoint.exe /extract:[BASEINSTALLDIR]WSS_3_sp1\ /passive /quiet
[BASEINSTALLDIR]WSS_3_sp1\setup.exe /config [BASEINSTALLDIR]WSS_3_sp1\WssSharePointConfig.xml
@ECHO OFF
GOTO END

:INSTALLED
ECHO WSS3 with SP1 Already installed
:END
