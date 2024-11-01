using ImageMagick;
using SuperPhotoShop.Infrostructure;
using SuperPhotoShop.Infrostructure.ViewCommands;
using SuperPhotoShop.Models;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SuperPhotoShop.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        #region Title
        private string _title = "SuperPhohotoshop";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region Image
        private BitmapImage _currentImage;
        public BitmapImage CurrentImage
        {
            get => _currentImage;
            set => Set(ref _currentImage, value);
        }
        #endregion

        #region CloseApplicationViewCommand
        public void OnCloseApplicationViewCommandExecuted(object param)
        {
            Debug.WriteLine("CloseApp");
            Application.Current.Shutdown();
        }

        public bool CanCloseApplicationViewCommandExecute(object param)
        {
            return true;
        } 
        
        public ICommand CloseApplicationViewCommand { get; }
        #endregion
        #region OpenImageViewComand
        public ICommand OpenImageViewCommand { get; }

        private bool CanOpenImageViewCommandExecute(object param) => true;
        private void OnOpenImageViewCommandExecuted(object param)
        {
            FileManager _fileManager = new FileManager();
            ImageModel model = _fileManager.LoadImage();
            MagickImage image = model.GetImage();
            CurrentImage = ConvertToBitmapImage(image);
        }
        private BitmapImage ConvertToBitmapImage(MagickImage image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Write(memoryStream, MagickFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();

                return bitmapImage;
            }

        }
        #endregion

        public MainWindowViewModel()
        {
            #region ViewCommands
            CloseApplicationViewCommand = new RelayCommand(OnCloseApplicationViewCommandExecuted, CanCloseApplicationViewCommandExecute);
            OpenImageViewCommand = new RelayCommand(OnOpenImageViewCommandExecuted, CanOpenImageViewCommandExecute);
            #endregion
        }
    }
}
