using DrivingSimulatorLibrary;
using System;
using System.Threading;
using GamesSaverLibrary;
using System.IO;

namespace SimulatorGame
{
    public class ConsoleMenu
    {
        readonly Game game;
        readonly GameRenderer renderer;
        readonly SteeringWheel wheel;
        readonly GameSaver<GameState> gameSaver;

        private static string filePath = "drivingSimSave.json";

        public ConsoleMenu()
        {
            game = new Game();
            renderer = new GameRenderer(game);
            wheel = new SteeringWheel(game);
            gameSaver = new GameSaver<GameState>();
        }

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
                    if (loadedStates != null)
                    {
                        game.SetScore(loadedStates.Score);
                        game.SetSpeed(loadedStates.Speed);
                        game.car.Health = loadedStates.Health;

                        game.Subscribe();
                    }
                    else
                    {
                        Console.WriteLine("Немає даних для завантаження.");
                        DisplayMenu();
                    }
                    PlayGame();
                    break;
                case "2":
                    game.NewGame();
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
            while (!game.isGameOver)
            {
                Console.Clear();

                renderer.DrawMap();

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    wheel.ProcessInput(key);

                    if (key == ConsoleKey.Q) break;
                }

                game.Update();
                if (game.isGameOver)
                {
                    Console.Clear();
                    Console.WriteLine($"Ви програли. Рахунок: {game.Score}");
                    File.Delete(filePath);
                    DisplayMenu();
                }

                Thread.Sleep(game.Speed);

                SaveGame();
            }

            Console.Clear();
            DisplayMenu();
        }

        public void SaveGame()
        {
            GameState gameState = new GameState(game.Score, game.Speed, game.car.Health);
            gameSaver.SaveGame(gameState, filePath);
        }
    }
}
