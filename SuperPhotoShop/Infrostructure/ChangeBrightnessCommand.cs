using ImageMagick;
using SuperPhotoShop.Models;
using System.Diagnostics;

namespace SuperPhotoShop.Infrostructure
{
    public class ChangeBrightnessCommand: Command
    {
        private double _oldBrightness;
        private double _newBrightness;

        private ImageModel _imageModel;

        public ChangeBrightnessCommand(double newBrightness, ImageModel _image) 
        {
            _newBrightness = newBrightness;
            _imageModel = _image;
        }

        public override void Execute()
        {
            Debug.WriteLine((Percentage)_newBrightness + " " + (Percentage)100.0);
            _imageModel.GetImage().Modulate((Percentage)_newBrightness, (Percentage)100.0, (Percentage)100);
            _imageModel.OnImageChanged();

        }
        public override void Redo()
        {
            base.Redo();
        }
        public override void Undo()
        {
            base.Undo();
        }
    }
}
