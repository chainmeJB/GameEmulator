using System;
using System.IO;
using GamesSaverLibrary;

namespace StrategyGame
{
    public class ConsoleMenu
    {
        readonly Game game = new Game(100, 5, 100, 5);
        readonly GameSaver<GameState> gameSaver = new GameSaver<GameState>();

        private static string filePath = "strategyGameSave.json";
        public void DisplayMenu()
        {
            Console.WriteLine("---Головне меню---" +
                "\n1. Продовжити\n" +
                "2. Нова гра");

            string choice = Console.ReadLine();

            Console.Clear();
            switch (choice)
            {
                case "1":
                    GameState loadedStates = gameSaver.LoadGame(filePath);
                    if (loadedStates != null)
                    {
                        game.Player.SetGold(loadedStates.PlayerGold);
                        game.Player.SetArmy(loadedStates.PlayerArmy);
                        game.Enemy.SetGold(loadedStates.EnemyGold);
                        game.Enemy.SetArmy(loadedStates.EnemyArmy);
                        Console.WriteLine($"Завантажено збережену гру.");
                    }
                    else
                    {
                        Console.WriteLine("Немає даних для завантаження.");
                        DisplayMenu();
                    }
                    PlayGame();
                    break;
                case "2":
                    game.Player.SetGold(100);
                    game.Player.SetArmy(5);
                    game.Enemy.SetGold(100);
                    game.Enemy.SetArmy(5);
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
            while (!game.IsGameOver())
            { 
                ShowStatus(game);
                Console.WriteLine("1. Зробити ход\n" +
    "2. Зберегти та вийти в меню");

                switch (Console.ReadLine())
                {
                    case "1":
                        PlayerTurn(game);
                        break;
                    case "2":
                        SaveGame();
                        DisplayMenu();
                        return;
                    default:
                        Console.WriteLine("Некоректный ввод.");
                        continue;
                }
                if (game.IsGameOver()) break;
                game.EnemyTurn();
                SaveGame();
            }

            Console.WriteLine(game.PlayerWon() ? "Ви перемогли!" : "Ви програли!");
            File.Delete(filePath);
            DisplayMenu();
        }

        public static void ShowStatus(Game game)
        {
            Console.WriteLine($"[Ваші ресурси] Золото: {game.Player.Gold}, Армія: {game.Player.Army}");
            Console.WriteLine($"[Ресурси ворога] Золото: {game.Enemy.Gold}, Армія: {game.Enemy.Army}");
        }

        public static void PlayerTurn(Game game)
        {
            Console.WriteLine("Виберіть дію: 1 - Найм солдат (-20 золота), 2 - Атака, 3 - Зміцнення (+2 армія)");
            switch (Console.ReadLine())
            {
                case "1":
                    game.Player.HireSoldiers();
                    Console.WriteLine("Ви найняли 3 солдат!");
                    break;
                case "2":
                    game.Player.Attack(game.Enemy, game.rand);
                    Console.WriteLine("Ви атакували ворога!");
                    break;
                case "3":
                    game.Player.Defend();
                    Console.WriteLine("Ви зміцнилися!");
                    break;
                default:
                    Console.WriteLine("Некоректный ввод.");
                    break;
            }
        }

        public void SaveGame()
        {
            GameState gameState = new GameState(game.Player.Gold, game.Player.Army, game.Enemy.Gold, game.Enemy.Army);
            gameSaver.SaveGame(gameState, filePath);
        }
    }
}
