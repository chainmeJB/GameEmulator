using GamesSaverLibrary;
using RouletteGameLibrary;
using System;
using System.IO;

namespace OnlineCasino
{
    internal class FileManager
    {
        private readonly GameSaver<GameState> gameSaver;
        private readonly GameFacade gameFacade;
        private readonly string filePath = "rouletteGameSave.json";

        public FileManager(GameFacade gameFacade)
        {
            this.gameFacade = gameFacade;
            gameSaver = new GameSaver<GameState>();
        }

        public void LoadGame()
        {
            try
            {
                GameState loadedState = gameSaver.LoadGame(filePath);
                if (loadedState != null)
                {
                    gameFacade.SetGameState(loadedState);
                }
                else
                {
                    gameFacade.StartNewGame();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void SaveGame()
        {
            try
            {
                GameState gameState = gameFacade.GetGameState();
                gameSaver.SaveGame(gameState, filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void DeleteSave()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
