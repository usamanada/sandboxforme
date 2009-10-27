@ECHO OFF
IF EXIST "%ProgramFiles%\Microsoft BizTalk Server 2009\SSO\Biztalk2009SSO.bak" GOTO INSTALLED
GOTO INSTALL

:INSTALL
ECHO Install Biztalk 2009 Configuration
@ECHO ON
powershell.exe [BASEINSTALLDIR]ConfigFiles\Biztalk2009Config\ReplaceSql2008ConfigurationFile.ps1 [BASEINSTALLDIR]ConfigFiles\Biztalk2009Config\ReplaceBiztalk2009Config.xml [BASEINSTALLDIR]ConfigFiles\Biztalk2009Config\Biztalk2009Config.xml [BASEINSTALLDIR] [USERNAME] [DOMAIN] [PASSWORD] [SERVERNAME]
mkdir "%ProgramFiles%\Microsoft BizTalk Server 2009\SSO\"
"%ProgramFiles%\Microsoft BizTalk Server 2009\Configuration.exe" /s [BASEINSTALLDIR]ConfigFiles\Biztalk2009Config\Biztalk2009Config.xml
@ECHO OFF
GOTO END

:INSTALLED
ECHO Biztalk 2009 Configuration Already installed
:END