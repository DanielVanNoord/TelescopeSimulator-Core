using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASCOM.Simulator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TelescopeSimulator.Alpaca
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
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

            Manager.m_MainForm = new FrmMain();
            Manager.m_MainForm.Show();
            Manager.m_MainForm.Visible = true;

            using (var host = CreateHostBuilder(args).Build())
            {
                host.RunAsync();
                Application.Run(Manager.m_MainForm);
                host.StopAsync();
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
