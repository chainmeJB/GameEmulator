using System;
using System.Collections.Generic;
using DrivingSimulatorLibrary;

namespace SimulatorGame
{
    public class GameRenderer
    {
        private readonly int roadHeight = 29;
        private readonly int roadWidth = 60;
        private readonly Game game;

        public GameRenderer(Game game)
        {
            this.game = game;
        }

        public void DrawMap()
        {
            DrawRoad(roadWidth);
            DrawObstacles(game.Obstacles);
            DrawCar(game.CarPosition);
            DrawInfo(game.Score, game.Speed);
            DrawHealth(game.car.Health);
        }

        public void DrawCar(int position)
        {
            Console.SetCursorPosition(30 + position, roadHeight - 1);
            Console.Write("🚗");
        }

        public void DrawObstacles(List<Obstacle> obstacles)
        {
            foreach (var obstacle in obstacles)
            {
                Console.SetCursorPosition(30 + obstacle.X, obstacle.Y);
                Console.Write("⚠");
            }
        }

        public void DrawRoad(int roadWidth)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < roadHeight - 1 ; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string('↟', 30));
                Console.SetCursorPosition(30 + roadWidth, i);
                Console.Write(new string('↟', 30));
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DrawInfo(int score, int speed)
        {
            Console.SetCursorPosition(0, roadHeight);
            Console.Write($"Рахунок: {score} | Швидкість: {speed}ms");

            Console.SetCursorPosition(90, roadHeight);
            Console.Write("A D - керуй. Q - головне меню.");
            Console.CursorVisible = false;
        }

        private void DrawHealth(int health)
        {
            Console.SetCursorPosition(0, roadHeight - 1);
            Console.Write("Здорв'я: ");
            for (int i = 0; i < health; i++)
            {
                Console.Write("❤️ ");
            }
        }
    }
}

