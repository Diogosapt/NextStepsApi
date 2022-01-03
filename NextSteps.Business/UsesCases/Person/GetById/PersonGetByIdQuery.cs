using MediatR;
using NextSteps.Business.Core.Common;
using System;

namespace NextSteps.Business.UsesCases
{
    public record PersonGetByIdQuery(Guid Id) : IRequest<ApiResult<Models.Person>>;
}