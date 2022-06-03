using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;

namespace Trading.Authen.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .WriteTo.Console(new RenderedCompactJsonFormatter())
          .WriteTo.Debug(outputTemplate: DateTime.Now.ToString("dd/MM/yyy HH:mm ss"))
          .WriteTo.File("Logs/log_error_.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error)
          .WriteTo.File("Logs/log_common_.txt", rollingInterval: RollingInterval.Day)
          .CreateLogger();
           CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    //.UseUrls("http://localhost:4000")
                    ;
                });
    }
}
