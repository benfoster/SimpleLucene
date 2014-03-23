using Lucene.Net.Documents;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleLucene
{
    /// <summary>
    /// Extension methods for SimpleLucene components
    /// </summary>
    public static class SimpleLuceneExtensions
    {
        /// <summary>
        /// Finds a valid index service that has a writer with matching index options
        /// </summary>
        /// <param name="services">The services collection to search</param>
        /// <param name="options">The index options to match</param>
        /// <returns>Index Service</returns>
        public static IIndexService FindWithOptions(this IEnumerable<IIndexService> services, IndexOptions options)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }

            return services.SingleOrDefault(s => s.IndexWriter.IndexOptions.Equals(options));
        }

        /// <summary>
        /// Creates a DirectoryInfo object from the index location's path
        /// </summary>
        /// <param name="location">The index location</param>
        /// <returns>DirectoryInfo</returns>
        public static DirectoryInfo GetDirectory(this IIndexLocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            return new DirectoryInfo(location.GetPath());
        }

        /// <summary>
        /// Searches an index using the provided query and returns a strongly typed result object
        /// </summary>
        /// <typeparam name="T">The type of result object to return</typeparam>
        /// <param name="query">A Lucene query to use for the search</param>
        /// <param name="definition">A delegate definition to transform the returned Lucene documents</param>
        /// <returns>A search result object containing both Lucene documents and typed objects based on the delegate definition</returns>
        public static SearchResult<T> SearchIndex<T>(this ISearchService searchService,
            Query query, Func<Document, T> definition)
        {
            if (searchService == null)
            {
                throw new ArgumentNullException("searchService");
            }

            return searchService.SearchIndex<T>(query, new DelegateSearchResultDefinition<T>(definition));
        }
    }
}
