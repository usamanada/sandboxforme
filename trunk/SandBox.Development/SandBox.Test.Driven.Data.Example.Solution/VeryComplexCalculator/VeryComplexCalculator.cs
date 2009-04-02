using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testExample
{
    public class VeryCompexCalculator
    {
        public static int Add(int a, int b)
        {
            //lets make some tests fail
            if (a == 1)
                return 2 + b;
            return a + b;
        }
    }
}
