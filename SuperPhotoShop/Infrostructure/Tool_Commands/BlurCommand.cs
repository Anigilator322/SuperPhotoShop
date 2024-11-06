using SuperPhotoShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPhotoShop.Infrostructure.Tool_Commands
{
    public class BlurCommand : Command
    {
        private double _newRadius;
        private double _newSigma;

        public BlurCommand(double newRadius, double newSigma, ImageModel imageModel)
        {
            _newRadius = newRadius;
            _newSigma = newSigma;
            _imageModel = imageModel;
        }

        public override void Execute()
        {
            _imageModel.GetImage().Blur(_newRadius, _newSigma);
            _imageModel.OnImageChanged();
        }

        public override void Redo()
        {
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
