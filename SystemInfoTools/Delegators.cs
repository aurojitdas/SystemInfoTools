using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SystemInfoTools
{
    class Delegators
    {
        UserInterface form1;
        Counters counter;
        
        public Delegators(UserInterface form1, Counters counter)
        {
            this.form1 = form1;
            this.counter = counter;
        }

        private delegate void cpuUpdateDelegate(int value);
        private delegate void ramUpdateDelegate(int value);
        private delegate void systemDetails();
        

        private  void updateCpuUtilizatoin(int value)
        {
            form1.progressBar1.Value = value;
            form1.label2.Text = value.ToString()+"%";
            form1.label12.Text = value.ToString() + "%";
            form1.label10.Text = counter.getMaxCpuFrequency()+" Mhz";
            


        }
        private void updateSystemDetails()
        {           
            form1.label9.Text = counter.getCpuName();
            form1.label11.Text = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            form1.label16.Text = counter.getTotalRAM()+" MB";
            form1.label20.Text = counter.getRamType();
            form1.label26.Text = counter.getOsName();
            form1.label25.Text = counter.getWindowsType();
            form1.label22.Text = counter.getUpTime();
            form1.label24.Text = counter.getSystemInstallDate();

        }
              

        private void updateRamUtilization(int value)
        {
            form1.progressBar2.Value = value;
            form1.label4.Text = value.ToString()+"%";
            form1.label18.Text = value.ToString() + "%";
            form1.label17.Text = counter.getAvailableRAM() + " MB";
            
        }

        public  void cpuUpdateRoutine()
        {          
                try
                {
               
                while (Program.uiThread.IsAlive)
                         {

                            _ = form1.Invoke(new cpuUpdateDelegate(updateCpuUtilizatoin), (int)counter.getCpuUtilizatoin());
                            Thread.Sleep(500);
                      }
            }
                catch (Exception e)
                {
                    Console.WriteLine("Error in cpuUpdateRoutine");
                    Console.WriteLine(e);
                }                           
        }

        public void systemInfoUpdate()
        {
           
            try
            {
                Thread.Sleep(300);
                _ = form1.groupBox1.Invoke(new systemDetails(updateSystemDetails));
            }
            catch (Exception)
            {

            }

        }
        
        public void ramUpdateRoutine()
        {

            Thread.Sleep(500);
            
            try
                {                      
                  while (Program.uiThread.IsAlive)
                        {
                        _ = form1.Invoke(new ramUpdateDelegate(updateRamUtilization), (int)counter.getRamUtilizaton());
                          Thread.Sleep(500);
                     }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in ramUpdateRoutine");              
                
                }

           

        }


    }
}
