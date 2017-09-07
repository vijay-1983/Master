using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace ReadyCollect.Web
{
    public class Startup
    {
        private static string ConnectionString { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ConnectionString = Configuration.GetValue<string>("ConnectionStrings:ReadyCollectDatabase");

            
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //Add Identity 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Dependency Injection
            services.AddTransient<Infrastructure.Admin.IRCADAttorneyService>(provider => new Data.Admin.RCADAttorneyService(ConnectionString));
            services.AddTransient<Infrastructure.Admin.IRCADUserService>(provider => new Data.Admin.RCADUserService(ConnectionString));
            services.AddTransient<Infrastructure.Base.ILoginService>(provider => new Data.Base.LoginService(ConnectionString));
            services.AddTransient<Infrastructure.Admin.IRCADCompanyService>(provider => new Data.Admin.RCADCompanyService(ConnectionString));
            services.AddTransient<Infrastructure.Admin.IRCADUserGroupService>(provider => new Data.Admin.RCADUserGroupService(ConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "MyCookeiAuthentication",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/RCLGLogin/Index"),
                AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/RCLGLogin/Index"),
                LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Home/RCLGLogoff"),
                AutomaticAuthenticate = true, 
                AutomaticChallenge = true
            });

            app.UseMvc();
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=RCLGDash}/{id?}");
            });
        }
    }
}
