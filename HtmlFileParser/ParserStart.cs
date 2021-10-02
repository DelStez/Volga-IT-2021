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
            
        }

        async void AnalyzeValues(string currentLine)
        {
            _analyzeWords.GetCount(currentLine);
        }

        public List<string> ParseMatches(List<string> currentCollections)
        {
            List<string> newCollection = new List<string>();
            foreach (var match in currentCollections)
            {
                //Console.WriteLine(match);
                string temp = Regex.Replace(match, @"<[^>]*>", string.Empty);
                if (temp != string.Empty)
                {
                    // Clearing punctuation marks (without white-space)
                    temp = Regex.Replace(temp, @"[\p{P}]", string.Empty);
                    // we have "clear" List words
                    AnalyzeValues(temp);
                }
            }
            return newCollection;
        }
    }
}