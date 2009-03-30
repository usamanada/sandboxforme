using System;
using System.Collections.Generic;
using System.Text;

namespace CharToggle
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] array = { 'L', 'O', 'S', 'T' };
            
            Console.WriteLine(new string(array));

            array = toggle(array, 3);

            Console.ReadKey();            
        }


        static char[] toggle(char[] array, int location)
        {
            array[location] = Char.ToUpper(array[location]);
            Console.WriteLine(new string(array));
            if (0 < location)
            {
                array = toggle(array, location - 1);
            }
            
            array[location] = Char.ToLower(array[location]);
            Console.WriteLine(new string(array));
            if (0 < location)
            {
                array = toggle(array, location - 1);
            }            
            return array;
        }
    }
}
