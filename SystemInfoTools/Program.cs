using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Management;

namespace SystemInfoTools
{
    class Program
    {
        static Counters counter;
        static UserInterface form1;
        public static Thread uiThread;
        static Thread cpuUpdaterThread;
        static Thread ramUpdaterThread;
        static Thread updateSystemInfo;


        static void Main(string[] args)
        {

            counter = new Counters();
            form1 = new UserInterface();
            Delegators deleg = new Delegators(form1, counter);
            Application.EnableVisualStyles();

            uiThread = new Thread(initializeUI);
            cpuUpdaterThread = new Thread(deleg.cpuUpdateRoutine);
            ramUpdaterThread = new Thread(deleg.ramUpdateRoutine);
            updateSystemInfo = new Thread(deleg.systemInfoUpdate);
            uiThread.Start();
            updateSystemInfo.Start();            
            cpuUpdaterThread.Start();
            ramUpdaterThread.Start();
        }


        public static void initializeUI()
        {
            Application.Run(form1);
            
            
        }

        


    }
}
