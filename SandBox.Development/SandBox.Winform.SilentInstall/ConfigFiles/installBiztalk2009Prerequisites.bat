@ECHO OFF
IF EXIST "%ProgramFiles%\Microsoft SQL Server\90\NotificationServices\9.0.242\bin\NsService.exe" GOTO INSTALLED
GOTO INSTALL

:INSTALL
ECHO Install Biztalk 2009 Prerequisites
@ECHO ON
[BASEINSTALLDIR]Biztalk2009_Prequisites\SQLXML3_sp3\sqlxml.msi /qb
[BASEINSTALLDIR]Biztalk2009_Prequisites\sql_notification.services\sqlncli_Yukon_SP2_CU9_SNAC.msi /qb
[BASEINSTALLDIR]Biztalk2009_Prequisites\sql_notification.services\SQLServer2005_NS.msi /qb
[BASEINSTALLDIR]Biztalk2009_Prequisites\sql_notification.services\SQLServer2005_XMO.msi /qb
[BASEINSTALLDIR]Biztalk2009_Prequisites\sql_notification.services\sqlncli_Yukon_SP2_CU9_SNAC.msi /qb
[BASEINSTALLDIR]Biztalk2009_Prequisites\sql_notification.services\SQLServer2005_NS.msi /qb
@ECHO OFF
GOTO END

:INSTALLED
ECHO Biztalk 2009 Prerequisites Already installed
:END




