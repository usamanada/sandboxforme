$finished = $false
do
{
	$process = Get-Process | where-object{$_.Name -match "uedit32"}
	if($process -ne $null)
	{
		Write-Host ("Process still running: " + $process.Name)
		Start-Sleep -Seconds 1
	}
	else
	{
		Write-Host ("Process not found")
		$finished = $true
	}
}
while(($finished) -eq $false)