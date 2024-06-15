using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;


namespace Syrx.Tests.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Convenience method for printing object graphs to the console. 
        /// </summary>
        /// <param name="instance"></param>
        public static void PrintAsJson(this object instance) => Console.WriteLine(instance.Serialize());
                
        public static string Serialize<T>(this T instance) => JsonSerializer.Serialize(instance, new JsonSerializerOptions { WriteIndented = true });

        public static T Deserialize<T>(this T instance, string input) => JsonSerializer.Deserialize<T>(input);

        //public static string SerializeToXml<T>(this T instance)
        //{
        //    var serializer = new XmlSerializer(typeof(T));
        //    var settings = new XmlWriterSettings { Indent = true }; // Include line breaks

        //    using (var stringWriter = new StringWriter())
        //    {
        //        //using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
        //        //{
        //        //    serializer.Serialize(xmlWriter, instance);
        //        //}
        //        //return stringWriter.ToString();
        //        serializer.Serialize(stringWriter, instance);
        //        return stringWriter.ToString();
        //    }
        //}

        //public static T DeserializeFromXml<T>(this T instance, string xmlString)
        //{
        //    var serializer = new XmlSerializer(typeof(T));

        //    using (var stringReader = new StringReader(xmlString))
        //    {
        //        return (T) serializer.Deserialize(stringReader);
        //    }
        //}

    }
}