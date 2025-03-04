using System.Collections.Generic;

namespace DrivingSimulatorLibrary
{
    public class Game
    {
        private const int roadWidth = 60;
        private const int roadHeight = 29;
        public Car car {  get; private set; }
        private ObstacleManager obstacleManager;
        public int Score { get; private set; }
        public int Speed { get; private set; } = 150;

        public void SetScore(int value) => Score = value;
        public void SetSpeed(int value) => Speed = value;

        public Game()
        {
            car = new Car(roadWidth);
            obstacleManager = new ObstacleManager(roadWidth, roadHeight);
        }

        public void Update()
        {
            obstacleManager.GenerateObstacle();
            obstacleManager.MoveObstacles();
            Score++;
            if (Score % 30 == 0 && Speed > 30) Speed -= 10;
        }

        public List<Obstacle> Obstacles => obstacleManager.GetObstacles();

        public int CarPosition => car.X;


        public bool Crash()
        {
            return obstacleManager.CheckCollision(car);
        }
    }
}