using Lucene.Net.Documents;
using System;

namespace SimpleLucene
{
    /// <summary>
    /// Extension methods for <see cref="Lucene.Net.Documents.Document"/>.
    /// </summary>
    public static class DocumentExtensions
    {
        public static T GetValue<T>(this Document document, string field)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            if (string.IsNullOrEmpty("field"))
            {
                throw new ArgumentNullException("field");
            }

            var value = document.Get(field);

            if (value != null)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            return default(T);
        }
    }
}
