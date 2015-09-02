using Windows.UI.Xaml.Media;
#if !WINDOWS_APP
using Coding4Fun.Toolkit.Controls;
#endif
using StoreFramework.Messaging.Common;
using StoreFramework.Themes;

namespace StoreFramework.Messaging.Notifications
{
    public class InAppNotificationManager : INotificationManager
    {
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
            toast.Background = new SolidColorBrush(EtsColors.MediumBrush);
            toast.Show();
#endif
        }
    }
}
