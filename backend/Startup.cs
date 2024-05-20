
using Microsoft.EntityFrameworkCore;
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
            } 
            else
            {               
               app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }   

            app.UseHttpsRedirection();        

            app.UseCors("VueCorsPolicy");            

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }    

            dbContext.Database.EnsureCreated();
            
            app.UseMvc();
            app.UseRouting();                       

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
               
            });

            
        }
    }
}