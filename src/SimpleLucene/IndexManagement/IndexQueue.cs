using System.Collections.Concurrent;
using System.Linq;

namespace SimpleLucene.IndexManagement
{
    /// <summary>
    /// Provides a simple queue for index tasks
    /// </summary>
    public class IndexQueue
    {
        static readonly IndexQueue instance = new IndexQueue();
        static readonly object padlock = new object();

        private ConcurrentQueue<IIndexTask> cq = new ConcurrentQueue<IIndexTask>();

        private IndexQueue() { }

        public static IndexQueue Instance
        {
            get { return instance; }
        }

        public void Queue(IIndexTask task)
        {
            cq.Enqueue(task);
        }

        public bool TryDequeue(out IIndexTask task)
        {
            return cq.TryDequeue(out task);
        }

        public bool Contains(IIndexTask instance)
        {
            lock (padlock)
            {
                return cq.Contains(instance);
            }
        }
    }
}
