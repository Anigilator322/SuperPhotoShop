using SuperPhotoShop.Models;

namespace SuperPhotoShop.Infrostructure
{
    public class Session
    {
        private ImageModel _imageModel;
        private CommandHistory _commandHistory;

        public Session(ImageModel imageModel)
        {
            _imageModel = imageModel;
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
