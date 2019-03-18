using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Acapedia.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Acapedia.Data.Models;
using Acapedia.Data.Contracts;
using Acapedia.Service;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Acapedia
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
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
                googleOptions.ClaimActions.MapJsonKey("urn:google:profile", "link");
                googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                //googleOptions.Events.OnCreatingTicket = (context) =>
                //{
                //    context.Identity.AddClaim(new Claim("avatar", context.User.GetValue("picture").ToString()));

                //    return Task.CompletedTask;
                //};
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<IdentityOptions>(options =>
                {
                    options.Stores.MaxLengthForKeys = 128;
                });

            services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/account/login";
                    options.LogoutPath = $"/account/logout";
                    options.AccessDeniedPath = $"/account/access-denied";
                });

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IExplore, ExploreService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

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
