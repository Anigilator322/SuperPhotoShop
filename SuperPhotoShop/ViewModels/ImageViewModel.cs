using ImageMagick;
using SuperPhotoShop.Infrostructure;
using SuperPhotoShop.Infrostructure.ViewCommands;
using SuperPhotoShop.Models;
using SuperPhotoShop.View.Windows;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SuperPhotoShop.ViewModels
{
    internal class ImageViewModel : ViewModel
    {
        public ViewModel MainViewModel { get; }
        private Session _session;
        
        #region Image
        private ImageModel _imageModel;
        private BitmapImage _currentImage;
        public BitmapImage CurrentImage
        {
            get => _currentImage;
            set => Set(ref _currentImage, value);
        }

        public void OnImageChanged(object sender, EventArgs e)
        {
            CurrentImage = ConvertToBitmapImage(_imageModel);
        }

        private BitmapImage ConvertToBitmapImage(ImageModel imageModel)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                imageModel.GetImage().Write(memoryStream, MagickFormat.Png);
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

        #region OpenImageViewComand
        public ICommand OpenImageViewCommand { get; }

        private bool CanOpenImageViewCommandExecute(object param) => true;
        private void OnOpenImageViewCommandExecuted(object param)
        {
            FileManager _fileManager = new FileManager();
            ImageModel model = _fileManager.LoadImage();
            if (model == null)
                return;
            _imageModel = model;
            _imageModel.ImageChanged += OnImageChanged;
            CurrentImage = ConvertToBitmapImage(model);
            _session = new Session(_imageModel);
        }

        #endregion

        #region Tools
        public ObservableCollection<Tool> Tools { get; } = new ObservableCollection<Tool>();
        private Tool _selectedTool;
        public Tool SelectedTool
        {
            get => _selectedTool;
            set
            {
                Set(ref _selectedTool, value);
                _selectedTool.Handle();
            }
        }
        #endregion

        #region SelectBrightnessEditorToolViewCommand
        public ICommand ApplyToolViewCommand { get; }
        private bool CanApplyToolViewCommandExecute(object param) => SelectedTool != null && _imageModel != null;
        private void OnApplyToolViewCommandExecuted(object param)
        {
            if(SelectedTool == null)
                return;

            if(SelectedTool.ParametrRequirmetns != null)
            {
                ParametrsRequirments requirments = SelectedTool.ParametrRequirmetns;
                InputDialog dialog = new InputDialog(requirments.FieldsLabels);

                if(dialog.ShowDialog() == true)
                {
                    for (int i = 0; i < requirments.FieldsLabels.Count; i++)
                    {
                        requirments.Parametrs[requirments.FieldsLabels[i]] = double.Parse(dialog.InputValues[i]);
                    }
                    SelectedTool.ApplyTool(_imageModel, _session.GetCommandHistory());
                }
                else
                {
                    return;
                }
            }
        }
        #endregion

        public ImageViewModel(ViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            Tools.Add(new BrightnessEditor());

            #region ViewCommands
            OpenImageViewCommand = new RelayCommand(OnOpenImageViewCommandExecuted, CanOpenImageViewCommandExecute);
            ApplyToolViewCommand = new RelayCommand(OnApplyToolViewCommandExecuted, CanApplyToolViewCommandExecute);
            #endregion
        }
    }
}
