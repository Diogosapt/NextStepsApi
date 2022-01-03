using NextSteps.Business.Models;
using System;
using System.Collections.Generic;

namespace NextSteps.Api.Dto
{
    public record FilterPersonDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Surname { get; init; }

        public string Job { get; init; }

        public string Email { get; init; }

        public IEnumerable<Hobbies> Hobbies { get; init; }
    }
}