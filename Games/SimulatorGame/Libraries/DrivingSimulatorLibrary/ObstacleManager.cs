using System;
using System.Collections.Generic;

namespace DrivingSimulatorLibrary
{
    public class ObstacleManager
    {
        private readonly List<Obstacle> obstacles = new List<Obstacle>();
        private readonly int roadWidth;
        private readonly int roadHeight;
        private readonly Random random = new Random();

        public ObstacleManager(int roadWidth, int roadHeight)
        {
            this.roadWidth = roadWidth;
            this.roadHeight = roadHeight;
        }

        public void GenerateObstacle()
        {
            if (random.Next(0, 3) == 0)
            {
                obstacles.Add(new Obstacle(random.Next(0, roadWidth - 2), 0));
            }
        }

        public void MoveObstacles()
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.MoveDown();
            }
            obstacles.RemoveAll(o => o.Y >= roadHeight);
        }

        public bool CheckCollision(Car car)
        {
            if (obstacles.Exists(o => o.Y == roadHeight - 1 && Math.Abs(o.X - car.X) < 2))
            {
                return true;
            }
            return false;
        }

        public List<Obstacle> GetObstacles()
        {
            return obstacles;
        }
    }
}