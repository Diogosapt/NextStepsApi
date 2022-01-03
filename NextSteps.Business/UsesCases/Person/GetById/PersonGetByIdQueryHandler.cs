using MediatR;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Ports;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Business.UsesCases
{
    public class PersonGetByIdQueryHandler : IRequestHandler<PersonGetByIdQuery, ApiResult<Models.Person>>
    {
        private readonly IPersonPort _personPort;

        public PersonGetByIdQueryHandler(IPersonPort personPort)
        {
            _personPort = personPort ?? throw new ArgumentNullException(nameof(personPort));
        }

        public async Task<ApiResult<Models.Person>> Handle(PersonGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _personPort.GetById(request.Id);
        }
    }
}