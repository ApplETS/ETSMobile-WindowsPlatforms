using Ets.Mobile.ViewModel.Bases;
using Ets.Mobile.ViewModel.Contracts.Settings;
using ReactiveUI;
using System;
using System.IO;
using System.IO.Compression;
using System.Reactive;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
#if WINDOWS_PHONE_APP
using Windows.ApplicationModel.Email;
#endif
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
                
                var attachments = new EmailAttachment
                {
                    FileName = "LogFiles.zip",
                    Data = await SendLogFilesImpl()
                };
                mail.Attachments.Add(attachments);

                // Open the share contract with Mail only:
                await EmailManager.ShowComposeNewEmailAsync(mail);
#else
                return await Task.FromResult(Unit.Default);
#endif
            });

            SendLogFiles.ThrownExceptions.Subscribe(ex =>
            {
                UserError.Throw(ex.Message, ex);
            });
        }

        private async Task<IRandomAccessStreamReference> SendLogFilesImpl()
        {
            var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("StorageFileEventListener");
            using (var zipStream = new MemoryStream())
            {
                var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true);
                var files = await folder.GetFilesAsync();
                foreach (var fileToCompress in files)
                {
                    var fileOpenAsync = await fileToCompress.OpenAsync(FileAccessMode.Read);
                    if (fileOpenAsync.Size != 0)
                    {
                        var buffer = (await FileIO.ReadBufferAsync(fileToCompress)).ToArray();

                        // Create a zip archive entry
                        var entry = archive.CreateEntry(fileToCompress.Name);

                        // And write the contents to it
                        using (var entryStream = entry.Open())
                        {
                            await entryStream.WriteAsync(buffer, 0, buffer.Length);
                        }
                    }
                }
                var file = await SaveStreamToFile(zipStream, "LogFiles.zip");
                return RandomAccessStreamReference.CreateFromFile(file);
            }
        }

        private async Task<StorageFile> SaveStreamToFile(Stream streamToSave, string fileName)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                streamToSave.Position = 0;
                await streamToSave.CopyToAsync(fileStream);
            }
            return file;
        }
    }
}