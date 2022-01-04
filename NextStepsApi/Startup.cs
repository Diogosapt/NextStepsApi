using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NextSteps.Adpater.Fake.Configuration;
using NextSteps.Adpater.Mongo.Configuration;
using NextSteps.Adpater.Mongo.Data;
using NextSteps.Adpater.SQL.Configuration;
using NextSteps.Adpater.SQL.Data;
using NextSteps.Api.Infrastructure;
using NextSteps.Business.Core.Configuration;

namespace NextStepsApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NextStepsApi",
                    Version = "v1"
                });
            });

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddAutoMapper(typeof(ContextAutoMapperProfile));
            services.AddAutoMapper(typeof(MongoContextAutoMapperProfile));

            services.AddNextStepsUseCases();

            //services.AddFakeNextStepsAdapter();
            //services.AddDatabaseAdapter();
            //services.AddSqlPersonAdapter();

            services.Configure<DataBaseMongo>(Configuration.GetSection("DataBaseMongo"));
            services.AddMongoAdapter();
            services.AddMongoPersonAdapter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NextStepsApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            //scope.ServiceProvider.GetService<NextStepsContext>().Database.Migrate();
        }
    }
}