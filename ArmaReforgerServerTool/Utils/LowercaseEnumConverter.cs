/******************************************************************************
 * File Name:    LowercaseEnumConverter.cs
 * Project:      Arma Reforger Dedicated Server Tool for Windows
 * Description:  Static class containing utility methods for 
 *               performing various JSON (de)serialisation tasks,
 *               including the housing of specific converters
 * 
 * Author:       Bradley Newman
 ******************************************************************************/

using System.Text.Json.Serialization;
using System.Text.Json;

namespace ReforgerServerApp.Utils
{
    internal partial class JsonUtils
    {
        /// <summary>
        /// JSON Converter for Enums. This will convert an enum to a lowercase string when
        /// serialising to JSON
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        public class LowercaseEnumConverter<T> : JsonConverter<T> where T : Enum
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                try
                {
                    var enumString = reader.GetString();
                    return enumString == null ? throw new JsonException() : (T)Enum.Parse(typeof(T), enumString, true);
                }
                catch (Exception ex)
                {
                    Utilities.DisplayErrorMessage("Unable to load value from configuration file.", ex.Message);
                    return default;
                }
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString().ToLower());
            }
        }
    }
}
