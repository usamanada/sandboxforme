using System;

namespace VisualXPath
{
	/// <summary>
	/// Summary description for Verbose.
	/// </summary>
	public struct Verbose
	{
        public static readonly string childText = "child::";
		public static readonly string attrText = "attribute::";
		public static readonly string selfNodeText = "self::node()";
		public static readonly string parentNodeText = "parent::node()";

		public static string GetPositionText(int Pos)
		{
			return "position()=" + Pos;
		}
	}
}
