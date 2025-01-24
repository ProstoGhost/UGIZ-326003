using PhotoEnhancer.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public abstract class PixelFilter : ParamerizedFilter
    {
        public PixelFilter(IParameters parameters) : base(parameters) { }

        public override Photo Process(Photo original, IParameters parameters)
        {
            var newPhoto = new Photo(original.Width, original.Height);

            for (var x = 0; x < original.Width; x++)
                for (var y = 0; y < original.Height; y++)
                    newPhoto[x, y] = ProcessPixel(original[x, y], parameters);

            return newPhoto;
        }

        public abstract Pixel ProcessPixel(Pixel pixel, IParameters parameters);
    }
}
