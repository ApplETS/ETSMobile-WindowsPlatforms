namespace Messaging.Interfaces.Common
{
    public interface IExceptionMessagingContent : IMessagingContent
    {
        System.Exception Exception { get; }
    }
}