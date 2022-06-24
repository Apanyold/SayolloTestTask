using System.IO;
using System.Xml.Serialization;

namespace Didenko.Extensions
{
    public static class XmlExtensions
    {
        public static T DeserializeXml<T>(this string @this) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(@this))
            {
                var result = (T)serializer.Deserialize(reader);
                return result;
            }
        }
    }
}