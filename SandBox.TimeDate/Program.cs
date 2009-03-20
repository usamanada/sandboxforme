using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandBox.TimeDate
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("u"));

            Console.WriteLine(now.ToString("zzzz"));
            
            Console.WriteLine(now.ToString("yyyyMMdd'T'HHmmss'.'FFF'Z'zzzz"));

            Console.WriteLine(now.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss'.'FFF'Z'"));

        }
    }
}
