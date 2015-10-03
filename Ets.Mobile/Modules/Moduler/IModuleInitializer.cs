using Splat;

namespace Moduler
{
    public interface IModuleInitializer
    {
        void Initialize(IMutableDependencyResolver container);
    }
}
