using Aquaculture.API.Data;
using Aquaculture.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aquaculture.API
{
    public class Startup
    {
        readonly string CorsAllowSpecificOrigins = "_corsAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Db Context
            services.AddDbContext<AquacultureContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AquacultureDB")));
            
            // Controllers
            services.AddControllers().AddNewtonsoftJson(option =>
                //Ignore circular references
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

            // Repository
            services.AddTransient<IFarmRepository, FarmRepository>();
            services.AddTransient<IWorkerRepository, WorkerRepository>();

            // Other
            services.AddAutoMapper(typeof(Startup));

            // CORS config
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000");
                                  });
            });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aquaculture.API", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aquaculture.API v1"));
            }

            app.UseHttpsRedirection();

            // CORS
            app.UseCors(CorsAllowSpecificOrigins);

            // Serve static files
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
