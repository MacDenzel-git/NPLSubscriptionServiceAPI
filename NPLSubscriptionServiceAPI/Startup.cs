using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NPLDataAccessLayer.Models;
using NPLSubscriptionServiceAPI;

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

        services.AddRepositories();
        services.AddServices();
        services.AddControllers();
        
           
        services.AddDbContext<NPLSubsctiptionServiceDBContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("NPLConnectionString"));
            options.EnableSensitiveDataLogging();
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "NPLWebServiceAPI", Version = "v1" });
      
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NPLWebServiceAPI v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


    }
}