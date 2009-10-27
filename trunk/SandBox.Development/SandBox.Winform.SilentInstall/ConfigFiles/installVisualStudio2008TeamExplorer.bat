@ECHO OFF
IF EXIST "%ProgramFiles%\Microsoft Visual Studio 9.0\Microsoft Visual Studio 2008 Team Explorer - ENU\vs_setup.msi" GOTO INSTALLED
GOTO INSTALL

:INSTALL
ECHO Install Visual Studio 2008 Team Explorer
@ECHO ON
[BASEINSTALLDIR]vs2008_TeamExplorer\setup.exe /q /norestart /unattendfile [BASEINSTALLDIR]ConfigFiles\vs2008TeamExplorer\config.ini
@ECHO OFF
GOTO END

:INSTALLED
ECHO Visual Studio 2008 Team Explorer Already installed
:END


