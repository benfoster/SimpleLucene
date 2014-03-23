using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleLucene.Tests.Entities;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace SimpleLucene.Tests.Definitions
{
    public class ProductIndexDefinition : IIndexDefinition<Product>
    {
        public Document Convert(Product p) {
            var document = new Document();
            document.Add(new Field("type", "product", Field.Store.NO, Field.Index.NOT_ANALYZED));
            document.Add(new Field("id", p.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("name", p.Name, Field.Store.YES, Field.Index.ANALYZED));
            return document;
        }

        public Term GetIndex(Product p) {
            return new Term("id", p.Id.ToString());
        }
    }
}
