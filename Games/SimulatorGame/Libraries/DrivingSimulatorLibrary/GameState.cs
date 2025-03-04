
namespace DrivingSimulatorLibrary
{
    public class GameState
    {
        public int Score {  get; private set; }
        public int Speed { get; private set; }

        public GameState(int score, int speed)
        {
            Score = score;
            Speed = speed;
        }
    }
}
