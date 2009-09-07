function ReplaceWordInFile($filePath, $findWord, $replaceWord, $ReplaceFilePath)
{
	$cont = Get-Content -Path $filePath
	$result = $cont | foreach {$_ -replace $findWord, $replaceWord} 
	Set-Content -Path $ReplaceFilePath -Value $result
}

#main

Write-Host("Number arguments passed: " + $args.Length)
foreach($arg in $args)
{
	Write-Host("arg: " + $arg)
}

$script:username = $args[3]
$script:domain = $args[4]
$script:password = $args[5]
$script:servername = $args[6]
$script:baseInstallerDir = "\[BASEINSTALLDIR\]"


$tempfile = Join-Path ".\" (([Guid]::NewGuid().ToString()) + ".tmp")
ReplaceWordInFile $args[0] $baseInstallerDir $args[2] $tempfile
ReplaceWordInFile $tempfile "\[DOMAIN\]" $domain $tempfile
ReplaceWordInFile $tempfile "\[USERNAME\]" $username $tempfile
ReplaceWordInFile $tempfile "\[PASSWORD\]" $password $tempfile
ReplaceWordInFile $tempfile "\[SERVERNAME\]" $servername $args[1]

if(Test-Path $tempfile)
{
	Write-Host("Remove temp file: " + $tempfile)
	Remove-Item $tempfile
}

