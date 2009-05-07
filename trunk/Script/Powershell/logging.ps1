Start-Transcript -Path "c:\run.log"
Start-Sleep -Seconds 3
$newline = [Environment]::NewLine
Write-Host ("Testing string" + $newline)
Write-Host ("Testing string1" + $newline)
Write-Host ("Testing string2" + $newline)

Stop-Transcript