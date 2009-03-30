// Created By: Simon Friend (DWS)   Date: 16-Feb-20005
// Description: This file contains all common Uecomm javascript functions
//
//


//Created by:	Jim McLeod (DWS)	Date: 14-Mar-2005
//Description:	Adds a trim() function to the String class to strip
//				whitespace from the string.
String.prototype.trim = function() {
	return this.replace(/^\s*/,"").replace(/\s*$/,"");
}


//Created By: Simon Friend (DWS)   Date:15-Feb-2005
//Description: Shows or hides help <div>
// strElementName: Name of the DIV tag which will contain and display text
// strText: The text that is to be displayed
function showHideHelp(strElementName, strText){
	if (document.getElementById(strElementName).style.display == 'none'){
		document.getElementById(strElementName).innerHTML = strText;
		document.getElementById(strElementName).style.display = '';
	}
	else{
		document.getElementById(strElementName).style.display = 'none';
	}
}

//Created By:Simon Friend (DWS) Date: 16-Feb-2005
//Description: This function is attached to fields when they fail validation

function BGWhite(){
		document.getElementById(this.id).style.backgroundColor= "white"
	}



/*
Purpose: return true if the date is valid, false otherwise

Arguments: day integer representing day of month
month integer representing month of year (1 - 12)
year integer representing year

Variables: dteDate - date object

*/
function isValidDate(day,month,year)
{
	var dteDate;

	//set up a Date object based on the day, month and year arguments
	//javascript months start at 0 (0-11 instead of 1-12)
	dteDate=new Date(year,month-1,day);

	/*
	Javascript Dates are a little too forgiving and will change the date to a reasonable guess if it's invalid. 
	We'll use this to our advantage by creating the date object and then comparing it to the details we put it. 
	If the Date object is different, then it must have been an invalid date to start with...
	*/

	return ((day==dteDate.getDate()) && (month-1==dteDate.getMonth()) && (year==dteDate.getFullYear()));
}
