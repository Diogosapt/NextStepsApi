using System.Threading.Tasks;

namespace NextSteps.Business.Core.Configuration
{
    public interface IAuthenticationTokenStore
    {
        Task<string> GetToken();
    }
}