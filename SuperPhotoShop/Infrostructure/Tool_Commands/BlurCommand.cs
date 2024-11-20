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
        public double _radius { get; set; }
        public double _sigma { get; set; }
        
        

        public BlurCommand()
        {

        }
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
