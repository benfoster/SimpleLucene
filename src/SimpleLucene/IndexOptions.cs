using System;
using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;

namespace SimpleLucene
{
    /// <summary>
    /// Used as a definition for resolving indexwriters
    /// </summary>
    public class IndexOptions : IEquatable<IndexOptions>
    {
        public IndexOptions()
        {
            this.Analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            this.Attributes = new Dictionary<string, object>();
        }

        public bool OptimizeIndex { get; set; }
        public bool RecreateIndex { get; set; }
        public IIndexLocation IndexLocation { get; set; }
        public Analyzer Analyzer { get; set; }
        public IDictionary<string, object> Attributes { get; set; }

        public bool Equals(IndexOptions other)
        {
            if (other == null)
                return false;

            return (AreEqual(other.OptimizeIndex, this.OptimizeIndex))
                && (AreEqual(other.RecreateIndex, this.RecreateIndex))
                && (AreEqual(other.IndexLocation, this.IndexLocation))
                && (AreEqual(other.Analyzer.GetType(), this.Analyzer.GetType()))
                && (CompareAttributes(other.Attributes));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IndexOptions);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected bool AreEqual(object obj1, object obj2)
        {
            if (obj1 == null && obj2 == null)
                return true;

            return (obj1.Equals(obj2));
        }

        protected bool CompareAttributes(IDictionary<string, object> other)
        {
            foreach (var item in this.Attributes)
            {
                object obj;
                if (other.TryGetValue(item.Key, out obj))
                {
                    if (!AreEqual(obj, item.Value))
                        return false;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
