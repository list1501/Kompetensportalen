using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Kompetensportalen
{
    public static class Serialize
    {
        // http://stackoverflow.com/questions/2434534/serialize-an-object-to-string
        //Must be in a static class as it is to work with more than one type of object

        public static string Serializing<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
        }

        public static T Deserialize<T>(this string toDeserialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var textReader = new StringReader(toDeserialize);
            return (T)xmlSerializer.Deserialize(textReader);
        }
    }
}
