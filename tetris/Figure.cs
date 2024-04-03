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
        public char[,] pic = new char[4, 4];
        public int[] heigts;
        public int[] widthsr;
        public int[] widthsl;
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
        public void Clear()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    pic[i, j] = (char)0;
        }
    }
    public class Stick: Shape
    {
        public Stick(int ox, int oy)
            :base(ox, oy)
        {
            for (int i = 0; i < 4; i++)
                this.pic[0, i] = symbol;
            this.heigts = new int[] { 1, 1, 1, 1 };
            this.widthsr = new int[] { 4 };
            this.widthsl = new int[] { 1 };
            this.xs = new int[,] { { 0, 1, 2, 3 }, { 0, 0, 0, 0 } };
            this.ys = new int[,] { { 0, 0, 0, 0 }, { 0, 1, 2, 3 } };
        }
        public override void Rotate()
        {
            index = (index + 1) % 2;
            Clear();
            if (index == 0)
            {
                for (int i = 0; i < 4; i++)
                    this.pic[0, i] = symbol;
                this.heigts = new int[] { 1, 1, 1, 1 };
                this.widthsr = new int[] { 4 };
                this.widthsl = new int[] { 1 };
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    this.pic[i, 0] = symbol;
                this.heigts = new int[] { 4 };
                this.widthsr = new int[] { 1, 1, 1, 1 };
                this.widthsl = new int[] { 1, 1, 1, 1 };
            }
        }
    }
    public class Square: Shape
    {
        public Square(int ox, int oy)
            : base(ox, oy)
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    this.pic[i, j] = symbol;
            this.heigts = new int[] { 2, 2 };
            this.widthsr = new int[] { 2, 2 };
            this.widthsl = new int[] { 1, 1 };
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
            this.pic[0, 0] = symbol;
            for (int i = 0; i < 3; i++)
                this.pic[i, 1] = symbol;
            this.heigts = new int[] { 1, 3 };
            this.widthsr = new int[] { 2, 2, 2 };
            this.widthsl = new int[] {1, 0, 0 };
            this.xs = new int[,] { { 0, 1, 1, 1 }, { 2, 0, 1, 2}, { 0, 0, 0, 1}, { 0, 1, 2, 0} };
            this.ys = new int[,] { { 0, 0, 1, 2 }, { 0, 1, 1, 1}, { 0, 1, 2, 2}, { 1, 0, 0, 0 } };
        }
        public override void Rotate()
        {
            index = (index + 1) % 4;
            Clear();
            switch (index)
            {
                case 0:
                    this.pic[0, 0] = symbol;
                    for (int i = 0; i < 3; i++)
                        this.pic[i, 1] = symbol;
                    this.heigts = new int[] { 1, 3 };
                    this.widthsr = new int[] { 2, 2, 2 };
                    this.widthsl = new int[] { 1, 0, 0 };
                    break;
                case 1:
                    this.pic[0, 2] = symbol;
                    for (int i = 0; i < 3; i++)
                        this.pic[1, i] = symbol;
                    this.heigts = new int[] { 2, 2, 2 };
                    this.widthsr = new int[] { 3, 3 };
                    this.widthsl = new int[] { -1, 1 };
                    break;
                case 2:
                    this.pic[2, 1] = symbol;
                    for (int i = 0; i < 3; i++)
                        this.pic[i, 0] = symbol;
                    this.heigts = new int[] { 3, 3 };
                    this.widthsr = new int[] { 1, 1, 2 };
                    this.widthsl = new int[] { 1, 1, 1 };
                    break;
                default:
                    this.pic[1, 0] = symbol;
                    for (int i = 0; i < 3; i++)
                        this.pic[0, i] = symbol;
                    this.heigts = new int[] { 2, 1, 1 };
                    this.widthsr = new int[] { 3, 1 };
                    this.widthsl = new int[] { 1, 1 };
                    break;

            }
        }
    }
    public class Horse2 : Shape
    {
        public Horse2(int ox, int oy)
            : base(ox, oy)
        {
            this.pic[0, 1] = symbol;
            for (int i = 0; i < 3; i++)
                this.pic[i, 0] = symbol;
            this.heigts = new int[] { 3, 1 };
            this.widthsr = new int[] { 2, 1, 1 };
            this.widthsl = new int[] { 1, 1, 1 };
            this.xs = new int[,] { { 0, 1, 0, 0 }, { 0, 1, 2, 2 }, { 1, 1, 0, 1}, { 0, 0, 1, 2 } };
            this.ys = new int[,] { { 0, 0, 1, 2 }, { 0, 0, 0, 1 }, { 0, 1, 2, 2}, { 0, 1, 1, 1 } };
        }
        public override void Rotate()
        {
            Clear();
            index = (index + 1) % 4;
            switch (index)
            {
                case 0:
                    this.pic[0, 1] = symbol;
                    for (int i = 0; i < 3; i++)
                        this.pic[i, 0] = symbol;
                    this.heigts = new int[] { 3, 1 };
                    this.widthsr = new int[] { 2, 1, 1 };
                    this.widthsl = new int[] { 1, 1, 1 };
                    break;
                case 1:
                    this.pic[1, 2] = symbol;
                    for (int i = 0; i < 3; i++)
                        this.pic[0, i] = symbol;
                    this.heigts = new int[] { 1, 1, 2 };
                    this.widthsr = new int[] { 3, 3 };
                    this.widthsl = new int[] { 1, -1 };
                    break;
                case 2:
                    this.pic[2, 0] = symbol;
                    for (int i = 0; i < 3; i++)
                        this.pic[i, 1] = symbol;
                    this.heigts = new int[] { 3, 3 };
                    this.widthsr = new int[] { 2, 2, 2 };
                    this.widthsl = new int[] { 0, 0, 1 };
                    break;
                default:
                    this.pic[0, 0] = symbol;
                    for (int i = 0; i < 3; i++)
                        this.pic[1, i] = symbol;
                    this.heigts = new int[] { 2, 2, 2 };
                    this.widthsr = new int[] { 1, 3 };
                    this.widthsl = new int[] { 1, 1 };
                    break;

            }
        }
    }
    public class Zigzag1 : Shape
    {
        public Zigzag1(int ox, int oy)
            : base(ox, oy)
        {
            this.pic[0, 0] = symbol;
            this.pic[0, 1] = symbol;
            this.pic[1, 1] = symbol;
            this.pic[1, 2] = symbol;
            this.heigts = new int[] { 1, 2, 2 };
            this.widthsr = new int[] { 2, 3 };
            this.widthsl = new int[] { 1, 0 };
            this.xs = new int[,] { { 0, 1, 1, 2}, { 1, 0, 1, 0 } };
            this.ys = new int[,] { { 0, 0, 1, 1}, { 0, 1, 1, 2 } };
        }
        public override void Rotate()
        {
            Clear();
            index = (index+1)%2;
            if (index == 0)
            {
                this.pic[0, 0] = symbol;
                this.pic[0, 1] = symbol;
                this.pic[1, 1] = symbol;
                this.pic[1, 2] = symbol;
                this.heigts = new int[] { 1, 2, 2 };
                this.widthsr = new int[] { 2, 3 };
                this.widthsl = new int[] { 1, 0 };
            }
            else
            {
                this.pic[0, 1] = symbol;
                this.pic[1, 0] = symbol;
                this.pic[1, 1] = symbol;
                this.pic[2, 0] = symbol;
                this.heigts = new int[] { 3, 2 };
                this.widthsr = new int[] { 2, 2, 1 };
                this.widthsl = new int[] { 0, 1, 1 };
            }
        }
    }
    public class Zigzag2 : Shape
    {
        
        public Zigzag2(int ox, int oy)
            : base(ox, oy)
        {
            this.pic[0, 1] = symbol;
            this.pic[0, 2] = symbol;
            this.pic[1, 0] = symbol;
            this.pic[1, 1] = symbol;
            this.heigts = new int[] { 2, 2, 1 };
            this.widthsr = new int[] { 3, 2 };
            this.widthsl = new int[] { 0, 1 };
            this.xs = new int[,] { { 1, 2, 0, 1 }, { 0, 0, 1, 1 } };
            this.ys = new int[,] { { 0, 0, 1, 1 }, { 0, 1, 1, 2 } };
        }
        public override void Rotate()
        {
            Clear();
            index = (index + 1) % 2;
            if (index == 0)
            {
                this.pic[0, 1] = symbol;
                this.pic[0, 2] = symbol;
                this.pic[1, 0] = symbol;
                this.pic[1, 1] = symbol;
                this.heigts = new int[] { 2, 2, 1 };
                this.widthsr = new int[] { 3, 2 };
                this.widthsl = new int[] { 0, 1 };
            }
            else
            {
                this.pic[0, 0] = symbol;
                this.pic[1, 0] = symbol;
                this.pic[1, 1] = symbol;
                this.pic[2, 1] = symbol;
                this.heigts = new int[] { 2, 3 };
                this.widthsr = new int[] { 1, 2, 2 };
                this.widthsl = new int[] { 1, 1, 0 };
            }
        }
    }
    public class Triangle : Shape
    {
        public Triangle(int ox, int oy)
            : base(ox, oy)
        {
            this.pic[0, 1] = symbol;
            this.pic[1, 0] = symbol;
            this.pic[1, 1] = symbol;
            this.pic[1, 2] = symbol;
            this.heigts = new int[] { 2, 2, 2 };
            this.widthsr = new int[] { 2, 3 };
            this.widthsl = new int[] { 0, 1 };
            this.xs = new int[,] { { 1, 0, 1, 2 }, { 0, 0, 1, 0 }, { 0, 1, 2, 1 }, { 1, 0, 1, 1 } };
            this.ys = new int[,] { { 0, 1, 1, 1 }, { 0, 1, 1, 2 }, { 0, 0, 0, 1 }, { 0, 1, 1, 2 } };
        }
        public override void Rotate()
        {
            Clear();
            index = (index + 1) % 4;
            switch (index)
            {
                case 0:
                    this.pic[0, 1] = symbol;
                    this.pic[1, 0] = symbol;
                    this.pic[1, 1] = symbol;
                    this.pic[1, 2] = symbol;
                    this.heigts = new int[] { 2, 2, 2 };
                    this.widthsr = new int[] { 2, 3 };
                    this.widthsl = new int[] { 0, 1 };
                    break;
                case 1:
                    this.pic[0, 0] = symbol;
                    this.pic[1, 0] = symbol;
                    this.pic[1, 1] = symbol;
                    this.pic[2, 0] = symbol;
                    this.heigts = new int[] { 3, 2 };
                    this.widthsr = new int[] { 1, 2, 1 };
                    this.widthsl = new int[] { 1, 1, 1 };
                    break;
                case 2:
                    this.pic[0, 0] = symbol;
                    this.pic[0, 1] = symbol;
                    this.pic[0, 2] = symbol;
                    this.pic[1, 1] = symbol;
                    this.heigts = new int[] { 1, 2, 1 };
                    this.widthsr = new int[] { 3, 2 };
                    this.widthsl = new int[] { 1, 0 };
                    break;
                default:
                    this.pic[0, 1] = symbol;
                    this.pic[1, 0] = symbol;
                    this.pic[1, 1] = symbol;
                    this.pic[2, 1] = symbol;
                    this.heigts = new int[] { 2, 3 };
                    this.widthsr = new int[] { 2, 2, 2 };
                    this.widthsl = new int[] { 0, 1, 0 };
                    break;

            }
        }
    }
}
