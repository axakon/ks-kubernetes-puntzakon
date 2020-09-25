using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using puntman.Data;
using puntman.Models;
using ServiceStack.Redis;
using Shared;

namespace puntman
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<IRedisClientsManager>(c => 
                new RedisManagerPool(Configuration.GetConnectionString("Redis")));
            services.AddScoped<PunService>();
            services.AddScoped<Publisher>();
            services.AddSingleton<GuidViewModel>((c) => new GuidViewModel { Guid = Guid.NewGuid().ToString() });
            services.AddSingleton<WeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PunService puns)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/api/puns", async context => {
                        Console.WriteLine("Hit puns API");
                        try
                        {
                            var body = await JsonSerializer.DeserializeAsync<PunViewModel>(context.Request.Body);
                            await puns.AddPun(body);
                            context.Response.StatusCode = 200;
                        }
                        catch
                        {
                            context.Response.StatusCode = 500;
                        }

                });
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
