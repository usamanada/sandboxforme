//-----------------------------------------------------------------------------
//   File: UeValidation.js
//   Desc: functions to validate userinterface data
//-----------------------------------------------------------------------------
//   Change History
//-----------------------------------------------------------------------------
//   Date: 12/11/2007   Author: Olof Szymczak  Description:
//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------
function UeSetFieldValues(szElement)
{
	document.getElementById(szElement).onfocus = BGWhite;
	document.getElementById(szElement).style.backgroundColor= "#FFC0C0";
}
//-----------------------------------------------------------------------------
function UeSetValues(szElement, szName, obValid, oMsg)
{
	obValid.value = false;
	oMsg.append ( '\n\t' + szName);
	UeSetFieldValues(szElement);
}
//-----------------------------------------------------------------------------
function UeValid_isEmail(szElement, szName, obValid, oEmailMsg)
{
	if (document.getElementById(szElement).value != "")
	{
		//var regex = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
		var regex =  /^.+@\w(\.?[\w-])*\.[a-z]{2,6}$/i		
		var blnEmail = regex.test(document.getElementById(szElement).value);
		if ( blnEmail == false)
		{
			UeSetValues(szElement, szName, obValid, oEmailMsg);
		}
	}
}
//-----------------------------------------------------------------------------
function UeValid_isValidIPAddress(strElement)
{
   var ipaddr = document.getElementById(strElement).value;

   var re = /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/;
   if (re.test(ipaddr))
   {
      var parts = ipaddr.split(".");
      if (parseInt(parseFloat(parts[0])) == 0) { return false; }
      for (var i=0; i<parts.length; i++)
      {
         if (parseInt(parseFloat(parts[i])) > 255) { return false; }
      }
      return true;
   }
   else
   {
      return false;
   }
}
//-----------------------------------------------------------------------------
function UeValid_isPhone(szElement, szName, obValid, oPhoneMsg)
{
	if (document.getElementById(szElement).value != "")
	{
		var blnPhone = /^[0-9 ]+$/.test(document.getElementById(szElement).value);
		if ( blnPhone == false)
		{
			UeSetValues(szElement, szName, obValid, oPhoneMsg);
		}
	}
}
//-----------------------------------------------------------------------------
function UeValid_isServiceID(szElement, szName, obValid, oServiceMsg)
{
	if (document.getElementById(szElement).value != "")
	{
		var blnService = /^[a-z]{3,3}-[a-z]{5,5}-[0-9]{3,3}$/i.test(document.getElementById(szElement).value);
		if ( blnService == false)
		{
			UeSetValues(szElement, szName, obValid, oServiceMsg);
		}
	}
}
//-----------------------------------------------------------------------------
function UeValid_isNumeric(szElement, szName, obValid, oNumericMsg)
{
	if (document.getElementById(szElement).value != "")
	{
		var blnNumeric = /^\d+$/.test(document.getElementById(szElement).value);
		if ( blnNumeric == false)
		{
			UeSetValues(szElement, szName, obValid, oNumericMsg);
		}
	}
}
//-----------------------------------------------------------------------------
function UeValid_isBlank(szElement, szName, szType, obValid, oBlankMsg)
{
	/*Check if field is blank*/
	if (szType == "select")
	{
		if (document.getElementById(szElement).options[document.getElementById(szElement).selectedIndex].value == 0)
		{
			UeSetValues(szElement, szName, obValid, oBlankMsg);
		}
	}
	else if (szType == "listbox")
	{
		if (document.getElementById(szElement).selectedIndex == -1)
		{
			UeSetValues(szElement, szName, obValid, oBlankMsg);
		}
	}
	else
	{
		if(document.getElementById(szElement).value == "" )
		{
			UeSetValues(szElement, szName, obValid, oBlankMsg);
		}
	}
}
//-----------------------------------------------------------------------------
function UeValid_isTime(szElement, szName, obValid, oTimeMsg)
{
	if (document.getElementById(szElement).value != "")
	{
		var blnNumeric = /^(2[0-3]|[01]?\d):[0-5]\d$/.test(document.getElementById(szElement).value);
		if ( blnNumeric == false)
		{
			UeSetValues(szElement, szName, obValid, oTimeMsg);
		}
	}
}
//-----------------------------------------------------------------------------
function UeValid_isFileAttached(szElement,szName, obValid, oFileAttachedMsg )
{	//Check if field is blank
	if(document.getElementById(szElement).value != "" )
	{
		UeSetValues(szElement, szName, obValid, oFileAttachedMsg);
	}
}
//-----------------------------------------------------------------------------
function UeAppend(szAdd)
{
	(this).value = (this).value + szAdd;
}
//-----------------------------------------------------------------------------
function UeMsg()
{
	this.value = "";
	this.append = UeAppend;
}
//-----------------------------------------------------------------------------
function UeValidForm()
{
	this.value = true;
	this.append	= UeAppend;
}
//-----------------------------------------------------------------------------


