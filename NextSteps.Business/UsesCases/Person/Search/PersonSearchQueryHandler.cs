using MediatR;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Ports;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Business.UsesCases
{
    public class PersonSearchQueryHandler : IRequestHandler<PersonSearchQuery, ApiResult<PagedResult<Models.Person>>>
    {
        private readonly IPersonPort _personPort;

        public PersonSearchQueryHandler(IPersonPort personPort)
        {
            _personPort = personPort ?? throw new ArgumentNullException(nameof(personPort));
        }

        public async Task<ApiResult<PagedResult<Models.Person>>> Handle(PersonSearchQuery request, CancellationToken cancellationToken)
        {
            return await _personPort.Search(request.filters, request.page, request.pageSize);
        }
    }
}