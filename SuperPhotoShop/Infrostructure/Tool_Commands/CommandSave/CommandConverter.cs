using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperPhotoShop.Infrostructure.Tool_Commands.CommandSave
{
    public class CommandConverter : JsonConverter<Command>
    {
        public override Command Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                string type = doc.RootElement.GetProperty("Type").GetString();

                Type commandType;

                switch (type)
                {
                    case "ColorCorrectionCommand":
                        commandType = typeof(ColorCorrectionCommand);
                        break;
                    case "BlurCommand":
                        commandType = typeof(BlurCommand);
                        break;
                    default:
                        throw new NotSupportedException($"Unknown command type: {type}");
                }
                
                return (Command)JsonSerializer.Deserialize(doc.RootElement.GetRawText(), commandType, options);
            }

        }

        public override void Write(Utf8JsonWriter writer, Command value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Type", value.GetType().Name);

            var json = JsonSerializer.Serialize(value, value.GetType(), options);
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                foreach (var element in doc.RootElement.EnumerateObject())
                {
                    element.WriteTo(writer);
                }
            }
            writer.WriteEndObject();
        }
    }
}
