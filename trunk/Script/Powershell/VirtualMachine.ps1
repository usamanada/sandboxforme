#Get current directory of executing path
function Get-ScriptPath
{
   Split-Path $myInvocation.ScriptName
}

#Setup Varialbes
$currentDir = Get-ScriptPath
$match = "{CURRENT_DIRECTORY}"
$replacement = $currentDir
$filename = "LauncherSettings.config"
$configDir = "ConfigFiles"

#get list of config directories
$coarses =  Get-ChildItem $configDir | where{$_.mode -match "d"}

foreach($coarse in $coarses)
{
   $configPath = "{0}\{1}\{2}\{3}" -f $currentDir, $configDir, $coarse, $filename

   $content = Get-Content $configPath
   $content = $content -creplace $match, $replacement
   
   $ReplaceConfigPath = "{0}\{1}\Data\{2}" -f $currentDir, $coarse, $filename
   
   $content | Set-Content $ReplaceConfigPath
   $DesktopDir = [Environment]::GetFolderPath("Desktop")
   
   $shortcut_name = [Environment]::GetFolderPath("Desktop") + "\" + $coarse + ".lnk"
   $shortcut_target = "I:\Virtual Machines\MS_Learning\6461\Data\LauncherApplication.exe" 
   $sh = new-object -com "WScript.Shell" 
   $lnk = $sh.CreateShortcut( $shortcut_name)  
   $lnk.TargetPath = $shortcut_target
   $lnk.Save()
}