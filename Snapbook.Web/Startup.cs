namespace Snapbook.Web
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using Services.Implementations;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SnapbookDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@ ";
                })
                .AddEntityFrameworkStores<SnapbookDbContext>()
                .AddDefaultTokenProviders();


            services.AddAutoMapper();

            services.AddTransient<PhotoService>();
            services.AddTransient<NotificationService>();
            services.AddDomainServices();

            services.AddRouting(routing => routing.LowercaseUrls = true);
            
            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = this.Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = this.Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = this.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = this.Configuration["Authentication:Google:ClientSecret"];
            });

            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = this.Configuration["Recaptcha:SiteKey"],
                SecretKey = this.Configuration["Recaptcha:SecretKey"]
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "notifications",
                    template: "users/{username}/notifications",
                    defaults: new { controller = "Users", action = "Notifications" });

                routes.MapRoute(
                    name: "change-profile-pic",
                    template: "users/{username}/change-profile-photo",
                    defaults: new { controller = "Users", action = "ChangeProfilePic" });

                routes.MapRoute(
                    name: "users",
                    template: "users/{username}",
                    defaults: new { controller = "Users", action = "Profile" });

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
