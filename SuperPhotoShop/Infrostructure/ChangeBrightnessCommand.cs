using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure
{
    public class ChangeBrightnessCommand: Command
    {
        private float _oldBrightness;
        private float _newBrightness;
        private ImageModel _imageModel;
        public ChangeBrightnessCommand(float newBrightness, float oldBrightness, ImageModel _image) 
        {
            _newBrightness = newBrightness;
            _oldBrightness = oldBrightness;
        }

        public override void Execute()
        {

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
