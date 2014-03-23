using System;
using Lucene.Net.Documents;

namespace SimpleLucene
{
    public class DelegateSearchResultDefinition<T> : IResultDefinition<T>
    {
        private readonly Func<Document, T> converter;
        public DelegateSearchResultDefinition(Func<Document, T> converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }
            
            this.converter = converter;
        }

        public T Convert(Document document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            return converter(document);
        }
    }
}
