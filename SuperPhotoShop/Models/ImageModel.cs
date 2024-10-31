using ImageMagick;

namespace SuperPhotoShop.Models
{
    public enum ImageFormat
    {
        JPEG,
        PNG
    }

    public class ImageModel
    {
        public ImageFormat Format;
        private MagickImage _image;

        public ImageModel(MagickImage image)
        {
            _image = image;
        }
        public void SetBrightness(float brightness)
        {

        }
    }
}
