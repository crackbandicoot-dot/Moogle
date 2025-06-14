namespace MoogleEngine;

public class SearchItem
{
    public SearchItem(string title, string snippet, float score,int pageNumber)
    {
        this.Title = title;
        this.Snippet = snippet;
        this.Score = score;
        this.PageNumber = pageNumber;
    }

    public string Title { get; private set; }

    public string Snippet { get; private set; }

    public int PageNumber { get; private set; }

    public float Score { get; private set; }
}
