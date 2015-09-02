using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using StoreFramework.Messaging.Common;

namespace StoreFramework.Messaging.Popup
{
    public class PopupManager : IPopupManager
    {
        public PopupManager(ResourceLoader loader)
        {
            _resourceLoader = loader;
        }

        private readonly ResourceLoader _resourceLoader;
        
        public Task<bool> ConfirmAsync(string title, string message)
        {
            var tcs = new TaskCompletionSource<bool>();

            var yesCommand = new UICommand(_resourceLoader.GetString("Yes"));
            var noCommand = new UICommand(_resourceLoader.GetString("No"));

            var msgDialog = new MessageDialog(message);
            msgDialog.Commands.Add(yesCommand);
            msgDialog.Commands.Add(noCommand);

            tcs.SetResult(msgDialog.ShowAsync().GetResults() == yesCommand);

            return tcs.Task;
        }


        public async void ShowBusy(bool isBusy)
        {
#if WINDOWS_PHONE_APP
            var progressIndicator = StatusBar.GetForCurrentView().ProgressIndicator;
            if (isBusy)
            {
                progressIndicator.Text = "Récupération en cours";
                await progressIndicator.ShowAsync();
            }
            else
            {
                progressIndicator.Text = string.Empty;
                await progressIndicator.HideAsync();
            }
#endif
        }

        public async void ShowMessage(string message, string title = "")
        {
            MessageDialog messageDialog;
            if (!string.IsNullOrEmpty(title))
            {
                messageDialog = new MessageDialog(message, title);
                
            }
            else
            {
                messageDialog = new MessageDialog(message);
            }

            messageDialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            await messageDialog.ShowAsync();
        }

        public void ShowMessage(IMessagingContent content)
        {
            ShowMessage(content.Message, content.Title);
        }

        public async void ShowLocalizedMessage(string key)
        {
            await new MessageDialog(_resourceLoader.GetString(key)).ShowAsync();
        }
    }
}
