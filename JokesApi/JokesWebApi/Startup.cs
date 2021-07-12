using AutoMapper;
using JokesWebApi.Configuration;
using JokesWebApp.DAL.Domain.Context;
using JokesWebApp.DAL.Models;
using JokesWebApp.DAL.Repositories.GenericRepository;
using JokesWebApp.DAL.UnitOfWork;
using JokesWebApp.Data.Mapper;
using JokesWebApp.Data.Mapper.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokesWebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JokesWebApi", Version = "v1" });
            });

            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddDbContext<JokeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection"), b => b.MigrationsAssembly("JokesWebApp.DAL")));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });

            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<JokeContext>();

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            var assembly = AppDomain.CurrentDomain.Load("JokesWebApp.Data");
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, JokeContext _context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JokesWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            _context.Database.Migrate();
        }
    }
}
