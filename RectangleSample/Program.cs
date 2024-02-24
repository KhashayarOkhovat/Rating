using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle() { Width = 2, Height = 5 };
            int rectArea = AreaCalculator.CalcArea(rect);

            Console.WriteLine($"Rectangle Area = {rectArea}");

            //

            Rectangle square = new Square() { Height = 2, Width = 10 };
            int squareArea = AreaCalculator.CalcArea(square);
            Console.WriteLine($"Square Area = {squareArea}");
        }
    }
}
