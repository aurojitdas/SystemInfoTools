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

        private delegate void cpuCountDelegate(int value);
        private delegate void ramCountDelegate(int value);
        private delegate void cpuDetails();


        private  void updateCpuUtilizatoin(int value)
        {
            form1.progressBar1.Value = value;
            form1.label2.Text = value.ToString()+"%";
            form1.label12.Text = value.ToString() + "%";
            form1.label10.Text = counter.getCpuFrequency()+" Mhz";
            


        }
        private void updateCpuDetails()
        {
            
            form1.label9.Text = counter.getCpuName();
            form1.label11.Text = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
        }

        private void updateRamUtilization(int value)
        {
            form1.progressBar2.Value = value;
            form1.label4.Text = value.ToString()+"%";
        }

        public  void cpuUpdateRoutine()
        {
            
                try
                {
                _ = form1.groupBox1.Invoke(new cpuDetails(updateCpuDetails));
                while (Program.uiThread.IsAlive)
                         {

                            _ = form1.Invoke(new cpuCountDelegate(updateCpuUtilizatoin), (int)counter.getCpuUtilizatoin());
                            Thread.Sleep(500);
                      }

            }
                catch (Exception)
                {
                    Console.WriteLine("Error in cpuUpdateRoutine");
                }
                
           
        }

        



        public void ramUpdateRoutine()
        {

            while (Program.uiThread.IsAlive)
            {
                try
                {
                    _ = form1.Invoke(new cpuCountDelegate(updateRamUtilization), (int)counter.getRamUtilizaton());
                    Thread.Sleep(500);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in ramUpdateRoutine");
                }

            }

        }


    }
}
