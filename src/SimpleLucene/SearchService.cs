using Lucene.Net.Documents;
using Lucene.Net.Search;
using System;
using System.Linq;

namespace SimpleLucene
{
    /// <summary>
    /// Default search service implementation
    /// </summary>
    public class SearchService : ISearchService
    {
        private readonly IIndexSearcher indexSearcher;
        private Searcher luceneSearcher;
        private bool isDisposed;

        public SearchService(IIndexSearcher indexSearcher)
        {
            if (indexSearcher == null)
            {
                throw new ArgumentNullException("indexSearcher");
            }

            this.indexSearcher = indexSearcher;
        }

        /// <summary>
        /// Searches an index using the provided query
        /// </summary>
        /// <param name="query">A Lucene query to use for the search</param>
        /// <returns>A search result containing Lucene documents</returns>
        public SearchResult<Document> SearchIndex(Query query)
        {
            return SearchIndex<Document>(query, new DocumentResultDefinition());
        }

        /// <summary>
        /// Searches an index using the provided query and returns a strongly typed result object
        /// </summary>
        /// <typeparam name="T">The type of result object to return</typeparam>
        /// <param name="query">A Lucene query to use for the search</param>
        /// <param name="definition">A search definition used to transform the returned Lucene documents</param>
        /// <returns>A search result object containing both Lucene documents and typed objects based on the definition</returns>
        public SearchResult<T> SearchIndex<T>(Query query, IResultDefinition<T> definition)
        {
            var searcher = this.GetSearcher();
            TopDocs hits = null;
            hits = searcher.Search(query, 25000);            
            var results = hits.scoreDocs.Select(h => searcher.Doc(h.doc));
            return new SearchResult<T>(results, definition);
        }

        /// <summary>
        /// Searches an index using the provided query and returns a strongly typed result object
        /// </summary>
        /// <typeparam name="T">The type of result object to return</typeparam>
        /// <param name="query">A Lucene query to use for the search</param>
        /// <param name="definition">A search definition used to transform the returned Lucene documents</param>
        /// <param name="filter">A filter used in the search</param>
        /// <param name="sort">A sort used in the search</param>
        /// <returns>A search result object containing both Lucene documents and typed objects based on the definition</returns>
        public SearchResult<T> SearchIndex<T>(Query query, IResultDefinition<T> definition, Filter filter, Sort sort)
        {
            var searcher = this.GetSearcher();
            TopDocs hits = null;
            hits = searcher.Search(query, filter, 25000, sort);
            var results = hits.scoreDocs.Select(h => searcher.Doc(h.doc));
            return new SearchResult<T>(results, definition);
        }

        public void Dispose()
        {
            if (!isDisposed && luceneSearcher != null)
            {
                luceneSearcher.Close();
            }
            luceneSearcher = null;
        }

        protected Searcher GetSearcher()
        {
            if (isDisposed)
            {
                isDisposed = false;
            }

            if (luceneSearcher == null)
            {
                luceneSearcher = indexSearcher.Create();
            }

            return luceneSearcher;
        }
    }
}
