using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;

namespace SimpleLucene.Tests
{
    [TestFixture]
    public class IndexOptionsTestFixture
    {
        [Test]
        public void Two_default_instances_are_equal()
        {
            var io1 = new IndexOptions();
            var io2 = new IndexOptions();

            Assert.AreEqual(io1, io2);
        }

        [Test]
        public void Two_references_to_the_same_object_are_equal()
        {
            var io1 = new IndexOptions { IndexLocation = new TestIndexLocation("products") };
            var io2 = io1;
            Assert.AreEqual(io1, io2);
        }

        [Test]
        public void Two_index_options_with_same_locations_are_equal()
        {
            var io1 = new IndexOptions { IndexLocation = new TestIndexLocation("products") };
            var io2 = new IndexOptions { IndexLocation = new TestIndexLocation("products") };
            Assert.AreEqual(io1, io2);
        }

        [Test]
        public void Two_index_options_with_different_locations_are_not_equal() {
            var io1 = new IndexOptions { IndexLocation = new TestIndexLocation("products") };
            var io2 = new IndexOptions { IndexLocation = new TestIndexLocation("products2") };
            Assert.AreNotEqual(io1, io2);
        }

        [Test]
        public void Two_index_options_with_different_recreate_index_are_not_equal()
        {
            var io1 = new IndexOptions { IndexLocation = new TestIndexLocation("products"), RecreateIndex = true };
            var io2 = new IndexOptions { IndexLocation = new TestIndexLocation("products"), RecreateIndex = false };
            Assert.AreNotEqual(io1, io2);
        }

        [Test]
        public void Two_index_options_with_different_optimize_index_are_not_equal()
        {
            var io1 = new IndexOptions { IndexLocation = new TestIndexLocation("products"), OptimizeIndex = true };
            var io2 = new IndexOptions { IndexLocation = new TestIndexLocation("products"), OptimizeIndex = false };
            Assert.AreNotEqual(io1, io2);
        }

        [Test]
        public void Two_index_options_with_different_analyzers_are_not_equal()
        {
            var io1 = new IndexOptions { IndexLocation = new TestIndexLocation("products") }; // default Standard Analyzer
            var io2 = new IndexOptions { IndexLocation = new TestIndexLocation("products"), Analyzer = new SimpleAnalyzer() };
            Assert.AreNotEqual(io1, io2);
        }

        [Test]
        public void Two_index_options_with_same_attributes_are_equal()
        {
            var io1 = new IndexOptions();
            io1.Attributes.Add("somekey", "somevalue");
            io1.Attributes.Add("somecount", 100);
            var io2 = new IndexOptions();
            io2.Attributes.Add("somekey", "somevalue");
            io2.Attributes.Add("somecount", 100);
            Assert.AreEqual(io1, io2);
        }

        [Test]
        public void Two_index_options_with_different_attributes_are_not_equal()
        {
            var io1 = new IndexOptions();
            io1.Attributes.Add("somekey", "somevalue");
            var io2 = new IndexOptions();
            io2.Attributes.Add("someotherkey", "someothervalue");
            Assert.AreNotEqual(io1, io2);
        }

        [Test]
        public void Two_index_options_with_same_attributes_but_diff_values_are_not_equal()
        {
            var io1 = new IndexOptions();
            io1.Attributes.Add("somekey", "somevalue");
            io1.Attributes.Add("somecount", 100);
            var io2 = new IndexOptions();
            io2.Attributes.Add("somekey", "somevalue2");
            io2.Attributes.Add("somecount", 150);
            Assert.AreNotEqual(io1, io2);
        }
    }
}

