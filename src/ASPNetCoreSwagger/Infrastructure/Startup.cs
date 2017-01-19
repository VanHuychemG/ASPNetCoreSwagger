using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASPNetCoreSwagger.Infrastructure
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.MultipleApiVersions(new Swashbuckle.Swagger.Model.Info[]
                {
                    new Swashbuckle.Swagger.Model.Info
                    {
                        Version = "v2",
                        Title = "API (version 2.0)",
                        Description = "A RESTful API to show Swagger and Swashbuckle"
                    },
                    new Swashbuckle.Swagger.Model.Info
                    {
                        Version = "v1",
                        Title = "API",
                        Description = "A RESTful API to show Swagger and Swashbuckle"
                    }
                }, (description, version) =>
                {
                    return description.RelativePath.Contains($"api/{version}");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            //app.UseMvc();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger();

            app.UseSwaggerUi();
        }
    }
}
