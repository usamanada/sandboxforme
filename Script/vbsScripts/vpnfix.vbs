'---------------------------------------------------------------------------------
'---------------------------------------------------------------------------------
Sub CopyFile(Source, Destination, Clobber)
	If FSO.FileExists(Source) Then
		If not (Clobber = False and FSO.FileExists(Destination) = True) Then
			Call FSO.CopyFile(Source, Destination)
		Else
			WScript.Echo "Copy: Skipped file as it already exists: " & source
		End If
	Else
		WScript.Echo "Copy: Could not find source file: " & source
	End If
End Sub
'---------------------------------------------------------------------------------
'---------------------------------------------------------------------------------

Set objNetwork = WScript.CreateObject("WScript.Network")
Set FSO = CreateObject("Scripting.FileSystemObject")


szVPNDir = "C:\Program Files\Cisco Systems\VPN Client"
szProfilesDir = szVPNDir & "\Profiles"
szRemoveFiles = szProfilesDir & "\*.*"
szLocDir = "\\umelit01\wininst$\Cisco\"
szAllDeskTop = "D:\Documents and Settings\All Users\Desktop\"
'Check VPN dir
If Not FSO.FolderExists( szVPNDir) Then
	WScript.Quit
End If

'Check Profiles Dir
If FSO.FolderExists( szProfilesDir) Then
	FSO.DeleteFile(szRemoveFiles)
Else
	FSO.CreateFolder(szProfilesDir)
End If

Call CopyFile(szLocDir & "\Uecomm Remote Access.pcf", szProfilesDir & "\", True)
Call CopyFile(szLocDir & "\Uecomm Remote Access.lnk", szAllDeskTop & "\", True)

WScript.Quit