using System;
using System.Text;

namespace VisualXPath
{
	/// <summary>
	/// Summary description for BooleanOp.
	/// </summary>
	public class BooleanOp
	{
		StringBuilder xpathStr = new StringBuilder();

		public BooleanOp()
		{
		}

		public BooleanOp(string query)
		{
			xpathStr.Append(query);
		}

		public string Or(string query)
		{
			if (!xpathStr.ToString().Equals(""))
			{
				xpathStr.Append(" | ");
			}

			xpathStr.Append(query);

			return xpathStr.ToString();
		}

		public string And(string query)
		{
			if (!xpathStr.ToString().Equals(""))
			{
				xpathStr.Append(" and ");
			}

			xpathStr.Append(query);

			return xpathStr.ToString();
		}

	}
}
