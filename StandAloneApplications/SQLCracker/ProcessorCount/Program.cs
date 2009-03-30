using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessorCount
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processor Count: " +Environment.ProcessorCount.ToString());
            Console.WriteLine("Press a key to continue.");
            Console.ReadKey();
        }
    }
}
