#!/usr/bin/env python
#python version 2.4
import sys
import re
import subprocess

def main():
    try:
        i = 10/0
    except:
        print "Error message"
        sys.exit(2)
        print "Error message2"

main()