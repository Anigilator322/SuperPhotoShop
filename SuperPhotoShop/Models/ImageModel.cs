using ImageMagick;
using System;

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

        public event EventHandler ImageChanged;

        public ImageModel(MagickImage image)
        {
            _image = image;
        }
        public MagickImage GetImage()
        {
            return _image;
        }
        public void SetBrightness(float brightness)
        {

        }

        private void OnImageChanged()
        {
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
