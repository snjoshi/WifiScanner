using Microsoft.Win32;
using NativeWifi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


//using ManagedWifi;

namespace NxWifiScanner
{
    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }
    public partial class WifiScanner : Form
    {
         int wifiCount = 0;
         int Wifi_count_names = 0;
       // static ListBox listBox_AvailableWifi = new ListBox();

        private void CreateDebugFile(string input)
        {

            string fileName = @"WifiScannerError.txt";
            FileInfo fi = new FileInfo(fileName);

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (!fi.Exists)
                {
                 //   fi.Delete();             
                // Create a new file     
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                        sw.WriteLine(input);
                       
                    }

                   // fi.Close();
                }
                else
                {
                    using (StreamWriter sw = fi.AppendText())
                    {
                        sw.WriteLine("File appended on: {0}", DateTime.Now.ToString());
                        sw.WriteLine(input);
                        
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
        private string GetWifiNetworks()
        {
            //execute the netsh command using process class
            Process processWifi = new Process();
            processWifi.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processWifi.StartInfo.FileName = "netsh";
            processWifi.StartInfo.Arguments = "wlan show profile";

            processWifi.StartInfo.UseShellExecute = false;
            processWifi.StartInfo.RedirectStandardError = true;
            processWifi.StartInfo.RedirectStandardInput = true;
            processWifi.StartInfo.RedirectStandardOutput = true;
            processWifi.StartInfo.CreateNoWindow = true;
            processWifi.Start();

            string output = processWifi.StandardOutput.ReadToEnd();

            processWifi.WaitForExit();
            return output;
        }
        private string ReadPassword(string Wifi_Name)
        {

            string argument = "wlan show profile name=\"" + Wifi_Name + "\" key=clear";
            Process processWifi = new Process();
            processWifi.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processWifi.StartInfo.FileName = "netsh";
            processWifi.StartInfo.Arguments = argument;


            processWifi.StartInfo.UseShellExecute = false;
            processWifi.StartInfo.RedirectStandardError = true;
            processWifi.StartInfo.RedirectStandardInput = true;
            processWifi.StartInfo.RedirectStandardOutput = true;
            processWifi.StartInfo.CreateNoWindow = true;
            processWifi.Start();

            string output = processWifi.StandardOutput.ReadToEnd();


            processWifi.WaitForExit();
            return output;
        }
        private string GetWifiPassword(string Wifi_Name)
        {
            try
            {
                string get_password = ReadPassword(Wifi_Name);
                using (StringReader reader = new StringReader(get_password))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Regex regex2 = new Regex(@"Key Content * : (?<after>.*)");
                        Match match2 = regex2.Match(line);

                        if (match2.Success)
                        {
                            string current_password = match2.Groups["after"].Value;
                            //  Console.WriteLine(current_password);
                            return current_password;
                        }
                    }
                }
                return "NO PASSWORD";
            }
            catch(Exception ex)
            {
                CreateDebugFile("#Error  : Exception while retrieveing Wifi Password "+ ex.Message);
                return null;
            }
        }
        private  bool get_Wifi_passwords()
        {
            try
            {
                SetLoading(true);

                List<string> WifiPasswords = new List<string>();
                string WifiNetworks = GetWifiNetworks();

                if (WifiNetworks != null)
                    CreateDebugFile("#Sucess  : Wifi Network List not empty");
                else
                    CreateDebugFile("#Error  : Wifi Network List empty");

                using (StringReader reader = new StringReader(WifiNetworks))
                {

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        wifiCount++;
                        Regex regex1 = new Regex(@"All User Profile * : (?<after>.*)");
                        Match match1 = regex1.Match(line);

                        if (match1.Success)
                        {
                            Wifi_count_names++;
                            string Wifi_name = match1.Groups["after"].Value;
                            string Wifi_password = GetWifiPassword(Wifi_name);
                            if(Wifi_password == null)
                            {
                                CreateDebugFile("#Error  : Error in retreiving Wifi password Function");
                            }
                            else
                            {
                                CreateDebugFile("#Success  : Wifi Password derived successfully");
                            }


                            
                            string Pwd_Strength = CheckingPasswordStrength(Wifi_password).ToString();
                            listView_wifiPasswords.Items.Add(new ListViewItem(new string[] { Wifi_name, Wifi_password, Pwd_Strength }));
                            //  DisplayWifiPasswords(Wifi_name, Wifi_password, Pwd_Strength);

                            Console.WriteLine(Wifi_name + " " + Wifi_password + " " + CheckingPasswordStrength(Wifi_password));
                        }
                    }
                }
                SetLoading(false);
                return true;

            }catch(Exception ex)
            {
                CreateDebugFile("#Error  : Exception thrown by get wifi password "+ ex.Message);
                return false;
            }


        }
        public void DisplayWifiPasswords(string ssid, string pass , string strength)
        {
           
        }
        private static List<string> GetAvailableWifi()

        {
            try
            {

                List<string> networkList = new List<string>();

                WlanClient client = new WlanClient();

                foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)

                {

                    // Lists all networks with WEP security

                    Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);

                    foreach (Wlan.WlanAvailableNetwork network in networks)

                    {

                        Wlan.Dot11Ssid ssid = network.dot11Ssid;

                        string networkname = Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
                        string strength = network.wlanSignalQuality.ToString();
                        //string SSID_label = "SSID = ";
                        

                        if (networkname != "")

                        {

                            networkList.Add(networkname.ToString() + "     Strength= "+  strength + "/100" );

                        }

                    }

                }


                if (networkList.Count > 0)

                {
                    Console.WriteLine("Listing the available wifi");
                    Console.WriteLine("--------------------------");
                    foreach (var item in networkList)

                    {
                        // listBox_AvailableWifi.Items.Add(item);
                        Console.WriteLine(item.ToString());
                    }
                }
                return networkList;
            }catch(Exception ex)
            {
                WifiScanner ws = new WifiScanner();
                ws.CreateDebugFile("#Error  :Exception while getting available wifi "+ ex.Message);
                return null;
            }
        }
        public static void GetWifiSsid()
        {
            WlanClient wlan = new WlanClient();
            List<String> connectedSsids = new List<string>();

            foreach (WlanClient.WlanInterface wlanInterface in wlan.Interfaces)
            {
                Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                connectedSsids.Add(new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength)));
            }

            foreach (var item in connectedSsids)

            {

                Console.WriteLine(item.ToString());

            }
        }
        private static PasswordScore CheckingPasswordStrength(string password)
        {
            int score = 1;
            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.IsMatch(password, @"[0-9]+(\.[0-9][0-9]?)?", RegexOptions.ECMAScript))
                score++;
            if (Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z]).+$", RegexOptions.ECMAScript))
                score++;
            if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript))
                score++;
            return (PasswordScore)score;
        }
        public WifiScanner()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            //{
            //    string resourceName = new AssemblyName(args.Name).Name + ".dll";
            //    string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

            //    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            //    {
            //        Byte[] assemblyData = new Byte[stream.Length];
            //        stream.Read(assemblyData, 0, assemblyData.Length);
            //        return Assembly.Load(assemblyData);
            //    }
            //};
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //  foo();
        }
        private void foo()
        {
            
        }

        public static bool IsSoftwareInstalled1(string softwareName)
        {
            var registryUninstallPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            var registryUninstallPathFor32BitOn64Bit = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

            if (Is32BitWindows())
                return IsSoftwareInstalled(softwareName, RegistryView.Registry32, registryUninstallPath);

            var is64BitSoftwareInstalled = IsSoftwareInstalled(softwareName, RegistryView.Registry64, registryUninstallPath);
            var is32BitSoftwareInstalled = IsSoftwareInstalled(softwareName, RegistryView.Registry64, registryUninstallPathFor32BitOn64Bit);
            return is64BitSoftwareInstalled || is32BitSoftwareInstalled;
        }

        private static bool Is32BitWindows() => Environment.Is64BitOperatingSystem == false;

        private static bool IsSoftwareInstalled(string softwareName, RegistryView registryView, string installedProgrammsPath)
        {
            var uninstallKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView)
                                                  .OpenSubKey(installedProgrammsPath);

            if (uninstallKey == null)
                return false;

            return uninstallKey.GetSubKeyNames()
                               .Select(installedSoftwareString => uninstallKey.OpenSubKey(installedSoftwareString))
                               .Select(installedSoftwareKey => installedSoftwareKey.GetValue("DisplayName") as string)
                               .Any(installedSoftwareName => installedSoftwareName != null && installedSoftwareName.Contains(softwareName));
        }
        //private bool IsSoftwareInstalled(string c_name)
        //{
        //    //var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall") ??
        //    //          Registry.LocalMachine.OpenSubKey(
        //    //              @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

        //    //if (key == null)
        //    //    return false;

        //    //return key.GetSubKeyNames()
        //    //    .Select(keyName => key.OpenSubKey(keyName))
        //    //    .Select(subkey => subkey.GetValue("DisplayName") as string)
        //    //    .Any(displayName => displayName != null && displayName.Contains(softwareName));
           
        //}

        private void Download_Click()
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("WinPcap_4_1_3.exe");
            Process.Start(sInfo);
            //using (WebClient wc = new WebClient())
            //{
            //  //  wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            //    wc.DownloadFileAsync(new System.Uri("https://www.winpcap.org/install/bin/WinPcap_4_1_3.exe"),
            //     "Result location");
            //}
        }
       
        private void WifiScanner_Load(object sender, EventArgs e)
        {
            try
            {
              


                CreateDebugFile("#Success : File created / appended");
                setButtonState();

                pictureBox1.Visible = false;

                pictureBox_AvailableWifi.Show();
                label_AvailableWifi.Show();
                listBox_AvailableWifi.Show();
                button_blockIP.Hide();


                
                if(lstLocal!=null)
                {
                    lstLocal.Hide();
                }
                if (label_wifiAdmin != null)
                {
                    label_wifiAdmin.Hide();
                }
                if (button_Back != null)
                {
                    button_Back.Hide();
                }
                if (label_SavedWifi != null)
                {
                    label_SavedWifi.Hide();
                }
                if (listView_wifiPasswords != null)
                {
                    listView_wifiPasswords.Hide();
                }
                if (pictureBox_SavedWifi != null)
                {
                    pictureBox_SavedWifi.Hide();
                }
                if (label_wifiState != null)
                {
                    label_wifiState.Hide();
                  
                }
                if (button_wifiState != null)
                {
                    button_wifiState.Hide();
                }




            //new Thread(() =>
            //{

                List<string> Available_Wifi = new List<string>();
                Available_Wifi = GetAvailableWifi();
                if (Available_Wifi == null)
                {
                    CreateDebugFile("#Success  :No Wifi Available");
                }
                else
                    CreateDebugFile("#Success : successfully got available wifi ");

                    //  listBox_AvailableWifi.Location = new Point(287, 109);
                    //  listBox_AvailableWifi.Size = new Size(120, 95);
                    // listBox_AvailableWifi.fontSize=
                    listBox_AvailableWifi.ForeColor = Color.Purple;

                if (Available_Wifi.Count > 0)
                {
                    CreateDebugFile("#Success  : Wifi Available");

                        //Console.WriteLine("Listing the available wifi");
                        //Console.WriteLine("--------------------------");
                        foreach (var item in Available_Wifi.Skip(1))
                    {
                        listBox_AvailableWifi.Items.Add(item);
                            //Console.WriteLine(item.ToString());
                        }
                }
                else
                {
                listBox_AvailableWifi.ForeColor = Color.Red;
                listBox_AvailableWifi.Items.Add("Check your WiFi connection. No WiFi Available!");
                CreateDebugFile("#Error  : No Wifi Available (list empty)");
                }
            //   }).Start();
        }catch(NullReferenceException)
            {
                CreateDebugFile("#Error  : Null reference exception");
                return;
            }


    // Adding ListBox control
    // to the form
    //  this.Controls.Add(listBox_AvailableWifi);
}

        private void listBox_AvailableWifi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_WifiDetails_Click(object sender, EventArgs e)
        {

            label_SavedWifi.Show();
            label_AvailableWifi.Hide();
            lstLocal.Hide();
            listBox_AvailableWifi.Hide();
            button_blockIP.Hide();
            button_AdminConsole.Enabled = true;

            InitializeListView();
        }
        public void InitializeListView()
        {
            new Thread(() =>
            {

                listView_wifiPasswords.View = View.Details;
                listView_wifiPasswords.Columns.Add("Wifi SSid", 239, HorizontalAlignment.Left);
                listView_wifiPasswords.Columns.Add("Password", 239, HorizontalAlignment.Left);
                listView_wifiPasswords.Columns.Add("Password Strength", 239, HorizontalAlignment.Left);
                listView_wifiPasswords.Items.Clear();



                label_SavedWifi.Show();
                label_AvailableWifi.Hide();
                pictureBox_SavedWifi.Show();
                pictureBox_AvailableWifi.Hide();
                label_wifiState.Hide();
                button_wifiState.Hide();

                label_wifiAdmin.Hide();


                listView_wifiPasswords.Show();
                button_Back.Show();

            }).Start();

            new Thread(() =>
            {
                if(get_Wifi_passwords())
                    CreateDebugFile("#Success  : Get wifi password successful");
                else
                    CreateDebugFile("#Error  : Get wifi password returned error");

            }).Start();

            //listView_wifiPasswords.Items.Add(new ListViewItem(new string[] { "1", "content","4" }));
            //listView_wifiPasswords.Items.Add(new ListViewItem(new string[] { "4", "content2","5" }));
            //listView_wifiPasswords.Items.Add(new ListViewItem(new string[] { "2", "content3","6" }));
        }
        private void listView_wifiPasswords_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            label_SavedWifi.Hide();
            pictureBox_AvailableWifi.Show();
            pictureBox_SavedWifi.Hide();
            label_AvailableWifi.Show();
            listView_wifiPasswords.Hide();
            button_Back.Hide();
            listBox_AvailableWifi.Show();
            lstLocal.Hide();
            label_wifiState.Hide();
            button_wifiState.Hide();
            label_AvailableWifi.Show();
            label_wifiAdmin.Hide();
            button_blockIP.Hide();

            button_AdminConsole.Enabled = true;
        }
        static string NetworkGateway()
        {
            try
            {
                string ip = null;

                foreach (NetworkInterface f in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (f.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (GatewayIPAddressInformation d in f.GetIPProperties().GatewayAddresses)
                        {
                            ip = d.Address.ToString();
                        }
                    }
                }

                return ip;
            }catch(Exception ex)
            {
                WifiScanner ws = new WifiScanner();
                ws.CreateDebugFile("#Error  : Exception while getting network gateway address" + ex.Message);
                return null;
            }
        }

        public bool Ping_all()
        {
            try
            {
                SetLoading(true);
                string gate_ip = NetworkGateway();
                if(gate_ip!=null)
                    CreateDebugFile("#Success  : gate ip received successfully");
                else
                    CreateDebugFile("#Error  : gateway address empty(null)");
                //   MessageBox.Show(gate_ip);

                //Extracting and pinging all other ip's.
                string[] array = gate_ip.Split('.');

                for (int i = 2; i <= 255; i++)
                {

                    string ping_var = array[0] + "." + array[1] + "." + array[2] + "." + i;

                    //time in milliseconds           
                    Ping(ping_var, 1, 4000);

                }

                SetLoading(false);
                return true;

            }catch(Exception ex)
            {
                CreateDebugFile("#Error  : Exception in Pinging all Ip" + ex.Message);
                return false;
            }

        }

        public void Ping(string host, int attempts, int timeout)
        {
        //    CreateDebugFile("#DEBUG  :inside ping");
            SetLoading(true);
            for (int i = 0; i < attempts; i++)
            {
                new Thread(delegate ()
               {
                  //  SetLoading(true);
                    try
                    {
                        System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                        ping.PingCompleted += new PingCompletedEventHandler(PingCompleted);

                        ping.SendAsync(host, timeout, host);
                    }
                    catch(Exception ex)
                    {
                 //      CreateDebugFile(ex.Message);
                       // Do nothing and let it try again until the attempts are exausted.
                       // Exceptions are thrown for normal ping failurs like address lookup
                       // failed.  For this reason we are supressing errors.
                   }
                    
                }).Start();
            }
           // SetLoading(false);
        }
        private void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            CreateDebugFile("#DEBUG  :inside ping completed");
            string ip = (string)e.UserState;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
            //    CreateDebugFile("#DEBUG  :inside ping completed IF");
                SetLoading(true);
                
                string hostname = GetHostName(ip);
             //   CreateDebugFile("Host name" + hostname);
                
                string macaddres = GetMacAddress(ip);
             //   CreateDebugFile("GetMacAddress" + macaddres);

                string[] arr = new string[4];

                //store all three parameters to be shown on ListView
                arr[0] = ip;
                arr[1] = hostname;
                arr[2] = macaddres;
                arr[3]="Unblocked";

                // Logic for Ping Reply Success
                ListViewItem item;
                if (this.InvokeRequired)
                {

                    this.Invoke(new Action(() =>
                    {

                        item = new ListViewItem(arr);

                    //    CreateDebugFile("Ping completed:"+ item.ToString());

                        lstLocal.Items.Add(item);
                       // Console.log(item);


                    }));
                }

                SetLoading(false);
            }
            else
            {
                CreateDebugFile("#DEBUG  :inside ping completed ELSE");
                // MessageBox.Show(e.Reply.Status.ToString());
            }
            
        }
        public string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipAddress);
                if (entry != null)
                {
                    return entry.HostName;
                }
            }
            catch (SocketException)
            {
                // MessageBox.Show(e.Message.ToString());
            }

            return null;
        }


        //Get MAC address
        public string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            System.Diagnostics.Process Process = new System.Diagnostics.Process();
            Process.StartInfo.FileName = "arp";
            Process.StartInfo.Arguments = "-a " + ipAddress;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.CreateNoWindow = true;
            Process.Start();
            string strOutput = Process.StandardOutput.ReadToEnd();
            string[] substrings = strOutput.Split('-');
            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                         + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                         + "-" + substrings[7] + "-"
                         + substrings[8].Substring(0, 2);
                return macAddress;
            }

            else
            {
                return "OWN Machine";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private bool load_Ip_Addresses()
        {
            
            try
            {
                button_AdminConsole.Enabled = false;
                label_AvailableWifi.Show();
                lstLocal.View = View.Details;
                lstLocal.Clear();
                lstLocal.GridLines = true;
                lstLocal.FullRowSelect = true;
                lstLocal.BackColor = System.Drawing.Color.Aquamarine;
                lstLocal.Columns.Add("IP", 120);
                lstLocal.Columns.Add("HostName", 250);
                lstLocal.Columns.Add("MAC Address", 150);
                lstLocal.Columns.Add("Status", 150);
                lstLocal.Sorting = SortOrder.Descending;


                lstLocal.Show();
                pictureBox_AvailableWifi.Hide();
                pictureBox_SavedWifi.Hide();
                listView_wifiPasswords.Hide();
                button_Back.Show();
                label_SavedWifi.Hide();
                label_wifiState.Show();
                button_wifiState.Show();
             //   pictureBox1.Show();

                label_AvailableWifi.Hide();
                listBox_AvailableWifi.Hide();
                label_wifiAdmin.Show();
                button_blockIP.Show();


                new Thread(() =>
                {
                    if(Ping_all())
                        CreateDebugFile("#Success  : Pinging all Ip successful");
                    else
                        CreateDebugFile("#Error  : Error in  Pinging all Ip");
                    //Automate pending
                }).Start();

                return true;
               
            }
            catch (Exception ex)
            {
                CreateDebugFile("#Error  : Error in loading Ip addresses" + ex.Message);
                return false;
            }
        }
        private void button_AdminConsole_Click(object sender, EventArgs e)
        {
            if (IsSoftwareInstalled1("WinPcap"))
            {
             //   MessageBox.Show("installed");
                if (load_Ip_Addresses())
                    CreateDebugFile("#Success  : Ip addresses loaded successfully");
                else
                    CreateDebugFile("#Error  : Error in loading ip addresses");

            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("You need to install a software to access admin console. Would you like to proceed?", "Installation Required", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //  MessageBox.Show("not installed");
                    if (IsInternetAvailable())
                    {
                        Download_Click();
                    }
                    else
                    {
                        MessageBox.Show("Check your internet connection", "TTB", MessageBoxButtons.OK);
                        return;
                    }
                    
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                   
                }
               
            }

            


        }


        //Turn Wifi Connection OFF/ON
        void InternetConnection(string str)
        {
            ProcessStartInfo internet = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = "/C ipconfig /" + str,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(internet);
        }
        public void setButtonState()
        {
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(1, 1, button_wifiState.Width - 4, button_wifiState.Height - 4);
            button_wifiState.Region = new Region(p);

            Color green = ColorTranslator.FromHtml("#328a3e");
            button_WifiDetails.BackColor = green;
            button_Back.BackColor = green;
            button_AdminConsole.BackColor = green;
            button_blockIP.BackColor = green;
            bool result = IsInternetAvailable();
            if(result==true)
            {
               
                button_wifiState.Text = "ON";
                button_wifiState.BackColor = Color.Green;

            }
            else
            {

                button_wifiState.Text = "OFF";
                button_wifiState.BackColor = Color.Red;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (button_wifiState.Text == "ON")
            {
                InternetConnection("release");
                button_wifiState.BackColor = Color.Red; //symbolizes light turned on

                button_wifiState.Text = "OFF";
            }

            else if (button_wifiState.Text == "OFF")
            {
                InternetConnection("renew");
                button_wifiState.BackColor = Color.Green; //symbolizes light turned off

                button_wifiState.Text = "ON";
            }
        }
        public bool IsInternetAvailable()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                CreateDebugFile("#Success  :Internet connection checked successfully");
                return (reply.Status == IPStatus.Success);
               
            }
            catch (Exception)
            {
              //  CreateDebugFile("#Error  :Internet connection checked successfully");
                return false;
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lstLocal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
         //   string selectedItem = lstLocal.Items[lstLocal.SelectedIndexChanged].ToString();
        }
       
        private void lstLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //string gate_ip = NetworkGateway();
            //if (gate_ip != null)
            //    CreateDebugFile("#Success  : gate ip received successfully");
            //else
            //    CreateDebugFile("#Error  : gateway address empty(null)");

            //if (lstLocal.SelectedItems.Count > 0)
            //{
            //    DialogResult dialogResult = MessageBox.Show("Do you want to block this IP:" + (lstLocal.SelectedItems[0]).Text + "?", "Firewall", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {

            //        ListViewItem item = lstLocal.SelectedItems[0];
            //        //   MessageBox.Show(item.SubItems[0].Text);

            //        try
            //        {
            //            System.Diagnostics.Process process = new System.Diagnostics.Process();
            //            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //            startInfo.FileName = "arpspoof.exe";
            //            startInfo.Arguments = " " + item.SubItems[0].Text;
            //            process.StartInfo = startInfo;
            //            process.Start();
            //            CreateDebugFile("#Start  : IP blocking started");
            //        }catch(Exception ex)
            //        {
            //            CreateDebugFile("#Error  : Exception while starting IP blocking" + ex.Message);
            //            MessageBox.Show("You don't have permission to block an IP", "Firewall", MessageBoxButtons.OK);
            //            return;

            //        }

            //        //do something
            //        Thread.Sleep(1000);
            //        MessageBox.Show("IP:" + (lstLocal.SelectedItems[0]).Text + "  successfully Blocked", "Firewall", MessageBoxButtons.OK);

            //    }
            //    else if (dialogResult == DialogResult.No)
            //    {
            //        //do something else
            //    }

               
            //}
            //else
            //{
            //  //  EmpIDtextBox.Text = string.Empty;
            //  //  EmpNametextBox.Text = string.Empty;
            //}
        }

        private bool BlockIP(string IP)
        {
            string gate_ip = NetworkGateway();
            if (gate_ip != null)
                CreateDebugFile("#Success  : gate ip received successfully");
            else
                CreateDebugFile("#Error  : gateway address empty(null)");

            if (lstLocal.SelectedItems.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to block this IP:" + IP + "?", "Firewall", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    ListViewItem item = lstLocal.SelectedItems[0];
                    //   MessageBox.Show(item.SubItems[0].Text);

                    try
                    {
                        if (File.Exists("arpspoof.exe"))
                        {
                            //new Thread(() =>
                            //{
                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            startInfo.FileName = "arpspoof.exe";
                            startInfo.Arguments = " " + IP;
                            //   startInfo.Arguments = " " + item.SubItems[0].Text;
                            process.StartInfo = startInfo;
                            process.Start();
                            CreateDebugFile("#Start  : IP blocking started");
                            //}).Start();
                        }
                        else
                        {
                            CreateDebugFile("#Error  : arpspoof.exe not found");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        CreateDebugFile("#Error  : Exception while starting IP blocking" + ex.Message);
                        MessageBox.Show("You don't have permission to block an IP", "Firewall", MessageBoxButtons.OK);
                        return false;

                    }

                    //do something
                    Thread.Sleep(1000);
                    MessageBox.Show("IP:" + IP + "  successfully Blocked", "Firewall", MessageBoxButtons.OK);
                    CreateDebugFile("#Success : IP blocked Successfully");
                    return true;

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                    return false;
                }


            }
            else
            {
                //  EmpIDtextBox.Text = string.Empty;
                //  EmpNametextBox.Text = string.Empty;
            }
            return false;
        }

        private void SetLoading(bool displayLoader)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    pictureBox1.Visible = true;
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    pictureBox1.Visible = false;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          //  var id = lstLocal.Columns[0].Index;
          //  var status = lstLocal.Columns[3].Index;

            //if (lstLocal.SelectedItems.Count <= 0)
            //{
            //    MessageBox.Show("no ip selected");
            //}
            //else
            //{
            //    foreach (ListViewItem ip in lstLocal.SelectedItems)
            //    {
            //        MessageBox.Show((lstLocal.SelectedItems[0]).Text);
            //        if (BlockIP((lstLocal.SelectedItems[0]).Text))
            //        {
            //         //   (lstLocal.SelectedItems[3]).Text = "Blocked";
            //        }
            //    }
            //    //foreach (ListViewItem item in lstLocal.Items)
            //    //{
            //    //    if (item.SubItems[id].Text == (lstLocal.SelectedItems[0]).Text)
            //    //    {
            //    //        item.SubItems[status].Text = "Blocked";
            //    //        break;
            //    //    }
            //    //}
            //}
           
           
        }

        private void button_blockIP1(object sender, EventArgs e)
        {
            if (lstLocal.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please Select an IP address first!", "Instruction", MessageBoxButtons.OK);
            }
            else
            {
                foreach (ListViewItem ip in lstLocal.SelectedItems)
                {
                  //  MessageBox.Show((lstLocal.SelectedItems[0]).Text);
                    if (BlockIP((lstLocal.SelectedItems[0]).Text))
                    {

                        ListViewItem myItem = lstLocal.SelectedItems[0];

                        myItem.SubItems[3].Text = "blocked";

                     //   (lstLocal.SelectedItems[0].SubItems[3]).Add("d");
                    }
                    else
                    {
                        MessageBox.Show("Error in blocking the IP", "Firewall", MessageBoxButtons.OK);
                      //  MessageBox.Show("Error in blocking the IP");
                       // CreateDebugFile("#Error  : Exception while starting IP blocking" + ex.Message);
                    }
                }
             
            }
        }

      
    }
}
