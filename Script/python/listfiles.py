#!/usr/bin/env python

import sys
import os
import os.path
import string
import glob

def main():
	aBFiles = []
	for file in os.listdir(os.getcwd() + "\\batchfiles"):
		if (file.endswith(".py")):
			aBFiles.append( file)

	print aBFiles
	print glob.glob(os.getcwd() + "\\batchfiles\\*.py")

main()