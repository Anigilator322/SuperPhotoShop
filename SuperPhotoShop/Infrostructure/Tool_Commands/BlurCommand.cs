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
        private double _radius;
        private double _sigma;

        public BlurCommand(double newRadius, double newSigma)
        {
            _radius = newRadius;
            _sigma = newSigma;
        }

        public override void Execute(ImageModel imageModel)
        {
            _imageOld = (MagickImage)imageModel.GetImage().Clone();
            imageModel.SetBlur(_radius, _sigma);
        }

        public override MagickImage Undo()
        {
            return _imageOld;
        }
    }
}
