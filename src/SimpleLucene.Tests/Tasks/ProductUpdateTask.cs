using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleLucene.Tests.Entities;
using SimpleLucene.IndexManagement;
using System.IO;

namespace SimpleLucene.Tests.Tasks
{
    public class ProductUpdateTask : IIndexTask
    {
        protected readonly Product entity;

        public ProductUpdateTask(Product entity)
        {
            this.entity = entity;
        }
        
        public override bool Equals(object obj)
        {
            return (Equals(obj as ProductUpdateTask));
        }

        public override int GetHashCode() {
            if (Equals(entity.Id, default(int)))
                return base.GetHashCode();
            return entity.Id.GetHashCode();
        }

        private static bool IsTransient(Product p)
        {
            return p != null &&
                Equals(p.Id, default(int));
        }

        public virtual bool Equals(ProductUpdateTask other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this.entity) && !IsTransient(other.entity))
                return Equals(entity.Id, other.entity.Id);


            return false;
        }

        #region IIndexTask Members

        public void Execute(IIndexService indexService)
        {
            throw new NotImplementedException();
        }

        public IndexOptions IndexOptions { get; set; }

        #endregion       
    }
}
