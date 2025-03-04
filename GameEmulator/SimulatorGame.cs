using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEmulator
{
    public class SimulatorGame : Game
    {
        public SimulatorGame(string name, string cpu, long ram, string gpu, long hddMemory)
            : base(name, cpu, ram, gpu, hddMemory)
        {
        }

        public override void StartGame()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string gamePath = Path.Combine(baseDirectory, "..", "..", "..", "Games", "SimulatorGame", "SimulatorGame", "bin", "Debug", "SimulatorGame.exe");
            gamePath = Path.GetFullPath(gamePath);
            Process.Start(gamePath);
        }
    }
}
