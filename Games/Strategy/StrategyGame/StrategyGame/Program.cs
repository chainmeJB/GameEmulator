using System;
using System.Text;

namespace StrategyGame
{
    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            ConsoleMenu menu = new ConsoleMenu();
            menu.DisplayMenu();
        }
    }
}