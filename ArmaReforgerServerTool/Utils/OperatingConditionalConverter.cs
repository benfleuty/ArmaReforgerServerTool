/******************************************************************************
 * File Name:    OperatingConditionalConverter.cs
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
        /// JSON converter for the Operating model. This allows disableNavmeshStreaming to be excluded when all Navmeshes are to be disabled
        /// which is not actually part of the server configuration model.
        /// </summary>
        public class OperatingConditionalConverter : JsonConverter<Operating>
        {
            public override Operating Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException("Expected StartObject token.");
                }

                Operating oper = Operating.Default;

                // Make this null so if we don't find this tag in the JSON, we can check if it's null later
                oper.disableNavmeshStreaming = null;

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
                        case nameof(Operating.lobbyPlayerSynchronise):
                            oper.lobbyPlayerSynchronise = reader.GetBoolean();
                            break;
                        case nameof(Operating.playerSaveTime):
                            oper.playerSaveTime = reader.GetInt32();
                            break;
                        case nameof(Operating.aiLimit):
                            oper.aiLimit = reader.GetInt32();
                            break;
                        case nameof(Operating.slotReservationTimeout):
                            oper.slotReservationTimeout = reader.GetInt32();
                            break;
                        case nameof(Operating.disableNavmeshStreaming):
                            oper.disableNavmeshStreaming = JsonSerializer.Deserialize<string[]>(ref reader, options);
                            break;
                        case nameof(Operating.disableServerShutdown):
                            oper.disableServerShutdown = reader.GetBoolean();
                            break;
                        case nameof(Operating.disableCrashReporter):
                            oper.disableCrashReporter = reader.GetBoolean();
                            break;
                        case nameof(Operating.disableAI):
                            oper.disableAI = reader.GetBoolean();
                            break;
                        case nameof(Operating.joinQueue):
                            oper.joinQueue = JsonSerializer.Deserialize<JoinQueue>(ref reader, options);
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
                return oper;
            }

            public override void Write(Utf8JsonWriter writer, Operating value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteBoolean(nameof(Operating.lobbyPlayerSynchronise), value.lobbyPlayerSynchronise);
                writer.WriteNumber(nameof(Operating.playerSaveTime), value.playerSaveTime);
                writer.WriteNumber(nameof(Operating.aiLimit), value.aiLimit);
                writer.WriteNumber(nameof(Operating.slotReservationTimeout), value.slotReservationTimeout);

                // Only include the disableNavmeshStreaming option if relevant,
                // handle this special case as if it is included with an empty list, it means
                // disable streaming ALL navmeshes
                if (ConfigurationManager.GetInstance().GetServerConfiguration().toggleDisableNavmeshStreaming)
                {
                    writer.WritePropertyName(nameof(Operating.disableNavmeshStreaming));
                    writer.WriteStartArray();
                    foreach (string item in value.disableNavmeshStreaming)
                    { writer.WriteStringValue(item); }
                    writer.WriteEndArray();
                }

                writer.WriteBoolean(nameof(Operating.disableServerShutdown), value.disableServerShutdown);
                writer.WriteBoolean(nameof(Operating.disableCrashReporter), value.disableCrashReporter);
                writer.WriteBoolean(nameof(Operating.disableAI), value.disableAI);

                if (value.joinQueue != null)
                {
                    writer.WritePropertyName(nameof(Operating.joinQueue));
                    JsonSerializer.Serialize(writer, value.joinQueue, options);
                }

                writer.WriteEndObject();
            }
        }
    }
}
