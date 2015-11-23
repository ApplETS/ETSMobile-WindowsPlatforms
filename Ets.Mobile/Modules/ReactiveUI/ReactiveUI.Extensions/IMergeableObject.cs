using System.Collections.Generic;

namespace ReactiveUI.Extensions
{
    public interface IMergeableObject<in T> : IMergeObject<T>, IEqualityComparer<T>
    {
    }
}