using System;
using System.Collections.Generic;
using DrivingSimulatorLibrary;

namespace SimulatorGame
{
    public class GameRenderer
    {
        private const int roadHeight = 29;

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
            for (int i = 0; i < roadHeight; i++)
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
    }
}

