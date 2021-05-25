using Microsoft.Win32;
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
                if (IsWindows10())
                {
                    if (CheckForInternetConnection())
                        Application.Run(new WifiScanner());
                    else
                        MessageBox.Show("Your Wifi is turned off");
                }
                else
                {
                    MessageBox.Show("Utility does not supports this OS version");
                }
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
        static bool IsWindows10()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName.StartsWith("Windows 10");
        }
    }
}
