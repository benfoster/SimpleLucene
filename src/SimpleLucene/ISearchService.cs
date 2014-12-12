﻿using System;
using Lucene.Net.Documents;
using Lucene.Net.Search;
namespace SimpleLucene
{
    public interface ISearchService : IDisposable
    {
        SearchResult<Document> SearchIndex(Query query);
        SearchResult<Document> SearchIndex(Query query, int maxResultCount);
        SearchResult<T> SearchIndex<T>(Query query, IResultDefinition<T> definition);
        SearchResult<T> SearchIndex<T>(Query query, IResultDefinition<T> definition, int maxResultCount);
    }
}
