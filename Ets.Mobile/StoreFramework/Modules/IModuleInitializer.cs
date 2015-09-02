using Splat;

namespace StoreFramework.Composite
{
    public interface IModuleInitializer
    {
        void Initialize(IMutableDependencyResolver container);
    }
}
