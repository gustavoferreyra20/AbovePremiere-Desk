using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AP_Desk_Ferreyra;
using AP_Web_Ferreyra.DAOs;
using AP_Web_Ferreyra.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AP_Web_Ferreyra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connString = "Server=localhost;Database=abovepremiere;Uid=root;Pwd=;";
            DBConnection.getInstance().connect(connString);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
