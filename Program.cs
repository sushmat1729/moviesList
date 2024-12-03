using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieList.Models;
using Microsoft.AspNetCore.Identity;
using MovieList.Data;
using MovieList.Middleware;

namespace MovieList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // Add services to the container
                        var configuration = context.Configuration;

                        services.AddControllersWithViews();

                        // Ensure you use ApplicationUser as the identity user type
                        services.AddDbContext<MovieContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("MovieContext")));

                        // Register Identity services with correct ApplicationUser
                        services.AddDefaultIdentity<ApplicationUser>(options =>
                        {
                            options.SignIn.RequireConfirmedAccount = true;
                        })
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<MovieContext>();  // Ensure this part is correct

                        // Configure routing
                        services.AddRouting(options =>
                        {
                            options.LowercaseUrls = true;
                            options.AppendTrailingSlash = true;
                        });
                    });

                    webBuilder.Configure((context, app) =>
                    {
                        // Configure the HTTP request pipeline
                        var env = context.HostingEnvironment;

                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseStaticFiles();
                        app.UseRouting();

                        // Authentication should be before middleware that requires it
                        app.UseAuthentication();
                        app.UseMiddleware<PrivacyMiddleware>(); // Privacy middleware should run after authentication
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
                            endpoints.MapRazorPages();
                        });
                    });
                });
    }
}
