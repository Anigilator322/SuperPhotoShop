using SuperPhotoShop.Models;
using System.Windows.Media.Imaging;

namespace SuperPhotoShop.Infrostructure.ImageLoader
{
    public interface IImageLoader
    {
        ImageModel LoadImage(string url);
    }
}