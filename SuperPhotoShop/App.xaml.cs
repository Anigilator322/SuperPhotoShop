using SuperPhotoShop.Infrostructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SuperPhotoShop
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Editor _Editor;
        protected override void OnStartup(StartupEventArgs e)
        {
            Editor editor = new Editor();
            editor.Initialize();
        }
    }
}
