using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Birder2.Data;
using Birder2.Models;
using Birder2.Services;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Birder2
{
    public class StartupDevelopment
    {
        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<StorageAccountOptions>(Configuration.GetSection("StorageAccount"));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<IMachineClockDateTime, MachineClockDateTime>();

            services.AddTransient<IApplicationUserAccessor, ApplicationUserAccessor>();

            services.AddScoped<IBirdRepository, BirdRepository>();
            services.AddScoped<IObservationRepository, ObservationRepository>();
            services.AddScoped<ISideBarRepository, SideBarRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IObservationsAnalysisService, ObservationsAnalysisService>();
            services.AddScoped<ILIstService, ListService>();

            //ToDo: Work out what type of service these should be - Singletons?
            services.AddTransient<IStreamService, StreamService>();
            services.AddTransient<IFlickrService, FlickrService>();

            services.AddMvc().AddJsonOptions
                (options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //if (env.IsProduction())
            //{
            //    var options = new RewriteOptions()
            //        .AddRedirectToHttps();

            //    app.UseRewriter(options);
            //}

            //app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Observation}/{action=Index}/{id?}");
            });
        }
    }
}
