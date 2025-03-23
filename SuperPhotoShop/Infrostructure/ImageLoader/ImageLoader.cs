using ImageMagick;
using SuperPhotoShop.Infrostructure.Storage;
using SuperPhotoShop.Models;
using System.Net;
using System.Windows.Media.Imaging;
namespace SuperPhotoShop.Infrostructure.ImageLoader
{
    public class ImageLoader
    {
        public ImageModel LoadImage(string url)
        {
            if(LocalCacheStorage.TryGet(url, out ImageModel image))
                return image;

            using (WebClient client = new WebClient())
            {
                byte[] data = client.DownloadData(url);
                var mImg = new MagickImage(data);
                var ImageModel = new ImageModel(mImg);
                LocalCacheStorage.Add(url, ImageModel);
                return ImageModel;
            }
        }
    }
}
