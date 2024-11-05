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

        public override void ApplyTool(ImageModel imageModel, CommandHistory commandHistory)
        {
            
            Debug.WriteLine("Apply brightness with params: ");
            foreach(var param in ParametrRequirmetns.Parametrs)
            {
                Debug.WriteLine(param.Key + " " + param.Value);
            }

            CreateCommand(commandHistory, imageModel);
        }

        private void CreateCommand(CommandHistory commandHistory, ImageModel imageModel)
        {
            ChangeBrightnessCommand command = new ChangeBrightnessCommand((double)ParametrRequirmetns.Parametrs["Яркость"], imageModel);
            commandHistory.ExecuteCommand(command);
        }
    }
}
