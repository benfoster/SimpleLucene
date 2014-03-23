using System;

namespace SimpleLucene.IndexManagement
{
    /// <summary>
    /// An task for reindexing an entity
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to index</typeparam>
    public class EntityUpdateTask<TEntity> : IIndexTask where TEntity : class
    {
        protected readonly TEntity entity;
        protected readonly IIndexDefinition<TEntity> definition;
        
        public IndexOptions IndexOptions { get; set; }

        public EntityUpdateTask(TEntity entity, IIndexDefinition<TEntity> definition, IIndexLocation indexLocation)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (definition == null)
            {
                throw new ArgumentNullException("definition");
            }

            if (indexLocation == null)
            {
                throw new ArgumentNullException("indexLocation");
            }

            this.entity = entity;
            this.definition = definition;
            this.IndexOptions = new IndexOptions { IndexLocation = indexLocation };
        }

        public void Execute(IIndexService indexService)
        {
            if (indexService == null)
            {
                throw new ArgumentNullException("indexService");
            }
            
            var result = indexService.IndexEntity(entity, definition);
        }
    }
}
