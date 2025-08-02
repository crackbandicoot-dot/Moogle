// Ignore Spelling: Utils

using MoogleEngine.Text;
using MoogleEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoogleEngine.DocumentsUtils
{
   public class TextPage
    {
        [JsonConstructor]
        public TextPage(int pageNumber, SimpleDictionary<string, double> wordsFrequencies, string pathToDocument)
        {
            PageNumber = pageNumber;
            WordsFrequencies = wordsFrequencies;
            PathToDocument = pathToDocument;
        }

        public int PageNumber { get; set; }
        public string PathToDocument { get; set; }
        public SimpleDictionary<string,double> WordsFrequencies { get; set; }

    }
}
