#!/usr/bin/env python
#python version 2.4
import sys
import getopt
import re
import subprocess
import os
import string
import shutil

#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def replaceWordInFile(szFileName, szReplaceWord, szWithWord):
	szCatCommand = os.path.abspath("c:/cygwin/bin/cat.exe")
	szSedCommand = os.path.abspath("c:/cygwin/bin/sed.exe")
	szunix2dosCommand = os.path.abspath("c:/cygwin/bin/unix2dos.exe")
	
	szTemFile = os.getcwd() + "/" + szWithWord + ".tmp"
	# Replace Word
	szProcess = szCatCommand + " " + os.path.abspath(szFileName) + " | " + szSedCommand + " -e \"s/" + szReplaceWord + "/" + szWithWord + "/g\" > " + szTemFile
	subprocess.call(szProcess, shell=True)
	# Convert from unix 2 dos
	szProcess = szunix2dosCommand + " " + szTemFile
	subprocess.call(szProcess, shell=True)
	#Return newly created file
	return szTemFile
	
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def main():
	os.system("cls")
	print replaceWordInFile( os.path.abspath(os.getcwd()) + "/test.txt", "CHANGESERVER", "UMELDD03")
	
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
main()
