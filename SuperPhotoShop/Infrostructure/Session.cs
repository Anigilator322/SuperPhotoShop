using SuperPhotoShop.Infrostructure.Tool_Commands;
using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure
{
    public class Session
    {
        private ImageModel _imageModel;
        private CommandHistory _commandHistory;

        public Session(ImageModel imageModel, CommandHistory commandHistory)
        {
            _imageModel = imageModel;
            _commandHistory = commandHistory;
        }
        public Session(ImageModel imageModel)
        {
            _imageModel = imageModel;
            _commandHistory = new CommandHistory();
        }
        public CommandHistory GetCommandHistory()
        {
            return _commandHistory;
        }

        public ImageModel GetImage()
        {
            return _imageModel;
        }

        public void SetImage(ImageModel image)
        {
            _imageModel = image;
        }

    }
}
