using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{

    /// <summary>
    /// Класс программы.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа.
        /// </summary>
        /// <param name="args"> Аргументы. </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Билдит все, что нам надо.
        /// </summary>
        /// <param name="args"> Аргументы. </param>
        /// <returns> Создатель хоста. </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
