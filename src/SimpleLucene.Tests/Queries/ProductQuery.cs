using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Search;
using Lucene.Net.Index;

namespace SimpleLucene.Tests.Queries
{
    public class ProductQuery : QueryBase
    {
        public ProductQuery(Query query) : base(query) { }
        public ProductQuery() { }

        public ProductQuery WithId(int id)
        {
            if (id > 0)
            {
                var query = new TermQuery(new Term("id", id.ToString()));
                this.AddQuery(query);
            }
            return this;
        }
    }
}
