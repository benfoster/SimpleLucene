using Lucene.Net.Index;

namespace SimpleLucene
{
    public interface IIndexWriter
    {
        IndexWriter Create();
        IndexOptions IndexOptions { get; set; }
    }
}
