using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace SimpleLucene
{
    public interface IIndexDefinition<T> where T : class
    {
        Document Convert(T entity);
        Term GetIndex(T entity);
    }
}
