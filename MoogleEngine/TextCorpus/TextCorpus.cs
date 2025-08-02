using MoogleEngine.DocumentsUtils;
using MoogleEngine.Text;
using MoogleEngine.TextReader;
using MoogleEngine.Utils;
using System.Text.Json.Serialization;
using MoogleEngine.Utils;
namespace MoogleEngine.TextCorpus
{
    class TextCorpus
    {
        public bool NeedsToSerialize { get; set; }
        public SimpleDictionary<string, TextDocument> Documents { get; set; }  // Added private setter
        public TextCorpus(string dirRoute)
        {

            Documents = new SimpleDictionary<string, TextDocument>();

            var filesRoutes = Utils.Utils.GetFilesRoutes(dirRoute);
            var readerFactory = new TextReaderFactory();
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            Parallel.ForEach(filesRoutes, options, (path) =>
            {
                var readDocument = new TextDocument(path);
                if (readDocument is not null)
                {
                    Documents.Add(readDocument.Path, readDocument);
                }
            });
        }
        [JsonConstructor]
        public TextCorpus(SimpleDictionary<string, TextDocument> documents, bool needsToSerialize)
        {
            Documents = documents;
            NeedsToSerialize = needsToSerialize;
        }

       
        public void Update(string dirRoute)
        {
            var filesRoutes = Utils.Utils.GetFilesRoutes(dirRoute);
            Dictionary<string, DateTime> filesLastModification= new();
            foreach (var route in filesRoutes)
            {
                filesLastModification.Add(route, File.GetLastWriteTime(route));
                if (Documents.TryGetValue(route, out TextDocument? storedDocument))
                {
                    if (storedDocument.Date != File.GetLastWriteTime(route))
                    {
                        Documents.Remove(storedDocument.Path);
                        var readDocument = new TextDocument(route);
                        Documents.Add(readDocument.Path, readDocument);
                        NeedsToSerialize = true;
                    }
                }
                else
                {
                    var readDocument = new TextDocument(route);
                    Documents.Add(readDocument.Path, readDocument);
                    NeedsToSerialize = true;
                }

            }
            
            foreach (var storedRoute in Documents.Keys)
            {
                if (!filesLastModification.ContainsKey(storedRoute))
                {
                    Documents.Remove(storedRoute);
                    NeedsToSerialize = true;
                }

            }

        }
    }
}