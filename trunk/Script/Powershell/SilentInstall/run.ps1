function Set-Color([string] $color)
{
    if ($color -eq "")
	{
		$color = $myDefaultColor
	}
	$Host.UI.RawUI.ForeGroundColor = $color
}
function ConvertTo-PlainText( [Security.SecureString]$secure )
{
	$marshal = [Runtime.InteropServices.Marshal]
	$marshal::PtrToStringAuto( $marshal::SecureStringToBSTR($secure) )
}
function Get-ScriptPath
{
	$result = ""
	if($MyInvocation.ScriptName.Length -ne 0)
	{
		$result = Split-Path $MyInvocation.ScriptName
	}
	else
	{
		Write-Host("Debug Code")
		$result = "c:\install"
	}
   	return $result	
}
function Get-ScriptQualifier
{
	$result = ""
	if($MyInvocation.ScriptName.Length -ne 0)
	{
		$result = Split-Path $MyInvocation.ScriptName -Qualifier
		
	}
	else
	{
		Write-Host("Debug Code")
		$result = "c:\"
	}
   	return $result	

}
function Get-ScriptFullName
{
	if($MyInvocation.ScriptName.Length -ne 0)
	{
		$result = Get-Item $MyInvocation.ScriptName
		return $result.Name
	}
	else
	{
		Write-Host("Debug Code")
		return "run.ps1"
	}	
}
function Get-ScriptName
{
	if($MyInvocation.ScriptName.Length -ne 0)
	{
		$file = Get-Item $MyInvocation.ScriptName
		return $file.Name.SubString(0, $file.Name.Length - $file.Extension.Length)
	}
	else
	{
		return "run"
	}
}
function setupAutoLogin
{
	$username = $env:USERNAME
	$domain = ""
	if($Env:USERDOMAIN.length -eq 0)
	{
		$domain = $Env:COMPUTERNAME
	}
	else
	{
		$domain = $Env:USERDOMAIN
	}
	
	Write-Host ("The install process requires reboot." + $newline)
	Set-Color "blue"
	Write-Host ("The Current user is: " + $domain + "\" + $username + $newline)
	Set-Color $ForegroundColor
	Set-Color "green"
	$password = Read-Host ("Enter password") -AsSecureString 
	Set-Color $ForegroundColor
	
	if(Test-Path $regPath)
	{
		saveExistingAutoLoginToConfig
		
		$regKey = get-itemproperty -Path $regPath
		
		if($regKey.AutoAdminLogon -ne $null)
		{
			if($regKey.AutoAdminLogon -ne "1")
			{
				Set-ItemProperty -Path $regPath -Name "AutoAdminLogon" -Value "1"
			}		
		}
		else
		{
			Set-ItemProperty -Path $regPath -Name "AutoAdminLogon" -Value "1"
		}
		
		if($regKey.DefaultUserName -ne $null)
		{
			if($regKey.DefaultUserName -ne $username)
			{
				Set-ItemProperty -Path $regPath -Name "DefaultUserName" -Value $username
			}		
		}
		else
		{
			Set-ItemProperty -Path $regPath -Name "DefaultUserName" -Value $username
		}
		
		$tempPassword = ConvertTo-PlainText($password)
		if($regKey.DefaultPassword -ne $null)
		{
			if($regKey.DefaultPassword -ne $tempPassword)
			{
				Set-ItemProperty -Path $regPath -Name "DefaultPassword" -Value $tempPassword
			}		
		}
		else
		{		
			Set-ItemProperty -Path $regPath -Name "DefaultPassword" -Value $tempPassword
		}
		
		if($regKey.DefaultDomainName -ne $null)
		{
			if($regKey.DefaultDomainName -ne $domain)
			{
				Set-ItemProperty -Path $regPath -Name "DefaultDomainName" -Value $domain 
			}		
		}
		else
		{
			Set-ItemProperty -Path $regPath -Name "DefaultDomainName" -Value $domain
		}
	}
	else
	{
		Write-Host ("Registry Path not found: " + $regPath + $newline)
		return $false
	}
	return $true
	
}
function copyContinueToStartup
{
	$currentPath = Get-ScriptPath
	$continuePath = Join-Path $currentPath $continueBatFileName
	
	Copy-Item  $continuePath $startupDir
	
	if(Test-Path $startupContinuebat)
	{
		Write-Host("copied " + $continueBatFileName + " to "+ $startupDir + $newline)
	}
	else
	{
		Write-Host("copied" + $continueBatFileName + " failed." + $newline)
	}
}
function readConfig
{
	$scriptPath = Get-ScriptPath
	$scriptName = Get-ScriptName
	$configFile = ( Join-Path $scriptPath $scriptName) + ".config"
	
	
	Write-Host ("location of config file: " + $configFile + $newline)
	
	$config = [xml](get-content $configFile)
	
	foreach ($addNode in $config.configuration.InstallApplications.add)
	{
		$InstallApplications[$addNode.Key] = $addNode.value
	}
	
	foreach ($addNode in $config.configuration.OrderToInstall.add)
	{
		$OrderToInstall[$addNode.Key] = $addNode.value
	}
}
function readWorkConfig
{
	Write-Host ("location of working config file: " + $workingConfigFile + $newline)
	
	$OrderID = ""
	if(Test-Path $workingConfigFile)
	{
		$workconfig = [xml](get-content $workingConfigFile)
		$OrderID = $workconfig.Work.Order
	}
	else
	{
		$OrderID = "0"
		updateWorkConfigOrder($OrderID)
	}
	return $OrderID
}
function saveExistingAutoLoginToConfig
{
	$regKey = get-itemproperty -Path $regPath
	$xml
	if((Test-Path $workingConfigFile ) -eq $false)
	{
		$xml = New-Object xml
		$root = $xml.CreateElement("Work")
		$xml.AppendChild($root)
		$xml.save("c:\test.config")
	}
	
	$xml = [xml](Get-Content $workingConfigFile)
	
	if($regKey.AutoAdminLogon -ne $null)
	{
		if($xml.Work.AutoAdminLogon -eq $null)
		{
			$node = $xml.CreateElement("AutoAdminLogon")
			$root = $xml.get_FirstChild()
			$root.AppendChild($node)
		}
		$xml.Work.AutoAdminLogon = $regKey.AutoAdminLogon
	}
	
	if($regKey.DefaultUserName -ne $null)
	{
		if($xml.Work.DefaultUserName -eq $null)
		{
			$node = $xml.CreateElement("DefaultUserName")
			$root = $xml.get_FirstChild()
			$root.AppendChild($node)
		}
		$xml.Work.DefaultUserName = $regKey.DefaultUserName
	}
	
	if($regKey.DefaultPassword -ne $null)
	{
		if($xml.Work.DefaultPassword -eq $null)
		{
			$node = $xml.CreateElement("DefaultPassword")
			$root = $xml.get_FirstChild()
			$root.AppendChild($node)
		}
		$xml.Work.DefaultPassword = $regKey.DefaultPassword
	}
	
	if($regKey.DefaultDomainName -ne $null)
	{
		if($xml.DefaultDomainName -eq $null)
		{
			$node = $xml.CreateElement("DefaultDomainName")
			$root = $xml.get_FirstChild()
			$root.AppendChild($node)
		}
		$xml.Work.DefaultDomainName = $regKey.DefaultDomainName
	}
	$xml.save($workingConfigFile)	
}
function updateWorkConfigOrder([string] $sequenceID)
{
	$xml
	if((Test-Path $workingConfigFile ) -eq $false)
	{
		$xml = New-Object xml
		$root = $xml.CreateElement("Work")	
		$xml.AppendChild($root)
		$order = $xml.CreateElement("Order")
		$order.PSBase.InnerText = $sequenceID
		$root.AppendChild($order)
	}
	else
	{
		$xml = [xml](Get-Content $workingConfigFile)
		if($xml.Work.Order -ne $null)
		{
			$xml.Work.Order = $sequenceID
		}
		else
		{
			$order = $xml.CreateElement("Order")
			$order.PSBase.InnerText = $sequenceID
			$root = $xml.get_FirstChild()
			$root.AppendChild($order)
		}
	}
	$xml.save($workingConfigFile)
	return $true
}
function initProcess
{
	Set-Color "blue"
	Write-Host("Current computer name is: " + $Env:COMPUTERNAME + $newline)
	Set-Color $ForegroundColor
	
	$invalidSelection = $true
	
	do
	{	
		Set-Color "green"
		$result = Read-Host("Do you want to continue?(Y/N)")
		Set-Color $ForegroundColor
	
		if($result.ToLower() -eq "n")
		{
			Write-Host("initProcess.continue = false" + $newline)
			return $false
		}
		elseif($result.ToLower() -eq "y")
		{
			$invalidSelection = $false
		}
		else
		{
			Write-Host("Invalid selection: " + $result)
		}
	}
	while($invalidSelection -eq $true)
	
	
	if((setupAutoLogin) -eq $false)
	{
		Write-Host("initProcess.setupAutoLogin - false" + $newline)		
		return $false
	}
	
	copyFiles
	
	write-Host ("initProcess complete" + $newline)
	return $true
}
function ReplaceWordInFile($filePath, $findWord, $replaceWord, $ReplaceFilePath)
{
	$cont = Get-Content -Path $filePath
	$result = $cont | foreach {$_ -replace $findWord, $replaceWord} 
	Set-Content -Path $ReplaceFilePath -Value $result
}
function copyFiles
{
	$configDir = Join-Path (Get-ScriptPath) "ConfigFiles"
	
	$qualifier = Get-ScriptQualifier
	
	if(Test-Path $configDir)
	{
		Write-Host ("Config Dir: " + $configDir)
		$files = (Get-Item $configDir -Force).GetFiles()
		$tempfile = Join-Path $configDir (([Guid]::NewGuid().ToString()) + ".tmp")
		Write-Host ("Temp file: " + $tempfile)
		foreach($file in $files)
		{
			Write-Host ("File: " + $file)
			$fileCopyTo = Join-Path (Get-ScriptPath) $file.Name
			Write-Host ("Copy to: " + $fileCopyTo)
			[string] $drive = Get-ScriptQualifier
			$drive = $drive.Replace("\", "")
			Write-Host ("drive: " + $drive)
			
			[string] $scritpath = Get-ScriptPath
			if($scritpath.EndsWith("\") -eq $false)
			{
				$scritpath = $scritpath + "\"
			}
			Write-Host ("installcation: " + $scritpath)
			
			ReplaceWordInFile $file.FullName $workingDrive $drive $tempfile
			ReplaceWordInFile $tempfile $baseInstallerDir $scritpath $fileCopyTo
		}
		
		if(Test-Path $tempfile)
		{
			Write-Host("Remove temp file: " + $tempfile)
			Remove-Item $tempfile
		}
		copyContinueToStartup
	}
	else
	{
		Write-Host ("Config Dir not found: " + $configDir)
		return $false
	}
	return $true

}
function doWork([string] $OrderID)
{
	$iOrderID = [System.Convert]::ToInt32($OrderID)
	
	$iTotalSequenceInstall = $OrderToInstall.Keys.Count
	while($iOrderID -lt $iTotalSequenceInstall)
	{
		$iOrderID += 1
		$CurrentOrderID = $iOrderID.ToString()
		Write-Host ("Install OrderID: " + $OrderID + $newline)
		Write-Host (Get-Date)
		$result = updateWorkConfigOrder($CurrentOrderID)
		
		$application = $OrderToInstall[$CurrentOrderID]
		if($application -eq $null)
		{
			Write-Error ("Error in Run.config file Install Order: " + $CurrentOrderID)
			Read-Host("Press key to continue")
			exit
		}
		if( $application.ToLower() -eq "reboot")
		{			
			$rebootPath = Join-Path (Get-ScriptPath) "reboot.bat"
			Write-Host("Rebooting" + $newline)
			Write-Host("calling: " + $rebootPath + $newline)
			
			$p = [Diagnostics.Process]::Start($rebootPath)
			$p.WaitForExit()
			return $false

		}
		elseif($application.ToLower() -eq "sleep")
		{
			Start-Sleep -Seconds 20
		}
		else
		{
			Write-Host ("Installing application: " + $application + $newline)
			$batchfile = $InstallApplications[$application]
			Write-Host ("Running batchfile: " + $batchfile + $newline)
			$p = [Diagnostics.Process]::Start($batchfile)
			$p.WaitForExit()
		}
	}
	Write-Host ("Install Complete" + $newline)
	return $true
}
function cleanup
{
	if(Test-Path $startupContinuebat)
	{
		Write-Host("Removing startup " + $continueBatFileName + $newline)
		Remove-Item -Path $startupContinuebat
	}
	
	
	if(Test-Path $regPath)
	{
		$workconfig = [xml](get-content $workingConfigFile)
		
		$regKey = get-itemproperty -Path $regPath
		
		if($regKey.AutoAdminLogon -ne $null)
		{
			if($workconfig.Work.AutoAdminLogon -ne $null)
			{
				Write-Host("Restoring reg key: " + $regPath + ".AutoAdminLogon" + $newline)
				Set-ItemProperty -Path $regPath -Name "AutoAdminLogon" -Value $workconfig.Work.AutoAdminLogon
			}
			else
			{
				Write-Host("Removing reg key: " + $regPath + ".AutoAdminLogon" + $newline)
				Remove-ItemProperty -Path $regPath -Name "AutoAdminLogon"
			}
		}
		
		if($regKey.DefaultUserName -ne $null)
		{
			if($workconfig.Work.DefaultUserName -ne $null)
			{
				Write-Host("Restoring reg key: " + $regPath + ".DefaultUserName" + $newline)
				Set-ItemProperty -Path $regPath -Name "DefaultUserName" -Value $workconfig.Work.DefaultUserName
			}
			else
			{
				Write-Host("Removing reg key: " + $regPath + ".DefaultUserName" + $newline)
				Remove-ItemProperty -Path $regPath -Name "DefaultUserName"
			}
		}
		if($regKey.DefaultPassword -ne $null)
		{
			if($workconfig.Work.DefaultPassword -ne $null)
			{
				Write-Host("Restoring reg key: " + $regPath + ".DefaultPassword" + $newline)
				Set-ItemProperty -Path $regPath -Name "DefaultPassword" -Value $workconfig.Work.DefaultPassword
			}
			else
			{
				Write-Host("Removing reg key: " + $regPath + ".DefaultPassword" + $newline)
				Remove-ItemProperty -Path $regPath -Name "DefaultPassword"
			}
		}
		
		if($regKey.DefaultDomainName -ne $username)
		{
			if($workconfig.Work.DefaultDomainName -ne $null)
			{
				Write-Host("Restoring reg key: " + $regPath + ".DefaultDomainName" + $newline)
				Set-ItemProperty -Path $regPath -Name "DefaultDomainName" -Value $workconfig.Work.DefaultDomainName
			}
			else
			{
				Write-Host("Removing reg key: " + $regPath + ".DefaultDomainName" + $newline)
				Remove-ItemProperty -Path $regPath -Name "DefaultDomainName"
			}
		}
	}
	if(Test-Path $workingConfigFile)
	{
		Write-Host("Removing working.config" + $newline)
		Remove-Item -Path $workingConfigFile
	}
}
function startLogging()
{
	if($MyInvocation.ScriptName.Length -ne 0)
	{
		$scriptlogPath = Join-Path (Get-ScriptPath) ((Get-ScriptName) + "bat.log")
		Write-Host("Log file: " + $scriptlogPath)
		Start-Transcript -Path $scriptlogPath -Append
	}
}
function stopLogging()
{
	if($MyInvocation.ScriptName.Length -ne 0)
	{
		Stop-Transcript
	}
}
#-------------------------------------------------------------------------------
#main
#-------------------------------------------------------------------------------
#Global Variables
$script:regPath = "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\"
$script:continueBatFileName = "continue.bat"
$script:workingDrive = "\[WORKINGDRIVE\]"
$script:baseInstallerDir = "\[BASEINSTALLDIR\]"
$script:newline = [Environment]::NewLine
$script:InstallApplications = @{}
$script:OrderToInstall = @{}
$script:workingConfigFileName = "working.config"
Write-Host($workingConfigFileName)
$script:workingConfigFile = Join-Path (Get-ScriptPath) $workingConfigFileName
$script:startupDir = [System.Environment]::GetFolderPath("Startup")
$startupContinuebat = Join-Path $startupDir $continueBatFileName
$script:ForegroundColor = [Console]::ForegroundColor

#-------------------------------------------------------------------------------
startLogging

$OrderID = readWorkConfig

Write-Host ("Order ID: " + $OrderID)
if($OrderID -eq "0")
{	
	$result = copyFiles
	
	if((initProcess) -eq $false)
	{
		Write-Host("initProcess - false" + $newline)
		stopLogging
		return
	}
}
readConfig

$result = doWork($OrderID)
Write-Host("Do Work result: " + $result + $newline)
if( $result -eq $false)
{
	Write-Host("Do Work - false" + $newline)
	stopLogging
	return
}
cleanup
stopLogging