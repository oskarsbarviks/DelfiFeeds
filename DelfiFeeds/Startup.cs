using FrontendLogic;
using FrontendLogic.FacebookClient;
using FrontendLogic.Interfaces;
using HostedServices;
using HostedServices.HttpClients;
using HostedServices.Interfaces;
using HostedServices.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories.Interfaces;
using Repositories.Repositories;
using Shared;
using Shared.Interfaces;

namespace DelfiFeeds
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
            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                      .AddCookie(options =>
                                      {
                                          options.Cookie.Name = "CatchSmartCookie";
                                          options.LoginPath = "/Home/Login";
                                      });

            services.AddTransient<IDelfiFeedService, DelfiFeedService>();
            services.AddTransient<IDelfiFeedEndpointManager, DelfiFeedEndpointManager>();
            services.AddTransient<IDelfiFeedClient, DelfiFeedClient>();
            services.AddTransient<IFeedRepository, FeedRepository>();
            services.AddTransient<IFeedsUpdateTimeRepository, FeedsUpdateTimeRepository>();
            services.AddScoped<IAuthorizationLogic, AuthorizationLogic>();
            services.AddScoped<IFacebookClient, FacebookClient>();
            services.AddScoped<ILoginLogic, LoginLogic>();
            services.AddScoped<IUserRepository, UserRepository>();
            // Add service which will run in background and update delfi rss feeds
            // services.AddHostedService<DelfiFeedHostedService>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
