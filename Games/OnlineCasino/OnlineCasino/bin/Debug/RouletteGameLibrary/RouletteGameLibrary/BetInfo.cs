namespace RouletteGameLibrary
{
    public class BetInfo
    {
        private int betAmount;
        private int chosenNumber;
        private int rolledNumber;
        private int multiplier;
        private int betCategory;

        public int BetAmount => betAmount;
        public int ChosenNumber => chosenNumber;
        public int RolledNumber => rolledNumber;
        public int Multiplier => multiplier;
        public int BetCategory => betCategory;

        public void SetBetAmount(int value) => betAmount = value;
        public void SetChosenNumber(int value) => chosenNumber = value;
        public void SetRolledNumber(int value) => rolledNumber = value;
        public void SetMultiplier(int value) => multiplier = value;
        public void SetBetCategory(int value) => betCategory = value;
    }
}
