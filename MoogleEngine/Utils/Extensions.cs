// Ignore Spelling: Moogle

using MoogleEngine.DocumentsUtils;
using MoogleEngine.Text;
using MoogleEngine.TextReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;

namespace MoogleEngine.Utils
{
    public static class Extensions
    {
        
        public static SimpleDictionary<string, double> CountWords(this string str)
        {
            var words = Regex.Split(str, @"\W+").Where(elem => elem != " " && elem != "").Select(s => s.ToLower());
            var wordFrequency = new SimpleDictionary<string, double>();
            foreach (var word in words)
            {
                if (!wordFrequency.TryGetValue(word, out double lastFrequency))
                {
                    wordFrequency.Add(word, 1d);
                }
                else
                {
                    wordFrequency[word] = ++lastFrequency;
                }
            }
            return wordFrequency;
        }
        public static IEnumerable<TextPage> Read(this ITextReader reader)
        {
            
            int pageNumber = 1;
            while (reader.ValidPage(pageNumber))
            {
                string page = "";
                try
                {
                    page = reader.ReadPage(pageNumber);
                }
                catch
                {

                }
                 
                yield return new TextPage(pageNumber,page.CountWords(), reader.DocumentPath); ;
                pageNumber++;
            }
        }
    }
}
