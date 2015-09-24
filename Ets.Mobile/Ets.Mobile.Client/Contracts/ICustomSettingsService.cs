using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI;
using Ets.Mobile.Entities.Signets.Interfaces;

namespace Ets.Mobile.Client.Contracts
{
    public interface ICustomSettingsService
    {
        /// <summary>
        /// Apply The Color of Each ICustomColor items according
        /// to the selector
        /// </summary>
        /// <param name="items">ICustomColor Items</param>
        /// <param name="semester">Semester in-which the item is</param>
        /// <param name="nameSelector">Course name Selector</param>
        /// <returns></returns>
        Task<IList<T>> ApplyColorOnItemsForSemester<T>(IList<T> items, string semester, Func<T, object> nameSelector) where T : class, ICustomColor;

        /// <summary>
        /// Apply The Color of one ICustomColor item according
        /// to the selector
        /// </summary>
        /// <param name="item">ICustomColor Item</param>
        /// <param name="semester">Semester in-which the item is</param>
        /// <param name="nameSelector">Course name Selector</param>
        /// <param name="color">Color of the item</param>
        /// <returns></returns>
        Task<T> ApplyColorOnItemsForSemester<T>(T item, string semester, Func<T, object> nameSelector, Color color) where T : class, ICustomColor;
    }
}
