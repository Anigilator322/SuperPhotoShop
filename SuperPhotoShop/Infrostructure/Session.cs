using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure
{
    public class Session
    {
        private ImageModel ImageModel;
        public ImageModel GetImage()
        {
            return ImageModel;
        }
        public void SetImage(ImageModel image)
        {
            ImageModel = image;
        }

    }
}
