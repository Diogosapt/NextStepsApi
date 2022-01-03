using MediatR;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Models;
using NextSteps.Business.Ports;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Business.UsesCases
{
    public class PersonUpdateCommandHandler : IRequestHandler<PersonUpdateCommand, ApiResult<Person>>
    {
        private readonly IPersonPort _personPort;

        public PersonUpdateCommandHandler(IPersonPort personPort)
        {
            _personPort = personPort ?? throw new ArgumentNullException(nameof(personPort));
        }

        public async Task<ApiResult<Person>> Handle(PersonUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _personPort.UpdatePerson(request.Person);
        }
    }
}