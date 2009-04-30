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
Write-Host("Number arguments passed: " + $args.Length)
foreach($arg in $args)
{
	Write-Host("arg: " + $arg)
}
ReplaceWordInFile $args[0] "ReplaceUsername" $fullusername $args[1]