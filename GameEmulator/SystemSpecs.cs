using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace GameEmulator
{
    public class SystemSpecs
    {
        public long AvailableHDDMemory { get; private set; }
        public string CPUPerformance { get; private set; }
        public long RAMSize { get; private set; }
        public string GPUPerformance { get; private set; }
        public bool HasInternetConnection { get; private set; }
        public bool HasBrowser { get; private set; }
        public bool IsWindows {  get; private set; }

        public static SystemSpecs GetCurrentSystemSpecs()
        {
            var specs = new SystemSpecs();
            specs.RAMSize = GetRAMSize();
            specs.AvailableHDDMemory = GetAvailableHDDMemory();
            specs.CPUPerformance = GetCPUPerformance();
            specs.GPUPerformance = GetGPUPerformance();
            specs.HasInternetConnection = NetworkInterface.GetIsNetworkAvailable();
            specs.HasBrowser = CheckBrowser();
            specs.IsWindows = IsWindowsOS();
            return specs;
        }

        public void UpdateHDDMemory(long UsedHDDMemory)
        {
            AvailableHDDMemory -= UsedHDDMemory;
        }

        private static long GetRAMSize()
        {
            long totalCapacity = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory")
                                    .Get()
                                    .OfType<ManagementObject>()
                                    .Sum(obj => Convert.ToInt64(obj["Capacity"]));

            long totalGB = totalCapacity / 1024 / 1024 / 1024;
            return totalGB;
        }

        private static string GetGPUPerformance()
        {
            var obj = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController").Get().OfType<ManagementObject>().FirstOrDefault();
            return obj["Name"].ToString();
        }

        private static string GetCPUPerformance()
        {
            var obj = new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get().OfType<ManagementObject>().FirstOrDefault();
            return obj["Name"].ToString();
        }

        private static long GetAvailableHDDMemory()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT FreeSpace FROM Win32_LogicalDisk WHERE DeviceID='C:'"))
            {
                var obj = searcher.Get().OfType<ManagementObject>().FirstOrDefault();
                return obj != null ? Convert.ToInt64(obj["FreeSpace"]) / 1024 / 1024 / 1024 : 0;
            }
        }

        private static bool CheckBrowser()
        {
            string[] paths = {
        @"C:\Program Files\Google\Chrome\Application\chrome.exe",
        @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
        @"C:\Program Files\Microsoft\Edge\Application\msedge.exe",
        @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"
            };

            return paths.Any(path => System.IO.File.Exists(path));
        }

        private static bool IsWindowsOS()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }
    }
}
