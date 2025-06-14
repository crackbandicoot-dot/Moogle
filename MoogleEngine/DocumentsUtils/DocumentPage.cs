// Ignore Spelling: Utils

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.DocumentsUtils
{
   public class DocumentPage
    {
        public DocumentPage(int pageNumber,string originalDocumentPath,IDictionary<string,int> wordsFrequency)
        {
            PageNumber = pageNumber;
            WordsFrequencies = wordsFrequency;
            OriginalDocumentPath = originalDocumentPath;
        }

        public int PageNumber { get; }

        public string OriginalDocumentPath { get; }
        public IDictionary<string,int> WordsFrequencies { get; }

        
    }
}
