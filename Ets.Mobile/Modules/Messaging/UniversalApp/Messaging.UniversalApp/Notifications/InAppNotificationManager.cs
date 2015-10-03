using Windows.UI;
using Windows.UI.Xaml.Media;
using Messaging.Interfaces.Common;
using Messaging.Interfaces.Notifications;
#if !WINDOWS_APP
using Coding4Fun.Toolkit.Controls;
#endif

namespace Messaging.UniversalApp.Notifications
{
    public sealed class InAppNotificationManager : INotificationManager
    {
        private Color DefaultBackground { get; }

        public InAppNotificationManager(Color color)
        {
            DefaultBackground = color;
        }

        public void Notify(IMessagingContent messageContent)
        {
#if !WINDOWS_APP
            var toast = new ToastPrompt();
            if (!string.IsNullOrEmpty(messageContent.Title))
            {
                toast.Title = messageContent.Title;
            }
            toast.TextOrientation = Windows.UI.Xaml.Controls.Orientation.Vertical;
            toast.Message = messageContent.Message;
            toast.TextWrapping = Windows.UI.Xaml.TextWrapping.WrapWholeWords;
            toast.Background = new SolidColorBrush(DefaultBackground);
            toast.Show();
#else
            // Not Implemented for WinRT yet
#endif
        }
    }
}