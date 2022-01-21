using Artizanalii.Data;
using Artizanalii.Interfaces;
using Artizanalii.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Artizanalii.Helpers;

namespace Artizanalii
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
            services.AddCors();
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore

                );
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProdusRepository, ProdusRepository>();
            services.AddScoped<IProducatorRepository, ProducatorRepository>();
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();

            services.AddScoped<JwtService>();

            services.AddDbContext<ArtizanaliiContext>(opt =>
            opt.UseSqlServer("Data Source=DESKTOP-I7UGUCL;Initial Catalog=DbArtizanalii;Integrated Security=True")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(options => {
                options.WithOrigins(new[] {"http://localhost:3000"}).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
