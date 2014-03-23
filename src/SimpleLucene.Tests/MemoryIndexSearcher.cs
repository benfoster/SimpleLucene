using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace SimpleLucene.Tests
{
    public class MemoryIndexSearcher : IIndexSearcher
    {
        private readonly bool readOnly;
        private readonly RAMDirectory directory;

        public MemoryIndexSearcher(RAMDirectory directory, bool readOnly) {
            this.readOnly = readOnly;
            this.directory = directory;
        }

        public Searcher Create() {
            return new IndexSearcher(directory, readOnly);
        }
    }
}
