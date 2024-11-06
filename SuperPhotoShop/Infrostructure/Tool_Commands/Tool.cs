using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public abstract class Tool
    {
        public string Name { get; set; }

        public ParametrsRequirments ParametrRequirmetns { get; protected set; }

        public abstract void Handle();

        public abstract void ApplyTool(ImageModel imageModel, CommandHistory commandHistory);

        protected abstract void CreateCommand(CommandHistory commandHistory, ImageModel imageModel);

    }
}
