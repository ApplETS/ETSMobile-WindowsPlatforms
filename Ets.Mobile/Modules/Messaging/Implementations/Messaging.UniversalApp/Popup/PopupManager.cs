using Messaging.Interfaces.Common;
using Messaging.Interfaces.Popup;
using Splat;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Popups;
#if WINDOWS_PHONE_APP
using Windows.UI.ViewManagement;
#endif

namespace Messaging.UniversalApp.Popup
{
    public sealed class PopupManager : IPopupManager, IEnableLogger
    {
        private readonly object _queueMonitor = new object();
        private readonly object _showMonitor = new object();
        private IAsyncOperation<IUICommand> _currentDialogOperation;
        private readonly Queue<MessageDialog> _dialogQueue = new Queue<MessageDialog>();

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
#else
            await Task.FromResult(isBusy);
#endif
        }

        public async Task ShowMessage(string message)
        {
            await ShowMessage(message, null);
        }

        public async Task ShowMessage(string message, string title)
        {
            var md = string.IsNullOrEmpty(title) ? new MessageDialog(message) : new MessageDialog(message, title);
            await ShowDialogAsync(md);
        }

        public async Task ShowMessage(IMessagingContent content)
        {
            await ShowMessage(content.Message, content.Title);
        }

        public async Task<bool> ShowYesNo(string question)
        {
            var messageDialogResult = await ShowYesNo(question, null);
            return messageDialogResult;
        }

        public async Task<bool> ShowYesNo(string question, string title)
        {
            var messageDialogResult = string.IsNullOrEmpty(title)
                ? new MessageDialog(question)
                : new MessageDialog(question, title);

            var yesCommand = new UICommand("Yes");
            var noCommand = new UICommand("No");

            messageDialogResult.Commands.Add(yesCommand);
            messageDialogResult.Commands.Add(noCommand);

            var result = await ShowDialogAsync(messageDialogResult);

            return result == yesCommand;
        }

        public async Task<bool> ShowYesNo(IMessagingContent content)
        {
            var messageDialogResult = await ShowYesNo(content.Message, content.Title);
            return messageDialogResult;
        }
        
        private async Task<IUICommand> ShowDialogAsync(MessageDialog messageDialog)
        {
            IUICommand command = new UICommand("Ok");
            await Task.Run(async () =>
            {
                lock (_queueMonitor)
                {
                    _dialogQueue.Enqueue(messageDialog);
                }
                try
                {
                    while (true)
                    {
                        MessageDialog nextMessageDialog;
                        lock (_queueMonitor)
                        {
                            if (_dialogQueue.Count > 1)
                            {
                                Monitor.Wait(_queueMonitor);
                            }

                            nextMessageDialog = _dialogQueue.Peek();
                        }

                        var showing = false;
                        await
                            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                async () =>
                                {
                                    try
                                    {
                                        lock (_showMonitor)
                                        {
                                            showing = true;
                                            _currentDialogOperation = nextMessageDialog.ShowAsync();
                                        }

                                        command = await _currentDialogOperation;

                                        lock (_showMonitor)
                                            _currentDialogOperation = null;
                                    }
                                    catch (Exception e)
                                    {
                                        this.Log()
                                            .Error(
                                                $"[PopupManager][ShowDialogAsync] An Exception Occured:{e.Message}, StackTrace:{e.StackTrace}");
                                    }
                                    lock (_showMonitor)
                                    {
                                        showing = false;
                                        Monitor.Pulse(_showMonitor);
                                    }
                                });

                        lock (_showMonitor)
                        {
                            if (showing)
                            {
                                Monitor.Wait(_showMonitor);
                            }
                        }
                        return true;
                    }
                }
                finally
                {
                    lock (_queueMonitor)
                    {
                        _dialogQueue.Dequeue();
                        Monitor.Pulse(_queueMonitor);
                    }
                }
            });

            return command;
        }
    }
}