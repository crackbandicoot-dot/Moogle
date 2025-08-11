using MoogleEngine.TextCorpus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace MoogleEngine.Utils
{
    internal static class TextCorpusCache
    {
        private static readonly string _cachePath = "../MoogleEngine/cache.json";

        public static TextCorpus.TextCorpus DeserializeOrCreate(string dirRoute)
        {
            TextCorpus.TextCorpus textCorpus;
            string jsonString = "";
            try
            {
                jsonString = File.ReadAllText(_cachePath);
            }
            catch { }

            if (string.IsNullOrEmpty(jsonString))
            {
                textCorpus = new(dirRoute);
                string jsonInfo = JsonSerializer.Serialize(textCorpus);
                File.WriteAllText(_cachePath, jsonInfo);
            }
            else
            {
                textCorpus = JsonSerializer.Deserialize<TextCorpus.TextCorpus>(jsonString)!;
                textCorpus.Update(dirRoute);
                if (textCorpus.NeedsToSerialize)
                {
                    textCorpus.NeedsToSerialize = false;
                    string jsonInfo = JsonSerializer.Serialize(textCorpus);
                    File.WriteAllText(_cachePath, jsonInfo);
                }
            }
            return textCorpus;
        }

    }
}
