using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public abstract class Command
    {
        protected ImageModel _imageModel;
        public abstract void Execute();
        public abstract void Undo();
        public abstract void Redo();
    }
}
