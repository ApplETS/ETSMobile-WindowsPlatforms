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
using Windows.Storage;
using Windows.Storage.Streams;
#if WINDOWS_PHONE_APP || WINDOWS_UWP
using Windows.ApplicationModel.Email;
#endif

namespace Ets.Mobile.ViewModel.Pages.Settings
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        public SettingsViewModel(IScreen screen) : base(screen, "Settings")
        {
            OnViewModelCreation();
        }

        protected sealed override void OnViewModelCreation()
        {
            SendFeedbackUri = new Uri("mailto:applets@ens.etsmtl.ca?subject=" +
#if WINDOWS_PHONE_APP
            "ÉtsMobile-WindowsPhone"
#elif WINDOWS_APP
            "ÉtsMobile-Windows"
#elif WINDOWS_UWP
            "ÉtsMobile-UWP"
#endif
            );

            SendLogFiles = ReactiveCommand.CreateAsyncTask(async _ => await SendLogFilesImpl());

            SendLogFiles.ThrownExceptions.Subscribe(ex =>
            {
                UserError.Throw(ex.Message, ex);
            });
        }

        private async Task<Unit> SendLogFilesImpl()
        {
#if WINDOWS_PHONE_APP || WINDOWS_UWP
            // Define Recipient
            var sendTo = new EmailRecipient
            {
                Name = "Club ApplETS",
                Address = "applets@ens.etsmtl.ca"
            };

            // Create email object
            var mail = new EmailMessage
            {
                Subject = string.Format(Resources().GetString("SendLogFilesSubject") + " ",
#if WINDOWS_PHONE_APP
                    "ÉtsMobile-WindowsPhone"
#elif WINDOWS_UWP
                    "ÉtsMobile-UWP"
#endif
                ),
                Body = Resources().GetString("SendLogFilesBody")
            };

            // Add recipients to the mail object
            mail.To.Add(sendTo);

            var zipFile = await CreateZipFileAsRandomAccessMemory();

            var attachments = new EmailAttachment
            {
                FileName = LogsZipFileName,
                Data = zipFile
            };
            mail.Attachments.Add(attachments);

            // Open the share contract with Mail only:
            await EmailManager.ShowComposeNewEmailAsync(mail);

            return Unit.Default;
#else
            return await Task.FromResult(Unit.Default);
#endif
        }

        private async Task<IRandomAccessStreamReference> CreateZipFileAsRandomAccessMemory()
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
                var file = await SaveStreamToFile(zipStream, LogsZipFileName);
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

        #region Properties

        private const string LogsZipFileName = "LogFiles.zip";
        public string VersionNumber => $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        public Uri SendFeedbackUri { get; private set; }
        public ReactiveCommand<Unit> SendLogFiles { get; private set; }


        #endregion
    }
}