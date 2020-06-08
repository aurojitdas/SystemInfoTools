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

        public String getTotalRAM()
        {
            long Ram_Bytes = 0;
            ManagementObjectSearcher Search = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem ");

            foreach (ManagementObject Mobject in Search.Get())

            {

                Ram_Bytes = ((Convert.ToInt32(Mobject["TotalVisibleMemorySize"]))/1024);

            }

            return Ram_Bytes.ToString();


            
        }

        public float getRamUtilizaton()
        {
            return totalRAMUtil.NextValue();
        }

        public String getMaxCpuFrequency()
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

        public string getRamType(){
           
                int type = 0;

                //ConnectionOptions connection = new ConnectionOptions();
                //connection.Impersonation = ImpersonationLevel.Impersonate;
                //ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
                //scope.Connect();
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    type = Convert.ToInt32(queryObj["MemoryType"]);
                }

                return getRAMTypeString(type);
            
        }


        private static string getRAMTypeString(int type)
        {
            string outValue = string.Empty;


            //got the below values from: chk Memory Type
          //http://msdn.microsoft.com/en-us/library/aa394347(VS.85).aspx

            switch (type)
            {
                case 0x0: outValue = "Unknown"; break;
                case 0x1: outValue = "Other"; break;
                case 0x2: outValue = "DRAM"; break;
                case 0x3: outValue = "Synchronous DRAM"; break;
                case 0x4: outValue = "Cache DRAM"; break;
                case 0x5: outValue = "EDO"; break;
                case 0x6: outValue = "EDRAM"; break;
                case 0x7: outValue = "VRAM"; break;
                case 0x8: outValue = "SRAM"; break;
                case 0x9: outValue = "RAM"; break;
                case 0xa: outValue = "ROM"; break;
                case 0xb: outValue = "Flash"; break;
                case 0xc: outValue = "EEPROM"; break;
                case 0xd: outValue = "FEPROM"; break;
                case 0xe: outValue = "EPROM"; break;
                case 0xf: outValue = "CDRAM"; break;
                case 0x10: outValue = "3DRAM"; break;
                case 0x11: outValue = "SDRAM"; break;
                case 0x12: outValue = "SGRAM"; break;
                case 0x13: outValue = "RDRAM"; break;
                case 0x14: outValue = "DDR"; break;
                case 0x15: outValue = "DDR2"; break;
                case 0x16: outValue = "DDR2 FB-DIMM"; break;
                case 0x17: outValue = "Undefined 23"; break;
                case 0x18: outValue = "DDR3"; break;
                case 0x19: outValue = "FBD2"; break;
                case 0x1a: outValue = "DDR4"; break;
                default: outValue = "Undefined"; break;
            }

            return outValue;
        }






    }
}
