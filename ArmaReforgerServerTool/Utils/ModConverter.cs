/******************************************************************************
 * File Name:    ModConverter.cs
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
        /// JSON Converter for the Mod model, this will exclude the 'version' field if version == latest (not a valid value for the server config)
        /// </summary>
        public class ModConverter : JsonConverter<Mod>
        {
            public override Mod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                Mod mod = new Mod();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        break;
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString();
                        reader.Read();

                        switch (propertyName)
                        {
                            case nameof(mod.modId):
                                mod.modId = reader.GetString();
                                break;
                            case nameof(mod.name):
                                mod.name = reader.GetString();
                                break;
                            case nameof(mod.version):
                                mod.version = reader.GetString();
                                break;
                            case nameof(mod.required):
                                mod.required = reader.GetBoolean();
                                break;
                        }
                    }
                }

                // Initialize version with 'latest' if its not present
                mod.version ??= "latest";

                return mod;
            }

            public override void Write(Utf8JsonWriter writer, Mod value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteString(nameof(value.modId), value.modId);
                writer.WriteString(nameof(value.name), value.name);

                // Only write version if it is not 'latest'
                if (value.version != "latest")
                {
                    writer.WriteString(nameof(value.version), value.version);
                }

                writer.WriteBoolean(nameof(value.required), value.required);
                writer.WriteEndObject();
            }
        }
    }
}
