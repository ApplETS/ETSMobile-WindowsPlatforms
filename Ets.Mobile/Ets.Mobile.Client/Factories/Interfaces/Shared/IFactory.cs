namespace Ets.Mobile.Client.Factories.Interfaces.Shared
{
    public interface IFactory<in TInput, out TOutput>
    {
        TOutput Create(TInput result);
    }
}