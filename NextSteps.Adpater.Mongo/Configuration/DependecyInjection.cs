using Microsoft.Extensions.DependencyInjection;
using NextSteps.Adpater.Mongo.Data;
using NextSteps.Adpater.Mongo.Data.Configuration;
using NextSteps.Adpater.Mongo.Infrastructure;
using NextSteps.Adpater.Mongo.Infrastructure.Repositories;
using NextSteps.Business.Ports;

namespace NextSteps.Adpater.Mongo.Configuration
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddMongoAdapter(this IServiceCollection services)
        {
            MongoConfiguration.Configure();
            services.AddScoped<IMongoContext, MongoContext>().AddUnitOfWork<IMongoContext>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : IMongoContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            return services;
        }

        public static IServiceCollection AddMongoPersonAdapter(this IServiceCollection services)
        {
            return services.AddScoped<IPersonPort, MongoPersonAdapter>();
        }
    }
}