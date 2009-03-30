#!/usr/bin/env python
#python version 2.4
import sys
import re
import subprocess

def main():
	#Each command line argument passed to the program will be in sys.argv, which is just a list. Here we are printing each argument on a separate line.
	for arg in sys.argv:
		print arg

main()