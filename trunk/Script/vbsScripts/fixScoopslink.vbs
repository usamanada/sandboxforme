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


szScoopsLink = "\\umela04\scoops\scoops2.lnk"
szDesktop = "D:\Documents and Settings\" & objNetwork.UserName &"\Desktop\"
szStartProgram = "D:\Documents and Settings\" & objNetwork.UserName &"\Start Menu\Programs\scoops2\"

Call CopyFile(szScoopsLink, szDesktop, True)
Call CopyFile(szScoopsLink, szStartProgram, True)


WScript.Echo "Finished."
WScript.Quit