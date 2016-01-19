using System.Globalization;
using Windows.UI;
using Messaging.Interfaces.Common;
using Messaging.Interfaces.Notifications;
using Windows.UI.Xaml.Media;
#if !WINDOWS_APP
using Coding4Fun.Toolkit.Controls;
#endif

namespace Messaging.UniversalApp.Notifications
{
    public sealed class InAppNotificationManager : INotificationManager
    {
        private Color DefaultBackground { get; }

        public InAppNotificationManager(string hexColor)
        {
            DefaultBackground = new Color
            {
                A = byte.Parse(hexColor.Substring(7, 2), NumberStyles.AllowHexSpecifier),
                R = byte.Parse(hexColor.Substring(1, 2), NumberStyles.AllowHexSpecifier),
                G = byte.Parse(hexColor.Substring(3, 2), NumberStyles.AllowHexSpecifier),
                B = byte.Parse(hexColor.Substring(5, 2), NumberStyles.AllowHexSpecifier)
            };
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