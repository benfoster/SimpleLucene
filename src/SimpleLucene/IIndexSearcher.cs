using Lucene.Net.Search;

namespace SimpleLucene
{
    public interface IIndexSearcher
    {
        Searcher Create();
    }
}
