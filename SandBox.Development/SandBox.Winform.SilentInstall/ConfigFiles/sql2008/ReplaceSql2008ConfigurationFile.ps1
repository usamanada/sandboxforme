function ReplaceWordInFile($filePath, $findWord, $replaceWord, $ReplaceFilePath)
{
	$cont = Get-Content -Path $filePath
	$result = $cont | foreach {$_ -replace $findWord, $replaceWord} 
	Set-Content -Path $ReplaceFilePath -Value $result
}

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
$fullusername = $domain + "\" + $username
#main
$script:baseInstallerDir = "\[BASEINSTALLDIR\]"

Write-Host("Number arguments passed: " + $args.Length)
foreach($arg in $args)
{
	Write-Host("arg: " + $arg)
}

$tempfile = Join-Path ".\" (([Guid]::NewGuid().ToString()) + ".tmp")
ReplaceWordInFile $args[0] $baseInstallerDir $args[2] $tempfile
ReplaceWordInFile $tempfile "ReplaceUsername" $fullusername $args[1]

if(Test-Path $tempfile)
{
	Write-Host("Remove temp file: " + $tempfile)
	Remove-Item $tempfile
}

