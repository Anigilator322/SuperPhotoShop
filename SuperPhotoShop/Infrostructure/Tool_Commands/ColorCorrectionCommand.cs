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

        public ColorCorrectionCommand(double newBrightness,double newSaturation,double newHue, ImageModel _image) 
        {
            _newBrightness = newBrightness;
            _newSaturation = newSaturation;
            _newHue = newHue;
            _imageModel = _image;
        }

        public override void Execute()
        {
            _imageModel.GetImage().Modulate((Percentage)_newBrightness, (Percentage)_newSaturation, (Percentage)_newSaturation);
            _imageModel.OnImageChanged();

        }
        public override void Redo()
        {
        }
        public override void Undo()
        {
        }
    }
}
