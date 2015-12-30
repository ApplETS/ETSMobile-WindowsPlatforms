using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Settings;
using ReactiveUI;
using System;
using System.Reactive;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Ets.Mobile.ViewModel.Pages.Settings
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        public string VersionNumber => $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        public Uri SendFeedbackUri { get; private set; }
        public ReactiveCommand<Unit> SendLogFiles { get; private set; }

        public SettingsViewModel(IScreen screen) : base(screen, "Settings")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            SendFeedbackUri = new Uri("mailto:Club ApplETS<clubapplets@googlegroups.com>?subject=" +
#if WINDOWS_PHONE_APP
            "ÉtsMobile-WindowsPhone"
#else
            "ÉtsMobile-Windows"
#endif
            );

            SendLogFiles = ReactiveCommand.CreateAsyncTask(async _ =>
            {
#if WINDOWS_PHONE_APP
                // Define Recipient
                var sendTo = new EmailRecipient
                {
                    Name = "Club ApplETS",
                    Address = "clubapplets@googlegroups.com"
                };

                // Create email object
                var mail = new EmailMessage
                {
                    Subject = string.Format(Resources().GetString("SendLogFilesSubject") + " ", "ÉtsMobile-WindowsPhone"),
                    Body = Resources().GetString("SendLogFilesBody")
                };

                // Add recipients to the mail object
                mail.To.Add(sendTo);

                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("StorageFileEventListener");
                foreach (var files in await folder.GetFilesAsync())
                {
                    var attachments = new EmailAttachment {FileName = files.Name};
                    var accessStream = await files.OpenAsync(FileAccessMode.Read);
                    attachments.Data = RandomAccessStreamReference.CreateFromStream(accessStream);
                    mail.Attachments.Add(attachments);
                }

                // Open the share contract with Mail only:
                await EmailManager.ShowComposeNewEmailAsync(mail);
#else
                return Task.FromResult(Unit.Default);
#endif
            });


        }
    }
}