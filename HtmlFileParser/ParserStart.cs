using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;

namespace HtmlFileParser
{
    internal interface ICommands
    {
        string pathFile { get; set; }
        string mods { get; set; }
    }

    public class ParserStart
    {
        public string pathFile { get; set; }
        public List<string> mods { get; set; }

        public Regex regexTags = new Regex(@"<(.*)/?>(.?)|<(.*)/>");
        public AnalyzeWords _analyzeWords;

        //Constructor 
        public ParserStart(string path, List<string> mod)
        {
            _analyzeWords = new AnalyzeWords();
            pathFile = path;
            mods = mod;
        }

        public ParserStart(string path)
        {
            _analyzeWords = new AnalyzeWords();
            pathFile = path;
        }

        private List<string> Reading(string dataOrPath, List<string> collections)
        {
            using ( StreamReader reader = new StreamReader( dataOrPath ) )
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    var temp = regexTags.Matches(line).Cast<Match>().Select(x => x.Value).ToArray();
                    foreach (var x in temp) collections.Add(x);
                }
            }
            return collections;
        }
        public void showTextContent()
        {
            List<string> collections = new List<string>();
            //Find all html string with contains of 
            collections = new List<string>(Reading(pathFile, collections));
            ParseMatches(collections);
            ShowResult();

        }

        public void ShowResult()
        { 
            var ordered = _analyzeWords.words
                .OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
            foreach (var x in ordered.Reverse())
            {
                Console.WriteLine(@"{0} - {1}", x.Key, x.Value);
            }
        }

        void AnalyzeValues(string currentLine)
        {
            _analyzeWords.GetCount(currentLine);
        }

        public void ParseMatches(List<string> currentCollections)
        {
            foreach (var match in currentCollections)
            {
                //Console.WriteLine(match);
                string temp = Regex.Replace(match, @"<[^>]+>|&nbsp;", " ").Trim();
                temp = Regex.Replace(temp, @"\s{2,}", " ");
                if (temp != string.Empty)
                {
                    // Clearing punctuation marks (without white-space)
                    temp = Regex.Replace(temp, @"[\s\p{P}]", " ");
                    //Console.WriteLine(temp);
                    // we have "clear" List words
                    AnalyzeValues(temp);
                }
            }
        }
    }
}