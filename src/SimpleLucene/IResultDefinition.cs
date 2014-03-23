using Lucene.Net.Documents;

namespace SimpleLucene
{
    public interface IResultDefinition<T>
    {
        T Convert(Document document);
    }
}
