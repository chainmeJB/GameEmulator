using System;
using System.IO;
using GamesSaverLibrary;
using RouletteGameLibrary;

namespace OnlineCasino
{
    public class ConsoleMenu
    {
        readonly RouletteGame rouletteGame = new RouletteGame();
        readonly GameSaver<GameState> gameSaver = new GameSaver<GameState>();

        private static string filePath = "rouletteGameSave.json";

        public void DisplayMenu()
        {
            Console.WriteLine("---Головне меню---\n" +
                "1. Продовжити\n" +
                "2. Нова гра");

            string choice = Console.ReadLine();

            Console.Clear();
            switch (choice)
            {
                case "1":
                    GameState loadedStates = gameSaver.LoadGame(filePath);
                    if (loadedStates != default)
                    {
                        rouletteGame.SetChips(loadedStates.Chips);
                        Console.WriteLine($"Завантажено збережену гру. Ваші фішки: {rouletteGame.Chips}.");
                    }
                    else
                    {
                        Console.WriteLine("Немає даних для завантаження.");
                        DisplayMenu();
                    }
                    PlayGame();
                    break;
                case "2":
                    rouletteGame.SetChips(200);
                    PlayGame();
                    break;
                default:
                    Console.WriteLine("Некоректный ввод.");
                    DisplayMenu();
                    break;
            }
        }

        public void PlayGame()
        {
            bool isRunning = true;

            RouletteUI.DisplayRoulette(rouletteGame.Chips);

            while (isRunning)
            {
                BetInfo betInfo = new BetInfo();

                Console.WriteLine("Введіть суму ставки: ");
                if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
                {
                    Console.WriteLine("Некоректный ввод. Введіть додатне число.");
                    continue;
                }
                betInfo.SetBetAmount(amount);

                if (!rouletteGame.PlaceBet(betInfo))
                {
                    Console.WriteLine($"Недостатньо фішок. Ваші фішки: {rouletteGame.Chips}");
                    continue;
                }

                Console.WriteLine("Виберіть вид ставки: \n" +
                                  "1. Ставка на число\n" +
                                  "2. Ставка на колір (червоний/чорний)\n" +
                                  "3. Ставка на групу чисел (1-12, 13-24, 25-36)");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введіть число (0-36):");
                        if (!int.TryParse(Console.ReadLine(), out int chosenNumber) || chosenNumber < 0 || chosenNumber > 36)
                        {
                            Console.WriteLine("Некоректний ввод. Введіть число від 0 до 36.");
                            continue;
                        }

                        betInfo.SetBetCategory(1);
                        betInfo.SetChosenNumber(chosenNumber);
                        break;

                    case "2":
                        Console.WriteLine("Виберіть колір:\n" +
                                          "1. Червоний\n" +
                                          "2. Чорний");

                        if (!int.TryParse(Console.ReadLine(), out int color) || (color != 1 && color != 2))
                        {
                            Console.WriteLine("Некоректний ввод. Выберіть 1 чи 2.");
                            continue;
                        }

                        betInfo.SetBetCategory(2);
                        betInfo.SetChosenNumber(color);
                        break;

                    case "3":
                        Console.WriteLine("Виберіть групу чисел:\n" +
                                          "1. 1-12\n" +
                                          "2. 13-24\n" +
                                          "3. 25-36");

                        if (!int.TryParse(Console.ReadLine(), out int group) || group < 1 || group > 3)
                        {
                            Console.WriteLine("Некоректний ввод. Виберіть 1, 2 чи 3.");
                            continue;
                        }

                        betInfo.SetBetCategory(3);
                        betInfo.SetChosenNumber(group);
                        break;
                    default:
                        Console.WriteLine("Некоректний ввод.");
                        break;
                }

                rouletteGame.SpinRouletteWheel(betInfo);

                if (rouletteGame.Won(betInfo))
                {
                    Console.WriteLine($"Вітаємо! Випало число: {betInfo.RolledNumber}");
                    rouletteGame.EvaluateBet(betInfo);
                }
                else
                {
                    Console.WriteLine($"Ви програли. Випало число: {betInfo.RolledNumber}");
                }

                Console.WriteLine($"Ваші фішки: {rouletteGame.Chips}");

                if (rouletteGame.Chips > 0)
                {
                    SaveGame();

                    Console.WriteLine("1. Ставити ще раз\n" +
                        "2. Вийти в меню");

                    string option = Console.ReadLine();

                    if (option == "2")
                    {
                        isRunning = false;
                    }
                }

                if (rouletteGame.Chips <= 0)
                {
                    Console.WriteLine("Ваші фішки закінчилися. Ви програли.");
                    File.Delete(filePath);

                    Console.WriteLine();
                    Console.WriteLine("Натисніть клавішу, щоб повернутися в головне меню...");
                    Console.ReadKey();
                    isRunning = false;
                }
            }
            Console.Clear();
            DisplayMenu();
        }

        public void SaveGame()
        {
            GameState gameState = new GameState(rouletteGame.Chips);
            gameSaver.SaveGame(gameState, filePath);
        }
    }
}