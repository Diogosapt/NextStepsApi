using MediatR;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Models;
using System;

namespace NextSteps.Business.UsesCases
{
    public record PersonCreateCommand(Models.Person Person) : IRequest<ApiResult<Person>>;
}