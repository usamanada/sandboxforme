@ECHO OFF
IF EXIST "%ProgramFiles%\Microsoft SQL Server\100\Tools\Binn\VSShell\Common7\IDE\Ssms.exe" GOTO INSTALLED
GOTO INSTALL

:INSTALL
ECHO Install SQL 2008
@ECHO ON
powershell.exe [BASEINSTALLDIR]ConfigFiles\sql2008\ReplaceSql2008ConfigurationFile.ps1 [BASEINSTALLDIR]ConfigFiles\sql2008\ReplaceSql2008ConfigurationFile.ini [BASEINSTALLDIR]ConfigFiles\sql2008\sql2008ConfigurationFile.ini [BASEINSTALLDIR]
[BASEINSTALLDIR]sql2008\setup.exe /ConfigurationFile=[BASEINSTALLDIR]ConfigFiles\sql2008\sql2008ConfigurationFile.ini /quiet
@ECHO OFF
GOTO END

:INSTALLED
ECHO SQL 2008 Already installed
:END