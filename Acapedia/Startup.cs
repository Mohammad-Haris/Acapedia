using System.Security.Claims;
using Acapedia.Data;
using Acapedia.Data.Contracts;
using Acapedia.Data.Models;
using Acapedia.Service;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Acapedia
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            // inject counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            services.Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddDbContext<AcapediaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AcapediaDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
                   {
                       googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                       googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                       googleOptions.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                       googleOptions.ClaimActions.Clear();
                       googleOptions.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                       googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Uri, "picture");
                       googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                       googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                       googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                   });

            services.AddMvc().AddNewtonsoftJson();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.Configure<IdentityOptions>(options =>
                        {
                            options.Stores.MaxLengthForKeys = 128;
                        });

            services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/account/login";
                    options.LogoutPath = $"/account/logout";
                });

            services.AddScoped<IExplore, ExploreService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIpRateLimiting();

            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithReExecute("/Error");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error");
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
