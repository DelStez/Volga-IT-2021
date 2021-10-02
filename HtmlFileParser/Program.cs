using System;
using System.IO;
using System.Linq;

namespace HtmlFileParser
{
    internal class Program
    {
        public static ParserStart parserstart;
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string path = ParseCommand(input);
            path = input;
            if (path != null)
            {
                parserstart = new ParserStart(path);
                parserstart.showTextContent();
            }
            Console.WriteLine("f");
            Console.ReadLine();
        }

        private static string ParseCommand(string temp)
        {
            var inputArr = temp.Split(' ').ToArray();
            foreach (var v in inputArr)
            {
                if (!string.IsNullOrEmpty(v) && v.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                {
                    return v;
                }
            }
            return null;
        }
    }
}