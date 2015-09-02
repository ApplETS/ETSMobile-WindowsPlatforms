using System;
using System.IO;
using System.Reactive.Linq;
using Splat;

namespace Akavache.Sqlite3
{
    /// <summary>
    /// Temporary Extension Class until "Registrations.cs" at
    /// https://github.com/akavache/Akavache/blob/3c1431250ae94d25cf7ac9637528f4445b131317/Akavache.Sqlite3/Registrations.cs
    /// is fixed.
    /// <para>Once resolved, remove this class</para>
    /// </summary>
    public static class Registrations
    {
        public static void RegisterCaching(this IMutableDependencyResolver resolver)
        {
            var fs = Locator.Current.GetService<IFilesystemProvider>();
            if (fs == null)
            {
                throw new Exception("Failed to initialize Akavache properly. Do you have a reference to Akavache.dll?");
            }

            var localCache = new Lazy<IBlobCache>(() =>
            {
                fs.CreateRecursive(fs.GetDefaultLocalMachineCacheDirectory()).SubscribeOn(BlobCache.TaskpoolScheduler).Wait();
                return new SQLitePersistentBlobCache(Path.Combine(fs.GetDefaultLocalMachineCacheDirectory(), "blobs.db"), BlobCache.TaskpoolScheduler);
            }).Value;
            resolver.Register(() => localCache, typeof(IBlobCache), "LocalMachine");

            var userAccount = new Lazy<IBlobCache>(() =>
            {
                fs.CreateRecursive(fs.GetDefaultRoamingCacheDirectory()).SubscribeOn(BlobCache.TaskpoolScheduler).Wait();
                return new SQLitePersistentBlobCache(Path.Combine(fs.GetDefaultRoamingCacheDirectory(), "userblobs.db"), BlobCache.TaskpoolScheduler);
            }).Value;
            resolver.Register(() => userAccount, typeof(IBlobCache), "UserAccount");

            var secure = new Lazy<ISecureBlobCache>(() =>
            {
                fs.CreateRecursive(fs.GetDefaultSecretCacheDirectory()).SubscribeOn(BlobCache.TaskpoolScheduler).Wait();
                return new SQLiteEncryptedBlobCache(Path.Combine(fs.GetDefaultSecretCacheDirectory(), "secret.db"), Locator.Current.GetService<IEncryptionProvider>(), BlobCache.TaskpoolScheduler);
            }).Value;
            resolver.Register(() => secure, typeof(ISecureBlobCache), null);
        }
    }
}