using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    public class GrayScaleFilter : PixelFilter
    {
        public override ParameterInfo[] GetParametersInfo() => new ParameterInfo[0];

        public override Pixel ProcessPixel(Pixel pixel, double[] parameters)
        {
            var lightness = pixel.R * 0.3 + pixel.G * 0.6 + pixel.B * 0.1;

            return new Pixel(lightness, lightness, lightness);
        }

        public override string ToString() => "Оттенки серого";
    }
}
