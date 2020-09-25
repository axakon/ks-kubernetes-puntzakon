using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack.Redis;
using Shared;

namespace puntworker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IRedisClientsManager>(c => 
                        new RedisManagerPool(hostContext.Configuration.GetConnectionString("Redis")));
                    services.AddSingleton<Subscriber>();
                    services.AddHostedService<Worker>();
                });
    }
}
