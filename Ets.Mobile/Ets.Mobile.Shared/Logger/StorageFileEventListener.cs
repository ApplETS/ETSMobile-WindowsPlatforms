using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Threading;
using Windows.Storage;

namespace Ets.Mobile.Logger
{
    /// <summary>
    /// This is an advanced useage, where you want to intercept the logging messages and devert them somewhere
    /// besides ETW.
    /// </summary>
    public class StorageFileEventListener : EventListener
    {
        /// <summary>
        /// Storage file to be used to write logs
        /// </summary>
        private StorageFile _storageFile;

        /// <summary>
        /// Name of the current event listener
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// The format to be used by logging.
        /// </summary>
        private const string Format = "{0:yyyy-MM-dd HH\\:mm\\:ss\\:ffff}\tType: {1}\tId: {2}\tMessage: '{3}'";

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public StorageFileEventListener(string name)
        {
            _name = name;

#if DEBUG
            Debug.WriteLine("StorageFileEventListener for {0} has name {1}", GetHashCode(), name);
#endif

            AssignLocalFile();
        }

        private async void AssignLocalFile()
        {
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("StorageFileEventListener",
                    CreationCollisionOption.OpenIfExists);
            _storageFile = await folder.CreateFileAsync(_name.Replace(" ", "_") + ".log",
                    CreationCollisionOption.OpenIfExists);
        }

        private async void WriteToFile(IEnumerable<string> lines)
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                await FileIO.AppendLinesAsync(_storageFile, lines);
            }
            catch (Exception)
            {
                // TODO:
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (_storageFile == null)
            {
                return;
            }

            var lines = new List<string>();

            var newFormatedLine = string.Format(Format, DateTime.Now, eventData.Level, eventData.EventId, eventData.Payload[0]);

#if DEBUG
            Debug.WriteLine(newFormatedLine);
#endif

            lines.Add(newFormatedLine);

            WriteToFile(lines);
        }
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
#if DEBUG
            Debug.WriteLine($"OnEventSourceCreated for Listener {GetHashCode()} - {_name} got eventSource {eventSource.Name}");
#endif
        }
    }
}