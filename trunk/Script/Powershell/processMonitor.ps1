function windowsServices()
{
	checkWindowsServiceIsRunning("slsvc")
	checkWindowsServiceIsRunning("WerSvc")
	checkWindowsServiceIsRunning("EventLog")
	checkWindowsServiceIsRunning("Winmgmt")
	checkWindowsServiceIsRunning("TrustedInstaller")
}
function checkWindowsServiceIsRunning($name)
{
	$s = Get-Service -name $name
	while($s.Status -ne "Running")
	{
		Write-Host ($s.DisplayName + " is not running" + $newline)
		Start-Sleep -Seconds 5
		$s = Get-Service -name $name
	}
	Write-Host ($s.DisplayName + " is running" + $newline)
}

windowsServices