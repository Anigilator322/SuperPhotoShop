using SuperPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public class ColorCorrectionTool:Tool
    {
        private List<string> _labels = new List<string>();
        public ColorCorrectionTool()
        {
            Name = "Цветокор";

            _labels.Add("Яркость");
            _labels.Add("Насыщенность");
            _labels.Add("Тон");
            ParametrRequirmetns = new ParametrsRequirments(_labels);
        }

        public override void Handle()
        {
            //Return tool properties like color or something like this
        }

        public override void ApplyTool(ImageModel imageModel, CommandHistory commandHistory)
        {
            CreateCommand(commandHistory, imageModel);
        }

        protected override void CreateCommand(CommandHistory commandHistory, ImageModel imageModel)
        {
            ColorCorrectionCommand command = new ColorCorrectionCommand(
                (double)ParametrRequirmetns.Parametrs[_labels[0]],
                (double)ParametrRequirmetns.Parametrs[_labels[1]],
                (double)ParametrRequirmetns.Parametrs[_labels[2]],
                imageModel);
            commandHistory.ExecuteCommand(command);
        }
    }
}
