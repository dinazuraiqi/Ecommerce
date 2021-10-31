using Ecommerce.Areas.Admin.Models;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class Program
    {               
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseIISIntegration()
                    .UseStartup<Startup>();
                    
                   
                });              
    }
}
