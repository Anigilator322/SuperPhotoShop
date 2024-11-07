using ImageMagick;
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

        public BlurCommand(double newRadius, double newSigma)
        {
            _newRadius = newRadius;
            _newSigma = newSigma;
        }

        public override void Execute(ImageModel imageModel)
        {
            _imageOld = (MagickImage)imageModel.GetImage().Clone();
            imageModel.GetImage().Blur(_newRadius, _newSigma);
            imageModel.OnImageChanged();
        }

        public override MagickImage Undo()
        {
            return _imageOld;
        }
    }
}
