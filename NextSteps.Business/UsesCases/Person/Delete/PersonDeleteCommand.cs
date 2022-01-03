using MediatR;
using NextSteps.Business.Core.Common;
using System;

namespace NextSteps.Business.UsesCases
{
    public record PersonDeleteCommand(Guid Id) : IRequest<ApiResult>;
}