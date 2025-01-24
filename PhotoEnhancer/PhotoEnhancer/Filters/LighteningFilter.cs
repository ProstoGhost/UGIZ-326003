﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    public class LighteningFilter : PixelFilter<LighteningParameters>
    {
         
        public override Pixel ProcessPixel(Pixel pixel, LighteningParameters parameters)
        {
            return parameters.Coefficient * pixel;
        }

        public override string ToString() => "Осветление / Затемение";
    }
}
