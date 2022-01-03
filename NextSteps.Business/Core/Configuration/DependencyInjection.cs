using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NextSteps.Business.Core.Behaviours;
using System.Reflection;

namespace NextSteps.Business.Core.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNextStepsUseCases(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly())
                    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            return services;
        }
    }
}