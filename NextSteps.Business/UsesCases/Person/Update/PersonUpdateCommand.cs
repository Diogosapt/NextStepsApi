using MediatR;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Models;

namespace NextSteps.Business.UsesCases
{
    public record PersonUpdateCommand(Models.Person Person) : IRequest<ApiResult<Person>>
    {
    }
}