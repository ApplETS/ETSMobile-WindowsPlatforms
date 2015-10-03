namespace Messaging.Interfaces.Common
{
    public interface IShowMessage
    {
        void ShowBusy(bool isBusy);
        void ShowMessage(string message, string title);
        void ShowMessage(IMessagingContent content);
        void ShowLocalizedMessage(string key);
    }
}
