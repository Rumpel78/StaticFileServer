using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace paprikon.StaticFileServer
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(ILogger<Startup> logger)
        {
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
            var basepath = (config["ASPNETCORE_BASEPATH"] ?? "").TrimEnd('/');

            _logger.LogInformation("ASPNETCORE_BASEPATH = " + basepath);
            _logger.LogInformation($"ASPNETCORE_SPA = {(config["ASPNETCORE_SPA"] ?? "false")}");
            _logger.LogInformation($"ASPNETCORE_INDEX = {(config["ASPNETCORE_INDEX"] ?? "index.html")}");

            app.UseDefaultFiles(basepath);
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
                },
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = basepath,
            });

            Boolean.TryParse(config["ASPNETCORE_SPA"], out var isSpaApplication);
            if (isSpaApplication) {
                app.UseMvc(routes => {
                    routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
                });
            }
        }
    }
}
