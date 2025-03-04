using System;
using System.Text;

namespace SimulatorGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ConsoleMenu menu = new ConsoleMenu();
            menu.DisplayMenu();

        }
    }
}
