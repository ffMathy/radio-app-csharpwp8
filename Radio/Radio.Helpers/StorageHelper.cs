using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Radio.Helpers
{
    public class StorageHelper
    {
        private static ApplicationDataContainer DataContainer
        {
            get { return ApplicationData.Current.RoamingSettings; }
        }

        private static StorageFolder StorageRoot
        {
            get { return ApplicationData.Current.RoamingFolder; }
        }

        /// <summary>
        ///     Stored a setting, returns true if success
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static async Task<bool> StoreSetting<T>(string key, T value, bool overwrite = true)
        {
            if (overwrite || !DataContainer.Values.ContainsKey(key))
            {
                var dataString = SerializationHelper.Serialize(value);
                var bytes = Encoding.UTF8.GetBytes(dataString);

                var file = await StorageRoot.CreateFileAsync(key, CreationCollisionOption.ReplaceExisting);
                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                    await stream.FlushAsync();
                }


                return true;
            }

            return false;
        }

        /// <summary>
        ///     Get a setting from storage, returns default value if it does not exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetSetting<T>(string key)
        {
            return await GetSetting(key, default(T));
        }

        public static async Task<T> GetSetting<T>(string key, T defaultValue)
        {
            if (DataContainer.Values.ContainsKey(key))
            {
                var file = await StorageRoot.GetFileAsync(key);
                using (var stream = await file.OpenStreamForReadAsync())
                {
                    var buffer = new byte[stream.Length];
                    await stream.ReadAsync(buffer, 0, buffer.Length);

                    var data = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    return SerializationHelper.Deserialize<T>(data);
                }
            }

            return defaultValue;
        }

        public static void RemoveSetting(string key)
        {
            DataContainer.Values.Remove(key);
        }
    }
}