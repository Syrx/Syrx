﻿using System.Runtime.Serialization;
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

        

    }
}