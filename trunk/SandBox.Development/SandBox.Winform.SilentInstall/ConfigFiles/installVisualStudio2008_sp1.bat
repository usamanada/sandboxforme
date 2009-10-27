@ECHO OFF
reg QUERY HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\9.0\VC\DesignTime\x86\VS2008SP1 /v Installed || GOTO INSTALL
GOTO INSTALLED

:INSTALL
ECHO Install Visual Studio 2008 SP1
@ECHO ON
[BASEINSTALLDIR]vs2008_sp1\vs90sp1\SPInstaller.exe /passive /norestart /log [BASEINSTALLDIR]logs\vs_2008log.htm
@ECHO OFF
GOTO END

:INSTALLED
ECHO Visual Studio 2008 SP1 Already installed
:END
