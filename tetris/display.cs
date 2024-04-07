using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace tetris
{
    public class Matrix
    {
        public char[,] array;
        public int ratioX = 2;
        public int ratioY = 1;
        private char wall = (char)9608;
        private int width;
        private int height;
        public Matrix(int width, int height) 
        {
            this.width = width;
            this.height = height;
            array = new char[(height + 4) * ratioY, (width+4)*ratioX];
            for (int i = 0; i < width; i++)
                Point(i, 0, wall);
            for (int i = 0; i < height; i++)
                Point(0, i, wall);
            for (int i = 0; i < width; i++)
                Point(i, height - 1, wall);
            for (int i = 0; i < height; i++)
                Point(width - 1, i, wall);
        }
        public char CheckPoint(int x, int y)
        {
            return array[y * ratioY, x * ratioX];
        }
        public void Point(int x, int y, char c)
        {
            for (int i = 0; i < ratioY; i++)
                for (int j = 0; j < ratioX; j++)
                    array[y * ratioY + i, x * ratioX + j] = c;
        }
        public void Draw(Shape shape)
        {
            for (int i = 0; i < 4; i++)
                Point(shape.ox + shape.xs[shape.index, i], 
                      shape.oy + shape.ys[shape.index, i], shape.symbol);
        }
        public void Clear(Shape shape)
        {
            for (int i = 0; i < 4; i++)
                Point(shape.ox + shape.xs[shape.index, i], 
                      shape.oy + shape.ys[shape.index, i], ' ');
        }
        public void OutPut(string info)
        {
            for (int i = 0; i < height * ratioY; i++)
            {
                for (int j = 0; j < width * ratioX; j++)
                    Console.Write(array[i, j]);
                Console.WriteLine();
            }
            Console.Write(info);
            //
        }
    }

    public class Display
    {
        private int width;
        private int height;
        public Matrix matrix;
        private char wall = (char)9608;
        private int speed = 600;
        private int score = 0;
        private string info = $"Total score:0\nRotate: TAB\nMoves: Arrows";
        public Display(int width, int height)
        {
            this.width = width;
            this.height = height;
            matrix = new Matrix(width, height);
        }
        private Shape GenerateFig()
        {
            int startx = width / 2;
            
            int starty = 1;
            Shape[] shapes = new Shape[] { new Stick(startx, starty),
                new Square(startx, starty),
                new Horse1(startx, starty),
                new Horse2(startx, starty),
                new Zigzag1(startx, starty),
                new Zigzag2(startx, starty),
                new Triangle(startx, starty)};
            Random random = new Random();
            return shapes[random.Next(shapes.Length)];
        }
        public void StartGame()
        {
            
            while (true)
            {
                bool gameStoped = true;
                Shape shape = GenerateFig();
                //Shape shape = new Square(width / 2, 1);
                while (AbleToDrop(shape))
                {
                    matrix.Clear(shape);
                    if (AbleToDrop(shape))
                        shape.oy++;
                    matrix.Draw(shape);
                    gameStoped = false;
                    DateTime startTime = DateTime.Now;
                    while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(speed))
                    {
                        matrix.Clear(shape);
                        MoveFigure(shape);
                        matrix.Draw(shape);
                        //Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        matrix.OutPut(info);
                        Thread.Sleep(5);
                    }
                    
                    //Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    matrix.OutPut(info);
                }
                CheckLinesAndDrop(shape);
                info = $"Total score:{score}\nRotate: TAB\nMoves: Arrows";
                if (gameStoped)
                {
                    matrix = new Matrix(width, height);
                    string massage = "Game over!";
                    for (int i = 0; i < massage.Length; i++)
                        matrix.array[(height * matrix.ratioY)/2, i+((width * matrix.ratioX)/ massage.Length * 4)] = massage[i];
                    Console.SetCursorPosition(0, 0);
                    matrix.OutPut("");
                    return;
                }

            }
        }
        private void MoveFigure(Shape shape)
        {
            
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.LeftArrow &&
                    AbleToMoveLeft(shape))
                    shape.ox--;

                if (consoleKeyInfo.Key == ConsoleKey.RightArrow && 
                    AbleToMoveRight(shape))
                    shape.ox++;

                if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
                    while (AbleToDrop(shape))
                        shape.oy++;

                if (consoleKeyInfo.Key == ConsoleKey.DownArrow && 
                    AbleToDrop(shape))
                    shape.oy++;

                if (consoleKeyInfo.Key == ConsoleKey.Tab && 
                    AbleToRotate(shape))
                    shape.Rotate();
                if (consoleKeyInfo.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
            }
            while (Console.KeyAvailable)
            {
                Console.ReadKey(intercept: true);
            }
        }
        
        private bool AbleToRotate(Shape shape)
        {
            bool ans = true;
            for (int i = 0; i < 4; i++)
            {
                char c = matrix.CheckPoint(shape.ox + shape.xs[(shape.index + 1) % (shape.xs.Length / 4), i],
                    shape.oy + shape.ys[(shape.index+1) % (shape.ys.Length/4), i]);
                ans = ans && c != shape.symbol && c != wall;
            }
            return ans;
        }
        private bool AbleToDrop(Shape shape)
        {
            bool ans = true;
            for (int i = 0; i < shape.heigts.Length; i++)
            {
                char c = matrix.CheckPoint(shape.ox + i, shape.oy + shape.heigts[i]);
                ans = ans && c != shape.symbol && c != wall;
            }
            return ans;
        }
        private bool AbleToMoveRight(Shape shape)
        {
            bool ans = true;
            for (int i = 0; i < 4; i++)
            {
                char c = matrix.CheckPoint(shape.ox + shape.xs[shape.index, i] + 1,
                    shape.oy + shape.ys[shape.index, i]);
                ans = ans && c != shape.symbol && c != wall;
            }
            return ans;
        }
        private bool AbleToMoveLeft(Shape shape)
        {
            bool ans = true;
            for (int i = 0; i < 4; i++)
            {
                char c = matrix.CheckPoint(shape.ox + shape.xs[shape.index, i] - 1,
                    shape.oy + shape.ys[shape.index, i]);
                ans = ans && c != shape.symbol && c != wall;
            }
            return ans;
        }

        private void CheckLinesAndDrop(Shape shape)
        {
            for (int i = 0; i < height; i++)
            {
                int count = 0;
                for (int j = 1; j < width - 1; j++)
                    if (matrix.CheckPoint(j, i) == shape.symbol)
                    {
                        count++;
                    }
                if (count == width - 2)
                {
                    if (speed > 200)
                        speed -= 100;
                    for (int t = i; t > 1; t--)
                        for (int j = 1; j < width - 1; j++)
                            matrix.Point(j, t, matrix.CheckPoint(j, t - 1));
                    score += width;
                }
            }
        }
    }
}
