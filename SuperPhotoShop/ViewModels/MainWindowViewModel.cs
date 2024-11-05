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
        public ImageViewModel ImageViewModel { get; }

        #region Title
        private string _title = "SuperPhohotoshop";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
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
        
        public MainWindowViewModel()
        {
            ImageViewModel = new ImageViewModel(this);
            #region ViewCommands
            CloseApplicationViewCommand = new RelayCommand(OnCloseApplicationViewCommandExecuted, CanCloseApplicationViewCommandExecute);
            #endregion
        }
    }
}
