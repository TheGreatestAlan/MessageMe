using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MessageMe.Interfaces;
using MessageMe.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MessageMe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            List<Task> tasks = new List<Task>();

            tasks.Add(host.RunAsync());
            tasks.Add(((MessageSender)host.Services.GetService(typeof(IMessageSender))).SendMessage());
            Task.WaitAll(tasks.ToArray());

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
