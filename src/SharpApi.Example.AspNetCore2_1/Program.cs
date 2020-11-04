﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SharpApi.Example.AspNetCore2_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<AspNetCoreStartup>();
    }
}
