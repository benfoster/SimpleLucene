using System;
using Lucene.Net.Documents;
using Lucene.Net.Search;
namespace SimpleLucene
{
    public interface ISearchService : IDisposable
    {
        SearchResult<Document> SearchIndex(Query query, int maxNumberOfResults = 25000);
        SearchResult<T> SearchIndex<T>(Query query, IResultDefinition<T> definition, int maxNumberOfResults = 25000);
    }
}
