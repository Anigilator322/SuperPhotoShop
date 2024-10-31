using SuperPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPhotoShop.Infrostructure
{
    public class BrightnessEditor:Tool
    {
        public CommandHistory CommandHistory;
        private float _oldBrightness;
        private float _newBrightness;
        private ImageModel _imageModel;
        public override void Handle()
        {
            base.Handle();
        }
        private void CreateCommand()
        {
            ChangeBrightnessCommand command = new ChangeBrightnessCommand(_oldBrightness, _newBrightness, _imageModel);
            CommandHistory.ExecuteCommand(command);
        }
    }
}
