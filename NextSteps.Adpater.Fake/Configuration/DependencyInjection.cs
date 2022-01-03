using Microsoft.Extensions.DependencyInjection;
using NextSteps.Business.Ports;

namespace NextSteps.Adpater.Fake.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFakeNextStepsAdapter(this IServiceCollection services)
        {
            return services.AddSingleton<IPersonPort, FakeNextStepsAdapter>();
        }
    }
}