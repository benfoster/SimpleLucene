using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SimpleLucene.Tests.Entities;
using SimpleLucene.IndexManagement;
using SimpleLucene.Tests.Definitions;
using System.IO;

namespace SimpleLucene.Tests.Tasks
{
    [TestFixture]
    public class TaskEqualityTestFixture
    {
        [Test]
        public void Two_of_same_task_type_with_different_entities_should_not_be_equal()
        {
            var p1 = new Product { Id = 1 };
            var p2 = new Product { Id = 2 };

            var task1 = new ProductUpdateTask(p1);
            var task2 = new ProductUpdateTask(p2);

            Assert.AreNotEqual(task1, task2);
        }

        [Test]
        public void Two_tasks_with_same_entity_should_be_equal()
        {
            var p1 = new Product { Id = 1 };

            var task1 = new ProductUpdateTask(p1);
            var task2 = new ProductUpdateTask(p1);

            Assert.AreEqual(task1, task2);
        }

        [Test]
        public void Two_different_task_types_with_same_entity_should_not_be_equal()
        {
            var p1 = new Product { Id = 1 };

            var task1 = new ProductUpdateTask(p1);
            var task2 = new EntityUpdateTask<Product>(p1, new ProductIndexDefinition(), new TestIndexLocation());

            Assert.AreNotEqual(task1, task2);
        }

        [Test]
        public void Index_queue_resolves_equality()
        {
            var p1 = new Product { Id = 1 };

            var task1 = new ProductUpdateTask(p1);

            IndexQueue.Instance.Queue(task1);

            var task2 = new ProductUpdateTask(p1);

            Assert.IsTrue(IndexQueue.Instance.Contains(task2));
        }
    }
}
