using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using RoundpayFinTech.AppCode.Configuration;
using RoundpayFinTech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.MiddleLayer.EFTModel;

namespace RoundpayFinTech
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
            services.AddService();
            services.AddDbContextPool<AppDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DBCon"));
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IEFTCommonH, EFTCommonH_ML>();
            services.AddDistributedMemoryCache();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowWEBAAP",
                builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession(options => {
                options.Cookie.Name = ".roundpay.fintech";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
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
            app.UseCors("AllowWEBAAP");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"Image")),
                RequestPath = new PathString("/Image")
            });

            FileExtensionContentTypeProvider contentTypes = new FileExtensionContentTypeProvider();
            contentTypes.Mappings[".apk"] = "application/vnd.android.package-archive";
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = contentTypes
            });
        }
    }
}
