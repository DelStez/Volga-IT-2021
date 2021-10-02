using System;
using System.IO;
using System.Linq;

namespace HtmlFileParser
{
    internal class Program
    {
        public static string path = string.Empty;
        public static ParserStart parserstart;
        public static void Main(string[] args)
        {
            StartProgram();
        }

        public static void StartProgram()
        {
            try
            {
                Console.Write("Path to html-file: ");
                string input = Console.ReadLine();
                if ((path = ParseCommand(input)) != null)
                {
                    parserstart = new ParserStart(path);
                    parserstart.showTextContent();
                }
                Console.WriteLine("Press any key for exit...");
                Console.ReadLine();
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("The file does not exist or the path is not correct. You can try again or exit.");
                string input = string.Empty;
                bool cTrue = false;
                do
                {
                    Console.Write("Continue?(Y/N)");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "N":
                        case "n":
                            return;
                        case "Y":
                        case "y":
                            cTrue = true;
                            break;
                        default:
                            Console.WriteLine("Unknown answer");
                            break;
                    }
                } while (!cTrue);
                StartProgram();
            }
        }

        private static string ParseCommand(string temp)
        {
            var inputArr = temp.Split(' ').ToArray();
            
            foreach (var v in inputArr)
            {
                FileAttributes attr = File.GetAttributes(v);
                if (File.Exists(v))
                {
                    return v;
                }
            }
            return null;
        }
    }
}