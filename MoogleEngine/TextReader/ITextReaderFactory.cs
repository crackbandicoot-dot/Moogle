namespace MoogleEngine.TextReader
{
    internal interface ITextReaderFactory
    {
        ITextReader CreateReader(string path);
    }
}