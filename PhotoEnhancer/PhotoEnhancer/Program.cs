using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();

            mainForm.AddFilter(new PixelFilter<LighteningParameters>(
                "Осветление / Затемение",
                (pixel, parameters) => parameters.Coefficient * pixel
            ));

            mainForm.AddFilter(new  PixelFilter<EmptyParameters>(
                "Оттенки серого",
                (pixel, parameters) =>
                {
                    var lightness = pixel.R * 0.3 + pixel.G * 0.6 + pixel.B * 0.1;

                    return new Pixel(lightness, lightness, lightness);
                }
                ));

            Application.Run(mainForm);
        }
    }
}
