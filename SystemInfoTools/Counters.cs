using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
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
               
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    type = Convert.ToInt32(queryObj["MemoryType"]);
                }          
                    return getRAMTypeString(type);
            
        }

        public String getSystemInstallDate()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                                                  "SELECT * FROM Win32_OperatingSystem");
            String date = null;
            foreach (ManagementObject queryObj in searcher.Get())
            {
                DateTime dt = ManagementDateTimeConverter.ToDateTime(queryObj["InstallDate"].ToString());
                date = dt.ToString();
            }

            return date ;
        }      

       


        public Dictionary<String,String> getNeworkAdapterDetails()
        {
            Dictionary<String, String> network = new Dictionary<String, String>();
            try
            {
                Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "netsh.exe";
                p.StartInfo.Arguments = "wlan show interfaces";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                string output = p.StandardOutput.ReadToEnd();
                string ssid = output.Substring(output.IndexOf("SSID"));
                ssid = ssid.Substring(ssid.IndexOf(":"));
                ssid = ssid.Substring(2, ssid.IndexOf("\n")).Trim();
                network.Add("SSID",ssid);

                String signalStrength = output.Substring(output.IndexOf("Signal"));
                signalStrength = signalStrength.Substring(signalStrength.IndexOf(":"));
                signalStrength = signalStrength.Substring(2, signalStrength.IndexOf("\n")).Trim();
                network.Add("SignalStrength", signalStrength);

                String status = output.Substring(output.IndexOf("State"));
                status = status.Substring(status.IndexOf(":"));
                status = status.Substring(2, status.IndexOf("\n")).Trim();
                network.Add("Status", status);

                String name = output.Substring(output.IndexOf("Name"));
                name = name.Substring(name.IndexOf(":"));
                name = name.Substring(2, name.IndexOf("\n")).Trim();
                network.Add("Name", name);

                String desc = output.Substring(output.IndexOf("Description"));
                desc = desc.Substring(desc.IndexOf(":"));
                desc = desc.Substring(2, desc.IndexOf("\n")).Trim();
                network.Add("Description", desc);

                String Speed = output.Substring(output.IndexOf("Receive rate (Mbps)"));
                Speed = Speed.Substring(Speed.IndexOf(":"));
                Speed = Speed.Substring(2, Speed.IndexOf("\n")).Trim();
                network.Add("Speed", Speed);






                return network;
            }
            catch (Exception e)
            {

               // Dictionary<String, String> network = new Dictionary<String, String>();
                network.Add("SSID", "Not Connected");
                network.Add("SignalStrength", "Not Connected");
                network.Add("Status", "Not Connected");
                network.Add("Name", "Not Connected");
                return network ;
                
            }
            finally
            {
                
            }

           
        }


        public String getOsName()
        {
            OperatingSystem os_info = System.Environment.OSVersion;
            //This code will only return correct win 10 info if app.manifest is present with win10 comp.
            return "Windows " + GetOsName(os_info); 
        }


        public String getWindowsType()
        {
            String type = null;

            if (Environment.Is64BitOperatingSystem)
            {
                type = "64-bit ";

            }
            else
            {
                type = "32-bit";
            }



            return type;
        }

        public String getUpTime()
        {
            var ticks = Stopwatch.GetTimestamp();
            var uptime = ((double)ticks) / Stopwatch.Frequency;
            var uptimeSpan = TimeSpan.FromSeconds(uptime);

            return uptimeSpan.ToString().Remove(10);
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

        private string GetOsName(OperatingSystem os_info)
        {
            string version =
                os_info.Version.Major.ToString() + "." +
                os_info.Version.Minor.ToString();
            switch (version)
            {
                case "10.0": return "10";
                case "6.3": return "8.1";
                case "6.2": return "8";
                case "6.1": return "7";
                case "6.0": return "Vista";
                case "5.2": return "Server 2003 R2/Server 2003/XP 64-Bit Edition";
                case "5.1": return "XP";
                case "5.0": return "2000";
            }
            return "Unknown";
        }






    }
}
