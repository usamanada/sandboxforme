#!/usr/bin/env python

import sys
import os
import os.path

def main():
	DIR = 'D:\scoops2b'
	listDir( DIR)

def listDir( szDirectory):
	for f in os.listdir(szDirectory):
		szPath = os.path.join(szDirectory, f)
		if os.path.isdir(szPath):
			listDir(szPath)
			print szPath


main()