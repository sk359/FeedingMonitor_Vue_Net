using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using backend.Data;

namespace backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
            services.AddDbContext<FeedingContext>(options => options.UseNpgsql(Configuration.GetConnectionString("FeedingDatabase")));  
            services.AddScoped<IFeedingRepository, FeedingRepository>();      
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);   

            services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot"; });
            
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                    //.AllowAnyHeader()
                    //.WithMethods("GET", "OPTIONS")
                    .AllowAnyMethod()
                    //.AllowCredentials()
                    .AllowAnyOrigin()
                    .AllowAnyHeader();
                    //.WithOrigins("http://localhost:5173");
                });
            });
            
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FeedingContext dbContext)
        {

            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
               /* app.UseSpa(spa =>
               {
                  spa.Options.SourcePath = "D:\\Dokumente\\csharp_projekte\\FeedingMonitor_Vue_Net\\frontend\\dist";
                  spa.UseProxyToSpaDevelopmentServer("https://localhost:5173");
               });  */
            } 
            else
            {
               //app.UseSpaStaticFiles();
               app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }   

            app.UseHttpsRedirection();        

            app.UseCors("VueCorsPolicy");
            /* app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials */

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }    

            dbContext.Database.EnsureCreated();
            //app.UseAuthentication();
            app.UseMvc();
            app.UseRouting();
            //app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpaStaticFiles();

            app.UseSpa(configuration: builder =>
            {
                builder.Options.SourcePath = "D:\\Dokumente\\csharp_projekte\\FeedingMonitor_Vue_Net\\backend\\wwwroot";
                if (env.IsDevelopment())
                {
                    builder.UseProxyToSpaDevelopmentServer("http://localhost:5173");
                }
                //spa.Options.SourcePath = "D:\\Dokumente\\AngularDotNet\\angular-dot-net\\dist\\angular-dot-net";
               
            });

            
        }
    }
}