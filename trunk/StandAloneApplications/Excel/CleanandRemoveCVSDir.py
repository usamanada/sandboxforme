#!/usr/bin/env python
#Used to help remove complied objects so that it is easier to commit code.
import sys
import os
import os.path
import glob
import shutil
#------------------------------------------------------------------------------
def delDirectory(szPath):
	if (szPath != 'RECYCLER' or szPath != 'System Volume Information'):
		shutil.rmtree(szPath)
		print ("Removing directory: " + szPath)
#------------------------------------------------------------------------------
def removeFileRules(szDirPath):
	if (szDirPath != gszPath + '/RECYCLER'):
		return
	if (gszPath + '/System Volume Information'):
		return
	for file in os.listdir(szDirPath):
		if (file.endswith(".pyc")):
			print ("Removing file: " + szDirPath + "\\" + file)
			os.remove( szDirPath + "\\" + file)
		elif (file.endswith(".db")):
			print ("Removing file: " + szDirPath + "\\" + file)
			os.remove( szDirPath + "\\" + file)
		elif (file.lower() == "uccmds32.cmf"):
			print ("Removing file: " + szDirPath + "\\" + file)
			os.remove( szDirPath + "\\" + file)
		elif (file.endswith(".suo")):
			print ("Removing file: " + szDirPath + "\\" + file)
			os.remove( szDirPath + "\\" + file)
#------------------------------------------------------------------------------
def findPathRec(szPath):
	if (szPath != szPath + '/RECYCLER' or szPath != szPath + '/System Volume Information'):
		removeFileRules(szPath)
	
	for name in glob.glob(szPath + "/*"):
		if os.path.isdir(name):
			adirectoryName = os.path.split(name)
			szDirNameLW = adirectoryName[1].lower()
			print (name)
			if szDirNameLW == "bin":
				delDirectory(name)
				continue
			elif szDirNameLW == "obj":
				delDirectory(name)
				continue
			elif szDirNameLW == "debug":
				delDirectory(name)
				continue
			elif szDirNameLW == "test":
				delDirectory(name)
				continue
			elif szDirNameLW == "prerelease":
				delDirectory(name)
				continue
			elif szDirNameLW == "release":
				delDirectory(name)
				continue
			elif szDirNameLW == "cvs":
				print ('Deleting CVS')
				delDirectory(name)
				continue
						
			findPathRec(name)
			
#------------------------------------------------------------------------------
def main():
	global gszPath
	gszPath = os.getcwd()
	findPathRec(os.getcwd())
#------------------------------------------------------------------------------
main()