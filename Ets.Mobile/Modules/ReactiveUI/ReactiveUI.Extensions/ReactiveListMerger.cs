using System.Collections.Generic;
using System.Linq;

namespace ReactiveUI.Extensions
{
    public static class ReactiveListMerger
    {
        public static void MergeWith<T>(this ReactiveList<T> rl, ReactiveList<T> reactiveList)
            where T : IMergeableObject<T>
        {
            rl.ToArray().MergeWith(reactiveList);
        }

        public static void MergeWith<T>(this List<T> list, ReactiveList<T> reactiveList)
            where T : IMergeableObject<T>
        {
            list.ToArray().MergeWith(reactiveList);
        }

        public static void MergeWith<T>(this IEnumerable<T> enumerable, ReactiveList<T> reactiveList)
            where T : IMergeableObject<T>
        {
            enumerable.ToArray().MergeWith(reactiveList);
        }

        public static void MergeWith<T>(this ReactiveList<T> reactiveList, IEnumerable<T> enumerable)
            where T : IMergeableObject<T>
        {
            reactiveList.MergeWith(enumerable.ToArray());
        }

        public static void MergeWith<T>(this T[] array, ReactiveList<T> reactiveList)
            where T : IMergeableObject<T>
        {
            // Add
            var itemsToAdd = array.Except(array.Where(x => reactiveList.Contains(x, x))).ToArray();
            if (itemsToAdd.Any())
            {
                reactiveList.AddRange(itemsToAdd);
            }

            // Remove
            var itemsToRemove = array.Where(x => !array.Contains(x, x)).ToArray();
            if (itemsToRemove.Any())
            {
                reactiveList.RemoveAll(itemsToRemove);
            }

            // Merge
            foreach (var item in array.Where(x => array.Contains(x, x)))
            {
                var mergeItem = array.First(x => x.Equals(x, item));
                item.MergeWith(mergeItem);
            }
        }

        public static void MergeWith<T>(this ReactiveList<T> reactiveList, T[] array)
            where T : IMergeableObject<T>
        {
            // Add
            var itemsToAdd = array.Except(array.Where(x => reactiveList.Contains(x, x))).ToArray();
            if (itemsToAdd.Any())
            {
                reactiveList.AddRange(itemsToAdd);
            }

            // Remove
            var itemsToRemove = array.Where(x => !array.Contains(x, x)).ToArray();
            if (itemsToRemove.Any())
            {
                reactiveList.RemoveAll(itemsToRemove);
            }

            // Merge
            foreach (var item in array.Where(x => array.Contains(x, x)))
            {
                var mergeItem = array.First(x => x.Equals(x, item));
                item.MergeWith(mergeItem);
            }
        }
    }
}