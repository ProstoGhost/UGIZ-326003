using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    public class GrayScaleFilter : IFilter
    {
        public ParameterInfo[] GetParametersInfo() => new ParameterInfo[0];

        public Photo Process(Photo original, double[] parameters)
        {
            var newPhoto = new Photo(original.Width, original.Height);

            for (var x = 0; x < original.Width; x++)
                for (var y = 0; y < original.Height; y++)
                {
                    var lightness =
                        original[x, y].R * 0.3 +
                        original[x, y].G * 0.6 +
                        original[x, y].B * 0.1;

                    Pixel p = new Pixel(lightness, lightness, lightness);

                    newPhoto[x, y] = p;
                }
                    

            return newPhoto;          
        }

        public override string ToString() => "Оттенки серого";
    }
}
