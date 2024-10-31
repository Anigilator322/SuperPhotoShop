using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure
{
    public abstract class Command
    {
        protected ImageModel _Image;
        public virtual void Execute() { }
        public virtual void Undo() { }
        public virtual void Redo() { }
    }
}
