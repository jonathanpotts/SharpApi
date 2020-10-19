using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SharpApi.AspNetCore;

namespace SharpApi.Example.AspNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            SharpApiHost.CreateBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
