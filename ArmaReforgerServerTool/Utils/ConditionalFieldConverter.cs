/******************************************************************************
 * File Name:    ConditionalFieldConverter.cs
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
        /// Custom JSON Converter for conditional fields
        /// </summary>
        public class ConditionalFieldConverter : JsonConverter<string>
        {
            public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return reader.GetString()!;
            }

            public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
            {
                // This method is intentionally left empty because we handle the writing logic in the Write method of the ModConverter.
            }
        }
    }
}
