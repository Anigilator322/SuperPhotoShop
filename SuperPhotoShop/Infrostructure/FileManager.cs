using Microsoft.Win32;
using SuperPhotoShop.Models;
using ImageMagick;
using System.Windows.Media.Imaging;
using System.IO;
using System;
using System.Text.Json;
using SuperPhotoShop.Infrostructure.Tool_Commands;
using System.Collections.Generic;
using SuperPhotoShop.Infrostructure.Tool_Commands.CommandSave;
namespace SuperPhotoShop.Infrostructure
{
    public class FileManager
    {

        private static JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Converters = { new CommandConverter() },
            WriteIndented = true
        };


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
                var json = File.ReadAllText(openFileDialog.FileName);

                var sessionData = JsonSerializer.Deserialize<dynamic>(json,jsonOptions);

                byte[] imageBytes = Convert.FromBase64String((string)sessionData["ImageData"]);
                MagickImage image = new MagickImage(imageBytes);

                var history = new CommandHistory(JsonSerializer.Deserialize<Stack<Command>>(sessionData["History"].ToString()));
                
                return new Session(new ImageModel(image),history);
            }
            return null;
        }
    }
}
