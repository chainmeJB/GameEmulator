
namespace DrivingSimulatorLibrary
{
    public class Obstacle
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Obstacle(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveDown()
        {
            Y++;
        }
    }
}
