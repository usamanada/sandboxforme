#!/usr/bin/env python

import sys
import os
import os.path
import string


def main():
	
	if len(sys.argv) != 4:
		print "Paramateres rename.py [directory] [extension] [replace extension]"
		return
	
	
	szWorkingPath = os.path.abspath(sys.argv[1]) + "\\" 

	print szWorkingPath
	
	aFiles = os.listdir( szWorkingPath)
	for file in aFiles:
		if (file.endswith(sys.argv[2])):
			szFile = str(file)
			
			szOrgFile = szWorkingPath + file
			print szOrgFile
			
			szNewFile = szWorkingPath + szFile[:-3] + sys.argv[3]
				
			print szNewFile 

			os.rename(szOrgFile, szNewFile)
				
main()