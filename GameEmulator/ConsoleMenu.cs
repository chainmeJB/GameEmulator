using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEmulator
{
    public class ConsoleMenu
    {
        private readonly SystemSpecs specs;
        readonly Game strategyGame = GameFactory.CreateGame(GameType.Strategy, "Битва королівств", "Intel core i3", 8, "AMD Radeon RX 580", 3);
        readonly Game simulatorGame = GameFactory.CreateGame(GameType.Simulator, "Симулятор водія", "AMD Ryzen 3", 16, "NVIDIA GeForce RTX 3060", 7);
        readonly Game onlineCasino = GameFactory.CreateGame(GameType.OnlineCasino, "Рулетка");

        public ConsoleMenu()
        {
            specs = SystemSpecs.GetCurrentSystemSpecs();
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("---Головне меню---\n" +
                    "1. Встановити гру\n" +
                    "2. Розпочати гру\n" +
                    "3. Зупинити гру");

                string input = Console.ReadLine();

                switch (input)
                {   
                    case "1":
                        InstallGames();
                        break;
                    case "2":
                        if (Game.RunningGame != null)
                        {
                            Console.WriteLine("Відкрита інша гра. Спочатку закрийте її");
                            break;
                        }
                        PlayGames();
                        break;
                    case "3":
                        if (Game.RunningGame != null)
                        {
                            Game.RunningGame.StopGame();
                            Console.WriteLine("Гра успішно зупинена");
                        }
                        else
                        {
                            Console.WriteLine("Немає активних ігр для зупинення");
                        }
                        break;
                    default:
                        Console.WriteLine("Некоректний ввод");
                        break;
                }
            }
        }

        public void InstallGames()
        {
            Console.WriteLine("Виберіть гру, яку хочете встановити\n" +
                            $"1. {strategyGame.Name}\n" +
                            $"2. {simulatorGame.Name}");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (strategyGame.IsInstalled)
                    {
                        Console.WriteLine("Ця гра вже встановлена");
                        return;
                    }
                    else if(!strategyGame.CheckHDDMemory(specs))
                    {
                        Console.WriteLine("Недостатньо місця на диску");
                        return;
                    }
                    strategyGame.Install(specs);
                    Console.WriteLine("Гра встановлена.");
                    break;
                case "2":
                    if (simulatorGame.IsInstalled)
                    {
                        Console.WriteLine("Ця гра вже встановлена");
                        return;
                    }
                    else if (!simulatorGame.CheckHDDMemory(specs))
                    {
                        Console.WriteLine("Недостатньо місця на диску");
                        return;
                    }
                    simulatorGame.Install(specs);
                    Console.WriteLine("Гра встановлена.");
                    break;
                default:
                    Console.WriteLine("Некоректний ввод");
                    break;
            }
        }

        public void PlayGames()
        {
            Console.WriteLine("Виберіть гру для запуску.\n" +
                            $"1. {strategyGame.Name}\n" +
                            $"2. {simulatorGame.Name}\n" +
                            $"3. {onlineCasino.Name}");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (!strategyGame.IsInstalled)
                    {
                        Console.WriteLine("Ця гра не встановлена");
                        return;
                    }
                    else if (!strategyGame.CheckRequirments(specs))
                    {
                        Console.WriteLine("Система не відповідає вимогам");
                        return;
                    }
                    strategyGame.StartGame();
                    break;
                case "2":
                    if (!simulatorGame.IsInstalled)
                    {
                        Console.WriteLine("Ця гра не встановлена");
                        return;
                    }
                    else if(!simulatorGame.CheckRequirments(specs))
                    {
                        Console.WriteLine("Система не відповідає вимогам");
                        return;
                    }
                    simulatorGame.StartGame();
                    break;
                case "3":
                    if (!onlineCasino.CheckRequirments(specs))
                    {
                        Console.WriteLine("Система не відповідає вимогам");
                        return;
                    }
                    onlineCasino.StartGame();
                    break;
                default:
                    Console.WriteLine("Некоректний ввод");
                    break;
            }
        }
    }
}
