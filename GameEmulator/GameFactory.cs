using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEmulator
{
    public class GameFactory
    {
        public static Game CreateGame(GameType gameType, string name, string cpu = "", long ram = 0, string gpu = "", long hddMemory = 0)
        {
            switch (gameType)
            {
                case GameType.Simulator:
                    return new SimulatorGame(name, cpu, ram, gpu, hddMemory);
                case GameType.OnlineCasino:
                    return new OnlineCasinoGame(name);
                case GameType.Strategy:
                    return new StrategyGame(name, cpu, ram, gpu, hddMemory);
                default:
                    throw new ArgumentException("Невідомий тип гри.");
            }
        }
    }
}
