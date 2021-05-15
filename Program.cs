using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace NxWifiScanner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex appMutex;

            // In some startup/initialization code
            bool createdNew;
            appMutex = new Mutex(true, "NxWifiMutex", out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Mutex Present");
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (CheckForInternetConnection())
                    Application.Run(new WifiScanner());
                else
                    MessageBox.Show("Your Wifi is turned off");
            }
           
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
