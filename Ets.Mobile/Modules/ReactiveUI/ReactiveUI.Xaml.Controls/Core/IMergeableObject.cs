using System.Collections.Generic;

namespace ReactiveUI.Xaml.Controls.Core
{
    public interface IMergeableObject<in T> : IMergeObject<T>, IEqualityComparer<T>
    {
    }
}
