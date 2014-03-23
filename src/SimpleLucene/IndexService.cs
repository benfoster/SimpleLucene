using System;
using System.Collections.Generic;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using System.Diagnostics;

namespace SimpleLucene
{
    /// <summary>
    /// Default index service implementation
    /// </summary>
    public class IndexService : IIndexService
    {
        private bool isDisposed;
        private readonly IIndexWriter indexWriter;
        private IndexWriter luceneWriter;

        public IndexService(IIndexWriter indexWriter)
        {
            if (indexWriter == null)
            {
                throw new ArgumentNullException("indexWriter");
            }

            this.indexWriter = indexWriter;
        }

        public IIndexWriter IndexWriter
        {
            get
            {
                return indexWriter;
            }
        }

        /// <summary>
        /// Indexes a collection of entities using the provided index definition
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to index</typeparam>
        /// <param name="entities">A list of entities</param>
        /// <param name="definition">The index definition</param>
        /// <returns>An index result with the outcome of the indexing operation</returns>
        public IndexResult IndexEntities<TEntity>(IEnumerable<TEntity> entities, IIndexDefinition<TEntity> definition) where TEntity : class
        {
            return IndexEntities(entities, definition.Convert);
        }

        /// <summary>
        /// Indexes a collection of entities using a delegate index definition
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to index</typeparam>
        /// <param name="entities">A list of entities</param>
        /// <param name="converter">The delegate converter</param>
        /// <returns>An index result with the outcome of the indexing operation</returns>
        public IndexResult IndexEntities<TEntity>(IEnumerable<TEntity> entities, Func<TEntity, Document> converter)
        {
            var result = new IndexResult();
            result.ExecutionTime = Time(() =>
            {
                foreach (TEntity entity in entities)
                {
                    this.GetWriter().AddDocument(converter(entity));
                    result.Count++;
                }
                result.Success = true;
            });

            return result;
        }

        /// <summary>
        /// Indexes a single entity with the provided definition
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to index</typeparam>
        /// <param name="entity">The entity to index</param>
        /// <param name="definition">The index definition</param>
        /// <returns>An index result with the outcome of the indexing operation</returns>
        public IndexResult IndexEntity<TEntity>(TEntity entity, IIndexDefinition<TEntity> definition) where TEntity : class
        {
            var result = new IndexResult();
            result.ExecutionTime = Time(() =>
            {
                this.GetWriter().UpdateDocument(definition.GetIndex(entity), definition.Convert(entity));
                result.Count++;
                result.Success = true;
            });

            return result;
        }

        /// <summary>
        /// Removes all documents that match the given term in the index
        /// </summary>
        /// <param name="searchTerm">A search term to match</param>
        public void Remove(Term searchTerm)
        {
            this.GetWriter().DeleteDocuments(searchTerm);
        }

        public void Dispose()
        {
            if (!isDisposed && luceneWriter != null)
            {
                if (indexWriter.IndexOptions.OptimizeIndex)
                    luceneWriter.Optimize();

                luceneWriter.Commit();
                luceneWriter.Dispose();
                isDisposed = true;
            }
            luceneWriter = null;
        }

        protected IndexWriter GetWriter()
        {
            if (isDisposed)
                isDisposed = false;

            if (luceneWriter == null)
                luceneWriter = indexWriter.Create();

            return luceneWriter;
        }

        private static long Time(Action action)
        {
            var sw = Stopwatch.StartNew();
            action.Invoke();
            return sw.ElapsedMilliseconds;
        }
    }
}
