using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TelescopeSimulator.Alpaca
{
    public class Startup
    {
        public static int PortNumber = 0;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            try
            {
                var addresses = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses;

                try
                {
                    PortNumber = new Uri(addresses.First()).Port;
                }
                catch
                {
                    //Some Addresses are not valid Uris. These should be in the form of http://*:port. This is a quick and dirty solution to test this.
                    PortNumber = Convert.ToInt32(addresses.First().Split(':')[2]);
                }
            } 
            catch
            {
                //Could not read port number
                //Todo fix this, add logging
            }
        }
    }
}
