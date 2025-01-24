using PhotoEnhancer.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class TransformFilter : ParametrizedFilter<EmptyParameters>
    {
        //по размеру старого фото определяем размер нового
        Func<Size, Size> sizeTransform;

        //по точке нового фото и размеру старого определяем,
        //какая точка старого фото соответсвует этой точке новго фото
        Func<Point, Size, Point> pointTransform;

        public TransformFilter(string name,
            Func<Size, Size> sizeTransform,
            Func<Point, Size, Point> pointTransform)
        {
            this.name = name;
            this.sizeTransform = sizeTransform;
            this.pointTransform = pointTransform;
        }

        public override Photo Process(Photo original, EmptyParameters parameters)
        {
            var oldSize = new Size(original.Width, original.Height);
            var newSize = sizeTransform(oldSize);

            var newPhoto = new Photo(newSize.Width, newSize.Height);

            for(var x = 0; x < newSize.Width; x++)
                for(var y = 0; y < newSize.Height; y++)
                {
                    var oldPoint = pointTransform(new Point(x, y), oldSize);
                    newPhoto[x, y] = original[oldPoint.X, oldPoint.Y];
                }

            return newPhoto;
        }
    }
}
