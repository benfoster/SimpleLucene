using System;
using System.Collections.Generic;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace SimpleLucene
{
    public interface IIndexService : IDisposable
    {
        IIndexWriter IndexWriter { get; }
        IndexResult IndexEntities<TEntity>(IEnumerable<TEntity> entities, IIndexDefinition<TEntity> definition) where TEntity : class;
        IndexResult IndexEntities<TEntity>(IEnumerable<TEntity> entities, Func<TEntity, Document> converter);
        IndexResult IndexEntity<TEntity>(TEntity entity, IIndexDefinition<TEntity> definition) where TEntity : class;
        void Remove(Term searchTerm);
    }
}
