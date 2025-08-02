using MoogleEngine.DocumentsUtils;
using MoogleEngine.TextReader;
using MoogleEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoogleEngine.Text
{

    public class TextDocument
    {
        private static ITextReaderFactory _readerFactory = new TextReaderFactory();
       
        public DateTime Date { get; private set; }

        public string Path { get; private set; }

        
        public List<TextPage> Pages { get; private set; }

        
        public int PageCount { get; private set; }

        [JsonConstructor]
        public TextDocument(DateTime date, string path, List<TextPage> pages, int pageCount)
        {
            Date = date;
            Path = path;
            Pages = pages;
            PageCount = pageCount;
        }
        public TextDocument(string path)
        {
            Date = File.GetLastWriteTime(path);
            Path = path;

            try
            {
                List<TextPage> pages = [new TextPage(0, [], path)];
                var reader = _readerFactory.CreateReader(path);
                foreach (var page in reader.Read())
                {
                    pages.Add(page);
                }

                Pages = [.. pages];
                PageCount = pages.Count;

            }
            catch (Exception ex) { 
                Pages = new List<TextPage>();
            }
        }
    }
}
