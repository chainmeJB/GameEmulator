using System.Collections.Generic;

namespace DrivingSimulatorLibrary
{
    public class Game
    {
        private readonly int roadWidth = 60;
        private readonly int roadHeight = 29;
        private readonly int health = 3;
        public Car car {  get; private set; }
        private ObstacleManager obstacleManager;
        public int Score { get; private set; } = 0;
        public int Speed { get; private set; } = 150;
        public bool isGameOver { get; private set; }

        public void SetScore(int value) => Score = value;
        public void SetSpeed(int value) => Speed = value;

        public Game()
        {
            car = new Car(roadWidth, health);
            obstacleManager = new ObstacleManager(roadWidth, roadHeight);
        }

        public void Update()
        {
            obstacleManager.GenerateObstacle();
            obstacleManager.MoveObstacles();
            UpdateCarHealth();
            Score++;
            if (Score % 30 == 0 && Speed > 30) Speed -= 10;
        }

        public List<Obstacle> Obstacles => obstacleManager.GetObstacles();

        public int CarPosition => car.X;

        public void NewGame()
        {
            Unsubscribe();
            isGameOver = false;
            Score = 0;
            Speed = 150;
            car = new Car(roadWidth, health);
            obstacleManager = new ObstacleManager(roadWidth, roadHeight);
            Subscribe();
        }

        private void UpdateCarHealth()
        {
            if (obstacleManager.CheckCollision(car))
            {
                car.LoseHealth();
            }
        }

        private void HealthChanged(int newHealth)
        {
            if (newHealth <= 0)
            {
                isGameOver = true;
            }
        }

        public void Subscribe()
        {
            car.HealthChanged += HealthChanged;
        }

        private void Unsubscribe()
        {
            car.HealthChanged -= HealthChanged;
        }
    }
}