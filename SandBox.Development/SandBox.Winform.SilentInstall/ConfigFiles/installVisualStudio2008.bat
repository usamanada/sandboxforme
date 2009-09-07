@ECHO OFF
IF EXIST "%ProgramFiles%\Microsoft Visual Studio 9.0\Common7\IDE\devenv.exe" GOTO INSTALLED
GOTO INSTALL

:INSTALL
ECHO Install Visual Studio 2008
@ECHO ON
[BASEINSTALLDIR]vs2008\WCU\msi31\WindowsInstaller-KB893803-v2-x86.exe /quiet /norestart
[BASEINSTALLDIR]vs2008\WCU\dotNetFramework\dotnetfx35setup.exe /q /norestart /lang:ENU
[BASEINSTALLDIR]vs2008\WCU\DExplore\dexplore.exe /q
[BASEINSTALLDIR]vs2008\WCU\WebDesignerCore\WebDesignerCore.exe /Q /install
[BASEINSTALLDIR]vs2008\Setup\setup.exe /q /norestart /unattendfile [BASEINSTALLDIR]ConfigFiles\vs2008\config.ini
@ECHO OFF
GOTO END

:INSTALLED
ECHO Visual Studio 2008 Already installed
:END





