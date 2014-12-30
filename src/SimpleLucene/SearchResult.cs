using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;

namespace SimpleLucene
{
    public class SearchResult<T>
    {
        private readonly IEnumerable<Document> documents;
        private readonly IResultDefinition<T> definition;
        private readonly int totalHits;

        private IEnumerable<T> entities;

        public SearchResult(IEnumerable<Document> documents, IResultDefinition<T> definition, int totalHits)
        {
            this.documents = documents;
            this.definition = definition;
            this.totalHits = totalHits;
        }

        public int TotalHits { get { return totalHits;  } }

        public IEnumerable<Document> Documents
        {
            get { return documents; }
        }

        public IEnumerable<T> Results
        {
            get
            {
                if (entities == null && definition != null)
                {
                    entities = documents.Select(doc => definition.Convert(doc)).ToList();
                }
                return entities;
            }
        }
    }
}
