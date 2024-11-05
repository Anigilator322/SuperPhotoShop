
using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure
{
    public abstract class Tool
    {
        public ParametrsRequirments ParametrRequirmetns { get; protected set; }
        public virtual void Handle()
        {

        }
        public virtual void ApplyTool(ImageModel imageModel)
        {
        }
    }
}
