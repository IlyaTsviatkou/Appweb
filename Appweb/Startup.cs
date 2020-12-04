using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System;
using Appweb.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Appweb.Infrastructure.Data;
using Appweb.Domain.Core;
using Appweb.Services.Business;
//using Microsoft.Extensions.Azure;

namespace Appweb
{
    public class Startup
    {
        private UserManager<User> userManager;
        private ApplicationContext _context;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
          
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Appweb")));
            

            services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequireNonAlphanumeric = false; 
            })
             .AddEntityFrameworkStores<ApplicationContext>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddViewLocalization();
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddAuthentication()
              .AddGoogle(options =>
              {
                  IConfigurationSection googleAuthNSection =
                     Configuration.GetSection("Authentication:Google");

                  options.ClientId = "514947319812-v7us8nlcqk1h7fupflf363jf0n2sik85.apps.googleusercontent.com";
                  options.ClientSecret = "0H03aJwJCs8sVxNAo3D5JLf_";
              });
              /*.AddFacebook(facebookOptions =>
              {
                  facebookOptions.AppId = "963468734148402";
                  facebookOptions.AppSecret = "b022e8014d1435cf516b4938d1d72580";
              });*/
            services.AddScoped<ArticleRepository>();
            services.AddTransient<EmailService>();
            services.AddSingleton<ImageService>();

            /*services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration["ConnectionStrings:DefaultConnection1"]);
            });*/

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/ErrorPro");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");
           // app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();  
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<CommentHub>("/comment");
            });
            
           
        }
    }
}