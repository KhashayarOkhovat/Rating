using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleSample.Refactor
{
    public interface IShape
    {
        int CalcArea();
    }

    public class Rectangle : IShape
    {
        private int Hight;
        private int Width;
        public Rectangle(int hight, int width)
        {
            Hight = hight;
            Width = width;
        }
        public int CalcArea()
        {
            return Hight * Width;
        }
    }

    public class Square : IShape
    {
        private int Side;
        public Square(int side)
        {
            Side = side;
        }
        public int CalcArea()
        {
            return Side * Side;
        }
    }
}
