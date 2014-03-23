using Lucene.Net.Index;
using System;

namespace SimpleLucene.IndexManagement
{
    /// <summary>
    /// A task for removing an entity from an index
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to remove</typeparam>
    public class EntityDeleteTask<TEntity> : IIndexTask where TEntity : class
    {
        protected readonly Term term;

        public IndexOptions IndexOptions { get; set; }

        public EntityDeleteTask(IIndexLocation indexLocation, string fieldName, string value)
        {
            if (indexLocation == null)
            {
                throw new ArgumentNullException("indexLocation");
            }

            if (string.IsNullOrEmpty("fieldName"))
            {
                throw new ArgumentNullException("fieldName");
            }

            if (string.IsNullOrEmpty("value"))
            {
                throw new ArgumentNullException("value");
            }

            term = new Term(fieldName, value);
            IndexOptions = new IndexOptions { IndexLocation = indexLocation };
        }

        public void Execute(IIndexService indexService)
        {
            if (indexService == null)
            {
                throw new ArgumentNullException("indexService");
            }
            
            indexService.Remove(term);
        }
    }
}
