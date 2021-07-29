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
         public static void ReceiveSignalFromClient()
        {
            //textBox1.Text = "Successful";
            while (true)
            {
                EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset, "NXISUPDTMODE");
                waitHandle.WaitOne();
                System.Environment.Exit(0);
            }

}
[STAThread]

        static void Main()
        {
            try
            {
                Thread thr = new Thread(ReceiveSignalFromClient);
                thr.Start();
                
                String dllFile,arpspoofPath,WinPcapPath;
                if (Environment.Is64BitOperatingSystem)
                {
                    string rootDir = Path.GetPathRoot(Environment.SystemDirectory);
                    dllFile = rootDir + "Program Files (x86)\\TTB\\TTBInternetSecurity\\Tools\\WindowsInput.dll";
                    arpspoofPath = rootDir + "Program Files (x86)\\TTB\\TTBInternetSecurity\\Tools\\arpspoof.exe";
                    WinPcapPath = rootDir + "Program Files (x86)\\TTB\\TTBInternetSecurity\\Tools\\WinPcap_4_1_3.exe";

                }
                else
                {
                    string rootDir = Path.GetPathRoot(Environment.SystemDirectory);
                    dllFile = rootDir + "Program Files\\TTB\\TTBInternetSecurity\\Tools\\ManagedWifi.dll";
                    arpspoofPath = rootDir + "Program Files\\TTB\\TTBInternetSecurity\\Tools\\arpspoof.exe";
                    WinPcapPath = rootDir + "Program Files\\TTB\\TTBInternetSecurity\\Tools\\WinPcap_4_1_3.exe";

                }
                //  System.Reflection.Assembly.LoadFrom(fileName);

              

                File.WriteAllBytes(arpspoofPath, Resources.arpspoof);
                File.WriteAllBytes(dllFile, Resources.ManagedWifi);
                File.WriteAllBytes(WinPcapPath, Resources.WinPcap_4_1_3);


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
