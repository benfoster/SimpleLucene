using Lucene.Net.Index;
using Lucene.Net.Search;
using NUnit.Framework;
using SimpleLucene.Tests.Data;
using SimpleLucene.Tests.Definitions;
using System.Linq;

namespace SimpleLucene.Tests
{
    [TestFixture]
    public class IndexServiceTestFixture
    {
        [Test]
        public void Can_index_a_single_entity()
        {
            var repo = new Repository();
            var product = repo.Products.First();

            var writer = new MemoryIndexWriter(true);

            using (var indexService = new IndexService(writer))
            {
                var result = indexService.IndexEntity(product, new ProductIndexDefinition());
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(0, result.Errors.Count);
            }
        }

        [Test]
        public void Can_index_a_collection_of_entities()
        {
            var repo = new Repository();

            var writer = new MemoryIndexWriter(true);
            using (var indexService = new IndexService(writer))
            {
                var result = indexService.IndexEntities(repo.Products, new ProductIndexDefinition());
                Assert.AreEqual(10, result.Count);
                Assert.AreEqual(0, result.Errors.Count);
            }
        }

        [Test]
        public void Can_use_the_same_index_writer_for_multiple_index_operations()
        {
            var repo = new Repository();

            var writer = new MemoryIndexWriter(true);           
            var indexService = new IndexService(writer);

            var p1p5 = repo.Products.Skip(0).Take(5);
            var result = indexService.IndexEntities(p1p5, new ProductIndexDefinition());
            Assert.AreEqual(5, result.Count);

            var p6p10 = repo.Products.Skip(5).Take(5);
            var result2 = indexService.IndexEntities(p6p10, new ProductIndexDefinition());
            Assert.AreEqual(5,result2.Count);

            var searcher = new MemoryIndexSearcher(writer.Directory, true);
            
            var searchResult = new SearchService(searcher).SearchIndex(new TermQuery(new Term("type", "product")));

            Assert.AreEqual(0, searchResult.Documents.Count(), "Index writer has not yet been committed so should return 0");
            // commits writer
            indexService.Dispose();

            searchResult = new SearchService(searcher).SearchIndex(new TermQuery(new Term("type", "product")));
            Assert.AreEqual(10, searchResult.Documents.Count());
        }
    }
}
