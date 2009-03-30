using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace SQLCracker2
{
    class DictionaryHelper
    {
        public List<Dictionary> Dictionarys;
        public DictionaryHelper()
        {
            Dictionarys = new List<Dictionary>();
        }
        public void BuildDictionaryList(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    Dictionary d = new Dictionary();
                    d.UpperCaseWord = sr.ReadLine().ToUpper();
                    d.WordLength = d.UpperCaseWord.Length;
                    byte[] plainTextBytes = Encoding.Unicode.GetBytes(d.UpperCaseWord);

                    d.UniCode8WordLength = plainTextBytes.Length;
                    d.SaltedWord = new Byte[plainTextBytes.Length + 4];
                    plainTextBytes.CopyTo(d.SaltedWord, 0);
                    Dictionarys.Add(d);
                }
            }

        }
    }
}
