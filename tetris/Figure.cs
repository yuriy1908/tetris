using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public abstract class Shape
    {
        public int ox;
        public int oy;
        public int[] heigts;
        public int[,] xs;
        public int[,] ys;
        public char symbol = '░';
        public int index = 0;
        public abstract void Rotate();
        public Shape(int ox, int oy)
        {
            this.ox = ox;
            this.oy = oy;
        }
        }
    public class Stick: Shape
    {
        public Stick(int ox, int oy)
            :base(ox, oy)
        {
            this.heigts = new int[] { 1, 1, 1, 1 };
            this.xs = new int[,] { { 0, 1, 2, 3 }, { 0, 0, 0, 0 } };
            this.ys = new int[,] { { 0, 0, 0, 0 }, { 0, 1, 2, 3 } };
        }
        public override void Rotate()
        {
            index = (index + 1) % 2;
            if (index == 0)
            {
                this.heigts = new int[] { 1, 1, 1, 1 };
            }
            else
            {
                this.heigts = new int[] { 4 };
            }
        }
    }
    public class Square: Shape
    {
        public Square(int ox, int oy)
            : base(ox, oy)
        {
            this.heigts = new int[] { 2, 2 };
            this.xs = new int[,] { { 0, 0, 1, 1 } };
            this.ys = new int[,] { { 0, 1, 0, 1 } };
        }
        public override void Rotate() { }
    }
    public class Horse1 : Shape
    {
        public Horse1(int ox, int oy)
            : base(ox, oy)
        {
            this.heigts = new int[] { 1, 3 };
            this.xs = new int[,] { { 0, 1, 1, 1 }, { 2, 0, 1, 2}, { 0, 0, 0, 1}, { 0, 1, 2, 0} };
            this.ys = new int[,] { { 0, 0, 1, 2 }, { 0, 1, 1, 1}, { 0, 1, 2, 2}, { 1, 0, 0, 0 } };
        }
        public override void Rotate()
        {
            index = (index + 1) % 4;
            switch (index)
            {
                case 0:
                    this.heigts = new int[] { 1, 3 };
                    break;
                case 1:
                    this.heigts = new int[] { 2, 2, 2 };
                    break;
                case 2:
                    this.heigts = new int[] { 3, 3 };
                    break;
                default:
                    this.heigts = new int[] { 2, 1, 1 };
                    break;

            }
        }
    }
    public class Horse2 : Shape
    {
        public Horse2(int ox, int oy)
            : base(ox, oy)
        {
            this.heigts = new int[] { 3, 1 };
            this.xs = new int[,] { { 0, 1, 0, 0 }, { 0, 1, 2, 2 }, { 1, 1, 0, 1}, { 0, 0, 1, 2 } };
            this.ys = new int[,] { { 0, 0, 1, 2 }, { 0, 0, 0, 1 }, { 0, 1, 2, 2}, { 0, 1, 1, 1 } };
        }
        public override void Rotate()
        {
            index = (index + 1) % 4;
            switch (index)
            {
                case 0:
                    this.heigts = new int[] { 3, 1 };
                    break;
                case 1:
                    this.heigts = new int[] { 1, 1, 2 };
                    break;
                case 2:
                    this.heigts = new int[] { 3, 3 };
                    break;
                default:
                    this.heigts = new int[] { 2, 2, 2 };
                    break;

            }
        }
    }
    public class Zigzag1 : Shape
    {
        public Zigzag1(int ox, int oy)
            : base(ox, oy)
        {
            this.heigts = new int[] { 1, 2, 2 };
            this.xs = new int[,] { { 0, 1, 1, 2}, { 1, 0, 1, 0 } };
            this.ys = new int[,] { { 0, 0, 1, 1}, { 0, 1, 1, 2 } };
        }
        public override void Rotate()
        {
            index = (index+1)%2;
            if (index == 0)
            {
                this.heigts = new int[] { 1, 2, 2 };
            }
            else
            {
                this.heigts = new int[] { 3, 2 };
            }
        }
    }
    public class Zigzag2 : Shape
    {
        
        public Zigzag2(int ox, int oy)
            : base(ox, oy)
        {
            this.heigts = new int[] { 2, 2, 1 };
            this.xs = new int[,] { { 1, 2, 0, 1 }, { 0, 0, 1, 1 } };
            this.ys = new int[,] { { 0, 0, 1, 1 }, { 0, 1, 1, 2 } };
        }
        public override void Rotate()
        {
            index = (index + 1) % 2;
            if (index == 0)
            {
                this.heigts = new int[] { 2, 2, 1 };
            }
            else
            {
                this.heigts = new int[] { 2, 3 };
            }
        }
    }
    public class Triangle : Shape
    {
        public Triangle(int ox, int oy)
            : base(ox, oy)
        {
            this.heigts = new int[] { 2, 2, 2 };
            this.xs = new int[,] { { 1, 0, 1, 2 }, { 0, 0, 1, 0 }, { 0, 1, 2, 1 }, { 1, 0, 1, 1 } };
            this.ys = new int[,] { { 0, 1, 1, 1 }, { 0, 1, 1, 2 }, { 0, 0, 0, 1 }, { 0, 1, 1, 2 } };
        }
        public override void Rotate()
        {
            index = (index + 1) % 4;
            switch (index)
            {
                case 0:
                    this.heigts = new int[] { 2, 2, 2 };
                    break;
                case 1:
                    this.heigts = new int[] { 3, 2 };
                    break;
                case 2:
                    this.heigts = new int[] { 1, 2, 1 };
                    break;
                default:
                    this.heigts = new int[] { 2, 3 };
                    break;

            }
        }
    }
}
