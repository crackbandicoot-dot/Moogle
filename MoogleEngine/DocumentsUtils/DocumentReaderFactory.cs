using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.DocumentsUtils
{
    class DocumentReaderFactory
    {

        public IDocumentReader CreateReader(string path)
        {
            if (path.EndsWith("pdf"))
            {
                return new PDFReader(path);
            }
            else if (path.EndsWith("txt"))
            {
                return new TXTReader(path);
            }
            else
            {
                throw new NotImplementedException("Unsupported document extension");
            }

        }
    }
}
