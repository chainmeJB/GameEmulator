using GamesSaverLibrary;
using RouletteGameLibrary;
using System.IO;
using System;
using Newtonsoft.Json.Linq;

namespace OnlineCasino
{
    public class GameFacade
    {
        private readonly RouletteGame rouletteGame;
        private readonly BetInfo betInfo;
        private readonly GameSaver<GameState> gameSaver;

        public GameFacade()
        {
            rouletteGame = new RouletteGame();
            betInfo = new BetInfo();
            gameSaver = new GameSaver<GameState>();
        }

        public void StartNewGame()
        {
            rouletteGame.SetChips(200);
        }

        public bool PlaceBet()
        {
            return rouletteGame.PlaceBet(betInfo);
        }

        public void SpinRouletteWheel()
        {
            rouletteGame.SpinRouletteWheel(betInfo);
        }

        public bool CheckWin()
        {
            return rouletteGame.Won(betInfo);
        }

        public void EvaluateBet()
        {
            rouletteGame.EvaluateBet(betInfo);
        }

        public int GetChips()
        {
            return rouletteGame.Chips;
        }

        public void SetBetInfo(int value, BetField field)
        {
            switch (field)
            {
                case BetField.BetAmount:
                    betInfo.SetBetAmount(value);
                    break;
                case BetField.ChosenNumber:
                    betInfo.SetChosenNumber(value); 
                    break;
                case BetField.RolledNumber:
                    betInfo.SetRolledNumber(value);
                    break;
                case BetField.BetCategory:
                    betInfo.SetBetCategory(value);
                    break;
            }
        }

        public int GetBetInfo(BetField field)
        {
            switch (field)
            {
                case BetField.BetAmount:
                    return betInfo.BetAmount;
                case BetField.ChosenNumber:
                    return betInfo.ChosenNumber;
                case BetField.RolledNumber:
                    return betInfo.RolledNumber;
                case BetField.BetCategory:
                    return betInfo.BetCategory;
                default:
                    return -1;
            }
        }

        public GameState GetGameState()
        {
            return new GameState(rouletteGame.Chips);
        }

        public void SetGameState(GameState state)
        {
            rouletteGame.SetChips(state.Chips);
        }
    }
}
