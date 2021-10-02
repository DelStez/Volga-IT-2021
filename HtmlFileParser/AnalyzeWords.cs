using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlFileParser
{
    public class AnalyzeWords
    {
        public Dictionary<string, int> words = new Dictionary<string, int>(); 

        public void GetCount(string line)
        {
            var temp = line.Split(' ');
            foreach (var x in temp)
            {
                if(words.ContainsKey(temp))
            }
        }
    }
    
}