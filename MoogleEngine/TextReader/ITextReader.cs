using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.TextReader
{
    public interface ITextReader
    {
        string DocumentPath { get; }
        bool ValidPage(int pageNumber);
        string ReadPage(int pageNumber);
    }
}
