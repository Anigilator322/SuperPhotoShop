
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
    }
}
