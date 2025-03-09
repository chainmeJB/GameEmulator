
using System;
using System.Collections.Generic;

namespace DrivingSimulatorLibrary
{
    public class Car
    {
        public int X { get; private set; }
        private readonly int roadWidth;
        private int health;
        public event Action<int> HealthChanged;

        public int Health 
        {
            get => health;
            set
            {
                if (value >= 0 && health != value)
                {
                    health = value;

                    HealthChanged?.Invoke(health);
                }
            }
        }

        public Car(int roadWidth, int health)
        {
            this.roadWidth = roadWidth;
            this.health = health;
            X = roadWidth / 2;
        }

        public void MoveLeft()
        {
            if (X > 1) X -= 2;
        }

        public void MoveRight()
        {
            if (X < roadWidth - 2) X += 2;
        }

        public void LoseHealth()
        {
            if (Health > 0)
            {
                Health--;
            }
        }
    }
}
