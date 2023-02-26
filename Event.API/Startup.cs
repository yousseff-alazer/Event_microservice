using Event.API.Event.DAL.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Event.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins,
            //          builder =>
            //          {
            //              builder.WithOrigins("https://localhost:4200", "http://127.0.0.1:4200", "http://localhost:4200")
            //          .AllowAnyMethod()
            //           .AllowAnyHeader()
            //           .AllowCredentials();
            //          }));
            services.AddCors();
            var dbConnectionString = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            services.AddDbContext<eventdbContext>(opt =>
                opt.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Event.API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event.API v1"));
            }
            app.UseDefaultFiles();
            app.UseStaticFiles(); // For the wwwroot folder
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            //app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            //app.UseCors(builder => builder
            //    .WithOrigins("http://192.168.1.1:4200", "http://127.0.0.1:4200", "http://localhost:4200")
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials());
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}