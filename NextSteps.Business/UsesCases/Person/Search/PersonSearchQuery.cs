using MediatR;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Models;

namespace NextSteps.Business.UsesCases
{
    public record PersonSearchQuery(Filters filters, int page, int pageSize) : IRequest<ApiResult<PagedResult<Models.Person>>>;
}