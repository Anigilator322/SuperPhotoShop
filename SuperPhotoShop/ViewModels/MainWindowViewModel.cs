
using System.Windows.Media.Imaging;

namespace SuperPhotoShop.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        #region Title
        private string _title = "SuperPhohotoshop2";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        } 
        #endregion

        private BitmapImage _currentImage;
        public BitmapImage CurrentImage
        {
            get => _currentImage;
            set => Set(ref _currentImage, value);
        }

    }
}
