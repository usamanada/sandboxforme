using System;

namespace VisualXPath
{
	/// <summary>
	/// Summary description for DisplayForm.
	/// </summary>
	public class DisplayForm
	{
		public static bool isVerbose = false;
        
		public static string GetElementName(string prefix, string elementName)
		{
			if (isVerbose)
				return Verbose.childText + prefix + elementName;
			else
				return prefix + elementName;
		}

		public static string GetAttrName(string attrName)
		{
			if (isVerbose)
				return Verbose.attrText + attrName;
			else
				return "@" + attrName;
		}

		public static string GetPositionName(int Pos)
		{
			if (isVerbose)
				return Verbose.GetPositionText(Pos);
			else
				return Convert.ToString(Pos);
		}

		public static string GetTextName()
		{
			if (isVerbose)
				return Verbose.childText + "text()";
			else
				return "text()";
		}

		public static string GetAllElementsName(string elementName)
		{
			string searchChar = "[";
			if (elementName.IndexOf(searchChar,0,elementName.Length) != -1)
                return elementName.Remove(elementName.LastIndexOf(searchChar),elementName.Length - elementName.LastIndexOf(searchChar));
			else
				return elementName;
		}
	}
}
