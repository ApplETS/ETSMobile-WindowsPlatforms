using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;

namespace Ets.Mobile.ViewModel
{
    public class ViewServiceBase : ViewModelBase
    {
        #region ViewService

        //private CoreDispatcher _coreDispatcher;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(() => IsBusy, ref _isBusy, value); }
        }

#if WINDOWS_PHONE_APP
        public void SetStatusBar(StatusBar statusBar)
        {
            _statusBar = statusBar;
        }
        public StatusBar GetStatusBar()
        {
            return _statusBar;
        }
        public StatusBar _statusBar;
#endif

        public async void ShowError(string message)
        {
            await new MessageDialog(message).ShowAsync();
        }

        public Task<bool> ConfirmAsync(string title, string message)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            UICommand yesCommand = new UICommand("Yes"),
              noCommand = new UICommand("No");

            MessageDialog msgDialog = new MessageDialog(message);
            msgDialog.Commands.Add(yesCommand);
            msgDialog.Commands.Add(noCommand);

            tcs.SetResult(msgDialog.ShowAsync().GetResults() == yesCommand);

            return tcs.Task;
        }

        public async Task ExecuteBusyActionAsync(Func<Task> func)
        {
            ShowBusy(true);
            try
            {
                await func();
            }
            catch (Exception e)
            {
                ShowError(e.ToString());
            }
            finally
            {
                ShowBusy(false);
            }
        }

        public void ShowBusy(bool isBusy)
        {
            if (_isBusy == isBusy) return;

            IsBusy = isBusy;
        }

        public string Message
        {
            get { return _message; }
            set { Set(() => Message, ref _message, value); }
        }
        private string _message;

        #endregion
    }
}
