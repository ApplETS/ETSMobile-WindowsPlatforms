using Splat;

namespace Ets.Mobile.Shared.Tests
{
    public class MockBase
    {
        protected IMutableDependencyResolver locator = Locator.CurrentMutable;
    }
}