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
            var temp = line.Split(' ')
                .GroupBy(x => x).Select(x => new {word = x.Key, value = x.Count()});
            foreach (var x in temp)
            {
                //Console.WriteLine(x.word);
                if (x.word != "" & x.word.Length >= 1)
                {
                    if (words.ContainsKey(x.word.ToUpper()))
                        words[x.word.ToUpper()] += x.value;
                    else
                        words.Add(x.word.ToUpper(), x.value);
                }
            }
        }
    }
    
}