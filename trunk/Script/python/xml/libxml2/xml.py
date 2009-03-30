import libxml2

#DOC = """<?xml version="1.0" encoding="UTF-8"?>
#<verse>
#  <attribution person="10">Christopher Okibgo</attribution>
#  <line>For he was a shrub among the poplars,</line>
#  <line>Needing more roots</line>
#  <line>More sap to grow to sunlight,</line>
#  <line>Thirsting for sunlight</line>
#</verse>
#"""
#doc = libxml2.parseDoc(DOC)
#------------------------------------------------------------------------------
agFileName = []
bgFile = 0
#------------------------------------------------------------------------------
def addToList(fileName):
	global agFileName
	agFileName.append(fileName)
#------------------------------------------------------------------------------
def setAddFile( status):
	global bgFile
	bgFile = status
#------------------------------------------------------------------------------
def getAddFile():
	global bgFile
	return bgFile
#------------------------------------------------------------------------------
def findFiles( child):
	while child is not None:
		if child.type == "element":
			if child.name == "folders":
				setAddFile(0)
				findFiles(child.children)
			elif child.name == "folder":
				setAddFile(0)
				findFiles(child.children)
			elif child.name == "files":
				setAddFile(0)
				findFiles(child.children)
			elif child.name == "file":
				setAddFile(1)
				findFiles(child.children)
			elif child.name == "name":
				if 1 == getAddFile():
					addToList(child.content)

		child = child.next
#------------------------------------------------------------------------------

def main():
	doc = libxml2.parseFile("C:/TEMP/UECustomerContacts.dnn")
	if doc.name != "C:/TEMP/UECustomerContacts.dnn":
		print "doc.name failed"
		sys.exit(1)
	root = doc.children

	#using xpath
	for node in doc.xpathEval('//folders/folder/files/file/name'):
		print node.content

	#iterate over children of verse
	child = root.children
	findFiles( child)
	print len(agFileName)
	print agFileName

	doc.freeDoc()

#------------------------------------------------------------------------------
main()