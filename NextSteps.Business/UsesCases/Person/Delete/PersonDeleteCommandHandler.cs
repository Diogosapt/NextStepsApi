using MediatR;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Ports;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Business.UsesCases
{
    public record PersonDeleteCommandHandler : IRequestHandler<PersonDeleteCommand, ApiResult>
    {
        private readonly IPersonPort _personPort;

        public PersonDeleteCommandHandler(IPersonPort personPort)
        {
            _personPort = personPort ?? throw new ArgumentNullException(nameof(personPort));
        }

        public async Task<ApiResult> Handle(PersonDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _personPort.DeletePerson(request.Id);
        }
    }
}