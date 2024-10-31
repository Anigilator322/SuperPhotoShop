using Microsoft.Win32;
using SuperPhotoShop.Models;
using ImageMagick;
using System.Windows.Media.Imaging;
using System.IO;
namespace SuperPhotoShop.Infrostructure
{
    public class FileManager
    {
        public void SaveImage(ImageModel image)
        {

        }
        public void SaveSession(Session session) 
        {

        }
        public ImageModel LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                using (MagickImage image = new MagickImage(openFileDialog.FileName))
                {
                    image.Resize(300, 300);
                    return new ImageModel(image);
                }

            }
            return null;
        }
        public Session LoadSession()
        {
            return new Session();
        }

        private BitmapImage ConvertToBitmapImage(MagickImage image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Write(memoryStream, MagickFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();

                return bitmapImage;
            }

        }
    }
}
