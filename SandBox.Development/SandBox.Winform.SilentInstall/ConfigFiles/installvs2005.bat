[BASEINSTALLDIR]vs2005\wcu\msi31\WindowsInstaller-KB893803-v2-x86.exe /quiet /norestart
[BASEINSTALLDIR]vs2005\wcu\DExplore\DExplore.exe /q:a /c:"install.exe /q" 
start /wait [BASEINSTALLDIR]vs2005\wcu\JSharpRedistCore\vjredist.exe /Q:A /C:"INSTALL.exe /Q"
start /wait [BASEINSTALLDIR]vs2005\setup\setup.exe /unattendfile [BASEINSTALLDIR]configfiles\vs2005\vs2005_unattended.ini




