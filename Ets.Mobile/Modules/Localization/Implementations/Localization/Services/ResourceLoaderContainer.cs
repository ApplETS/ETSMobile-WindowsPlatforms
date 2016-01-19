using Localization.Interface.Contracts;
using Windows.ApplicationModel.Resources;

namespace Localization.Services
{
    public sealed class ResourceLoaderContainer : IResourceContainer
    {

        private readonly ResourceLoader _resourceLoader;

        public ResourceLoaderContainer(ResourceLoader loader)
        {
            _resourceLoader = loader;
        }

        public string GetStringForKey(string key)
        {
            return _resourceLoader.GetString(key);
        }
    }
}