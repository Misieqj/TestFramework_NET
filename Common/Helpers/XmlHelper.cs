using System.Xml.Serialization;

namespace TestFramework_NET.Common.Helpers
{
    public static class XmlHelper
    {
        public static T LoadXml<T>(string filePath)
        {
            XmlSerializer serializer = new(typeof(T));
            using StreamReader reader = new(filePath);
            var result = serializer.Deserialize(reader);
            if (result == null)
                throw new InvalidDataException($"Unable to deserialize data from file: {filePath}");

            return (T)result;
        }
    }
}
