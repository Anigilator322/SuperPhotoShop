using ImageMagick;
using SuperPhotoShop.Infrostructure;
using SuperPhotoShop.Infrostructure.ViewCommands;
using SuperPhotoShop.Models;
using SuperPhotoShop.View.Windows;
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

        private Session _session;

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
            if (model == null)
                return;
            _session = new Session(model);
            InitImageView();
        }
        #endregion

        #region SaveAsImageViewCommand
        public ICommand SaveAsImageViewCommand { get; }

        private bool CanSaveAsImageViewCommandExecute(object param) => _session!=null;
        private void OnSaveAsImageViewCommandExecuted(object param)
        {
            FileManager fileManager = new FileManager();
            fileManager.SaveImage(_session.GetImage());
        }
        #endregion

        #region SaveAsSessionViewCommand
        public ICommand SaveAsSessionViewCommand { get; }
        private bool CanSaveAsSessionViewCommandExecute(object param) => _session!=null;
        private void OnSaveAsSessionViewCommandExecuted(object param)
        {
            FileManager fileManager = new FileManager();
            fileManager.SaveSession(_session);
        }
        #endregion

        #region LoadSessionViewCommand
        public ICommand LoadSessionViewCommand { get; }
        private bool CanLoadSessionViewCommandExecute(object param) => true;
        private void OnLoadSessionViewCommandExecuted(object param)
        {
            FileManager fileManager = new FileManager();
            _session = fileManager.LoadSession();
            InitImageView();
        }
        #endregion

        private void InitImageView()
        {
            ImageViewModel.InitializeSession(_session);
        }

        public MainWindowViewModel()
        {
            ImageViewModel = new ImageViewModel(this);
            #region ViewCommands
            CloseApplicationViewCommand = new RelayCommand(OnCloseApplicationViewCommandExecuted, CanCloseApplicationViewCommandExecute);
            OpenImageViewCommand = new RelayCommand(OnOpenImageViewCommandExecuted, CanOpenImageViewCommandExecute);
            SaveAsImageViewCommand = new RelayCommand(OnSaveAsImageViewCommandExecuted, CanSaveAsImageViewCommandExecute);
            SaveAsSessionViewCommand = new RelayCommand(OnSaveAsSessionViewCommandExecuted, CanSaveAsSessionViewCommandExecute);
            LoadSessionViewCommand = new RelayCommand(OnLoadSessionViewCommandExecuted, CanLoadSessionViewCommandExecute);
            #endregion
        }
    }
}
