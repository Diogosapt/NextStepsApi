using MediatR;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Models;
using NextSteps.Business.Ports;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Business.UsesCases
{
    public class PersonCreateCommandHandler : IRequestHandler<PersonCreateCommand, ApiResult<Person>>
    {
        private readonly IPersonPort _personService;

        public PersonCreateCommandHandler(IPersonPort personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        public async Task<ApiResult<Person>> Handle(PersonCreateCommand request, CancellationToken cancellationToken)
        {
            return await _personService.AddPerson(request.Person);
        }
    }
}