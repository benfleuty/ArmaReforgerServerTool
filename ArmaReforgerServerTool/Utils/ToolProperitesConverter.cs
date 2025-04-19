/******************************************************************************
 * File Name:    ToolProperitesConverter.cs
 * Project:      Arma Reforger Dedicated Server Tool for Windows
 * Description:  Static class containing utility methods for 
 *               performing various JSON (de)serialisation tasks,
 *               including the housing of specific converters
 * 
 * Author:       Bradley Newman
 ******************************************************************************/

using System.Text.Json.Serialization;
using System.Text.Json;
using ReforgerServerApp.Models;

namespace ReforgerServerApp.Utils
{
    internal partial class JsonUtils
    {
        /// <summary>
        /// JSON converter for the ToolProperties model. This allows default values to be used for missing keys,
        /// providing safety when needing to add parameters and keeping old versions intact.
        /// </summary>
        public class ToolProperitesConverter : JsonConverter<ToolProperties>
        {
            public override ToolProperties? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException("Expected StartObject token");
                }

                ToolProperties props = ToolProperties.Default;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return props;
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString();
                        reader.Read(); // Move to the value token

                        switch (propertyName)
                        {
                            case "defaultScenarios":
                                props.defaultScenarios = JsonSerializer.Deserialize<string[]>(ref reader, options)!.ToList();
                                break;
                            case "modDatabaseFile":
                                props.modDatabaseFile = reader.GetString();
                                break;
                            case "installDirectoryFile":
                                props.installDirectoryFile = reader.GetString();
                                break;
                            case "updateRepositoryUrl":
                                props.updateRepositoryUrl = reader.GetString();
                                break;
                            case "releaseRepositoryUrl":
                                props.releaseRepositoryUrl = reader.GetString();
                                break;
                            case "bugReportUrl":
                                props.bugReportUrl = reader.GetString();
                                break;
                            case "checkForUpdatesOnStartup":
                                props.checkForUpdatesOnStartup = reader.GetBoolean();
                                break;
                            case "steamCmdDownloadUrl":
                                props.steamCmdDownloadUrl = reader.GetString();
                                break;
                            case "armaWorkshopUrl":
                                props.armaWorkshopUrl = reader.GetString();
                                break;
                            case "logFile":
                                props.logFile = reader.GetString();
                                break;
                            case "minimumLogLevel":
                                props.minimumLogLevel = reader.GetString();
                                break;
                            case "autoRestartTime_ms":
                                props.autoRestartTime_ms = reader.GetInt32();
                                break;
                            default:
                                throw new JsonException($"Unexpected property: {propertyName}");
                        }
                    }
                }
                throw new JsonException("Invalid JSON format for ToolProperties");
            }

            public override void Write(Utf8JsonWriter writer, ToolProperties value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("defaultScenarios");
                JsonSerializer.Serialize(writer, value.defaultScenarios, options);

                writer.WriteString("modDatabaseFile", value.modDatabaseFile);
                writer.WriteString("installDirectoryFile", value.installDirectoryFile);
                writer.WriteString("updateRepositoryUrl", value.updateRepositoryUrl);
                writer.WriteString("releaseRepositoryUrl", value.releaseRepositoryUrl);
                writer.WriteString("bugReportUrl", value.bugReportUrl);
                writer.WriteBoolean("checkForUpdatesOnStartup", value.checkForUpdatesOnStartup);
                writer.WriteString("steamCmdDownloadUrl", value.steamCmdDownloadUrl);
                writer.WriteString("armaWorkshopUrl", value.armaWorkshopUrl);
                writer.WriteString("logFile", value.logFile);
                writer.WriteString("minimumLogLevel", value.minimumLogLevel);
                writer.WriteNumber("autoRestartTime_ms", value.autoRestartTime_ms);

                writer.WriteEndObject();
            }
        }
    }
}
