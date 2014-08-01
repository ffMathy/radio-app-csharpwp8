using System;
using System.IO;
using System.Xml.Serialization;

namespace Radio.Helpers
{
    public static class SerializationHelper
    {
        public static string Serialize<T>(T obj)
        {
            using (var outStream = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof (T));
                serializer.Serialize(outStream, obj);
                return outStream.ToString();
            }
        }

        public static T Deserialize<T>(string serialized)
        {
            if (string.IsNullOrEmpty(serialized))
            {
                //there's no string to serialize - return null.
                return default(T);
            }

            try
            {
                using (var inStream = new StringReader(serialized))
                {
                    var serializer = new XmlSerializer(typeof (T));
                    return (T) serializer.Deserialize(inStream);
                }
            }
            catch (InvalidOperationException)
            {
                //serialization failed - let's return null.
                return default(T);
            }
        }
    }
}