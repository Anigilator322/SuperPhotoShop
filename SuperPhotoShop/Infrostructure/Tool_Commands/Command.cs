using ImageMagick;
using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public abstract class Command
    {
        //protected ImageModel _imageModel;
        protected MagickImage _imageOld;
        public abstract void Execute(ImageModel imageModel);
        public abstract MagickImage Undo();
    }
}
