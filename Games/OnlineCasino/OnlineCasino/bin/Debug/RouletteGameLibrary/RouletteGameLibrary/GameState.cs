namespace RouletteGameLibrary
{
    public class GameState
    {
        public int Chips {  get; private set; }

        public GameState(int chips)
        {
            Chips = chips;
        }
    }
}
