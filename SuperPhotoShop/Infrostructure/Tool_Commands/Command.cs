using ImageMagick;
using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public abstract class Command
    {
        //protected ImageModel _imageModel;
        public MagickImage _imageOld { get; set; }
        public abstract void Execute(ImageModel imageModel);
        public abstract MagickImage Undo();
    }
}
