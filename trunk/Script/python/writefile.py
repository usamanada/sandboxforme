#!/usr/bin/env python

import sys
import os
import os.path
import string

def main():

	file = open('test.txt', 'r')
	szLine = file.readline()
	print szLine
	file.close()
	szaLine = string.split(szLine, ":")
	print len(szaLine)
	#file = open('test.txt', 'w')
	#file.write('text changed')
	#file.close()

main()