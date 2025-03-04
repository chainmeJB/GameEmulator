using System;

namespace RouletteGameLibrary
{
    public class RouletteGame
    {
        private readonly Random random = new Random();
        public int Chips { get; private set; }

        public void SetChips(int value) => Chips = value;

        public RouletteGame() 
        {
            Chips = 200;
        }

        public void SpinRouletteWheel(BetInfo betInfo)
        {
            betInfo.SetRolledNumber(random.Next(0, 37));
        }

        public bool PlaceBet(BetInfo betInfo)
        {
            if (betInfo.BetAmount > Chips)
            {
                return false;
            }

            Chips -= betInfo.BetAmount;
            return true;
        }

        public bool Won(BetInfo betInfo)
        {
            switch (betInfo.BetCategory)
            {
                case 1:
                    if (betInfo.RolledNumber == betInfo.ChosenNumber)
                    {
                        betInfo.SetMultiplier(36);
                        return true;
                    }
                    break;
                case 2:
                    if (IsSameColor(betInfo.RolledNumber, betInfo.ChosenNumber))
                    {
                        betInfo.SetMultiplier(2);
                        return true;
                    }
                    break;
                case 3:
                    if (IsSameThird(betInfo.RolledNumber, betInfo.ChosenNumber))
                    {
                        betInfo.SetMultiplier(3);
                        return true;
                    }
                    break;
                default:
                    break;
            }

            betInfo.SetMultiplier(0);
            return false;
        }

        public void EvaluateBet(BetInfo betInfo)
        {
            if (Won(betInfo))
            {
                Chips += betInfo.BetAmount * betInfo.Multiplier;
            }
        }

        private static bool IsSameColor(int num1, int num2)
        {
            int[] redNumbers = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            bool isRed1 = Array.Exists(redNumbers, n => n == num1);
            bool isRed2 = Array.Exists(redNumbers, n => n == num2);
            return isRed1 == isRed2;
        }

        private static bool IsSameThird(int rolled, int chosen)
        {
            return (rolled - 1) / 12 == (chosen - 1) / 12;
        }
    }
}
