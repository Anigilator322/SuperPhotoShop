using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
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
                JsonElement root = doc.RootElement;
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
                
                var command = (Command)JsonSerializer.Deserialize(root.GetRawText(), commandType, options);
                
                if (root.TryGetProperty("Image", out JsonElement imageElement) &&
                imageElement.ValueKind == JsonValueKind.String)
                {
                    // Преобразуем строку Base64 в MagickImage
                    byte[] imageBytes = Convert.FromBase64String(imageElement.GetString());
                    var image = new MagickImage(imageBytes);

                    command._imageOld = image;
                }

                return command;

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
                    if (element.Name == "_imageOld") continue;
                    element.WriteTo(writer);
                }
            }
            writer.WriteString("Image", ConvertImageToBase64(value._imageOld));

            writer.WriteEndObject();
        }

        private static string ConvertImageToBase64(MagickImage image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Write(memoryStream, MagickFormat.Png); // Можно выбрать другой формат при необходимости
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

    }
}
