using MoogleEngine.DocumentsUtils;

namespace MoogleEngine.SearchEngine
{
    struct SearchRank
    {
        public SearchRank(TextPage page, double score)
        {
            Page = page;
            Score = score;
        }

        public TextPage Page { get; }
        public double Score { get; }
    }

}
