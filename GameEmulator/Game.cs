using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameEmulator
{
    public abstract class Game
    {
        public string Name { get; protected set; }
        public string RequiredCPU { get; protected set; }
        public long RequiredRAM { get; protected set; }
        public string RequiredGPU { get; protected set; }
        public long RequiredHDDMemory { get; protected set; }
        public bool IsInstalled { get; private set; }
        public static Game RunningGame { get; private set; } = null;
        

        protected Game(string name, string cpu, long ram, string gpu, long hddMemory)
        {
            Name = name;
            RequiredCPU = cpu;
            RequiredRAM = ram;
            RequiredGPU = gpu;
            RequiredHDDMemory = hddMemory;
            IsInstalled = false;
        }

        public void Install(SystemSpecs specs)
        {
            IsInstalled = true;
            specs.UpdateHDDMemory(RequiredHDDMemory);
        }

        public bool CheckHDDMemory(SystemSpecs specs)
        {
            if (specs.AvailableHDDMemory >= RequiredHDDMemory)
            {
                return true;
            }
            return false;
        }

        public bool CheckRequirments(SystemSpecs specs)
        {
            int userGpuPower = GetGpuPower(specs.GPUPerformance);
            int minGpuPower = GetGpuPower(RequiredGPU);

            int userCpuPower = GetCpuPower(specs.CPUPerformance);
            int minCpuPower = GetCpuPower(RequiredCPU);

            if (userCpuPower >= minCpuPower &&
                specs.RAMSize >= RequiredRAM &&
                userGpuPower >= minGpuPower &&
                specs.IsWindows)
            {
                RunningGame = this;
                return true;
            }
            else if (RequiredHDDMemory == 0 && specs.HasBrowser && specs.HasInternetConnection)
            {
                IsInstalled = true;
                RunningGame = this;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StopGame()
        {
            string[] processNames = { "StrategyGame", "SimulatorGame", "OnlineCasino" };

            foreach (string processName in processNames)
            {
                Process[] processes = Process.GetProcessesByName(processName);

                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            RunningGame = null;
        }

        private int GetGpuPower(string gpu)
        {
            Regex regex = new Regex(@"(\w+)\s*(\w+)\s*(\w*)\s*(\d+)?");
            Match match = regex.Match(gpu);

            if (!match.Success) return 0;

            string brand = match.Groups[1].Value;
            string series = match.Groups[2].Value;
            string model = match.Groups[3].Value;
            int modelNumber = match.Groups[4].Success ? int.Parse(match.Groups[4].Value) : 0;

            int baseScore = 0;

            if (brand.Contains("NVIDIA"))
            {
                if (series.Contains("RTX"))
                {
                    if (modelNumber >= 4000) baseScore += 3000;
                    else if (modelNumber >= 3000) baseScore += 2500;
                    else if (modelNumber >= 2000) baseScore += 2000;
                }
                else if (series.Contains("GTX"))
                {
                    if (modelNumber >= 1600) baseScore += 1500;
                    else if (modelNumber >= 1000) baseScore += 1000;
                    else if (modelNumber >= 900) baseScore += 800;
                    else if (modelNumber >= 700) baseScore += 600;
                    else if (modelNumber >= 600) baseScore += 400;
                    else baseScore += 200;
                }
            }

            else if (brand.Contains("AMD"))
            {
                if (series.Contains("Radeon"))
                {
                    if (model.Contains("RX"))
                    {
                        if (modelNumber >= 7000) baseScore += 3000;
                        else if (modelNumber >= 6000) baseScore += 2500;
                        else if (modelNumber >= 5000) baseScore += 2000;
                        else if (modelNumber >= 500) baseScore += 1500;
                        else if (modelNumber >= 400) baseScore += 1000;
                        else baseScore += 500;
                    }
                    else if (model.Contains("Vega")) baseScore += 1800;
                    else if (model.Contains("R9")) baseScore += 1200;
                    else if (model.Contains("R7")) baseScore += 800;
                    else if (model.Contains("R5")) baseScore += 600;
                }
            }

            return baseScore + modelNumber;
        }

        private int GetCpuPower(string cpu)
        {
            Regex regex = new Regex(@"(\w+)\s*(\w+)\s*(\w+)-?(\d+)");
            Match match = regex.Match(cpu);

            if (!match.Success) return 0;

            string brand = match.Groups[1].Value;
            string series = match.Groups[3].Value;
            int baseScore = 0;

            if (brand.Contains("Intel"))
            {
                if (series.Contains("i9")) baseScore += 4000;
                if (series.Contains("i7")) baseScore += 3000;
                if (series.Contains("i5")) baseScore += 2000;
                if (series.Contains("i3")) baseScore += 1000;
            }

            else if (brand.Contains("AMD"))
            {
                if (series.Contains("Ryzen 9")) baseScore += 4000;
                if (series.Contains("Ryzen 7")) baseScore += 3000;
                if (series.Contains("Ryzen 5")) baseScore += 2000;
                if (series.Contains("Ryzen 3")) baseScore += 1000;
            }

            return baseScore;
        }

        public abstract void StartGame();
    }
}
