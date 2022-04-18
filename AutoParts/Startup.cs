namespace AutoParts
{
    using AutoParts.Core.Contracts;
    using AutoParts.Core.Services;
    using AutoParts.Extensions;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Identity;
    using AutoParts.Infrastructure.Data.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                    .AddScoped<IApplicationDbRepository, ApplicationDbRepository>()
                    .AddScoped<IOrderService, OrderService>()
                    .AddDbContext<AutoPartsDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAuthentication()
     .AddFacebook(options =>
     {
         options.AppId = Configuration["Authentication:Facebook:AppId"];
         options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
     });

            services
                    .AddDefaultIdentity<IdentityUser>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                    .AddEntityFrameworkStores<AutoPartsDbContext>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
