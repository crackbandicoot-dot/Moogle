using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.DocumentsUtils
{
    public interface IDocumentReader
    {
        string DocumentPath { get; }
        bool EndOfDocument();
        string ReadPage();
    }
}
