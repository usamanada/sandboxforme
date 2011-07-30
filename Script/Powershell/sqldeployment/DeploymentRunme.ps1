function readConfigFile()
{
	$currentDirectory = Get-Location
	$appConfigFile = [IO.Path]::Combine($currentDirectory, 'App.config')
	$appConfig = New-Object XML
	$appConfig.Load($appConfigFile)
	return $appConfig
}
function getConnection
{
	param([string]$connectionName)
	$appConfig = readConfigFile
	$resultFind = findConnection $appConfig $connectionName
	return $resultFind
}
function findConnection
{
	param($appConfig, [string]$connectionName)
	foreach($connectionString in $appConfig.configuration.connectionStrings.add)
	{
		if($connectionName -eq $connectionString.name)
		{
			return $connectionString.connectionString
		}
	}
}

function getServerName
{
	param($appConfig, [string]$connectionName)
}
function runsqlCmd
{
	param([string]$serverName, [string]$database, [string]$inputfile)
	$cmd = "sqlcmd -S " + $serverName + " -d " + $database + " -i """ + $inputfile + """"
	Invoke-Expression $cmd
	write-host ("Run Successfull" + $cmd)
}
function DeploySolomonErpAustralia
{
	genericDeployment "SolomonErpAustralia" "C:\Data\Development\TFS\Regional_APAC\Brightstar.BI.RegionalDistribution\Source\SolomonErp.Australia\Schema Objects\Schemas\dbo\Programmability\Stored Procedures\"
}
function DeploySolomonErpHongKong
{
	genericDeployment "SolomonErpHongKong" "C:\Data\Development\TFS\Regional_APAC\Brightstar.BI.RegionalDistribution\Source\SolomonErp.HongKong\Schema Objects\Schemas\dbo\Programmability\Stored Procedures\"
}
function DeploySolomonErpMalaysia
{
	genericDeployment "SolomonErpMalaysia" "C:\Data\Development\TFS\Regional_APAC\Brightstar.BI.RegionalDistribution\Source\SolomonErp.Malaysia\Schema Objects\Schemas\dbo\Programmability\Stored Procedures\"
}
function DeploySolomonErpNewZealand
{\
	genericDeployment "SolomonErpNewZealand" "C:\Data\Development\TFS\Regional_APAC\Brightstar.BI.RegionalDistribution\SourceSolomonErp.NewZealand\Schema Objects\Schemas\dbo\Programmability\Stored Procedures\"
}
function DeploySolomonErpRouteChannelAustralia
{
	genericDeployment "SolomonErpRouteChannelAustralia" "C:\Data\Development\TFS\Regional_APAC\Brightstar.BI.RegionalDistribution\Source\SolomonErp.RouteChannelAustralia\Schema Objects\Schemas\dbo\Programmability\Stored Procedures\"
}
function DeploySolomonErpSingapore
{
	genericDeployment "SolomonErpSingapore" "C:\Data\Development\TFS\Regional_APAC\Brightstar.BI.RegionalDistribution\Source\SolomonErp.Singapore\Schema Objects\Schemas\dbo\Programmability\Stored Procedures\"
}
function DeploySolomonErpSouthAfrica
{
	genericDeployment "SolomonErpSouthAfrica" "C:\Data\Development\TFS\Regional_APAC\Brightstar.BI.RegionalDistribution\Source\SolomonErp.SouthAfrica\Schema Objects\Schemas\dbo\Programmability\Stored Procedures\"
}

function genericDeployment
{
	param([string]$ErpName, [string]$DirPath)
	write-host ("Deploying " + $ErpName)
	$connectionString = getConnection $ErpName
	$sClient = New-Object System.Data.SqlClient.SqlConnectionStringBuilder $connectionString
	write-host "SQL Server name: " $sClient.DataSource
	write-host "SQL Database name: " $sClient.InitialCatalog
	
	runsqlCmd $sClient.DataSource $sClient.InitialCatalog "C:\Data\Development\TFS\Regional_APAC\Brightstar.BI.RegionalDistribution\Source\DeploymentScript.sql"
	
	runsqlCmd $sClient.DataSource $sClient.InitialCatalog ($DirPath + "XBS_RegionalExtractInventory.proc.sql")
	runsqlCmd $sClient.DataSource $sClient.InitialCatalog ($DirPath + "XBS_RegionalExtractInvoices.proc.sql")
	runsqlCmd $sClient.DataSource $sClient.InitialCatalog ($DirPath + "XBS_RegionalExtractPurchaseOrders.proc.sql")
	runsqlCmd $sClient.DataSource $sClient.InitialCatalog ($DirPath + "XBS_RegionalExtractSalesOrders.proc.sql")
	
	write-host ""
}

$DepolAll = read-host "Deploy to all [Y|N]: "
$DepolAll = $DepolAll.ToUpper()
if($DepolAll -eq "Y")
{
	write-host "Deploying to all."
	DeploySolomonErpAustralia
	DeploySolomonErpHongKong
	DeploySolomonErpMalaysia
	DeploySolomonErpNewZealand
	DeploySolomonErpRouteChannelAustralia
	DeploySolomonErpSingapore
	DeploySolomonErpSouthAfrica
}
elseif($DepolAll -eq "N")
{
	[string]$result = read-host "Deploy to SolomonErp.Australia [Y|N]: "
	$Deploy += $result.ToUpper()
	$result = read-host "Deploy to SolomonErp.HongKong [Y|N]: "
	$Deploy += $result.ToUpper()
	$result = read-host "Deploy to SolomonErp.Malaysia [Y|N]: "
	$Deploy += $result.ToUpper()
	$result = read-host "Deploy to SolomonErp.NewZealand [Y|N]: "
	$Deploy += $result.ToUpper()
	$result = read-host "Deploy to SolomonErp.RouteChannelAustralia [Y|N]: "
	$Deploy += $result.ToUpper()
	$result = read-host "Deploy to SolomonErp.Singapore [Y|N]: "
	$Deploy += $result.ToUpper()
	$result = read-host "Deploy to SolomonErp.SouthAfrica [Y|N]: "
	$Deploy += $result.ToUpper()
	
	if($Deploy[0] -eq "Y")
	{
		DeploySolomonErpAustralia
	}
	if($Deploy[1] -eq "Y")
	{
		DeploySolomonErpHongKong
	}
	if($Deploy[2] -eq "Y")
	{		
		DeploySolomonErpMalaysia
	}
	if($Deploy[3] -eq "Y")
	{
		DeploySolomonErpNewZealand
	}
	if($Deploy[4] -eq "Y")
	{
		DeploySolomonErpRouteChannelAustralia
	}
	if($Deploy[5] -eq "Y")
	{
		DeploySolomonErpSingapore
	}
	if($Deploy[6] -eq "Y")
	{
		DeploySolomonErpSouthAfrica
	}
	
	Remove-Item variable:Deploy
}
else
{
	write-host "Error Invalid Parameter Entered " $DepolAll
}

