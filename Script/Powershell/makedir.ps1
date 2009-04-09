#------------------------------------------------------------------------------
#Get current directory of executing path
#------------------------------------------------------------------------------
function Get-ScriptPath
{
	Write-Host($myInvocation)
	Write-Host(Get-Location)
	Write-Host([Environment]::CurrentDirectory=(Get-Location -PSProvider FileSystem).ProviderPath)
	if($myInvocation.ScriptName.Length -ne 0)
	{
		Split-Path $myInvocation.ScriptName
	}
	else
	{
		Write-Host("***Warnging this is running Debug mode ***")
		return "c:\temp"
	}
}
#------------------------------------------------------------------------------
function isDir($szPath)
{
	return test-path $szPath -pathtype container
}
#------------------------------------------------------------------------------
function ValidateDir($szPath)
{
	return test-path $szPath -pathtype container -isValid
}
#------------------------------------------------------------------------------
function createDir($szPath)
{
	if(ValidateDir($szPath))
	{
		if($false -eq (isDir($szPath)))
		{
			write-host ("Create Path: " + $szPath)
			New-Item -Path $szPath -type directory
		}
		else
		{
			write-host ("Path already exists: " + $szPath)
		}
	}
	else
	{
		write-host ("Invalid Path: " + $szPath)
	}
}
#------------------------------------------------------------------------------

$szSetupDir = ""
if($args.Count -eq 0)
{
	$szSetupDir = Get-ScriptPath
}
else
{
	if(isDir($args[0]))
	{
		$szSetupDir = $args[0]
	}
	else
	{
		write-host ("Invalid Path passed: " + $args[0])
		return 1
	}
}

createDir(Join-Path -path $szSetupDir "Biztalk\Sandbox\Receive\EncryptedMessage")
createDir(Join-Path -path $szSetupDir "Biztalk\Sandbox\Receive\Message")
createDir(Join-Path -path $szSetupDir "Biztalk\Sandbox\Send\DecrptedMessage")
createDir(Join-Path -path $szSetupDir "Biztalk\Sandbox\Send\EncryptedMessage")
