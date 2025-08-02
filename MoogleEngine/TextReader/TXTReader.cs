// Ignore Spelling: Moogle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.TextReader
{
    class TXTReader : ITextReader
    {

     
        public string DocumentPath { get; }

        public TXTReader(string path)
        {
            DocumentPath = path;

        }
        
        public bool ValidPage(int pageNumber)
        {
            return pageNumber == 1;
        }

        public string ReadPage(int pageNumber)
        {
            if (!ValidPage(pageNumber))
            {
                return "";
            }
            return File.ReadAllText(DocumentPath);
        }
    }
}
