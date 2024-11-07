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
        public void SetColorCorrection(double brightness, double saturation, double hue)
        {
            _image.Modulate((Percentage)brightness, (Percentage)saturation, (Percentage)hue);
            OnImageChanged();
        }
        public void SetBlur(double radius, double sigma)
        {
            _image.Blur(radius, sigma);
            OnImageChanged();
        }
        public void OnImageChanged()
        {
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
