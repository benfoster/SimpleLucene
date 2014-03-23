using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimpleLucene
{
    public class IndexResult
    {
        private List<string> errors;

        public IndexResult()
        {
            errors = new List<string>();
        }

        public bool Success { get; set; }
        public int Count { get; set; }
        public long ExecutionTime { get; set; }

        public void AddError(string error)
        {
            errors.Add(error);
        }

        public ReadOnlyCollection<string> Errors { get { return errors.AsReadOnly(); } }
    }
}
