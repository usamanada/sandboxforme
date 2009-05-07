function readConfig
{
	$configFile = "C:\DATA\source\Script\Powershell\SilentInstall\run.config"
		
	Write-Host ("location of config file: " + $configFile + $newline)
	
	$config = [xml](get-content $configFile)
	
	foreach ($addNode in $config.configuration.InstallApplications.add)
	{
		$InstallApplications[$addNode.Key] = $addNode.value
	}
	
	foreach ($addNode in $config.configuration.OrderToInstall.add)
	{
		Write-Host ("valuename: " + $addNode.value)
		$data = "" | Select value,duration
		$data.value = $addNode.value
		if($addNode.value.ToString().ToLower() -eq "sleep")
		{
			Write-Host ("addNode.Key: " + $addNode.Key)
			Write-Host ("Sleep found, duration: " + $addNode.duration)
			$data.duration = $addNode.duration
		}
		
		$OrderToInstall[$addNode.Key] = $data		
	}
	$result = $OrderToInstall["5"]
	Write-Host ($result.value)
	Write-Host ($result.duration)
}

#------------------------------------------------------------------------------
$script:InstallApplications = @{}
$script:OrderToInstall = @{}
#------------------------------------------------------------------------------
readConfig