using AutoMapper;
using ContentsLimitInsurance.App.Dtos;
using ContentsLimitInsurance.App.Mappings;
using ContentsLimitInsurance.App.Models;
using ContentsLimitInsurance.App.Repositories;
using ContentsLimitInsurance.App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ContentsLimitInsurance.App
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
            #region DBContext

            var appSettings = "appsettings.json";
#if DEBUG
            appSettings = "appsettings.Development.json";
#endif
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(appSettings).Build();

            services.AddDbContext<dbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlserver")));
            #endregion
            services.AddScoped<IHighValueItemsService, HighValueItemsService>();

            services.AddControllersWithViews();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HighValueItemProfile>();
                cfg.CreateMap<HighValueItemDto, HighValueItem>();
                cfg.CreateMap<HighValueItem, HighValueItemDto>();
                cfg.AddProfile<ItemCategoryProfile>();
                cfg.CreateMap<ItemCategoryDto, ItemCategory>();
                cfg.CreateMap<ItemCategory, ItemCategoryDto>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSpaStaticFiles(new StaticFileOptions { RequestPath = "/ClientApp/build" });
            //app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
