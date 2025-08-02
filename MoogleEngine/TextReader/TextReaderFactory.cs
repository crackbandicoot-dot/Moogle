using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Graphics.Operations.MarkedContent;

namespace MoogleEngine.TextReader
{
    class TextReaderFactory : ITextReaderFactory
    {

        public ITextReader CreateReader(string path)
        {
            string extension = Path.GetExtension(path).ToLower();
            return extension switch
            {
                ".pdf" => new PDFReader(path),
                ".txt" => new TXTReader(path),
                _ => throw new Exception("Unsoported extension"),
            };
        }
    }
}
