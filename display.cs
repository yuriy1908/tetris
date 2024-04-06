using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace tetris
{
    public class Display
    {
        private int width;
        private int height;
        public char[,] matrix;
        private char wall = (char)9608;
        private int speed = 600;
        private int score = 0;
        public Display(int width, int height)
        {
            this.width = width;
            this.height = height;
            ClearMatrix();
        }

        private void ClearMatrix()
        {
            matrix = new char[height+4, width+4];
            for (int i = 0; i < width; i++)
                matrix[0, i] = wall;
            for (int i = 0; i < height; i++)
                matrix[i, 0] = wall;
            for (int i = 0; i < width; i++)
                matrix[height - 1, i] = wall;
            for (int i = 0; i < height; i++)
                matrix[i, width - 1] = wall;;

        }
        private void Draw(Shape shape)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (shape.pic[i, j] == shape.symbol)
                        matrix[shape.oy+i, shape.ox+j] = shape.pic[i, j];
        }
        private void Clear(Shape shape)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (shape.pic[i, j] == shape.symbol)
                        matrix[shape.oy + i, shape.ox + j] = ' ';
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
                    Clear(shape);
                    if (AbleToDrop(shape))
                        shape.oy++;
                    Draw(shape);
                    gameStoped = false;
                    DateTime startTime = DateTime.Now;
                    while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(speed))
                    {
                        Clear(shape);
                        MoveFigure(shape);
                        Draw(shape);
                        //Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        OutPut();
                        Thread.Sleep(5);
                    }
                    
                    //Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    OutPut();
                }
                CheckLinesAndDrop(shape);
                if (gameStoped)
                {
                    ClearMatrix();
                    string massage = "Game over!";
                    for (int i = 0; i < massage.Length; i++)
                        matrix[height/2, i+(width/ massage.Length * 4)] = massage[i];
                    Console.SetCursorPosition(0, 0);
                    OutPut();
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
        private void OutPut()
        {
            string info = $"Total score:{score}\nУправление стрелочками и TAB\nВыйти Esc\n";
            for (int i = 0; i < height+1; i++)
            {
                for (int j = 0; j < width; j++)
                    Console.Write(matrix[i, j]);
                Console.WriteLine();
            }
            Console.Write(info);
            //
        }
        private bool AbleToRotate(Shape shape)
        {
            bool ans = true;
            for (int i = 0; i < 4; i++)
            {
                char c = matrix[shape.oy + shape.ys[(shape.index+1) % (shape.ys.Length/4), i], 
                    shape.ox + shape.xs[(shape.index + 1) % (shape.xs.Length / 4), i]];
                ans = ans && c != shape.symbol && c != wall;
            }
            return ans;
        }
        private bool AbleToDrop(Shape shape)
        {
            bool ans = true;
            for (int i = 0; i < shape.heigts.Length; i++)
            {
                char c = matrix[shape.oy + shape.heigts[i], shape.ox + i];
                ans = ans && c != shape.symbol && c != wall;
            }
            return ans;
        }
        private bool AbleToMoveRight(Shape shape)
        {
            bool ans = true;
            for (int i = 0; i < 4; i++)
            {
                char c = matrix[shape.oy + shape.ys[shape.index, i],
                    shape.ox + shape.xs[shape.index, i] + 1];
                ans = ans && c != shape.symbol && c != wall;
            }
            return ans;
        }
        private bool AbleToMoveLeft(Shape shape)
        {
            bool ans = true;
            for (int i = 0; i < 4; i++)
            {
                char c = matrix[shape.oy + shape.ys[shape.index, i],
                    shape.ox + shape.xs[shape.index, i] - 1];
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
                    if (matrix[i, j] == shape.symbol)
                    {
                        count++;
                    }
                if (count == width - 2)
                {
                    if (speed > 200)
                        speed -= 100;
                    for (int t = i; t > 1; t--)
                        for (int j = 1; j < width - 1; j++)
                            matrix[t, j] = matrix[t - 1, j];
                    score += width;
                }
            }
        }
    }
}
