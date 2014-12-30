using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Index;
using System.IO;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;

namespace SimpleLucene.Tests
{
    public class MemoryIndexWriter : IIndexWriter
    {
        public bool CreateIndex {get; private set;}

        public RAMDirectory Directory { get; set; }

        public MemoryIndexWriter(bool createIndex)
        {
            this.Directory = new RAMDirectory();
            this.CreateIndex = createIndex;
            this.IndexOptions = new IndexOptions();
        }

        public IndexOptions IndexOptions { get; set; }

        public IndexWriter Create()
        {
            var ramDirectory = new RAMDirectory();
            this.Directory = ramDirectory;
            return new IndexWriter(ramDirectory,
                                        new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29),
                                        CreateIndex,
                                        IndexWriter.MaxFieldLength.UNLIMITED);
        }
    }
}
