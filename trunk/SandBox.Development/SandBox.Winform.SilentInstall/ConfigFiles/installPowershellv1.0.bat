@ECHO OFF
IF EXIST %systemroot%\System32\WindowsPowerShell\v1.0\powershell.exe GOTO INSTALLED
GOTO INSTALL

:INSTALL
ECHO Install Power Shell v1.0
@ECHO ON
servermanagercmd -i powershell
@ECHO OFF
GOTO END

:INSTALLED
ECHO Power Shell v1.0 Already installed
:END
