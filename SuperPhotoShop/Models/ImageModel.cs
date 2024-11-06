using ImageMagick;
using System;
using System.Diagnostics;

namespace SuperPhotoShop.Models
{
    public class ImageModel
    {
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

        public void OnImageChanged()
        {
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
