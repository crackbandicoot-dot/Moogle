namespace MoogleEngine;

public class SearchItem
{
    public SearchItem(string title, string snippet,List<int> relevantPages)
    {
        this.Title = title;
        this.Snippet = snippet;
        RelevantPages = relevantPages;
    }
    public string Title { get;  set; }
    public string Snippet { get; set; }
    public List<int> RelevantPages { get; set; }
}
