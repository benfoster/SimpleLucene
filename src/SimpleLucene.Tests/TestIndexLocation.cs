using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleLucene.Tests
{
    public class TestIndexLocation : BaseIndexLocation {

        private string indexLocation;

        public TestIndexLocation(string indexDirectory = "")
        {
            indexLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "indexes");
            
            if (!string.IsNullOrEmpty(indexDirectory)) 
                indexLocation = Path.Combine(indexLocation, indexDirectory);
        }
        
        public override string GetPath() {
            return indexLocation;
        }
    }
}
