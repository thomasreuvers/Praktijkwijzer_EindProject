using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MaasVallei.Models;
using MaasVallei.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MaasVallei
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
            services.AddControllersWithViews();

            // Configure Database settings
            services.Configure<MaasValleiDatabaseSettings>(
                Configuration.GetSection(nameof(MaasValleiDatabaseSettings)));

            services.AddSingleton<IMaasValleiDatabaseSettings>(x =>
                x.GetRequiredService<IOptions<MaasValleiDatabaseSettings>>().Value);

            // Add db services
            services.AddSingleton<UserService>();
            services.AddSingleton<ReservationService>();
            services.AddSingleton<ScheduleService>();
            services.AddSingleton<ComplaintsService>();
            
            // Register the password hashing service
            services.AddSingleton<PasswordHashService>();

            // Add cookie authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.Cookie.Name = "UserLoginCookie";
                config.LoginPath = "/Account/Login";
                config.AccessDeniedPath = "/Account/Login";
                config.LogoutPath = "/Account/Logout";
            });

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
