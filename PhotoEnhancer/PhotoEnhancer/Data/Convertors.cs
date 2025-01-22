using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public static class Convertors
    {
        public static Photo BitmapToPhoto(Bitmap bmp)
        {
            var photo = new Photo(bmp.Width, bmp.Height);

            for(var x = 0; x < bmp.Width; x++)
                for(var y = 0; y < bmp.Height; y++)
                {
                    var color = bmp.GetPixel(x, y);

                    Pixel p = new Pixel(
                        color.R / 255.0,
                        color.G / 255.0,
                        color.B / 255.0);

                    photo[x, y] = p;
                }

            return photo;
        }

        public static Bitmap PhotoToBitmap(Photo photo) 
        {
            var bmp = new Bitmap(photo.Width, photo.Height);

            for (var x = 0; x < photo.Width; x++)
                for (var y = 0; y < photo.Height; y++)
                    bmp.SetPixel(x, y,
                        Color.FromArgb(
                            (int)Math.Round(photo[x, y].R * 255),
                            (int)Math.Round(photo[x, y].G * 255),
                            (int)Math.Round(photo[x, y].B * 255)
                            ));

            return bmp;
        }
    }
}
