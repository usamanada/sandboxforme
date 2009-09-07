@ECHO OFF
IF EXIST "%ProgramFiles%\Microsoft Office\Office12\excel.exe" GOTO INSTALLED
GOTO INSTALL

:INSTALL
ECHO Install Office 2007
@ECHO ON
[BASEINSTALLDIR]office2007\setup.exe /adminfile [BASEINSTALLDIR]ConfigFiles\office2007\Office2007SilentConfig.MSP
@ECHO OFF
GOTO END

:INSTALLED
ECHO Office 2007 Already installed
:END




