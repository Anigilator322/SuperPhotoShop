using ImageMagick;
using SuperPhotoShop.Infrostructure;
using SuperPhotoShop.Infrostructure.Tool_Commands;
using SuperPhotoShop.Infrostructure.ViewCommands;
using SuperPhotoShop.Models;
using SuperPhotoShop.View.Windows;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
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

        #region ApplyToolViewCommand
        public ICommand ApplyToolViewCommand { get; }
        private bool CanApplyToolViewCommandExecute(object param) => SelectedTool != null && _imageModel != null;
        private void OnApplyToolViewCommandExecuted(object param)
        {
            if (SelectedTool == null)
                return;

            if (SelectedTool.ParametrRequirmetns != null)
            {
                ParametrsRequirments requirments = SelectedTool.ParametrRequirmetns;
                InputDialog dialog = new InputDialog(requirments.FieldsLabels);

                if (dialog.ShowDialog() == true)
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

        #region RevertLastCommandViewCommand
        public ICommand RevertLastCommandViewCommand { get; }
        private bool CanRevertLastCommandViewCommandExecute(object param)
        {
            if(_session!=null)
                return _session.GetCommandHistory().CanUndoCommand;
            else return false;
        }
        private void OnRevertLastCommandViewCommandExecuted(object param)
        {
            ImageModel newImageModel = _session.GetCommandHistory().UndoCommand();
            _session.SetImage(newImageModel);
            _imageModel = newImageModel;
            newImageModel.ImageChanged += OnImageChanged;
            CurrentImage = ConvertToBitmapImage(_imageModel);
        }
        #endregion

        public void InitializeSession(Session session)
        {
            _session = session;
            _imageModel = session.GetImage();
            _imageModel.ImageChanged += OnImageChanged;
            CurrentImage = ConvertToBitmapImage(_imageModel);
        }

        public ImageViewModel(ViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            Tools.Add(new ColorCorrectionTool());
            Tools.Add(new BlurTool());

            #region ViewCommands
            ApplyToolViewCommand = new RelayCommand(OnApplyToolViewCommandExecuted, CanApplyToolViewCommandExecute);
            RevertLastCommandViewCommand = new RelayCommand(OnRevertLastCommandViewCommandExecuted, CanRevertLastCommandViewCommandExecute);
            #endregion
        }
    }
}
