using Microsoft.Win32;
using NxWifiScanner.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
            try
            {
                //String fileName;
                //if (Environment.Is64BitOperatingSystem)
                //{
                //    string dir = Path.GetPathRoot(Environment.SystemDirectory);
                //    fileName = dir + "Program Files (x86)\\Netlux\\NetluxInternetSecurity\\tools\\WindowsInput.dll";
                //    LoadLibrary(fileName);
                //}
                //else
                //{
                //    string dir = Path.GetPathRoot(Environment.SystemDirectory);
                //    fileName = dir + "Program Files\\Netlux\\NetluxInternetSecurity\\tools\\WindowsInput.dll";
                //    LoadLibrary(fileName);
                //}
                //  System.Reflection.Assembly.LoadFrom(fileName);
                File.WriteAllBytes(@"arpspoof.exe", Resources.arpspoof);
                File.WriteAllBytes(@"ManagedWifi.dll", Resources.ManagedWifi);
                File.WriteAllBytes(@"WinPcap_4_1_3.exe", Resources.WinPcap_4_1_3);


                string resource1 = "NxWifiScanner.lib.ManagedWifi.dll";

                EmbeddedAssembly.Load(resource1, "ManagedWifi.dll");

                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);


                Mutex appMutex;

                // In some startup/initialization code
                bool createdNew;
                appMutex = new Mutex(true, "NxWifiMutex", out createdNew);
                if (!createdNew)
                {
                  //  MessageBox.Show("Mutex Present");
                    Application.Exit();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //if (IsWindows10())
                    //{
                    if (CheckForInternetConnection())
                        Application.Run(new WifiScanner());
                    else
                        MessageBox.Show("Your Wifi is turned off");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Utility does not supports this OS version");
                    //}
                }
            }catch
            {
                return;
            }
           
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
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
