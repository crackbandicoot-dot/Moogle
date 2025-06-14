// Ignore Spelling: Moogle

using MoogleEngine.DocumentsUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoogleEngine.ExtensorMethods
{
    public static class Extensions
    {
        public static IEnumerable<DocumentPage> Read(this IDocumentReader reader)
        {
            int pageNumber = 1;
            while (!reader.EndOfDocument())
            {
                string page = reader.ReadPage();
                var words = Regex.Split(page, @"\W+").Where(elem => elem != " " && elem != "").Select(s => s.ToLower());
                var wordFrequency = new Dictionary<string, int>();
                foreach (var word in words)
                {
                    if (!wordFrequency.TryGetValue(word, out int lastFrequency))
                    {
                        wordFrequency.Add(word,1);
                    }
                    else
                    {
                        wordFrequency[word] = ++lastFrequency;
                    }
                }
                yield return new DocumentPage(pageNumber,reader.DocumentPath,wordFrequency);
                pageNumber++;
            }
        }
    }
}
