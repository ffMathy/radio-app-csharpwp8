using Windows.Storage;

namespace Radio.Helpers
{
    public class StorageHelper
    {
        private static ApplicationDataContainer DataContainer
        {
            get { return ApplicationData.Current.RoamingSettings; }
        }

        /// <summary>
        ///     Stored a setting, returns true if success
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static bool StoreSetting<T>(string key, T value, bool overwrite = true)
        {
            if (overwrite || !DataContainer.Values.ContainsKey(key))
            {
                var dataString = SerializationHelper.Serialize(value);
                DataContainer.Values[key] = dataString;

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
        public static T GetSetting<T>(string key)
        {
            return GetSetting(key, default(T));
        }

        public static T GetSetting<T>(string key, T defaultValue)
        {
            if (DataContainer.Values.ContainsKey(key))
            {
                var data = (string)DataContainer.Values[key];
                return SerializationHelper.Deserialize<T>(data);
            }

            return defaultValue;
        }

        public static void RemoveSetting(string key)
        {
            DataContainer.Values.Remove(key);
        }
    }
}