$shortcut_name = [Environment]::GetFolderPath("Desktop") + "\" + "6461.lnk"
if(!(test-path $shortcut_name))
{
   $shortcut_target = "I:\Virtual Machines\MS_Learning\6461\Data\LauncherApplication.exe" 
   $sh = new-object -com "WScript.Shell" 
   $lnk = $sh.CreateShortcut( $shortcut_name)  
   $lnk.TargetPath = $shortcut_target
   $lnk.Save()
}
