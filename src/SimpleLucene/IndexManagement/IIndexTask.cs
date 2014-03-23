
namespace SimpleLucene.IndexManagement
{
    /// <summary>
    /// Interface for index tasks
    /// </summary>
    public interface IIndexTask
    {
        void Execute(IIndexService indexService);
        IndexOptions IndexOptions { get; set; }
    }
}
