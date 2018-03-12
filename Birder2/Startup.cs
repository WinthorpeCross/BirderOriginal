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

namespace Birder2
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
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

            //ToDo: Work out what type of service these should be - Singletons?
            services.AddTransient<IStream, StreamService>();
            services.AddTransient<IFlickrService, FlickrService>();

            services.AddMvc().AddJsonOptions
                (options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            //app.UseWelcomePage()

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
