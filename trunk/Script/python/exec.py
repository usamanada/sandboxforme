#!/usr/bin/env python
#python version 2.4
import sys
import re
import subprocess

def main():
	#subprocess.call sits and waits for the call to be finished
	#Case sensitive
	sts = subprocess.call("C:\\Program Files\\GNU\\WinCvs 1.3\\wincvs.exe")
	sts = subprocess.call('explorer')
	sts = subprocess.call('echo hello world', shell=True)
	sts = subprocess.call(["dir", "c:\\temp"], shell=True)
	sts = subprocess.call("dir c:\\temp", shell=True)

main()