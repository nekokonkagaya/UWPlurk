using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPlurk.Api
{
    /// <summary>
    /// Algorithm is refer to http://www.stum.de/2008/10/20/base36-encoderdecoder-in-c/
    /// </summary>
    public class Base36
    {
        public static string Encode(long input)
        {
            char[] clist = new char[] {
                            '0', '1', '2', '3', '4',
                            '5', '6', '7', '8', '9',
                            'a', 'b', 'c', 'd', 'e',
                            'f', 'g', 'h', 'i', 'j',
                            'k', 'l', 'm', 'n', 'o',
                            'p', 'q', 'r', 's', 't',
                            'u', 'v', 'w', 'x', 'y',
                            'z' };

            StringBuilder sb = new StringBuilder();
            while (input != 0)
            {
                sb.Append(clist[input % 36]);
                input /= 36;
            }
            return Reverse(sb.ToString());
        }

        public long Decode(string input)
        {
            string clist = "0123456789abcdefghijklmnopqrstuvwxyz";

            input = Reverse(input.ToLower());
            long result = 0;
            int pos = 0;
            foreach (char c in input)
            {
                result += clist.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }

        private static string Reverse(string input)
        {
            Stack<char> resultStack = new Stack<char>();
            foreach (char c in input)
            {
                resultStack.Push(c);
            }

            StringBuilder sb = new StringBuilder();
            while (resultStack.Count > 0)
            {
                sb.Append(resultStack.Pop());
            }
            return sb.ToString();
        }
    }
}
