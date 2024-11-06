using SuperPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public class BlurTool : Tool
    {
        

        private List<string> _labels = new List<string>();

        public BlurTool()
        {
            Name = "Блюр";
            _labels.Add("Радиус");
            _labels.Add("Сигма");
            ParametrRequirmetns = new ParametrsRequirments(_labels);
        }

        public override void Handle()
        {

        }

        public override void ApplyTool(ImageModel imageModel, CommandHistory commandHistory)
        {
            CreateCommand(commandHistory, imageModel);
        }

        protected override void CreateCommand(CommandHistory commandHistory, ImageModel imageModel)
        {
            BlurCommand command = new BlurCommand((double)ParametrRequirmetns.Parametrs[_labels[0]], (double)ParametrRequirmetns.Parametrs[_labels[1]], imageModel);
            commandHistory.ExecuteCommand(command);
        }
    }
}
