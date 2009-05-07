$p = [Diagnostics.Process]::Start("notepad.exe")
$p.WaitForExit()
Write-Host("notepad.exe finished")