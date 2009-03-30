import zipfile
import sys
import re
import os
import subprocess

file = zipfile.ZipFile("UECustomerContacts.zip", "r")
print file.namelist()

# list filenames
for name in file.namelist():
    print name,
print

# list file information
for info in file.infolist():
    print info.filename, info.date_time, info.file_size