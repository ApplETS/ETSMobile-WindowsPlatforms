using Splat;
using System;
using System.IO;
using System.IO.Compression;
using System.Reactive;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Ets.Mobile.ViewModel.Helpers
{
    public static class LogHelper
    {
        public static async Task ZipApplicationLogsAndSendEmail(string emailBody)
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
                Subject = string.Format(Locator.Current.GetService<ResourceLoader>().GetString("SendLogFilesSubject") + " ",
#if WINDOWS_PHONE_APP
                    "ÉtsMobile-WindowsPhone"
#elif WINDOWS_UWP
                    "ÉtsMobile-UWP"
#endif
                ),
                Body = emailBody
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
#else
            return await Task.FromResult(Unit.Default);
#endif
        }

        private static async Task<IRandomAccessStreamReference> CreateZipFileAsRandomAccessMemory()
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

        private static async Task<StorageFile> SaveStreamToFile(Stream streamToSave, string fileName)
        {
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                streamToSave.Position = 0;
                await streamToSave.CopyToAsync(fileStream);
            }
            return file;
        }

        private const string LogsZipFileName = "LogFiles.zip";
    }
}