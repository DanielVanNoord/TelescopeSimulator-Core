using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASCOM.Simulator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TelescopeSimulator.Alpaca
{
    public class Program
    {
        private static IHost host;

        public static void Main(string[] args)
        {
            Console.CancelKeyPress += delegate
            {
                Shutdown();
            };

            Start(args);
        }

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

            using (host = CreateHostBuilder(args).Build())
            {


                //Start and do not block on Alpaca
                host.Start();

                if (Startup.PortNumber != 0)
                {
                    try
                    {
                        ASCOM.Standard.Discovery.Responder server = new ASCOM.Standard.Discovery.Responder(Startup.PortNumber);
                    }
                    catch
                    {
                        //Todo do not crash but log discovery down
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
                });
    }
}
