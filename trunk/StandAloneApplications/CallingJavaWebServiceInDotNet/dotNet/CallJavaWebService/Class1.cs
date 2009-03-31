using System;

namespace CallJavaWebService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("Calling Calculator Service");
			Calculator.CalculatorServiceService proxy = new Calculator.CalculatorServiceService();
			int result = proxy.Add(3,5);
			Console.WriteLine("Calculator Service Returned: " + result.ToString());
			Console.Read();
		}
	}
}
