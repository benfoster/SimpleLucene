using System;
using Lucene.Net.Documents;
using Lucene.Net.Search;
namespace SimpleLucene
{
    public interface ISearchService : IDisposable
    {
        SearchResult<Document> SearchIndex(Query query);
        SearchResult<T> SearchIndex<T>(Query query, IResultDefinition<T> definition);
        SearchResult<T> SearchIndex<T>(Query query, IResultDefinition<T> definition, Filter filter, Sort sort);
    }
}
