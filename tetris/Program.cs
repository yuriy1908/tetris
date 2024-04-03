using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите высоту (рекомендованно 30)...");
            int hight = int.Parse(Console.ReadLine());
            Console.WriteLine("и длину тетриса (рекомендованно 50)...");
            int width = int.Parse(Console.ReadLine());
            Console.SetWindowSize(width + 2, hight + 5);
            Display display = new Display(width, hight);
            display.StartGame();
            //Square square = new Square(38, 40);
            //Console.WriteLine(display.matrix[2, 2]);
            //9608
        }
    }
}