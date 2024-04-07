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
            //input
            Console.WriteLine("Write hight (recommended 30)...");
            int hight = int.Parse(Console.ReadLine());
            Console.WriteLine("and width of tetris (recommended 20)...");
            int width = int.Parse(Console.ReadLine());
            
            Display display = new Display(width, hight);

            Console.SetWindowSize((width + 2) * display.matrix.ratioX, (hight + 5) * display.matrix.ratioY);

            display.StartGame();

        }
    }
}
