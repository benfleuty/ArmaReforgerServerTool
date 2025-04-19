/******************************************************************************
 * File Name:    RconConditionalConverter.cs
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
        /// JSON converter for the Rcon model. This allows Rcon to be excluded when it's 'disabled' which is not actually part of the 
        /// server configuration model.
        /// </summary>
        public class RconConditionalConverter : JsonConverter<Rcon>
        {
            public override Rcon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException("Expected StartObject token.");
                }

                string address = null;
                int port = 0;
                string password = null;
                string permission = null;
                string[] blacklist = null;
                string[] whitelist = null;
                int maxClients = 0;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        break;
                    }

                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new JsonException($"Unexpected token type: {reader.TokenType}");
                    }

                    string propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case nameof(Rcon.address):
                            address = reader.GetString();
                            break;
                        case nameof(Rcon.port):
                            port = reader.GetInt16();
                            break;
                        case nameof(Rcon.password):
                            password = reader.GetString();
                            break;
                        case nameof(Rcon.permission):
                            permission = reader.GetString();
                            break;
                        case nameof(Rcon.blacklist):
                            blacklist = JsonSerializer.Deserialize<string[]>(ref reader, options);
                            break;
                        case nameof(Rcon.whitelist):
                            whitelist = JsonSerializer.Deserialize<string[]>(ref reader, options);
                            break;
                        case nameof(Rcon.maxClients):
                            maxClients = reader.GetInt16();
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
                return new Rcon(address, port, password, Utilities.StringToEnum<RconPermission>(permission),
                    blacklist, whitelist, maxClients);
            }

            public override void Write(Utf8JsonWriter writer, Rcon value, JsonSerializerOptions options)
            {
                if (ConfigurationManager.GetInstance().GetServerConfiguration().rconEnabled)
                {
                    writer.WriteStartObject();
                    writer.WriteString(nameof(Rcon.address), value.address);
                    writer.WriteNumber(nameof(Rcon.port), value.port);
                    writer.WriteString(nameof(Rcon.password), value.password);
                    writer.WriteString(nameof(Rcon.permission), Utilities.RconPermissionToString(value.permission));
                    writer.WritePropertyName(nameof(Rcon.blacklist));
                    writer.WriteStartArray();
                    foreach (string item in value.blacklist)
                    { writer.WriteStringValue(item); }
                    writer.WriteEndArray();
                    writer.WritePropertyName(nameof(Rcon.whitelist));
                    writer.WriteStartArray();
                    foreach (string item in value.whitelist)
                    { writer.WriteStringValue(item); }
                    writer.WriteEndArray();
                    writer.WriteNumber(nameof(Rcon.maxClients), value.maxClients);
                    writer.WriteEndObject();
                }
            }
        }
    }
}
