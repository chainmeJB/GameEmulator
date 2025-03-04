using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameEmulator
{
    public class StrategyGame : Game
    {
        public StrategyGame(string name, string cpu, long ram, string gpu, long hddMemory)
            : base(name, cpu, ram, gpu, hddMemory)
        { }

        public override void StartGame()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string gamePath = Path.Combine(baseDirectory, "..", "..", "..", "Games", "Strategy", "StrategyGame", "StrategyGame", "bin", "Debug", "StrategyGame.exe");
            gamePath = Path.GetFullPath(gamePath);
            Process.Start(gamePath);
        }
    }
}
