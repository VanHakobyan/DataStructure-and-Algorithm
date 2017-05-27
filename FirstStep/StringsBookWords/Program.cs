using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringsBackWords
{
    class Program
    {
        static void BookWord(string s)
        {
            if (s.Length >= 1)
            {
                string s1 = s.Substring(1, s.Length - 1);
                BookWord(s1);
                Console.Write(s[0]);
            }
        }
        static void Main(string[] args)
        {
            BookWord("armen");
            do
            {
            } while (true);
        }
    }
}
