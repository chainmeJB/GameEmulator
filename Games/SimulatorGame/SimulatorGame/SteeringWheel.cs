using System;
using DrivingSimulatorLibrary;

namespace SimulatorGame
{
    public class SteeringWheel
    {
        private readonly Game game;

        public SteeringWheel(Game game)
        {
            this.game = game;
        }
        public void ProcessInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.A:
                    game.car.MoveLeft();
                    break;
                case ConsoleKey.D:
                    game.car.MoveRight();
                    break;
            }
        }
    }
}
