using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultaMedica.Models;
using ConsultaMedica.Models.Validaciones;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsultaMedica
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //json
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Add Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddControllers();
            services.AddTransient<IValidator<Paciente>,PacienteValidacion>();
            services.AddTransient<IValidator<Doctor>, DoctorValidacion>();
            services.AddSingleton<DbDataContext>();
            services.AddSingleton<DbDataDoctores>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            // Enable Cors
            app.UseCors("MyPolicy");

            app.UseEndpoints(endpoints =>
            {
                //ndpoints.MapControllers();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pacientes}/{action=Index}/{id?}");
            });
        }
    }
}
