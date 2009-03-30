#!/usr/bin/env python
#python version 2.4
import sys
import getopt
import re
import subprocess
import os
import string
import shutil
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def runSQLCommand(szString, szServer, szDataBase):
	
	szArguments  = " -S " + szServer + " -E -n -d " + szDataBase + " -i " + os.path.abspath(szString) + gszLogFile
	result = subprocess.call("osql" + szArguments, shell=True)
	if result != 0:
		print "osql has failed: " + szString
		LogCommand("osql has failed: " + szString)
		sys.exit(0)
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def LogCommand(szString):
	
	print szString
	szArguments  = " " + os.path.abspath(szString) + gszLogFile
	print szArguments
	result = subprocess.call("echo" + szArguments, shell=True)
	if result != 0:
		print "Log message has failed: " + szString
		LogCommand("Log message has failed: " + szString)
		sys.exit(0)

#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def installTableCommand(szPath, szFilename):
	LogCommand("Installing Table Change: " + szFilename)
	runSQLCommand(szPath + szFilename, gszServer, gszDataBase)
	LogCommand("Successful installed Table Change: " + szFilename)
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def installFunctionCommand(szPath, szFilename):
	LogCommand("Installing stored proc: " + szFilename)
	runSQLCommand(szPath + szFilename, gszServer, gszDataBase)
	LogCommand("Successful installed stored proc: " + szFilename)
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def installSPCommand(szPath, szFilename):
	LogCommand("Installing stored proc: " + szFilename)
	runSQLCommand(szPath + szFilename, gszServer, gszDataBase)
	LogCommand("Successful installed stored proc: " + szFilename)
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def installViewCommand(szPath, szFilename):
	LogCommand("Installing View: " + szFilename)
	runSQLCommand(szPath + szFilename, gszServer, gszDataBase)
	LogCommand("Successful installed View: " + szFilename)
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def installProjectTrackingViewCommand(szPath, szFilename):
	LogCommand("Installing View: " + szFilename)
	runSQLCommand(szPath + szFilename, gszPTServer, gszPTDataBase)
	LogCommand("Successful installed View: " + szFilename)
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def copyFiles(src, dst):
	srcname = os.path.abspath(src)
	dstname = os.path.abspath(dst)
		
	LogCommand("copy " + srcname + " " + dstname)
	shutil.copy2(srcname, dstname)
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def tableChanges():
	szCurrentPath = os.path.abspath(os.getcwd())

	LogCommand("table changes path: " + szCurrentPath)

	LogCommand("Step 1")
	installTableCommand(szCurrentPath, "/1_PRJ_PROJECT_COLLECTION_TABLECHANGE.sql")

	LogCommand("Step 2")
	installTableCommand(szCurrentPath, "/2_PRJ_PROJECT_DATE_TABLECHANGE.sql")

	LogCommand("Step 3")
	installTableCommand(szCurrentPath, "/3_PRJ_PROJECT_TABLECHANGE.sql")

	LogCommand("Step 4")
	installTableCommand(szCurrentPath, "/4_PRJ_PROJECT_VLAN_TYPE_TABLECHANGE.sql")

	LogCommand("Step 5")
	installTableCommand(szCurrentPath, "/5_PRJ_PROJECT_TYPE_TABLECHANGE.sql")

	LogCommand("Step 6")
	installTableCommand(szCurrentPath, "/6_PRJ_All_other_TABLECHANGE.sql")
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def installStoreProcs():
	szCurrentPath = os.path.abspath(os.getcwd() + "/../../SQLScripts/StoredProcedures")
	LogCommand("Install Stored Proc path: " + szCurrentPath)
	
	LogCommand("Install Stored Procs")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_COLLECTION.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_DATE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_DATE_TYPE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_DELAY.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_LOCATION.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_NOTE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_NOTE_TYPE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_PROJECT.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_PROJECT_TYPE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_SERVICE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_STATUS.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_T_DIM_PRJ_VLAN_TYPE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_COLLECTION.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_DATE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_DATE_TYPE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_DELAY.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_LOCATION.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_NOTE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_NOTE_TYPE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_PROJECT.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_PROJECT_TYPE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_SERVICE.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_STATUS.PRC")
	installSPCommand(szCurrentPath, "/dbo.USP_ETL_E_DIM_PRJ_VLAN_TYPE.PRC")
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def installDataChanges():
	szCurrentPath = os.path.abspath(os.getcwd())
	LogCommand("Data changes path: " + szCurrentPath)
	
	LogCommand("Step 7")
	installTableCommand(szCurrentPath, "/7_Global_Variables_DATA.sql")
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def installProjectTrackingViews():
	szCurrentPath = os.path.abspath(os.getcwd() + "/../../SQLScripts/Views/projectTracking")
	LogCommand("Install Project Tracking View path: " + szCurrentPath)
	
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_Collections.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_Dates.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_Delays.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_Locations.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_Notes.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_NoteTypes.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_ProjectDates.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_Projects.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_ProjectTypes.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_Services.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_Statuses.VIW")
	installProjectTrackingViewCommand(szCurrentPath, "/dbo.vwDW_VlanType.VIW")
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def copyDTSPackages():
	szCurrentPath = os.path.abspath(os.getcwd() + "/../../DTSPackages")
	LogCommand("Copy DTS Packages Path: " + szCurrentPath)
	szDest = "\\\\" + gszServer + "\\g$\\dts"
	
	copyFiles(szCurrentPath + "/ETL_EXT_DIM_PRJ.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_COLLECTION.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_DATE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_DATE_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_DELAY.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_LOCATION.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_NOTE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_NOTE_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_PROJECT.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_PROJECT_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_SERVICE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_STATUS.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_L_DIM_PRJ_VLAN_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_COLLECTION.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_DATE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_DATE_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_DELAY.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_LOCATION.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_NOTE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_NOTE_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_PROJECT.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_SERVICE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_PROJECT_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_VLAN_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_T_DIM_PRJ_STATUS.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_COLLECTION.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_DATE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_DATE_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_DELAY.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_LOCATION.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_NOTE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_NOTE_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_PROJECT.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_SERVICE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_STATUS.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_PROJECT_TYPE.dts", szDest)
	copyFiles(szCurrentPath + "/ETL_E_DIM_PRJ_VLAN_TYPE.dts", szDest)
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def install():
	#tableChanges()
	#installStoreProcs()
	#installDataChanges()
	installProjectTrackingViews()
	#copyDTSPackages()	
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def manageUsers():
	#Dont run this on production server
	if gszServer == "uessqla" and gszDataBase == "scoops2prod":
		print "Test users not installed"
		LogCommand("Test users not installed")
		return
	else:
		szCurrentPath = os.path.abspath(os.getcwd())
		LogCommand("Manage users path: " + szCurrentPath)
	
		LogCommand("Step 14")
		LogCommand("Test Users installing")
		runSQLCommand(szCurrentPath + "/06_manageusers.sql")
		LogCommand("Test Users installed")
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def parameters():
	global gszServer
	gszServer = raw_input("Data Warehouse Server: ")
	global gszDataBase
	gszDataBase = raw_input("Data Warehouse Database: ")
	
	global gszPTServer	
	gszPTServer = raw_input("Project Tracking Server: ")
	global gszPTDataBase
	gszPTDataBase = raw_input("Project Tracking Database: ")
		
	
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def correctParameters():
	bResult = False
	while True:
		szContinue = raw_input("Are the parameters correct (y)Yes,(n)No or (x)exit: ")
		if( szContinue.lower() == "y"):
			bResult = True
			break
		if( szContinue.lower() == "n"):
			bResult = False
			break
		if( szContinue.lower() == "x"):
			sys.exit(0)
			
		print "Argument entered was not y, n or x please try again."
	return bResult
			
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def Usage():
	while True:
		os.system("cls")
		parameters()
		print "Installing DataWarehouse on Server: " + gszServer + " Database: " + gszDataBase
		print "Installing Project Tracking on Server: " + gszPTServer + " Database: " + gszPTDataBase
		if( correctParameters() == True):
			break	
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
def init():
	global gszLogFile
	gszLogFile = " >> " + os.getcwd() + "\\Install.log"
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------

def main():
	
	init()
	
	Usage()	
	
	LogCommand("Starting install")
	
	install()
	
	LogCommand("***********Successfully installed*************")
#-------------------------------------------------------------------------------
#-------------------------------------------------------------------------------
main()