using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public struct Pixel
    {
        double r;
        public double R 
        { 
            get => r; 
            set => r = CheckValue(value); 
        }

        double g;
        public double G
        {
            get => g;
            set => g = CheckValue(value);
        }

        double b;
        public double B
        {
            get => b;
            set => b = CheckValue(value);
        }

        public Pixel(double red, double green, double blue) : this()
        {
            R = red;
            G = green;
            B = blue;
        }

        //public Pixel() : this(0, 0, 0) { }

        private double CheckValue(double val)
        {
            if (val < 0 || val > 1)
                throw new ArgumentException("Неверная яркость канала");

            return val;
        }

        public static Pixel operator *(double k, Pixel p)
        {
            var result = new Pixel();

            result.R = Trim(p.R * k);
            result.G = Trim(p.G * k);
            result.B = Trim(p.B * k);

            return result;
        }

        static double Trim(double lightness) => lightness > 1 ? 1 : lightness;

    }
}
