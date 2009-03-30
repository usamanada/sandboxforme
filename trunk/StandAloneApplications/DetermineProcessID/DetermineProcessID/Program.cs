using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace DetermineProcessID
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Process ID: " + Process.GetCurrentProcess().Id.ToString());
            Console.ReadKey();
        }
    }
}
