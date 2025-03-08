using System;

namespace OnlineCasino
{
    public static class RouletteUI
    {
        public static void DisplayRoulette(int chips)
        {
            Console.WriteLine("**********************************************************");
            Console.WriteLine(" ####    ###   #   #  #      #####  #####  #####  #####");
            Console.WriteLine(" #   #  #   #  #   #  #      #        #      #    #");
            Console.WriteLine(" #   #  #   #  #   #  #      #        #      #    #");
            Console.WriteLine(" ####   #   #  #   #  #      ####     #      #    ####");
            Console.WriteLine(" # #    #   #  #   #  #      #        #      #    #");
            Console.WriteLine(" #  #   #   #  #   #  #      #        #      #    #");
            Console.WriteLine(" #   #   ###    ###   #####  #####    #      #    #####");
            Console.WriteLine("**********************************************************\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Красные числа: 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("ЧЕРНЫЕ ЧИСЛА: 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35");

            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("0");
            Console.ResetColor();

            Console.WriteLine($"\nОставшиеся фишки: {chips}");
            Console.WriteLine("**********************************************************");
        }
    }
}
