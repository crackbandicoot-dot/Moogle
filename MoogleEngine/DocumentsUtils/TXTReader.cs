// Ignore Spelling: Moogle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.DocumentsUtils
{
    class TXTReader : IDocumentReader
    {

        private bool read;
     
        public string DocumentPath { get; }

        public TXTReader(string path)
        {
            DocumentPath = path;

        }
        
        public bool EndOfDocument()
        {
            return read;
        }

        public string ReadPage()
        {
            read = true;
            return File.ReadAllText(DocumentPath);
        }
    }
}
