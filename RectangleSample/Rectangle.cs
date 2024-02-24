using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleSample
{
    public class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Square : Rectangle
    {

    }

    public class AreaCalculator
    {
        public static int CalcArea(Rectangle rect)
        {
            if (rect is Square)
            {
                return rect.Height * rect.Height;
            }
            else
            {
                return rect.Height * rect.Width;
            }
        }

    }
}
