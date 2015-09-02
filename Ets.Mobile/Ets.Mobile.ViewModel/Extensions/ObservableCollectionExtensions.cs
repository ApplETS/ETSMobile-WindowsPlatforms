using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ets.Mobile.ViewModel.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static ObservableCollection<T> AsObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }
    }
}
