'---------------------------------------------------------------------------------
Dim FSO
Dim SOURCE_PATH_BINARY_FILES
Dim TARGET_PATH_DNN
Dim SOURCE_PATH_CODE



Dim dict,name,entry  ' Create some variables.
Set dict = CreateObject("Scripting.Dictionary")
name = InputBox("Enter a name", "Address Book Entry")
While name <> ""
   entry = InputBox("Enter Details - Street, Town, Phone number", "Address Book Entry")
   dict.Add name, entry ' Add key and details.
   name = InputBox("Enter a name","Address Book Entry")
Wend

' Now read back the values
name = InputBox("Enter a name","Address Book Lookup")
While name <> ""
   MsgBox(name & " - " & dict.Item(name))
   name = InputBox("Enter a name","Address Book Lookup")
Wend
WScript.Quit