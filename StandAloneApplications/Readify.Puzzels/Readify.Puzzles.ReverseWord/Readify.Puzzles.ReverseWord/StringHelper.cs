using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Readify.Puzzles.ReverseWord
{
    public class StringHelper
    {
        /// <summary>
        /// This method reverses all the words in a string
        /// </summary>
        /// <param name="source">String to have words reversed</param>
        /// <returns>String - Reverse Word</returns>
        public static string ReverseWords(string source)
        {
            if (!String.IsNullOrEmpty(source))
            {
                char[] charArray = new char[source.Length];
                int startOfWordIndex = 0;

                do
                {
                    int endOfWordIndex = IndexOfFirstEndOfWord(source, startOfWordIndex);

                    int WordLen = endOfWordIndex - startOfWordIndex;
                    
                    //copy the char to the array in reverse order
                    for (int i = 0; i <= WordLen; i++)
                    {
                        charArray[startOfWordIndex + i] = source[endOfWordIndex - i];
                    }

                    //Add white space char to the array
                    if (endOfWordIndex < source.Length - 1)
                    {
                        endOfWordIndex++;
                        charArray[endOfWordIndex] = source[endOfWordIndex];
                    }

                    //Set index to the start of the next word
                    startOfWordIndex = endOfWordIndex + 1;
                }
                while (startOfWordIndex < source.Length);

                return new string(charArray);
            }
            return "";
        }
        /// <summary>
        /// Used to find the first or next word index
        /// </summary>
        /// <param name="source">String where next word is to be found</param>
        /// <param name="StartIndex">Start searching from this location</param>
        /// <returns>Int - An index of the end of word</returns>
        private static int IndexOfFirstEndOfWord(string source, int StartIndex)
        {
            for (int index = StartIndex; index < source.Length; index++)
            {
                if (Char.IsWhiteSpace(source[index]))
                {
                    return --index;
                }
            }
            //If no white space character is found return end of string array index
            return source.Length - 1;
        }
    }
}
