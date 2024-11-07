using ImageMagick;
using SuperPhotoShop.Models;
using System.Diagnostics;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public class ColorCorrectionCommand: Command
    {

        private double _newBrightness;
        private double _newSaturation;
        private double _newHue;

        public ColorCorrectionCommand(double newBrightness,double newSaturation,double newHue) 
        {
            _newBrightness = newBrightness;
            _newSaturation = newSaturation;
            _newHue = newHue;
        }

        public override void Execute(ImageModel imageModel)
        {
            _imageOld = (MagickImage)imageModel.GetImage().Clone();
            imageModel.GetImage().Modulate((Percentage)_newBrightness, (Percentage)_newSaturation, (Percentage)_newSaturation);
            imageModel.OnImageChanged();

        }
        
        public override MagickImage Undo()
        {
            return _imageOld;
        }
    }
}
