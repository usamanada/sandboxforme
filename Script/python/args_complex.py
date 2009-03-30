#!/usr/bin/env python
#python version 2.4
import sys
import getopt
import re
import subprocess


def Usage():
	print "args_complex.py [-f][-h][-v]"
	sys.exit(0)

def main():
	try:
		optlist, list = getopt.getopt(sys.argv[1:],':fhv')
		print "optlist =", optlist
		print "list =", list
	except getopt.GetoptError:
		Usage()
		print "called exception"
		sys.exit(2)

	for opt in optlist:
		print opt
		if opt[0] == '-h':
			Usage()
		if opt[0] == '-v':
			print "verbose found"
		if opt[0] == '-p':
			print "probeonly found"


main()