using SuperPhotoShop.Infrostructure.Storage;
using SuperPhotoShop.Models;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace SuperPhotoShop.Infrostructure.ImageLoader
{
    public class ImageLoaderProxy : IImageLoader
    {
        private ImageLoader _realObject = new ImageLoader();

        public ImageModel LoadImage(string url)
        {
            if(LocalCacheStorage.TryGet(url, out var model))
                return model;

            var img = _realObject.LoadImage(url);
            LocalCacheStorage.Add(url, img);

            return img;
        }
    }
}
