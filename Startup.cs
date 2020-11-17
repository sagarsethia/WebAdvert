using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using WebAdvertisment.Contract;
using WebAdvertApi.Repository;
using WebAdvertApi.Contract;
using WebAdvertApi.HealthChecks;

namespace WebAdvertisment
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
            services.AddScoped<ISignUpUser, Repository.SignUpRepository>();
            services.AddTransient<IAdvertStorage ,DynamoDBAdvertStorage>();
            services.AddAutoMapper(typeof(DynamoDBAdvertStorage).Assembly);
            services.AddHealthChecks();
            // services.AddHealthChecks(check=>{
            //     check.AddCheck<StorageHealth>("Storage",new System.TimeSpan(0,1,0));
            //     });
            services.AddControllers();
            services.AddCognitoIdentity();
            services.AddHttpsRedirection(opt => opt.HttpsPort = 443);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseHealthChecks("/Health");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
