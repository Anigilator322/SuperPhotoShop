using ImageMagick;
using SuperPhotoShop.Infrostructure;
using SuperPhotoShop.Infrostructure.ViewCommands;
using SuperPhotoShop.Models;
using SuperPhotoShop.View.Windows;
using System.Collections.Generic;
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

        #region DownloadImageFromWeb
        public ICommand DownloadImageFromWebCommand { get; }

        private bool CanDownloadImageFromWebCommandExecute(object param) => true;
        private void OnDownloadImageFromWebCommandExecuted(object param)
        {
            var labels = new List<string> { "URL" };
            var downloadWindow = new InputDialog(labels);
            if (downloadWindow.ShowDialog() == true)
            {
                var url = downloadWindow.InputValues[0];
                FileManager fileManager = new FileManager();
                var model = fileManager.GetImageFromWeb(url);
                if (model == null)
                    return;
                _session = new Session(model);
                InitImageView();
            }
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
            DownloadImageFromWebCommand = new RelayCommand(OnDownloadImageFromWebCommandExecuted, CanDownloadImageFromWebCommandExecute);
            #endregion
        }
    }
}
