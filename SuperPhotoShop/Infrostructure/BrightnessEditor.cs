using SuperPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPhotoShop.Infrostructure
{
    public class BrightnessEditor:Tool
    {
        public string Name => "Яркость";
        private float _oldBrightness;
        private float _newBrightness;
        private ImageModel _imageModel;

        public BrightnessEditor()
        {
            List<string> labels = new List<string>();
            labels.Add("Яркость");
            ParametrRequirmetns = new ParametrsRequirments(labels);
        }

        public override void Handle()
        {
            //Return tool properties like color or something like this
            base.Handle();
        }

        public override void ApplyTool(ImageModel imageModel)
        {
            Debug.WriteLine("Apply brightness with params: ");
            foreach(var param in ParametrRequirmetns.Parametrs)
            {
                Debug.WriteLine(param.Key + " " + param.Value);
            }
            base.ApplyTool(imageModel);
        }

        private void CreateCommand(CommandHistory commandHistory)
        {
            ChangeBrightnessCommand command = new ChangeBrightnessCommand(_oldBrightness, _newBrightness, _imageModel);
            commandHistory.ExecuteCommand(command);
        }
    }
}
