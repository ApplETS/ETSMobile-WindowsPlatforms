using System.Threading.Tasks;

namespace Messaging.Interfaces.Common
{
    public interface IShowMessage
    {
        void ShowBusy(bool isBusy);
        Task ShowMessage(string message);
        Task ShowMessage(string message, string title);
        Task ShowMessage(IMessagingContent content);
        Task<bool> ShowYesNo(string question);
        Task<bool> ShowYesNo(string question, string title);
        Task<bool> ShowYesNo(IMessagingContent content);
    }
}