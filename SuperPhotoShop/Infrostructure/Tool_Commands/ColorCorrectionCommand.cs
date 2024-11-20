using ImageMagick;
using SuperPhotoShop.Models;
using System.Diagnostics;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public class ColorCorrectionCommand: Command
    {

        public double _brightness { get; set; }
        public double _saturation { get; set; }
        public double _hue { get; set; }

        public ColorCorrectionCommand()
        {

        }

        public ColorCorrectionCommand(double newBrightness,double newSaturation,double newHue) 
        {
            _brightness = newBrightness;
            _saturation = newSaturation;
            _hue = newHue;
        }

        public override void Execute(ImageModel imageModel)
        {
            _imageOld = (MagickImage)imageModel.GetImage().Clone();
            imageModel.SetColorCorrection(_brightness, _saturation, _hue);
        }
        
        public override MagickImage Undo()
        {
            return _imageOld;
        }
    }
}
