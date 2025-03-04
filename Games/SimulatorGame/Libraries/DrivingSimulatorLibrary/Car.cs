
namespace DrivingSimulatorLibrary
{
    public class Car
    {
        public int X { get; private set; }
        private readonly int roadWidth;

        public Car(int roadWidth)
        {
            this.roadWidth = roadWidth;
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
    }
}
