@ECHO OFF
C:\Windows\System32\inetsrv\appcmd list site /name:"SharePoint Central Administration v3" || GOTO INSTALL

GOTO INSTALLED

:INSTALL
ECHO Install SharePoint Central Administration
@ECHO ON

set bin="%COMMONPROGRAMFILES%\Microsoft shared\Web server extensions\12\Bin"
set server=[SERVERNAME]
set password=[PASSWORD]
set domainusername=[DOMAIN]\[USERNAME]


%bin%\psconfig -cmd configdb -create -server "%server%" -database "WSS_Config" -user "%username%" -password "%password%" -admincontentdatabase "SharePoint_AdminContent_Wss"
%bin%\psconfig -cmd helpcollections -installall    
%bin%\psconfig -cmd secureresources
%bin%\psconfig -cmd services install
%bin%\stsadm -o spsearch -action start -farmserviceaccount "%domainusername%" -farmservicepassword "%password%"
%bin%\psconfig -cmd services –provision 
%bin%\psconfig -cmd installfeatures
%bin%\psconfig -cmd adminvs -provision -port "10000" -windowsauthprovider onlyusentlm
%bin%\psconfig -cmd applicationcontent install
@ECHO OFF
GOTO END

:INSTALLED
ECHO SharePoint Central Administration Already installed
:END

