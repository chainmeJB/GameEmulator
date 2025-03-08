using System;

namespace OnlineCasino
{
    public class ConsoleMenu
    {
        private static readonly GameFacade gameFacade = new GameFacade();
        private readonly FileManager fileManager = new FileManager(gameFacade);

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
                    fileManager.LoadGame();
                    PlayGame();
                    break;
                case "2":
                    gameFacade.StartNewGame();
                    PlayGame();
                    break;
                default:
                    Console.WriteLine("Некоректний ввод.");
                    DisplayMenu();
                    break;
            }
        }

        public void PlayGame()
        {
            bool isRunning = true;
            RouletteUI.DisplayRoulette(gameFacade.GetChips());

            while (isRunning)
            {
                Console.WriteLine("Введіть суму ставки: ");
                if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
                {
                    Console.WriteLine("Некоректний ввод. Введіть додатне число.");
                    continue;
                }
                gameFacade.SetBetInfo(amount, BetField.BetAmount);

                if (!gameFacade.PlaceBet())
                {
                    Console.WriteLine($"Недостатньо фішок. Ваші фішки: {gameFacade.GetChips()}");
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

                        gameFacade.SetBetInfo(1, BetField.BetCategory);
                        gameFacade.SetBetInfo(chosenNumber, BetField.ChosenNumber);
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

                        gameFacade.SetBetInfo(2, BetField.BetCategory);
                        gameFacade.SetBetInfo(color, BetField.ChosenNumber);
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

                        gameFacade.SetBetInfo(3, BetField.BetCategory);
                        gameFacade.SetBetInfo(group, BetField.ChosenNumber);
                        break;

                    default:
                        Console.WriteLine("Некоректний ввод.");
                        break;
                }

                gameFacade.SpinRouletteWheel();

                if (gameFacade.CheckWin())
                {
                    Console.WriteLine($"Вітаємо! Випало число: {gameFacade.GetBetInfo(BetField.RolledNumber)}");
                    gameFacade.EvaluateBet();
                }
                else
                {
                    Console.WriteLine($"Ви програли. Випало число: {gameFacade.GetBetInfo(BetField.RolledNumber)}");
                }

                Console.WriteLine($"Ваші фішки: {gameFacade.GetChips()}");

                if (gameFacade.GetChips() > 0)
                {
                    fileManager.SaveGame();

                    Console.WriteLine("1. Ставити ще раз\n" +
                        "2. Вийти в меню");

                    string option = Console.ReadLine();

                    if (option == "2")
                    {
                        isRunning = false;
                    }
                }

                if (gameFacade.GetChips() <= 0)
                {
                    Console.WriteLine("Ваші фішки закінчилися. Ви програли.");
                    fileManager.DeleteSave();

                    Console.WriteLine();
                    Console.WriteLine("Натисніть клавішу, щоб повернутися в головне меню...");
                    Console.ReadKey();
                    isRunning = false;
                }
            }
            Console.Clear();
            DisplayMenu();
        }
    }
}