import os
import stat
import time
import shutil
import ConfigParser
import sys
import datetime
#------------------------------------------------------------------------------
def moveFile(szFilePath, szFile):
	fileStats = os.stat( szFilePath)
	fileTimeStamp = datetime.datetime.fromtimestamp(fileStats [ stat.ST_CTIME ])
	
	szYearDir = szgDestinationPath + "/" + str(fileTimeStamp.year)
	szMonthDir = szYearDir + "/" + str.zfill(str(fileTimeStamp.month),2)
	
	FiveDaysOld = datetime.timedelta(days=-5)
	
#------------------------------------------------------------------------------
def main():
	#moveFile()
	print "Today is day", time.localtime()[7], "of the current year" 
	# Today is day 218 of the current year
	today = datetime.date.today()
	print "Today is day", today.timetuple()[7], "of ", today.year
	# Today is day 218 of 2003

	print "Today is day", today.strftime("%j"), "of the current year" 
	# Today is day 218 of the current year
	
	today = datetime.date.today()
	print "The date is", today 
	#=> The date is 2003-08-06

	# the function strftime() (string-format time) produces nice formatting
	# All codes are detailed at http://www.python.org/doc/current/lib/module-time.html
	print time.strftime("four-digit year: %Y, two-digit year: %y, month: %m, day: %d") 
	#=> four-digit year: 2003, two-digit year: 03, month: 08, day: 06
	
	fileStats = os.stat( "logArchive.ini")
	FileTimeDetails = time.localtime( fileStats [ stat.ST_CTIME ] )
	print FileTimeDetails
	
	timeNow = time.localtime()
	print timeNow
	
	FiveDaysOld = datetime.timedelta(days=-5)
	print FiveDaysOld
	
	fileTimeStamp = datetime.datetime.fromtimestamp(fileStats [ stat.ST_CTIME ])
	print fileTimeStamp
		
	Result = fileTimeStamp + FiveDaysOld
	
	print "time: " + str(Result)
	print "year: " + str(Result.year)
	print "month: " + str(Result.month)
	print "day: " + str(Result.day)
	print "hour: " + str(Result.hour)
	print "minute: " + str(Result.minute)
	print "second: " + str(Result.second)
	print "microsecond: " + str(Result.microsecond)
	
	today = datetime.datetime.today()
	print "Today: " + str(today)
	
	FiveDaysAgo = today + FiveDaysOld
	
	if(fileTimeStamp < FiveDaysAgo):
		print "yes move file"
	else:
		print "don't move file"
		
#------------------------------------------------------------------------------
main()



