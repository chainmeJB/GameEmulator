using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEmulator
{
    public class OnlineCasinoGame : Game
    {
        public OnlineCasinoGame(string name)
            : base(name, "", 0, "", 0)
        {
        }

        public override void StartGame()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string gamePath = Path.Combine(baseDirectory, "..", "..", "..", "Games", "OnlineCasino", "OnlineCasino", "bin", "Debug", "OnlineCasino.exe");
            gamePath = Path.GetFullPath(gamePath);
            Process.Start(gamePath);
        }
    }
}
