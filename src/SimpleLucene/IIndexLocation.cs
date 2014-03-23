using System;

namespace SimpleLucene
{
    public interface IIndexLocation : IEquatable<IIndexLocation>
    {
        string GetPath();
    }
}
