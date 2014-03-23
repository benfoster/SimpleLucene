using System;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleLucene.IndexManagement;
using SimpleLucene.Tests.Definitions;
using SimpleLucene.Tests.Entities;

namespace SimpleLucene.Tests
{
    [TestFixture]
    public class IndexQueueTestFixture
    {
        [Test] 
        public void Can_add_index_task_to_queue()
        {
            var task = new EntityUpdateTask<Product>(new Product(),
                new ProductIndexDefinition(), new TestIndexLocation());

            IndexQueue.Instance.Queue(task);

            Assert.IsTrue(IndexQueue.Instance.Contains(task));
            
            IIndexTask fromQueue;
            IndexQueue.Instance.TryDequeue(out fromQueue);

            Assert.AreEqual(task, fromQueue);
        }

        
        [Test]
        public void Can_resolve_index_services_when_processing_index_queue()
        {
            var t1 = new EntityUpdateTask<Product>(new Product(), new ProductIndexDefinition(), new TestIndexLocation("products"));
            var t2 = new EntityUpdateTask<Product>(new Product(), new ProductIndexDefinition(), new TestIndexLocation("products"));
            var t3 = new EntityUpdateTask<Product>(new Product(), new ProductIndexDefinition(), new TestIndexLocation("products2"));

            Action<IIndexTask> queue = t => IndexQueue.Instance.Queue(t);

            queue(t1);
            queue(t2);
            queue(t3);

            var services = new HashSet<IIndexService>();

            IIndexTask task;
            while (IndexQueue.Instance.TryDequeue(out task))
            {
                var service = services.FindWithOptions(task.IndexOptions);

                if (service == null) {
                    service = new IndexService(new DirectoryIndexWriter(task.IndexOptions.IndexLocation.GetDirectory(), task.IndexOptions.RecreateIndex));
                    services.Add(service);
                }

                // process the task
            }

            Assert.IsTrue(services.Count == 2);
        }
    }
}
