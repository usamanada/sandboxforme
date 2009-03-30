import ConfigParser
import string
#------------------------------------------------------------------------------
# Method 1
#------------------------------------------------------------------------------
cp = ConfigParser.ConfigParser()
cp.readfp (open('config.ini'))

for section in cp.sections():
    print (section)
    for item in cp.items(section):
        print ("key = " + item[0] + " value = " + item[1])
#------------------------------------------------------------------------------
# Method 2
#------------------------------------------------------------------------------
config = ConfigParser.ConfigParser()

config.read("samples/sample.ini")

# dump entire config file
for section in config.sections():
	print section
	for option in config.options(section):
		print " ", option, "=", config.get(section, option)
#find a key		
config.get(section, option)

#------------------------------------------------------------------------------
# Method 3 Writing to config file
#------------------------------------------------------------------------------
import ConfigParser
import sys

config = ConfigParser.ConfigParser()

# set a number of parameters
config.add_section("book")
config.set("book", "title", "the python standard library")
config.set("book", "author", "fredrik lundh")

config.add_section("ematter")
config.set("ematter", "pages", 250)

# write to screen
config.write(sys.stdout)