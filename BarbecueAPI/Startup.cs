using System.IO;
using BarbecueAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;
using Newtonsoft.Json;
using Services;

namespace BarbecueAPI
{
    public class Startup
    {
        private ILogger<Startup> _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string WWWRootPath { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRouting(options => options.LowercaseUrls = true);
            services
                .AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Error =
                            (sender, args) => _logger.LogCritical(args.ErrorContext.Error.Message);
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        // options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    }
                );

            services
                .AddBarbecueDependencies();

            services.AddSingleton(_ => new WWWRootPathHolder(WWWRootPath));
            
            services
                .AddSwaggerGen(swagger => swagger.SwaggerDoc("v1", new OpenApiInfo() {Title = "Barbecue API Docs"}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMiddleware<RequestCounterMiddleware>();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Barbecue API Docs");
                c.RoutePrefix = "docs";
            });

            WWWRootPath = Path.GetFullPath("../barbecue-front", env.ContentRootPath);
            env.WebRootPath = WWWRootPath;

            // app.UseHttpsRedirection();

            _logger = loggerFactory.CreateLogger<Startup>();

            _logger.LogInformation(WWWRootPath);

            env.WebRootFileProvider = new PhysicalFileProvider(WWWRootPath);

            app.UseDefaultFiles(); // Serve index.html for route "/"
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = env.WebRootFileProvider,
                ServeUnknownFileTypes = true
            });

            app.UseRouting();

            app.UseCors(builder => builder
                .WithOrigins(
                    "http://localhost:8080",
                    "http://localhost:4200",
                    "https://localhost:8443",
                    "https://localhost:4200",
                    "http://akiana.io:8080",
                    "https://akiana.io:8443")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "api_area",
                    areaName: "API",
                    pattern: "api/{controller}/{action}");

                endpoints.MapAreaControllerRoute(
                    name: "mobile_area",
                    areaName: "Mobile",
                    pattern: "m/{controller}/{action}");

                endpoints.MapDefaultControllerRoute();
            });

            // serve index.html for everything not mapped
            app.UseSpa(builder => { builder.Options.SourcePath = WWWRootPath; });
        }
    }
}