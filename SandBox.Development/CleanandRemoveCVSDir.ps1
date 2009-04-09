#------------------------------------------------------------------------------
#Get current directory of executing path
#------------------------------------------------------------------------------
function Get-ScriptPath
{
   Split-Path $myInvocation.ScriptName
}
#------------------------------------------------------------------------------
function isFile($szFilePath)
{
	return test-path $szFilePath -pathType Leaf	
}
#------------------------------------------------------------------------------
function isDir($szPath)
{
	return test-path $szPath -pathtype container	
}
#------------------------------------------------------------------------------
function AbsolutePath($szPath)
{
	return [system.io.path]::GetFullPath($szPath)	
}
#------------------------------------------------------------------------------
function delDirectory($szPath)
{
	if(Test-Path $dirPath)
	{
		Remove-Item $szPath -recurse -force
		write-host("Removing directory:" + $szPath)
	}		
}
#------------------------------------------------------------------------------
function delFile($szFilePath)
{
	if(Test-Path $szFilePath)
	{
		Remove-Item $szFilePath -recurse -force
		write-host("deleting File: " + $szFilePath)
	}
}
#------------------------------------------------------------------------------
function CleanFileRules($szDirPath)
{
	if (($szDirPath -eq ($szCurrentDir + '/RECYCLER')) -or ($szDirPath -eq ($szCurrentDir + '/System Volume Information')))
	{
		return
	}
	if(Test-Path $szDirPath)
	{
		$files = (Get-Item $szDirPath -force).GetFiles()	
		foreach($file in $files)
		{
			$Extension = ($file.Extension).ToLower()
	
			$filePath = Join-Path $szDirPath $file
			switch($Extension)
			{						
				".pyc"
				{
					delFile($filePath)
				}
				".db"
				{
					delFile($filePath)
				}			
				".suo"
				{
					delFile($filePath)
				}
			}
			switch($file)
			{						
				"uccmds32.cmf"
				{
					delFile($filePath)
				}			
			}		
		}
	}
}
#------------------------------------------------------------------------------
function CleanPaths($szPath)
{
	if (($szPath -ne ($szCurrentDir + '/RECYCLER')) -or ($szPath -ne ($szCurrentDir + '/System Volume Information')))
	{
		CleanFileRules($szPath)
	}
	
	if(Test-Path $szPath)
	{
		$dirs = (Get-Item $szPath -force).GetDirectories()	
		
		foreach($dir in $dirs)
		{
			$dirPath = Join-Path $szPath $dir
			if(Test-Path $dirPath)
			{
				switch($dir.ToString().ToLower())
				{
					"bin"
					{
						delDirectory($dirPath)
						continue
					}
					"obj"
					{
						delDirectory($dirPath)
						continue
					}
					"debug"
					{
						delDirectory($dirPath)
						continue
					}
					"test"
					{
						delDirectory($dirPath)
						continue
					}
					"prerelease"
					{
						delDirectory($dirPath)
						continue
					}
					"release"
					{
						delDirectory($dirPath)
						continue
					}
					"cvs"
					{
						delDirectory($dirPath)
						continue
					}			
				}
				CleanPaths($dirPath)
			}
		}
	}
}
#------------------------------------------------------------------------------			
#Main
$szCurrentDir = Get-ScriptPath

write-host $szCurrentDir

CleanPaths($szCurrentDir)
