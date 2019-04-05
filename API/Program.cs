﻿using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "WebApi";
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5001")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
