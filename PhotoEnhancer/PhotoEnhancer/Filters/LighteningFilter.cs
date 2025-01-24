using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    public class LighteningFilter : PixelFilter
    {
        public LighteningFilter() : base(new LighteningParameters()) { }
         
        public override Pixel ProcessPixel(Pixel pixel, IParameters parameters)
        {
            return (parameters as LighteningParameters).Coefficient * pixel;
        }

        public override string ToString() => "Осветление / Затемение";
    }
}
