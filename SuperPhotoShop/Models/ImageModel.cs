using ImageMagick;
using System;
using System.Diagnostics;

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
        public void SetBrightness(double brightness)
        {
            _image.Modulate((Percentage)brightness, (Percentage)100.0,(Percentage)100);
            OnImageChanged();
        }

        public void OnImageChanged()
        {
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
