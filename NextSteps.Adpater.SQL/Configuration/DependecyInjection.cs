using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextSteps.Adpater.SQL.Data;
using NextSteps.Adpater.SQL.Infrastructure;
using NextSteps.Adpater.SQL.Infrastructure.Repositories;
using NextSteps.Business.Ports;

namespace NextSteps.Adpater.SQL.Configuration
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddDatabaseAdapter(this IServiceCollection services)
        {
            return services.AddDbContext<NextStepsContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"))
                .AddUnitOfWork<NextStepsContext>();
        }

        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
           where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            return services;
        }

        public static IServiceCollection AddSqlPersonAdapter(this IServiceCollection services)
        {
            return services.AddScoped<IPersonPort, SqlPersonAdapter>();
        }
    }
}