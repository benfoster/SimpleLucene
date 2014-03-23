using Lucene.Net.Index;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;

namespace SimpleLucene
{
    /// <summary>
    /// A base class for deriving custom query objects
    /// </summary>
    public abstract class QueryBase
    {
        private readonly BooleanQuery baseQuery = new BooleanQuery();

        protected QueryBase() { }

        protected QueryBase(Query query)
        {
            AddQuery(query);
        }

        public Query Query
        {
            get { return baseQuery; }
        }

        protected void AddQuery(Query query)
        {
            AddQuery(query, Occur.MUST);
        }

        protected void AddQuery(Query query, Occur occur)
        {
            if (query != null)
            {
                baseQuery.Add(query, occur);
            }
        }

        protected Query GetQueryFromList<T>(IList<T> list, string key)
        {
            return GetQueryFromList(list, (i) => key, i => i.ToString());
        }

        protected Query GetQueryFromList<T>(IList<T> list, Func<T, string> key, Func<T, string> value)
        {
            if (list.Count > 1)
            {
                var query = new BooleanQuery();
                foreach (var id in list)
                {
                    query.Add(TermQuery(key(id), value(id)), Occur.MUST);
                }
                return query;
            }
            if (list.Count == 1)
            {
                return TermQuery(key(list[0]), value(list[0]));
            }
            return null;
        }

        protected Query TermQuery(string key, string value)
        {
            return new TermQuery(new Term(key, value));
        }
    }
}
