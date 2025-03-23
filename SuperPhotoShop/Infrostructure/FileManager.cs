using Microsoft.Win32;
using SuperPhotoShop.Models;
using ImageMagick;
using System.IO;
using System;
using System.Text.Json;
using SuperPhotoShop.Infrostructure.Tool_Commands;
using System.Collections.Generic;
using SuperPhotoShop.Infrostructure.Tool_Commands.CommandSave;
using SuperPhotoShop.Infrostructure.ImageLoader;
using System.Drawing;
namespace SuperPhotoShop.Infrostructure
{
    public class FileManager
    {
        ImageLoader.ImageLoader imageLoader;

        private static JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Converters = { new CommandConverter() },
            WriteIndented = true
        };


        public ImageModel GetImageFromWeb(string url)
        {
            imageLoader = new ImageLoader.ImageLoader();
            return imageLoader.LoadImage(url);
        }

        public void SaveImage(ImageModel imageModel)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить изображение как",
                Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp|TIFF Image|*.tiff",
                DefaultExt = "jpg",
                FileName = "image"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                MagickFormat format = MagickFormat.Unknown;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1: format = MagickFormat.Jpeg; break;
                    case 2: format = MagickFormat.Png; break;
                    case 3: format = MagickFormat.Bmp; break;
                    case 4: format = MagickFormat.Tiff; break;
                }
                imageModel.GetImage().Format = format;
                imageModel.GetImage().Write(filePath);
            }
        }
        public void SaveSession(Session session) 
        {
            byte[] imageBytes;

            var saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить проект как",
                Filter = "Json File|*.json",
                DefaultExt = "json",
                FileName = "project"
            };
            if (saveFileDialog.ShowDialog() == true) 
            {
                string filePath = saveFileDialog.FileName;
                using (var memoryStream = new MemoryStream())
                {
                    session.GetImage().GetImage().Write(memoryStream, MagickFormat.Png);
                    imageBytes = memoryStream.ToArray();
                }

                var sessionData = new
                {
                    ImageData = Convert.ToBase64String(imageBytes),
                    History = session.GetCommandHistory().Commands
                };

                var json = JsonSerializer.Serialize(sessionData, jsonOptions);
                File.WriteAllText(filePath, json);
            }
        }
        public ImageModel LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                MagickImage image = new MagickImage(openFileDialog.FileName);
                ImageModel imageModel = new ImageModel(image);
                return (imageModel);
            }
            return null;
        }
        public Session LoadSession()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Читаем JSON из файла
                var json = File.ReadAllText(openFileDialog.FileName);

                // Десериализуем объект данных из JSON
                JsonDocument document = JsonDocument.Parse(json);
                JsonElement root = document.RootElement;

                // Извлекаем данные изображения как строку
                if (root.TryGetProperty("ImageData", out JsonElement imageDataElement) &&
                    imageDataElement.ValueKind == JsonValueKind.String)
                {
                    string imageDataString = imageDataElement.GetString();
                    byte[] imageBytes = Convert.FromBase64String(imageDataString);
                    MagickImage image = new MagickImage(imageBytes);
                    ImageModel model = new ImageModel(image);
                    // Восстанавливаем историю команд
                    CommandHistory history;
                    if (root.TryGetProperty("History", out JsonElement historyElement))
                    {
                        history = new CommandHistory(new Stack<Command>(JsonSerializer.Deserialize<Stack<Command>>(historyElement.GetRawText(), jsonOptions)));
                        return new Session(model, history);
                    }
                    
                }
                else
                {
                    throw new InvalidDataException("Image data is missing or not in the correct format.");
                }
            }
            return null;
        }
    }
}
