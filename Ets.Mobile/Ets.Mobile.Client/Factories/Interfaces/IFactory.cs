namespace Ets.Mobile.Client.Factories.Interfaces
{
    public interface IFactory<in TInput, out TOutput>
    {
        TOutput Create(TInput result);
    }
}
