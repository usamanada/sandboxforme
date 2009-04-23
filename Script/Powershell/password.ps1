function GetSecurePass ($SecurePassword)
{
$Ptr = [System.Runtime.InteropServices.Marshal]::SecureStringToCoTaskMemUnicode($SecurePassword)
$pwd = [System.Runtime.InteropServices.Marshal]::PtrToStringUni($Ptr)
[System.Runtime.InteropServices.Marshal]::ZeroFreeCoTaskMemUnicode($Ptr)
$pwd
}
Write-Host("This script requires auto login.")
$username = $env:USERNAME
$domain = ""
if($env:USERDOMAIN.Length -eq 0)
{
	$domain = $env:USERDOMAIN
}
else
{
	$domain = $env:COMPUTERNAME
}

Write-Host("Currnet user is: " + $domain + "\" + $env:username)
$password = Read-Host "Enter Password: " -AsSecureString

Write-Host(GetSecurePass $password)
