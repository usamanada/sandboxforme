#!/usr/bin/env python

import sys
import os
import os.path
import string

DIR = 'D:\Work\portal2'
Password = 's5nys5ny'

def main():

	listDir( DIR)

def listDir( szDirectory):
	for f in os.listdir(szDirectory):
		szPath = os.path.join(szDirectory, f)
		if os.path.isdir(szPath):
			listDir(szPath)
		if f == 'Root':
			changeRoot(szPath)

def changeRoot( szPath):
	file = open(szPath, 'r')
	szLine = file.readline()
	file.close()
	print szPath
	print szLine

	szaLine = string.split(szLine, ":")
	size = len(szaLine)
	if size == 5:
		szaPassword = string.split( szaLine[3], "@")

		szaPassword[0] = Password
		print szaPassword[0]
		szFinished = ''
		index = 0

		while index < len(szaLine):
			if index == 3:
				szFinished += szaPassword[0] + '@' + szaPassword[1] + ':'
			elif index == 4:
				szFinished += szaLine[index]
			else:
				szFinished += szaLine[index] + ':'
			index += 1

		print szFinished

		file = open(szPath, 'w')
		file.write(szFinished)
		file.close()

	else:
		print '************ Error ***************'


main()
