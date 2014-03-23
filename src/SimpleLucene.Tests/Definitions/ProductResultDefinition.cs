using SimpleLucene.Tests.Entities;

namespace SimpleLucene.Tests.Definitions
{
    public class ProductResultDefinition : IResultDefinition<Product>
    {
        public Product Convert(Lucene.Net.Documents.Document document)
        {
            return new Product
            {
                Id = document.GetValue<int>("id"),
                Name = document.GetValue<string>("name")
            };
        }
    }
}
