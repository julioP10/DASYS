using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WEB.Data;
using WEB.Models;
using WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Dependencia;
using Microsoft.AspNetCore.Http;
using Utilis;
using Microsoft.Extensions.Logging;
using Acces;
//using DataAccessContext;

namespace WEB
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public static string  _conection = "";
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Conection();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.Add(new ServiceDescriptor(typeof(DataContext), new DataContext()));
             _conection = Configuration.GetConnectionString("DefaultConnection");
            Conection();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.Password.RequiredLength = 20;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
               .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.LoginPath = "/Login/Index/";
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConfiguration>(Configuration);
            //services.Configure<RazorViewEngineOptions>(options => {
            //    options.ViewLocationExpanders.Add(new ViewLocationExpander());
            //});
            services.AddMvc()
           .AddSessionStateTempDataProvider();

            services.AddScoped<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Auditoria", policy => policy.RequireClaim("Auditoria", "True"));
                options.AddPolicy(
                    name: "DynamicPermission",
                    configurePolicy: policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new DynamicPermissionRequirement());
                    });
            });

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddDependencyInjectionInterfaces(); // registrando las dependencias
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.Cookie.Name = ".DASys.Session";
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
           
            app.UseStaticFiles();
            app.UseSession();
            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });
      
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                  name: "Registro1",
                  template: "Acceso",
                  defaults: new { controller = "Home", action = "registro1" });

                routes.MapRoute(
                  name: "Registro2",
                  template: "datos-personales",
                  defaults: new { controller = "Home", action = "registro2" });

                routes.MapRoute(
                  name: "Registro3",
                  template: "formacion",
                  defaults: new { controller = "Home", action = "registro3" });

                routes.MapRoute(
                  name: "Registro4",
                  template: "experiencias",
                  defaults: new { controller = "Home", action = "registro4" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
       
            //app.AddCustomErrorHandler();
        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static IHttpContextAccessor HttpContextAccessor;

        public class DynamicPermissionRequirement : IAuthorizationRequirement
        {
        }

        public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
        {
            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicPermissionRequirement requirement)
            {
                var mvcContext = context.Resource as AuthorizationFilterContext;
                if (mvcContext == null)
                {
                    return;
                }

                var actionDescriptor = mvcContext.ActionDescriptor;
                var result = mvcContext.Result;
                var area = actionDescriptor.RouteValues["area"];
                var controller = actionDescriptor.RouteValues["controller"];
                var action = actionDescriptor.RouteValues["action"];

                var sEvento = area + controller + action;

                if (!context.User.HasClaim(c => c.Type == sEvento || c.Value == sEvento))
                {
                    context.Fail();
                    return;
                }
                else
                {
                    context.Succeed(requirement);
                }

            }
        }

        public static void Conection() {
            DataContext.ConnectionString = _conection;
        }
    }
}
