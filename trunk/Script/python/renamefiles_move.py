#!/usr/bin/env python

import sys
import os
import os.path
import string
import shutil


def main():
	print "START"
	szWorkingPath = os.getcwd()
	szSAPPath = szWorkingPath + "/SAP"
	szSEPPath = szWorkingPath + "/SEP/"
	print "Processing " + szSAPPath
	aFiles = os.listdir( szSAPPath)
	print aFiles
	for file in aFiles:
		szFile = str(file)
#-------------------------------------------------------------------------------
# Create dirs
#-------------------------------------------------------------------------------
		szNewPath = szSEPPath + szFile[:6]
		print szNewPath
		if(os.path.isdir( szNewPath) == False):
			print "create dir: " + szNewPath
			os.mkdir( szSEPPath + szFile[:6])
#-------------------------------------------------------------------------------
#Copy files
#-------------------------------------------------------------------------------
		szOrgFile = szSAPPath + "/" + szFile
		
		szNewFile = szNewPath + "/" + szFile[13:]
		
		print "copy " + szOrgFile + " " + szNewFile
		shutil.copy2(szOrgFile, szNewFile)
#-------------------------------------------------------------------------------
# Create tag in each dir
#-------------------------------------------------------------------------------
	aDirs = os.listdir( szSEPPath)
	for szDir in aDirs:
		file = open(szSEPPath + "/" + szDir + "/SAP_dim_zzz.tag", 'w')
		file.close()
	
main()