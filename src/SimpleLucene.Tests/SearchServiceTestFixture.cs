using System.Linq;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using NUnit.Framework;
using SimpleLucene.Tests.Data;
using SimpleLucene.Tests.Definitions;
using SimpleLucene.Tests.Queries;
using SimpleLucene.Tests.Entities;

namespace SimpleLucene.Tests
{
    [TestFixture]
    public class SearchServiceTestFixture
    {
        RAMDirectory directory;
        
        [TestFixtureSetUp]
        public void SetUp()
        {
            var repo = new Repository();

            var writer = new MemoryIndexWriter(true);
            using (var indexService = new IndexService(writer))
            {
                indexService.IndexEntities(repo.Products, new ProductIndexDefinition());
            }
            directory = writer.Directory;
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            directory.Close();
        }

        [Test]
        public void Can_search_index()
        {
            var indexSearcher = new MemoryIndexSearcher(directory, true);
            var searchService = new SearchService(indexSearcher);

            var result = searchService.SearchIndex(new ProductQuery().WithId(5).Query);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Documents.Count());
        }

        [Test]
        public void Can_transform_results_with_result_definition()
        {
            var indexSearcher = new MemoryIndexSearcher(directory, true);
            var searchService = new SearchService(indexSearcher);

            var result = searchService.SearchIndex(
                new TermQuery(new Term("type", "product")),
                new ProductResultDefinition()
            );
            
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Results.Count());
        }

        [Test]
        public void Can_transform_results_with_delegate_definition()
        {
            var indexSearcher = new MemoryIndexSearcher(directory, true);
            var searchService = new SearchService(indexSearcher);

            var result = searchService.SearchIndex(
                new TermQuery(new Term("id", "1")),
                doc => {
                    return new Product { Name = doc.Get("name") };
                });

            Assert.AreEqual(result.Results.Count(), 1);
            Assert.AreEqual(result.Results.First().Name, "Football");
        }

        [Test]
        public void Can_filter_and_sort_index()
        {
            var indexSearcher = new MemoryIndexSearcher(directory, true);
            var searchService = new SearchService(indexSearcher);

            var result = searchService.SearchIndex(
                new TermQuery(new Term("type", "product")),
                new ProductResultDefinition(),
                new PrefixFilter(new Term("name", "f")),
                new Sort(new SortField("id", SortField.INT, true))
            );

            var repo = new Repository();

            //Filtered:
            Assert.AreEqual(2, result.Documents.Count());
            //Sorted:
            Assert.AreEqual(repo.Products.First(p => p.Id == 7).Id, result.Results.ElementAt(0).Id);
        }

        [Test]
        public void Can_return_limited_number()
        {
            var indexSearcher = new MemoryIndexSearcher(directory, true);
            var searchService = new SearchService(indexSearcher);

            var numberResult = 1;

            var result = searchService.SearchIndex(
                new TermQuery(new Term("type", "product")),
                new ProductResultDefinition(),
                new PrefixFilter(new Term("name", "f")),
                new Sort(new SortField("id", SortField.INT, true)),
                numberResult
            );

            var repo = new Repository();

            //Filtered:
            Assert.AreEqual(numberResult, result.Documents.Count());
            //Sorted:
            Assert.AreEqual(repo.Products.First(p => p.Id == 7).Id, result.Results.ElementAt(0).Id);
        }
    }
}
