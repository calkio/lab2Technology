using Emgu.CV;
using Emgu.CV.Structure;
using lab2.Data.GenerateDataStrategy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Data.GenerateDataStrategy
{
    internal class ImageData : IGenerateData
    {
        public double[] GenerateData(string pathFile)
        {
            Image<Gray, Byte> image = ReadImageFile(pathFile);
            double[] pixelColors = VectorMergingPhoto(image);
            return pixelColors;
        }

        private Image<Gray, Byte> ReadImageFile(string pathFile)
        {
            Image<Gray, Byte> imageGray = new Image<Gray, byte>(pathFile);
            return imageGray;
        }

        private double[] VectorMergingPhoto(Image<Gray, Byte> image)
        {
            double[] pixelColors = image.Data.Cast<byte>().Select(value => (double)value).ToArray();
            return pixelColors;
        }
    }
}
