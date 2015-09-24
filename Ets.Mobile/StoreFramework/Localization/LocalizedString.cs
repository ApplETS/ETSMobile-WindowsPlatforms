using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;
using Splat;

namespace StoreFramework.Localization
{
    public class LocalizedStrings
    {
        private static readonly ResourceLoader Resources = Locator.Current.GetService<ResourceLoader>();

        public string this[string name] => Resources.GetString(name);
    }
}
