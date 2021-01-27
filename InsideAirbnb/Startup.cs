using InsideAirbnb.Models;
using InsideAirbnb.Repositories;
using InsideAirbnb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Profiling.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnb
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
            services.AddResponseCompression();

            services.AddMiniProfiler(options => {
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
                options.PopupShowTimeWithChildren = true;
                // options.Storage = new SqlServerStorage(Configuration.GetConnectionString("MiniProfiler"));
            }).AddEntityFramework();

            services.AddDbContext<AirBNBContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("AirBNB")
            ));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("Redis");
                options.InstanceName = "InsideAirbnb";
            });

            services.AddScoped<IListingSummaryRepository, ListingSummaryRepository>();
            services.AddScoped<IListingRepository, ListingRepository>();

            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme).AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));
            services.AddAuthorization(options => {
                options.AddPolicy("Admin", policy => policy.RequireClaim("extension_Role", "Admin"));
            });

            services.AddControllersWithViews();
            services.AddRazorPages(); // For Azure B2C
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

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

            if(env.IsDevelopment())
            {
                app.UseMiniProfiler();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
