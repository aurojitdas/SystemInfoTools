using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoTools
{
    public class Counters
    {

        PerformanceCounter cpuTotal;
        PerformanceCounter cpuFrequency;
        PerformanceCounter availableRAM;
        PerformanceCounter totalRAMUtil;
        ManagementObjectSearcher Searcher;




        public Counters()
        {
            this.cpuTotal = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            this.cpuFrequency = new PerformanceCounter("Processor Information", "Processor Frequency", "_Total");
            this.availableRAM = new PerformanceCounter("Memory", "Available MBytes");
            this.totalRAMUtil = new PerformanceCounter("Memory", "% Committed Bytes In Use");

        }

        public float getCpuUtilizatoin()
        {           
            return cpuTotal.NextValue();
        }

        public float getAvailableRAM()
        {           
            return availableRAM.NextValue();

        }

        public float getRamUtilizaton()
        {
            return totalRAMUtil.NextValue();
        }

        public String getCpuFrequency()
        {
            String freq = null;
            Searcher = new ManagementObjectSearcher("select MaxClockSpeed from Win32_Processor");
            foreach (ManagementObject mo in Searcher.Get())
            {
                freq = mo["MaxClockSpeed"].ToString();
            }
            return freq;
        }

        public String getCpuName()
        {
            String name = null;
             Searcher= new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject mo in Searcher.Get())
            {
                name =  mo["Name"].ToString();
            }
            return name;
        }






    }
}
