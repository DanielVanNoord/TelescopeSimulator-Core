using ASCOM.Simulator;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using TelescopeSimulator.Alpaca;

namespace TelescopeSimulator
{
    public class AlpacaHost
    {
        private static IWebHost host;

        public static void Start(string[] args)
        {
            if (!args.Any(str => str.Contains("--urls")))
            {
                Console.WriteLine("No startup args detected, binding to local host and default port.");

                var temparray = new string[args.Length + 1];

                args.CopyTo(temparray, 0);

                temparray[args.Length] = "--urls=http://127.0.0.1:4321";

                args = temparray;
            }

            ASCOM.Alpaca.Controllers.DeviceManager.SetTelescopeAccess(TelescopeSimulator.DeviceManager.GetTelescope);

            using (host = CreateWebHostBuilder(args).Build())
            {
                //Start and do not block on Alpaca
                host.Start();

                if (4321 != 0)
                {
                    try
                    {
                        DiscoveryServer server = new DiscoveryServer(4321);
                    }
                    catch
                    {
                        /*using (ASCOM.Utilities.TraceLogger tl = new ASCOM.Utilities.TraceLogger())
                        {
                        
                        }*/
                    }
                }

                //OSX does not support UI yet, just run Alpaca.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    host.WaitForShutdown();
                }
                else
                {
                    //Linux and Windows support UI, start and block on it
                    using (Manager.m_MainForm = new FrmMain())
                    {
                        Manager.m_MainForm.ShowDialog();
                        Shutdown();
                    }
                }
            }
        }

        public static void Shutdown()
        {
            host.StopAsync();
            try
            {
                if (Manager.m_MainForm.InvokeRequired)
                {
                    Manager.m_MainForm.Invoke(new Action(() => Manager.m_MainForm.Close()));
                }
                else
                {
                    Manager.m_MainForm.Close();
                }
            }
            catch
            {
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel();
    }
}