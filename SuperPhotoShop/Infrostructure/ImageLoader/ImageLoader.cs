using ImageMagick;
using SuperPhotoShop.Models;
using System.Net;
using System.Windows.Media.Imaging;
namespace SuperPhotoShop.Infrostructure.ImageLoader
{
    public class ImageLoader : IImageLoader
    {
        public ImageModel LoadImage(string url)
        {
            using (WebClient client = new WebClient())
            {
                byte[] data = client.DownloadData(url);
                var mImg = new MagickImage(data);
                var ImageModel = new ImageModel(mImg);
                return ImageModel;
            }
        }
    }
}
