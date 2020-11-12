using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AP_Desk_Ferreyra.DAOs;
using AP_Desk_Ferreyra.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AP_Desk_Ferreyra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MiPropioMain();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void MiPropioMain()
        {

            UsuarioDAO.getInstancia()
                .add(new Usuario("pepito", "123"))
                .add(new Usuario("123", "123"))
                .add(new Usuario("admin", "admin"));

        }
    }
}
